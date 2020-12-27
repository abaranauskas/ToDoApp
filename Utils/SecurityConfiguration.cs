namespace TasksManagementApp.Utils
{
    public class SecurityConfiguration
    {
        public string Secret { get; set; }
        public int TokenExpiryInMinutes { get; set; }
    }
}