using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class StockItemRepository
    {
        private readonly string connectionString;

        public StockItemRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<StockItem> GetStockItem(string stockItemNo, int? currentYear, int? currentMonth)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { stockItemNo, currentYear, currentMonth };
                return connection.Query<StockItem>("b2badminuser.pr_GetStockItem", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }
    }
}
