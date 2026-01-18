using System;
using System.Collections.Generic;
using System.Text;

namespace AkinBook.Application.Auth.Dtos
{
    public sealed class RegisterRequest
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
