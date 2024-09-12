using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace APIConfig.Class
{
    public class PasswordHelper
    {
        public  string HashPassword(string password)
        {
            var hashed = new StringBuilder();
            using (var hashAlgoritm = SHA256.Create()){
                var bytes = hashAlgoritm.ComputeHash(Encoding.UTF8.GetBytes(password));
                foreach(var @byte in bytes){
                    hashed.Append(@byte.ToString("x2"));
                }
            }
            return hashed.ToString();
        }
    }
}