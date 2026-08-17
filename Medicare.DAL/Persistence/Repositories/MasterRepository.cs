using Dapper;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Interfaces.Master;
using Medicare.Application.Models.CommonModels.ErrorLog;
using Medicare.Application.Models.Master;
using Medicare.Application.Models.MasterModels;
using Medicare.Application.Models.Speciality;
using Medicare.DAL.Persistence.Dapper;

namespace Medicare.DAL.Persistence.Repositories
{
    public class MasterRepository : IMasterRepository
    {
        private readonly IDapperContext _context;
        private readonly IErrorLogRepository _errorLog;
        public MasterRepository(IDapperContext context, IErrorLogRepository errorLogRepository)
        {
            _context = context;
            _errorLog = errorLogRepository;
        }

        public async Task<List<WeekDaysModel>> GetWeekDaysListAsync()
        {
            string procName = "USP_GetWeekDays";
            List<WeekDaysModel> returnData = new List<WeekDaysModel>();
            try
            {
                returnData = await _context.QueryStoredProcListAsync<WeekDaysModel>(procName);
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }
        public async Task<List<RoleDepartmentSpecialityModel>> GetRoleDepartmentSpecialityHierarchyAsync()
        {
            string procName = "USP_GetRoleDepartmentSpecialityHierarchy";
            List<RoleDepartmentSpecialityModel> returnData = new List<RoleDepartmentSpecialityModel>();
            try
            {
                returnData = await _context.QueryStoredProcListAsync<RoleDepartmentSpecialityModel>(procName);
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }
        public async Task<List<RoleDataModel>> GetRoleListAsync()
        {
            string procName = "USP_GetRoleList";
            List<RoleDataModel> returnData = new List<RoleDataModel>();
            try
            {
                var param = new DynamicParameters();
                returnData = await _context.QueryStoredProcListAsync<RoleDataModel>(procName, param);
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }
        public async Task<List<DepartmentDataModel>> GetDepartmentByRoleIdAsync(int roleId)
        {
            string procName = "USP_GetDepartmentByRoleId";
            List<DepartmentDataModel> returnData = new List<DepartmentDataModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("RoleId", roleId);

                returnData = await _context.QueryStoredProcListAsync<DepartmentDataModel>(procName, param);
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }
        public async Task<List<SpecialityTypeModel>> GetSpecialityByDepartmentIdAsync(int departmentId)
        {
            string procName = "USP_GetSpecialityByDepartmentId";
            List<SpecialityTypeModel> returnData = new List<SpecialityTypeModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("DepartmentId", departmentId);

                returnData = await _context.QueryStoredProcListAsync<SpecialityTypeModel>(procName, param);
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }
        public async Task<List<DesignationDataModel>> GetDesignationByRoleIdAsync(int roleId)
        {
            string procName = "USP_GetDesignationByRoleId";
            List<DesignationDataModel> returnData = new List<DesignationDataModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("RoleId", roleId);

                returnData = await _context.QueryStoredProcListAsync<DesignationDataModel>(procName, param);
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }

        public async Task<List<AgeTypeModel>> GetAgeTypeListAsync()
        {
            string procName = "USP_GetAgeTypeList";
            List<AgeTypeModel> returnData = new List<AgeTypeModel>();
            try
            {
                returnData = await _context.QueryStoredProcListAsync<AgeTypeModel>(procName);
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }

        public async Task<List<RelationTypeModel>> GetRelationTypeListAsync()
        {
            string procName = "USP_GetRelationTypeList";
            List<RelationTypeModel> returnData = new List<RelationTypeModel>();
            try
            {
                returnData = await _context.QueryStoredProcListAsync<RelationTypeModel>(procName);
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }

        public async Task<List<StatusKeyModel>> GetStatusListAsync()
        {
            string procName = "USP_GetStatusList";
            List<StatusKeyModel> returnData = new List<StatusKeyModel>();
            try
            {
                returnData = await _context.QueryStoredProcListAsync<StatusKeyModel>(procName);
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }
    }
}
