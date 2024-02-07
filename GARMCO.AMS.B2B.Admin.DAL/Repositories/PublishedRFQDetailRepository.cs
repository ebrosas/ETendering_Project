using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class PublishedRFQDetailRepository
    {
        private readonly string connectionString;

        public PublishedRFQDetailRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<PublishedRFQDetail> GetPublishedRFQDetail(string orderDetCompany, double? orderDetNo, string orderDetType, string orderDetSuffix)
        {
            try
            {
                using (var connection = new SqlConnection(this.connectionString))
                {
                    var param = new { orderDetCompany, orderDetNo, orderDetType, orderDetSuffix };
                    return connection.Query<PublishedRFQDetail>("b2badminuser.pr_GetPublishedRFQDetail", param, commandType: CommandType.StoredProcedure).AsList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
