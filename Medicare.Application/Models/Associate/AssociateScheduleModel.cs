namespace Medicare.Application.Models.Associate
{
    public class AssociateScheduleModel
    {
        public int AssociateId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public TimeSpan? BreakTimeFrom { get; set; }
        public TimeSpan? BreakTimeTo { get; set; }
        public string WorkingDays { get; set; }
        public int? ConsultationTime { get; set; }
        public decimal? AverageCharge { get; set; }
        public string? OtpMethod { get; set; }
        public string CreatedBy { get; set; }
    }
}
