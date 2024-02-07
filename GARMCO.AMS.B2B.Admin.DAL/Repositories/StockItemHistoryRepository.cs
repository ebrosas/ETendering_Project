using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class StockItemHistoryRepository
    {
        private readonly string connectionString;

        public StockItemHistoryRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<StockItemHistory> GetStockItemHistory(string stockItemNo, int? startRowIndex, int? maximumRows, string sort)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                if (maximumRows == null || maximumRows == 0) maximumRows = 10;
                var param = new { stockItemNo, startRowIndex, maximumRows, sort };
                return connection.Query<StockItemHistory>("b2badminuser.pr_GetStockItemHistory", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }
        public int GetStockItemHistoryTotal(string stockItemNo, string sort)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { stockItemNo };
                return connection.Query<int>("b2badminuser.pr_GetStockItemHistoryTotal", param, commandType: CommandType.StoredProcedure).Single();
            }
        }
    }
}
