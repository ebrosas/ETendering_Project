using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class OrderRequisitionHistoryRepository
    {
        private readonly string connectionString;

        public OrderRequisitionHistoryRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<OrderRequisitionHistory> GetOrderRequisitionHistory(double? ohOrderNo, int? startRowIndex, int? maximumRows)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                if (maximumRows == null) maximumRows = 10;
                var param = new { ohOrderNo, startRowIndex, maximumRows };
                return connection.Query<OrderRequisitionHistory>("b2badminuser.pr_GetOrderRequisitionHistory", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }

        public int GetOrderRequisitionHistoryTotal(double? ohOrderNo)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { ohOrderNo };
                return connection.Query<int>("b2badminuser.pr_GetOrderRequisitionHistoryTotal", param, commandType: CommandType.StoredProcedure).Single();
            }
        }
    }
}
