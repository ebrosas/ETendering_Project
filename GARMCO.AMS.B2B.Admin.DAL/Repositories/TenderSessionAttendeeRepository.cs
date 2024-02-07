using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Dapper;
using GARMCO.AMS.B2B.Utility;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class TenderSessionAttendeeRepository
    {
        private readonly string connectionString;

        public TenderSessionAttendeeRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public List<TenderSessionAttendeeItem> GetTenderSessionAttendeeList(byte? mode, int? tsAttTSID)
        {
            var list = new List<TenderSessionAttendeeItem>();

            var attendees = this.GetTenderSessionAttendee(mode, tsAttTSID);
            if (attendees != null) foreach (var attendee in attendees) list.Add(new TenderSessionAttendeeItem(attendee));

            return list;
        }
        public IEnumerable<TenderSessionAttendee> GetTenderSessionAttendee(byte? mode, int? tsAttTSID)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { mode, tsAttTSID };
                return connection.Query<TenderSessionAttendee>("b2badminuser.pr_GetTenderSessionAttendee", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }
        public void UpdateTenderSessionAttendee(TenderSessionAttendeeItem item, int? tsAttCreatedModifiedBy, string tsAttCreatedModifiedName, ref int? retError, ref string errorMsg)
        {
            retError = B2BConstants.DB_STATUS_OK;
            errorMsg = string.Empty;

            try
            {
                using (var scope = new TransactionScope())
                {
                    this.UpdateTenderSessionAttendee(item, tsAttCreatedModifiedBy, tsAttCreatedModifiedName, ref retError);
                    if (retError == B2BConstants.DB_STATUS_OK) scope.Complete();
                }
            }
            catch (Exception exception)
            {
                retError = B2BConstants.DB_STATUS_ERROR;
                errorMsg = exception.Message;
            }
        }
        public void UpdateTenderSessionAttendee(TenderSessionAttendeeItem item, int? tsAttCreatedModifiedBy, string tsAttCreatedModifiedName, ref int? retError)
        {
            byte? mode = 100;

            if (item.MarkForDeletion) mode = B2BConstants.DB_DELETE_RECORD;
            else if (item.Added) mode = B2BConstants.DB_INSERT_RECORD;
            else if (item.Modified) mode = B2BConstants.DB_UPDATE_RECORD;

            if (mode != 100)
            {
                using (var connection = new SqlConnection(this.connectionString))
                {
                    var param = new DynamicParameters();
                    param.Add("@mode", mode);
                    param.Add("@tsAttID", item.TSAttID);
                    param.Add("@tsAttTSID", item.TSAttTSID);
                    param.Add("@tsAttEmpNo", item.TSAttEmpNo);
                    param.Add("@tsAttEmpName", item.TSAttEmpName);
                    param.Add("@tsAttEmpEmail", item.TSAttEmpEmail);
                    param.Add("@tsAttusername", item.TSAttEmpUsername);
                    param.Add("@tsAttEmpPosition", item.TSAttEmpPosition);
                    param.Add("@tsAttDate", item.TSAttDate);
                    param.Add("@tsAttPresent", item.TSAttPresent);
                    param.Add("@tsAttPrimary", item.TSAttPrimary);
                    param.Add("@tsAttTenderComm", item.TSAttTenderComm);
                    param.Add("@tsAttSigned", item.TSAttSigned);
                    param.Add("@tsAttCreatedModifiedBy", tsAttCreatedModifiedBy);
                    param.Add("@tsAttCreatedModifiedName", tsAttCreatedModifiedName);
                    param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Execute("b2badminuser.pr_InsertUpdateDeleteTenderSessionAttendee", param, commandType: CommandType.StoredProcedure);
                    retError = param.Get<int>("@retError");
                }
            }
        }
    }
}
