using System;

namespace GARMCO.AMS.B2B.eTendering.DAL
{
    public class SupplierOrderAlternative
    {
        public int SODAltCreatedBy { get; set; }
        public DateTime? SODAltCreatedDate { get; set; }
        public string SODAltCreatedName { get; set; }
        public string SODAltCurrencyCode { get; set; }
        public string SODAltCurrencyCodeDesc { get; set; }
        public string SODAltDeliveryPt { get; set; }
        public int SODAltDeliveryTime { get; set; }
        public string SODAltDelTerm { get; set; }
        public string SODAltDesc { get; set; }
        public double SODAltExtPrice { get; set; }
        public double SODAltFXRate { get; set; }
        public int SODAltID { get; set; }
        public double SODAltLineNo { get; set; }
        public bool SODAltLowest { get; set; }
        public string SODAltManufacturerCode { get; set; }
        public int SODAltModifiedBy { get; set; }
        public DateTime? SODAltModifiedDate { get; set; }
        public string SODAltModifiedName { get; set; }
        public double SODAltQuantity { get; set; }
        public bool SODAltSelected { get; set; }
        public int SODAltShipVia { get; set; }
        public int SODAltSODID { get; set; }
        public string SODAltSupplierCode { get; set; }
        public int SODAltTotalAttachment { get; set; }
        public string SODAltUM { get; set; }
        public double SODAltUnitCost { get; set; }
        public double SODAltUnitCostBD { get; set; }
        public DateTime SODAltValidityPeriod { get; set; }
    }
}