using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace GARMCO.AMS.B2B.DAL
{
    public class DeliveryTermRepository
    {
        private readonly string connectionString;

        public DeliveryTermRepository()
        {
            this.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<DeliveryTerm> GetAll()
        {
            var sql = string.Format("SELECT b.UDCID, b.UDCDesc1 FROM b2badminuser.UserDefinedCodeGroup AS a INNER JOIN b2badminuser.UserDefinedCode AS b ON b.UDCUDCGID = a.UDCGID WHERE a.UDCGCode = '{0}'", "DELTERM");

            return Disposable
                .Using(
                    () => new SqlConnection(this.connectionString),
                    connection => connection.Query<DeliveryTerm>(sql))
                .OrderBy(x => x.UDCDesc1);
        }
    }
}
