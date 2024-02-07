using System;
using System.Data;
using GARMCO.Common.Object;

namespace GARMCO.AMS.B2B.Admin.DAL
{
	[Serializable]
	sealed public class SupplierOrderRequisitionItem : ObjectItem
	{
		#region Properties
		public string OrderDetCompany { get; set; }
		public double? OrderDetNo { get; set; }
		public string OrderDetType { get; set; }
		public string OrderDetSuffix { get; set; }
		public double? OrderDetLineNo { get; set; }
		public string OrderDetLineType { get; set; }
		public string OrderDetItemCode { get; set; }
		public double? OrderDetQuantity { get; set; }
		public string OrderDetUM { get; set; }
		public double? OrderDetUnitCost { get; set; }
		public double? OrderDetExtPrice { get; set; }
		public double? OrderDetForeignUnitCost { get; set; }
		public double? OrderDetForeignExtPrice { get; set; }
		public string OrderDetCurrencyCode { get; set; }
		public string OrderDetDesc { get; set; }
		public string OrderDetUNSPSC { get; set; }
		public double? OrderDetStockShortItemNo { get; set; }
		public string OrderDetStockLongItemNo { get; set; }
		public string OrderDetStatus { get; set; }
		public int OrderPrimary { get; set; }

		public string SupOrderSupplierName { get; set; }
		public double SupOrderSupplierJDERefNo { get; set; }
		public int SupOrderSupplierNo { get; set; }
		public bool SupOrderSupplierInvited { get; set; }
		public int SupOrderDetSupOrderID { get; set; }
		public int SupOrderDetSODID { get; set; }
		public int SupOrderDetSODAltID { get; set; }
		public int SupOrderDetOtherChgID { get; set; }
		public double SupOrderDetLineNo { get; set; }
		public double SupOrderDetAltLineNo { get; set; }
		public string SupOrderDetSupplierCode { get; set; }
		public string SupOrderDetManufactureCode { get; set; }
		public string SupOrderDetCurrencyCode { get; set; }
		public DateTime? SupOrderDetValidityPeriod { get; set; }
		public int SupOrderDetDeliveryTime { get; set; }
		public string SupOrderDetDelTerm { get; set; }
		public string SupOrderDetDelTermDesc { get; set; }
		public int SupOrderDetShipVia { get; set; }
		public string SupOrderDetDeliveryPt { get; set; }
		public string SupOrderDetPaymentTerm { get; set; }
		public string SupOrderDetDesc { get; set; }
		public double SupOrderDetQuantity { get; set; }
		public string SupOrderDetUM { get; set; }
		public double SupOrderDetUnitCost { get; set; }
		public double SupOrderDetExtPrice { get; set; }
		public double SupOrderDetFXRate { get; set; }
		public double SupOrderDetUnitCostBD { get; set; }
		public string SupOrderDetRemarks { get; set; }
		public bool SupOrderDetSelected { get; set; }
		public bool SupOrderDetLowest { get; set; }
		public int SupOrderDetTotalAttach { get; set; }

		public int SupOrderDetCreatedBy { get; set; }
		public string SupOrderDetCreatedName { get; set; }
		public DateTime? SupOrderDetCreatedDate { get; set; }
		public int SupOrderDetModifiedBy { get; set; }
		public string SupOrderDetModifiedName { get; set; }
		public DateTime? SupOrderDetModifiedDate { get; set; }
		#endregion

