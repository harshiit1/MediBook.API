namespace Medicare.Application.Models.Doctor
{
    public class DoctorAvailabilityModel
    {
        public int AssociateId { get; set; }
        public int SlotId { get; set; }
        public string SlotDate { get; set; }
        public string WorkingDays { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool IsBooked { get; set; }
        public bool IsAvailable { get; set; }
    }
}
