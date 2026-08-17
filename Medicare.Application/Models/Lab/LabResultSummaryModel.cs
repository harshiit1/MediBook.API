using Medicare.Application.Interfaces.IErrorHandling;

namespace Medicare.Application.Models.Lab
{
    public class LabResultSummaryModel : IErrorHandling
    {
        public int ResultId { get; set; }
        public int ProfileId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; } 
        public string TestName { get; set; } 
        public string TestCode { get; set; } 
        public string LabName { get; set; } 
        public DateTime ReportDate { get; set; }
        public string ResultValue { get; set; }
        public string ReferenceRange { get; set; } 
        public string ResultStatus { get; set; } 
        public string? Notes { get; set; }
        public int IsSuccess { get; set; }
        public int Status { get; set; }
        public string ResponseMessage { get; set; }
    }
}
