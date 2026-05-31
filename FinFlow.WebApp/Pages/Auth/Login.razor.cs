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
            bool isLoggedIn = await _loginViewModel.HandleLogin(loginModel);
            if(!isLoggedIn)
            {
                _popupService.ShowError("Login failed. Please check your credentials and try again.");
            }
            await Task.Delay(5000);
            _loaderService.Hide();
        }

        public class LoginModel
        {
            public string Email { get; set; } = "";
            public string Password { get; set; } = "";
        }
    }
}
