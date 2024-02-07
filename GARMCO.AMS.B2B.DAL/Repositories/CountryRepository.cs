using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace GARMCO.AMS.B2B.DAL
{
    public class CountryRepository
    {
        private readonly string connectionString;

        public CountryRepository()
        {
            this.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<Country> GetAll()
        {
            var sql = string.Format("SELECT a.DRKY, a.DRDL01 FROM b2badminuser.F0005 a WHERE a.DRSY = '{0}' AND a.DRRT = '{1}'", "00", "CN");

            return Disposable
                .Using(
                    () => new SqlConnection(this.connectionString),
                    connection => connection.Query<Country>(sql))
                .OrderBy(x => x.DRDL01);
        }
    }
}
