using Dapper;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Interfaces.ILocations;
using Medicare.Application.Models.CommonModels.ErrorLog;
using Medicare.Application.Models.Location;
using Medicare.DAL.Persistence.Dapper;

namespace Medicare.DAL.Persistence.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly IDapperContext _context;
        private readonly IErrorLogRepository _errorLog;

        public LocationRepository(IDapperContext context, IErrorLogRepository errorLog)
        {
            _context = context;
            _errorLog = errorLog;
        }

        public async Task<List<CountryModel>> GetCountriesAsync()
        {
            string procName = "USP_GetCountries";
            List<CountryModel> returnData = new();
            try
            {
                returnData = await _context.QueryStoredProcListAsync<CountryModel>(procName, null);
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

        public async Task<List<StateModel>> GetStatesByCountryAsync(int countryId)
        {
            string procName = "USP_GetStatesByCountry";
            List<StateModel> returnData = new();
            try
            {
                var param = new DynamicParameters();
                param.Add("CountryId", countryId);
                returnData = await _context.QueryStoredProcListAsync<StateModel>(procName, param);
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

        public async Task<List<CityModel>> GetCitiesByStateAsync(int stateId)
        {
            string procName = "USP_GetCitiesByState";
            List<CityModel> returnData = new();
            try
            {
                var param = new DynamicParameters();
                param.Add("StateId", stateId);
                returnData = await _context.QueryStoredProcListAsync<CityModel>(procName, param);
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
