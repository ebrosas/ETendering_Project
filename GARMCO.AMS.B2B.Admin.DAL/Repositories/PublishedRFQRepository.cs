using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class PublishedRFQRepository
    {
        private readonly string connectionString;

        public PublishedRFQRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<PublishedRFQ> GetPublishedRFQ()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                return connection.Query<PublishedRFQ>("b2badminuser.pr_GetPublishedRFQ", commandType: CommandType.StoredProcedure).AsList();
            }
        }
    }
}
