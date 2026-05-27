using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinFlow.WebApp.Pages.Auth
{
    public partial class CreatePassword
    {
        private string PasswordStrength = "";
        private int StrengthLevel = 0;
        private CreatePasswordModel passwordModel = new();
        private PasswordStrengthModel passwordStrengthModel = new();

        private bool showPassword;
        private bool showConfirmPassword;
        private string password = "";

        private string Password
        {
            get => password;

            set
            {
                password = value;

                passwordModel.Password = value;

                CheckPasswordStrength(value);
            }
        }

        private string passwordClass => showPassword ? "fa-solid fa-check" : "fa-solid fa-xmark" ;
        private string passwordColor => showPassword ? "color : #0f7b3f" : "color : red" ;

        private string PasswordType =>
            showPassword ? "text" : "password";

        private string ConfirmPasswordType =>
            showConfirmPassword ? "text" : "password";

        private void TogglePassword()
        {
            showPassword = !showPassword;
        }

        private void ToggleConfirmPassword()
        {
            showConfirmPassword = !showConfirmPassword;
        }

        private async Task CreatePasswordBtn()
        {

        }
        private void CheckPasswordStrength(string e)
        {
            var password = e.ToString() ?? "";

            int score = 0;

            if (password.Length >= 8)
            {
                passwordStrengthModel.LengthIcon = "fa-solid fa-check";
                passwordStrengthModel.LengthColor = "color : #0f7b3f";
                score++;
            }
            else
            {
                passwordStrengthModel.LengthIcon = "fa-solid fa-xmark";
                passwordStrengthModel.LengthColor = "color : red";
            }

            if (password.Any(char.IsUpper))
            {
                passwordStrengthModel.UpperCaseIcon = "fa-solid fa-check";
                passwordStrengthModel.UpperCaseColor = "color : #0f7b3f";
                score++;
            }
            else
            {
                passwordStrengthModel.UpperCaseIcon = "fa-solid fa-xmark";
                passwordStrengthModel.UpperCaseColor = "color : red";
            }

            if (password.Any(char.IsDigit))
            {
                passwordStrengthModel.NumberIcon = "fa-solid fa-check";
                passwordStrengthModel.NumberColor = "color : #0f7b3f";
                score++;
            }
            else
            {
                passwordStrengthModel.NumberIcon = "fa-solid fa-xmark";
                passwordStrengthModel.NumberColor = "color : red";
            }

            if (password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                passwordStrengthModel.SpecialCharacterIcon = "fa-solid fa-check";
                passwordStrengthModel.SpecialCharacterColor = "color : #0f7b3f";
                score++;
            }
            else
            {
                passwordStrengthModel.SpecialCharacterIcon = "fa-solid fa-xmark";
                passwordStrengthModel.SpecialCharacterColor = "color : red";
            }
                
            StrengthLevel = score;

            PasswordStrength = score switch
            {
                1 => "Weak",
                2 or 3 => "Medium",
                4 => "Strong",
                _ => ""
            };

            StateHasChanged();
        }

        public class CreatePasswordModel
        {
            public string Password { get; set; } = "";

            public string ConfirmPassword { get; set; } = "";
        }

        public class PasswordStrengthModel
        {
            public string LengthIcon { get; set; } = "fa-solid fa-xmark";
            public string LengthColor { get; set; } = "color : red";
            public string UpperCaseIcon { get; set; } = "fa-solid fa-xmark";
            public string UpperCaseColor { get; set; } = "color : red";
            public string SpecialCharacterIcon { get; set; } = "fa-solid fa-xmark";
            public string SpecialCharacterColor { get; set; } = "color : red";
            public string NumberIcon { get; set; } = "fa-solid fa-xmark";
            public string NumberColor { get; set; } = "color : red";
        }
    }
}