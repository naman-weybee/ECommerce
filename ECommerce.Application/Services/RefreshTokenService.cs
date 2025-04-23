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
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRepository<RefreshToken> _repository;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public RefreshTokenService(IRepository<RefreshToken> repository, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<RefreshTokenDTO>> GetAllRefreshTokensAsync(RequestParams requestParams, Guid userId = default)
        {
            var query = _repository.GetQuery();

            if (userId != default)
                query = query.Where(x => x.UserId == userId);

            var items = await _repository.GetAllAsync(requestParams, query);

            return _mapper.Map<List<RefreshTokenDTO>>(items);
        }

        public async Task<RefreshTokenDTO> GetRefreshTokenByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);

            return _mapper.Map<RefreshTokenDTO>(item);
        }

        public async Task<RefreshTokenDTO> CreateRefreshTokenAsync(RefreshTokenCreateDTO dto)
        {
            var item = _mapper.Map<RefreshToken>(dto);
            var aggregate = new RefreshTokenAggregate(item, _eventCollector);
            aggregate.CreateRefreshToken(item.UserId);

            await _repository.InsertAsync(aggregate.Entity);

            return await GetRefreshTokenByIdAsync(aggregate.RefreshToken.Id);
        }

        public async Task UpdateRefreshTokenAsync(RefreshTokenUpdateDTO dto)
        {
            var item = _mapper.Map<RefreshToken>(dto);
            var aggregate = new RefreshTokenAggregate(item, _eventCollector);
            aggregate.UpdateRefreshToken(item);

            await _repository.UpdateAsync(aggregate.Entity);
        }

        public async Task DeleteRefreshTokenAsync(Guid id, Guid userId)
        {
            var query = _repository.GetQuery()
                .Where(x => x.UserId == userId);

            var item = await _repository.GetByIdAsync(id, query);
            var aggregate = new RefreshTokenAggregate(item, _eventCollector);
            aggregate.DeleteRefreshToken();

            await _repository.DeleteAsync(item);
        }

        public async Task RevokeRefreshTokenAsync(RevokeRefreshTokenDTO dto)
        {
            var entity = await _repository.GetQuery()
                .SingleOrDefaultAsync(x => x.Token == dto.RefreshToken);

            var refreshToken = _mapper.Map<RefreshToken>(entity);
            var aggregate = new RefreshTokenAggregate(refreshToken, _eventCollector);
            aggregate.RevokeToken();

            await _repository.UpdateAsync(aggregate.Entity);
        }
    }
}