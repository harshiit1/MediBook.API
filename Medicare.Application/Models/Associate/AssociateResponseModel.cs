using Medicare.Application.Interfaces.IErrorHandling;

namespace Medicare.Application.Models.Associate
{
    public class AssociateResponseModel : IErrorHandling
    {
        public Guid UserId { get; set; }
        public string EmployeeId { get; set; }
        public string ResponseMessage { get; set; }
        public int IsSuccess { get; set; }
        public int Status { get; set; }
        public int ResponseId { get; set; }
    }
}
