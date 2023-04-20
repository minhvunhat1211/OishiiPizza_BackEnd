using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Domain.Common.HashPass
{
    public static class HashPass
    {
        public static string Hash(this string passWord)
        {
            var sha = SHA256.Create();
            var asByteArr = Encoding.Default.GetBytes(passWord);
            var hashedPass = sha.ComputeHash(asByteArr);
            return Convert.ToBase64String(hashedPass);
        }
    }
}
