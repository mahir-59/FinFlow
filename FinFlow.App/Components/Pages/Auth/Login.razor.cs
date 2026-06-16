using FinFlow.App.Services;
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
                GenericResponse genericResponse = await _loginViewModel.HandleLogin(LoginRequest);
                if(!genericResponse.IsSuccess)
                {
                    _dialogService.ShowError(genericResponse.Message);
                }
                else
                {
                    _dialogService.ShowSuccess(genericResponse.Message);
                    _navigationManager.NavigateTo("/landing");
                }
            }
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
