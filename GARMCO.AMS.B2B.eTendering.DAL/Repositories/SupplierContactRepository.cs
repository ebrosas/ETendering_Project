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
    public class SupplierContactRepository
    {
        private readonly string connectionString;

        public SupplierContactRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<SupplierContact> GetSupplierContact(byte? mode, int? contactID, string contactEmail, string contactActiveKey, int? contactSupplierNo)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { mode, contactID, contactEmail, contactActiveKey, contactSupplierNo };
                return connection.Query<SupplierContact>("b2badminuser.pr_GetSupplierContact", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }
        public SupplierContact InsertUpdateDeleteSupplierContact(byte? mode, ref int? contactID, int? contactSupplierNo, string contactName, string contactEmail, string contactPassword, string contactTelNo, string contactMobNo, string contactFaxNo, string contactActiveKey, bool? contactPrimary, int? contactCreatedModifiedBy, string contactCreatedModifiedName, ref int? retError, ref string errorMsg)
        {
            SupplierContact supplierContact = null;
            retError = B2BConstants.DB_STATUS_OK;
            errorMsg = string.Empty;

            try
            {
                using (var scope = new TransactionScope())
                {
                    InsertUpdateDeleteSupplierContact(mode, ref contactID, contactSupplierNo, contactName, contactEmail, contactPassword, contactTelNo, contactMobNo, contactFaxNo, contactActiveKey, contactPrimary, contactCreatedModifiedBy, contactCreatedModifiedName, ref retError);
                    if (retError == B2BConstants.DB_STATUS_OK) scope.Complete();
                }

                if (mode == B2BConstants.DB_INSERT_RECORD && retError == B2BConstants.DB_STATUS_OK)
                {
                    var supplierContacts = this.GetSupplierContact(0, contactID, string.Empty, string.Empty, 0);
                    if (supplierContacts != null && supplierContacts.Count() > 0) supplierContact = supplierContacts.First();
                }
            }
            catch (Exception exception)
            {
                retError = B2BConstants.DB_STATUS_ERROR;
                errorMsg = exception.Message;
            }

            return supplierContact;
        }
        public void InsertUpdateDeleteSupplierContact(byte? mode, ref int? contactID, int? contactSupplierNo, string contactName, string contactEmail, string contactPassword, string contactTelNo, string contactMobNo, string contactFaxNo, string contactActiveKey, bool? contactPrimary, int? contactCreatedModifiedBy, string contactCreatedModifiedName, ref int? retError)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@mode", mode);
                param.Add("@contactID", value: contactID, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
                param.Add("@contactSupplierNo", contactSupplierNo);
                param.Add("@contactName", contactName);
                param.Add("@contactEmail", contactEmail);
                param.Add("@contactPassword", contactPassword);
                param.Add("@contactTelNo", contactTelNo);
                param.Add("@contactMobNo", contactMobNo);
                param.Add("@contactFaxNo", contactFaxNo);
                param.Add("@contactActiveKey", contactActiveKey);
                param.Add("@contactPrimary", contactPrimary);
                param.Add("@contactCreatedModifiedBy", contactCreatedModifiedBy);
                param.Add("@contactCreatedModifiedName", contactCreatedModifiedName);
                param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("b2badminuser.pr_InsertUpdateDeleteSupplierContact", param, commandType: CommandType.StoredProcedure);
                contactID = param.Get<int>("@contactID");
                retError = param.Get<int>("@retError");
            }
        }
    }
}
