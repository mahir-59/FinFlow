using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinFlow.WebApp.Pages.Auth
{
    public partial class ForgotPassword : ComponentBase
    {
        private ForgotPasswordModel forgotModel = new();

        private async Task HandleForgotPassword()
        {
            _navigationManager.NavigateTo("/verify-otp");
        }

        public class ForgotPasswordModel
        {
            public string Email { get; set; } = "";
        }
    }
}
