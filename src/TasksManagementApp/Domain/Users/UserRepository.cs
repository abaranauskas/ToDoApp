using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
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

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Email == (Email)email);
        }

        public async Task<User> GetById(int userId)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
        }
    }
}
