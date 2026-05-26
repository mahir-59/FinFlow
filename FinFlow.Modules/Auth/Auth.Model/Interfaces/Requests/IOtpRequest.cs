using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinFlow.Modules.Auth.Auth.Model.Interfaces.Requests
{
    public interface IOtpRequest
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Otp { get; set; }

        // ForgotPassword
        // EmailVerification
        // LoginOtp
        // TwoFactor
        public string Purpose { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool IsUsed { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
