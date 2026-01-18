using System;
using System.Collections.Generic;
using System.Text;

namespace AkinBook.Application.Auth.Dtos
{
    public sealed class AuthResponse
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
