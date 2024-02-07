using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace GARMCO.AMS.B2B.eTendering.DAL
{
    public class SupplierOrderAlternativeRepository
    {
        private readonly string connectionString;

        public SupplierOrderAlternativeRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<SupplierOrderAlternative> GetSupplierOrderAlternative(byte? mode, int? sodAltSODID)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { mode, sodAltSODID };
                return connection.Query<SupplierOrderAlternative>("b2badminuser.pr_GetSupplierOrderAlternative", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }
        public List<SupplierOrderDetailAltItem> GetSupplierOrderAlternativeList(byte? mode, int? sodAltSODID)
        {
            var list = new List<SupplierOrderDetailAltItem>();
            var supplierOrderAlternatives = this.GetSupplierOrderAlternative(mode, sodAltSODID);

            if (supplierOrderAlternatives != null) foreach (var supplierOrderAlternative in supplierOrderAlternatives) list.Add(new SupplierOrderDetailAltItem(supplierOrderAlternative));

            return list;
        }
    }
}