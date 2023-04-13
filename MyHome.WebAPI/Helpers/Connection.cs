using Microsoft.Data.SqlClient;
using System.Data;

namespace MyHome.WebAPI.Helpers
{
    public class Connection : IConnection
    {
        public string ConnectionString
        {
            get 
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);

                IConfiguration config = builder.Build();

                return config.GetValue<string>("ConnectionStrings:SQLConnection"); 
            }
        }
        public IDbConnection GetConnection
        {
            get { return new SqlConnection(ConnectionString); }
        }
    }
};