using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class PublishedRfqInvitedSupplierContactRepository
    {
        private readonly string connectionString;

        public PublishedRfqInvitedSupplierContactRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<PublishedRFQInvitedSupplierContact> GetPublishedRFQInvitedSupplierContact(double supplierJDERefNo)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { supplierJDERefNo };
                return connection.Query<PublishedRFQInvitedSupplierContact>("b2badminuser.pr_GetPublishedRFQInvitedSupplierContact", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }
    }
}