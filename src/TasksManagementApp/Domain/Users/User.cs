using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TasksManagementApp.Domain.TaskItems;

namespace TasksManagementApp.Domain.Users
{
    public class User : Entity
    {
        private readonly List<TaskItem> _tasks = new List<TaskItem>();

        protected User()
        {
        }

        private User(Email email, Role role, string name, byte[] passwordHash, byte[] passwordSalt)
        {
            Email = email;
            Role = role;
            Name = name;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public Email Email { get; }
        public Role Role { get; }
        public string Name { get; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public Guid ResetPasswordToken { get; private set; }
        public DateTimeOffset ResetPasswordTokenExpires { get; private set; }
        public IReadOnlyList<TaskItem> Tasks => _tasks.ToList();

        public static Result<User> Create(Email email, Role role, string name, byte[] passwordHash, byte[] passwordSalt)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<User>("Name should not be empty");

            return new User(email, role, name, passwordHash, passwordSalt);
        }

        public void SetPasswordResetToken()
        {
            ResetPasswordToken = Guid.NewGuid();
            ResetPasswordTokenExpires = DateTimeOffset.UtcNow.AddDays(1);
        }

        public Result CanResetPassword(Guid token)
        {
            if (ResetPasswordToken == token && ResetPasswordTokenExpires >= DateTimeOffset.UtcNow)
                return Result.Success();

            return Result.Failure("Token is invalid.");
        }

        public void SetNewPassword(byte[] hash, byte[] salt, Guid token)
        {
            if (CanResetPassword(token).IsFailure)
                throw new InvalidOperationException();

            PasswordHash = hash;
            PasswordSalt = salt;
            ResetPasswordTokenExpires = DateTimeOffset.MinValue;
        }
    }
}
