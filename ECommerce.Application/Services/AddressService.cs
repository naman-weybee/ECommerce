using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IRepository<AddressAggregate, Address> _repository;
        private readonly IMapper _mapper;

        public AddressService(IRepository<AddressAggregate, Address> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<AddressDTO>> GetAllAddressesAsync(RequestParams requestParams)
        {
            var items = await _repository.GetAllAsync(requestParams);

            return _mapper.Map<List<AddressDTO>>(items);
        }

        public async Task<AddressDTO> GetAddressByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);

            return _mapper.Map<AddressDTO>(item);
        }

        public async Task CreateAddressAsync(AddressCreateDTO dto)
        {
            var item = _mapper.Map<Address>(dto);
            var aggregate = new AddressAggregate(item);
            aggregate.CreateAddress(item);

            await _repository.InsertAsync(aggregate);
        }

        public async Task UpdateAddressAsync(AddressUpdateDTO dto)
        {
            var item = _mapper.Map<Address>(dto);
            var aggregate = new AddressAggregate(item);
            aggregate.UpdateAddress(item);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task DeleteAddressAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new AddressAggregate(item);
            aggregate.DeleteAddress();

            await _repository.DeleteAsync(item);
        }
    }
}