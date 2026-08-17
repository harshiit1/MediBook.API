namespace Medicare.Application.Models.Appointment
{
    public class UpdateAppointmentRequestModel
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int AssociateId { get; set; }
        public int SlotId { get; set; }
        public string VisitPurpose { get; set; }
        public string VisitType { get; set; }
    }
}
