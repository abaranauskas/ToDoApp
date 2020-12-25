namespace TasksManagementApp.Tasks.Dto
{
    public class TaskForAdminResponse
    {
        public TaskForAdminResponse(int id, string name, bool isCompleted, int userId, string userName, string email)
        {
            Id = id;
            Name = name;
            IsCompleted = isCompleted;
            UserId = userId;
            UserName = userName;
            Email = email;
        }

        public int Id { get; }
        public string Name { get; }
        public bool IsCompleted { get; }
        public int UserId { get; }
        public string UserName { get; }
        public string Email { get; }
    }
}
