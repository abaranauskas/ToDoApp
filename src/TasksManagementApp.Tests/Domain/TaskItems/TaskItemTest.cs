using FluentAssertions;
using TasksManagementApp.Domain.TaskItems;
using TasksManagementApp.Domain.Users;
using Xunit;

namespace TasksManagementApp.Tests.Domain.TaskItems
{
    public class TaskItemTest
    {
        [Fact]
        public void CreatesTaskItemForUser()
        {
            //Arrange
            var user = User.Create(Email.Create("user@gmail.com").Value, Role.Admin, "Aidas", new byte[1], new byte[1]);

            //Act
            var result = TaskItem.Create("Task name", user.Value);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeOfType(typeof(TaskItem));
            result.Value.Should().NotBeNull();
            result.Value.Name.Should().Be("Task name");
            result.Value.User.Name.Should().Be("Aidas");
            result.Value.User.Role.Should().Be(Role.Admin);
        }

        [Fact]
        public void ReturnErrorWhenTaskNameIsInvalid()
        {
            //Arrange
            var user = User.Create(Email.Create("user@gmail.com").Value, Role.Admin, "Aidas", new byte[1], new byte[1]);

            //Act
            var result = TaskItem.Create("", user.Value);

            //Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("Task name can not be empty.");           
        }
    }
}
