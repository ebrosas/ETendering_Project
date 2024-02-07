using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class OrderRequisitionDetailRepository
    {
        private readonly string connectionString;

        public OrderRequisitionDetailRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<OrderRequisitionDetail> GetOrderRequisitionDetail(double? orderDetNo)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { orderDetNo };
                return connection.Query<OrderRequisitionDetail>("b2badminuser.pr_GetOrderRequisitionDetail", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }

        public void InsertUpdateDeleteOrderRequisitionDetail(byte? mode, string orderDetCompany, double? orderDetNo, string orderDetType, string orderDetSuffix, double? orderDetLineNo, string orderDetLineType, string orderDetItemCode, double? orderDetQuantity, string orderDetUM, double? orderDetUnitCost, double? orderDetExtPrice, double? orderDetForeignUnitCost, double? orderDetForeignExtPrice, string orderDetCurrencyCode, string orderDetDesc, string orderDetUNSPSC, string orderDetStockLongItemNo, double? orderDetStockShortItemNo, string orderDetStatus, int? orderDetCreatedModifiedBy, string orderDetCreatedModifiedName, ref int? retError)
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
                param.Add("@orderDetLineType", orderDetLineType);
                param.Add("@orderDetItemCode", orderDetItemCode);
                param.Add("@orderDetQuantity", orderDetQuantity);
                param.Add("@orderDetUM", orderDetUM);
                param.Add("@orderDetUnitCost", orderDetUnitCost);
                param.Add("@orderDetExtPrice", orderDetExtPrice);
                param.Add("@orderDetForeignUnitCost", orderDetForeignUnitCost);
                param.Add("@orderDetForeignExtPrice", orderDetForeignExtPrice);
                param.Add("@orderDetCurrencyCode", orderDetCurrencyCode);
                param.Add("@orderDetDesc", orderDetDesc);
                param.Add("@orderDetUNSPSC", orderDetUNSPSC);
                param.Add("@orderDetStockShortItemNo", orderDetStockShortItemNo);
                param.Add("@orderDetStockLongItemNo", orderDetStockLongItemNo);
                param.Add("@orderDetStatus", orderDetStatus);
                param.Add("@orderDetCreatedModifiedBy", orderDetCreatedModifiedBy);
                param.Add("@orderDetCreatedModifiedName", orderDetCreatedModifiedName);
                param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("b2badminuser.pr_InsertUpdateDeleteOrderRequisitionDetail", param, commandType: CommandType.StoredProcedure);
                retError = param.Get<int>("@retError");
            }
        }
    }
}
