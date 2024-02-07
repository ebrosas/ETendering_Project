using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GARMCO.AMS.B2B.Admin.DAL
{
	[Serializable]
	sealed public class PublishedRFQInvitedSupplierItem
	{
		#region Properties
		public double? SupInvJDERefNo { get; set; }
		public string SupInvSupplierName { get; set; }
		public int SupInvSupplierNo { get; set; }
		public bool SupInvRegistered { get; set; }

		public string SupInvSupplierAddr { get; set; }
		public string SupInvSupplierCity { get; set; }
		public string SupInvSupplierState { get; set; }
		public string SupInvSupplierCountry { get; set; }
		public string SupInvSupplierPostalCode { get; set; }

		public List<PublishedRFQInvitedSupplierContactItem> SupInvContactList { get; set; }
		public List<PublishedRFQDetailItem> RFQDetailItemList { get; set; }
		#endregion

		#region Constructors
		public PublishedRFQInvitedSupplierItem()
		{
			this.SupInvJDERefNo = 0;
			this.SupInvSupplierName = String.Empty;
			this.SupInvSupplierNo = 0;
			this.SupInvRegistered = false;

			this.SupInvSupplierAddr = String.Empty;
			this.SupInvSupplierCity = String.Empty;
			this.SupInvSupplierState = String.Empty;
			this.SupInvSupplierCountry = String.Empty;
			this.SupInvSupplierPostalCode = String.Empty;

			this.SupInvContactList = new List<PublishedRFQInvitedSupplierContactItem>();
			this.RFQDetailItemList = new List<PublishedRFQDetailItem>();
		}

		public PublishedRFQInvitedSupplierItem(PublishedRFQInvitedSupplierItem item)
		{
			this.SupInvJDERefNo = item.SupInvJDERefNo;
			this.SupInvSupplierName = item.SupInvSupplierName;
			this.SupInvSupplierNo = item.SupInvSupplierNo;
			this.SupInvRegistered = item.SupInvRegistered;

			this.SupInvSupplierAddr = item.SupInvSupplierAddr;
			this.SupInvSupplierCity = item.SupInvSupplierCity;
			this.SupInvSupplierState = item.SupInvSupplierState;
			this.SupInvSupplierCountry = item.SupInvSupplierCountry;
			this.SupInvSupplierPostalCode = item.SupInvSupplierPostalCode;

			this.RFQDetailItemList = new List<PublishedRFQDetailItem>();

			this.SupInvContactList = new List<PublishedRFQInvitedSupplierContactItem>();
			// Adds each contacts
			foreach (PublishedRFQInvitedSupplierContactItem contactItem in item.SupInvContactList)
				this.SupInvContactList.Add(new PublishedRFQInvitedSupplierContactItem(contactItem));
		}

		public PublishedRFQInvitedSupplierItem(PublishedRFQInvitedSupplier row)
		{
			this.SupInvJDERefNo = row.SupInvJDERefNo;
			this.SupInvSupplierName = row.SupInvSupplierName;
			this.SupInvSupplierNo = row.SupInvSupplierNo;
			this.SupInvSupplierAddr = row.SupInvSupplierAddress;
			this.SupInvSupplierCity = row.SupInvSupplierCity;
			this.SupInvSupplierState = row.SupInvSupplierState;
			this.SupInvSupplierCountry = row.SupInvSupplierCountry;
			this.SupInvSupplierPostalCode = row.SupInvSupplierPostalCode;
			this.SupInvRegistered = row.SupInvRegistered;

			this.SupInvContactList = new List<PublishedRFQInvitedSupplierContactItem>();
			this.RFQDetailItemList = new List<PublishedRFQDetailItem>();
		}
		#endregion
	}
}
