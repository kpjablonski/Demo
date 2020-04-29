using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Tenders.AdsSearch
{
    public class SqlConnectionFactory
    {
        private SqlConnectionStringBuilder bzp;
        public SqlConnectionFactory(SqlConnectionStringBuilder bzp)
        {
            this.bzp = bzp;
        }
        public async Task<SqlConnection> CreateAsync()
        {
            var connection = new SqlConnection();
            connection.ConnectionString = bzp.ConnectionString;
            await connection.OpenAsync();
            return connection;
        }
    }
}
