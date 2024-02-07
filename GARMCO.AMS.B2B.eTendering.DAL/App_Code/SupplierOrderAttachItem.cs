using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GARMCO.AMS.B2B.Utility;
using GARMCO.Common.Object;
using SautinSoft;

namespace GARMCO.AMS.B2B.eTendering.DAL
{
	[Serializable]
	sealed public class SupplierOrderAttachItem : ObjectItem
	{
		#region Properties
		public double? OrderAttachNo { get; set; }
		public double? OrderAttachLineNo { get; set; }
		public decimal? OrderAttachSeq { get; set; }
		public bool? OrderAttachSupplier { get; set; }
		public int? OrderAttachSODID { get; set; }
		public int? OrderAttachSODAltID { get; set; }
		public B2BConstants.MediaObjectType OrderAttachType { get; set; }
		public string OrderAttachDisplayName { get; set; }
		public string OrderAttachFilename { get; set; }
		public string OrderAttachContentRtf { get; set; }
		public string OrderAttachContentHtml { get; set; }
		public string OrderAttachContentText
		{
			get
			{
				string plainText = String.Empty;

				// To handle some media objects that are not in RTF format but just plain text
				try
				{
					RichTextBox txtRtf = new RichTextBox();
					txtRtf.Rtf = this.OrderAttachContentRtf;
					plainText = txtRtf.Text;
				}

				catch
				{
					if (String.IsNullOrEmpty(this.OrderAttachContentHtml))
						plainText = this.OrderAttachContentRtf.Replace("\0", String.Empty);

					else
						plainText = this.OrderAttachContentHtml;
				}

				return plainText;
			}
		}

		public int? OrderAttachCreatedBy { get; set; }
		public string OrderAttachCreatedName  { get; set; }
		public DateTime? OrderAttachCreatedDate { get; set; }
		public int? OrderAttachModifiedBy { get; set; }
		public string OrderAttachModifiedName { get; set; }
		public DateTime? OrderAttachModifiedDate { get; set; }

		public bool IsMediaObjectConvertHtmlOK { get; set; }
		#endregion

		#region Constructors
		public SupplierOrderAttachItem() :
			base()
		{
			this.OrderAttachNo = 0;
			this.OrderAttachLineNo = 0;
			this.OrderAttachSeq = 1;
			this.OrderAttachSODID = -1;
			this.OrderAttachSODAltID = -1;
			this.OrderAttachType = B2BConstants.MediaObjectType.Text;
			this.OrderAttachDisplayName = String.Empty;
			this.OrderAttachFilename = String.Empty;
			this.OrderAttachContentRtf = String.Empty;
			this.OrderAttachContentHtml = String.Empty;

			this.OrderAttachCreatedBy = 0;
			this.OrderAttachCreatedName = String.Empty;
			this.OrderAttachCreatedDate = DateTime.Now;
			this.OrderAttachModifiedBy = 0;
			this.OrderAttachModifiedName = String.Empty;
			this.OrderAttachModifiedDate = DateTime.Now;

			this.IsMediaObjectConvertHtmlOK = false;
		}

		public SupplierOrderAttachItem(DataRow row) :
			this()
		{
			this.AssignItem(row);
		}

		public SupplierOrderAttachItem(SupplierOrderAttachment attachment) :
			this()
		{
			this.OrderAttachNo = attachment.OrderAttachNo;
			this.OrderAttachLineNo = attachment.OrderAttachLineNo;
			this.OrderAttachSeq = attachment.OrderAttachSeq;
			this.OrderAttachSupplier = attachment.OrderAttachSupplier;
			this.OrderAttachSODID = attachment.OrderAttachSODID;
			this.OrderAttachSODAltID = attachment.OrderAttachSODAltID;
			this.OrderAttachType = (B2BConstants.MediaObjectType)attachment.OrderAttachType;
			this.OrderAttachDisplayName = attachment.OrderAttachDisplayName;
			this.OrderAttachFilename = attachment.OrderAttachFilename;
			this.OrderAttachContentRtf = Encoding.Unicode.GetString(attachment.OrderAttachContent);

			this.OrderAttachCreatedBy = attachment.OrderAttachCreatedBy;
			this.OrderAttachCreatedName = attachment.OrderAttachCreatedName;
			this.OrderAttachCreatedDate = !attachment.OrderAttachCreatedDate.HasValue ? default(DateTime?) : attachment.OrderAttachCreatedDate.Value;
			this.OrderAttachModifiedBy = attachment.OrderAttachModifiedBy;
			this.OrderAttachModifiedName = attachment.OrderAttachModifiedName;
			this.OrderAttachModifiedDate = !attachment.OrderAttachModifiedDate.HasValue ? default(DateTime?) : attachment.OrderAttachModifiedDate.Value;
		}

