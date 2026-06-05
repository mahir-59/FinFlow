using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinFlow.App.Components.Pages.Auth
{
    public partial class CreatePassword : ComponentBase
    {
        private string Password = "";
        private string ConfirmPassword = "";

        private bool ShowPassword;
        private bool ShowConfirmPassword;
        private bool HasMinLength =>
            !string.IsNullOrWhiteSpace(Password) &&
            Password.Length >= 8;

        private bool HasUpperCase =>
            !string.IsNullOrWhiteSpace(Password) &&
            Password.Any(char.IsUpper);

        private bool HasLowerCase =>
            !string.IsNullOrWhiteSpace(Password) &&
            Password.Any(char.IsLower);

        private bool HasNumber =>
            !string.IsNullOrWhiteSpace(Password) &&
            Password.Any(char.IsDigit);

        private bool HasSpecialCharacter =>
            !string.IsNullOrWhiteSpace(Password) &&
            Password.Any(ch => !char.IsLetterOrDigit(ch));

        private void TogglePassword()
        {
            ShowPassword = !ShowPassword;
        }

        private void ToggleConfirmPassword()
        {
            ShowConfirmPassword = !ShowConfirmPassword;
        }

        private int StrengthLevel
        {
            get
            {
                int score = 0;

                if (Password.Length >= 8)
                    score++;

                if (Password.Any(char.IsUpper))
                    score++;

                if (Password.Any(char.IsDigit))
                    score++;

                if (Password.Any(ch => !char.IsLetterOrDigit(ch)))
                    score++;

                return score;
            }
        }

        private string StrengthText =>
    StrengthLevel switch
    {
        1 => "Weak",
        2 => "Fair",
        3 => "Good",
        4 => "Strong",
        _ => ""
    };

        private string StrengthClass =>
            StrengthLevel switch
            {
                1 => "weak",
                2 => "fair",
                3 => "good",
                4 => "strong",
                _ => ""
            };
        
        private async Task ResetPassword()
        {
                // Call API to reset password here
                _navigationManager.NavigateTo("/password-changed");
        }

        public async Task GoBack()
        {
            await NavigationService.RaiseBackPressed();
        }
    }
}
