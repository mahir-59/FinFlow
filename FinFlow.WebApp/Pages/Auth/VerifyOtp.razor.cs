using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinFlow.WebApp.Pages.Auth
{
    public partial class VerifyOtp : ComponentBase
    {
        private OtpModel otpModel = new();

        private async Task VerifyOTP()
        {

        }

        public class OtpModel
        {
            public string OTP { get; set; } = "";
        }
    }
}
