using FinFlow.Modules.Auth.Auth.Model.Classes.Entities;
using FinFlow.Modules.Auth.Auth.Model.Interfaces.Entities;
using FinFlow.Modules.Auth.Auth.Model.Interfaces.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinFlow.Modules.Auth.Auth.Model.Classes.Responses
{
    public class LoginResponse : ILoginResponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
        public IUserEntity UserData { get; set; }
    }
}
