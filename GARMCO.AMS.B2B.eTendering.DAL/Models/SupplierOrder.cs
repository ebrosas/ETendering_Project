using System;

namespace GARMCO.AMS.B2B.eTendering.DAL
{
    public class SupplierOrder
    {
        public string OrderBuyerEmpEmail { get; set; }
        public string OrderBuyerEmpName { get; set; }
        public int OrderBuyerEmpNo { get; set; }
        public string OrderCategory { get; set; }
        public DateTime OrderClosingDate { get; set; }
        public string OrderCompany { get; set; }
        public string OrderDescription { get; set; }
        public double OrderNo { get; set; }
        public DateTime OrderOpenedDate { get; set; }
        public string OrderOriginatorEmpEmail { get; set; }
        public string OrderOriginatorEmpName { get; set; }
        public int OrderOriginatorEmpNo { get; set; }
        public string OrderPriority { get; set; }
        public string OrderPRNo { get; set; }
        public DateTime OrderPublishedDate { get; set; }
        public string OrderStatus { get; set; }
        public string OrderStatusDesc { get; set; }
        public string OrderSuffix { get; set; }
        public string OrderType { get; set; }
        public int SupOrderCreatedBy { get; set; }
        public DateTime SupOrderCreatedDate { get; set; }
        public string SupOrderCreatedName { get; set; }
        public string SupOrderCurrencyCode { get; set; }
        public string SupOrderDeliveryPt { get; set; }
        public int SupOrderDeliveryTime { get; set; }
        public string SupOrderDelTerm { get; set; }
        public int SupOrderID { get; set; }
        public int SupOrderModifiedBy { get; set; }
        public DateTime? SupOrderModifiedDate { get; set; }
        public string SupOrderModifiedName { get; set; }
        public string SupOrderPaymentTerm { get; set; }
        public int SupOrderShipVia { get; set; }
        public string SupOrderStatus { get; set; }
        public string SupOrderStatusDesc { get; set; }
        public DateTime? SupOrderValidityPeriod { get; set; }
        public bool SupplierDeclined { get; set; }
        public bool SupplierInvited { get; set; }
        public double SupplierJDERefNo { get; set; }
        public string SupplierName { get; set; }
        public int SupplierNo { get; set; }
    }
}