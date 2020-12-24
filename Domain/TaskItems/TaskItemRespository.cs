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
    }
}
