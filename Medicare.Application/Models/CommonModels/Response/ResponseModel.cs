using Medicare.Application.Interfaces.IErrorHandling;
using System.Net;

namespace Medicare.Application.Models.CommonModels.ResponseModel
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public string StatusMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public int Result { get; set; }
        public string TokenKey { get; set; }
        public string? RefreshToken { get; set; }
    }
    public class ResponseModel : IErrorHandling
    {
        public string ResponseMessage { get; set; }
        public int IsSuccess { get; set; }
        public int Status { get; set; }
        public int ResponseId { get; set; }
    }
}