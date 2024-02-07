using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GARMCO.AMS.B2B.Utility;

namespace GARMCO.AMS.B2B.Admin.DAL
{
	[Serializable]
	sealed public class PublishedRFQDetailAttachItem
	{
		#region Properties
		public string MediaObjName { get; set; }
		public string MediaKey { get; set; }
		public decimal MediaSeqNo { get; set; }
		public B2BConstants.MediaObjectType MediaType { get; set; }
		public string MediaDocName { get; set; }
		public string MediaFolderName { get; set; }
		public string MediaFilename { get; set; }
		public string MediaRtf { get; set; }
		public string MediaHtml { get; set; }
		public string MediaPlainText
		{
			get
			{
				string plainText = String.Empty;

				// To handle some media objects that are not in RTF format but just plain text
				try
				{
					RichTextBox txtRtf = new RichTextBox();
					txtRtf.Rtf = this.MediaRtf;
					plainText = txtRtf.Text;
				}

				catch
				{
					if (String.IsNullOrEmpty(this.MediaHtml))
						plainText = this.MediaRtf.Replace("\0", String.Empty);

					else
						plainText = this.MediaHtml;
				}

				return plainText;
			}
		}
		#endregion

		#region Constructors
		public PublishedRFQDetailAttachItem()
		{
			this.MediaObjName = String.Empty;
			this.MediaKey = String.Empty;
			this.MediaSeqNo = 0;
			this.MediaType = B2BConstants.MediaObjectType.NoMediaObjType;
			this.MediaDocName = String.Empty;
			this.MediaFolderName = String.Empty;
			this.MediaFilename = String.Empty;
			this.MediaRtf = String.Empty;
			this.MediaHtml = String.Empty;
		}

		public PublishedRFQDetailAttachItem(PublishedRfqDetailAttachment row)
		{
			this.MediaObjName = row.MediaObjName;
			this.MediaKey = row.MediaKey;
			this.MediaSeqNo = row.MediaSeqNo;
			this.MediaType = (B2BConstants.MediaObjectType)row.MediaType;
			this.MediaDocName = row.MediaDocName;
			this.MediaFolderName = row.MediaFolderName;
			this.MediaFilename = row.MediaFilename;
			this.MediaRtf = Encoding.Unicode.GetString(row.MediaText);
		}

		public PublishedRFQDetailAttachItem(DetailAttachment detailAttachment)
		{
			this.MediaObjName = String.Empty;
			this.MediaKey = String.Empty;
			this.MediaSeqNo = detailAttachment.OrderAttachSeq;
			this.MediaType = (B2BConstants.MediaObjectType)detailAttachment.OrderAttachType;
			this.MediaDocName = detailAttachment.OrderAttachDisplayName;
			this.MediaFolderName = String.Empty;
			this.MediaFilename = detailAttachment.OrderAttachFilename;
			this.MediaRtf = Encoding.Unicode.GetString(detailAttachment.OrderAttachContent);
		}
		#endregion
	}
}
