namespace Medicare.Application.Models.Appointment
{
    public class UpdateAppointmentScheduleRequestModel
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int AssociateId { get; set; }
        public int SlotId { get; set; }
        public string VisitPurpose { get; set; }
        public string VisitType { get; set; }
        public string LastUpdatedBy { get; set; }
        public string AssociateRole { get; set; }
        public string RescheduleReason { get; set; }
    }
}
