using FinFlow.Modules.Auth.Auth.Model.Classes.Entities;
using FinFlow.Modules.Auth.Auth.Model.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinFlow.Modules.Auth.Auth.BL.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(IUserEntity user);

        string GenerateRefreshToken();
    }
}
