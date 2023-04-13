using System.Data;

namespace MyHome.WebAPI.Helpers
{
    public interface IConnection
    {
        string ConnectionString { get; }    
        IDbConnection GetConnection { get; }
    }
}
