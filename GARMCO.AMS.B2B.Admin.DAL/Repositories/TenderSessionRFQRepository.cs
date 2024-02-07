using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class TenderSessionRFQRepository
    {
        private readonly string connectionString;

        public TenderSessionRFQRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<TenderSessionRFQ> GetTenderSessionRFQ(int? rfqTSID)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { rfqTSID };
                return connection.Query<TenderSessionRFQ>("b2badminuser.pr_GetTenderSessionRFQ", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }
    }
}
