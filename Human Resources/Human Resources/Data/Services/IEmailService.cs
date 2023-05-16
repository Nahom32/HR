using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface IEmailService
    {
        void SendEmail(EMessage message);
        Task SendEmailAsync(EMessage message);
    }
}
