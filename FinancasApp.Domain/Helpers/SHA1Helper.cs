using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Helpers
{
    public class SHA1Helper
    {
        public static string Encrypt(string input)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = sha1.ComputeHash(inputBytes);

                var stringBuilder = new StringBuilder();
                foreach (var item in hashBytes)
                    stringBuilder.Append(item.ToString("x2"));

                return stringBuilder.ToString();
            }
        }
    }
}



