﻿namespace TasksManagementApp.Infrastructure.EmailService
{
    public class SmtpConfiguration
    {
        public string EmailFrom { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpPassword { get; set; }
    }
}