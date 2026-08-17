using MediatR;
using Medicare.Application.Features.Queries.Associate;
using Medicare.Application.Interfaces.IAssociate;
using Medicare.Application.Models.Associate;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetAssociateDetailByIdQueryHandler : IRequestHandler<GetAssociateDetailByIdQuery, AssociateDetailModel>
    {
        private readonly IAssociateRepository _associateRepository;
        public GetAssociateDetailByIdQueryHandler(IAssociateRepository associateRepository)
        {
            _associateRepository = associateRepository;
        }
        public async Task<AssociateDetailModel> Handle(GetAssociateDetailByIdQuery request, CancellationToken cancellationToken)
        {
            List<AssociateDetailDto> associateData = await _associateRepository.GetAssociateDetailByIdAsync(request.associateId);

            if (associateData.Count == 0)
                return new AssociateDetailModel
                {
                    IsSuccess = 0,
                    Status = 0,
                    ResponseMessage = "Account Does Not Exist"
                };

            var result = associateData.FirstOrDefault();

            var returnData = new AssociateDetailModel
            {
                AssociateId = result.AssociateId,
                UserId = result.UserId,
                TenantId = result.TenantId,
                UserType = "Associate",
                FirstName = result.FirstName,
                MiddleName = result.MiddleName,
                LastName = result.LastName,
                DateOfBirth = result.DateOfBirth,
                Gender = result.Gender,
                IdentityDocument = result.IdentityDocument,
                EmployeeId = result.EmployeeId,
                PhoneCountryCode = result.PhoneCountryCode,
                PhoneNumber = result.PhoneNumber,
                EmailId = result.EmailId,
                ResidentialAddress = result.ResidentialAddress,
                PermanentAddress = result.PermanentAddress,
                CityId = result.CityId,
                StateId = result.StateId,
                CountryId = result.CountryId,
                ZipCode = result.ZipCode,
                LanguagesSpoken = result.LanguagesSpoken,
                EmergencyName = result.EmergencyName,
                EmergencyRelationship = result.EmergencyRelationship,
                EmergencyPhone = result.EmergencyPhone,
                EmergencyCode = result.EmergencyCode,
                AssociateType = result.AssociateType,
                IsActive = result.IsActive,
                JoiningDate = result.JoiningDate,
                EmployeeType = result.EmployeeType,
                DepartmentId = result.DepartmentId,
                DepartmentName = result.DepartmentName,
                RoleId = result.RoleId,
                RoleName = result.RoleName,
                SpecialityId = result.SpecialityId,
                SpecialityName = result.SpecialityName,
                DesignationId = result.DesignationId,
                DesignationName = result.DesignationName,
                IsSuccess = result.IsSuccess,
                Status = result.Status,
                ResponseMessage = result.ResponseMessage,

                Schedule = new AssociateScheduleDetailModel
                {
                    ScheduleId = result.ScheduleId,
                    FromDate = result.FromDate,
                    ToDate = result.ToDate,
                    FromTime = result.FromTime,
                    ToTime = result.ToTime,
                    BreakTimeFrom = result.BreakTimeFrom,
                    BreakTimeTo = result.BreakTimeTo,
                    WorkingDays = result.WorkingDays,
                    ConsultationTime = result.ConsultationTime,
                    AverageCharge = result.AverageCharge,
                    OtpMethod = result.OtpMethod
                },

                Qualifications = associateData
                    .Where(r => r.QualificationId > 0)
                    .GroupBy(r => r.QualificationId)
                    .Select(g =>
                    {
                        var q = g.First();
                        return new AssociateQualificationDetailModel
                        {
                            QualificationId = q.QualificationId,
                            HighestDegree = q.HighestDegree,
                            Specialization = q.Specialization,
                            InstitutionName = q.InstitutionName,
                            YearOfPassing = q.YearOfPassing,
                            RegistrationNumber = q.RegistrationNumber,
                            LicenseExpiry = q.LicenseExpiry,
                            AdditionalCertifications = q.AdditionalCertifications
                        };
                    }),

                Experiences = associateData
                    .Where(r => r.ExperienceId > 0)
                    .GroupBy(r => r.ExperienceId)
                    .Select(g =>
                    {
                        var e = g.First();
                        return new AssociateExperienceDetailModel
                        {
                            ExperienceId = e.ExperienceId,
                            ExperienceYears = e.ExperienceYears,
                            OrganizationName = e.OrganizationName,
                            DesignationRole = e.DesignationRole,
                            DepartmentWorked = e.DepartmentWorked,
                            KeySkills = e.KeySkills
                        };
                    }),
            };
            return returnData;
        }
    }
}
