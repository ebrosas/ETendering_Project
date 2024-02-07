using System;

namespace GARMCO.AMS.B2B.eTendering.DAL
{
    public class Supplier
    {
        public bool ContactActive { get; set; }
        public string ContactActiveKey { get; set; }
        public int ContactCreatedBy { get; set; }
        public DateTime ContactCreatedDate { get; set; }
        public string ContactCreatedName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactFaxNo { get; set; }
        public int ContactID { get; set; }
        public string ContactMobNo { get; set; }
        public int ContactModifiedBy { get; set; }
        public DateTime ContactModifiedDate { get; set; }
        public string ContactModifiedName { get; set; }
        public string ContactName { get; set; }
        public string ContactPassword { get; set; }
        public bool ContactPrimary { get; set; }
        public bool ContactRejected { get; set; }
        public string ContactRejectReason { get; set; }
        public bool ContactReviewed { get; set; }
        public int ContactSupplierNo { get; set; }
        public string ContactTelNo { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierCity { get; set; }
        public string SupplierCountry { get; set; }
        public int SupplierCreatedBy { get; set; }
        public DateTime SupplierCreatedDate { get; set; }
        public string SupplierCreatedName { get; set; }
        public string SupplierCurrency { get; set; }
        public DateTime SupplierDateActivated { get; set; }
        public DateTime SupplierDateRegistered { get; set; }
        public string SupplierDelTerm { get; set; }
        public bool SupplierIncProdServ { get; set; }
        public double SupplierJDERefNo { get; set; }
        public int SupplierModifiedBy { get; set; }
        public DateTime SupplierModifiedDate { get; set; }
        public string SupplierModifiedName { get; set; }
        public string SupplierName { get; set; }
        public bool SupplierNews { get; set; }
        public int SupplierNo { get; set; }
        public bool SupplierNotProdServ { get; set; }
        public bool SupplierOld { get; set; }
        public string SupplierPostalCode { get; set; }
        public int SupplierShipVia { get; set; }
        public string SupplierState { get; set; }
        public string SupplierURL { get; set; }
    }
}