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
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _repository;
        private readonly IServiceHelper<Role> _serviceHelper;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public RoleService(IRepository<Role> repository, IServiceHelper<Role> serviceHelper, IUserService userService, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _serviceHelper = serviceHelper;
            _userService = userService;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<RoleDTO>> GetAllRolesAsync(RequestParams? requestParams = null)
        {
            var items = await _serviceHelper.GetAllAsync(requestParams);

            return _mapper.Map<List<RoleDTO>>(items);
        }

        public async Task<List<RoleDTO>> GetAllRolesByUserIdAsync(Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            var query = _repository.GetQuery()
                .Where(x => x.Id == user.RoleId)
                .OrderBy(x => x.RoleEntity)
                .ThenBy(x => x.HasFullPermission);

            var items = await _serviceHelper.GetAllAsync(query: query);

            return _mapper.Map<List<RoleDTO>>(items);
        }

        public async Task<RoleDTO> GetRoleByIdAsync(Guid id)
        {
            var item = await _serviceHelper.GetByIdAsync(id);

            return _mapper.Map<RoleDTO>(item);
        }

        public async Task<RoleDTO> GetSpecificRoleByUserAsync(Guid id, Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            var query = _repository.GetQuery()
                .Where(x => x.Id == user.RoleId);

            var item = await _serviceHelper.GetByIdAsync(id, query);

            return _mapper.Map<RoleDTO>(item);
        }

        public async Task CreateRoleAsync(RoleCreateDTO dto)
        {
            var item = _mapper.Map<Role>(dto);
            var aggregate = new RoleAggregate(item, _eventCollector);
            aggregate.CreateRole(item);

            await _repository.InsertAsync(aggregate.Entity);
        }

        public async Task UpdateRoleAsync(RoleUpdateDTO dto)
        {
            var item = _mapper.Map<Role>(dto);
            var aggregate = new RoleAggregate(item, _eventCollector);
            aggregate.UpdateRole(item);

            await _repository.UpdateAsync(aggregate.Entity);
        }

        public async Task DeleteRoleAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new RoleAggregate(item, _eventCollector);
            aggregate.DeleteRole();

            await _repository.DeleteAsync(item);
        }
    }
}