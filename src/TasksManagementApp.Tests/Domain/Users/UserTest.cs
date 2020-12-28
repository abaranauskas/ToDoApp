using FluentAssertions;
using System;
using TasksManagementApp.Domain.Users;
using Xunit;

namespace TasksManagementApp.Tests.Domain.Users
{
    public class UserTest
    {
        [Fact]
        public void CreatesUser()
        {
            //Arrange
            var email = Email.Create("user@gmail.com").Value;
            var role = Role.User;

            //Act
            var user = User.Create(email, role, "Aidas", new byte[1], new byte[1]);

            //Assert
            user.IsSuccess.Should().BeTrue();
            user.Value.Should().BeOfType(typeof(User));
            user.Value.Should().NotBeNull();
            user.Value.Name.Should().Be("Aidas");
        }

        [Fact]
        public void ReturnsErrorWhenNameProvidedIsInvalid()
        {
            //Arrange
            var email = Email.Create("user@gmail.com").Value;
            var role = Role.User;

            //Act
            var user = User.Create(email, role, string.Empty, new byte[1], new byte[1]);

            //Assert
            user.IsSuccess.Should().BeFalse();
            user.Error.Should().Be("Name should not be empty");
        }

        [Fact]
        public void SetsUserPasswordResetPassword()
        {
            //Arrange
            var email = Email.Create("user@gmail.com").Value;
            var role = Role.User;
            var user = User.Create(email, role, "Aidas", new byte[1], new byte[1]).Value;

            user.ResetPasswordToken.Should().Be(Guid.Empty);
            user.ResetPasswordTokenExpires.Should().Be(DateTimeOffset.MinValue);

            //Act
            user.SetPasswordResetToken();

            //Assert
            user.ResetPasswordToken.Should().NotBe(Guid.Empty);
            user.ResetPasswordTokenExpires.Should().NotBe(DateTimeOffset.MinValue);
        }

        [Fact]
        public void ReturnsSuccessIfTokenIsValid()
        {
            //Arrange           
            var user = User.Create(Email.Create("user@gmail.com").Value, Role.User, "Aidas", new byte[1], new byte[1]).Value;
            user.SetPasswordResetToken();
            var token = user.ResetPasswordToken;

            //Act
            var result = user.CanResetPassword(token);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void ReturnsErrorWithMessageIfTokenIsInvalid()
        {
            //Arrange           
            var user = User.Create(Email.Create("user@gmail.com").Value, Role.User, "Aidas", new byte[1], new byte[1]).Value;
            user.SetPasswordResetToken();
            var token = Guid.NewGuid();

            //Act
            var result = user.CanResetPassword(token);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Token is invalid.");
        }

        [Fact]
        public void SetsNewPasswordHashAndSaltForUserIfTokenIsValid()
        {
            //Arrange           
            var user = User.Create(Email.Create("user@gmail.com").Value, Role.User, "Aidas", new byte[1], new byte[1]).Value;
            user.SetPasswordResetToken();
            var token = user.ResetPasswordToken;
            Random rnd = new Random();
            byte[] passwordHash = new byte[64];
            byte[] passwordSalt = new byte[128];
            rnd.NextBytes(passwordHash);

            //Act
            user.SetNewPassword(passwordHash, passwordSalt, token);

            //Assert
            Convert.ToBase64String(user.PasswordHash).Should().Be(Convert.ToBase64String(passwordHash));
            Convert.ToBase64String(user.PasswordSalt).Should().Be(Convert.ToBase64String(passwordSalt));
        }

        [Fact]
        public void ThrowsInvalidOperationExceptionIfTokenIsInvalid()
        {
            //Arrange           
            var user = User.Create(Email.Create("user@gmail.com").Value, Role.User, "Aidas", new byte[1], new byte[1]).Value;
            user.SetPasswordResetToken();
            var token = Guid.NewGuid();
            Random rnd = new Random();
            byte[] passwordHash = new byte[64];
            byte[] passwordSalt = new byte[128];
            rnd.NextBytes(passwordHash);

            //Act
            Action action = () => user.SetNewPassword(passwordHash, passwordSalt, token);

            //Assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}
