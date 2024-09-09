using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace APIConfig.Class
{
    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            // สร้าง salt
            var salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            
            // แฮชรหัสผ่าน
            var hashed = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);
            
            // รวม salt กับ hashed password
            var hashedPassword = Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hashed);
            return hashedPassword;
        }


        public static bool VerifyPassword(string hashedPassword, string password)
        {
            var parts = hashedPassword.Split(':');
            if (parts.Length != 2) return false;

            var salt = Convert.FromBase64String(parts[0]);
            var hash = Convert.FromBase64String(parts[1]);

            var newHash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            return newHash.SequenceEqual(hash);
        }
    }
}