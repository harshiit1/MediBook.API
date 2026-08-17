namespace Medicare.Application.Models.Notification
{
    public class SaveNotificationModel
    {
       public int RefId { get; set; }
        public string UserType { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string NotifType { get; set; }
        public int? ReferenceId { get; set; }
    }
}
