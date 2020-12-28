using FluentAssertions;
using TasksManagementApp.Domain.Users;
using Xunit;

namespace TasksManagementApp.Tests.Domain.Users
{
    public class EmailTest
    {
        [Fact]
        public void CreatesEmailIfEmailIsValid()
        {
            var email = Email.Create("user@gmail.com");

            email.IsSuccess.Should().BeTrue();
            email.Value.Should().BeOfType(typeof(Email));
            email.Value.Should().NotBeNull();
            email.Value.Value.Should().Be("user@gmail.com");
        }

        [Fact]
        public void ReturnsErrorIfEmailIsEmpty()
        {
            var email = Email.Create(null);

            email.IsSuccess.Should().BeFalse();           
            email.Error.Should().Be("Email should not be empty");
        }

        [Fact]
        public void ReturnsErrorIfEmailIsToLong()
        {
            var email = Email.Create("very.long.email@yahooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo.com");

            email.IsSuccess.Should().BeFalse();
            email.Error.Should().Be("Email is too long");
        }

        [Fact]
        public void ReturnsErrorIfEmailIsInInvalidFormat()
        {
            var email = Email.Create("just some random string");

            email.IsSuccess.Should().BeFalse();
            email.Error.Should().Be("Email is invalid");
        }
    }
}
