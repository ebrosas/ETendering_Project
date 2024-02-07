using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using Dapper;
using GARMCO.AMS.B2B.Utility;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class OrderRequisitionRepository
    {
        private readonly string connectionString;
        private readonly RFQNoteRepository rfqNoteRepository;

        public OrderRequisitionRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
            this.rfqNoteRepository = new RFQNoteRepository();
        }

        public IEnumerable<OrderRequisition> GetOrderRequisition(byte? mode, double? orderNo, DateTime? orderCloseDateStart, DateTime? orderCloseDateEnd, int? orderOriginatorEmpNo, string orderOriginatorEmpName, int? orderBuyerEmpNo, string orderBuyerEmpName, int? orderSupplierNo, string orderSupplierName, string orderPriority, string orderStatus, string orderDesc, byte? orderCurrentlyAssignedTo, int? orderCurrentUser, int? startRowIndex, int? maximumRows, string sort)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                if (maximumRows == null) maximumRows = 10;
                var param = new { mode, orderNo, orderCloseDateStart, orderCloseDateEnd, orderOriginatorEmpNo, orderOriginatorEmpName, orderBuyerEmpNo, orderBuyerEmpName, orderSupplierNo, orderSupplierName, orderPriority, orderStatus, orderDesc, orderCurrentlyAssignedTo, orderCurrentUser, startRowIndex, maximumRows, sort };
                return connection.Query<OrderRequisition>("b2badminuser.pr_GetOrderRequisition", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }

        public int GetOrderRequisitionTotal(byte? mode, double? orderNo, DateTime? orderCloseDateStart, DateTime? orderCloseDateEnd, int? orderOriginatorEmpNo, string orderOriginatorEmpName, int? orderBuyerEmpNo, string orderBuyerEmpName, int? orderSupplierNo, string orderSupplierName, string orderPriority, string orderStatus, string orderDesc, byte? orderCurrentlyAssignedTo, int? orderCurrentUser, string sort)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { mode, orderNo, orderCloseDateStart, orderCloseDateEnd, orderOriginatorEmpNo, orderOriginatorEmpName, orderBuyerEmpNo, orderBuyerEmpName, orderSupplierNo, orderSupplierName, orderPriority, orderStatus, orderDesc, orderCurrentlyAssignedTo, orderCurrentUser };
                return connection.Query<int>("b2badminuser.pr_GetOrderRequisitionTotal", param, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }
        public void UpdateOrderRequisitionOpenedDate(string orderCompany, double? orderNo, string orderType, string orderSuffix, DateTime? orderOpenedDate, int? orderRFQID, int? orderCreatedModifiedBy, string orderCreatedModifiedName, ref int? retError, ref string errorMsg)
        {
            retError = B2BConstants.DB_STATUS_OK;
            errorMsg = String.Empty;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (var connection = new SqlConnection(this.connectionString))
                    {
                        var param = new DynamicParameters();
                        param.Add("@orderCompany", orderCompany);
                        param.Add("@orderNo", orderNo);
                        param.Add("@orderType", orderType);
                        param.Add("@orderSuffix", orderSuffix);
                        param.Add("@orderOpenedDate", orderOpenedDate);
                        param.Add("@orderRFQID", orderRFQID);
                        param.Add("@orderCreatedModifiedBy", orderCreatedModifiedBy);
                        param.Add("@orderCreatedModifiedName", orderCreatedModifiedName);
                        param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Execute("b2badminuser.pr_UpdateOrderRequisitionOpenedDate", param, commandType: CommandType.StoredProcedure);
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
        public void ReOpenOrderRequisition(string orderCompany, double? orderNo, string orderType, string orderSuffix, DateTime? orderCloseDate, int? orderTSID, string orderNote, string orderProgramID, string orderWorkStationID, int? orderCreatedModifiedBy, string orderCreatedModifiedName, ref int? retError, ref string errorMsg)
        {
            retError = B2BConstants.DB_STATUS_OK;
            errorMsg = String.Empty;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (var connection = new SqlConnection(this.connectionString))
                    {
                        var param = new DynamicParameters();
                        param.Add("@orderCompany", orderCompany);
                        param.Add("@orderNo", orderNo);
                        param.Add("@orderType", orderType);
                        param.Add("@orderSuffix", orderSuffix);
                        param.Add("@orderCloseDate", orderCloseDate);
                        param.Add("@orderTSID", orderTSID);
                        param.Add("@orderNote", orderNote);
                        param.Add("@orderCreatedModifiedBy", orderCreatedModifiedBy);
                        param.Add("@orderCreatedModifiedName", orderCreatedModifiedName);
                        param.Add("@orderProgramID", orderProgramID);
                        param.Add("@orderWorkStationID", orderWorkStationID);
                        param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Execute("b2badminuser.pr_ReOpenRFQ", param, commandType: CommandType.StoredProcedure);
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
        public void PushBackToTenderCommittee(string orderCompany, double? orderNo, string orderType, string orderSuffix, string orderRFQNote, int? orderCreatedModifiedBy, string orderCreatedModifiedName, ref int? retError, ref string errorMsg)
        {
            retError = B2BConstants.DB_STATUS_OK;
            errorMsg = String.Empty;

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    this.rfqNoteRepository.InsertUpdateDeleteRFQNote((byte)B2BConstants.DB_INSERT_RECORD, 0, orderNo, orderRFQNote, orderCreatedModifiedBy, orderCreatedModifiedName, ref retError);

                    if (retError == B2BConstants.DB_STATUS_OK)
                    {
                        this.InsertUpdateDeleteOrderRequisition(4, orderCompany, orderNo, orderType, orderSuffix, String.Empty, String.Empty, 0, String.Empty, String.Empty, 0, String.Empty, String.Empty, null, null, null, String.Empty, String.Empty, "201", false, false, true, orderCreatedModifiedBy, orderCreatedModifiedName, ref retError);
                        if (retError == B2BConstants.DB_STATUS_OK) scope.Complete();
                    }
                }
            }
            catch (Exception exception)
            {
                retError = B2BConstants.DB_STATUS_ERROR;
                errorMsg = exception.Message;
            }
        }
        public void InsertUpdateDeleteOrderRequisition(byte? mode, string orderCompany, double? orderNo, string orderType, string orderSuffix, string orderDesc, string orderPRNo, int? orderOriginatorEmpNo, string orderOriginatorEmpName, string orderOriginatorEmpEmail, int? orderBuyerEmpNo, string orderBuyerEmpName, string orderBuyerEmail, DateTime? orderPublishedDate, DateTime? orderTransactionDate, DateTime? orderClosingDate, string orderCategory, string orderPriority, string orderStatus, bool? orderUploaded, bool? orderDownloaded, bool? orderTenderComm, int? orderCreatedModifiedBy, string orderCreatedModifiedName, ref int? retError)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@mode", mode);
                param.Add("@orderCompany", orderCompany);
                param.Add("@orderNo", orderNo);
                param.Add("@orderType", orderType);
                param.Add("@orderSuffix", orderSuffix);
                param.Add("@orderDesc", orderDesc);
                param.Add("@orderPRNo", orderPRNo);
                param.Add("@orderOriginatorEmpNo", orderOriginatorEmpNo);
                param.Add("@orderOriginatorEmpName", orderOriginatorEmpName);
                param.Add("@orderOriginatorEmpEmail", orderOriginatorEmpEmail);
                param.Add("@orderBuyerEmpNo", orderBuyerEmpNo);
                param.Add("@orderBuyerEmpName", orderBuyerEmpName);
                param.Add("@orderBuyerEmail", orderBuyerEmail);
                param.Add("@orderPublishedDate", orderPublishedDate);
                param.Add("@orderTransactionDate", orderTransactionDate);
                param.Add("@orderClosingDate", orderClosingDate);
                param.Add("@orderCategory", orderCategory);
                param.Add("@orderPriority", orderPriority);
                param.Add("@orderStatus", orderStatus);
                param.Add("@orderUploaded", orderUploaded);
                param.Add("@orderDownloaded", orderDownloaded);
                param.Add("@orderTenderComm", orderTenderComm);
                param.Add("@orderCreatedModifiedBy", orderCreatedModifiedBy);
                param.Add("@orderCreatedModifiedName", orderCreatedModifiedName);
                param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("b2badminuser.pr_InsertUpdateDeleteOrderRequisition", param, commandType: CommandType.StoredProcedure);
                retError = param.Get<int>("@retError");
            }
        }
        public void SendBackToBuyer(string orderCompany, double? orderNo, string orderType, string orderSuffix, int? orderRFQID, string orderRFQNote, int? orderCreatedModifiedBy, string orderCreatedModifiedName, ref int? retError, ref string errorMsg)
        {
            retError = B2BConstants.DB_STATUS_OK;
            errorMsg = string.Empty;

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    using (var connection = new SqlConnection(this.connectionString))
                    {
                        var param = new DynamicParameters();
                        param.Add("@orderCompany", orderCompany);
                        param.Add("@orderNo", orderNo);
                        param.Add("@orderType", orderType);
                        param.Add("@orderSuffix", orderSuffix);
                        param.Add("@orderRFQID", orderRFQID);
                        param.Add("@orderCreatedModifiedBy", orderCreatedModifiedBy);
                        param.Add("@orderCreatedModifiedName", orderCreatedModifiedName);
                        param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Execute("b2badminuser.pr_SendBackToBuyer", param, commandType: CommandType.StoredProcedure);
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
        public IEnumerable<PublishedRFQInvitedSupplier> GetSendRFQReminder(double? orderNo, int? orderModifiedBy, string orderModifiedName, ref int? retError, ref string errorMsg)
        {
            retError = B2BConstants.DB_STATUS_OK;
            errorMsg = string.Empty;
            IEnumerable<PublishedRFQInvitedSupplier> dataTable = null;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (var connection = new SqlConnection(this.connectionString))
                    {
                        var param = new DynamicParameters();
                        param.Add("@orderNo", orderNo);
                        param.Add("@orderModifiedBy", orderModifiedBy);
                        param.Add("@orderModifiedName", orderModifiedName);
                        param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        dataTable = connection.Query<PublishedRFQInvitedSupplier>("b2badminuser.pr_SendRFQReminder", param, commandType: CommandType.StoredProcedure);
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

            return dataTable;
        }

        public void Cancel(string orderDetCompany, double? orderDetNo, string orderDetType, string orderDetSuffix, int? orderDetModifiedBy, string orderDetModifiedName, string orderDetProgramID, ref int? retError, ref string errorMsg)
        {
            this.UpdatePublishedRFQ(
                3,
                orderDetCompany,
                orderDetNo,
                orderDetType,
                orderDetSuffix,
                0,
                null,
                null,
                null,
                orderDetModifiedBy,
                orderDetModifiedName,
                orderDetProgramID,
                null,
                ref retError,
                ref errorMsg);
        }
        public void CancelLine(string orderDetCompany, double? orderDetNo, string orderDetType, string orderDetSuffix, double? orderDetLineNo, int? orderDetModifiedBy, string orderDetModifiedName, string orderDetProgramID, ref int? retError, ref string errorMsg)
        {
            UpdatePublishedRFQ(
                1,
                orderDetCompany,
                orderDetNo,
                orderDetType,
                orderDetSuffix,
                orderDetLineNo,
                null,
                null,
                null,
                orderDetModifiedBy,
                orderDetModifiedName,
                orderDetProgramID,
                null,
                ref retError,
                ref errorMsg);
        }
        public void UpdateClosingDate(string orderDetCompany, double? orderDetNo, string orderDetType, string orderDetSuffix, DateTime? orderDetCloseDate, int? orderDetModifiedBy, string orderDetModifiedName, string orderDetProgramID, ref int? retError, ref string errorMsg)
        {
            UpdatePublishedRFQ(
                2,
                orderDetCompany,
                orderDetNo,
                orderDetType,
                orderDetSuffix,
                0,
                null,
                null,
                orderDetCloseDate,
                orderDetModifiedBy,
                orderDetModifiedName,
                orderDetProgramID,
                null,
                ref retError,
                ref errorMsg);
        }
        public void UpdatePublishedRFQ(byte? mode, string orderDetCompany, double? orderDetNo, string orderDetType, string orderDetSuffix, double? orderDetLineNo, string orderDetLastStatus, string orderDetNextStatus, DateTime? orderDetCloseDate, int? orderDetModifiedBy, string orderDetModifiedName, string orderDetProgramID, string orderDetWorkstation, ref int? retError, ref string errorMsg)
        {
            retError = B2BConstants.DB_STATUS_OK;
            errorMsg = String.Empty;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    this.UpdatePublishedRFQ(mode, orderDetCompany, orderDetNo, orderDetType, orderDetSuffix, orderDetLineNo, orderDetLastStatus, orderDetNextStatus, orderDetCloseDate, orderDetModifiedBy, orderDetModifiedName, orderDetProgramID, orderDetWorkstation, ref retError);
                    if (retError == B2BConstants.DB_STATUS_OK) scope.Complete();
                }
            }
            catch (Exception exception)
            {
                retError = B2BConstants.DB_STATUS_ERROR;
                errorMsg = exception.Message;
            }
        }
        public void UpdatePublishedRFQ(byte? mode, string orderDetCompany, double? orderDetNo, string orderDetType, string orderDetSuffix, double? orderDetLineNo, string orderDetLastStatus, string orderDetNextStatus, DateTime? orderDetCloseDate, int? orderDetModifiedBy, string orderDetModifiedName, string orderDetProgramID, string orderDetWorkstation, ref int? retError)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@mode", mode);
                param.Add("@orderDetCompany", orderDetCompany);
                param.Add("@orderDetNo", orderDetNo);
                param.Add("@orderDetType", orderDetType);
                param.Add("@orderDetSuffix", orderDetSuffix);
                param.Add("@orderDetLineNo", orderDetLineNo);
                param.Add("@orderDetLastStatus", orderDetLastStatus);
                param.Add("@orderDetNextStatus", orderDetNextStatus);
                param.Add("@orderDetCloseDate", orderDetCloseDate);
                param.Add("@orderDetModifiedBy", orderDetModifiedBy);
                param.Add("@orderDetModifiedName", orderDetModifiedName);
                param.Add("@orderDetProgramID", orderDetProgramID);
                param.Add("@orderDetWorkstation", orderDetWorkstation);
                param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("b2badminuser.pr_UpdatePublishedRFQ", param, commandType: CommandType.StoredProcedure);
                retError = param.Get<int>("@retError");
            }
        }
    }
}
