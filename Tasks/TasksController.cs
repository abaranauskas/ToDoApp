using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TasksManagementApp.Common;
using TasksManagementApp.Domain.TaskItems;
using TasksManagementApp.Domain.Users;
using TasksManagementApp.Tasks.Dto;
using TasksManagementApp.Utils;

namespace ToDoApp.Tasks
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public TasksController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> ListForUser()
        {
            var userId = int.Parse(User.Claims.Single(x => x.Type == "userId").Value);

            var tasks = await _unitOfWork.TaskItemRespository.GetByUserId(userId);

            return Ok(tasks.Select(x => new TaskForUserResponse(x.Id, x.Name, x.IsCompleted)));
        }

        [Authorize(Roles = "admin")]
        [HttpGet("All")]
        public async Task<IActionResult> ListAll()
        {
            var tasks = await _unitOfWork.TaskItemRespository.GetAll();

            return Ok(tasks.Select(x => new TaskForAdminResponse(
                x.Id, x.Name, x.IsCompleted, x.User.Id, x.User.Name, x.User.Email)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] NewTaskRequest model)
        {
            var userId = int.Parse(User.Claims.Single(x => x.Type == "userId").Value);
            var user = await _unitOfWork.UserRepository.GetById(userId);

            var newTaskOrError = TaskItem.Create(model.TaskName, user);

            if (newTaskOrError.IsFailure)
                return BadRequest(new ErrorResponse(newTaskOrError.Error));

            await _unitOfWork.AddAndSave(newTaskOrError.Value);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskRequest model)
        {
            var userId = int.Parse(User.Claims.Single(x => x.Type == "userId").Value);
            var task = await _unitOfWork.TaskItemRespository.GetByIdAndUserId(id, userId);

            if (task is null)
                return NotFound(new ErrorResponse("Task does not exist."));

            var result = task.UpdateNameAndStatus(model.TaskName, model.IsCompleted);

            if (result.IsFailure)
                return BadRequest(new ErrorResponse(result.Error));

            await _unitOfWork.Commit();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var userId = int.Parse(User.Claims.Single(x => x.Type == "userId").Value);
            var userRole = User.Claims.Single(x => x.Type == ClaimTypes.Role).Value;

            TaskItem task;

            if (userRole == Role.Admin)
                task = await _unitOfWork.TaskItemRespository.GetById(id);
            else
                task = await _unitOfWork.TaskItemRespository.GetByIdAndUserId(id, userId);

            if (task is null)
                return NotFound(new ErrorResponse("Task does not exist."));

            await _unitOfWork.DeleteAndSave(task);

            return Ok();
        }
    }
}
