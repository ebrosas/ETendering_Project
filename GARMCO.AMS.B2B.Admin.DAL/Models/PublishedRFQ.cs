using System;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class PublishedRFQ
    {
        public string OrderBuyerEmpEmail { get; set; }
        public string OrderBuyerEmpName { get; set; }
        public double OrderBuyerEmpNo { get; set; }
        public string OrderCategory { get; set; }
        public DateTime OrderClosingDate { get; set; }
        public string OrderCompany { get; set; }
        public string OrderEmpName { get; set; }
        public double OrderEmpNo { get; set; }
        public double OrderNo { get; set; }
        public string OrderPriority { get; set; }
        public string OrderPRNo { get; set; }
        public string OrderSuffix { get; set; }
        public DateTime OrderTransactionDate { get; set; }
        public string OrderType { get; set; }
    }
}
