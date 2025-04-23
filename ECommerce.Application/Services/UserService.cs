using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Infrastructure.Services;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly IServiceHelper<User> _serviceHelper;
        private readonly IRepository<OTP> _otpRepository;
        private readonly IOTPService _otpService;
        private readonly IEmailTemplates _emailTemplates;
        private readonly ITransactionManagerService _transactionManagerService;
        private readonly IMD5Service _mD5Service;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public UserService(IRepository<User> repository, IServiceHelper<User> serviceHelper, IRepository<OTP> otpRepository, IOTPService otpService, IEmailTemplates emailTemplates, ITransactionManagerService transactionManagerService, IMD5Service mD5Service, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _serviceHelper = serviceHelper;
            _otpRepository = otpRepository;
            _otpService = otpService;
            _emailTemplates = emailTemplates;
            _transactionManagerService = transactionManagerService;
            _mD5Service = mD5Service;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync(RequestParams? requestParams = null, bool useQuery = false)
        {
            IQueryable<User> query = useQuery
                ? _repository.GetQuery().Include(c => c.Role)!
                : null!;

            var items = await _serviceHelper.GetAllAsync(requestParams);

            return _mapper.Map<List<UserDTO>>(items);
        }

        public async Task<List<UserDTO>> GetAllActiveUsersAsync(RequestParams? requestParams = null, bool useQuery = false)
        {
            IQueryable<User> query = useQuery
                ? _repository.GetQuery().Include(c => c.Role).Where(x => x.IsActive)!
                : null!;

            var items = await _serviceHelper.GetAllAsync(requestParams);

            return _mapper.Map<List<UserDTO>>(items);
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid id, bool useQuery = false)
        {
            IQueryable<User> query = useQuery
                ? _repository.GetQuery().Include(c => c.Role)!
                : null!;

            var item = await _serviceHelper.GetByIdAsync(id, query);

            return _mapper.Map<UserDTO>(item);
        }

        public async Task<UserDTO> GetUserByEmailAsync(string email, bool useQuery = false)
        {
            IQueryable<User> query = useQuery
                ? _repository.GetQuery().Where(x => x.Email == email)!
                : null!;

            var item = await _serviceHelper.GetByQueryAsync(query);

            return _mapper.Map<UserDTO>(item);
        }

        public async Task CreateUserAsync(UserCreateDTO dto)
        {
            // Begin Transaction
            await _transactionManagerService.BeginTransactionAsync();

            try
            {
                var item = _mapper.Map<User>(dto);

                var aggregate = new UserAggregate(item, _eventCollector);
                aggregate.CreateUser(item);

                await _repository.InsertAsync(aggregate.Entity);

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

            await _repository.UpdateAsync(aggregate.Entity);
        }

        public async Task PasswordResetAsync(PasswordResetDTO dto)
        {
            // Begin Transaction
            await _transactionManagerService.BeginTransactionAsync();

            try
            {
                var otp = await _otpRepository.GetQuery()
                    .SingleOrDefaultAsync(x => x.Token == dto.Token && x.Type == eOTPType.PasswordReset && !x.IsUsed && x.TokenExpiredDate >= DateTime.UtcNow)
                    ?? throw new InvalidOperationException("Invalid Token.");

                var user = await _repository.GetQuery()
                    .SingleOrDefaultAsync(x => x.Id == otp.UserId)
                    ?? throw new InvalidOperationException("User not found.");

                var newPassword = _mD5Service.ComputeMD5Hash(dto.NewPassword);
                if (user.Password == newPassword)
                    throw new InvalidOperationException("New password cannot be the same as the old password.");

                user.Password = newPassword;
                var aggregate = new UserAggregate(user, _eventCollector);
                aggregate.UpdateUser(user);

                await _repository.UpdateAsync(aggregate.Entity);

                // Mark OTP as used
                await _otpService.SetOTPIsUsedAsync(otp.Id);

                // Commit transaction
                await _transactionManagerService.CommitTransactionAsync();
            }
            catch (Exception)
            {
                // Rollback transaction on error
                await _transactionManagerService.RollbackTransactionAsync();
                throw;
            }
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
            var query = _repository.GetQuery();

            var user = await query.SingleOrDefaultAsync(x => x.EmailVerificationToken == token)
                ?? throw new InvalidOperationException("Invalid token.");

            user.EmailVerificationToken = null;
            user.IsEmailVerified = true;

            var aggregate = new UserAggregate(user, _eventCollector);
            aggregate.EmailVerified();

            await _repository.UpdateAsync(aggregate.Entity);
        }
    }
}