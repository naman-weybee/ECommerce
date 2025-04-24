using AutoMapper;
using ECommerce.Application.DTOs.City;
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
        private readonly IRepository<City> _repository;
        private readonly IServiceHelper<City> _serviceHelper;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public CityService(IRepository<City> repository, IServiceHelper<City> serviceHelper, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _serviceHelper = serviceHelper;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<CityDTO>> GetAllCitiesAsync(RequestParams? requestParams = null)
        {
            var items = await _serviceHelper.GetAllAsync(requestParams);

            return _mapper.Map<List<CityDTO>>(items);
        }

        public async Task<List<CityDTO>> GetAllCitiesByStateIdAsync(Guid stateId, RequestParams? requestParams = null)
        {
            var query = _repository.GetQuery()
                .Where(x => x.StateId == stateId);

            var items = await _serviceHelper.GetAllAsync(requestParams, query);

            return _mapper.Map<List<CityDTO>>(items);
        }

        public async Task<CityDTO> GetCityByIdAsync(Guid id)
        {
            var item = await _serviceHelper.GetByIdAsync(id);

            return _mapper.Map<CityDTO>(item);
        }

        public async Task CreateCityAsync(CityCreateDTO dto)
        {
            var item = _mapper.Map<City>(dto);
            var aggregate = new CityAggregate(item, _eventCollector);
            aggregate.CreateCity(item);

            await _repository.InsertAsync(aggregate.Entity);
        }

        public async Task UpdateCityAsync(CityUpdateDTO dto)
        {
            var item = _mapper.Map<City>(dto);
            var aggregate = new CityAggregate(item, _eventCollector);
            aggregate.UpdateCity(item);

            await _repository.UpdateAsync(aggregate.Entity);
        }

        public async Task DeleteCityAsync(Guid id)
        {
            var item = await _serviceHelper.GetByIdAsync(id);
            var aggregate = new CityAggregate(item, _eventCollector);
            aggregate.DeleteCity();

            await _repository.DeleteAsync(item);
        }
    }
}