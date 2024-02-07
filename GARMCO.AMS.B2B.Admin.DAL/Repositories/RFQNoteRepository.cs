using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class RFQNoteRepository
    {
        private readonly string connectionString;

        public RFQNoteRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<RFQNote> GetRFQNote(byte? mode, int? noteID, double? noteRFQNo, string noteRemark, int? noteCurrentUser, int? startRowIndex, int? maximumRows, string sort)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                if (maximumRows == null || maximumRows == 0) maximumRows = 10;
                var param = new { mode, noteID, noteRFQNo, noteRemark, noteCurrentUser, startRowIndex, maximumRows, sort };
                return connection.Query<RFQNote>("b2badminuser.pr_GetRFQNote", param, commandType: CommandType.StoredProcedure).AsList();
            }
        }
        public int GetRFQNoteTotal(byte? mode, int? noteID, double? noteRFQNo, string noteRemark, int? noteCurrentUser, string sort)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new { noteID, noteRFQNo, noteRemark, noteCurrentUser };
                return connection.Query<int>("b2badminuser.pr_GetRFQNoteTotal", param, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }
        public void InsertUpdateDeleteRFQNote(byte? mode, int? noteID, double? noteRFQNo, string noteRemark, int? noteCreatedModifiedBy, string noteCreatedModifiedName, ref int? retError)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@mode", mode);
                param.Add("@noteID", noteID);
                param.Add("@noteRFQNo", noteRFQNo);
                param.Add("@noteRemark", noteRemark);
                param.Add("@noteCreatedModifiedBy", noteCreatedModifiedBy);
                param.Add("@noteCreatedModifiedName", noteCreatedModifiedName);
                param.Add("@retError", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("b2badminuser.pr_InsertUpdateDeleteRFQNote", param, commandType: CommandType.StoredProcedure);
                retError = param.Get<int>("@retError");
            }
        }
    }
}