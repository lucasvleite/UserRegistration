using System.Security.Cryptography;
using System.Text;

namespace UserRegistration.WebApi.Util
{
    public static class PasswordService
    {
        public static string Encrypt(this string password)
        {
            using MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }

        public static bool ComparePassword(string password, string? encryptedPassword) =>
            password.Encrypt() == encryptedPassword;
    }
}
