using System;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class TenderSessionRFQ
    {
        public int RFQCreatedBy { get; set; }
        public DateTime RFQCreatedDate { get; set; }
        public string RFQCreatedName { get; set; }
        public DateTime RFQDateClosed { get; set; }
        public DateTime RFQDateOpened { get; set; }
        public int RFQID { get; set; }
        public int RFQModifiedBy { get; set; }
        public DateTime RFQModifiedDate { get; set; }
        public string RFQModifiedName { get; set; }
        public double RFQNo { get; set; }
        public bool RFQOpened { get; set; }
        public bool RFQProcessed { get; set; }
        public int RFQTSID { get; set; }
        public int TotalOrderDetLine { get; set; }
        public int TotalOrderSupplierBidded { get; set; }
        public int TotalOrderSupplierInvited { get; set; }
    }
}