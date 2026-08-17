namespace Medicare.Application.Models.Appointment
{
    public class PatientAppointmentModel
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int ProfileId { get; set; }
        public string PatientName { get; set; }
        public int AssociateId { get; set; }
        public string DoctorName { get; set; }
        public string Speciality { get; set; }
        public string AppointmentDate { get; set; }
        public string SlotStartTime { get; set; }
        public string SlotEndTime { get; set; }
        public string AppointmentStatus { get; set; }         // Scheduled / Cancelled / Completed
        public string VisitPurpose { get; set; }   
        public string VisitType { get; set; }
        public string RelationTypeName { get; set; }
        public string CreatedDate { get; set; }
    }

    public class AvailableAppointmentModel
    {
        public int SlotId { get; set; }
        public int AssociateId { get; set; }
        public string DoctorName { get; set; }
        public string Degree { get; set; }
        public string ImagePath { get; set; }
        public string SpcialityName { get; set; }
        public string DepartmentName { get; set; }
        public DateTime SlotDate { get; set; }
        public string SlotStartTime { get; set; }
        public string SlotEndTime { get; set; }
        public bool IsBooked { get; set; }
        public bool IsAvailable { get; set; }
    }
    public class AvailableAppointmentRequestModel
    {
        public int AssociateId { get; set; }
        public DateTime RequestedDate { get; set; }
    }
}
