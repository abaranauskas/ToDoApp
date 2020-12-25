using CSharpFunctionalExtensions;
using TasksManagementApp.Domain.Users;

namespace TasksManagementApp.Domain.TaskItems
{
    public class TaskItem : Entity
    {
        public TaskItem()
        {
        }

        private TaskItem(string name, User user, bool isCompleted)
        {
            Name = name;
            User = user;
            IsCompleted = isCompleted;
        }

        public string Name { get; private set; }
        public bool IsCompleted { get; private set; }
        public User User { get; }

        public static Result<TaskItem> Create(string name, User user)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<TaskItem>("Task name can not be empty.");

            return Result.Success(new TaskItem(name, user, false));
        }

        public Result UpdateNameAndStatus(string taskName, bool isCompleted)
        {
            if (string.IsNullOrWhiteSpace(taskName))
                return Result.Failure("Task name can not be empty.");

            Name = taskName;
            IsCompleted = isCompleted;

            return Result.Success();
        }
    }
}
