namespace Medicare.Application.Models.Doctor
{
    public class DoctorTimeSlotRequestModel
    {
        public int AssociateId { get; set; }
        public DateTime? FromDate{ get; set; }
        public DateTime? ToDate { get; set; }
    }
}
