using System.Collections.Generic;

namespace TasksManagementApp.Domain.Users
{
    public class Role : ValueObject
    {
        public Role()
        {
        }

        public static Role User = new Role("user");
        public static Role Admin = new Role("admin");

        public string Value { get; }

        private Role(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(Role role)
        {
            return role.Value;
        }

        public static explicit operator Role(string role)
        {
            return new Role(role);
        }
    }
}
