﻿using AutoMapper;
using ECommerce.Application.DTOs.Auth;
using ECommerce.Application.DTOs.RefreshToken;
using ECommerce.Application.DTOs.User;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        private readonly IUserService _userService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IAccessTokenService _accessTokenService;
        private readonly IMD5Service _md5Service;
        private readonly ITransactionManagerService _transactionManagerService;
        private readonly IMapper _mapper;

        public AuthService(IRepository<User> userRepository, IRepository<RefreshToken> refreshTokenRepository, IUserService userService, IRefreshTokenService refreshTokenService, IAccessTokenService accessTokenService, IMD5Service md5Service, ITransactionManagerService transactionManagerService, IMapper mapper)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _userService = userService;
            _refreshTokenService = refreshTokenService;
            _accessTokenService = accessTokenService;
            _md5Service = md5Service;
            _transactionManagerService = transactionManagerService;
            _mapper = mapper;
        }

        public async Task RegisterAsync(UserUpsertDTO dto)
        {
            var query = _userRepository.GetQuery();

            var isEmailExist = await query.AnyAsync(x => x.Email == dto.Email);

            if (isEmailExist)
                throw new InvalidOperationException($"User with Email = {dto.Email} is already exist.");

            dto.Password = _md5Service.ComputeMD5Hash(dto.Password);

            await _userService.UpsertUserAsync(dto);
        }

        public async Task<UserTokenDTO> LoginAsync(UserLoginDTO dto)
        {
            var query = _userRepository.GetQuery();

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
            // Begin Transaction
            await _transactionManagerService.BeginTransactionAsync();

            try
            {
                var refreshToken = await _refreshTokenRepository.GetQuery()
                        .Where(x => x.Token == dto.RefreshToken).FirstOrDefaultAsync();

                if (refreshToken?.IsRevoked != false || refreshToken.ExpiredDate <= DateTime.UtcNow)
                    throw new InvalidOperationException("Token Expired.");

                var user = await _userService.GetUserByIdAsync(refreshToken.UserId, true);

                await _refreshTokenService.RevokeRefreshTokenAsync(new RevokeRefreshTokenDTO { RefreshToken = refreshToken.Token });

                var token = await GenerateUserTokenAsync(user);

                // Save
                await _userRepository.SaveChangesAsync();

                // Commit transaction
                await _transactionManagerService.CommitTransactionAsync();

                return token;
            }
            catch (Exception)
            {
                // Rollback transaction on error
                await _transactionManagerService.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<UserTokenDTO> GenerateUserTokenAsync(UserDTO dto)
        {
            var accessToken = await _accessTokenService.CreateAccessTokenAsync(_mapper.Map<User>(dto))
                ?? throw new InvalidOperationException("Access Token is not generated.");

            var refreshToken = await _refreshTokenService.UpsertRefreshTokenAsync(new RefreshTokenUpsertDTO { UserId = dto.Id })
                ?? throw new InvalidOperationException("Refresh Token is not generated.");

            return new UserTokenDTO()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task ReSendEmailVerificationAsync(ResendEmailVerificationDTO dto)
        {
            var item = await _userRepository.GetQuery()
                .FirstOrDefaultAsync(x => x.Email == dto.Email)
                ?? throw new InvalidOperationException($"No User found with Email {dto.Email}");

            if (string.IsNullOrEmpty(item.EmailVerificationToken) || item.IsEmailVerified)
                throw new InvalidOperationException("User Email is already Verified");

            await _userService.SendVerificationEmailAsync(item);
        }

        public async Task VerifyEmailAsync(string token)
        {
            await _userService.VerifyEmailAsync(token);
        }
    }
}