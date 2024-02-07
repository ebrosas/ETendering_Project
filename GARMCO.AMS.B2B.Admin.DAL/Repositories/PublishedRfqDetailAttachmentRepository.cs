using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class PublishedRfqDetailAttachmentRepository
    {
        private readonly string connectionString;

        public PublishedRfqDetailAttachmentRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<PublishedRfqDetailAttachment> GetPublishedRFQDetailAttachment(string mediaObjName, string mediaObjOrderNo, string mediaObjCompany, string mediaObjDocType, double? mediaObjLineNo)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { mediaObjName, mediaObjOrderNo, mediaObjCompany, mediaObjDocType, mediaObjLineNo };
                return connection.Query<PublishedRfqDetailAttachment>("b2badminuser.pr_GetPublishedRFQDetailAttachment", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }
    }
}
