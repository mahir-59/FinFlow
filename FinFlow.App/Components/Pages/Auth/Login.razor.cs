using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinFlow.App.Components.Pages.Auth
{
    public partial class Login : ComponentBase
    {
        private bool showPassword = false;
        public LoginRequest LoginRequest { get; set; } = new LoginRequest();
        private string PasswordType =>
            showPassword ? "text" : "password";

        private async Task SendOtp()
        {
            if(!string.IsNullOrEmpty(LoginRequest.UserName) && !string.IsNullOrEmpty(LoginRequest.Password))
            {
                
            }
            else
            {
                // Show error message
            }
        }
        private void TogglePassword()
        {
            showPassword = !showPassword;
        }
        public async Task GoBack()
        {
            await NavigationService.RaiseBackPressed();
        }
    }
}
