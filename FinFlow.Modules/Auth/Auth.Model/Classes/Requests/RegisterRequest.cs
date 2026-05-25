using FinFlow.Modules.Auth.Auth.Model.Interfaces.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinFlow.Modules.Auth.Auth.Model.Classes.Requests
{
    public class RegisterRequest : IRegisterRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(12)]
        public string Password { get; set; }
    }
}
