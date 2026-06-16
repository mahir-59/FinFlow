using FinFlow.Modules.Auth.Auth.Model.Classes.Requests;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinFlow.WebApp.Pages.Auth
{
    public partial class Login : ComponentBase
    {
        private LoginRequest loginModel = new();

        private bool showPassword = false;

        private string PasswordType =>
            showPassword ? "text" : "password";

        private void TogglePassword()
        {
            showPassword = !showPassword;
        }

        private async Task OnLoginClick()
        {
            _loaderService.Show();
            // var isLoggedIn = await _loginViewModel.HandleLogin(loginModel);
            // _loaderService.Hide();
            // if(!isLoggedIn)
            // {
            //     _popupService.ShowError("Login failed. Please check your credentials and try again.");
            // }
        }

        public class LoginModel
        {
            public string Email { get; set; } = "";
            public string Password { get; set; } = "";
        }
    }
}
