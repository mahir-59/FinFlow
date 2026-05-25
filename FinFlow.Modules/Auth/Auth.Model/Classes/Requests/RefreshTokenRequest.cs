using FinFlow.Modules.Auth.Auth.Model.Interfaces.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinFlow.Modules.Auth.Auth.Model.Classes.Requests
{
    public class RefreshTokenRequest : IRefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
