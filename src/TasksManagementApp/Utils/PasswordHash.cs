using CSharpFunctionalExtensions;
using System.Text;

namespace TasksManagementApp.Utils
{
    public static class PasswordHash
    {
        public static Result<Password> CreatePasswordHash(string password, string confirmPassword)
        {
            if (password.Length < 12)
                return Result.Failure<Password>("Password must be at least 12 characters");

            if (password != confirmPassword)
                return Result.Failure<Password>("Password and confirm password values do not match");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var passwordSalt = hmac.Key;
                var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return Result.Success(new Password(passwordHash, passwordSalt));
            }

        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public class Password
        {
            public Password(byte[] hash, byte[] salt)
            {
                Hash = hash;
                Salt = salt;
            }

            public byte[] Hash { get; }
            public byte[] Salt { get; }
        }
    }
}
