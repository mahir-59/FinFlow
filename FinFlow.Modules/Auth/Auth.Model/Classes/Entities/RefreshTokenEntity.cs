using FinFlow.Modules.Auth.Auth.Model.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinFlow.Modules.Auth.Auth.Model.Classes.Entities
{
    public class RefreshTokenEntity : IRefreshTokenEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Token { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? RevokedAt { get; set; }

        public string? ReplacedByToken { get; set; }
        public string? OldRefreshToken { get; set; }

        public bool IsRevoked { get; set; }

        public string? CreatedByIp { get; set; }
    }
}
