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
    public class RoleService : IRoleService
    {
        private readonly IRepository<RoleAggregate, Role> _repository;
        private readonly IRepository<UserAggregate, User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public RoleService(IRepository<RoleAggregate, Role> repository, IRepository<UserAggregate, User> userRepository, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<RoleDTO>> GetAllRolesAsync(RequestParams requestParams)
        {
            var items = await _repository.GetAllAsync(requestParams);

            return _mapper.Map<List<RoleDTO>>(items);
        }

        public async Task<List<RoleDTO>> GetAllRolesByUserIdAsync(Guid userId)
        {
            var user = await _userRepository.GetDbSet()
                    .SingleOrDefaultAsync(x => x.Id == userId)
                    ?? throw new InvalidOperationException($"User with Id = {userId} is not Exist.");

            var items = await _repository.GetDbSet()
                .Where(x => x.Id == user.RoleId)
                .OrderBy(x => x.RoleEntity)
                .ThenBy(x => x.HasFullPermission)
                .ToListAsync();

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
            var aggregate = new RoleAggregate(item, _eventCollector);
            aggregate.CreateRole(item);

            await _repository.InsertAsync(aggregate);
        }

        public async Task UpdateRoleAsync(RoleUpdateDTO dto)
        {
            var item = _mapper.Map<Role>(dto);
            var aggregate = new RoleAggregate(item, _eventCollector);
            aggregate.UpdateRole(item);

            await _repository.UpdateAsync(aggregate);
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