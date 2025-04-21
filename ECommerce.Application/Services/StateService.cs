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
    public class StateService : IStateService
    {
        private readonly IRepository<StateAggregate, State> _repository;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public StateService(IRepository<StateAggregate, State> repository, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<StateDTO>> GetAllStatesAsync(RequestParams requestParams)
        {
            var query = _repository.GetQuery()
                .Include(x => x.Cities);

            var items = await _repository.GetAllAsync(requestParams, query);

            return _mapper.Map<List<StateDTO>>(items);
        }

        public async Task<List<StateDTO>> GetAllStatesByCountryIdAsync(Guid countryID)
        {
            var items = await _repository.GetQuery()
                .Where(x => x.CountryId == countryID).Include(x => x.Cities).ToListAsync();

            if (items.Count == 0)
                throw new InvalidOperationException($"No State Found for Country Id = {countryID}");

            return _mapper.Map<List<StateDTO>>(items);
        }

        public async Task<StateDTO> GetStateByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);

            return _mapper.Map<StateDTO>(item);
        }

        public async Task CreateStateAsync(StateCreateDTO dto)
        {
            var item = _mapper.Map<State>(dto);
            var aggregate = new StateAggregate(item, _eventCollector);
            aggregate.CreateState(item);

            await _repository.InsertAsync(aggregate);
        }

        public async Task UpdateStateAsync(StateUpdateDTO dto)
        {
            var item = _mapper.Map<State>(dto);
            var aggregate = new StateAggregate(item, _eventCollector);
            aggregate.UpdateState(item);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task DeleteStateAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new StateAggregate(item, _eventCollector);
            aggregate.DeleteState();

            await _repository.DeleteAsync(item);
        }
    }
}