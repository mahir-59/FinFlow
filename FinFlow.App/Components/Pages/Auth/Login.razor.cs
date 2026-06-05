using FinFlow.Modules.Auth.Auth.Model.Classes.Requests;
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

        private async Task SubmitBtn()
        {
            _loaderService.Show();
            if(!string.IsNullOrEmpty(LoginRequest.Username) && !string.IsNullOrEmpty(LoginRequest.Password))
            {
                // bool isLoggedIn = await _loginViewModel.HandleLogin(LoginRequest);
                // if(!isLoggedIn)
                // {
                //     //_popupService.ShowError("Login failed. Please check your credentials and try again.");
                // }
            }
            else
            {
                // Show error message
            }
            _navigationManager.NavigateTo("/landing");
            _loaderService.Hide();
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
