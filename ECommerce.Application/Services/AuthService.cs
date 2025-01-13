using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Shared.Repositories;

namespace ECommerce.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<UserAggregate, User> _repository;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMD5Service _md5Service;
        private readonly IMapper _mapper;

        public AuthService(IRepository<UserAggregate, User> repository, IUserService userService, ITokenService tokenService, IMD5Service md5Service, IMapper mapper)
        {
            _repository = repository;
            _userService = userService;
            _tokenService = tokenService;
            _md5Service = md5Service;
            _mapper = mapper;
        }

        public async Task<string> LoginAsync(UserLoginDTO dto)
        {
            if (!await _repository.IsUserExistByEmailAsync(dto.Email))
                throw new InvalidOperationException($"User with Email = {dto.Email} is not registered.");

            dto.Password = _md5Service.ComputeMD5Hash(dto.Password);

            return await GetTokenAsync(dto.Email, dto.Password);
        }

        public async Task RegisterAsync(UserCreateDTO dto)
        {
            if (await _repository.IsUserExistByEmailAsync(dto.Email))
                throw new InvalidOperationException($"User with Email = {dto.Email} already exists.");

            dto.Password = _md5Service.ComputeMD5Hash(dto.Password);

            await _userService.CreateUserAsync(dto);
        }

        private async Task<string> GetTokenAsync(string email, string password)
        {
            var user = await _userService.GetUserByEmailAndPasswordAsync(email, password)
                ?? throw new InvalidOperationException("Invalid Credentials.");

            return await _tokenService.GenerateTokenAsync(_mapper.Map<User>(user))
                ?? throw new InvalidOperationException("Token is not generated.");
        }
    }
}