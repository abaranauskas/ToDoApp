using System.Threading.Tasks;
using TasksManagementApp.Domain.TaskItems;
using TasksManagementApp.Domain.Users;
using TasksManagementApp.Infrastructure;

namespace TasksManagementApp.Utils
{
    public class UnitOfWork
    {
        private readonly TasksManagementContext _context;
        private UserRepository _userRepository;
        private TaskItemRespository _taskItemRepository;

        public UnitOfWork(TasksManagementContext context)
        {
            _context = context;
        }

        public UserRepository UserRepository => 
            _userRepository = _userRepository ?? new UserRepository(_context);

        public TaskItemRespository TaskItemRespository => 
            _taskItemRepository = _taskItemRepository ?? new TaskItemRespository(_context);

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
