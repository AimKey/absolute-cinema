using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordVerificationResult = Microsoft.AspNetCore.Identity.PasswordVerificationResult;

namespace Services.Helpers
{
    public class HashPasswordService : IHashPasswordService
    {
        private readonly PasswordHasher<string> _hasher = new();

        public bool VerifyPassword(string providePassword, string existingPassword)
        {
            var result = _hasher.VerifyHashedPassword(null, existingPassword, providePassword);
            if (result.Equals(PasswordVerificationResult.Success))
            {
                return true;
            }
            else if (result.Equals(PasswordVerificationResult.Failed))
            {
                return false;
            }
            else
            {
                throw new Exception("Unexpected password verification result: " + result.ToString());
            }
        }

        public string HashPassword(string password)
        {
            var result = _hasher.HashPassword(null, password);
            return result;
        }

    }
}
