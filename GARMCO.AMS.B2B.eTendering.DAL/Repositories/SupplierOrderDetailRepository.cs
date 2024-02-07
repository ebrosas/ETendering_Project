using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using GARMCO.AMS.B2B.Utility;

namespace GARMCO.AMS.B2B.eTendering.DAL
{
    public class SupplierOrderDetailRepository
    {
        private readonly string connectionString;

        public SupplierOrderDetailRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public void UpdateSupplierOrderDetail(int? sodPrimary, int? sodID, double? sodFXRate, double? sodUnitCostBD, bool? sodSelected, int? sodCreatedModifiedBy, string sodCreatedModifiedName, ref int? retError)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@sodPrimary", sodPrimary);
                param.Add("@sodID", sodID);
                param.Add("@sodFXRate", sodFXRate);
                param.Add("@sodUnitCostBD", sodUnitCostBD);
                param.Add("@sodSelected", sodSelected);
                param.Add("@sodCreatedModifiedBy", sodCreatedModifiedBy);
                param.Add("@sodCreatedModifiedName", sodCreatedModifiedName);
                param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("b2badminuser.pr_UpdateSupplierOrderDetail", param, commandType: CommandType.StoredProcedure);
                retError = param.Get<int>("@retError");
            }
        }

        public void UpdateSupplierOrderDetail(List<SupplierOrderDetailItem> supOrderList, List<SupplierOrderOtherChargeItem> supOtherChgList, int? sodCreatedModifiedBy, string sodCreatedModifiedName, ref int? retError)
        {
            retError = B2BConstants.DB_STATUS_OK;

            foreach (SupplierOrderDetailItem item in supOrderList)
            {

                this.UpdateSupplierOrderDetail(1, item.SODID, item.SODFXRate, item.SODUnitCostBD, item.SODSelected, sodCreatedModifiedBy, sodCreatedModifiedName, ref retError);

                if (retError == B2BConstants.DB_STATUS_OK)
                {
                    foreach (SupplierOrderDetailAltItem altItem in item.SupplierOrderDetAltList)
                    {

                        this.UpdateSupplierOrderDetail(2, altItem.SODAltID, altItem.SODAltFXRate, altItem.SODAltUnitCostBD, altItem.SODAltSelected, sodCreatedModifiedBy, sodCreatedModifiedName, ref retError);
                        if (retError != B2BConstants.DB_STATUS_OK) break;
                    }

                    if (retError != B2BConstants.DB_STATUS_OK) break;

                }
                else break;

            }

            if (retError == B2BConstants.DB_STATUS_OK)
            {
                foreach (SupplierOrderOtherChargeItem otherItem in supOtherChgList)
                {
                    this.UpdateSupplierOrderDetail(3, otherItem.OtherChgID, otherItem.OtherChgFXRate, otherItem.OtherChgAmountBD, otherItem.OtherChgSelected, sodCreatedModifiedBy, sodCreatedModifiedName, ref retError);
                    if (retError != B2BConstants.DB_STATUS_OK) break;
                }
            }
        }

        public IEnumerable<SupplierOrderDetail> GetSupplierOrderDetail(byte? mode, double? orderNo, int? supplierNo)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { mode, orderNo, supplierNo };
                return connection.Query<SupplierOrderDetail>("b2badminuser.pr_GetSupplierOrderDetail", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }

        public List<SupplierOrderDetailItem> GetSupplierOrderDetailList(byte? mode, double? orderNo, int? supplierNo)
        {
            var list = new List<SupplierOrderDetailItem>();
            var supplierOrderDetails = this.GetSupplierOrderDetail(mode, orderNo, supplierNo);
            if (supplierOrderDetails != null) foreach (var supplierOrderDetail in supplierOrderDetails) list.Add(new SupplierOrderDetailItem(supplierOrderDetail));
            return list;
        }
    }
}
