using Microsoft.AspNetCore.Identity.UI.Services;

namespace haliSahaRandevu.Helpers
{
    public class EmailSender : IEmailSender
    {
        private readonly IWebHostEnvironment _env;

        public EmailSender(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // In a real app, you'd use SMTP or an API (SendGrid, Mailgun, etc.)
            // For this demo, we'll write the email content to a local file so the user can see the reset link.
            
            string logPath = Path.Combine(_env.ContentRootPath, "App_Data", "Logs");
            if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);

            string fileName = $"Email_{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid().ToString().Substring(0, 8)}.html";
            string filePath = Path.Combine(logPath, fileName);

            string content = $"To: {email}\nSubject: {subject}\n\n{htmlMessage}";
            await File.WriteAllTextAsync(filePath, content);

            // Also log to console
            Console.WriteLine($"\n--- EMAIL SENT TO {email} ---\nSubject: {subject}\nLink: {htmlMessage}\n----------------------------\n");
        }
    }
}
