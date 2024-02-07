using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace GARMCO.AMS.B2B.eTendering.DAL
{
    public class SupplierOrderChargeRepository
    {
        private readonly string connectionString;

        public SupplierOrderChargeRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<SupplierOrderCharge> GetSupplierOrderOtherCharge(int? otherChgSupOrderID, double? otherChgOrderNo)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { otherChgSupOrderID, otherChgOrderNo };
                return connection.Query<SupplierOrderCharge>("b2badminuser.pr_GetSupplierOrderOtherCharge", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }
        public List<SupplierOrderOtherChargeItem> GetSupplierOrderOtherChargeList(int? otherChgSupOrderID, double? otherChgOrderNo)
        {
            var list = new List<SupplierOrderOtherChargeItem>();
            var charges = this.GetSupplierOrderOtherCharge(otherChgSupOrderID, otherChgOrderNo);

            if (charges != null) foreach (var charge in charges) list.Add(new SupplierOrderOtherChargeItem(charge));

            return list;
        }
    }
}