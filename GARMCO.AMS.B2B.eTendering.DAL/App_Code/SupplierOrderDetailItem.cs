using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GARMCO.AMS.B2B.Utility;
using GARMCO.Common.Object;

namespace GARMCO.AMS.B2B.eTendering.DAL
{
	[Serializable]
	sealed public class SupplierOrderDetailItem : ObjectItem
	{
		#region Properties
		public int? SupOrderSupplierNo { get; set; }
		public int? SODID { get; set; }
		public int? SODSupOrderID { get; set; }
		public double? SODOrderLineNo { get; set; }
		public string SODSupplierCode { get; set; }
		public string SODManufacturerCode { get; set; }
		public double? SODQuantity { get; set; }
		public string SODUM { get; set; }
		public double? SODUnitCost { get; set; }
		public double? SODExtPrice { get; set; }
		public double? SODUnitCostBD { get; set; }
		public double? SODFXRate { get; set; }
		public double? SODTotalUnitCostBD
		{
			get
			{
                double? quantity = 1d;
                if (this.OrderDetQuantity.HasValue && this.OrderDetQuantity.Value != 0d)
                {
                    quantity = this.OrderDetQuantity.Value;
                }
				return (this.SODUnitCostBD * quantity);
			}
		}
		public string SODRemarks { get; set; }
		public bool? SODSelected { get; set; }
		public bool? SODLowest { get; set; }
		//public int? SODTotalAttachment { get; set; }

		public int? SODCreatedBy { get; set; }
		public string SODCreatedName { get; set; }
		public DateTime? SODCreatedDate { get; set; }
		public int? SODModifiedBy { get; set; }
		public string SODModifiedName { get; set; }
		public DateTime? SODModifiedDate { get; set; }

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
		public string OrderDetStatusDesc { get; set; }
		public int? OrderDetTotalAttachmentText { get; set; }
		public int? OrderDetTotalAttachmentFile { get; set; }

		public List<SupplierOrderAttachItem> OrderDetAttachList { get; set; }
		public string OrderDetFullDescriptionHtml
		{
			get
			{
				StringBuilder description = new StringBuilder();

				// Adds the original description
				description.Append(this.OrderDetDesc);

				#region Add the media objects
				foreach (SupplierOrderAttachItem attachItem in this.OrderDetAttachList)
				{

					if (!attachItem.MarkForDeletion &&
						attachItem.OrderAttachType == B2BConstants.MediaObjectType.Text)
					{

						description.Append("\r\n");
						description.Append(attachItem.OrderAttachContentText);

					}
				}
				#endregion

				return description.ToString();
			}
		}

		public List<SupplierOrderDetailAltItem> SupplierOrderDetAltList { get; set; }
		private int _totalSupplierOrderDetAlternatives = 0;
		public int TotalSupplierOrderDetAlternatives
		{
			get
			{
				int total = this._totalSupplierOrderDetAlternatives;
				if (this.SupplierOrderDetAltList != null && this.SODAlternativeDownloaded)
					total = this.SupplierOrderDetAltList.Count(tempItem => !tempItem.MarkForDeletion);

				return total;
			}
		}

		public List<SupplierOrderAttachItem> SupplierOrderDetAttachList { get; set; }
		private int _totalSupplierOrderDetAttachments = 0;
		public int TotalSupplierOrderDetAttachments
		{
			get
			{
				int total = this._totalSupplierOrderDetAttachments;
				if (this.SupplierOrderDetAttachList != null && this.SODAttachDownloaded)
					total = this.SupplierOrderDetAttachList.Count(tempItem => !tempItem.MarkForDeletion);

				return total;
			}
		}

		public bool OrderDetAttachDownloaded { get; set; }
		public bool SODAlternativeDownloaded { get; set; }
		public bool SODAttachDownloaded { get; set; }
		#endregion

		#region Constructors
		public SupplierOrderDetailItem() :
			base()
		{
			this.SupOrderSupplierNo = 0;
			this.SODID = 0;
			this.SODSupOrderID = 0;
			this.SODSupplierCode = String.Empty;
			this.SODManufacturerCode = String.Empty;
			this.SODQuantity = 0;
			this.SODUM = String.Empty;
			this.SODUnitCost = 0;
			this.SODExtPrice = 0;
			this.SODUnitCostBD = 0;
			this.SODFXRate = 0;
			this.SODRemarks = String.Empty;
			this.SODSelected = false;
			this.SODLowest = false;
			//this.SODTotalAttachment = 0;

			this.SODCreatedBy = 0;
			this.SODCreatedName = String.Empty;
			this.SODCreatedDate = DateTime.Now;
			this.SODModifiedBy = 0;
			this.SODModifiedName = String.Empty;
			this.SODModifiedDate = DateTime.Now;

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
			this.OrderDetStatusDesc = String.Empty;
			this.OrderDetTotalAttachmentText = 0;
			this.OrderDetTotalAttachmentFile = 0;

			this.OrderDetAttachList = new List<SupplierOrderAttachItem>();
			this.SupplierOrderDetAltList = new List<SupplierOrderDetailAltItem>();
			this.SupplierOrderDetAttachList = new List<SupplierOrderAttachItem>();

			this.OrderDetAttachDownloaded = false;
			this.SODAlternativeDownloaded = false;
			this.SODAttachDownloaded = false;
		}

