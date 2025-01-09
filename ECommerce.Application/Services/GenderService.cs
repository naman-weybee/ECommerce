using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;
using MediatR;

namespace ECommerce.Application.Services
{
    public class GenderService : IGenderService
    {
        private readonly IRepository<GenderAggregate, Gender> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GenderService(IRepository<GenderAggregate, Gender> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
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
            var aggregate = new GenderAggregate(item, _mediator);
            await aggregate.CreateGender(item);

            await _repository.InsertAsync(aggregate);
        }

        public async Task UpdateGenderAsync(GenderUpdateDTO dto)
        {
            var item = _mapper.Map<Gender>(dto);
            var aggregate = new GenderAggregate(item, _mediator);
            await aggregate.UpdateGender(item);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task DeleteGenderAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new GenderAggregate(item, _mediator);
            await aggregate.DeleteGender();

            await _repository.DeleteAsync(item);
        }
    }
}