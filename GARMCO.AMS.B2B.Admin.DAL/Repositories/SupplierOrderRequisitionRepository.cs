using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using Dapper;
using GARMCO.AMS.B2B.Utility;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class SupplierOrderRequisitionRepository
    {
        private readonly string connectionString;
        private readonly RFQNoteRepository rfqNoteRepository;

        public SupplierOrderRequisitionRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
            this.rfqNoteRepository = new RFQNoteRepository();
        }

        public List<SupplierOrderRequisitionItem> GetSupplierOrderRequisitionList(double? rfqOrderNo, double? rfqOrderID, bool? rfqOrderAlt)
        {
            var list = new List<SupplierOrderRequisitionItem>();
            var supplierOrderRequisitions = new List<SupplierOrderRequisition>();

            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { rfqOrderNo, rfqOrderID, rfqOrderAlt };
                supplierOrderRequisitions = connection.Query<SupplierOrderRequisition>("b2badminuser.pr_GetSupplierOrderRequisition", param, commandType: CommandType.StoredProcedure).AsList();
            }

            foreach (var supplierOrderRequisition in supplierOrderRequisitions) list.Add(new SupplierOrderRequisitionItem(supplierOrderRequisition));

            return list;
        }
        public void UpdateSelectedBids(bool? orderDetTenderComm, string orderCompany, double? orderNo, string orderType, string orderSuffix, int? orderRFQID, bool? orderRFQProcessed, string orderRFQNote, List<SupplierOrderRequisitionItem> supOrderDetList, string orderDetProgramID, string orderDetWorkstationID, int? orderDetCreatedModifiedBy, string orderDetCreatedModifiedName, ref int? retError, ref string errorMsg)
        {
            retError = B2BConstants.DB_STATUS_OK;
            errorMsg = string.Empty;

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    this.UpdateSelectedBid(orderDetTenderComm, orderCompany, orderNo, orderType, orderSuffix, orderRFQID, orderRFQProcessed, orderRFQNote, orderDetCreatedModifiedBy, orderDetCreatedModifiedName, ref retError);
                    if (orderDetCreatedModifiedName.Length > 10) orderDetCreatedModifiedName = orderDetCreatedModifiedName.Substring(0, 10);

                    if (retError == B2BConstants.DB_STATUS_OK)
                    {
                        if (orderDetTenderComm == false)
                        {
                            var rfqNotes = this.rfqNoteRepository.GetRFQNote(2, 0, orderNo, String.Empty, orderDetCreatedModifiedBy, 0, 10, "NoteCreatedDate");

                            if (rfqNotes != null)
                            {
                                RichTextBox txtRTF = new RichTextBox();

                                foreach (var rfqNote in rfqNotes)
                                {
                                    txtRTF.Text = rfqNote.NoteRemark;
                                    byte[] mediaObjTextList = Encoding.Unicode.GetBytes(txtRTF.Rtf);
                                    this.UploadRFQNote("GT4301", orderNo, 0, 1, 1, "Text", String.Empty, String.Empty, mediaObjTextList, "OQ", orderDetCreatedModifiedBy, orderDetCreatedModifiedName, ref retError);
                                    if (retError != B2BConstants.DB_STATUS_OK) break;
                                }
                            }
                        }

                        if (retError == B2BConstants.DB_STATUS_OK)
                        {
                            foreach (SupplierOrderRequisitionItem item in supOrderDetList)
                            {
                                this.UpdateSelectedBidDetail(orderDetTenderComm, item.OrderDetCompany, item.OrderDetNo, item.OrderDetType, item.OrderDetSuffix, item.OrderDetLineNo, item.SupOrderSupplierJDERefNo, item.SupOrderDetQuantity, item.SupOrderDetUM, item.SupOrderDetUnitCostBD, Math.Round(item.SupOrderDetUnitCostBD * item.SupOrderDetQuantity, 3), item.SupOrderDetUnitCost, (item.SupOrderDetUnitCost * item.SupOrderDetQuantity), item.SupOrderDetCurrencyCode, item.SupOrderDetFXRate, item.SupOrderDetDesc, (byte)item.OrderPrimary, item.SupOrderDetSelected, item.SupOrderDetSODID, item.SupOrderDetSODAltID, item.SupOrderDetOtherChgID, orderDetCreatedModifiedBy, orderDetCreatedModifiedName, orderDetProgramID, orderDetWorkstationID, ref retError);
                                if (retError != B2BConstants.DB_STATUS_OK) break;
                            }

                            if (retError == B2BConstants.DB_STATUS_OK) scope.Complete();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                retError = B2BConstants.DB_STATUS_ERROR;
                errorMsg = exception.Message;
            }
        }
        public void UpdateSelectedBid(bool? orderDetTenderComm, string orderCompany, double? orderNo, string orderType, string orderSuffix, int? orderRFQID, bool? orderRFQProcessed, string orderRFQNote, int? orderDetCreatedModifiedBy, string orderDetCreatedModifiedName, ref int? retError)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@orderTenderComm", orderDetTenderComm);
                param.Add("@orderCompany", orderCompany);
                param.Add("@orderNo", orderNo);
                param.Add("@orderType", orderType);
                param.Add("@orderSuffix", orderSuffix);
                param.Add("@orderRFQID", orderRFQID);
                param.Add("@orderRFQProcessed", orderRFQProcessed);
                param.Add("@orderRFQNote", orderRFQNote);
                param.Add("@orderModifiedBy", orderDetCreatedModifiedBy);
                param.Add("@orderModifiedName", orderDetCreatedModifiedName);
                param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("b2badminuser.pr_UpdateSelectedBid", param, commandType: CommandType.StoredProcedure);
                retError = param.Get<int>("@retError");
            }
        }
        public void UploadRFQNote(string mediaObjName, double? mediaObjOrderNo, byte? mediaObjType, decimal? mediaObjLineNo, decimal? mediaObjSeq, string mediaObjDocName, string mediaObjFolderName, string mediaObjFilename, byte[] mediaObjText, string mediaObjDocType, double? mediaObjModifiedBy, string mediaObjModifiedName, ref int? retError)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@mediaObjName", mediaObjName);
                param.Add("@mediaObjOrderNo", mediaObjOrderNo);
                param.Add("@mediaObjType", mediaObjType);
                param.Add("@mediaObjLineNo", mediaObjLineNo);
                param.Add("@mediaObjSeq", mediaObjSeq);
                param.Add("@mediaObjDocName", mediaObjDocName);
                param.Add("@mediaObjFolderName", mediaObjFolderName);
                param.Add("@mediaObjFilename", mediaObjFilename);
                param.Add("@mediaObjText", mediaObjText);
                param.Add("@mediaObjDocType", mediaObjDocType);
                param.Add("@mediaObjModifiedBy", mediaObjModifiedBy);
                param.Add("@mediaObjModifiedName", mediaObjModifiedName);
                param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("b2badminuser.pr_UploadRFQNote", param, commandType: CommandType.StoredProcedure);
                retError = param.Get<int>("@retError");
            }
        }
        public void UpdateSelectedBidDetail(bool? orderDetTenderComm, string orderDetCompany, double? orderDetNo, string orderDetType, string orderDetSuffix, double? orderDetLineNo, double? orderDetSupplierJDERefNo, double? orderDetQuantity, string orderDetUM, double? orderDetUnitCostBD, double? orderDetExtPrice, double? orderDetForeignUnitCost, double? orderDetForeignExtPrice, string orderDetCurrencyCode, double? orderDetFXRate, string orderDetDesc, byte? orderDetPrimary, bool? orderDetSelected, int? orderDetSODID, int? orderDetSODAltID, int? orderDetOtherChgID, int? orderDetCreatedModifiedBy, string orderDetCreatedModifiedName, string orderDetProgramID, string orderDetWorkstationID, ref int? retError)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@orderDetTenderComm", orderDetTenderComm);
                param.Add("@orderDetCompany", orderDetCompany);
                param.Add("@orderDetNo", orderDetNo);
                param.Add("@orderDetType", orderDetType);
                param.Add("@orderDetSuffix", orderDetSuffix);
                param.Add("@orderDetLineNo", orderDetLineNo);
                param.Add("@orderDetSupplierJDERefNo", orderDetSupplierJDERefNo);
                param.Add("@orderDetQuantity", orderDetQuantity);
                param.Add("@orderDetUM", orderDetUM);
                param.Add("@orderDetUnitCostBD", orderDetUnitCostBD);
                param.Add("@orderDetExtPrice", orderDetExtPrice);
                param.Add("@orderDetForeignUnitCost", orderDetForeignUnitCost);
                param.Add("@orderDetForeignExtPrice", orderDetForeignExtPrice);
                param.Add("@orderDetCurrencyCode", orderDetCurrencyCode);
                param.Add("@orderDetFXRate", orderDetFXRate);
                param.Add("@orderDetDesc", orderDetDesc);
                param.Add("@orderDetPrimary", orderDetPrimary);
                param.Add("@orderDetSelected", orderDetSelected);
                param.Add("@orderDetSODID", orderDetSODID);
                param.Add("@orderDetSODAltID", orderDetSODAltID);
                param.Add("@orderDetOtherChgID", orderDetOtherChgID);
                param.Add("@orderDetCreatedModifiedBy", orderDetCreatedModifiedBy);
                param.Add("@orderDetCreatedModifiedName", orderDetCreatedModifiedName);
                param.Add("@orderDetProgramID", orderDetProgramID);
                param.Add("@orderDetWorkstationID", orderDetWorkstationID);
                param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("b2badminuser.pr_UpdateSelectedBidDetail", param, commandType: CommandType.StoredProcedure);
                retError = param.Get<int>("@retError");
            }
        }
    }
}