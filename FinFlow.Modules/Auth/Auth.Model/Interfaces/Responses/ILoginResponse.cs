using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinFlow.Modules.Auth.Auth.Model.Interfaces.Responses
{
    internal interface ILoginResponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
