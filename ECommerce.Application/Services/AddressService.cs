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
    public class AddressService : IAddressService
    {
        private readonly IRepository<Address> _repository;
        private readonly IServiceHelper<Address> _serviceHelper;
        private readonly ITransactionManagerService _transactionManagerService;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public AddressService(IRepository<Address> repository, IServiceHelper<Address> serviceHelper, ITransactionManagerService transactionManagerService, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _serviceHelper = serviceHelper;
            _transactionManagerService = transactionManagerService;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<AddressDTO>> GetAllAddressesAsync(RequestParams? requestParams = null)
        {
            var items = await _serviceHelper.GetAllAsync(requestParams);

            return _mapper.Map<List<AddressDTO>>(items);
        }

        public async Task<List<AddressDTO>> GetAllAddressesByUserAsync(Guid userId, RequestParams? requestParams = null)
        {
            var query = _repository.GetQuery()
                .Where(x => x.UserId == userId);

            var items = await _serviceHelper.GetAllAsync(requestParams, query);

            return _mapper.Map<List<AddressDTO>>(items);
        }

        public async Task<AddressDTO> GetAddressByIdAsync(Guid id)
        {
            var item = await _serviceHelper.GetByIdAsync(id);

            return _mapper.Map<AddressDTO>(item);
        }

        public async Task<AddressDTO> GetSpecificAddressByUserAsync(Guid id, Guid userId)
        {
            var query = _repository.GetQuery()
                .Where(x => x.UserId == userId);

            var item = await _serviceHelper.GetByIdAsync(id, query);

            return _mapper.Map<AddressDTO>(item);
        }

        public async Task CreateAddressAsync(AddressCreateDTO dto)
        {
            var item = _mapper.Map<Address>(dto);
            var aggregate = new AddressAggregate(item, _eventCollector);
            aggregate.CreateAddress(item);

            await _repository.InsertAsync(aggregate.Entity);
        }

        public async Task UpdateAddressAsync(AddressUpdateDTO dto)
        {
            var item = _mapper.Map<Address>(dto);
            var aggregate = new AddressAggregate(item, _eventCollector);
            aggregate.UpdateAddress(item);

            await _repository.UpdateAsync(aggregate.Entity);
        }

        public async Task UpdateAddressTypeAsync(AddressTypeUpdateDTO dto)
        {
            // Begin Transaction
            await _transactionManagerService.BeginTransactionAsync();

            try
            {
                var query = _repository.GetQuery();

                // Get all addresses for the user
                var addresses = await query.Where(x => x.UserId == dto.UserId).ToListAsync()
                    ?? throw new InvalidOperationException($"No Address found for User Id = {dto.UserId}.");

                // Get the address to update
                var address = addresses.SingleOrDefault(x => x.UserId == dto.UserId && x.Id == dto.Id)
                    ?? throw new InvalidOperationException($"Address with Id = {dto.Id} for User Id = {dto.UserId} does not exist.");

                // Check if the address type is default
                if (dto.AdderessType == eAddressType.Default)
                {
                    var previousDefaultAddress = addresses.SingleOrDefault(x => x.UserId == dto.UserId && x.AdderessType == eAddressType.Default && x.Id != dto.Id);

                    if (previousDefaultAddress != null)
                        await SetAddressTypeAsync(previousDefaultAddress, eAddressType.Billing);
                }

                // Update the address
                await SetAddressTypeAsync(address, dto.AdderessType);

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

        public async Task DeleteAddressAsync(Guid id, Guid userId)
        {
            var query = _repository.GetQuery()
                .Where(x => x.UserId == userId);

            var item = await _repository.GetByIdAsync(id, query);
            var aggregate = new AddressAggregate(item, _eventCollector);
            aggregate.DeleteAddress();

            await _repository.DeleteAsync(item);
        }

        private async Task SetAddressTypeAsync(Address address, eAddressType addressType)
        {
            var aggregate = new AddressAggregate(address, _eventCollector);
            aggregate.UpdateAddressType(addressType);

            await _repository.UpdateAsync(aggregate.Entity);
        }
    }
}