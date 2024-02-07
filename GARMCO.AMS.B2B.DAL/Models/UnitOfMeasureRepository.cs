using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

namespace GARMCO.AMS.B2B.DAL
{
    public class UnitOfMeasureRepository
    {
        public IEnumerable<UnitOfMeasure> GetAll()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<UnitOfMeasure>("SELECT RTRIM(LTRIM(a.DRKY)) DRKY, RTRIM(a.DRDL01) DRDL01 FROM b2badminuser.F0005 a WHERE a.DRSY = '00' AND a.DRRT = 'UM' ORDER BY RTRIM(a.DRDL01)").AsList();
            }
        }
    }
}
