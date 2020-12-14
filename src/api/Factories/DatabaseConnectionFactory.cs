using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace api.Factories
{
    public class DatabaseConnectionFactory
    {
        private IConfiguration _configuration;

        public DatabaseConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Create()
        {
            var connectionString = new SqlConnectionStringBuilder();
            connectionString.ApplicationName = "Wall";
            connectionString.Authentication = SqlAuthenticationMethod.SqlPassword;
            connectionString.DataSource = _configuration["Connection:Server"];
            connectionString.InitialCatalog = _configuration["Connection:Database"];
            connectionString.UserID = _configuration["Connection:UserName"];
            connectionString.Password = _configuration["Connection:Password"];

            return new SqlConnection(connectionString.ConnectionString); 
        }
    }
}