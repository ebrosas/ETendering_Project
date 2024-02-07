using System;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class OrderRequisitionDetail
    {
        public string OrderBuyerEmpName { get; set; }
        public int OrderBuyerEmpNo { get; set; }
        public DateTime OrderClosingDate { get; set; }
        public string OrderDetCompany { get; set; }
        public string OrderDetCurrencyCode { get; set; }
        public string OrderDetDesc { get; set; }
        public double OrderDetExtPrice { get; set; }
        public double OrderDetForeignExtPrice { get; set; }
        public double OrderDetForeignUnitCost { get; set; }
        public string OrderDetItemCode { get; set; }
        public double OrderDetLineNo { get; set; }
        public string OrderDetLineType { get; set; }
        public double OrderDetNo { get; set; }
        public double OrderDetQuantity { get; set; }
        public string OrderDetStatus { get; set; }
        public string OrderDetStatusDesc { get; set; }
        public string OrderDetStatusSpecialHandlingCode { get; set; }
        public string OrderDetStockLongItemNo { get; set; }
        public double OrderDetStockShortItemNo { get; set; }
        public string OrderDetSuffix { get; set; }
        public int OrderDetTotalExtAttachment { get; set; }
        public int OrderDetTotalFileAttachment { get; set; }
        public int OrderDetTotalSupplierBidded { get; set; }
        public string OrderDetType { get; set; }
        public string OrderDetUM { get; set; }
        public double OrderDetUnitCost { get; set; }
        public string OrderDetUNSPSC { get; set; }
        public DateTime OrderPublishedDate { get; set; }
    }
}