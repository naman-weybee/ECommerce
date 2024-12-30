using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserAggregate, User> _repository;
        private readonly IMapper _mapper;

        public UserService(IRepository<UserAggregate, User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync(RequestParams requestParams)
        {
            var items = await _repository.GetAllAsync(requestParams);

            return _mapper.Map<List<UserDTO>>(items);
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);

            return _mapper.Map<UserDTO>(item);
        }

        public async Task CreateUserAsync(UserCreateDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            var aggregate = new UserAggregate(user);
            aggregate.CreateUser(user);

            await _repository.InsertAsync(aggregate);
        }

        public async Task UpdateUserAsync(UserUpdateDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            var aggregate = _mapper.Map<UserAggregate>(user);
            aggregate.UpdateUser(user);

            await _repository.UpdateAsync(aggregate);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            var aggregate = new UserAggregate(item);
            aggregate.DeleteUser();

            await _repository.DeleteAsync(item);
        }
    }
}