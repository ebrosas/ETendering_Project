using System;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class PublishedRFQDetail
    {
        public string OrderBuyerEmpEmail { get; set; }
        public string OrderBuyerEmpName { get; set; }
        public double OrderBuyerEmpNo { get; set; }
        public DateTime OrderClosingDate { get; set; }
        public string OrderDetCompany { get; set; }
        public string OrderDetCurrencyCode { get; set; }
        public string OrderDetDesc1 { get; set; }
        public string OrderDetDesc2 { get; set; }
        public string OrderDetLastStatus { get; set; }
        public double OrderDetLineNo { get; set; }
        public string OrderDetLineType { get; set; }
        public string OrderDetNextStatus { get; set; }
        public double OrderDetNo { get; set; }
        public string OrderDetPrintMsg { get; set; }
        public double OrderDetQuantity { get; set; }
        public string OrderDetStockLongItemNo { get; set; }
        public double OrderDetStockShortItemNo { get; set; }
        public string OrderDetSuffix { get; set; }
        public string OrderDetType { get; set; }
        public string OrderDetUM { get; set; }
        public string OrderDetUNSPSC { get; set; }
        public DateTime OrderPublishedDate { get; set; }
    }
}