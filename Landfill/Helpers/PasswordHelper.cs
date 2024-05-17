using System;
using System.Security.Cryptography;
using System.Text;

namespace Landfill.Helpers
{
    public static class PasswordHelper
    {
        public static string GenerateSalt()
        {
            const int SaltLength = 16;
            byte[] salt = new byte[SaltLength];

            var generator = RandomNumberGenerator.Create();
            generator.GetBytes(salt);
 
            return Convert.ToBase64String(salt);
        }

        public static string GenerateMD5Hash(this string password, string salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltedPassword = new byte[salt.Length + passwordBytes.Length];

            return Convert.ToBase64String(MD5.HashData(saltedPassword));
        }

        public static bool VerifyPassword(this string enteredPassword, string storedHash, string storedSalt)
        {
            return enteredPassword.GenerateMD5Hash(storedSalt) == storedHash;
        }
    }
}
