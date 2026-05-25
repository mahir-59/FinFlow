using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinFlow.Modules.Auth.Auth.Model.Interfaces.Requests
{
    public interface IRefreshTokenRequest
    {
        public string RefreshToken { get; set; }
    }
}
