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

        private User(Email email, Role role, string name, string passwordHash, string passwordSalt)
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
        public string PasswordHash { get; }
        public string PasswordSalt { get; }
        public IReadOnlyList<TaskItem> Tasks => _tasks.ToList();

        public static User Create(Email email, Role role, string name, string passwordHash, string passwordSalt)
        {
            //TODO: validation

            return new User(email, role, name, passwordHash, passwordSalt);
        }
    }
}
