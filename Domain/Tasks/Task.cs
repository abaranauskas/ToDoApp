namespace ToDoApp.Domain.Tasks
{
    public class Task : Entity
    {
        public Task()
        {
        }

        public string Name { get; }
        public bool IsCompleted { get; }
    }
}
