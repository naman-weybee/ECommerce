using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Services;
using ECommerce.Shared.Constants;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserAggregate, User> _repository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public UserService(IRepository<UserAggregate, User> repository, IEmailService emailService, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _emailService = emailService;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync(RequestParams requestParams)
        {
            var query = _repository.GetDbSet()
                .Include(u => u.Role);

            var items = await _repository.GetAllAsync(requestParams, query);

            return _mapper.Map<List<UserDTO>>(items);
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid id)
        {
            var query = _repository.GetDbSet()
                .Include(u => u.Role);

            var item = await _repository.GetByIdAsync(id, query);

            return _mapper.Map<UserDTO>(item);
        }

        public async Task CreateUserAsync(UserCreateDTO dto)
        {
            var item = _mapper.Map<User>(dto);

            item.EmailVerificationToken = Guid.NewGuid().ToString();

            var aggregate = new UserAggregate(item, _eventCollector);
            aggregate.CreateUser(item);

            await _repository.InsertAsync(aggregate);

            await SendVerificationEmail(item);
        }

        public async Task UpdateUserAsync(UserUpdateDTO dto)
        {
            var item = _mapper.Map<User>(dto);
            var aggregate = new UserAggregate(item, _eventCollector);
            aggregate.UpdateUser(item);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new UserAggregate(item, _eventCollector);
            aggregate.DeleteUser();

            await _repository.DeleteAsync(item);
        }

        public async Task VerifyEmailAsync(string token)
        {
            var query = _repository.GetDbSet();

            var user = await query.SingleOrDefaultAsync(x => x.EmailVerificationToken == token)
                ?? throw new InvalidOperationException("Invalid token.");

            user.EmailVerificationToken = null;
            user.IsEmailVerified = true;

            var aggregate = new UserAggregate(user, _eventCollector);
            aggregate.EmailVerified();

            await _repository.UpdateAsync(aggregate);
        }

        private async Task SendVerificationEmail(User user)
        {
            var verificationLink = $"https://{Constants.MyIpv4}/api/v1/Auth/verify-email?token={Uri.EscapeDataString(user.EmailVerificationToken!)}";

            var dto = new EmailSendDTO()
            {
                ReceiverEmail = user.Email,
                Subject = "Email Verification Required",
                Body = $@"
                        <p>Dear <b>{user.FirstName} {user.LastName}</b>,</p>
                        <p>Thank you for registering with us. To complete your registration, please verify your email address by clicking the link below:</p>
                        <p><a href='{verificationLink}' target='_blank'>Verify Email Address</a></p>
                        <p>If you did not request this verification, please ignore this email.</p>
                        <br/>
                        <p>Best regards,</p>
                        <p>ECommerce Pvt Ltd.</p>",
                IsHtml = true,
                Link = verificationLink
            };

            await _emailService.SendEmailAsync(dto);
        }
    }
}