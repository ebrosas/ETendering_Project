using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Dapper;
using GARMCO.AMS.B2B.Utility;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class SupplierInvitedBiddedRepository
    {
        private readonly string connectionString;

        public SupplierInvitedBiddedRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<SupplierInvBidded> GetSupplierInvitedBidded(bool? orderSupplierInvitedOnly, double? orderNo, double? orderLineNo)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { orderSupplierInvitedOnly, orderNo, orderLineNo };
                return connection.Query<SupplierInvBidded>("b2badminuser.pr_GetSupplierInvitedBidded", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }
        public void InsertUpdateDeleteInvitedSupplier(byte? mode, double? supInvJDERefNo, double? supInvOrderNo, bool? supInvDeclined, int? supInvCreatedModifiedBy, string supInvCreatedModifiedName, ref int? retError)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@mode", mode);
                param.Add("@supInvJDERefNo", supInvJDERefNo);
                param.Add("@supInvOrderNo", supInvOrderNo);
                param.Add("@supInvDeclined", supInvDeclined);
                param.Add("@supInvCreatedModifiedBy", supInvCreatedModifiedBy);
                param.Add("@supInvCreatedModifiedName", supInvCreatedModifiedName);
                param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("b2badminuser.pr_InsertUpdateDeleteInvitedSupplier", param, commandType: CommandType.StoredProcedure);
                retError = param.Get<int>("@retError");
            }
        }
        public void InsertUpdateDeleteInvitedSupplier(ref byte? mode, double? supInvJDERefNo, double? supInvOrderNo, bool? supInvDeclined, int? supInvCreatedModifiedBy, string supInvCreatedModifiedName, ref int? retError, ref string errorMsg)
        {
            retError = B2BConstants.DB_STATUS_OK;
            errorMsg = string.Empty;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (var connection = new SqlConnection(this.connectionString))
                    {
                        var param = new DynamicParameters();
                        param.Add("@mode", mode);
                        param.Add("@supInvJDERefNo", supInvJDERefNo);
                        param.Add("@supInvOrderNo", supInvOrderNo);
                        param.Add("@supInvDeclined", supInvDeclined);
                        param.Add("@supInvCreatedModifiedBy", supInvCreatedModifiedBy);
                        param.Add("@supInvCreatedModifiedName", supInvCreatedModifiedName);
                        param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Execute("b2badminuser.pr_InsertUpdateDeleteInvitedSupplier", param, commandType: CommandType.StoredProcedure);
                        retError = param.Get<int>("@retError");
                    }
                    if (retError == B2BConstants.DB_STATUS_OK) scope.Complete();
                }
            }
            catch (Exception exception)
            {
                retError = B2BConstants.DB_STATUS_ERROR;
                errorMsg = exception.Message;
            }
        }
    }
}
