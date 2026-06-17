using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinFlow.Modules.Auth.Auth.Model.Classes.Entities;
using FinFlow.Modules.Auth.Auth.Model.Interfaces.Entities;

namespace FinFlow.Modules.Base.Base.Model
{
    public class TokenStore
    {
        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public UserEntity UserDetails { get; set; }
    }
}
