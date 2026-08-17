using System.Data;

namespace Medicare.Application.Interfaces.Dapper
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
