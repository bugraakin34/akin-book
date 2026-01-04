using System;
using System.Collections.Generic;
using System.Text;

namespace AkinBook.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Role { get; set; } = "User";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
