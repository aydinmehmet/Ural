using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Ural.Common.Helpers
{
    /// <summary>
    /// The CryptoHelper Class 
    /// </summary>
    public static class CryptoHelper
    {
        /// <summary>
        /// Creates the sh a256.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string CreateSHA256(string text)
        {
            StringBuilder stringBuilder = new StringBuilder();

            using (SHA256 sha = SHA256.Create())
            {
                byte[] hashValue = sha.ComputeHash(Encoding.UTF8.GetBytes(text));

                for (int i = 0; i < hashValue.Length; i++)
                {
                    stringBuilder.Append(hashValue[i].ToString("x2", CultureInfo.InvariantCulture));
                }
            }

            return stringBuilder.ToString();
        }
    }
}
