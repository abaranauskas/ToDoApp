using TasksManagementApp.Domain.Users;

namespace TasksManagementApp.Domain.TaskItems
{
    public class TaskItem : Entity
    {
        public TaskItem()
        {
        }

        public string Name { get; }
        public bool IsCompleted { get; }
        public User User { get; set; }
    }
}
