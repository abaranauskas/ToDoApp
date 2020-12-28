namespace TasksManagementApp.Users.Dto
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse(int userId, string email, string token)
        {
            UserId = userId;
            Email = email;
            Token = token;
        }
        public int UserId { get; }
        public string Email { get; }
        public string Token { get; }
    }
}
