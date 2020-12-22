using System.Collections.Generic;

namespace ToDoApp.Domain.Users
{
    public class Role : ValueObject
    {
        public static Role User = new Role("User");
        public static Role Admin = new Role("Admin");

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
    }
}
