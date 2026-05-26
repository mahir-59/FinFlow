using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinFlow.Modules.Auth.Auth.BL.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendOtpEmail(string toEmail, string otp);
    }
}
