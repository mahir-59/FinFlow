using Microsoft.AspNetCore.Components;

namespace FinFlow.App.Components.Pages.Auth
{
    public partial class OnBoarding : ComponentBase
    {
        private void GoToLogin()
        {
            _navigationManager.NavigateTo("/login");
        }
    }
}