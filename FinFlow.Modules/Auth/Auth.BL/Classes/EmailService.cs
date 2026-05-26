using FinFlow.Modules.Auth.Auth.BL.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FinFlow.Modules.Auth.Auth.BL.Classes
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration
        _configuration;

        public EmailService(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendOtpEmail(string toEmail, string otp)
        {
            try
            {
                var email =
                _configuration["EmailSettings:Email"];

                var password =
                    _configuration["EmailSettings:Password"];

                var host =
                    _configuration["EmailSettings:Host"];

                var port =
                    Convert.ToInt32(
                        _configuration["EmailSettings:Port"]);

                using var smtpClient =
                    new SmtpClient(host, port)
                    {
                        Credentials =
                            new NetworkCredential(
                                email,
                                password),

                        EnableSsl = true
                    };

                var message =
                    new MailMessage
                    {
                        From = new MailAddress(email),

                        Subject = "OTP Verification",

                        Body =
                            $"Your OTP is: {otp}",

                        IsBodyHtml = false
                    };

                message.To.Add(toEmail);

                await smtpClient.SendMailAsync(
                    message);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
