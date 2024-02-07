using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class PublishedRFQInvitedSupplierRepository
    {
        private readonly string connectionString;

        public PublishedRFQInvitedSupplierRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<PublishedRFQInvitedSupplier> GetPublishedRFQInvitedSupplier(byte? mode, string orderCompany, double? orderNo, string orderType, string orderSuffix, string orderDetUNSPSC, double? orderSupplierJDERefNo = 0)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { mode, orderCompany, orderNo, orderType, orderSuffix, orderDetUNSPSC, orderSupplierJDERefNo };
                return connection.Query<PublishedRFQInvitedSupplier>("b2badminuser.pr_GetPublishedRFQInvitedSupplier", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }
    }
}
