using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GARMCO.AMS.B2B.Admin.DAL
{
	[Serializable]
	sealed public class PublishedRFQInvitedSupplierContactItem
	{
		#region Properties
		public string SupInvSupplierContactName { get; set; }
		public string SupInvSupplierContactEmail { get; set; }
		public bool SupInvSupplierContactPrimary { get; set; }
		#endregion

		#region Constructors
		public PublishedRFQInvitedSupplierContactItem()
		{
			this.SupInvSupplierContactName = String.Empty;
			this.SupInvSupplierContactEmail = String.Empty;
			this.SupInvSupplierContactPrimary = false;
		}

		public PublishedRFQInvitedSupplierContactItem(PublishedRFQInvitedSupplierContactItem item)
		{
			this.SupInvSupplierContactName = item.SupInvSupplierContactName;
			this.SupInvSupplierContactEmail = item.SupInvSupplierContactEmail;
			this.SupInvSupplierContactPrimary = item.SupInvSupplierContactPrimary;
		}

		public PublishedRFQInvitedSupplierContactItem(
			string supInvSupplierContactName, string supInvSupplierContactEmail, bool supInvSupplierContactPrimary)
		{
			this.SupInvSupplierContactName = supInvSupplierContactName;
			this.SupInvSupplierContactEmail = supInvSupplierContactEmail;
			this.SupInvSupplierContactPrimary = supInvSupplierContactPrimary;
		}
		#endregion
	}
}
