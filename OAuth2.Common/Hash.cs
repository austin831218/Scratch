using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace OAuth2.Common
{
    public class Hash
    {
        public static string MD5(string secret)
        {
            var buffer = Encoding.Default.GetBytes(secret);
            var output = new MD5CryptoServiceProvider().ComputeHash(buffer);
            return BitConverter.ToString(output).Replace("-", string.Empty);
        }
    }
}
