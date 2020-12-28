using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TasksManagementApp.Common;
using TasksManagementApp.Domain.Users;
using TasksManagementApp.Infrastructure.EmailService;
using TasksManagementApp.Users.Dto;
using TasksManagementApp.Utils;

namespace TasksManagementApp.Users
{   
    public class UsersController : BaseController
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly TokenGenerator _tokenGenerator;
        private readonly EmailSender _emailSender;

        public UsersController(UnitOfWork unitOfWork, TokenGenerator tokenGenerator, EmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
            _emailSender = emailSender;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AuthenticateResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(model.Email);

            if (user is null ||
                !PasswordHash.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest(new ErrorResponse("Email or password is incorrect"));
            }

            var token = _tokenGenerator.GenerateJwtToken(user.Id, user.Email, user.Role);

            return Ok(new AuthenticateResponse(user.Id, user.Email, token));
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgotPasswordRequest model)
        {
            var emailOrError = Email.Create(model.Email);

            if (emailOrError.IsFailure)
                return BadRequest(new ErrorResponse(emailOrError.Error));

            var user = await _unitOfWork.UserRepository.GetByEmail(emailOrError.Value);

            if (user is null)
                return Ok("Please check your email for password reset instructions") ;

            user.SetPasswordResetToken();
            var passwordResetUrl = GeneratePasswordResetUrl(user);

            _emailSender.Send(user.Email,
                "Task management app password reset.",
                $"<a href=\"{passwordResetUrl}\">{passwordResetUrl}</a>");

            await _unitOfWork.Commit();
            return Ok(new { Message = passwordResetUrl.ToString() });
        }

        [AllowAnonymous]
        [HttpPost("{id}/[action]/{token}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ResetPassword(int id, Guid token, [FromBody] ResetPasswordRequest model)
        {
            var paswordOrError = PasswordHash.CreatePasswordHash(model.Password, model.ConfirmPassword);

            if (paswordOrError.IsFailure)
                return BadRequest(new ErrorResponse(paswordOrError.Error));

            var user = await _unitOfWork.UserRepository.GetById(id);

            if (user is null)
                return BadRequest(new ErrorResponse("User does not exist"));

            var validTokenResult = user.CanResetPassword(token);

            if (validTokenResult.IsFailure)
                return BadRequest(new ErrorResponse(validTokenResult.Error));

            user.SetNewPassword(paswordOrError.Value.Hash, paswordOrError.Value.Salt, token);

            await _unitOfWork.Commit();

            return Ok();
        }

        private string GeneratePasswordResetUrl(User user)
        {
            return new StringBuilder(Request.Scheme)
                .Append("://")
                .Append(Request.Host.Value)
                .Append("/")
                .Append(nameof(UsersController).Replace("Controller", string.Empty))
                .Append("/")
                .Append(user.Id)
                .Append("/")
                .Append(nameof(UsersController.ResetPassword))
                .Append("/")
                .Append(user.ResetPasswordToken)
                .ToString();
        }
    }
}
