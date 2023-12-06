using System.Security.Cryptography;

namespace NonTransitiveGame.Security
{
    public class KeyGenerator
    {
        public static byte[] GenerateRandomKey(int keySizeInBytes)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var key = new byte[keySizeInBytes];
                rng.GetBytes(key);
                return key;
            }
        }
    }
}