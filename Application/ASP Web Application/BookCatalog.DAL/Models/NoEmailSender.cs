using Microsoft.AspNetCore.Identity.UI.Services;

public class NoEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // Don't send anything, just return
        return Task.CompletedTask;
    }
}
