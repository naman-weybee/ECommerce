using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Services;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Services
{
    public class CityService : ICityService
    {
        private readonly IRepository<CityAggregate, City> _repository;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public CityService(IRepository<CityAggregate, City> repository, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<CityDTO>> GetAllCitiesAsync(RequestParams requestParams)
        {
            var items = await _repository.GetAllAsync(requestParams);

            return _mapper.Map<List<CityDTO>>(items);
        }

        public async Task<CityDTO> GetCityByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);

            return _mapper.Map<CityDTO>(item);
        }

        public async Task CreateCityAsync(CityCreateDTO dto)
        {
            var item = _mapper.Map<City>(dto);
            var aggregate = new CityAggregate(item, _eventCollector);
            aggregate.CreateCity(item);

            await _repository.InsertAsync(aggregate);
        }

        public async Task UpdateCityAsync(CityUpdateDTO dto)
        {
            var item = _mapper.Map<City>(dto);
            var aggregate = new CityAggregate(item, _eventCollector);
            aggregate.UpdateCity(item);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task DeleteCityAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new CityAggregate(item, _eventCollector);
            aggregate.DeleteCity();

            await _repository.DeleteAsync(item);
        }
    }
}
