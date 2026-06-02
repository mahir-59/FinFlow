using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinFlow.App.Components.Pages.Auth
{
    public partial class PasswordChanged : ComponentBase
    {
        public async Task GoBack()
        {
            await NavigationService.RaiseBackPressed();
        }
    }
}
