using Medicare.Application.Interfaces.Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Medicare.DAL.Persistence.Dapper
{
    public class DapperConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        public DapperConnectionFactory(string connectionString) 
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
