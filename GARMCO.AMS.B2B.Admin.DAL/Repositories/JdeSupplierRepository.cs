using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class JdeSupplierRepository
    {
        private readonly string connectionString;

        public JdeSupplierRepository()
        {
            this.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GRMB2BAdminDBConnectionString"].ConnectionString;
        }

        public IEnumerable<JdeSupplier> GetBy(int? supplierNo, string supplierName, string supplierContact, int startRowIndex, int? maximumRows, string sortExpression)
        {
            var predicates = new List<string>();

            if (supplierNo != null) predicates.Add("a.CustSupNo = @supplierNo");
            else
            {
                if (!string.IsNullOrWhiteSpace(supplierName)) predicates.Add("a.CustSupName LIKE '%' + @supplierName + '%'");
                if (!string.IsNullOrWhiteSpace(supplierContact)) predicates.Add("a.CustSupContact LIKE '%' + @supplierContact + '%'");
            }

            var searchCondition = string.Join(" AND ", predicates);
            var whereClause = searchCondition == string.Empty ? string.Empty : string.Format("WHERE {0}", searchCondition);

            var sql = new StringBuilder()
                .AppendFormat(" SELECT a.*")
                .AppendFormat(" FROM b2badminuser.JDESupplier a")
                .AppendFormat(" {0}", whereClause)
                .AppendFormat(" ORDER BY a.{0} OFFSET @startRowIndex ROWS FETCH NEXT @maximumRows ROWS ONLY", (!string.IsNullOrWhiteSpace(sortExpression)) ? sortExpression : "CustSupName ASC")
                .ToString();

            startRowIndex *= maximumRows.Value;

            var param = new { supplierNo, supplierName, supplierContact, startRowIndex, maximumRows };

            return Disposable.Using(
                () => new SqlConnection(this.connectionString),
                connection => connection.Query<JdeSupplier>(sql, param));
        }
        public IEnumerable<JdeSupplier> GetById(int supplierNo)
        {
            var sql = new StringBuilder()
                .Append(" SELECT a.*")
                .Append(" FROM b2badminuser.JDESupplier a")
                .Append(" WHERE a.CustSupNo = @supplierNo")
                .ToString();

            var param = new { supplierNo };

            return Disposable
                .Using(
                    () => new SqlConnection(this.connectionString),
                    connection => connection.Query<JdeSupplier>(sql, param))
                .AsList();
        }
        public int GetCountBy(int? supplierNo, string supplierName, string supplierContact)
        {
            var predicates = new List<string>();

            if (supplierNo != null) predicates.Add("a.CustSupNo = @supplierNo");
            else
            {
                if (!string.IsNullOrWhiteSpace(supplierName)) predicates.Add("a.CustSupName LIKE '%' + @supplierName + '%'");
                if (!string.IsNullOrWhiteSpace(supplierContact)) predicates.Add("a.CustSupContact LIKE '%' + @supplierContact + '%'");
            }

            var searchCondition = string.Join(" AND ", predicates);
            var whereClause = searchCondition == string.Empty ? string.Empty : string.Format(" WHERE {0}", searchCondition);

            var sql = new StringBuilder()
                .Append("SELECT COUNT(1) FROM b2badminuser.JDESupplier a")
                .AppendFormat("{0}", whereClause)
                .ToString();

            var param = new { supplierNo, supplierName, supplierContact };

            return Disposable
                .Using(
                    () => new SqlConnection(this.connectionString),
                    connection => connection.Query<int>(sql, param))
                .SingleOrDefault();
        }
    }
}
