using FinFlow.Modules.Auth.Auth.Model.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinFlow.Modules.Auth.Auth.Model.Classes.Entities
{
    public class UserEntity : IUserEntity
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Role { get; set; }

        public string? Email { get; set; }

        public string Password { get; set; }
        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        // Optional Navigation-like Property
        // Useful even with Dapper
        public List<RefreshTokenEntity>? RefreshTokens { get; set; }
    }
}
