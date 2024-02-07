using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace GARMCO.AMS.B2B.DAL
{
    public class CurrencyRepository
    {
        private readonly string connectionString;

        public CurrencyRepository()
        {
            this.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<Currency> GetAll()
        {
            var sql = "SELECT a.CVCRCD, a.CVDL01 FROM b2badminuser.F0013 a";

            return Disposable
                .Using(
                    () => new SqlConnection(this.connectionString),
                    connection => connection.Query<Currency>(sql))
                .OrderBy(x => x.CVDL01);
        }
    }
}
