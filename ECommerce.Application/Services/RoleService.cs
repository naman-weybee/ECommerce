using AutoMapper;
using ECommerce.Application.DTOs.Role;
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
        private readonly IRepository<Role> _repository;
        private readonly IServiceHelper<Role> _serviceHelper;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IDomainEventCollector _eventCollector;

        public RoleService(IRepository<Role> repository, IServiceHelper<Role> serviceHelper, IRepository<User> userRepository, IMapper mapper, IDomainEventCollector eventCollector)
        {
            _repository = repository;
            _serviceHelper = serviceHelper;
            _userRepository = userRepository;
            _mapper = mapper;
            _eventCollector = eventCollector;
        }

        public async Task<List<RoleDTO>> GetAllRolesAsync(RequestParams? requestParams = null)
        {
            var items = await _serviceHelper.GetAllAsync(requestParams);

            return _mapper.Map<List<RoleDTO>>(items);
        }

        public async Task<RoleDTO> GetRoleByUserIdAsync(Guid userId)
        {
            var user = await _userRepository.GetQuery()
                    .FirstOrDefaultAsync(x => x.Id == userId)
                    ?? throw new InvalidOperationException($"User with Id = {userId} is not Exist.");

            var items = await _serviceHelper.GetByIdAsync(user.RoleId);

            return _mapper.Map<RoleDTO>(items);
        }

        public async Task<RoleDTO> GetRoleByIdAsync(Guid id)
        {
            var item = await _serviceHelper.GetByIdAsync(id);

            return _mapper.Map<RoleDTO>(item);
        }

        public async Task<RoleDTO> GetSpecificRoleByUserAsync(Guid id, Guid userId)
        {
            var user = await _userRepository.GetQuery()
                    .FirstOrDefaultAsync(x => x.Id == userId)
                    ?? throw new InvalidOperationException($"User with Id = {userId} is not Exist.");

            var query = _repository.GetQuery()
                .Where(x => x.Id == user.RoleId);

            var item = await _serviceHelper.GetByIdAsync(id, query);

            return _mapper.Map<RoleDTO>(item);
        }

        public async Task UpsertRoleAsync(RoleUpsertDTO dto)
        {
            var item = await _repository.GetByIdAsync(dto.Id);
            bool isNew = item == null;

            item = _mapper.Map(dto, item)!;
            var aggregate = new RoleAggregate(item, _eventCollector);

            if (isNew)
            {
                aggregate.CreateRole();
                await _repository.InsertAsync(aggregate.Entity);
            }
            else
            {
                aggregate.UpdateRole();
            }
        }

        public async Task DeleteRoleAsync(Guid id)
        {
            var item = await _serviceHelper.GetByIdAsync(id);
            var aggregate = new RoleAggregate(item, _eventCollector);
            aggregate.DeleteRole();

            _repository.Delete(item);
        }
    }
}