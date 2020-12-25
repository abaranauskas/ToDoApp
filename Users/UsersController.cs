using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TasksManagementApp.Domain.Users;
using TasksManagementApp.Users.Dto;
using TasksManagementApp.Utils;

namespace TasksManagementApp.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly TokenGenerator _tokenGenerator;

        public UsersController(UnitOfWork unitOfWork, TokenGenerator tokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(model.Email);

            if (user is null ||
                !PasswordHash.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest(new { message = "Email or password is incorrect" });
            }

            var token = _tokenGenerator.GenerateJwtToken(user.Id, user.Email, user.Role);

            return Ok(new AuthenticateResponse(user.Id, user.Email, token));
        }

        [Authorize(Roles = "user")]
        [HttpGet("action")]
        public IActionResult Action()
        {
            return Ok(new { Message = "Works well" });
        }
    }
}
