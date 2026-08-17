namespace Medicare.Application.Models.BackgroundJob.Appointment
{ 
    public class ScheduledReminderRequestModel
    {
        public Guid TenantId { get; set; }
        public string ReminderType { get; set; }  // "24Hr" or "1Week   "
    }
    public class AppointmentReminderLogRequestModel
    {
        public int AppointmentId { get; set; }
        public string ReminderType { get; set; }
        public string NotificationChannel { get; set; }
        public string SentTo { get; set; }
        public int Reminder24HrSent { get; set; }
        public int Reminder1WeekSent { get; set; }
    }
    public class ReleaseAppointmentRequestModel
    {
        public int AppointmentId { get; set; }
        public int SlotId { get; set; }
    }
}
