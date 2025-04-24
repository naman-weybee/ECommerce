using AutoMapper;
using ECommerce.Application.DTOs.Gender;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Services;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Services
{
    public class GenderService : IGenderService
    {
        private readonly IRepository<Gender> _repository;
        private readonly IServiceHelper<Gender> _serviceHelper;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public GenderService(IRepository<Gender> repository, IServiceHelper<Gender> serviceHelper, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _serviceHelper = serviceHelper;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<GenderDTO>> GetAllGendersAsync(RequestParams? requestParams = null)
        {
            var items = await _serviceHelper.GetAllAsync(requestParams);

            return _mapper.Map<List<GenderDTO>>(items);
        }

        public async Task<GenderDTO> GetGenderByIdAsync(Guid id)
        {
            var item = await _serviceHelper.GetByIdAsync(id);

            return _mapper.Map<GenderDTO>(item);
        }

        public async Task UpsertGenderAsync(GenderUpsertDTO dto)
        {
            var item = await _repository.GetByIdAsync(dto.Id);
            bool isNew = item == null;

            item = _mapper.Map(dto, item)!;
            var aggregate = new GenderAggregate(item, _eventCollector);

            if (isNew)
            {
                aggregate.CreateGender(item);
                await _repository.InsertAsync(aggregate.Entity);
            }
            else
            {
                aggregate.UpdateGender(item!);
            }

            await _repository.SaveChangesAsync();
        }

        public async Task DeleteGenderAsync(Guid id)
        {
            var item = await _serviceHelper.GetByIdAsync(id);
            var aggregate = new GenderAggregate(item, _eventCollector);
            aggregate.DeleteGender();

            _repository.Delete(item);
            await _repository.SaveChangesAsync();
        }
    }
}