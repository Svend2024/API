using System.Security.Cryptography;
using System.Text;

namespace KameGameAPI.Security
{
    public class SALT
    {
        public static string Hashing(string password)
        {
            using(SHA512 sha512 = SHA512.Create())
            {
                byte[] hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes("SALTString" + password));
                string hashString = BitConverter.ToString(hashBytes).Replace("-", "");
                return hashString;                
            }
        }
    }
}
