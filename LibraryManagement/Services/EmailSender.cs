using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // For development: log it, or just return completed task
        Console.WriteLine($"Sending email to {email}: Subject: {subject}, Message: {htmlMessage}");
        return Task.CompletedTask;
    }
}