using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Identity.Client;

namespace APIConfig.Class
{
    public class TokenSet
    {
        public string Issuer {get;set;}
        public string Audience {get;set;}
        public string secretKey { get; set; }

        public void Main(){
            Random random= new Random();
            int lengthS = 40 ,lengthP = 10 , UID = 10;
            const string AllowedChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!@#$%&*";

            StringBuilder randomString = new StringBuilder();
            StringBuilder randomPass = new StringBuilder();
            StringBuilder randomUID = new StringBuilder();   

            for (int i = 0; i < lengthS; i++)
            {
                int randomIndex = random.Next(AllowedChars.Length);
                char randomChar = AllowedChars[randomIndex];
                randomString.Append(randomChar);
                
            }
            secretKey = randomString.ToString();

            for(int i = 0; i < lengthP; i++)
            {
                int randomIndex = random.Next(AllowedChars.Length);
                char randomChar = AllowedChars[randomIndex];
                randomPass.Append(randomChar);
                
            }
            Audience = randomString.ToString();

            for (int i = 0; i < UID; i++)
            {
                int randomIndex = random.Next(AllowedChars.Length);
                char randomChar = AllowedChars[randomIndex];
                randomUID.Append(randomChar);
                
            }
            Issuer = randomUID.ToString();
        }

    }
}