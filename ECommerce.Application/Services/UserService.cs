using AutoMapper;
using ECommerce.Application.DTOs.User;
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
            var query = useQuery
                ? _repository.GetQuery().Include(c => c.Role)!
                : null!;

            var items = await _serviceHelper.GetAllAsync(requestParams);

            return _mapper.Map<List<UserDTO>>(items);
        }

        public async Task<List<UserDTO>> GetAllActiveUsersAsync(RequestParams? requestParams = null, bool useQuery = false)
        {
            var query = useQuery
                ? _repository.GetQuery().Include(c => c.Role).Where(x => x.IsActive)!
                : null!;

            var items = await _serviceHelper.GetAllAsync(requestParams);

            return _mapper.Map<List<UserDTO>>(items);
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid id, bool useQuery = false)
        {
            var query = useQuery
                ? _repository.GetQuery().Include(c => c.Role)!
                : null!;

            var item = await _serviceHelper.GetByIdAsync(id, query);

            return _mapper.Map<UserDTO>(item);
        }

        public async Task<UserDTO> GetUserByEmailAsync(string email, bool useQuery = false)
        {
            var query = useQuery
                ? _repository.GetQuery().Where(x => x.Email == email)!
                : null!;

            var item = await _serviceHelper.GetByQueryAsync(query);

            return _mapper.Map<UserDTO>(item);
        }

        public async Task UpsertUserAsync(UserUpsertDTO dto)
        {
            // Begin Transaction
            await _transactionManagerService.BeginTransactionAsync();

            try
            {
                var item = await _repository.GetByIdAsync(dto.Id);
                bool isNew = item == null;

                item = _mapper.Map(dto, item)!;
                var aggregate = new UserAggregate(item, _eventCollector);

                if (isNew)
                {
                    aggregate.CreateUser();
                    await _repository.InsertAsync(aggregate.Entity);
                }
                else
                {
                    aggregate.UpdateUser();
                }

                // Save
                await _repository.SaveChangesAsync();

                // Commit transaction
                await _transactionManagerService.CommitTransactionAsync();

                // Send Email to User if Newly Registered
                if (isNew)
                    await _emailTemplates.SendVerificationEmailAsync(aggregate.User.Id);
            }
            catch
            {
                // Rollback transaction on error
                await _transactionManagerService.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task ReSendEmailVerificationAsync(Guid userId)
        {
            var item = await _repository.GetByIdAsync(userId);

            if (string.IsNullOrEmpty(item.EmailVerificationToken) || item.IsEmailVerified)
                throw new InvalidOperationException("User Email is already Verified");

            await _emailTemplates.SendVerificationEmailAsync(item.Id);
        }

        public async Task PasswordResetAsync(PasswordResetDTO dto)
        {
            // Begin Transaction
            await _transactionManagerService.BeginTransactionAsync();

            try
            {
                var otp = await _otpRepository.GetQuery()
                    .FirstOrDefaultAsync(x => x.Token == dto.Token && x.Type == eOTPType.PasswordReset && !x.IsUsed && x.TokenExpiredDate >= DateTime.UtcNow)
                    ?? throw new InvalidOperationException("Invalid Token.");

                var user = await _repository.GetQuery()
                    .FirstOrDefaultAsync(x => x.Id == otp.UserId)
                    ?? throw new InvalidOperationException("User not found.");

                var newPassword = _mD5Service.ComputeMD5Hash(dto.NewPassword);
                if (user.Password == newPassword)
                    throw new InvalidOperationException("New password cannot be the same as the old password.");

                user.Password = newPassword;
                var aggregate = new UserAggregate(user, _eventCollector);
                aggregate.UpdateUser();

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
            var item = await _serviceHelper.GetByIdAsync(id);
            var aggregate = new UserAggregate(item, _eventCollector);
            aggregate.DeleteUser();

            _repository.Delete(item);
        }

        public async Task VerifyEmailAsync(string token)
        {
            var query = _repository.GetQuery()
                .Where(x => x.EmailVerificationToken == token);

            var user = await _serviceHelper.GetByQueryAsync(query)
                ?? throw new InvalidOperationException("Invalid token.");

            user.EmailVerificationToken = null;
            user.IsEmailVerified = true;

            var aggregate = new UserAggregate(user, _eventCollector);
            aggregate.EmailVerified();
        }
    }
}