		#region Constructors
		public SupplierOrderRequisitionItem() :
			base()
		{
			this.OrderDetCompany = String.Empty;
			this.OrderDetNo = 0;
			this.OrderDetType = String.Empty;
			this.OrderDetSuffix = String.Empty;
			this.OrderDetLineNo = 0;
			this.OrderDetLineType = String.Empty;
			this.OrderDetItemCode = String.Empty;
			this.OrderDetQuantity = 0;
			this.OrderDetUM = String.Empty;
			this.OrderDetUnitCost = 0;
			this.OrderDetExtPrice = 0;
			this.OrderDetForeignUnitCost = 0;
			this.OrderDetForeignExtPrice = 0;
			this.OrderDetCurrencyCode = String.Empty;
			this.OrderDetDesc = String.Empty;
			this.OrderDetUNSPSC = String.Empty;
			this.OrderDetStockShortItemNo = 0;
			this.OrderDetStockLongItemNo = String.Empty;
			this.OrderDetStatus = String.Empty;
			this.OrderPrimary = 0;

			this.SupOrderSupplierName = String.Empty;
			this.SupOrderSupplierJDERefNo = 0;
			this.SupOrderSupplierNo = 0;
			this.SupOrderSupplierInvited = false;
			this.SupOrderDetSupOrderID = 0;
			this.SupOrderDetSODID = 0;
			this.SupOrderDetSODAltID = 0;
			this.SupOrderDetOtherChgID = 0;
			this.SupOrderDetLineNo = 0;
			this.SupOrderDetAltLineNo = 0;
			this.SupOrderDetSupplierCode = String.Empty;
			this.SupOrderDetManufactureCode = String.Empty;
			this.SupOrderDetCurrencyCode = String.Empty;
			this.SupOrderDetValidityPeriod = DateTime.Now;
			this.SupOrderDetDeliveryTime = 0;
			this.SupOrderDetDelTerm = String.Empty;
			this.SupOrderDetDelTermDesc = String.Empty;
			this.SupOrderDetShipVia = 0;
			this.SupOrderDetDeliveryPt = String.Empty;
			this.SupOrderDetPaymentTerm = String.Empty;
			this.SupOrderDetDesc = String.Empty;
			this.SupOrderDetQuantity = 0;
			this.SupOrderDetUM = String.Empty;
			this.SupOrderDetUnitCost = 0;
			this.SupOrderDetExtPrice = 0;
			this.SupOrderDetFXRate = 0;
			this.SupOrderDetUnitCostBD = 0;
			this.SupOrderDetRemarks = String.Empty;
			this.SupOrderDetSelected = false;
			this.SupOrderDetLowest = false;
			this.SupOrderDetTotalAttach = 0;

			this.SupOrderDetCreatedBy = 0;
			this.SupOrderDetCreatedName = String.Empty;
			this.SupOrderDetCreatedDate = DateTime.Now;
			this.SupOrderDetModifiedBy = 0;
			this.SupOrderDetModifiedName = String.Empty;
			this.SupOrderDetModifiedDate = DateTime.Now;
		}

		public SupplierOrderRequisitionItem(DataRow row) :
			this()
		{
			this.AssignItem(row);
		}

