namespace Medicare.Application.Models.Doctor
{
    public class DoctorCategoryModel
    {
        public int SpecialityId { get; set; }
        public string Category { get; set; }
        public List<DoctorProfileModel> Doctors { get; set; }
    }
    public class DoctorProfileModel
    {
        public int AssociateId { get; set; }
        public string Name { get; set; }
        public string Degree { get; set; }
        public string Image { get; set; }
        public string Department { get; set; }
        public string DesignationName { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<string> AvailableWeekDays { get; set; }
    }
    public class DoctorItemModel
    {
        public int SpecialityId { get; set; }
        public string Category { get; set; }
        public int AssociateId { get; set; }
        public string Name { get; set; }
        public string Degree { get; set; }
        public string Image { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string AvailableWeekDays { get; set; }
    }
}
