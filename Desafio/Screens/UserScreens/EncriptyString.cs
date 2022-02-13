using System;
using System.Security.Cryptography;
using System.Text;
using Blog.Models;
using Blog.Repositories;

namespace Blog.Screens.UserScreens
{
    public static class EncriptyString
    {
        public static String sha256_hash(string value)
        {
            StringBuilder stringBuilder = new StringBuilder();

            using (var hash = SHA256.Create())            
            {
                Encoding encoding = Encoding.UTF8;
                byte[] result = hash.ComputeHash(encoding.GetBytes(value));

                foreach (byte bt in result)
                    stringBuilder.Append(bt.ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}