		public SupplierOrderDetailItem(DataRow row) :
			this()
		{
			this.AssignItem(row);
		}

		public SupplierOrderDetailItem(SupplierOrderDetail sodRow)
		{
			this.SupOrderSupplierNo = sodRow.SupOrderSupplierNo;
			this.SODID = sodRow.SODID;
			this.SODSupOrderID = sodRow.SODSupOrderID;
			this.SODSupplierCode = sodRow.SODSupplierCode;
			this.SODManufacturerCode = sodRow.SODManufacturerCode;
			this.SODQuantity = sodRow.SODQuantity;
			this.SODUM = sodRow.SODUM;
			this.SODUnitCost = sodRow.SODUnitCost;
			this.SODExtPrice = sodRow.SODExtPrice;
			this.SODUnitCostBD = sodRow.SODUnitCostBD;
			this.SODFXRate = sodRow.SODFXRate;
			this.SODRemarks = sodRow.SODRemarks;
			this.SODSelected = sodRow.SODSelected;
			this.SODLowest = sodRow.SODLowest;
			//this.SODTotalAttachment = sodRow.SODTotalAttachment;

			this.SODCreatedBy = sodRow.SODCreatedBy;
			this.SODCreatedName = sodRow.SODCreatedName;
			this.SODCreatedDate = !sodRow.SODCreatedDate.HasValue ? default(DateTime?) : sodRow.SODCreatedDate.Value;
			this.SODModifiedBy = sodRow.SODModifiedBy;
			this.SODModifiedName = sodRow.SODModifiedName;
			this.SODModifiedDate = !sodRow.SODModifiedDate.HasValue ? default(DateTime?) : sodRow.SODModifiedDate.Value;

			this.OrderDetCompany = sodRow.OrderDetCompany;
			this.OrderDetNo = sodRow.OrderDetNo;
			this.OrderDetType = sodRow.OrderDetType;
			this.OrderDetSuffix = sodRow.OrderDetSuffix;
			this.OrderDetLineNo = sodRow.OrderDetLineNo;
			this.OrderDetLineType = sodRow.OrderDetLineType;
			this.OrderDetItemCode = sodRow.OrderDetItemCode;
			this.OrderDetQuantity = sodRow.OrderDetQuantity;
			this.OrderDetUM = sodRow.OrderDetUM;
			this.OrderDetUnitCost = sodRow.OrderDetUnitCost;
			this.OrderDetExtPrice = sodRow.OrderDetExtPrice;
			this.OrderDetForeignUnitCost = sodRow.OrderDetForeignUnitCost;
			this.OrderDetForeignExtPrice = sodRow.OrderDetForeignExtPrice;
			this.OrderDetCurrencyCode = sodRow.OrderDetCurrencyCode;
			this.OrderDetDesc = sodRow.OrderDetDesc;
			this.OrderDetUNSPSC = sodRow.OrderDetUNSPSC;
			this.OrderDetStockShortItemNo = sodRow.OrderDetStockShortItemNo;
			this.OrderDetStockLongItemNo = sodRow.OrderDetStockLongItemNo;
			this.OrderDetStatus = sodRow.OrderDetStatus;
			this.OrderDetStatusDesc = sodRow.OrderDetStatusDesc;
			this.OrderDetTotalAttachmentText = sodRow.OrderDetTotalAttachmentText;
			this.OrderDetTotalAttachmentFile = sodRow.OrderDetTotalAttachmentFile;

			this._totalSupplierOrderDetAttachments = sodRow.SODTotalAttachment;
			this._totalSupplierOrderDetAlternatives = sodRow.SODTotalAlternative;

			this.SupplierOrderDetAttachList = new List<SupplierOrderAttachItem>();
			this.SupplierOrderDetAltList = new List<SupplierOrderDetailAltItem>();
		}
		#endregion

		#region Override Methods
		public override void AssignItem(DataRow row)
		{
		}
		#endregion

		#region Public Methods
		public void ResetValues()
		{
			this.SODSupplierCode = String.Empty;
			this.SODManufacturerCode = String.Empty;
			this.SODQuantity = 0;
			this.SODUM = String.Empty;
			this.SODUnitCost = 0;
			this.SODExtPrice = 0;
			this.SODUnitCostBD = 0;
			this.SODFXRate = 0;
			this.SODRemarks = String.Empty;
			this.SODSelected = false;
			this.SODLowest = false;

			this.SupplierOrderDetAltList = new List<SupplierOrderDetailAltItem>();
			this.SupplierOrderDetAttachList = new List<SupplierOrderAttachItem>();
		}

		public void CopyAllAlternatives(List<SupplierOrderDetailAltItem> list)
		{
			// Clears the current list
			this.SupplierOrderDetAltList.Clear();

			if (list != null)
			{

				foreach (SupplierOrderDetailAltItem item in list)
					this.SupplierOrderDetAltList.Add(new SupplierOrderDetailAltItem(item));

			}
		}

		public void CopyAllAttachments(List<SupplierOrderAttachItem> list)
		{
			// Clears the current list
			this.SupplierOrderDetAttachList.Clear();

			if (list != null)
			{

				foreach (SupplierOrderAttachItem item in list)
					this.SupplierOrderDetAttachList.Add(new SupplierOrderAttachItem(item));

			}
		}
		#endregion
	}
}
