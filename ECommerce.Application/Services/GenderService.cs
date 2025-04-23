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
    public class GenderService : IGenderService
    {
        private readonly IRepository<Gender> _repository;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public GenderService(IRepository<Gender> repository, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<GenderDTO>> GetAllGendersAsync(RequestParams requestParams)
        {
            var items = await _repository.GetAllAsync(requestParams);

            return _mapper.Map<List<GenderDTO>>(items);
        }

        public async Task<GenderDTO> GetGenderByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);

            return _mapper.Map<GenderDTO>(item);
        }

        public async Task CreateGenderAsync(GenderCreateDTO dto)
        {
            var item = _mapper.Map<Gender>(dto);
            var aggregate = new GenderAggregate(item, _eventCollector);
            aggregate.CreateGender(item);

            await _repository.InsertAsync(aggregate.Entity);
        }

        public async Task UpdateGenderAsync(GenderUpdateDTO dto)
        {
            var item = _mapper.Map<Gender>(dto);
            var aggregate = new GenderAggregate(item, _eventCollector);
            aggregate.UpdateGender(item);

            await _repository.UpdateAsync(aggregate.Entity);
        }

        public async Task DeleteGenderAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new GenderAggregate(item, _eventCollector);
            aggregate.DeleteGender();

            await _repository.DeleteAsync(item);
        }
    }
}