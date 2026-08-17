using Medicare.Application.Models.CommonModels.Email;

namespace Medicare.Application.Interfaces.IEmail
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailModel email);
    }
}
