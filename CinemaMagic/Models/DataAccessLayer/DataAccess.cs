using Microsoft.Data.SqlClient;
using System.Configuration;

namespace CinemaMagic.Models.DataAccessLayer
{
    public abstract class DataAccess
    {
        private readonly string _connectionString;
        public DataAccess()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CinemaMagic"].ConnectionString;
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
