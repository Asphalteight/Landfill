using System;
using System.Security.Cryptography;
using System.Text;

namespace Landfill.Common.Helpers
{
    public static class PasswordHelper
    {
        public static string GenerateSalt()
        {
            const int SaltLength = 32;
            byte[] salt = new byte[SaltLength];

            var generator = RandomNumberGenerator.Create();
            generator.GetBytes(salt);

            return Convert.ToBase64String(salt);
        }

        public static string GetHash(this string password, string salt)
        {
            byte[] unhashedBytes = Encoding.Unicode.GetBytes(string.Concat(salt, password));

            byte[] hashedBytes = SHA256.HashData(unhashedBytes);

            return Convert.ToBase64String(hashedBytes);
        }

        public static bool VerifyPassword(this string enteredPassword, string storedHash, string storedSalt)
        {
            string enteredHash = enteredPassword.GetHash(storedSalt);

            return storedHash == enteredHash;
        }
    }
}