		public SupplierOrderAttachItem(SupplierOrderAttachItem item)
		{
			this.Added = item.Added;
			this.Modified = item.Modified;
			this.MarkForDeletion = item.MarkForDeletion;

			this.OrderAttachNo = item.OrderAttachNo;
			this.OrderAttachLineNo = item.OrderAttachLineNo;
			this.OrderAttachSeq = item.OrderAttachSeq;
			this.OrderAttachSupplier = item.OrderAttachSupplier;
			this.OrderAttachSODID = item.OrderAttachSODID;
			this.OrderAttachSODAltID = item.OrderAttachSODAltID;
			this.OrderAttachType = item.OrderAttachType;
			this.OrderAttachDisplayName = item.OrderAttachDisplayName;
			this.OrderAttachFilename = item.OrderAttachFilename;
			this.OrderAttachContentRtf = item.OrderAttachContentRtf;
			this.OrderAttachContentHtml = item.OrderAttachContentHtml;

			this.OrderAttachCreatedBy = item.OrderAttachCreatedBy;
			this.OrderAttachCreatedName = item.OrderAttachCreatedName;
			this.OrderAttachCreatedDate = item.OrderAttachCreatedDate;
			this.OrderAttachModifiedBy = item.OrderAttachModifiedBy;
			this.OrderAttachModifiedName = item.OrderAttachModifiedName;
			this.OrderAttachModifiedDate = item.OrderAttachModifiedDate;

			this.IsMediaObjectConvertHtmlOK = item.IsMediaObjectConvertHtmlOK;
		}
		#endregion

		#region Override Methods
		public override void AssignItem(DataRow row)
		{
		}
		#endregion

		#region Public Methods
		public string GetMediaObjectTextRtf(string htmlKey)
		{
			HtmlToRtf h = new HtmlToRtf();
			h.Serial = htmlKey;

			this.OrderAttachContentRtf = h.ConvertString(this.OrderAttachContentHtml);

			return this.OrderAttachContentRtf;
		}

		public string GetMediaObjectTextHtml(string rtfKey)
		{
			this.IsMediaObjectConvertHtmlOK = true;

			RtfToHtml r = new RtfToHtml();
			r.Serial = rtfKey;

			this.OrderAttachContentHtml = r.ConvertString(this.OrderAttachContentRtf);

			return this.OrderAttachContentHtml;
		}

		public void SetSupplierOrderAttachItem(SupplierOrderAttachItem item)
		{
			this.Added = item.Added;
			this.Modified = item.Modified;
			this.MarkForDeletion = item.MarkForDeletion;

			this.OrderAttachNo = item.OrderAttachNo;
			this.OrderAttachLineNo = item.OrderAttachLineNo;
			this.OrderAttachSeq = item.OrderAttachSeq;
			this.OrderAttachSupplier = item.OrderAttachSupplier;
			this.OrderAttachSODID = item.OrderAttachSODID;
			this.OrderAttachSODAltID = item.OrderAttachSODAltID;
			this.OrderAttachType = item.OrderAttachType;
			this.OrderAttachDisplayName = item.OrderAttachDisplayName;
			this.OrderAttachFilename = item.OrderAttachFilename;
			this.OrderAttachContentRtf = item.OrderAttachContentRtf;
			this.OrderAttachContentHtml = item.OrderAttachContentHtml;

			this.OrderAttachCreatedBy = item.OrderAttachCreatedBy;
			this.OrderAttachCreatedName = item.OrderAttachCreatedName;
			this.OrderAttachCreatedDate = item.OrderAttachCreatedDate;
			this.OrderAttachModifiedBy = item.OrderAttachModifiedBy;
			this.OrderAttachModifiedName = item.OrderAttachModifiedName;
			this.OrderAttachModifiedDate = item.OrderAttachModifiedDate;

			this.IsMediaObjectConvertHtmlOK = item.IsMediaObjectConvertHtmlOK;
		}
		#endregion
	}
}
