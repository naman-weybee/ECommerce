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
    public class CountryService : ICountryService
    {
        private readonly IRepository<CountryAggregate, Country> _repository;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public CountryService(IRepository<CountryAggregate, Country> repository, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<CountryDTO>> GetAllCountriesAsync(RequestParams requestParams)
        {
            var query = _repository.GetDbSet().Include(x => x.States);

            var items = await _repository.GetAllAsync(requestParams, query);

            return _mapper.Map<List<CountryDTO>>(items);
        }

        public async Task<CountryDTO> GetCountryByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);

            return _mapper.Map<CountryDTO>(item);
        }

        public async Task CreateCountryAsync(CountryCreateDTO dto)
        {
            var item = _mapper.Map<Country>(dto);
            var aggregate = new CountryAggregate(item, _eventCollector);
            aggregate.CreateCountry(item);

            await _repository.InsertAsync(aggregate);
        }

        public async Task UpdateCountryAsync(CountryUpdateDTO dto)
        {
            var item = _mapper.Map<Country>(dto);
            var aggregate = new CountryAggregate(item, _eventCollector);
            aggregate.UpdateCountry(item);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task DeleteCountryAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new CountryAggregate(item, _eventCollector);
            aggregate.DeleteCountry();

            await _repository.DeleteAsync(item);
        }
    }
}