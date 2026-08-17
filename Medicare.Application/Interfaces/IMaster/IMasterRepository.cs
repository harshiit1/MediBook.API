using Medicare.Application.Models.Master;
using Medicare.Application.Models.MasterModels;
using Medicare.Application.Models.Speciality;

namespace Medicare.Application.Interfaces.Master
{
    public interface IMasterRepository
    {
        Task<List<StatusKeyModel>> GetStatusListAsync();
        Task<List<WeekDaysModel>> GetWeekDaysListAsync();
        Task<List<SpecialityTypeModel>> GetSpecialityByDepartmentIdAsync(int departmentId);
        Task<List<RoleDepartmentSpecialityModel>> GetRoleDepartmentSpecialityHierarchyAsync();
        Task<List<RoleDataModel>> GetRoleListAsync();
        Task<List<DepartmentDataModel>> GetDepartmentByRoleIdAsync(int roleId);
        Task<List<DesignationDataModel>> GetDesignationByRoleIdAsync(int roleId);
        Task<List<AgeTypeModel>> GetAgeTypeListAsync();
        Task<List<RelationTypeModel>> GetRelationTypeListAsync();

    }
}
