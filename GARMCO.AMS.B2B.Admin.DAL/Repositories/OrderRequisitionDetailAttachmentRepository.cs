using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class OrderRequisitionDetailAttachmentRepository
    {
        private readonly string connectionString;

        public OrderRequisitionDetailAttachmentRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<DetailAttachment> GetOrderRequisitionDetailAttachment(byte? mode, double? orderAttachNo, double? orderAttachLineNo, decimal? orderAttachSeq, int? orderAttachSODID, int? orderAttachSODAltID, bool? orderAttachSupplier, decimal? orderAttachType)
        {
            var sql = "b2badminuser.pr_GetOrderRequisitionDetailAttachment";
            var param = new { mode, orderAttachNo, orderAttachLineNo, orderAttachSeq, orderAttachSODID, orderAttachSODAltID, orderAttachSupplier, orderAttachType };

            return
                Disposable
                    .Using(
                        () => new SqlConnection(this.connectionString),
                        connection => connection.Query<DetailAttachment>(sql, param, commandType: CommandType.StoredProcedure))
                    .AsList();
        }
        public void InsertUpdateDeleteOrderRequisitionDetailAttachment(byte? mode, double? orderAttachNo, double? orderAttachLineNo, double? orderAttachSeq, bool? orderAttachSupplier, int? orderAttachSODID, int? orderAttachSODAltID, decimal? orderAttachType, string orderAttachDisplayName, string orderAttachFilename, byte[] orderAttachContent, int? orderAttachCreatedModifiedBy, string orderAttachCreatedModifiedName, ref int? retError)
        {
            var sql = "b2badminuser.pr_InsertUpdateDeleteOrderRequisitionDetailAttachment";
            var param = new DynamicParameters();
            param.Add("@mode", mode);
            param.Add("@orderAttachNo", orderAttachNo);
            param.Add("@orderAttachLineNo", orderAttachLineNo);
            param.Add("@orderAttachSeq", orderAttachSeq);
            param.Add("@orderAttachSupplier", orderAttachSupplier);
            param.Add("@orderAttachSODID", orderAttachSODID);
            param.Add("@orderAttachSODAltID", orderAttachSODAltID);
            param.Add("@orderAttachType", orderAttachType);
            param.Add("@orderAttachDisplayName", orderAttachDisplayName);
            param.Add("@orderAttachFilename", orderAttachFilename);
            param.Add("@orderAttachContent", orderAttachContent);
            param.Add("@orderAttachCreatedModifiedBy", orderAttachCreatedModifiedBy);
            param.Add("@orderAttachCreatedModifiedName", orderAttachCreatedModifiedName);
            param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = new SqlConnection(this.connectionString))
            {

                connection.Execute(sql, param, commandType: CommandType.StoredProcedure);
                retError = param.Get<int>("@retError");
            }
        }
    }
}
