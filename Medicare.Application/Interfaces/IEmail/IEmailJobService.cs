using System;
using System.Collections.Generic;
using System.Text;

namespace Medicare.Application.Interfaces.IEmail
{
    public interface IEmailJobService
    {
        // Queues OTP email — returns Hangfire job ID
        string QueueOtpEmail(string toEmail, string toName, string otpCode);

        // Queues any generic email
        string QueueEmail(string toEmail, string toName, string subject, string body);

        // Schedule for later (e.g. reminder emails)
        string ScheduleEmail(string toEmail, string toName,
                             string subject, string body,
                             TimeSpan delay);
    }
}
