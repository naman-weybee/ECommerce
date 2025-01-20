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
using System.Net.Mail;

namespace ECommerce.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserAggregate, User> _repository;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public UserService(IRepository<UserAggregate, User> repository, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync(RequestParams requestParams)
        {
            var query = _repository.GetDbSet();
            query = query.Include(u => u.Role);

            var items = await _repository.GetAllAsync(requestParams, query);

            return _mapper.Map<List<UserDTO>>(items);
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid id)
        {
            var query = _repository.GetDbSet();
            query = query.Include(u => u.Role);

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

            await SendVerificationEmail(item.Email, item.EmailVerificationToken);
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
            aggregate.VerifyEmail();

            await _repository.UpdateAsync(aggregate);
        }

        private static async Task SendVerificationEmail(string email, string token)
        {
            var verificationLink = $"https://{Constants.MyIpv4}/api/v1/User/verify-email?token={Uri.EscapeDataString(token)}";

            var subject = "Verify Your Email";
            var body = $"Please click the link to verify your email: {verificationLink}";

            using var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new System.Net.NetworkCredential(Constants.Email, Constants.Password),
                EnableSsl = true
            };

            using var message = new MailMessage(Constants.Email, email, subject, body);
            await smtpClient.SendMailAsync(message);
        }
    }
}