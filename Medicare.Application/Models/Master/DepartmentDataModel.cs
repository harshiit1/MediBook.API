namespace Medicare.Application.Models.Master
{
    public class DepartmentDataModel
    {
        public int DepartmentId { get; set; }
        public int SpecialityId { get; set; }
        public string Department { get; set; } 
        public string? Description { get; set; }
    }
}
