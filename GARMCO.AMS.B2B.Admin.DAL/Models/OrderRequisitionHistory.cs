using System;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class OrderRequisitionHistory
    {
        public int OHCreatedBy { get; set; }
        public DateTime OHCreatedDate { get; set; }
        public string OHCreatedName { get; set; }
        public string OHDesc { get; set; }
        public decimal OHID { get; set; }
        public int OHModifiedBy { get; set; }
        public DateTime OHModifiedDate { get; set; }
        public string OHModifiedName { get; set; }
        public double OHOrderNo { get; set; }
        public int OHTSID { get; set; }
    }
}