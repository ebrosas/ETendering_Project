using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class TenderSessionHistoryRepository
    {
        private readonly string connectionString;

        public TenderSessionHistoryRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<TenderSessionHistory> GetTenderSessionHistory(int? tsHistID)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { tsHistID };
                return connection.Query<TenderSessionHistory>("b2badminuser.pr_GetTenderSessionHistory", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }
    }
}
