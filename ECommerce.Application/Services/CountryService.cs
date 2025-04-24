using AutoMapper;
using ECommerce.Application.DTOs.Country;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Services;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> _repository;
        private readonly IServiceHelper<Country> _serviceHelper;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public CountryService(IRepository<Country> repository, IServiceHelper<Country> serviceHelper, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _serviceHelper = serviceHelper;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<CountryDTO>> GetAllCountriesAsync(RequestParams? requestParams = null, bool useQuery = false)
        {
            var query = useQuery
                ? _repository.GetQuery().Include(c => c.States)!
                : null!;

            var items = await _serviceHelper.GetAllAsync(requestParams, query);

            return _mapper.Map<List<CountryDTO>>(items);
        }

        public async Task<CountryDTO> GetCountryByIdAsync(Guid id, bool useQuery = false)
        {
            var query = useQuery
                ? _repository.GetQuery().Include(c => c.States)!
                : null!;

            var item = await _serviceHelper.GetByIdAsync(id, query);

            return _mapper.Map<CountryDTO>(item);
        }

        public async Task CreateCountryAsync(CountryCreateDTO dto)
        {
            var item = _mapper.Map<Country>(dto);
            var aggregate = new CountryAggregate(item, _eventCollector);
            aggregate.CreateCountry(item);

            await _repository.InsertAsync(aggregate.Entity);
        }

        public async Task UpdateCountryAsync(CountryUpdateDTO dto)
        {
            var item = _mapper.Map<Country>(dto);
            var aggregate = new CountryAggregate(item, _eventCollector);
            aggregate.UpdateCountry(item);

            await _repository.UpdateAsync(aggregate.Entity);
        }

        public async Task DeleteCountryAsync(Guid id)
        {
            var item = await _serviceHelper.GetByIdAsync(id);
            var aggregate = new CountryAggregate(item, _eventCollector);
            aggregate.DeleteCountry();

            await _repository.DeleteAsync(item);
        }
    }
}