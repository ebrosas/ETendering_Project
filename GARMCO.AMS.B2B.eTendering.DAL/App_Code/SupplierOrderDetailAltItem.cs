using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GARMCO.Common.Object;

namespace GARMCO.AMS.B2B.eTendering.DAL
{
	[Serializable]
	sealed public class SupplierOrderDetailAltItem : ObjectItem
	{
		#region Properties
		public int? SODAltID { get; set; }
		public int? SODAltSODID { get; set; }
		public double? SODAltLineNo { get; set; }
		public string SODAltDesc { get; set; }
		public string SODAltSupplierCode { get; set; }
		public string SODAltManufacturerCode { get; set; }
		public double? SODAltQuantity { get; set; }
		public string SODAltUM { get; set; }
		public double? SODAltUnitCost { get; set; }
		public double? SODAltExtPrice { get; set; }
		public double? SODAltUnitCostBD { get; set; }
		public double? SODAltTotalUnitCostBD
		{
			get
			{
				return (this.SODAltUnitCostBD * this.SODAltQuantity);
			}
		}
		public string SODAltCurrencyCode { get; set; }
		public string SODAltCurrencyCodeDesc { get; set; }
		public double? SODAltFXRate { get; set; }
		public DateTime? SODAltValidityPeriod { get; set; }
		public int? SODAltDeliveryTime { get; set; }
		public string SODAltDelTerm { get; set; }
		public int? SODAltShipVia { get; set; }
		public string SODAltDeliveryPt { get; set; }
		public bool? SODAltSelected { get; set; }
		public bool? SODAltLowest { get; set; }

		public int? SODAltCreatedBy { get; set; }
		public string SODAltCreatedName { get; set; }
		public DateTime? SODAltCreatedDate { get; set; }
		public int? SODAltModifiedBy { get; set; }
		public string SODAltModifiedName { get; set; }
		public DateTime? SODAltModifiedDate { get; set; }

		public List<SupplierOrderAttachItem> SupplierOrderAlternativeAttachList { get; set; }
		private int _totalSupplierOrderAlternativeAttachments = 0;
		public int TotalSupplierOrderAlternativeAttachments
		{
			get
			{
				int total = this._totalSupplierOrderAlternativeAttachments;
				if (this.SupplierOrderAlternativeAttachList != null && this.SODAltAttachDownloaded)
					total = this.SupplierOrderAlternativeAttachList.Count(tempItem => !tempItem.MarkForDeletion);

				return total;
			}
		}

		public bool SODAltAttachDownloaded { get; set; }
		#endregion

		#region Constructors
		public SupplierOrderDetailAltItem() :
			base()
		{
			this.SODAltID = 0;
			this.SODAltSODID = 0;
			this.SODAltLineNo = 0;
			this.SODAltDesc = String.Empty;
			this.SODAltSupplierCode = String.Empty;
			this.SODAltManufacturerCode = String.Empty;
			this.SODAltQuantity = 0;
			this.SODAltUM = String.Empty;
			this.SODAltUnitCost = 0;
			this.SODAltExtPrice = 0;
			this.SODAltUnitCostBD = 0;
			this.SODAltCurrencyCode = String.Empty;
			this.SODAltCurrencyCodeDesc = String.Empty;
			this.SODAltFXRate = 0;
			this.SODAltValidityPeriod = null;
			this.SODAltDeliveryTime = 0;
			this.SODAltDelTerm = String.Empty;
			this.SODAltShipVia = 0;
			this.SODAltDeliveryPt = String.Empty;
			this.SODAltSelected = false;
			this.SODAltLowest = false;

			this.SODAltCreatedBy = 0;
			this.SODAltCreatedName = String.Empty;
			this.SODAltCreatedDate = DateTime.Now;
			this.SODAltModifiedBy = 0;
			this.SODAltModifiedName = String.Empty;
			this.SODAltModifiedDate = DateTime.Now;

			this.SupplierOrderAlternativeAttachList = new List<SupplierOrderAttachItem>();
			this.SODAltAttachDownloaded = false;
		}

		public SupplierOrderDetailAltItem(DataRow row) :
			this()
		{
			this.AssignItem(row);
		}

