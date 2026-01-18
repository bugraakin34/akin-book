using AkinBook.Application.Auth.Dtos;
using AkinBook.Domain.Entities;
using AkinBook.Domain.Enums;
using AkinBook.Infrastructure.Auth;
using AkinBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AkinBook.Application.Auth
{
    public sealed class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly IPasswordHasher _hasher;
        private readonly ITokenService _tokenService;

        public AuthService(AppDbContext db, IPasswordHasher hasher, ITokenService tokenService)
        {
            _db = db;
            _hasher = hasher;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var email = request.Email.Trim().ToLowerInvariant();

            var exists = await _db.Users.AnyAsync(x => x.Email == email);
            if (exists)
                throw new InvalidOperationException("Bu email zaten kayıtlı");

            var user = new User
            {
                Email = email,
                PasswordHash = _hasher.Hash(request.Password),
                Role = Roles.User
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return new AuthResponse
            {
                AccessToken = _tokenService.CreateAccessToken(user),
                RefreshToken = _tokenService.CreateRefreshToken(),
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var email = request.Email.Trim().ToLowerInvariant();

            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user is null)
                throw new InvalidOperationException("Email veya parola hatalı");

            var ok = _hasher.Verify(request.Password, user.PasswordHash);
            if (!ok)
                throw new InvalidOperationException("Email veya parola hatalı");

            return new AuthResponse
            {
                AccessToken = _tokenService.CreateAccessToken(user),
                RefreshToken= _tokenService.CreateRefreshToken(),
            };

        }

    }
}
