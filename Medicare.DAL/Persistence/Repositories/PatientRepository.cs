using Dapper;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Interfaces.IPatient;
using Medicare.Application.Models.CommonModels.ErrorLog;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Patient;
using Medicare.DAL.Persistence.Dapper;
namespace Medicare.DAL.Persistence.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IDapperContext _context;
        private readonly IErrorLogRepository _errorLog;
        public PatientRepository(IDapperContext context, IErrorLogRepository errorLog) 
        {
            _context = context;
            _errorLog = errorLog;
        }
        public async Task<ResponseModel> CreatePatientDetails(PatientMasterModel model)
        {
            string procName = "USP_RegisterPatientAccount";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("FirstName", model.FirstName);
                param.Add("MiddleName", model.MiddleName);
                param.Add("LastName", model.LastName);
                param.Add("DateOfBirth", model.DateOfBirth);
                param.Add("PhoneCountryCode", model.PhoneCountryCode);
                param.Add("PhoneNumber", model.PhoneNumber);
                param.Add("Email", model.Email);
                param.Add("Gender", model.Gender);
                param.Add("AddressLine1", model.AddressLine1);
                param.Add("AddressLine2", model.AddressLine2);
                param.Add("CityId", model.CityId);
                param.Add("ZipCode", model.ZipCode);
                param.Add("StateId", model.StateId);
                param.Add("CountryId", model.CountryId);
                param.Add("Username", model.Username);
                param.Add("Password", model.PasswordHash);
                param.Add("SecurityQuestionId", model.SecurityQuestionId);
                param.Add("SecurityAnswerHash", model.SecurityAnswerHash);
                param.Add("CreatedBy", model.CreatedBy);
                returnData = await _context.QuerySingleStoredProcAsync<ResponseModel>(procName, param);
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

        public async Task<ResponseModel> UpdatePatientDetails(UpdatePatientRequestModel model)
        {
            string procName = "USP_UpdatePatientAccount";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("PatientId", model.PatientId);
                param.Add("FirstName", model.FirstName);
                param.Add("MiddleName", model.MiddleName);
                param.Add("LastName", model.LastName);
                param.Add("DateOfBirth", model.DateOfBirth);
                param.Add("PhoneCountryCode", model.PhoneCountryCode);
                param.Add("PhoneNumber", model.PhoneNumber);
                param.Add("Email", model.Email);
                param.Add("Gender", model.Gender);
                param.Add("AddressLine1", model.AddressLine1);
                param.Add("AddressLine2", model.AddressLine2);
                param.Add("CityId", model.CityId);
                param.Add("ZipCode", model.ZipCode);
                param.Add("StateId", model.StateId);
                param.Add("CountryId", model.CountryId);
                param.Add("IsActive", model.IsActive);
                param.Add("UpdatedBy", model.UpdatedBy);
                returnData = await _context.QuerySingleStoredProcAsync<ResponseModel>(procName, param);
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

        public async Task<PatientMasterModel> GetPatientByContact(string contactNo)
        {
            string procName = "USP_GetPatientByContactNo";
            PatientMasterModel returnData = new PatientMasterModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("PhoneNumber", contactNo);
                returnData = await _context.QuerySingleStoredProcAsync<PatientMasterModel>(procName, param);

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

        public async Task<PatientDetailModel> GetPatientById(int Id)
        {
            string procName = "USP_GetPatientAccountById";
            PatientDetailModel returnData = new PatientDetailModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("PatientId", Id);
                returnData = await _context.QuerySingleStoredProcAsync<PatientDetailModel>(procName, param);

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
     
        public async Task<PatientDetailModel> GetPatientInfoByUsername(string Username)
        {
            string procName = "USP_GetPatientAccountByUsername";
            PatientDetailModel returnData = new PatientDetailModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("Username", Username);

                returnData = await _context.QuerySingleStoredProcAsync<PatientDetailModel>(procName, param);
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

        public async Task<List<PatientProfileModel>> GetPatientProfileListByIdAsync(int patientId)
        {
            string procName = "USP_GetPatientProfileListById";
            List<PatientProfileModel> returnData = new List<PatientProfileModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("PatientId", patientId);

                returnData = await _context.QueryStoredProcListAsync<PatientProfileModel>(procName, param);
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
        public async Task<PatientProfileModelDto> GetPatientProfileByProfileIdAsync(int profileId)
        {
            string procName = "USP_GetPatientProfileByProfileId";
            PatientProfileModelDto returnData = new PatientProfileModelDto();
            try
            {
                var param = new DynamicParameters();
                param.Add("ProfileId", profileId);

                returnData = await _context.QuerySingleStoredProcAsync<PatientProfileModelDto>(procName, param);
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
