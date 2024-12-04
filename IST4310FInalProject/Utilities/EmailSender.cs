using Microsoft.AspNetCore.Identity.UI.Services;

namespace IST4310FInalProject.Utilities
{
    public class EmailSender:IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string body)
        {
            //here is the logic if we actully want to send an email
            return Task.CompletedTask;
        }
    }
}
