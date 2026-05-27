using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinFlow.WebApp.Pages.Auth
{
    public partial class ResetSuccess : ComponentBase
    {
        private void GoToLogin()
        {
            _navigationManager.NavigateTo("/");
        }
    }
}