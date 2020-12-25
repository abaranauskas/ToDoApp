using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksManagementApp.Infrastructure;

namespace TasksManagementApp.Domain.TaskItems
{
    public class TaskItemRespository
    {
        private readonly TasksManagementContext _context;

        public TaskItemRespository(TasksManagementContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<TaskItem>> GetByUserId(int userId)
        {
            return await _context.Tasks
                .AsNoTracking()
                .Where(x => x.User.Id == userId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<TaskItem>> GetAll()
        {
            return await _context.Tasks
                .AsNoTracking()
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<TaskItem> GetByIdAndUserId(int id, int userId)
        {
            return await _context.Tasks.SingleOrDefaultAsync(x => x.Id == id && x.User.Id == userId);
        }

        public async Task<TaskItem> GetById(int id)
        {
            return await _context.Tasks.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
