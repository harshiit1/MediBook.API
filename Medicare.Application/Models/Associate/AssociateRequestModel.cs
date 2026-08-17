namespace Medicare.Application.Models.Associate
{
    public class UpdateAssociateRequestModel
    {
        public int AssociateId { get; set; }
        public int RoleId { get; set; }
        public int DepartmentId { get; set; }
        public int SpecialityId { get; set; }
        public int DesignationId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? ToTime { get; set; }
        public TimeSpan? BreakTimeFrom { get; set; }
        public TimeSpan? BreakTimeTo { get; set; }
        public string WorkingDays { get; set; }
        public int? ConsultationTime { get; set; }
        public decimal? AverageCharge { get; set; }
        public string UpdatedBy { get; set; }
    }
    public class DeleteAssociateRequestModel
    {
        public int AssociateId { get; set; }
        public int IsActive { get; set; } = 0;
        public string UpdatedBy { get; set; }
    }
}
