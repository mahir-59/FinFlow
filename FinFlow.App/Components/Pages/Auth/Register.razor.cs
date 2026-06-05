using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using FinFlow.Modules.Auth.Auth.Model.Classes.Requests;

namespace FinFlow.App.Components.Pages.Auth
{
    public partial class Register : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        public RegisterRequest _registerRequest { get; set; } = new();
        private bool showPassword = false;
        private string PasswordType =>
            showPassword ? "text" : "password";

        private void RegisterbtnClick()
        {
            
        }

        private void GoBack()
        {
            NavigationManager.NavigateTo("/login");
        }
    }
}