using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinFlow.Modules.Auth.Auth.BL.Classes
{
    using FinFlow.Modules.Auth.Auth.BL.Interfaces;
    using Microsoft.Extensions.Configuration;
    using System.Security.Cryptography;

    public class PasswordService : IPasswordService
    {
        private readonly IConfiguration _configuration;

        public PasswordService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public (string hash, string salt)
            HashPassword(string password)
        {
            // Generate Salt
            byte[] saltBytes =
                RandomNumberGenerator.GetBytes(16);

            string salt =
                Convert.ToBase64String(saltBytes);

            // Pepper
            string pepper =
                _configuration["Security:Pepper"];

            // Password + Pepper
            string passwordWithPepper =
                password + pepper;

            // PBKDF2 Hash
            using var pbkdf2 =
                new Rfc2898DeriveBytes(
                    passwordWithPepper,
                    saltBytes,
                    100000,
                    HashAlgorithmName.SHA256);

            byte[] hashBytes =
                pbkdf2.GetBytes(32);

            string hash =
                Convert.ToBase64String(hashBytes);

            return (hash, salt);
        }

        public bool VerifyPassword(
            string password,
            string storedHash,
            string storedSalt)
        {
            string pepper =
                _configuration["Security:Pepper"];

            string passwordWithPepper =
                password + pepper;

            byte[] saltBytes =
                Convert.FromBase64String(storedSalt);

            using var pbkdf2 =
                new Rfc2898DeriveBytes(
                    passwordWithPepper,
                    saltBytes,
                    100000,
                    HashAlgorithmName.SHA256);

            byte[] hashBytes =
                pbkdf2.GetBytes(32);

            string computedHash =
                Convert.ToBase64String(hashBytes);

            return computedHash == storedHash;
        }
    }
}
