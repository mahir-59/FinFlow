using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinFlow.App.Components.Pages.Auth
{
    public partial class Login : ComponentBase
    {
        private string Email = string.Empty;

        private async Task SendOtp()
        {
            _navigationManager.NavigateTo("verify-otp");
        }
    }
}
