using System;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class SupplierInvBidded
    {
        public bool SupplierBidded { get; set; }
        public int SupplierCreateBy { get; set; }
        public DateTime SupplierCreatedDate { get; set; }
        public string SupplierCreatedName { get; set; }
        public bool SupplierDeclined { get; set; }
        public bool SupplierInvited { get; set; }
        public double SupplierJDERefNo { get; set; }
        public int SupplierModifiedBy { get; set; }
        public DateTime SupplierModifiedDate { get; set; }
        public string SupplierModifiedName { get; set; }
        public string SupplierName { get; set; }
        public double SupplierOrderNo { get; set; }
    }
}