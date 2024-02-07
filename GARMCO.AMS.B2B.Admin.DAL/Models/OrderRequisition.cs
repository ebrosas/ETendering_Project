using System;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class OrderRequisition
    {
        public string OrderBuyerEmpEmail { get; set; }
        public string OrderBuyerEmpName { get; set; }
        public int OrderBuyerEmpNo { get; set; }
        public string OrderCategory { get; set; }
        public DateTime OrderClosingDate { get; set; }
        public string OrderCompany { get; set; }
        public string OrderDescription { get; set; }
        public bool OrderDownloaded { get; set; }
        public double OrderNo { get; set; }
        public DateTime? OrderOpenedDate { get; set; }
        public string OrderOriginatorEmpName { get; set; }
        public int OrderOriginatorEmpNo { get; set; }
        public string OrderPriority { get; set; }
        public string OrderPRNo { get; set; }
        public DateTime? OrderPublishedDate { get; set; }
        public string OrderStatus { get; set; }
        public string OrderStatusDesc { get; set; }
        public string OrderStatusSpecialHandlingCode { get; set; }
        public string OrderSuffix { get; set; }
        public bool OrderSupplierParticipated { get; set; }
        public bool OrderTenderComm { get; set; }
        public DateTime OrderTransactionDate { get; set; }
        public string OrderType { get; set; }
        public bool OrderUploaded { get; set; }
        public int TotalOrderLine { get; set; }
        public int TotalOrderSupplierInvited { get; set; }
        public int TotalOrderSupplierParticipated { get; set; }
    }
}