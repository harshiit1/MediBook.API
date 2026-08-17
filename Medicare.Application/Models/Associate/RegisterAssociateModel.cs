namespace Medicare.Application.Models.Associate
{
    public class RegisterAssociateModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string IdentityDocument { get; set; }
        public string? IdentityFile { get; set; }   
        public byte[]? IdentityFileBytes { get; set; }
        public string EmployeeId { get; set; }
        public string PhoneCountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }
        public string ResidentialAddress { get; set; }
        public string PermanentAddress { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string ZipCode { get; set; }
        public string LanguagesSpoken { get; set; }
        public string EmergencyName { get; set; }
        public string EmergencyRelationship { get; set; }
        public string EmergencyPhone { get; set; }
        public string EmergencyCode { get; set; }
        public DateTime JoiningDate { get; set; }
        public string EmployeeType { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        public int SpecialityId { get; set; }
        public int DesignationId { get; set; }
        public string CreatedBy { get; set; }
        public AssociateQualificationModel AssociateQualification { get; set; }
        public AssociateExperienceModel AssociateExperience { get; set; }
    }

    public class AssociateQualificationModel
    {
        public string HighestDegree { get; set; }
        public string Specialization { get; set; }
        public string InstitutionName { get; set; }
        public int YearOfPassing { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime LicenseExpiry { get; set; }
        public string AdditionalCertifications { get; set; }
        public string? QualificationDocuments { get; set; }  
        public byte[]? QualificationDocumentBytes { get; set; }
    }

    public class AssociateExperienceModel
    {
        public int ExperienceYears { get; set; }
        public string OrganizationName { get; set; }
        public string DesignationRole { get; set; }
        public string DepartmentWorked { get; set; }
        public string KeySkills { get; set; }
    }
}
