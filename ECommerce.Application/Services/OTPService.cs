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
    public class OTPService : IOTPService
    {
        private readonly IRepository<OTPAggregate, OTP> _repository;
        private readonly IRepository<UserAggregate, User> _userRepository;
        private readonly IEmailTemplates _emailTemplates;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public OTPService(IRepository<OTPAggregate, OTP> repository, IRepository<UserAggregate, User> userRepository, IEmailTemplates emailTemplates, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _userRepository = userRepository;
            _emailTemplates = emailTemplates;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<OTPDTO>> GetAllOTPAsync(RequestParams requestParams)
        {
            var items = await _repository.GetAllAsync(requestParams);

            return _mapper.Map<List<OTPDTO>>(items);
        }

        public async Task<OTPDTO> GetOTPByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);

            return _mapper.Map<OTPDTO>(item);
        }

        public async Task CreateOTPAsync(OTPCreateFromEmailDTO dto)
        {
            var user = await _userRepository.GetDbSet()
                .SingleOrDefaultAsync(x => x.Email == dto.Email)
                ?? throw new InvalidOperationException($"User with Email = {dto.Email} is not registered.");

            var otp = new OTPCreateDTO()
            {
                UserId = user.Id,
                Type = dto.Type
            };

            var item = _mapper.Map<OTP>(otp);
            var aggregate = new OTPAggregate(item, _eventCollector);
            aggregate.CreateOTP(item);

            await _repository.InsertAsync(aggregate);

            //Send Email to User
            await _emailTemplates.SendOTPEmailAsync(aggregate.OTP.Id, dto.Email, dto.Type);
        }

        public async Task<OTPTokenDTO> VerifyOTPAsync(OTPVerifyDTO dto)
        {
            var user = await _userRepository.GetDbSet()
                    .SingleOrDefaultAsync(x => x.Email == dto.Email)
                    ?? throw new InvalidOperationException($"User with Email = {dto.Email} is not registered.");

            var otp = await _repository.GetDbSet()
                .SingleOrDefaultAsync(x => x.UserId == user.Id && x.Code == dto.Code && !x.IsUsed && x.OTPExpiredDate >= DateTime.UtcNow)
                ?? throw new InvalidOperationException("Invalid OTP.");

            var item = _mapper.Map<OTP>(otp);
            var aggregate = new OTPAggregate(item, _eventCollector);
            aggregate.VerifyOTP();

            await _repository.UpdateAsync(aggregate);

            return new OTPTokenDTO() { Token = aggregate.OTP.Token! };
        }

        public async Task UpdateOTPAsync(OTPUpdateDTO dto)
        {
            var item = _mapper.Map<OTP>(dto);
            var aggregate = new OTPAggregate(item, _eventCollector);
            aggregate.UpdateOTP(item);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task SetOTPIsUsedAsync(Guid otpId)
        {
            var otp = await _repository.GetDbSet()
                .SingleOrDefaultAsync(x => x.Id == otpId)
                ?? throw new InvalidOperationException("OTP not found.");

            var item = _mapper.Map<OTP>(otp);
            var aggregate = new OTPAggregate(item, _eventCollector);
            aggregate.MarkAsUsed();

            await _repository.UpdateAsync(aggregate);
        }

        public async Task DeleteOTPAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new OTPAggregate(item, _eventCollector);
            aggregate.DeleteOTP();

            await _repository.DeleteAsync(item);
        }
    }
}