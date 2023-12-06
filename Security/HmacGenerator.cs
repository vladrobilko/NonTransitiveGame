using System.Security.Cryptography;
using System.Text;
using System;

namespace NonTransitiveGame.Security
{
    public class HmacGenerator
    {
        public static string CalculateHmac(byte[] key, string data)
        {
            using (var hmac = new HMACSHA256(key))
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                byte[] hmacBytes = hmac.ComputeHash(dataBytes);
                return BitConverter.ToString(hmacBytes).Replace("-", "");
            }
        }
    }
}