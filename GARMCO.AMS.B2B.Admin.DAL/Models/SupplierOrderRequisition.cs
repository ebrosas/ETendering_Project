using System;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class SupplierOrderRequisition
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
        public string OrderDetStockLongItemNo { get; set; }
        public double OrderDetStockShortItemNo { get; set; }
        public string OrderDetSuffix { get; set; }
        public string OrderDetType { get; set; }
        public string OrderDetUM { get; set; }
        public double OrderDetUnitCost { get; set; }
        public string OrderDetUNSPSC { get; set; }
        public int OrderPrimary { get; set; }
        public double SupOrderDetAltLineNo { get; set; }
        public int SupOrderDetCreatedBy { get; set; }
        public DateTime? SupOrderDetCreatedDate { get; set; }
        public string SupOrderDetCreatedName { get; set; }
        public string SupOrderDetCurrencyCode { get; set; }
        public string SupOrderDetDeliveryPt { get; set; }
        public int SupOrderDetDeliveryTime { get; set; }
        public string SupOrderDetDelTerm { get; set; }
        public string SupOrderDetDelTermDesc { get; set; }
        public string SupOrderDetDesc { get; set; }
        public double SupOrderDetExtPrice { get; set; }
        public double SupOrderDetFXRate { get; set; }
        public double SupOrderDetLineNo { get; set; }
        public bool SupOrderDetLowest { get; set; }
        public string SupOrderDetManufactureCode { get; set; }
        public int SupOrderDetModifiedBy { get; set; }
        public DateTime? SupOrderDetModifiedDate { get; set; }
        public string SupOrderDetModifiedName { get; set; }
        public int SupOrderDetOtherChgID { get; set; }
        public string SupOrderDetPaymentTerm { get; set; }
        public double SupOrderDetQuantity { get; set; }
        public string SupOrderDetRemarks { get; set; }
        public bool SupOrderDetSelected { get; set; }
        public int SupOrderDetShipVia { get; set; }
        public int SupOrderDetSODAltID { get; set; }
        public int SupOrderDetSODID { get; set; }
        public int SupOrderDetSupOrderID { get; set; }
        public string SupOrderDetSupplierCode { get; set; }
        public int SupOrderDetTotalAttach { get; set; }
        public string SupOrderDetUM { get; set; }
        public double SupOrderDetUnitCost { get; set; }
        public double SupOrderDetUnitCostBD { get; set; }
        public DateTime? SupOrderDetValidityPeriod { get; set; }
        public bool SupOrderSupplierInvited { get; set; }
        public double SupOrderSupplierJDERefNo { get; set; }
        public string SupOrderSupplierName { get; set; }
        public int SupOrderSupplierNo { get; set; }
    }
}