using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace GARMCO.AMS.B2B.eTendering.DAL
{
    public class SupplierProductServiceRepository
    {
        private readonly string connectionString;

        public SupplierProductServiceRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public void Update(int? prodServSupplierNo, string prodServCode, byte? prodServCheckState, ref int? retError)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@prodServSupplierNo", prodServSupplierNo);
                param.Add("@prodServCode", prodServCode);
                param.Add("@prodServCheckState", prodServCheckState);
                param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("b2badminuser.pr_InsertUpdateDeleteSupplierProductService", param, commandType: CommandType.StoredProcedure);
                retError = param.Get<int>("@retError");
            }
        }
        public IEnumerable<SupplierProductService> GetSupplierProductService(byte? mode, int? prodServSupplierNo, string prodServCode, string prodServCodeDesc, byte? prodServParentLevel, int? startRowIndex, int? maximumRows, string sort)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { mode, prodServSupplierNo, prodServCode, prodServCodeDesc, prodServParentLevel, startRowIndex, maximumRows, sort };
                return connection.Query<SupplierProductService>("b2badminuser.pr_GetSupplierProductService", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }
        public List<SupplierProdServItem> GetSupplierProductServiceList(byte? mode, int? prodServSupplierNo)
        {
            List<SupplierProdServItem> list = new List<SupplierProdServItem>();

            var supplierProductServices = this.GetSupplierProductService(mode, prodServSupplierNo, string.Empty, string.Empty, 0, 0, 10, string.Empty);
            if (supplierProductServices != null) foreach (var supplierProductService in supplierProductServices) list.Add(new SupplierProdServItem(supplierProductService));

            return list;
        }
    }
}
