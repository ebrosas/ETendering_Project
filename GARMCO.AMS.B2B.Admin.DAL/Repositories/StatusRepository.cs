using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class StatusRepository
    {
        private string connectionString;

        public StatusRepository()
        {
            this.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<Status> GetAll()
        {
            var sql = new StringBuilder("SELECT b.UDCCode, b.UDCDesc1 FROM b2badminuser.UserDefinedCodeGroup a INNER JOIN b2badminuser.UserDefinedCode b ON b.UDCUDCGID = a.UDCGID WHERE a.UDCGCode = 'STATUS' AND b.UDCField = '07'").ToString();

            using (var connection = new SqlConnection(this.connectionString))
            {
                return connection
                    .Query<Status>(sql)
                    .OrderBy(x => x.UDCDesc1);
            }
        }
    }
}
