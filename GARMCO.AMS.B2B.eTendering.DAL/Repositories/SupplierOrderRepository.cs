using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;
using Dapper;
using GARMCO.AMS.B2B.Utility;

namespace GARMCO.AMS.B2B.eTendering.DAL
{
    public class SupplierOrderRepository
    {
        private readonly string connectionString;

        public SupplierOrderRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<SupplierOrder> GetSupplierOrder(double? orderNo, int? supplierNo)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { orderNo, supplierNo };
                return connection.Query<SupplierOrder>("b2badminuser.pr_GetSupplierOrder", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }
        public void InsertUpdateDeleteSupplierOrder(byte? mode, ref int? supOrderID, double? supOrderOrderNo, int? supOrderSupplierNo, string supOrderCurrencyCode, double? supOrderFXRate, DateTime? supOrderValidityPeriod, int? supOrderDeliveryTime, string supOrderDeliveryTerm, int? supOrderShipVia, string supOrderDeliveryPt, string supOrderPayTerm, string supOrderStatus, List<SupplierOrderDetailItem> supOrderDetList, List<SupplierOrderOtherChargeItem> supOrderOtherChgList, int? supOrderCreatedModifiedBy, string supOrderCreatedModifiedName, ref int? retError, ref string errorMsg)
        {
            retError = B2BConstants.DB_STATUS_OK;
            errorMsg = string.Empty;

            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var connection = new SqlConnection(this.connectionString))
                    {
                        var param = new DynamicParameters();
                        param.Add("@mode", mode);
                        param.Add("@supOrderID", value: supOrderID, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
                        param.Add("@supOrderOrderNo", supOrderOrderNo);
                        param.Add("@supOrderSupplierNo", supOrderSupplierNo);
                        param.Add("@supOrderCurrencyCode", supOrderCurrencyCode);
                        param.Add("@supOrderFXRate", supOrderFXRate);
                        param.Add("@supOrderValidityPeriod", supOrderValidityPeriod);
                        param.Add("@supOrderDeliveryTime", supOrderDeliveryTime);
                        param.Add("@supOrderDeliveryTerm", supOrderDeliveryTerm);
                        param.Add("@supOrderShipVia", supOrderShipVia);
                        param.Add("@supOrderDeliveryPt", supOrderDeliveryPt);
                        param.Add("@supOrderPayTerm", supOrderPayTerm);
                        param.Add("@supOrderStatus", supOrderStatus);
                        param.Add("@supOrderCreatedModifiedBy", supOrderCreatedModifiedBy);
                        param.Add("@supOrderCreatedModifiedName", supOrderCreatedModifiedName);
                        param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Execute("b2badminuser.pr_InsertUpdateDeleteSupplierOrder", param, commandType: CommandType.StoredProcedure);
                        retError = param.Get<int>("@retError");
                    }

                    if (retError == B2BConstants.DB_STATUS_OK)
                    {
                        if (mode == 3 || mode == 4) scope.Complete();
                        else
                        {
                            foreach (var item in supOrderDetList)
                            {
                                item.SODSupOrderID = supOrderID;

                                if ((item.SODUnitCost > 0 || item.TotalSupplierOrderDetAlternatives > 0) && item.SODID == 0) item.Added = true;
                                else if (item.SODUnitCost == 0 && item.TotalSupplierOrderDetAlternatives == 0) item.MarkForDeletion = true;
                                else if ((item.SODUnitCost > 0 || item.TotalSupplierOrderDetAlternatives > 0) && item.SODID > 0) item.Modified = true;

                                this.InsertUpdateDeleteSupplierOrderDetail(item, supOrderCreatedModifiedBy, supOrderCreatedModifiedName, ref retError);
                                if (retError != B2BConstants.DB_STATUS_OK) break;
                            }

                            if (retError == B2BConstants.DB_STATUS_OK)
                            {
                                foreach (SupplierOrderOtherChargeItem item in supOrderOtherChgList)
                                {
                                    item.OtherChgSupOrderID = supOrderID;
                                    this.InsertUpdateDeleteSupplierOrderOtherCharge(item, supOrderCreatedModifiedBy, supOrderCreatedModifiedName, ref retError);
                                    if (retError != B2BConstants.DB_STATUS_OK) break;
                                }
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
        public void InsertUpdateDeleteSupplierOrderDetail(SupplierOrderDetailItem item, int? sodCreatedModifiedBy, string sodCreatedModifiedName, ref int? retError)
        {
            retError = B2BConstants.DB_STATUS_OK;
            var sodID = item.SODID;

            if (item.MarkForDeletion) this.InsertUpdateDeleteSupplierOrderDetail(B2BConstants.DB_DELETE_RECORD, ref sodID, item.SODSupOrderID, item.OrderDetNo, item.OrderDetLineNo, item.SODSupplierCode, item.SODManufacturerCode, item.OrderDetQuantity, item.OrderDetUM, item.SODUnitCost, item.SODExtPrice, item.SODUnitCostBD, item.SODRemarks, sodCreatedModifiedBy, sodCreatedModifiedName, ref retError);
            else if (item.Added) this.InsertUpdateDeleteSupplierOrderDetail(B2BConstants.DB_INSERT_RECORD, ref sodID, item.SODSupOrderID, item.OrderDetNo, item.OrderDetLineNo, item.SODSupplierCode, item.SODManufacturerCode, item.OrderDetQuantity, item.OrderDetUM, item.SODUnitCost, item.SODExtPrice, item.SODUnitCostBD, item.SODRemarks, sodCreatedModifiedBy, sodCreatedModifiedName, ref retError);
            else if (item.Modified) this.InsertUpdateDeleteSupplierOrderDetail(B2BConstants.DB_UPDATE_RECORD, ref sodID, item.SODSupOrderID, item.OrderDetNo, item.OrderDetLineNo, item.SODSupplierCode, item.SODManufacturerCode, item.OrderDetQuantity, item.OrderDetUM, item.SODUnitCost, item.SODExtPrice, item.SODUnitCostBD, item.SODRemarks, sodCreatedModifiedBy, sodCreatedModifiedName, ref retError);

            if (retError == B2BConstants.DB_STATUS_OK)
            {
                foreach (var attachItem in item.SupplierOrderDetAttachList)
                {
                    attachItem.OrderAttachSODID = sodID;
                    attachItem.OrderAttachSODAltID = -1;
                    attachItem.OrderAttachLineNo = item.OrderDetLineNo;

                    if (item.MarkForDeletion) attachItem.MarkForDeletion = item.MarkForDeletion;
                    else if (item.Added) attachItem.Added = item.Added;

                    this.InsertUpdateDeleteSupplierOrderAttachment(attachItem, sodCreatedModifiedBy, sodCreatedModifiedName, ref retError);
                    if (retError != B2BConstants.DB_STATUS_OK) break;
                }
            }

            if (retError == B2BConstants.DB_STATUS_OK)
            {
                foreach (var altItem in item.SupplierOrderDetAltList)
                {
                    altItem.SODAltSODID = sodID;

                    if (item.MarkForDeletion) altItem.MarkForDeletion = item.MarkForDeletion;
                    else if (item.Added) altItem.Added = item.Added;

                    this.InsertUpdateDeleteSupplierOrderAlternative(altItem, sodCreatedModifiedBy, sodCreatedModifiedName, ref retError);
                    if (retError != B2BConstants.DB_STATUS_OK) break;
                }
            }
        }
        public void InsertUpdateDeleteSupplierOrderDetail(byte? mode, ref int? sodID, int? sodSupOrderID, double? sodOrderNo, double? sodOrderLineNo, string sodSupplierCode, string sodManufacturerCode, double? sodQuantity, string sodUM, double? sodUnitCost, double? sodExtPrice, double? sodUnitCostBD, string sodRemarks, int? sodCreatedModifiedBy, string sodCreatedModifiedName, ref int? retError)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@mode", mode);
                param.Add("@sodID", value: sodID, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
                param.Add("@sodSupOrderID", sodSupOrderID);
                param.Add("@sodOrderNo", sodOrderNo);
                param.Add("@sodOrderLineNo", sodOrderLineNo);
                param.Add("@sodSupplierCode", sodSupplierCode);
                param.Add("@sodManufacturerCode", sodManufacturerCode);
                param.Add("@sodQuantity", sodQuantity);
                param.Add("@sodUM", sodUM);
                param.Add("@sodUnitCost", sodUnitCost);
                param.Add("@sodExtPrice", sodExtPrice);
                param.Add("@sodUnitCostBD", sodUnitCostBD);
                param.Add("@sodRemarks", sodRemarks);
                param.Add("@sodCreatedModifiedBy", sodCreatedModifiedBy);
                param.Add("@sodCreatedModifiedName", sodCreatedModifiedName);
                param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("b2badminuser.pr_InsertUpdateDeleteSupplierOrderDetail", param, commandType: CommandType.StoredProcedure);
                retError = param.Get<int>("@retError");
            }
        }
        public void InsertUpdateDeleteSupplierOrderAlternative(SupplierOrderDetailAltItem item, int? sodAltCreatedModifiedBy, string sodAltCreatedModifiedName, ref int? retError)
        {
            retError = B2BConstants.DB_STATUS_OK;
            var sodAltID = item.SODAltID;

            if (item.MarkForDeletion) this.InsertUpdateDeleteSupplierOrderAlternative(B2BConstants.DB_DELETE_RECORD, ref sodAltID, item.SODAltSODID, item.SODAltLineNo, item.SODAltDesc, item.SODAltSupplierCode, item.SODAltManufacturerCode, item.SODAltQuantity, item.SODAltUM, item.SODAltUnitCost, item.SODAltExtPrice, item.SODAltUnitCostBD, item.SODAltCurrencyCode, item.SODAltFXRate, item.SODAltValidityPeriod, item.SODAltDeliveryTime, item.SODAltDelTerm, item.SODAltShipVia, item.SODAltDeliveryPt, item.SODAltSelected, sodAltCreatedModifiedBy, sodAltCreatedModifiedName, ref retError);
            else if (item.Added) this.InsertUpdateDeleteSupplierOrderAlternative(B2BConstants.DB_INSERT_RECORD, ref sodAltID, item.SODAltSODID, item.SODAltLineNo, item.SODAltDesc, item.SODAltSupplierCode, item.SODAltManufacturerCode, item.SODAltQuantity, item.SODAltUM, item.SODAltUnitCost, item.SODAltExtPrice, item.SODAltUnitCostBD, item.SODAltCurrencyCode, item.SODAltFXRate, item.SODAltValidityPeriod, item.SODAltDeliveryTime, item.SODAltDelTerm, item.SODAltShipVia, item.SODAltDeliveryPt, item.SODAltSelected, sodAltCreatedModifiedBy, sodAltCreatedModifiedName, ref retError);
            else if (item.Modified) this.InsertUpdateDeleteSupplierOrderAlternative(B2BConstants.DB_UPDATE_RECORD, ref sodAltID, item.SODAltSODID, item.SODAltLineNo, item.SODAltDesc, item.SODAltSupplierCode, item.SODAltManufacturerCode, item.SODAltQuantity, item.SODAltUM, item.SODAltUnitCost, item.SODAltExtPrice, item.SODAltUnitCostBD, item.SODAltCurrencyCode, item.SODAltFXRate, item.SODAltValidityPeriod, item.SODAltDeliveryTime, item.SODAltDelTerm, item.SODAltShipVia, item.SODAltDeliveryPt, item.SODAltSelected, sodAltCreatedModifiedBy, sodAltCreatedModifiedName, ref retError);

            if (retError == B2BConstants.DB_STATUS_OK && (item.Added || item.Modified))
            {
                foreach (var attachItem in item.SupplierOrderAlternativeAttachList)
                {
                    attachItem.OrderAttachSODID = -1;
                    attachItem.OrderAttachSODAltID = sodAltID;
                    attachItem.OrderAttachLineNo = item.SODAltLineNo;

                    if (item.MarkForDeletion) attachItem.MarkForDeletion = item.MarkForDeletion;
                    else if (item.Added) attachItem.Added = item.Added;

                    this.InsertUpdateDeleteSupplierOrderAttachment(attachItem, sodAltCreatedModifiedBy, sodAltCreatedModifiedName, ref retError);
                    if (retError != B2BConstants.DB_STATUS_OK) break;
                }
            }
        }
        public void InsertUpdateDeleteSupplierOrderAlternative(byte? mode, ref int? sodAltID, int? sodAltSODID, double? sodAltLineNo, string sodAltDesc, string sodAltSupplierCode, string sodAltManufacturerCode, double? sodAltQuantity, string sodAltUM, double? sodAltUnitCost, double? sodAltExtPrice, double? sodAltUnitCostBD, string sodAltCurrencyCode, double? sodAltFXRate, DateTime? sodAltValidityPeriod, int? sodAltDeliveryTime, string sodAltDelTerm, int? sodAltShipVia, string sodAltDeliveryPt, bool? sodAltSelected, int? sodAltCreatedModifiedBy, string sodAltCreatedModifiedName, ref int? retError)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@mode", mode);
                param.Add("@sodAltID", value: sodAltID, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
                param.Add("@sodAltSODID", sodAltSODID);
                param.Add("@sodAltLineNo", sodAltLineNo);
                param.Add("@sodAltDesc", sodAltDesc);
                param.Add("@sodAltSupplierCode", sodAltSupplierCode);
                param.Add("@sodAltManufacturerCode", sodAltManufacturerCode);
                param.Add("@sodAltQuantity", sodAltQuantity);
                param.Add("@sodAltUM", sodAltUM);
                param.Add("@sodAltUnitCost", sodAltUnitCost);
                param.Add("@sodAltExtPrice", sodAltExtPrice);
                param.Add("@sodAltUnitCostBD", sodAltUnitCostBD);
                param.Add("@sodAltCurrencyCode", sodAltCurrencyCode);
                param.Add("@sodAltFXRate", sodAltFXRate);
                param.Add("@sodAltValidityPeriod", sodAltValidityPeriod);
                param.Add("@sodAltDeliveryTime", sodAltDeliveryTime);
                param.Add("@sodAltDelTerm", sodAltDelTerm);
                param.Add("@sodAltShipVia", sodAltShipVia);
                param.Add("@sodAltDeliveryPt", sodAltDeliveryPt);
                param.Add("@sodAltSelected", sodAltSelected);
                param.Add("@sodAltCreatedModifiedBy", sodAltCreatedModifiedBy);
                param.Add("@sodAltCreatedModifiedName", sodAltCreatedModifiedName);
                param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("b2badminuser.pr_InsertUpdateDeleteSupplierOrderAlternative", param, commandType: CommandType.StoredProcedure);
                retError = param.Get<int>("@retError");
            }
        }
        public void InsertUpdateDeleteSupplierOrderAttachment(SupplierOrderAttachItem item, int? orderAttachCreatedModifiedBy, string orderAttachCreatedModifiedName, ref int? retError)
        {
            retError = B2BConstants.DB_STATUS_OK;
            byte[] orderAttachContent = null;

            if (item.MarkForDeletion) this.InsertUpdateDeleteSupplierOrderAttachment(B2BConstants.DB_DELETE_RECORD, item.OrderAttachNo, item.OrderAttachLineNo, item.OrderAttachSeq, item.OrderAttachSupplier, item.OrderAttachSODID, item.OrderAttachSODAltID, (decimal)item.OrderAttachType, item.OrderAttachDisplayName, item.OrderAttachFilename, orderAttachContent, orderAttachCreatedModifiedBy, orderAttachCreatedModifiedName, ref retError);
            else if (item.Added)
            {
                if (item.IsMediaObjectConvertHtmlOK) item.OrderAttachContentRtf = item.GetMediaObjectTextRtf(ConfigurationManager.AppSettings["HTML_RTF_KEY"]);
                orderAttachContent = Encoding.Unicode.GetBytes(item.OrderAttachContentRtf);
                this.InsertUpdateDeleteSupplierOrderAttachment(B2BConstants.DB_INSERT_RECORD, item.OrderAttachNo, item.OrderAttachLineNo, item.OrderAttachSeq, item.OrderAttachSupplier, item.OrderAttachSODID, item.OrderAttachSODAltID, (decimal)item.OrderAttachType, item.OrderAttachDisplayName, item.OrderAttachFilename, orderAttachContent, orderAttachCreatedModifiedBy, orderAttachCreatedModifiedName, ref retError);
            }

            else if (item.Modified)
            {
                if (item.IsMediaObjectConvertHtmlOK) item.OrderAttachContentRtf = item.GetMediaObjectTextRtf(ConfigurationManager.AppSettings["HTML_RTF_KEY"]);
                orderAttachContent = Encoding.Unicode.GetBytes(item.OrderAttachContentRtf);
                this.InsertUpdateDeleteSupplierOrderAttachment(B2BConstants.DB_UPDATE_RECORD, item.OrderAttachNo, item.OrderAttachLineNo, item.OrderAttachSeq, item.OrderAttachSupplier, item.OrderAttachSODID, item.OrderAttachSODAltID, (decimal)item.OrderAttachType, item.OrderAttachDisplayName, item.OrderAttachFilename, orderAttachContent, orderAttachCreatedModifiedBy, orderAttachCreatedModifiedName, ref retError);
            }
        }
        public void InsertUpdateDeleteSupplierOrderAttachment(byte? mode, double? orderAttachNo, double? orderAttachLineNo, decimal? orderAttachSeq, bool? orderAttachSupplier, int? orderAttachSODID, int? orderAttachSODAltID, decimal? orderAttachType, string orderAttachDisplayName, string orderAttachFilename, byte[] orderAttachContent, int? orderAttachCreatedModifiedBy, string orderAttachCreatedModifiedName, ref int? retError)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
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

                connection.Execute("b2badminuser.pr_InsertUpdateDeleteOrderRequisitionDetailAttachment", param, commandType: CommandType.StoredProcedure);
                retError = param.Get<int>("@retError");
            }
        }
        public void InsertUpdateDeleteSupplierOrderOtherCharge(SupplierOrderOtherChargeItem item, int? otherChgCreatedModifiedBy, string otherChgCreatedModifiedName, ref int? retError)
        {
            retError = B2BConstants.DB_STATUS_OK;

            if (item.MarkForDeletion) this.InsertUpdateDeleteSupplierOrderOtherCharge(B2BConstants.DB_DELETE_RECORD, item.OtherChgID, item.OtherChgSupOrderID, item.OtherChgDesc, item.OtherChgCurrencyCode, item.OtherChgAmount, item.OtherChgAmountBD, item.OtherChgSelected, otherChgCreatedModifiedBy, otherChgCreatedModifiedName, ref retError);
            else if (item.Added) this.InsertUpdateDeleteSupplierOrderOtherCharge(B2BConstants.DB_INSERT_RECORD, item.OtherChgID, item.OtherChgSupOrderID, item.OtherChgDesc, item.OtherChgCurrencyCode, item.OtherChgAmount, item.OtherChgAmountBD, item.OtherChgSelected, otherChgCreatedModifiedBy, otherChgCreatedModifiedName, ref retError);
            else if (item.Modified) this.InsertUpdateDeleteSupplierOrderOtherCharge(B2BConstants.DB_UPDATE_RECORD, item.OtherChgID, item.OtherChgSupOrderID, item.OtherChgDesc, item.OtherChgCurrencyCode, item.OtherChgAmount, item.OtherChgAmountBD, item.OtherChgSelected, otherChgCreatedModifiedBy, otherChgCreatedModifiedName, ref retError);
        }
        public void InsertUpdateDeleteSupplierOrderOtherCharge(byte? mode, int? otherChgID, int? otherChgSupOrderID, string otherChgDesc, string otherChgCurrencyCode, double? otherChgAmount, double? otherChgAmountBD, bool? otherChgSelected, int? otherChgCreatedModifiedBy, string otherChgCreatedModifiedName, ref int? retError)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@mode", mode);
                param.Add("@otherChgID", otherChgID);
                param.Add("@otherChgSupOrderID", otherChgSupOrderID);
                param.Add("@otherChgDesc", otherChgDesc);
                param.Add("@otherChgCurrencyCode", otherChgCurrencyCode);
                param.Add("@otherChgAmount", otherChgAmount);
                param.Add("@otherChgAmountBD", otherChgAmountBD);
                param.Add("@otherChgSelected", otherChgSelected);
                param.Add("@otherChgCreatedModifiedBy", otherChgCreatedModifiedBy);
                param.Add("@otherChgCreatedModifiedName", otherChgCreatedModifiedName);
                param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("b2badminuser.pr_InsertUpdateDeleteSupplierOrderOtherCharge", param, commandType: CommandType.StoredProcedure);
                retError = param.Get<int>("@retError");
            }
        }
    }
}
