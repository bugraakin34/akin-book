using System;
using System.Collections.Generic;
using System.Text;

namespace AkinBook.Infrastructure.Auth
{
    public interface IPasswordHasher
    {
        string Hash(string password);

        bool Verify(string password, string passwordHash);
    }
}
