namespace Medicare.Application.Models.MasterModels
{
    public class RoleDepartmentSpecialityModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int? SpecialityId { get; set; }
        public string SpecialityName { get; set; }
        public int? DesignationId { get; set; }
        public string DesignationName { get; set; }
    }
    public class RoleHierarchyModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<DepartmentHierarchyModel> Departments { get; set; }
        public List<DesignationModel> Designations { get; set; }
    }

    public class DepartmentHierarchyModel
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public List<SpecialityHierarchyModel> Specialities { get; set; }
    }

    public class SpecialityHierarchyModel
    {
        public int SpecialityId { get; set; }
        public string SpecialityName { get; set; }
    }

    public class DesignationModel
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
    }
}
