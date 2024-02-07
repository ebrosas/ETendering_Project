using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GARMCO.AMS.B2B.Admin.DAL;
using GARMCO.AMS.B2B.Utility;
using GARMCO.AMS.Common.Web;
using Telerik.Web.UI;

namespace GARMCO.AMS.B2B.eTendering.Website.CommonObject
{
	public partial class RFQDetailAttachment : BaseWebForm
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{

				#region Sets the parameters and retrieve the attachments
				this.hidOrderNo.Value = Request.QueryString["orderDetNo"];
				this.hidOrderLineNo.Value = Request.QueryString["orderDetLineNo"];
				this.hidOrderAttachSODID.Value = Request.QueryString["orderAttachSODID"];
				this.hidOrderAttachSODAltID.Value = Request.QueryString["orderAttachSODAltID"];
				this.hidOrderSupplier.Value = Request.QueryString["orderDetSupplier"];

				this.objOrderDetAttach.Select();
				#endregion

			}
		}

		#region Override Methods
		protected override void OnInit(EventArgs e)
		{
			// Don't check the session
			this.IsToCheckSession = false;

			if (!this.IsPostBack)
			{

				// Set the attachment type
				if (!String.IsNullOrEmpty(Request.QueryString["orderDetAttachType"]))
					this.objOrderDetAttach.SelectParameters["orderAttachType"].DefaultValue = Request.QueryString["orderDetAttachType"];

			}

			base.OnInit(e);
		}
		#endregion

		#region Data Binding
		protected void lstAttachment_ItemDataBound(object sender, RadListViewItemEventArgs e)
		{
			if (e.Item.ItemType == RadListViewItemType.AlternatingItem || e.Item.ItemType == RadListViewItemType.DataItem)
			{

				RadListViewDataItem item = e.Item as RadListViewDataItem;
				if (item != null)
				{

					LinkButton lnkAttachDisplayName = item.FindControl("lnkAttachDisplayName") as LinkButton;
					lnkAttachDisplayName.Attributes.Add("attachSeq", (item.FindControl("litOrderAttachSeq") as Literal).Text);

					HyperLink lnkAttachFilename = item.FindControl("lnkAttachFilename") as HyperLink;

					// Checks the attachment type
					B2BConstants.MediaObjectType attachType = (B2BConstants.MediaObjectType)Enum.Parse(typeof(B2BConstants.MediaObjectType),
						(item.FindControl("litOrderAttachType") as Literal).Text);
					if (attachType == B2BConstants.MediaObjectType.Text)
					{

						lnkAttachDisplayName.Visible = true;
						(item.FindControl("imgAttachIcon") as Image).ImageUrl = "~/includes/images/toolText_f1.gif";

					}

					else
					{

						lnkAttachFilename.Visible = true;
						if (Convert.ToBoolean(this.hidOrderSupplier.Value))
							lnkAttachFilename.NavigateUrl = String.Format("~/CommonObject/FileHandler.ashx?type=s&filename={0}",
								Server.UrlEncode((item.FindControl("litOrderAttachFilename") as Literal).Text));

						else if ((item.FindControl("litOrderAttachType") as Literal).Text.Equals("1"))
							lnkAttachFilename.NavigateUrl = String.Format("~/CommonObject/FileHandler.ashx?type=j&filename={0}",
								Server.UrlEncode((item.FindControl("litOrderAttachFilename") as Literal).Text));

						else if ((item.FindControl("litOrderAttachType") as Literal).Text.Equals("5"))
							lnkAttachFilename.NavigateUrl = String.Format("~/CommonObject/FileHandler.ashx?type=j5&filename={0}",
								Server.UrlEncode((item.FindControl("litOrderAttachFilename") as Literal).Text));

						(item.FindControl("imgAttachIcon") as Image).ImageUrl = "~/includes/images/toolFile_f1.gif";

					}
				}
			}
		}

		protected void lnkAttachDisplayName_Click(object sender, EventArgs e)
		{
			// Hides both panels
			this.panDisplayText.Style[HtmlTextWriterStyle.Display] = "none";

			LinkButton lnkAttachDisplayName = sender as LinkButton;

			this.objOrderDetAttachDet.SelectParameters["orderAttachSeq"].DefaultValue = lnkAttachDisplayName.Attributes["attachSeq"];
			this.objOrderDetAttachDet.Select();
		}
		#endregion

		#region Database Access
		protected void objOrderDetAttach_Selected(object sender, ObjectDataSourceStatusEventArgs e)
		{
			this.lstAttachment.DataSource = e.ReturnValue as IEnumerable<DetailAttachment>;
			this.lstAttachment.DataBind();
		}

		protected void objOrderDetAttachDet_Selected(object sender, ObjectDataSourceStatusEventArgs e)
		{
			var detailAttachments = e.ReturnValue as IEnumerable<DetailAttachment>;

			if (detailAttachments != null && detailAttachments.Count() > 0)
			{

				var detailAttchment = detailAttachments.First();

				// Checks the media object type
				if ((B2BConstants.MediaObjectType)detailAttchment.OrderAttachType == B2BConstants.MediaObjectType.Text)
				{

					// Converts from rtf to html
					this.txtOrderAttachText.Content = B2BFunctions.ConvertRtfToHtml(B2BFunctions.ConvertBytesToString(detailAttchment.OrderAttachContent),
						ConfigurationManager.AppSettings[B2BConstants.RTF_HTML_KEY]);

					// Show the panel
					this.panDisplayText.Style[HtmlTextWriterStyle.Display] = String.Empty;

				}

				else
				{

					#region Opens the file
					string filename = String.Format("{0}{1}", Convert.ToBoolean(this.hidOrderSupplier.Value) ?
						ConfigurationManager.AppSettings["SUPPLIER_SOURCE_PATH"] : ConfigurationManager.AppSettings["JDE_SOURCE_PATH"], detailAttchment.OrderAttachFilename);

					ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "OpenFile",
						String.Format("OpenFileAttachment('j', '{0}');", String.Empty), true);
					#endregion

				}
			}
		}
		#endregion
	}
}