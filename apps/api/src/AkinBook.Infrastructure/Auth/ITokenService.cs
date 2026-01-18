using AkinBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AkinBook.Infrastructure.Auth
{
    public interface ITokenService
    {
        string CreateAccessToken(User user);

        string CreateRefreshToken();
    }
}
