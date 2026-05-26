using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinFlow.WebApp.Pages.Auth
{
    public partial class Login : ComponentBase
    {
        private LoginModel loginModel = new();

        private bool showPassword = false;

        private string PasswordType =>
            showPassword ? "text" : "password";

        private void TogglePassword()
        {
            showPassword = !showPassword;
        }

        private async Task HandleLogin()
        {

        }

        public class LoginModel
        {
            public string Email { get; set; } = "";
            public string Password { get; set; } = "";
        }
    }
}
