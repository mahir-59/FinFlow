using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinFlow.App.Components.Pages.Auth
{
    public partial class Login : ComponentBase
    {
        private string Email = string.Empty;
        public LoginRequest LoginRequest { get; set; } = new LoginRequest();

        private async Task SendOtp()
        {
            if(!string.IsNullOrEmpty(LoginRequest.Email) && LoginRequest.Email.Contains("@"))
            {
                _navigationManager.NavigateTo("/verify-otp/true");
            }
            else
            {
                // Show error message
            }
        }

        public async Task GoBack()
        {
            await NavigationService.RaiseBackPressed();
        }
    }
}
