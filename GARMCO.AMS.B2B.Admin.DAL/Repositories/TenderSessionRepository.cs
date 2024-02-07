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
    public class TenderSessionRepository
    {
        private readonly string connectionString;
        private readonly TenderSessionAttendeeRepository attendeeRepository;

        public TenderSessionRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
            this.attendeeRepository = new TenderSessionAttendeeRepository();
        }

        public IEnumerable<TenderSession> GetTenderSession(byte? mode, int? tsID, DateTime? tsDateStart, DateTime? tsDateEnd, double? tsRFQNo, int? tsAttendeeEmpNo, string tsAttendeeEmpName, int? tsCurrentUser, int? startRowIndex, int? maximumRows, string sort)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                if (maximumRows == null) maximumRows = 10;
                var param = new { mode, tsID, tsDateStart, tsDateEnd, tsRFQNo, tsAttendeeEmpNo, tsAttendeeEmpName, tsCurrentUser, startRowIndex, maximumRows, sort };
                return connection.Query<TenderSession>("b2badminuser.pr_GetTenderSession", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }
        public int GetTenderSessionTotal(byte? mode, int? tsID, DateTime? tsDateStart, DateTime? tsDateEnd, double? tsRFQNo, int? tsAttendeeEmpNo, string tsAttendeeEmpName, int? tsCurrentUser, string sort)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { tsID, tsDateStart, tsDateEnd, tsRFQNo, tsAttendeeEmpNo, tsAttendeeEmpName, tsCurrentUser };
                return connection.Query<int>("b2badminuser.pr_GetTenderSessionTotal", param, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }
        public void InsertTenderSession(byte? mode, ref int? tsID, DateTime? tsStartDate, DateTime? tsEndDate, bool? tsOpen, int? tsRFQOpen, int? tsRFQProcessed, List<TenderSessionAttendeeItem> tsAttendeeList, int? tsCreatedModifiedBy, string tsCreatedModifiedName, ref int? retError, ref string errorMsg)
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
                        param.Add("@tsID", value: tsID, dbType: DbType.Int32, direction: ParameterDirection.Output);
                        param.Add("@tsStartDate", tsStartDate);
                        param.Add("@tsEndDate", tsEndDate);
                        param.Add("@tsOpen", tsOpen);
                        param.Add("@tsRFQOpen", tsRFQOpen);
                        param.Add("@tsRFQProcessed", tsRFQProcessed);
                        param.Add("@tsCreatedModifiedBy", tsCreatedModifiedBy);
                        param.Add("@tsCreatedModifiedName", tsCreatedModifiedName);
                        param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Execute("b2badminuser.pr_InsertUpdateDeleteTenderSession", param, commandType: CommandType.StoredProcedure);
                        retError = param.Get<int>("@retError");
                    }

                    if (retError == B2BConstants.DB_STATUS_OK)
                    {
                        foreach (TenderSessionAttendeeItem item in tsAttendeeList)
                        {
                            item.Added = true;
                            item.TSAttTSID = (int)tsID;
                            item.TSAttDate = tsStartDate;

                            this.attendeeRepository.UpdateTenderSessionAttendee(item, tsCreatedModifiedBy, tsCreatedModifiedName, ref retError);

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
        public void DeleteTenderSession(byte? mode, int? tsID, DateTime? tsStartDate, DateTime? tsEndDate, bool? tsOpen, int? tsRFQOpen, int? tsRFQProcessed, int? tsCreatedModifiedBy, string tsCreatedModifiedName, ref int? retError, ref string errorMsg)
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
                        param.Add("@tsID", tsID, dbType: DbType.Int32, direction: ParameterDirection.Output);
                        param.Add("@tsStartDate", tsStartDate);
                        param.Add("@tsEndDate", tsEndDate);
                        param.Add("@tsOpen", tsOpen);
                        param.Add("@tsRFQOpen", tsRFQOpen);
                        param.Add("@tsRFQProcessed", tsRFQProcessed);
                        param.Add("@tsCreatedModifiedBy", tsCreatedModifiedBy);
                        param.Add("@tsCreatedModifiedName", tsCreatedModifiedName);
                        param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Execute("b2badminuser.pr_InsertUpdateDeleteTenderSession", param, commandType: CommandType.StoredProcedure);
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
        public void UpdateTenderSession(byte? mode, int? tsID, DateTime? tsStartDate, DateTime? tsEndDate, bool? tsOpen, int? tsRFQOpen, int? tsRFQProcessed, List<TenderSessionAttendeeItem> tsAttendeeList, int? tsCreatedModifiedBy, string tsCreatedModifiedName, ref int? retError, ref string errorMsg)
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
                        param.Add("@tsID", tsID, dbType: DbType.Int32, direction: ParameterDirection.Output);
                        param.Add("@tsStartDate", tsStartDate);
                        param.Add("@tsEndDate", tsEndDate);
                        param.Add("@tsOpen", tsOpen);
                        param.Add("@tsRFQOpen", tsRFQOpen);
                        param.Add("@tsRFQProcessed", tsRFQProcessed);
                        param.Add("@tsCreatedModifiedBy", tsCreatedModifiedBy);
                        param.Add("@tsCreatedModifiedName", tsCreatedModifiedName);
                        param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Execute("b2badminuser.pr_InsertUpdateDeleteTenderSession", param, commandType: CommandType.StoredProcedure);
                        retError = param.Get<int>("@retError");
                    }

                    if (retError == B2BConstants.DB_STATUS_OK)
                    {
                        foreach (TenderSessionAttendeeItem item in tsAttendeeList)
                        {
                            if (item.Added)
                            {
                                item.TSAttTSID = (int)tsID;
                                item.TSAttDate = tsStartDate;
                            }

                            this.attendeeRepository.UpdateTenderSessionAttendee(item, tsCreatedModifiedBy, tsCreatedModifiedName, ref retError);

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
    }
}
