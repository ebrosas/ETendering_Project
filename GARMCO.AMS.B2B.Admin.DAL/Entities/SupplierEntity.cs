using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GARMCO.AMS.B2B.Admin.DAL.Entities
{
    [Serializable]
    public class SupplierEntity
    {
        public int SupplierNo { get; set; }
        public double SupplierJDERefNo { get; set; }
        public string SupplierName { get; set; }
        public string SupplierURL { get; set; }
        public string SupplierAddress { get; set; }
        public string SupOrderPaymentTerm { get; set; }
        public string SupOrderCreatedName { get; set; }
        public DateTime? SupOrderCreatedDate { get; set; }
        public double RFQNo { get; set; }
        public string OrderCompany { get; set; }
        public string OrderType { get; set; }
        public string OrderSuffix { get; set; }
        public string OrderPRNo { get; set; }
        public int OrderBuyerEmpNo { get; set; }
        public string OrderBuyerEmpName { get; set; }
    }
}
