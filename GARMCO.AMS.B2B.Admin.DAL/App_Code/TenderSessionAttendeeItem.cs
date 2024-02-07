using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GARMCO.AMS.B2B.Admin.DAL;
using GARMCO.Common.Object;

namespace GARMCO.AMS.B2B.Admin.DAL
{
	[Serializable]
	sealed public class TenderSessionAttendeeItem : ObjectItem
	{
		#region Properties
		public int TSAttID { get; set; }
		public int TSAttTSID { get; set; }
		public int TSAttEmpNo { get; set; }
		public string TSAttEmpName { get; set; }
		public string TSAttEmpEmail { get; set; }
		public string TSAttEmpUsername { get; set; }
		public string TSAttEmpPosition { get; set; }
		public DateTime? TSAttDate { get; set; }
		public bool TSAttPresent { get; set; }
		public bool TSAttPrimary { get; set; }
		public bool TSAttTenderComm { get; set; }
		public bool TSAttSigned { get; set; }

		public int TSAttCreatedBy { get; set; }
		public string TSAttCreatedName { get; set; }
		public DateTime? TSAttCreatedDate { get; set; }
		public int TSAttModifiedBy { get; set; }
		public string TSAttModifiedName { get; set; }
		public DateTime? TSAttModifiedDate { get; set; }
		#endregion

		#region Constructors
		public TenderSessionAttendeeItem() :
			base()
		{
			this.TSAttID = 0;
			this.TSAttTSID = 0;
			this.TSAttEmpNo = 0;
			this.TSAttEmpName = String.Empty;
			this.TSAttEmpEmail = String.Empty;
			this.TSAttEmpUsername = String.Empty;
			this.TSAttEmpPosition = String.Empty;
			this.TSAttDate = DateTime.Now;
			this.TSAttPresent = false;
			this.TSAttPrimary = false;
			this.TSAttTenderComm = false;
			this.TSAttSigned = false;

			this.TSAttCreatedBy = 0;
			this.TSAttCreatedName = String.Empty;
			this.TSAttCreatedDate = DateTime.Now;
			this.TSAttModifiedBy = 0;
			this.TSAttModifiedName = String.Empty;
			this.TSAttModifiedDate = DateTime.Now;
		}

		public TenderSessionAttendeeItem(DataRow row) :
			this()
		{
			this.AssignItem(row);
		}

		public TenderSessionAttendeeItem(TenderSessionAttendee attendee) :
			this()
		{
			this.TSAttID = attendee.TSAttID;
			this.TSAttTSID = attendee.TSAttTSID;
			this.TSAttEmpNo = attendee.TSAttEmpNo;
			this.TSAttEmpName = attendee.TSAttName;
			this.TSAttEmpEmail = attendee.TSAttEmail;
			this.TSAttEmpUsername = attendee.TSAttUsername;
			this.TSAttEmpPosition = attendee.TSAttEmpPosition;
			this.TSAttDate = attendee.TSAttDate;
			this.TSAttPresent = attendee.TSAttPresent;
			this.TSAttPrimary = attendee.TSAttPrimary;
			this.TSAttTenderComm = attendee.TSAttTenderComm;
			this.TSAttSigned = attendee.TSAttSigned;

			this.TSAttCreatedBy = attendee.TSAttCreatedBy;
			this.TSAttCreatedName = attendee.TSAttCreatedName;
			if (attendee.TSAttCreatedDate.HasValue)
				this.TSAttCreatedDate = attendee.TSAttCreatedDate;
			this.TSAttModifiedBy = attendee.TSAttModifiedBy;
			this.TSAttModifiedName = attendee.TSAttModifiedName;
			if (!attendee.TSAttModifiedDate.HasValue)
				this.TSAttModifiedDate = attendee.TSAttModifiedDate;
		}
		#endregion

		#region Override Methods
		public override void AssignItem(DataRow row)
		{
		}
		#endregion
	}
}
