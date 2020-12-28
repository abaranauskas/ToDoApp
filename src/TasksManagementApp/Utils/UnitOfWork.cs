using System.Threading.Tasks;
using TasksManagementApp.Domain;
using TasksManagementApp.Domain.TaskItems;
using TasksManagementApp.Domain.Users;
using TasksManagementApp.Infrastructure;

namespace TasksManagementApp.Utils
{
    public class UnitOfWork
    {
        private readonly TasksManagementContext _context;
        private UserRepository _userRepository;
        private TaskItemRepository _taskItemRepository;

        public UnitOfWork(TasksManagementContext context)
        {
            _context = context;
        }

        public UserRepository UserRepository => 
            _userRepository = _userRepository ?? new UserRepository(_context);

        public TaskItemRepository TaskItemRespository => 
            _taskItemRepository = _taskItemRepository ?? new TaskItemRepository(_context);

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAndSave<T>(T item) where T: Entity
        {
            _context.Set<T>().Remove(item);
            await Commit();
        }

        public async Task AddAndSave<T>(T item) where T : Entity
        {
            _context.Set<T>().Add(item);
            await Commit();
        }
    }
}
