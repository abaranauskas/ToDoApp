using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Domain.Users
{
    public class User : Entity
    {
        private readonly List<Task> _tasks = new List<Task>();

        protected User()
        {
        }

        private User(Email email, Role role, string name, string passwordHash)
        {
            Email = email;
            Role = role;
            Name = name;
            PasswordHash = passwordHash;
        }

        public Email Email { get; }
        public Role Role { get; }
        public string Name { get; }
        public string PasswordHash { get; }
        public IReadOnlyList<Task> Tasks => _tasks.ToList();

        public User Create(Email email, Role role, string name, string passwordHash)
        {
            //TODO: validation

            return new User(email, role, name, passwordHash);
        }
    }
}
