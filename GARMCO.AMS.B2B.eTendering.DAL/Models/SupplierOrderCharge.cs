using System;

namespace GARMCO.AMS.B2B.eTendering.DAL
{
    public class SupplierOrderCharge
    {
        public double OtherChgAmount { get; set; }
        public double OtherChgAmountBD { get; set; }
        public int OtherChgCreatedBy { get; set; }
        public DateTime? OtherChgCreatedDate { get; set; }
        public string OtherChgCreatedName { get; set; }
        public string OtherChgCurrencyCode { get; set; }
        public string OtherChgCurrencyCodeDesc { get; set; }
        public string OtherChgDesc { get; set; }
        public double OtherChgFXRate { get; set; }
        public int OtherChgID { get; set; }
        public int OtherChgModifiedBy { get; set; }
        public DateTime? OtherChgModifiedDate { get; set; }
        public string OtherChgModifiedName { get; set; }
        public bool OtherChgSelected { get; set; }
        public int OtherChgSupOrderID { get; set; }
    }
}