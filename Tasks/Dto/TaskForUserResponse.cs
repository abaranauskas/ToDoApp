namespace TasksManagementApp.Tasks.Dto
{
    public class TaskForUserResponse
    {
        public TaskForUserResponse(int id, string name, bool isCompleted)
        {
            Id = id;
            Name = name;
            IsCompleted = isCompleted;
        }

        public int Id { get; }
        public string Name { get; }
        public bool IsCompleted { get; }
    }
}