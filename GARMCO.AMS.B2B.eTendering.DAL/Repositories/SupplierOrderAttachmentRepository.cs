using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace GARMCO.AMS.B2B.eTendering.DAL
{
    public class SupplierOrderAttachmentRepository
    {
        private readonly string connectionString;

        public SupplierOrderAttachmentRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<SupplierOrderAttachment> GetSupplierOrderAttachment(byte? mode, double? orderAttachNo, double? orderAttachLineNo, decimal? orderAttachSeq, int? orderAttachSODID, int? orderAttachSODAltID, bool? orderAttachSupplier, decimal? orderAttachType)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { mode, orderAttachNo, orderAttachLineNo, orderAttachSeq, orderAttachSODID, orderAttachSODAltID, orderAttachSupplier, orderAttachType };
                return connection.Query<SupplierOrderAttachment>("b2badminuser.pr_GetOrderRequisitionDetailAttachment", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }
        public List<SupplierOrderAttachItem> GetSupplierOrderAttachmentList(byte? mode, double? orderAttachNo, double? orderAttachLineNo, decimal? orderAttachSeq, int? orderAttachSODID, int? orderAttachSODAltID, bool? orderAttachSupplier, decimal? orderAttachType)
        {
            var list = new List<SupplierOrderAttachItem>();
            var attachments = this.GetSupplierOrderAttachment(mode, orderAttachNo, orderAttachLineNo, orderAttachSeq, orderAttachSODID, orderAttachSODAltID, orderAttachSupplier, orderAttachType);

            if (attachments != null) foreach (var attachment in attachments) list.Add(new SupplierOrderAttachItem(attachment));

            return list;
        }
    }
}