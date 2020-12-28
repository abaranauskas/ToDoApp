using FluentAssertions;
using TasksManagementApp.Utils;
using Xunit;

namespace TasksManagementApp.Tests.Utils
{
    public class PasswordHashTest
    {
        [Fact]
        public void ReturnsPasswordIfPasswordMatchesAndLengthIsValid()
        {
            var password = PasswordHash.CreatePasswordHash("123456789012", "123456789012");

            password.IsSuccess.Should().BeTrue();
            password.Value.Should().BeOfType(typeof(PasswordHash.Password));
            password.Value.Hash.Should().NotBeEmpty();
            password.Value.Salt.Should().NotBeEmpty();
        }

        [Fact]
        public void ReturnsErrorIfPasswordIsLessThan12CharactersLength()
        {
            var password = PasswordHash.CreatePasswordHash("1234567890", "1234567890");

            password.IsSuccess.Should().BeFalse();
            password.Error.Should().Be("Password must be at least 12 characters");           
        }

        [Fact]
        public void ReturnsErrorIfPasswordsDoNotMatch()
        {
            var password = PasswordHash.CreatePasswordHash("123456789012", "210987654321");

            password.IsSuccess.Should().BeFalse();
            password.Error.Should().Be("Password and confirm password values do not match");
        }

        [Fact]
        public void VerifiesAndReturnsTrueIfPasswordMatchesHash()
        {
            //Arrange
            var password = PasswordHash.CreatePasswordHash("123456789012", "123456789012").Value;
            var hash = password.Hash;
            var salt = password.Salt;

            //Act
            var result = PasswordHash.VerifyPasswordHash("123456789012", hash, salt);

            result.Should().BeTrue();
        }

        [Fact]
        public void VerifiesAndReturnsFalseIfPasswordDoesNotMatchesHash()
        {
            //Arrange
            var password = PasswordHash.CreatePasswordHash("123456789012", "123456789012").Value;
            var hash = password.Hash;
            var salt = password.Salt;

            //Act
            var result = PasswordHash.VerifyPasswordHash("AnyPassword", hash, salt);

            result.Should().BeFalse();
        }
    }
}
