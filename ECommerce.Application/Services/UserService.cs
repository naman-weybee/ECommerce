using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Services;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserAggregate, User> _repository;
        private readonly IEmailTemplates _emailTemplates;
        private readonly ITransactionManagerService _transactionManagerService;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public UserService(IRepository<UserAggregate, User> repository, IEmailTemplates emailTemplates, ITransactionManagerService transactionManagerService, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _emailTemplates = emailTemplates;
            _transactionManagerService = transactionManagerService;
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
            // Begin Transaction
            await _transactionManagerService.BeginTransactionAsync();

            try
            {
                var item = _mapper.Map<User>(dto);
                item.EmailVerificationToken = Guid.NewGuid().ToString();

                var aggregate = new UserAggregate(item, _eventCollector);
                aggregate.CreateUser(item);

                await _repository.InsertAsync(aggregate);

                // Commit transaction
                await _transactionManagerService.CommitTransactionAsync();

                // Send Email to User
                await _emailTemplates.SendVerificationEmailAsync(aggregate.User.Id);
            }
            catch (Exception)
            {
                // Rollback transaction on error
                await _transactionManagerService.RollbackTransactionAsync();
                throw;
            }
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
    }
}