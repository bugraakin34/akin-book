using AkinBook.Application.Auth.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AkinBook.Application.Auth
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}
