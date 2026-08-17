

namespace Medicare.Application.Models.Appointment
{
    public class CancelAppointmentScheduleRequestModel
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string CancelReason { get; set; }
        public string LastUpdatedBy { get; set; }
        public string AssociateRole { get; set; }
    }
}