		public SupplierOrderRequisitionItem(SupplierOrderRequisition supplierOrderRequisition) :
			this()
		{
			this.OrderDetCompany = supplierOrderRequisition.OrderDetCompany;
			this.OrderDetNo = supplierOrderRequisition.OrderDetNo;
			this.OrderDetType = supplierOrderRequisition.OrderDetType;
			this.OrderDetSuffix = supplierOrderRequisition.OrderDetSuffix;
			this.OrderDetLineNo = supplierOrderRequisition.OrderDetLineNo;
			this.OrderDetLineType = supplierOrderRequisition.OrderDetLineType;
			this.OrderDetItemCode = supplierOrderRequisition.OrderDetItemCode;
			this.OrderDetQuantity = supplierOrderRequisition.OrderDetQuantity;
			this.OrderDetUM = supplierOrderRequisition.OrderDetUM;
			this.OrderDetUnitCost = supplierOrderRequisition.OrderDetUnitCost;
			this.OrderDetExtPrice = supplierOrderRequisition.OrderDetExtPrice;
			this.OrderDetForeignUnitCost = supplierOrderRequisition.OrderDetForeignUnitCost;
			this.OrderDetForeignExtPrice = supplierOrderRequisition.OrderDetForeignExtPrice;
			this.OrderDetCurrencyCode = supplierOrderRequisition.OrderDetCurrencyCode;
			this.OrderDetDesc = supplierOrderRequisition.OrderDetDesc;
			this.OrderDetUNSPSC = supplierOrderRequisition.OrderDetUNSPSC;
			this.OrderDetStockShortItemNo = supplierOrderRequisition.OrderDetStockShortItemNo;
			this.OrderDetStockLongItemNo = supplierOrderRequisition.OrderDetStockLongItemNo;
			this.OrderDetStatus = supplierOrderRequisition.OrderDetStatus;
			this.OrderPrimary = supplierOrderRequisition.OrderPrimary;

			this.SupOrderSupplierName = supplierOrderRequisition.SupOrderSupplierName;
			this.SupOrderSupplierJDERefNo = supplierOrderRequisition.SupOrderSupplierJDERefNo;
			this.SupOrderSupplierNo = supplierOrderRequisition.SupOrderSupplierNo;
			this.SupOrderSupplierInvited = supplierOrderRequisition.SupOrderSupplierInvited;
			this.SupOrderDetSupOrderID = supplierOrderRequisition.SupOrderDetSupOrderID;
			this.SupOrderDetSODID = supplierOrderRequisition.SupOrderDetSODID;
			this.SupOrderDetSODAltID = supplierOrderRequisition.SupOrderDetSODAltID;
			this.SupOrderDetOtherChgID = supplierOrderRequisition.SupOrderDetOtherChgID;
			this.SupOrderDetLineNo = supplierOrderRequisition.SupOrderDetLineNo;
			this.SupOrderDetAltLineNo = supplierOrderRequisition.SupOrderDetAltLineNo;
			this.SupOrderDetSupplierCode = supplierOrderRequisition.SupOrderDetSupplierCode;
			this.SupOrderDetManufactureCode = supplierOrderRequisition.SupOrderDetManufactureCode;
			this.SupOrderDetCurrencyCode = supplierOrderRequisition.SupOrderDetCurrencyCode;
			this.SupOrderDetValidityPeriod = !supplierOrderRequisition.SupOrderDetValidityPeriod.HasValue ? default(DateTime?) : supplierOrderRequisition.SupOrderDetValidityPeriod;
			this.SupOrderDetDeliveryTime = supplierOrderRequisition.SupOrderDetDeliveryTime;
			this.SupOrderDetDelTerm = supplierOrderRequisition.SupOrderDetDelTerm;
			this.SupOrderDetDelTermDesc = supplierOrderRequisition.SupOrderDetDelTermDesc;
			this.SupOrderDetShipVia = supplierOrderRequisition.SupOrderDetShipVia;
			this.SupOrderDetDeliveryPt = supplierOrderRequisition.SupOrderDetDeliveryPt;
			this.SupOrderDetPaymentTerm = supplierOrderRequisition.SupOrderDetPaymentTerm;
			this.SupOrderDetDesc = supplierOrderRequisition.SupOrderDetDesc;
			this.SupOrderDetQuantity = supplierOrderRequisition.SupOrderDetQuantity;
			this.SupOrderDetUM = supplierOrderRequisition.SupOrderDetUM;
			this.SupOrderDetUnitCost = supplierOrderRequisition.SupOrderDetUnitCost;
			this.SupOrderDetExtPrice = supplierOrderRequisition.SupOrderDetExtPrice = supplierOrderRequisition.SupOrderDetExtPrice == 0 ? supplierOrderRequisition.SupOrderDetUnitCost * supplierOrderRequisition.SupOrderDetQuantity : supplierOrderRequisition.SupOrderDetExtPrice;
			this.SupOrderDetFXRate = supplierOrderRequisition.SupOrderDetFXRate;
			this.SupOrderDetUnitCostBD = supplierOrderRequisition.SupOrderDetUnitCostBD;
			this.SupOrderDetRemarks = supplierOrderRequisition.SupOrderDetRemarks;
			this.SupOrderDetSelected = supplierOrderRequisition.SupOrderDetSelected;
			this.SupOrderDetLowest = supplierOrderRequisition.SupOrderDetLowest;
			this.SupOrderDetTotalAttach = supplierOrderRequisition.SupOrderDetTotalAttach;

			this.SupOrderDetCreatedBy = supplierOrderRequisition.SupOrderDetCreatedBy;
			this.SupOrderDetCreatedName = supplierOrderRequisition.SupOrderDetCreatedName;
			if (supplierOrderRequisition.SupOrderDetCreatedDate.HasValue)
				this.SupOrderDetCreatedDate = supplierOrderRequisition.SupOrderDetCreatedDate;
			this.SupOrderDetModifiedBy = supplierOrderRequisition.SupOrderDetModifiedBy;
			this.SupOrderDetModifiedName = supplierOrderRequisition.SupOrderDetModifiedName;
			if (supplierOrderRequisition.SupOrderDetModifiedDate.HasValue)
				this.SupOrderDetModifiedDate = supplierOrderRequisition.SupOrderDetModifiedDate;
		}

