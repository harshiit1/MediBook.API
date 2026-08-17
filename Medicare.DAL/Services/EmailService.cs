using Medicare.Application.Interfaces.IEmail;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Models.CommonModels.Email;
using Medicare.Application.Models.CommonModels.ErrorLog;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Medicare.DAL.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettingsModel _settings;
        private readonly IErrorLogRepository _errorLog;
        public EmailService(IOptions<EmailSettingsModel> settings, IErrorLogRepository errorLog)
        {
            _settings = settings.Value;
            _errorLog = errorLog;
        }   
        public async Task<bool> SendEmailAsync(EmailModel email)
        {
            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
                    Subject = email.Subject,
                    Body = email.Body,
                    IsBodyHtml = email.IsHtml
                };

                message.To.Add(new MailAddress(email.ToEmail, email.ToName));

                using var client = new SmtpClient(_settings.Host, _settings.Port)
                {
                    Credentials = new NetworkCredential(_settings.SenderEmail, _settings.Password),
                    EnableSsl = _settings.EnableSsl
                };

                await client.SendMailAsync(message);

                return true;
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = "",
                    Error_Trace = ex.StackTrace
                });
                return false;
            }
        }
    }
}
