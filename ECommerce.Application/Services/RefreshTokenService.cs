using AutoMapper;
using ECommerce.Application.DTOs.RefreshToken;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Services;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRepository<RefreshToken> _repository;
        private readonly IServiceHelper<RefreshToken> _serviceHelper;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public RefreshTokenService(IRepository<RefreshToken> repository, IServiceHelper<RefreshToken> serviceHelper, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _serviceHelper = serviceHelper;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<RefreshTokenDTO>> GetAllRefreshTokensAsync(RequestParams? requestParams = null)
        {
            var items = await _serviceHelper.GetAllAsync(requestParams);

            return _mapper.Map<List<RefreshTokenDTO>>(items);
        }

        public async Task<List<RefreshTokenDTO>> GetAllRefreshTokensByUserAsync(Guid userId, RequestParams? requestParams = null)
        {
            var query = _repository.GetQuery()
                .Where(x => x.UserId == userId);

            var items = await _serviceHelper.GetAllAsync(requestParams, query);

            return _mapper.Map<List<RefreshTokenDTO>>(items);
        }

        public async Task<RefreshTokenDTO> GetRefreshTokenByIdAsync(Guid id)
        {
            var item = await _serviceHelper.GetByIdAsync(id);

            return _mapper.Map<RefreshTokenDTO>(item);
        }

        public async Task<RefreshTokenDTO> GetSpecificRefreshTokenByUserAsync(Guid id, Guid userId)
        {
            var query = _repository.GetQuery()
                .Where(x => x.UserId == userId);

            var item = await _serviceHelper.GetByIdAsync(id, query);

            return _mapper.Map<RefreshTokenDTO>(item);
        }

        public async Task<RefreshTokenDTO> UpsertRefreshTokenAsync(RefreshTokenUpsertDTO dto)
        {
            var item = await _repository.GetByIdAsync(dto.Id);
            bool isNew = item == null;

            item = _mapper.Map(dto, item)!;
            var aggregate = new RefreshTokenAggregate(item, _eventCollector);

            if (isNew)
            {
                aggregate.CreateRefreshToken(item.UserId);
                await _repository.InsertAsync(aggregate.Entity);
            }
            else
            {
                aggregate.UpdateRefreshToken();
            }

            return await GetRefreshTokenByIdAsync(aggregate.RefreshToken.Id);
        }

        public async Task DeleteRefreshTokenAsync(Guid id, Guid userId)
        {
            var query = _repository.GetQuery()
                .Where(x => x.UserId == userId);

            var item = await _serviceHelper.GetByIdAsync(id, query);
            var aggregate = new RefreshTokenAggregate(item, _eventCollector);
            aggregate.DeleteRefreshToken();

            _repository.Delete(item);
        }

        public async Task RevokeRefreshTokenAsync(RevokeRefreshTokenDTO dto)
        {
            var query = _repository.GetQuery()
                .Where(x => x.Token == dto.RefreshToken);

            var item = await _serviceHelper.GetByQueryAsync(query);
            var aggregate = new RefreshTokenAggregate(item, _eventCollector);
            aggregate.RevokeToken();
        }
    }
}