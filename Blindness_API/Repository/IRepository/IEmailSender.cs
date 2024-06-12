namespace Blindness_API.Repository.IRepository
{
    
    
        public interface IEmailSender
        {
            Task SendEmailAsync(string email, string subject, string htmlMessage);
        }
    
}
