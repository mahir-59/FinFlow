using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinFlow.App.Components.Pages.Auth
{
    public partial class ForgotPassword : ComponentBase
    {
        private async Task SendOtp()
        {
            _navigationManager.NavigateTo("/verify-otp");
        }

        public async Task GoBack()
        {
            await NavigationService.RaiseBackPressed();
        }
    }
}
