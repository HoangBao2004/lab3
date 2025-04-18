﻿namespace ASC.WEB.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            _logger.LogInformation($"Sending email to {email} with subject {subject}");
            return Task.CompletedTask;
        }
    }
}