		public SupplierOrderRequisitionItem(SupplierOrderRequisitionItem item) :
			this()
		{
			this.MarkForDeletion = item.MarkForDeletion;
			this.Added = item.Added;
			this.Modified = item.Modified;

			this.OrderDetCompany = item.OrderDetCompany;
			this.OrderDetNo = item.OrderDetNo;
			this.OrderDetType = item.OrderDetType;
			this.OrderDetSuffix = item.OrderDetSuffix;
			this.OrderDetLineNo = item.OrderDetLineNo;
			this.OrderDetLineType = item.OrderDetLineType;
			this.OrderDetItemCode = item.OrderDetItemCode;
			this.OrderDetQuantity = item.OrderDetQuantity;
			this.OrderDetUM = item.OrderDetUM;
			this.OrderDetUnitCost = item.OrderDetUnitCost;
			this.OrderDetExtPrice = item.OrderDetExtPrice;
			this.OrderDetForeignUnitCost = item.OrderDetForeignUnitCost;
			this.OrderDetForeignExtPrice = item.OrderDetForeignExtPrice;
			this.OrderDetCurrencyCode = item.OrderDetCurrencyCode;
			this.OrderDetDesc = item.OrderDetDesc;
			this.OrderDetUNSPSC = item.OrderDetUNSPSC;
			this.OrderDetStockShortItemNo = item.OrderDetStockShortItemNo;
			this.OrderDetStockLongItemNo = item.OrderDetStockLongItemNo;
			this.OrderDetStatus = item.OrderDetStatus;
			this.OrderPrimary = item.OrderPrimary;

			this.SupOrderSupplierName = item.SupOrderSupplierName;
			this.SupOrderSupplierJDERefNo = item.SupOrderSupplierJDERefNo;
			this.SupOrderSupplierNo = item.SupOrderSupplierNo;
			this.SupOrderSupplierInvited = item.SupOrderSupplierInvited;
			this.SupOrderDetSupOrderID = item.SupOrderDetSupOrderID;
			this.SupOrderDetSODID = item.SupOrderDetSODID;
			this.SupOrderDetSODAltID = item.SupOrderDetSODAltID;
			this.SupOrderDetOtherChgID = item.SupOrderDetOtherChgID;
			this.SupOrderDetLineNo = item.SupOrderDetLineNo;
			this.SupOrderDetAltLineNo = item.SupOrderDetAltLineNo;
			this.SupOrderDetSupplierCode = item.SupOrderDetSupplierCode;
			this.SupOrderDetManufactureCode = item.SupOrderDetManufactureCode;
			this.SupOrderDetCurrencyCode = item.SupOrderDetCurrencyCode;
			this.SupOrderDetValidityPeriod = item.SupOrderDetValidityPeriod;
			this.SupOrderDetDeliveryTime = item.SupOrderDetDeliveryTime;
			this.SupOrderDetDelTerm = item.SupOrderDetDelTerm;
			this.SupOrderDetDelTermDesc = item.SupOrderDetDelTermDesc;
			this.SupOrderDetShipVia = item.SupOrderDetShipVia;
			this.SupOrderDetDeliveryPt = item.SupOrderDetDeliveryPt;
			this.SupOrderDetPaymentTerm = item.SupOrderDetPaymentTerm;
			this.SupOrderDetDesc = item.SupOrderDetDesc;
			this.SupOrderDetQuantity = item.SupOrderDetQuantity;
			this.SupOrderDetUM = item.SupOrderDetUM;
			this.SupOrderDetUnitCost = item.SupOrderDetUnitCost;
			this.SupOrderDetExtPrice = item.SupOrderDetExtPrice;
			this.SupOrderDetFXRate = item.SupOrderDetFXRate;
			this.SupOrderDetUnitCostBD = item.SupOrderDetUnitCostBD;
			this.SupOrderDetRemarks = item.SupOrderDetRemarks;
			this.SupOrderDetSelected = item.SupOrderDetSelected;
			this.SupOrderDetLowest = item.SupOrderDetLowest;
			this.SupOrderDetTotalAttach = item.SupOrderDetTotalAttach;

			this.SupOrderDetCreatedBy = item.SupOrderDetCreatedBy;
			this.SupOrderDetCreatedName = item.SupOrderDetCreatedName;
			this.SupOrderDetCreatedDate = item.SupOrderDetCreatedDate;
			this.SupOrderDetModifiedBy = item.SupOrderDetModifiedBy;
			this.SupOrderDetModifiedName = item.SupOrderDetModifiedName;
			this.SupOrderDetModifiedDate = item.SupOrderDetModifiedDate;
		}
		#endregion

		#region Override Methods
		public override void AssignItem(DataRow row)
		{
		}
		#endregion
	}
}
