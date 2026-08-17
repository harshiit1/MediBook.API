namespace Medicare.Application.Models.BackgroundJob.Appointment
{
    public class AppointmentBackgroundJobModel
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int ProfileId { get; set; }
        public int AssociateId { get; set; }
        public Guid TenantId { get; set; }
        public int OtpTypeId { get; set; }
        public string NotificationChannel { get; set; }  // 'SMS', 'EMAIL', 'WHATSAPP'
        public string PatientEmail { get; set; }
        public string PatientPhone { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string HospitalName { get; set; }
        public string AppointmentDate { get; set; }
        public string SlotStartTime { get; set; }
        public string VisitPurpose { get; set; }
        public string VisitType { get; set; }
        public int ReminderCount { get; set; }
        public string ConfirmationStatus { get; set; }
        public string OtpStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    
    public class ReleaseableAppointmentModel
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int SlotId { get; set; }
        public Guid TenantId { get; set; }
    }
}
