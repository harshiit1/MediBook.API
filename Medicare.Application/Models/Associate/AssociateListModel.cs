namespace Medicare.Application.Models.Associate
{
    public class AssociateListModel
    {
        public int AssociateId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int? SpecialityId { get; set; }
        public string? SpecialityName { get; set; }
        public int? DesignationId { get; set; }
        public string? DesignationName { get; set; }
        public bool IsActive { get; set; }
    }
}
