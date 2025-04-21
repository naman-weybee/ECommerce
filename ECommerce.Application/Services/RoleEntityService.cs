using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Services
{
    public class RoleEntityService : IRoleEntityService
    {
        private readonly IRepository<RoleEntityAggregate, RoleEntity> _repository;

        public RoleEntityService(IRepository<RoleEntityAggregate, RoleEntity> repository)
        {
            _repository = repository;
        }

        public async Task<List<RoleEntityDTO>> GetAllRoleEntitiesAsync(RequestParams requestParams)
        {
            var items = await _repository.GetAllAsync(requestParams);

            return items?.Select(x => new RoleEntityDTO
            {
                Id = x.Id,
                Name = x.Name,
            })?.ToList()!;
        }
    }
}