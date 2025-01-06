using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<RoleAggregate, Role> _repository;
        private readonly IMapper _mapper;

        public RoleService(IRepository<RoleAggregate, Role> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<RoleDTO>> GetAllRolesAsync(RequestParams requestParams)
        {
            var items = await _repository.GetAllAsync(requestParams);

            return _mapper.Map<List<RoleDTO>>(items);
        }

        public async Task<RoleDTO> GetRoleByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);

            return _mapper.Map<RoleDTO>(item);
        }

        public async Task CreateRoleAsync(RoleCreateDTO dto)
        {
            var item = _mapper.Map<Role>(dto);
            var aggregate = new RoleAggregate(item);
            aggregate.CreateRole(item);

            await _repository.InsertAsync(aggregate);
        }

        public async Task UpdateRoleAsync(RoleUpdateDTO dto)
        {
            var item = _mapper.Map<Role>(dto);
            var aggregate = new RoleAggregate(item);
            aggregate.UpdateRole(item);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task DeleteRoleAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new RoleAggregate(item);
            aggregate.DeleteRole();

            await _repository.DeleteAsync(item);
        }
    }
}