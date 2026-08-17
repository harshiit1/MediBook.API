namespace Medicare.Application.Models.Master
{
    public class StatusKeyModel
    {
        public int StatusId { get; set; }
        public string StatusKey { get; set; }
        public string StatusValue { get; set; }
        public string Category { get; set; }
    }
    public class StatusCategoryModel
    {
        public string Category { get; set; }
        public List<StatusModel> Statuses { get; set; }
    }

    public class StatusModel
    {
        public int StatusId { get; set; }
        public string StatusKey { get; set; }
        public string StatusValue { get; set; }
    }
}
