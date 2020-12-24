using Microsoft.AspNetCore.Mvc;
using TasksManagementApp.Utils;

namespace TasksManagementApp.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public UsersController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
