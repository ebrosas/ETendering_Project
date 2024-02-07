using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class SupplierSelectionRepository
    {
        private readonly string connectionString;

        public SupplierSelectionRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public void InsertUpdateSupplierSelection(string orderCompany, double? orderNo, string orderType, string orderSuffix, double? orderLineNo, double? orderSupplierJDERefNo, string orderQuotePrintedFlag, DateTime? orderTransactionDate, string orderResponseFunc, DateTime? orderReqQuoteResDate, decimal? orderQuoteResDate, decimal? orderSchedPickDate, double? orderUnitReleased, double? orderAmountReleased, ref int? retError)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@orderCompany", orderCompany);
                param.Add("@orderNo", orderNo);
                param.Add("@orderType", orderType);
                param.Add("@orderSuffix", orderSuffix);
                param.Add("@orderLineNo", orderLineNo);
                param.Add("@orderSupplierJDERefNo", orderSupplierJDERefNo);
                param.Add("@orderQuotePrintedFlag", orderQuotePrintedFlag);
                param.Add("@orderTransactionDate", orderTransactionDate);
                param.Add("@orderResponseFunc", orderResponseFunc);
                param.Add("@orderReqQuoteResDate", orderReqQuoteResDate);
                param.Add("@orderQuoteResDate", orderQuoteResDate);
                param.Add("@orderSchedPickDate", orderSchedPickDate);
                param.Add("@orderUnitReleased", orderUnitReleased);
                param.Add("@orderAmoutReleased", orderAmountReleased);
                param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("b2badminuser.pr_InsertUpdateSupplierSelection", param, commandType: CommandType.StoredProcedure);
                retError = param.Get<int>("@retError");
            }
        }
    }
}
