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
	sealed public class SupplierOrderOtherChargeItem : ObjectItem
	{
		#region Properties
		public int? OtherChgID { get; set; }
		public int? OtherChgSupOrderID { get; set; }
		public string OtherChgDesc { get; set; }
		public string OtherChgCurrencyCode { get; set; }
		public string OtherChgCurrencyCodeDesc { get; set; }
		public double? OtherChgAmount { get; set; }
		public double? OtherChgAmountBD { get; set; }
		public double? OtherChgFXRate { get; set; }
		public bool? OtherChgSelected { get; set; }

		public int? OtherChgCreatedBy { get; set; }
		public string OtherChgCreatedName { get; set; }
		public DateTime? OtherChgCreatedDate { get; set; }
		public int? OtherChgModifiedBy { get; set; }
		public string OtherChgModifiedName { get; set; }
		public DateTime? OtherChgModifiedDate { get; set; }
		#endregion

		#region Constructors
		public SupplierOrderOtherChargeItem() :
			base()
		{
			this.OtherChgID = 0;
			this.OtherChgSupOrderID = 0;
			this.OtherChgDesc = String.Empty;
			this.OtherChgCurrencyCode = String.Empty;
			this.OtherChgCurrencyCodeDesc = String.Empty;
			this.OtherChgAmount = 0;
			this.OtherChgAmountBD = 0;
			this.OtherChgFXRate = 0;
			this.OtherChgSelected = false;

			this.OtherChgCreatedBy = 0;
			this.OtherChgCreatedName = String.Empty;
			this.OtherChgCreatedDate = DateTime.Now;
			this.OtherChgModifiedBy = 0;
			this.OtherChgModifiedName = String.Empty;
			this.OtherChgModifiedDate = DateTime.Now;
		}

		public SupplierOrderOtherChargeItem(DataRow row) :
			this()
		{
			this.AssignItem(row);
		}

		public SupplierOrderOtherChargeItem(SupplierOrderCharge charge) :
			this()
		{
			this.OtherChgID = charge.OtherChgID;
			this.OtherChgSupOrderID = charge.OtherChgSupOrderID;
			this.OtherChgDesc = charge.OtherChgDesc;
			this.OtherChgCurrencyCode = charge.OtherChgCurrencyCode;
			this.OtherChgCurrencyCodeDesc = charge.OtherChgCurrencyCodeDesc;
			this.OtherChgAmount = charge.OtherChgAmount;
			this.OtherChgAmountBD = charge.OtherChgAmountBD;
			this.OtherChgFXRate = charge.OtherChgFXRate;
			this.OtherChgSelected = charge.OtherChgSelected;

			this.OtherChgCreatedBy = charge.OtherChgCreatedBy;
			this.OtherChgCreatedName = charge.OtherChgCreatedName;
			this.OtherChgCreatedDate = !charge.OtherChgCreatedDate.HasValue ? default(DateTime?) : charge.OtherChgCreatedDate.Value;
			this.OtherChgModifiedBy = charge.OtherChgModifiedBy;
			this.OtherChgModifiedName = charge.OtherChgModifiedName;
			this.OtherChgModifiedDate = !charge.OtherChgModifiedDate.HasValue ? default(DateTime?) : charge.OtherChgModifiedDate.Value;
		}

		public SupplierOrderOtherChargeItem(string currencyCode, string currencyDesc) :
			this()
		{
			this.Added = true;
			this.OtherChgCurrencyCode = currencyCode;
			this.OtherChgCurrencyCodeDesc = currencyDesc;
		}
		#endregion

		#region Override Methods
		public override void AssignItem(DataRow row)
		{
		}
		#endregion

		#region Public Methods
		public bool IsFound(string otherChgDesc)
		{
			return this.OtherChgDesc.ToLower().Equals(otherChgDesc.Trim().ToLower());
		}
		#endregion
	}
}