		public SupplierOrderDetailAltItem(SupplierOrderAlternative alternative) :
			this()
		{
			this.SODAltID = alternative.SODAltID;
			this.SODAltSODID = alternative.SODAltSODID;
			this.SODAltLineNo = alternative.SODAltLineNo;
			this.SODAltDesc = alternative.SODAltDesc;
			this.SODAltSupplierCode = alternative.SODAltSupplierCode;
			this.SODAltManufacturerCode = alternative.SODAltManufacturerCode;
			this.SODAltQuantity = alternative.SODAltQuantity;
			this.SODAltUM = alternative.SODAltUM;
			this.SODAltUnitCost = alternative.SODAltUnitCost;
			this.SODAltExtPrice = alternative.SODAltExtPrice;
			this.SODAltUnitCostBD = alternative.SODAltUnitCostBD;
			this.SODAltCurrencyCode = alternative.SODAltCurrencyCode;
			this.SODAltCurrencyCodeDesc = alternative.SODAltCurrencyCodeDesc;
			this.SODAltFXRate = alternative.SODAltFXRate;
			this.SODAltValidityPeriod = alternative.SODAltValidityPeriod;
			this.SODAltDeliveryTime = alternative.SODAltDeliveryTime;
			this.SODAltDelTerm = alternative.SODAltDelTerm;
			this.SODAltShipVia = alternative.SODAltShipVia;
			this.SODAltDeliveryPt = alternative.SODAltDeliveryPt;
			this.SODAltSelected = alternative.SODAltSelected;
			this.SODAltLowest = alternative.SODAltLowest;

			this.SODAltCreatedBy = alternative.SODAltCreatedBy;
			this.SODAltCreatedName = alternative.SODAltCreatedName;
			this.SODAltCreatedDate = !alternative.SODAltCreatedDate.HasValue ? default(DateTime?) : alternative.SODAltCreatedDate.Value;
			this.SODAltModifiedBy = alternative.SODAltModifiedBy;
			this.SODAltModifiedName = alternative.SODAltModifiedName;
			this.SODAltModifiedDate = !alternative.SODAltModifiedDate.HasValue ? default(DateTime?) : alternative.SODAltModifiedDate.Value;

			this._totalSupplierOrderAlternativeAttachments = alternative.SODAltTotalAttachment;
		}

		public SupplierOrderDetailAltItem(SupplierOrderDetailAltItem item) :
			this()
		{
			this.Added = item.Added;
			this.Modified = item.Modified;
			this.MarkForDeletion = item.MarkForDeletion;

			this.SODAltID = item.SODAltID;
			this.SODAltSODID = item.SODAltSODID;
			this.SODAltLineNo = item.SODAltLineNo;
			this.SODAltDesc = item.SODAltDesc;
			this.SODAltSupplierCode = item.SODAltSupplierCode;
			this.SODAltManufacturerCode = item.SODAltManufacturerCode;
			this.SODAltQuantity = item.SODAltQuantity;
			this.SODAltUM = item.SODAltUM;
			this.SODAltUnitCost = item.SODAltUnitCost;
			this.SODAltExtPrice = item.SODAltExtPrice;
			this.SODAltUnitCostBD = item.SODAltUnitCostBD;
			this.SODAltCurrencyCode = item.SODAltCurrencyCode;
			this.SODAltCurrencyCodeDesc = item.SODAltCurrencyCodeDesc;
			this.SODAltFXRate = item.SODAltFXRate;
			this.SODAltValidityPeriod = item.SODAltValidityPeriod;
			this.SODAltDeliveryTime = item.SODAltDeliveryTime;
			this.SODAltDelTerm = item.SODAltDelTerm;
			this.SODAltShipVia = item.SODAltShipVia;
			this.SODAltDeliveryPt = item.SODAltDeliveryPt;
			this.SODAltSelected = item.SODAltSelected;
			this.SODAltLowest = item.SODAltLowest;

			this._totalSupplierOrderAlternativeAttachments = item._totalSupplierOrderAlternativeAttachments;

			this.SODAltCreatedBy = item.SODAltCreatedBy;
			this.SODAltCreatedName = item.SODAltCreatedName;
			this.SODAltCreatedDate = item.SODAltCreatedDate;
			this.SODAltModifiedBy = item.SODAltModifiedBy;
			this.SODAltModifiedName = item.SODAltModifiedName;
			this.SODAltModifiedDate = item.SODAltModifiedDate;

			// Copies all attachments
			this.SupplierOrderAlternativeAttachList.Clear();
			foreach (SupplierOrderAttachItem attachItem in item.SupplierOrderAlternativeAttachList)
				this.SupplierOrderAlternativeAttachList.Add(new SupplierOrderAttachItem(attachItem));
		}

		public SupplierOrderDetailAltItem(double? sodAltLineNo) :
			this()
		{
			this.Added = true;
			this.SODAltLineNo = sodAltLineNo;
		}

		public SupplierOrderDetailAltItem(double? sodAltLineNo, string sodAltUM, string sodAltCurrencyCode, string sodAltDelTerm,
			DateTime? sodAltValidityPeriod, int sodAltDeliveryTime, string sodAltDeliveryPt) :
			this()
		{
			this.Added = true;
			this.SODAltLineNo = sodAltLineNo;
			this.SODAltUM = sodAltUM;
			this.SODAltCurrencyCode = sodAltCurrencyCode;
			this.SODAltDelTerm = sodAltDelTerm;
			this.SODAltValidityPeriod = sodAltValidityPeriod;
			this.SODAltDeliveryTime = sodAltDeliveryTime;
			this.SODAltDeliveryPt = sodAltDeliveryPt;
		}
		#endregion

		#region Override Methods
		public override void AssignItem(DataRow row)
		{
		}
		#endregion

		#region Public Methods
		public void CopyAllAttachments(List<SupplierOrderAttachItem> list)
		{
			// Clears the current list
			this.SupplierOrderAlternativeAttachList.Clear();

			if (list != null)
			{

				foreach (SupplierOrderAttachItem item in list)
					this.SupplierOrderAlternativeAttachList.Add(new SupplierOrderAttachItem(item));

			}
		}
		#endregion
	}
}