using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GARMCO.AMS.B2B.Admin.DAL.Entities;

namespace GARMCO.AMS.B2B.Admin.DAL.Helpers
{
    public class ADONetDataService
    {
        #region Properties
        public static string DBConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ToString();
            }
        }
        #endregion

        #region ADO.NET Methods
        private static DataSet RunSPReturnDataset(string spName, string connectionString, params ADONetParameter[] parameters)
        {
            try
            {
                SqlConnection connection = new SqlConnection()
                {
                    ConnectionString = connectionString
                };

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spName;
                    command.CommandTimeout = 300;
                    command.Connection = connection;

                    CompileParameters(command, parameters);
                    //AddSQLCommand(command);

                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = command;
                        adapter.SelectCommand.CommandTimeout = 300;
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        private static int RunSPReturnInt32(string spname, string connectionString, string outputname, params ADONetParameter[] parameters)
        {
            try
            {
                SqlConnection connection = new SqlConnection()
                {
                    ConnectionString = connectionString
                };

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spname;
                    command.CommandTimeout = 300;
                    command.Connection = connection;

                    CompileParameters(command, parameters);
                    SqlParameter retval = AddParameter(command, outputname, SqlDbType.Int, ParameterDirection.Output);

                    command.Connection.Open();
                    command.ExecuteScalar();
                    command.Connection.Close();

                    // Return the output parameter
                    return Convert.ToInt32(retval.Value);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        private static void CompileParameters(SqlCommand comm, ADONetParameter[] parameters)
        {
            try
            {
                foreach (ADONetParameter parameter in parameters)
                {
                    if (parameter.ParameterValue == null)
                        parameter.ParameterValue = DBNull.Value;

                    comm.Parameters.Add(parameter.Parameter);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void AddSQLCommand(SqlCommand command, SqlConnection connection)
        {
            command.Connection = connection;
            //command.Transaction = this.transaction;
        }

        private static SqlParameter AddParameter(SqlCommand command, string parameterName, SqlDbType dbType, ParameterDirection direction)
        {
            SqlParameter parameter = new SqlParameter(parameterName, dbType);
            parameter.Direction = direction;
            command.Parameters.Add(parameter);
            return parameter;
        }
        #endregion

        #region Public Methods
        public static List<PublishedRFQItem> GetPublishedRFQ(double rfqNo, ref string error, ref string innerError)
        {
            List<PublishedRFQItem> result = null;

            try
            {
                ADONetParameter[] parameters = new ADONetParameter[1];
                parameters[0] = new ADONetParameter("@RFQNo", SqlDbType.Float, rfqNo);

                DataSet ds = RunSPReturnDataset("b2badminuser.pr_GetAlreadyPublishedRFQ", DBConnectionString, parameters);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = new List<PublishedRFQItem>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        PublishedRFQItem rfqDetail = new PublishedRFQItem()
                        {
                            OrderCompany = DALHelper.ConvertObjectToString(row["OrderCompany"]),
                            OrderNo = DALHelper.ConvertObjectToDouble(row["OrderNo"]),
                            OrderType = DALHelper.ConvertObjectToString(row["OrderType"]),
                            OrderSuffix = DALHelper.ConvertObjectToString(row["OrderSuffix"]),
                            OrderEmpNo = DALHelper.ConvertObjectToDouble(row["OrderEmpNo"]),
                            OrderEmpName = DALHelper.ConvertObjectToString(row["OrderEmpName"]),
                            OrderBuyerEmpNo = DALHelper.ConvertObjectToDouble(row["OrderBuyerEmpNo"]),
                            OrderBuyerEmpName = DALHelper.ConvertObjectToString(row["OrderBuyerEmpName"]),
                            OrderBuyerEmpEmail = DALHelper.ConvertObjectToString(row["OrderBuyerEmpEmail"]),
                            OrderTransactionDate = DALHelper.ConvertObjectToDate(row["OrderTransactionDate"]),
                            OrderClosingDate = DALHelper.ConvertObjectToDate(row["OrderClosingDate"]),
                            OrderPRNo = DALHelper.ConvertObjectToString(row["OrderPRNo"]),
                            OrderCategory = DALHelper.ConvertObjectToString(row["OrderCategory"]),
                            OrderPriority = DALHelper.ConvertObjectToString(row["OrderPriority"])
                        };

                        // Add to collection
                        result.Add(rfqDetail);
                    }
                }

                return result;
            }
            catch (SqlException sqlErr)
            {
                throw new Exception(sqlErr.Message.ToString());
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                if (ex.InnerException != null)
                    innerError = ex.InnerException.Message.ToString();
                return null;
            }
        }

        public static List<PublishedRFQInvitedSupplierEntity> GetInvitedSuppliers(double orderNo, int modifiedByEmpNo, 
            string modifedByUserID, ref int? retError, ref string error, ref string innerError)
        {
            List<PublishedRFQInvitedSupplierEntity> result = null;

            try
            {
                ADONetParameter[] parameters = new ADONetParameter[4];
                parameters[0] = new ADONetParameter("@orderNo", SqlDbType.Float, orderNo);
                parameters[1] = new ADONetParameter("@orderModifiedBy", SqlDbType.Int, modifiedByEmpNo);
                parameters[2] = new ADONetParameter("@orderModifiedName", SqlDbType.VarChar, 50, modifedByUserID);
                parameters[3] = new ADONetParameter("@retError", SqlDbType.Int, retError);

                DataSet ds = RunSPReturnDataset("b2badminuser.pr_SendRFQReminder", DBConnectionString, parameters);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = new List<PublishedRFQInvitedSupplierEntity>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        PublishedRFQInvitedSupplierEntity rawData = new PublishedRFQInvitedSupplierEntity()
                        {
                            SupInvJDERefNo = DALHelper.ConvertObjectToDouble(row["SupInvJDERefNo"]),
                            SupInvSupplierName = DALHelper.ConvertObjectToString(row["SupInvSupplierName"]),
                            SupInvSupplierNo = DALHelper.ConvertObjectToInt(row["SupInvSupplierNo"]),
                            SupInvSupplierAddress = DALHelper.ConvertObjectToString(row["SupInvSupplierAddress"]),
                            SupInvSupplierCity = DALHelper.ConvertObjectToString(row["SupInvSupplierCity"]),
                            SupInvSupplierState = DALHelper.ConvertObjectToString(row["SupInvSupplierState"]),
                            SupInvSupplierCountry = DALHelper.ConvertObjectToString(row["SupInvSupplierCountry"]),
                            SupInvSupplierPostalCode = DALHelper.ConvertObjectToString(row["SupInvSupplierPostalCode"]),
                            SupInvRegistered = DALHelper.ConvertObjectToBolean(row["SupInvRegistered"])
                        };

                        // Add to collection
                        result.Add(rawData);
                    }
                }

                return result;
            }
            catch (SqlException sqlErr)
            {
                throw new Exception(sqlErr.Message.ToString());
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                if (ex.InnerException != null)
                    innerError = ex.InnerException.Message.ToString();
                return null;
            }
        }

        public static void InsertOrderRequisitionHistory(double orderNo, int modifiedByEmpNo,
            string modifiedByUserID, double supplierJDERefNo, ref int OHID, ref string error, ref string innerError)
        {
            try
            {
                ADONetParameter[] parameters = new ADONetParameter[4];
                parameters[0] = new ADONetParameter("@orderNo", SqlDbType.Float, orderNo);
                parameters[1] = new ADONetParameter("@orderModifiedBy", SqlDbType.Int, modifiedByEmpNo);
                parameters[2] = new ADONetParameter("@orderModifiedName", SqlDbType.VarChar, 50, modifiedByUserID);
                parameters[3] = new ADONetParameter("@supplierJDERefNo", SqlDbType.Float, supplierJDERefNo);

                OHID = RunSPReturnInt32("b2badminuser.Pr_Insert_OrderRequisitionHistory", DBConnectionString, "OHID", parameters);
            }
            catch (SqlException sqlErr)
            {
                throw new Exception(sqlErr.Message.ToString());
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                if (ex.InnerException != null)
                    innerError = ex.InnerException.Message.ToString();
            }
        }

        public static List<SupplierEntity> GetSupplierRFQBids(double supplierNo, double rfqNo, ref string error, ref string innerError)
        {
            List<SupplierEntity> result = null;

            try
            {
                ADONetParameter[] parameters = new ADONetParameter[2];
                parameters[0] = new ADONetParameter("@SupplierNo", SqlDbType.Float, supplierNo);
                parameters[1] = new ADONetParameter("@RFQNo", SqlDbType.Float, rfqNo);

                DataSet ds = RunSPReturnDataset("b2badminuser.Pr_CheckSupplierBid", DBConnectionString, parameters);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = new List<SupplierEntity>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        SupplierEntity rawData = new SupplierEntity()
                        {
                            SupplierNo = DALHelper.ConvertObjectToInt(row["SupplierNo"]),
                            SupplierJDERefNo = DALHelper.ConvertObjectToDouble(row["SupplierJDERefNo"]),
                            SupplierName = DALHelper.ConvertObjectToString(row["SupplierName"]),
                            SupplierURL = DALHelper.ConvertObjectToString(row["SupplierURL"]),
                            SupplierAddress = DALHelper.ConvertObjectToString(row["SupplierAddress"]),
                            SupOrderPaymentTerm = DALHelper.ConvertObjectToString(row["SupOrderPaymentTerm"]),
                            SupOrderCreatedName = DALHelper.ConvertObjectToString(row["SupOrderCreatedName"]),
                            SupOrderCreatedDate = DALHelper.ConvertObjectToDate(row["SupOrderCreatedDate"]),
                            RFQNo = DALHelper.ConvertObjectToDouble(row["SupOrderOrderNo"]),
                            OrderCompany = DALHelper.ConvertObjectToString(row["OrderCompany"]),
                            OrderType = DALHelper.ConvertObjectToString(row["OrderType"]),
                            OrderSuffix = DALHelper.ConvertObjectToString(row["OrderSuffix"]),
                            OrderPRNo = DALHelper.ConvertObjectToString(row["OrderPRNo"]),
                            OrderBuyerEmpNo = DALHelper.ConvertObjectToInt(row["OrderBuyerEmpNo"]),
                            OrderBuyerEmpName = DALHelper.ConvertObjectToString(row["OrderBuyerEmpName"])
                        };

                        // Add to collection
                        result.Add(rawData);
                    }
                }

                return result;
            }
            catch (SqlException sqlErr)
            {
                throw new Exception(sqlErr.Message.ToString());
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                if (ex.InnerException != null)
                    innerError = ex.InnerException.Message.ToString();
                return null;
            }
        }

        // <summary>
        /// Gets the media object file from the database table
        /// </summary>
        /// <param name="fileName">Pass the unique filename</param>
        /// <returns>Returns file data as a byte array or null</returns>
        public static Byte[] GetMediaObjectFileFromDatabase(string fileName)
        {
            ADONetParameter[] parameters = null;
            DataSet ds = null;

            try
            {
                parameters = new ADONetParameter[1];
                parameters[0] = new ADONetParameter("@fileName", SqlDbType.VarChar, fileName);

                ds = ADONetDataService.RunSPReturnDataset("b2badminuser.pr_GetMediaObjectFileFromDatabase", DBConnectionString, parameters);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return (Byte[])ds.Tables[0].Rows[0]["ZUTXFT"];
                    }
                    return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                parameters = null;
                ds = null;
            }

        }
        #endregion
    }
}
