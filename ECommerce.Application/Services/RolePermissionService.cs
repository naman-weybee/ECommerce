using AutoMapper;
using ECommerce.Application.DTOs.RolePermission;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Infrastructure.Services;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IRepository<RolePermission> _repository;
        private readonly IServiceHelper<RolePermission> _serviceHelper;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public RolePermissionService(IRepository<RolePermission> repository, IServiceHelper<RolePermission> serviceHelper, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _serviceHelper = serviceHelper;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<RolePermissionDTO>> GetAllRolePermissionsAsync(RequestParams? requestParams = null, bool useQuery = false)
        {
            var query = useQuery
                ? _repository.GetQuery().Include(x => x.RoleEntity)
                : null!;

            var items = await _serviceHelper.GetAllAsync(requestParams, query);

            return _mapper.Map<List<RolePermissionDTO>>(items);
        }

        public async Task<List<RolePermissionDTO>> GetAllRolePermissionsByRoleAsync(Guid roleId, RequestParams? requestParams = null, bool useQuery = false, bool isSortByPermission = false)
        {
            var query = _repository.GetQuery()
                .Where(x => x.RoleId == roleId);

            if (useQuery)
                query = query.Include(x => x.RoleEntity);

            if (isSortByPermission)
                query = query.OrderByDescending(x => x.HasFullPermission).ThenBy(x => x.RoleEntityId);

            var items = await _serviceHelper.GetAllAsync(requestParams, query);

            return _mapper.Map<List<RolePermissionDTO>>(items);
        }

        public async Task<RolePermissionDTO> GetRolePermissionByIdsAsync(Guid roleId, eRoleEntity roleEntityId, bool useQuery = false)
        {
            var query = _repository.GetQuery()
                .Where(x => x.RoleId == roleId && x.RoleEntityId == roleEntityId);

            if (useQuery)
                query = query.Include(x => x.RoleEntity);

            var item = await _serviceHelper.GetByQueryAsync(query);

            return _mapper.Map<RolePermissionDTO>(item);
        }

        public async Task UpsertRolePermissionAsync(RolePermissionUpsertDTO dto)
        {
            var item = await _repository.GetQuery()
                .FirstOrDefaultAsync(x => x.RoleId == dto.RoleId && x.RoleEntityId == dto.RoleEntityId);

            bool isNew = item == null;

            item = _mapper.Map(dto, item)!;
            var aggregate = new RolePermissionAggregate(item, _eventCollector);

            if (isNew)
            {
                aggregate.CreateRolePermission();
                await _repository.InsertAsync(aggregate.Entity);
            }
            else
            {
                aggregate.UpdateRolePermission();
            }
        }

        public async Task DeleteRolePermissionByRoleAsync(Guid roleId)
        {
            var items = await GetAllRolePermissionsByRoleAsync(roleId);

            foreach (var item in items)
                await DeleteRolePermissionAsync(roleId, item.RoleEntityId);
        }

        public async Task DeleteRolePermissionAsync(Guid roleId, eRoleEntity roleEntityId)
        {
            var query = _repository.GetQuery()
                .Where(x => x.RoleId == roleId && x.RoleEntityId == roleEntityId);

            var item = await _serviceHelper.GetByQueryAsync(query);
            var aggregate = new RolePermissionAggregate(item, _eventCollector);
            aggregate.DeleteRolePermission();

            _repository.Delete(aggregate.Entity);
        }
    }
}