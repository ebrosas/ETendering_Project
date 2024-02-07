using System;
using System.Linq;
using System.Data.SqlClient;
using Dapper;

namespace GARMCO.AMS.B2B.DAL
{
    public class TenderCommitteeCriterionRepository
    {
        private readonly string connectionString;

        public TenderCommitteeCriterionRepository()
        {
            this.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public TenderCommitteeCriterion Get()
        {
            var sql = "SELECT (SELECT b.UDCAmount FROM b2badminuser.UserDefinedCodeGroup AS a INNER JOIN b2badminuser.UserDefinedCode AS b ON b.UDCUDCGID = a.UDCGID WHERE a.UDCGCode = 'TENDERCOMM' AND b.UDCCode = 'MINBID') MINBID, (SELECT b.UDCAmount FROM b2badminuser.UserDefinedCodeGroup AS a INNER JOIN b2badminuser.UserDefinedCode AS b ON b.UDCUDCGID = a.UDCGID WHERE a.UDCGCode = 'TENDERCOMM'AND b.UDCCode = 'MINQUOTED') MINQUOTED";

            return Disposable
                .Using(
                    () => new SqlConnection(this.connectionString),
                    connection => connection.Query<TenderCommitteeCriterion>(sql))
                .Single();
        }
    }
}
