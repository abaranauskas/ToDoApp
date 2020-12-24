using Microsoft.AspNetCore.Mvc;
using TasksManagementApp.Utils;

namespace ToDoApp.Tasks
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController: ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public TasksController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
