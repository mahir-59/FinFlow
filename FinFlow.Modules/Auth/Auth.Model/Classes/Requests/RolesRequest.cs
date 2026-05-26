using FinFlow.Modules.Auth.Auth.Model.Interfaces.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinFlow.Modules.Auth.Auth.Model.Classes.Requests
{
    public class RolesRequest : IRolesRequest
    {
        public string? UserId { get; set; }
        public string? RoleName { get; set; }
    }
}
