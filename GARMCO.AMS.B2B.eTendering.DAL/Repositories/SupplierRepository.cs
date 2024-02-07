using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using Dapper;
using GARMCO.AMS.B2B.Utility;

namespace GARMCO.AMS.B2B.eTendering.DAL
{
    public class SupplierRepository
    {
        private readonly string connectionString;
        private readonly SupplierContactRepository supplierContactRepository;
        private readonly SupplierProductServiceRepository supplierProductServiceRepository;

        public SupplierRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
            this.supplierContactRepository = new SupplierContactRepository();
            this.supplierProductServiceRepository = new SupplierProductServiceRepository();
        }

        public IEnumerable<Supplier> GetSupplier(byte? mode, int? supplierNo, string supplierName, int? supplierContactID, string supplierContactName, string supplierContactEmail, string supplierCountry, byte? supplierStatus, int? startRowIndex, int? maximumRows, string sort)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                if (maximumRows == null || maximumRows == 0) maximumRows = 10;
                var param = new { mode, supplierNo, supplierName, supplierContactID, supplierContactName, supplierContactEmail, supplierCountry, supplierStatus, startRowIndex, maximumRows, sort };
                return connection.Query<Supplier>("b2badminuser.pr_GetSupplier", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }
        public int GetSupplierTotal(byte? mode, int? supplierNo, string supplierName, int? supplierContactID, string supplierContactName, string supplierContactEmail, string supplierCountry, byte? supplierStatus, string sort)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { supplierNo, supplierName, supplierContactID, supplierContactName, supplierContactEmail, supplierCountry, supplierStatus };
                return connection.Query<int>("b2badminuser.pr_GetSupplierTotal", param, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }
        public void ApproveRejectSupplierContact(int? supplierNo, double? supplierJDERefNo, int? supplierContactID, bool? supplierContactRejected, ref string supplierContactRejectReason, int? supplierCreatedModifiedBy, string supplierCreatedModifiedName, ref int? retError, ref string errorMsg)
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
                        param.Add("@supplierNo", supplierNo);
                        param.Add("@supplierJDERefNo", supplierJDERefNo);
                        param.Add("@supplierContactID", supplierContactID);
                        param.Add("@supplierContactRejected", supplierContactRejected);
                        param.Add("@supplierContactRejectReason", supplierContactRejectReason);
                        param.Add("@supplierCreatedModifiedBy", supplierCreatedModifiedBy);
                        param.Add("@supplierCreatedModifiedName", supplierCreatedModifiedName);
                        param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Execute("b2badminuser.pr_ApproveRejectSupplierContact", param, commandType: CommandType.StoredProcedure);
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
        public void UpdateSupplier(byte? mode, ref int? supplierNo, double? supplierJDERefNo, string supplierName, string supplierURL, bool? supplierOld, string supplierAddress, string supplierCity, string supplierState, string supplierCountry, string supplierPostalCode, string supplierCurrency, string supplierDelTerm, int? supplierShipVia, bool? supplierNews, bool? supplierIncProdServ, bool? supplierNotProdServ, List<SupplierProdServItem> list, int? supplierCreatedModifiedBy, string supplierCreatedModifiedName, ref int? retError, ref string errorMsg)
        {
            retError = B2BConstants.DB_STATUS_OK;
            errorMsg = string.Empty;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    this.InsertUpdateSupplier(mode, ref supplierNo, supplierJDERefNo, supplierName, supplierURL, supplierOld, supplierAddress, supplierCity, supplierState, supplierCountry, supplierPostalCode, supplierCurrency, supplierDelTerm, supplierShipVia, supplierNews, supplierIncProdServ, supplierNotProdServ, supplierCreatedModifiedBy, supplierCreatedModifiedName, ref retError);

                    if (retError == B2BConstants.DB_STATUS_OK)
                    {
                        foreach (SupplierProdServItem item in list)
                        {
                            using (var connection = new SqlConnection(this.connectionString))
                            {
                                var param = new DynamicParameters();
                                param.Add("@prodServSupplierNo", supplierNo);
                                param.Add("@prodServCode", item.ProdServCode);
                                param.Add("@prodServCheckState", (byte?)item.ProdServCheckState);
                                param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                                connection.Execute("b2badminuser.pr_InsertUpdateDeleteSupplierProductService", param, commandType: CommandType.StoredProcedure);
                                retError = param.Get<int>("@retError");
                            }
                            if (retError != B2BConstants.DB_STATUS_OK) break;
                        }

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
        public void InsertUpdateSupplier(byte? mode, ref int? supplierNo, double? supplierJDERefNo, string supplierName, string supplierURL, bool? supplierOld, string supplierAddress, string supplierCity, string supplierState, string supplierCountry, string supplierPostalCode, string supplierCurrency, string supplierDelTerm, int? supplierShipVia, bool? supplierNews, bool? supplierIncProdServ, bool? supplierNotProdServ, int? supplierCreatedModifiedBy, string supplierCreatedModifiedName, ref int? retError)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@mode", mode);
                param.Add("@supplierNo", value: supplierNo, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
                param.Add("@supplierJDERefNo", supplierJDERefNo);
                param.Add("@supplierName", supplierName);
                param.Add("@supplierURL", supplierURL);
                param.Add("@supplierOld", supplierOld);
                param.Add("@supplierAddress", supplierAddress);
                param.Add("@supplierCity", supplierCity);
                param.Add("@supplierState", supplierState);
                param.Add("@supplierCountry", supplierCountry);
                param.Add("@supplierPostalCode", supplierPostalCode);
                param.Add("@supplierCurrency", supplierCurrency);
                param.Add("@supplierDelTerm", supplierDelTerm);
                param.Add("@supplierShipVia", supplierShipVia);
                param.Add("@supplierNews", supplierNews);
                param.Add("@supplierIncProdServ", supplierIncProdServ);
                param.Add("@supplierNotProdServ", supplierNotProdServ);
                param.Add("@supplierDateRegistered", DateTime.Now);
                param.Add("@supplierDateActivated", null);
                param.Add("@supplierCreatedModifiedBy", supplierCreatedModifiedBy);
                param.Add("@supplierCreatedModifiedName", supplierCreatedModifiedName);
                param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("b2badminuser.pr_InsertUpdateDeleteSupplier", param, commandType: CommandType.StoredProcedure);
                supplierNo = param.Get<int>("@supplierNo");
                retError = param.Get<int>("@retError");
            }
        }
        public SupplierContact InsertSupplier(byte? mode, ref int? supplierNo, double? supplierJDERefNo, string supplierName, string supplierURL, bool? supplierOld, string supplierAddress, string supplierCity, string supplierState, string supplierCountry, string supplierPostalCode, string supplierCurrency, string supplierDelTerm, int? supplierShipVia, bool? supplierNews, bool? supplierIncProdServ, bool? supplierNotProdServ, string supplierProfileName, string supplierProfileFilename, string contactName, string contactEmail, string contactPassword, string contactTelNo, string contactMobNo, string contactFaxNo, string contactActiveKey, List<SupplierProdServItem> list, int? supplierCreatedModifiedBy, string supplierCreatedModifiedName, ref int? retError, ref string errorMsg)
        {
            SupplierContact row = null;
            int? contactID = 0;
            retError = B2BConstants.DB_STATUS_OK;
            errorMsg = String.Empty;

            try
            {
                using (var scope = new TransactionScope())
                {
                    this.InsertUpdateSupplier(mode, ref supplierNo, supplierJDERefNo, supplierName, supplierURL, supplierOld, supplierAddress, supplierCity, supplierState, supplierCountry, supplierPostalCode, supplierCurrency, supplierDelTerm, supplierShipVia, supplierNews, supplierIncProdServ, supplierNotProdServ, supplierCreatedModifiedBy, supplierCreatedModifiedName, ref retError);

                    if (retError == B2BConstants.DB_STATUS_OK)
                    {
                        this.supplierContactRepository.InsertUpdateDeleteSupplierContact(mode, ref contactID, supplierNo, contactName, contactEmail, contactPassword, contactTelNo, contactMobNo, contactFaxNo, contactActiveKey, true, supplierCreatedModifiedBy, supplierCreatedModifiedName, ref retError);

                        if (retError == B2BConstants.DB_STATUS_OK)
                        {
                            foreach (SupplierProdServItem item in list)
                            {
                                this.supplierProductServiceRepository.Update(supplierNo, item.ProdServCode, (byte?)item.ProdServCheckState, ref retError);
                                if (retError != B2BConstants.DB_STATUS_OK) break;
                            }

                            if (retError == B2BConstants.DB_STATUS_OK) scope.Complete();
                        }
                    }
                }

                if (mode == B2BConstants.DB_INSERT_RECORD && retError == B2BConstants.DB_STATUS_OK)
                {

                    var dataTable = this.supplierContactRepository.GetSupplierContact(0, contactID, String.Empty, String.Empty, 0);
                    if (dataTable != null && dataTable.Count() > 0) row = dataTable.First();
                }
            }

            catch (Exception error)
            {
                retError = B2BConstants.DB_STATUS_ERROR;
                errorMsg = error.Message;
            }

            return row;
        }
    }
}
