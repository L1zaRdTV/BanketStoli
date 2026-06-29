using System.Configuration;
using System.Data.SqlClient;

namespace BanketStoli.Wpf.Data
{
    public static class Database
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["BanketStoliDb"].ConnectionString;

        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
