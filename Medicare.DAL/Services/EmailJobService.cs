using Hangfire;
using Medicare.Application.Interfaces.IAuthRepository;
using Medicare.Application.Interfaces.IEmail;

namespace Medicare.DAL.Services
{
    public class EmailJobService : IEmailJobService
    {
        private readonly IBackgroundJobClient _jobClient;
        public EmailJobService(IBackgroundJobClient jobClient)
        {
            _jobClient = jobClient;
        }   
        public string QueueEmail(string toEmail, string toName, string subject, string body)
        {
            var jobId = _jobClient.Enqueue<IEmailService>(
               queue: "emails",
               methodCall: x => x.SendEmailAsync(new Medicare.Application.Models.CommonModels.Email.EmailModel
               {
                   ToEmail = toEmail,
                   ToName = toName,
                   Subject = subject,
                   Body = body,
                   IsHtml = true
               })
           );

            return jobId;
        }

        public string QueueOtpEmail(string toEmail, string toName, string otpCode)
        {
            var jobId = _jobClient.Enqueue<IAuthRepository>(
                queue: "emails",
                methodCall: x => x.SendOtpEmailAsync(toEmail, toName, otpCode)
            );

            return jobId;
        }

        public string ScheduleEmail(string toEmail, string toName, string subject, string body, TimeSpan delay)
        {
            var jobId = _jobClient.Schedule<IEmailService>(
                methodCall: x => x.SendEmailAsync(new Medicare.Application.Models.CommonModels.Email.EmailModel
                {
                    ToEmail = toEmail,
                    ToName = toName,
                    Subject = subject,
                    Body = body,
                    IsHtml = true
                }),
                delay: delay
            );

            return jobId;
        }
    }
}
