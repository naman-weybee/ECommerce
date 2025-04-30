using AutoMapper;
using ECommerce.Application.DTOs.Address;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Infrastructure.Services;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IRepository<Address> _repository;
        private readonly IServiceHelper<Address> _serviceHelper;
        private readonly ILocationHierarchyCacheService _locationHierarchyCacheService;
        private readonly ITransactionManagerService _transactionManagerService;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public AddressService(IRepository<Address> repository, IServiceHelper<Address> serviceHelper, ILocationHierarchyCacheService locationHierarchyCacheService, ITransactionManagerService transactionManagerService, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _serviceHelper = serviceHelper;
            _locationHierarchyCacheService = locationHierarchyCacheService;
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

        public async Task UpsertAddressAsync(AddressUpsertDTO dto)
        {
            if (!await _locationHierarchyCacheService.IsValidLocationAsync(dto.CountryId, dto.StateId, dto.CityId))
                throw new InvalidOperationException($"Invalid location hierarchy: CountryId = {dto.CountryId}, StateId = {dto.StateId}, CityId = {dto.CityId}.");

            var item = await _repository.GetByIdAsync(dto.Id);
            bool isNew = item == null;

            item = _mapper.Map(dto, item)!;
            var aggregate = new AddressAggregate(item, _eventCollector);

            if (isNew)
            {
                aggregate.CreateAddress();
                await _repository.InsertAsync(aggregate.Entity);
            }
            else
            {
                aggregate.UpdateAddress();
            }
        }

        public async Task UpdateAddressTypeAsync(AddressTypeUpdateDTO dto)
        {
            // Begin Transaction
            await _transactionManagerService.BeginTransactionAsync();

            try
            {
                var query = _repository.GetQuery()
                    .Where(x => x.UserId == dto.UserId);

                var addresses = await _serviceHelper.GetAllAsync(query: query)
                    ?? throw new InvalidOperationException($"No Address found for User Id = {dto.UserId}.");

                // Get the address to update
                var address = addresses.FirstOrDefault(x => x.UserId == dto.UserId && x.Id == dto.Id)
                    ?? throw new InvalidOperationException($"Address with Id = {dto.Id} for User Id = {dto.UserId} does not exist.");

                // Check if the address type is default
                if (dto.AdderessType == eAddressType.Default)
                {
                    var previousDefaultAddress = addresses.FirstOrDefault(x => x.UserId == dto.UserId && x.AdderessType == eAddressType.Default && x.Id != dto.Id);

                    if (previousDefaultAddress != null)
                        SetAddressType(previousDefaultAddress, eAddressType.Billing);
                }

                // Update the address
                SetAddressType(address, dto.AdderessType);

                // Save
                await _repository.SaveChangesAsync();

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

        public async Task DeleteAddressByUserAsync(Guid id, Guid userId)
        {
            var query = _repository.GetQuery()
                .Where(x => x.UserId == userId);

            var item = await _serviceHelper.GetByIdAsync(id, query);
            var aggregate = new AddressAggregate(item, _eventCollector);
            aggregate.DeleteAddress();

            _repository.Delete(aggregate.Entity);
        }

        public async Task DeleteAddressAsync(Guid id)
        {
            var item = await _serviceHelper.GetByIdAsync(id);
            var aggregate = new AddressAggregate(item, _eventCollector);
            aggregate.DeleteAddress();

            _repository.Delete(aggregate.Entity);
        }

        private void SetAddressType(Address address, eAddressType addressType)
        {
            var aggregate = new AddressAggregate(address, _eventCollector);
            aggregate.UpdateAddressType(addressType);
        }
    }
}