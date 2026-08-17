using Dapper;
using System.Data;
using Medicare.Application.Interfaces.Dapper;
using Microsoft.Extensions.Logging;
using Medicare.Application.Models.CommonModels.ErrorLog;

namespace Medicare.DAL.Persistence.Dapper
{
    public class DapperContext : IDapperContext
    {
        private readonly IDbConnectionFactory _factory;
        private readonly ILogger<DapperContext> _logger;
        private ErrorLogModel errorLog;
        public DapperContext(IDbConnectionFactory factory, ILogger<DapperContext> logger)
        {
            _factory = factory;
            _logger = logger;
        }

        // Raw SQL 
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryAsync<T>(sql, param, commandType: CommandType.Text);
        }

        public async Task<T> QuerySingleAsync<T>(string sql, object param = null)
        {
            using var connection = _factory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(sql, param, commandType: CommandType.Text);
        }

        // Stored Procs 
        public async Task<IEnumerable<T>> QueryStoredProcAsync<T>(string procName, object param = null)
        {
            return await ExecuteWithLoggingAsync(async (conn) =>
                await conn.QueryAsync<T>(procName, param, commandType: CommandType.StoredProcedure),
                procName, param);
        }

        public async Task<T> QuerySingleStoredProcAsync<T>(string procName, object param = null)
        {
            return await ExecuteWithLoggingAsync(async (conn) =>
                await conn.QueryFirstOrDefaultAsync<T>(procName, param, commandType: CommandType.StoredProcedure),
                procName, param);
        }

        public async Task<int> ExecuteStoredProcAsync(string procName, object param = null)
        {
            return await ExecuteWithLoggingAsync(async (conn) =>
                await conn.ExecuteAsync(procName, param, commandType: CommandType.StoredProcedure),
                procName, param);
        }

        public async Task<List<T>> QueryStoredProcListAsync<T>(string procName, object param = null)
        {
            var result = await ExecuteWithLoggingAsync(async (conn) =>
                await conn.QueryAsync<T>(procName, param, commandType: CommandType.StoredProcedure),
                procName, param);

            return result.ToList();
        }

        private async Task<TResult> ExecuteWithLoggingAsync<TResult>(
        Func<IDbConnection, Task<TResult>> operation,
        string procName,
        object param)
        {
            try
            {
                using var connection = _factory.CreateConnection();
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                return await operation(connection);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database operation failed. ProcName: {ProcName}. Params: {@Params}", procName, param);
                throw;
            }
        }
    }
    public interface IDapperContext
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null);
        Task<T> QuerySingleAsync<T>(string sql, object param = null);
        Task<IEnumerable<T>> QueryStoredProcAsync<T>(string procName, object param = null);
        Task<T> QuerySingleStoredProcAsync<T>(string procName, object param = null);
        Task<List<T>> QueryStoredProcListAsync<T>(string procName, object param = null);
        Task<int> ExecuteStoredProcAsync(string procName, object param = null);
    }
}
