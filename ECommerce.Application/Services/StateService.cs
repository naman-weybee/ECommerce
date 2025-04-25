using AutoMapper;
using ECommerce.Application.DTOs.State;
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
        private readonly IRepository<State> _repository;
        private readonly IServiceHelper<State> _serviceHelper;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public StateService(IRepository<State> repository, IServiceHelper<State> serviceHelper, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _serviceHelper = serviceHelper;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<StateDTO>> GetAllStatesAsync(RequestParams? requestParams = null, bool useQuery = false)
        {
            var query = useQuery
                ? _repository.GetQuery().Include(c => c.Cities)!
                : null!;

            var items = await _serviceHelper.GetAllAsync(requestParams);

            return _mapper.Map<List<StateDTO>>(items);
        }

        public async Task<List<StateDTO>> GetAllStatesByCountryIdAsync(Guid countryId, RequestParams? requestParams = null)
        {
            var query = _repository.GetQuery()
                .Where(x => x.CountryId == countryId);

            var items = await _serviceHelper.GetAllAsync(requestParams, query);

            return _mapper.Map<List<StateDTO>>(items);
        }

        public async Task<StateDTO> GetStateByIdAsync(Guid id)
        {
            var item = await _serviceHelper.GetByIdAsync(id);

            return _mapper.Map<StateDTO>(item);
        }

        public async Task UpsertStateAsync(StateUpsertDTO dto)
        {
            var item = await _repository.GetByIdAsync(dto.Id);
            bool isNew = item == null;

            item = _mapper.Map(dto, item)!;
            var aggregate = new StateAggregate(item, _eventCollector);

            if (isNew)
            {
                aggregate.CreateState();
                await _repository.InsertAsync(aggregate.Entity);
            }
            else
            {
                aggregate.UpdateState();
            }

            await _repository.SaveChangesAsync();
        }

        public async Task DeleteStateAsync(Guid id)
        {
            var item = await _serviceHelper.GetByIdAsync(id);
            var aggregate = new StateAggregate(item, _eventCollector);
            aggregate.DeleteState();

            _repository.Delete(item);
            await _repository.SaveChangesAsync();
        }
    }
}