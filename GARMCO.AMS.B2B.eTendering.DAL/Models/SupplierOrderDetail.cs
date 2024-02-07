using System;

namespace GARMCO.AMS.B2B.eTendering.DAL
{
    public class SupplierOrderDetail
    {
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
        public string OrderDetStockLongItemNo { get; set; }
        public double OrderDetStockShortItemNo { get; set; }
        public string OrderDetSuffix { get; set; }
        public int OrderDetTotalAttachmentFile { get; set; }
        public int OrderDetTotalAttachmentText { get; set; }
        public string OrderDetType { get; set; }
        public string OrderDetUM { get; set; }
        public double OrderDetUnitCost { get; set; }
        public string OrderDetUNSPSC { get; set; }
        public int SODCreatedBy { get; set; }
        public DateTime? SODCreatedDate { get; set; }
        public string SODCreatedName { get; set; }
        public double SODExtPrice { get; set; }
        public double SODFXRate { get; set; }
        public int SODID { get; set; }
        public bool SODLowest { get; set; }
        public string SODManufacturerCode { get; set; }
        public int SODModifiedBy { get; set; }
        public DateTime? SODModifiedDate { get; set; }
        public string SODModifiedName { get; set; }
        public double SODQuantity { get; set; }
        public string SODRemarks { get; set; }
        public bool SODSelected { get; set; }
        public int SODSupOrderID { get; set; }
        public string SODSupplierCode { get; set; }
        public int SODTotalAlternative { get; set; }
        public int SODTotalAttachment { get; set; }
        public string SODUM { get; set; }
        public double SODUnitCost { get; set; }
        public double SODUnitCostBD { get; set; }
        public int SupOrderSupplierNo { get; set; }
    }
}