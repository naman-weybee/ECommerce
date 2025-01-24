using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<UserAggregate, User> _userRepository;
        private readonly IRepository<RefreshTokenAggregate, RefreshToken> _refreshTokenRepository;
        private readonly IUserService _userService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IAccessTokenService _accessTokenService;
        private readonly IMD5Service _md5Service;
        private readonly IMapper _mapper;

        public AuthService(IRepository<UserAggregate, User> userRepository, IRepository<RefreshTokenAggregate, RefreshToken> refreshTokenRepository, IUserService userService, IRefreshTokenService refreshTokenService, IAccessTokenService accessTokenService, IMD5Service md5Service, IMapper mapper)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _userService = userService;
            _refreshTokenService = refreshTokenService;
            _accessTokenService = accessTokenService;
            _md5Service = md5Service;
            _mapper = mapper;
        }

        public async Task RegisterAsync(UserCreateDTO dto)
        {
            var query = _userRepository.GetDbSet();

            var isEmailExist = await query.AnyAsync(x => x.Email == dto.Email);

            if (isEmailExist)
                throw new InvalidOperationException($"User with Email = {dto.Email} is already exist.");

            dto.Password = _md5Service.ComputeMD5Hash(dto.Password);

            await _userService.CreateUserAsync(dto);
        }

        public async Task<UserTokenDTO> LoginAsync(UserLoginDTO dto)
        {
            var query = _userRepository.GetDbSet();

            var isEmailExist = await query.AnyAsync(x => x.Email == dto.Email && x.IsEmailVerified);

            if (!isEmailExist)
                throw new InvalidOperationException($"User with Email = {dto.Email} is not registered or verified.");

            dto.Password = _md5Service.ComputeMD5Hash(dto.Password);

            var item = await query.Where(x => x.Email == dto.Email && x.Password == dto.Password).Include(u => u.Role).FirstOrDefaultAsync()
                ?? throw new InvalidOperationException("Invalid Credentials.");

            var user = _mapper.Map<UserDTO>(item);

            return await GenerateUserTokenAsync(user);
        }

        public async Task RevokeRefreshTokenAsync(RevokeRefreshTokenDTO dto)
        {
            await _refreshTokenService.RevokeRefreshTokenAsync(dto);
        }

        public async Task<UserTokenDTO> ReCreateAccessTokenAsync(AccessTokenCreateDTO dto)
        {
            var refreshToken = await _refreshTokenRepository.GetDbSet()
                .Where(x => x.Token == dto.RefreshToken).FirstOrDefaultAsync();

            if (refreshToken?.IsRevoked != false || refreshToken.ExpiredDate <= DateTime.UtcNow)
                throw new InvalidOperationException("Token Expired.");

            var user = await _userService.GetUserByIdAsync(refreshToken.UserId);

            await _refreshTokenService.RevokeRefreshTokenAsync(new RevokeRefreshTokenDTO { RefreshToken = refreshToken.Token });

            return await GenerateUserTokenAsync(user);
        }

        public async Task<UserTokenDTO> GenerateUserTokenAsync(UserDTO dto)
        {
            var accessToken = await _accessTokenService.CreateAccessTokenAsync(_mapper.Map<User>(dto))
                ?? throw new InvalidOperationException("Access Token is not generated.");

            var refreshToken = await _refreshTokenService.CreateRefreshTokenAsync(new RefreshTokenCreateDTO { UserId = dto.Id })
                ?? throw new InvalidOperationException("Refresh Token is not generated.");

            return new UserTokenDTO()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task VerifyEmailAsync(string token)
        {
            await _userService.VerifyEmailAsync(token);
        }
    }
}