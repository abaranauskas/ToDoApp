using CSharpFunctionalExtensions;
using System;
using System.Text;

namespace TasksManagementApp.Utils
{
    public static partial class PasswordHash
    {
        public static Result<Password> CreatePasswordHash(string password)
        {
            if (password.Length < 12)
                return Result.Failure<Password>("Password must be at least 12 characters");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var passwordSalt = Convert.ToBase64String(hmac.Key);
                var passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));

                return Result.Success(new Password(passwordHash, passwordSalt));
            }

        }

        public static bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(Convert.FromBase64String(passwordSalt)))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hashInBytes = Convert.FromBase64String(passwordHash);
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != hashInBytes[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public class Password
        {
            public Password(string hash, string salt)
            {
                Hash = hash;
                Salt = salt;
            }

            public string Hash { get; }
            public string Salt { get; }
        }
    }
}
