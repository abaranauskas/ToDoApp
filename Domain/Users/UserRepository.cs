using System;
using TasksManagementApp.Infrastructure;

namespace TasksManagementApp.Domain.Users
{
    public class UserRepository
    {
        private readonly TasksManagementContext _context;

        public UserRepository(TasksManagementContext context)
        {
            _context = context;
        }
    }
}
