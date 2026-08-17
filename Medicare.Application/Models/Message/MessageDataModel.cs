using Medicare.Application.Interfaces.IErrorHandling;

namespace Medicare.Application.Models.Message
{
    public class MessageResponseModel : IErrorHandling
    {
        public int MessageId { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public string NotifType { get; set; }
        public string DoctorName { get; set; }
        public string DoctorRole { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string IsRead { get; set; }
        public string ResponseMessage { get; set; }
        public int IsSuccess { get; set; }
    }
    public class UpdateMessageRequestModel 
    {
        public int MessageId { get; set; }
        public bool IsRead { get; set; } = true;
    }
}
