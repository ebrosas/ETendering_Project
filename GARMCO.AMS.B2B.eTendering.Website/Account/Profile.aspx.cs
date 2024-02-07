using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GARMCO.AMS.B2B.DAL;
using GARMCO.AMS.B2B.eTendering.DAL;
using GARMCO.AMS.B2B.Report.eTendering;
using GARMCO.AMS.B2B.Utility;
using GARMCO.AMS.Common.Web;
using Telerik.Web.UI;

namespace GARMCO.AMS.B2B.eTendering.Website.Account
{
	public partial class Profile : BaseWebForm
	{
		#region Properties
		public List<SupplierProdServItem> SupplierProductServiceList
		{
			get
			{
				List<SupplierProdServItem> list = new List<SupplierProdServItem>();
				if (Session[B2BConstants.ITEM_LIST_SUPPLIER_PRODSERVICE] != null)
					list = Session[B2BConstants.ITEM_LIST_SUPPLIER_PRODSERVICE] as List<SupplierProdServItem>;

				return list;
			}

			set
			{
				Session[B2BConstants.ITEM_LIST_SUPPLIER_PRODSERVICE] = value;
			}
		}
		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{

				// Sets the selected menu
				this.Master.SetSelectedMenu(Request.Url.PathAndQuery);

				// Set the default button when enter key is pressed
				this.Master.DefaultButton = this.btnUpdate.UniqueID;
				this.txtSupplierName.Focus();

				// Retrieves the supplier information
				this.objSupplier.Select();

			}
		}

		#region Override Methods
		protected override void OnInit(EventArgs e)
		{
			if (!this.IsPostBack)
			{

				// Retrieves the current list of products and services
				this.objSupplierProdServ.Select();

			}

			#region Ajaxify the controls
			AjaxSetting ajaxSetting = new AjaxSetting(this.Master.AjaxMngr.ClientID);
			ajaxSetting.UpdatedControls.Add(new AjaxUpdatedControl(this.panEntry.ID, this.loadingPanel.ID));
			this.Master.AjaxMngr.AjaxSettings.Add(ajaxSetting);
			#endregion

			base.OnInit(e);
		}

		public override void ajaxMngrBase_AjaxRequest(object sender, AjaxRequestEventArgs e)
		{
			string returnValue = e.Argument;
			if (returnValue.IndexOf(B2BConstants.AJAX_RETURN_CONFIRMED) > -1)
			{

				// Parse the argument
				string[] argList = returnValue.Split(B2BConstants.LIST_SEPARATOR);
				bool confirmed = Convert.ToBoolean(argList[2]);

				#region Checks which button
				// Success
				if ((argList[1] == "Success" && confirmed))
					Response.Redirect("Default.aspx", false);
				#endregion

			}

			#region Contact Person
			else if (returnValue.IndexOf(B2BConstants.AJAX_RETURN_SUPPLIER_CONTACT_LOOKUP) > -1)
			{

				// Parse the argument
				string[] argList = returnValue.Split(B2BConstants.LIST_SEPARATOR);

				// Checks if primary contact person has been changed
				if (argList[1].Equals(Session[B2BConstants.CONTACT_ID].ToString()) && Convert.ToBoolean(Session[B2BConstants.CONTACT_PRIMARY]) &&
					!Convert.ToBoolean(argList[2]))
				{

					// Update the primary contact session
					Session[B2BConstants.CONTACT_PRIMARY] = false;
					Response.Redirect("Profile.aspx");

				}

				// Update the contact's list
				else
				{

					#region Sends notification of a new contact person to Purchasing
					if (Convert.ToBoolean(argList[3]))
					{

						#region Creates the report
						List<string> attachmentList = new List<string>();
						SupplierContactRep rep = new SupplierContactRep();

						// Sets the data source
						var supplierContacts = new SupplierContactRepository().GetSupplierContact(0, Convert.ToInt32(argList[1]), String.Empty, String.Empty,Convert.ToInt32(Session[B2BConstants.CONTACT_SUPPLIER_NO]));
						rep.DataSource = supplierContacts.First();

						string mimeType = string.Empty;
						string ext = string.Empty;
						Encoding encoding = Encoding.Default;

						string filename = String.Format("{0}_{1}_Temp.pdf", Session[B2BConstants.CONTACT_SUPPLIER_NO].ToString(), DateTime.Now.ToString("yyyymmddHHmmss"));
						filename = Path.Combine(Server.MapPath("~/Temp"), filename);

						byte[] reportBytes = Telerik.Reporting.Processing.ReportProcessor.Render("PDF",
							rep, null, out mimeType, out ext, out encoding);

						// Saves the report to a file
						FileStream fs = new FileStream(filename, FileMode.Create);
						fs.Write(reportBytes, 0, reportBytes.Length);
						fs.Close();

						attachmentList.Add(filename);
						#endregion

						#region Creates recipients
						string[] purchasingList = ConfigurationManager.AppSettings["PURCHASING_TEAM"].Split(B2BConstants.LIST_SEPARATOR);
						List<MailAddress> toList = new List<MailAddress>();
						foreach (string to in purchasingList)
						{

							if (to.Length > 0)
								toList.Add(new MailAddress(to));

						}

						string[] supportList = ConfigurationManager.AppSettings["SUPPORT_TEAM"].Split(B2BConstants.LIST_SEPARATOR);
						List<MailAddress> bccList = new List<MailAddress>();
						foreach (string bcc in supportList)
						{

							if (bcc.Length > 0)
								bccList.Add(new MailAddress(bcc));

						}
						#endregion

						// Sets the subject and body
						string subject = "GARMCO e-Tendering New Contact Person";
						string body = B2BFunctions.RetrieveXmlMessage(
							Server.MapPath("~/Messages/Registration.xml")).Replace("\r\n", "<br />");

						// Send the email
						this.Master.SendEmail(toList, null, bccList, subject, body,
							attachmentList, ConfigurationManager.AppSettings["eTenderingAdminName"],
							ConfigurationManager.AppSettings["eTenderingAdminEmail"]);

					}
					#endregion
					
					this.gvContact.DataBind();

				}
			}
			#endregion
		}
		#endregion

		#region Action Buttons
		protected void btnUpdate_Click(object sender, EventArgs e)
		{
			this.Validate();
			if (this.IsValid)
				this.objSupplier.Update();
		}
		#endregion

		#region Data Binding
		#region Contacts
		protected void gvContact_PreRender(object sender, EventArgs e)
		{

		}

		protected void gvContact_ItemDataBound(object sender, GridItemEventArgs e)
		{
			if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
			{

				GridDataItem item = e.Item as GridDataItem;
				if (item != null)
				{

					#region Formats the display
					bool contactRejected = Convert.ToBoolean(item["ContactRejected"].Text);

					item["ContactName"].ToolTip = (item["ContactName"].FindControl("litContactName") as Literal).Text;
					item["ContactEmail"].ToolTip = (item["ContactEmail"].FindControl("litContactEmail") as Literal).Text;
					item["ContactTelNo"].ToolTip = (item["ContactTelNo"].FindControl("litContactTelNo") as Literal).Text;
					item["ContactMobNo"].ToolTip = (item["ContactMobNo"].FindControl("litContactMobNo") as Literal).Text;
					item["ContactFaxNo"].ToolTip = (item["ContactFaxNo"].FindControl("litContactFaxNo") as Literal).Text;

					//item["ContactRejectReason"].ToolTip = (item["ContactRejectReason"].FindControl("litContactRejectReason") as Literal).Text;
					item["ContactRejected"].Text = contactRejected ? "Yes" : "No";
					if (contactRejected)
						item["ContactRejected"].ToolTip = item["ContactRejectReason"].Text;

					//item["ContactCreatedName"].ToolTip = (item["ContactCreatedName"].FindControl("litContactCreatedName") as Literal).Text;
					item["ContactModifiedName"].ToolTip = (item["ContactModifiedName"].FindControl("litContactModifiedName") as Literal).Text;

					// Checks if not primary
					(item["DeleteImageButton"].Controls[0] as ImageButton).Visible = Convert.ToBoolean(Session[B2BConstants.CONTACT_PRIMARY]) &&
						!item["ContactID"].Text.Equals(Session[B2BConstants.CONTACT_ID].ToString());

					// Enables the update option
					(item["UpdateContact"].Controls[0] as LinkButton).Visible = item["ContactID"].Text.Equals(Session[B2BConstants.CONTACT_ID].ToString()) ||
						Convert.ToBoolean(Session[B2BConstants.CONTACT_PRIMARY]);
					#endregion

				}
			}
		}

		protected void gvContact_ItemCommand(object sender, GridCommandEventArgs e)
		{
			#region Creates a new contact
			if (e.CommandName == RadGrid.InitInsertCommandName)
			{

				#region Opens the contact window
				StringBuilder script = new StringBuilder();

				script.Append("ShowSupplierContactLookup('");
				script.Append(this.Master.AjaxMngr.ClientID);
				script.Append("', 0, 0);");

				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Contact", script.ToString(), true);
				#endregion

				e.Canceled = true;

			}
			#endregion

			#region Updates contact info
			else if (e.CommandName == "UpdateContact")
			{

				GridDataItem item = e.Item as GridDataItem;
				if (item != null)
				{

					#region Opens the contact window
					StringBuilder script = new StringBuilder();

					script.Append("ShowSupplierContactLookup('");
					script.Append(this.Master.AjaxMngr.ClientID);
					script.Append("', 1, ");
					script.Append(item["ContactID"].Text);
					script.Append(");");

					ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Contact", script.ToString(), true);
					#endregion

				}
			}
			#endregion

			#region Delete Contact
			else if (e.CommandName == RadGrid.DeleteCommandName)
			{

				GridDataItem item = e.Item as GridDataItem;
				if (item != null)
				{

					this.objSupplierContact.UpdateParameters["contactID"].DefaultValue = item["ContactID"].Text;
					this.objSupplierContact.Update();

				}
			}
			#endregion
		}
		#endregion

		#region Products/Servcies
		protected void lstSupplierProdServ_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			var row = e.Item.DataItem as SupplierProductService;

			// Add attribute to the checkbox
			CheckBox chkProdServ = e.Item.FindControl("chkProdServ") as CheckBox;
			chkProdServ.Attributes.Add("value", row.ProdServCode);

			// Add tooltip
			LinkButton lnkProdServ = e.Item.FindControl("lnkProdServ") as LinkButton;
			lnkProdServ.Attributes.Add("value", row.ProdServCode);

			// Check the item
			if (this.SupplierProductServiceList.Exists(tempItem => tempItem.ProdServCode.Equals(row.ProdServCode) &&
				!tempItem.MarkForDeletion))
				chkProdServ.Checked = true;
		}

		protected void chkProdServ_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox chkProdSev = sender as CheckBox;

			#region Removes the products/services
			if (!chkProdSev.Checked)
			{

				string prodServCode = chkProdSev.Attributes["value"];
				int index = prodServCode.IndexOf("0");

				// Increments the index if current position is odd number
				if ((index % 2) != 0)
					index++;

				prodServCode = prodServCode.Substring(0, index);

				// Removes all items in the current list
				this.SupplierProductServiceList.RemoveAll(tempItem => tempItem.ProdServCode.IndexOf(prodServCode) == 0);

			}
			#endregion

			#region Removes all child items of the parent node
			else
			{

				string prodServCode = chkProdSev.Attributes["value"];
				int index = prodServCode.IndexOf("0");

				// Increments the index if current position is odd number
				if ((index % 2) != 0)
					index++;

				prodServCode = prodServCode.Substring(0, index);

				// Removes all items in the current list
				this.SupplierProductServiceList.RemoveAll(tempItem => tempItem.ProdServCode.IndexOf(prodServCode) == 0);

				// Adds the parent node
				this.SupplierProductServiceList.Add(new SupplierProdServItem()
				{
					ProdServSupplierNo = 0,
					ProdServCode = chkProdSev.Attributes["value"],
					ProdServCodeDesc = String.Empty,
					ProdServCheckState = B2BConstants.CheckState.Checked
				});

			}
			#endregion
		}

		protected void lnkProdServ_Click(object sender, EventArgs e)
		{
			LinkButton lnkProdServ = sender as LinkButton;

			#region Build the script
			StringBuilder script = new StringBuilder();

			script.Append("ShowSupplierProdServLookUp('");
			script.Append(this.Master.AjaxMngr.ClientID);
			script.Append("', '");
			script.Append(lnkProdServ.Attributes["value"]);
			script.Append("', '");
			script.Append(Server.UrlEncode(lnkProdServ.Text));
			script.Append("');");

			ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "UNSPSC", script.ToString(), true);
			#endregion
		}
		#endregion
		#endregion

		#region Database Access
		protected void objSupplier_Selected(object sender, ObjectDataSourceStatusEventArgs e)
		{
			var suppliers = e.ReturnValue as IEnumerable<GARMCO.AMS.B2B.eTendering.DAL.Supplier>;

			if (suppliers != null)
			{

				#region Retrieves supplier info
				var supplier = suppliers.First();

				this.hidSupplierNo.Value = supplier.SupplierNo.ToString();
				this.txtSupplierName.Text = supplier.SupplierName;
				this.txtSupplierURL.Text = supplier.SupplierURL;
				this.txtSupplierAddr.Text = supplier.SupplierAddress;
				this.txtSupplierCity.Text = supplier.SupplierCity;
				this.txtSupplierState.Text = supplier.SupplierState;
				if (this.cmbSupplierCountry.Items.Count == 0)
					this.cmbSupplierCountry.DataBind();
				this.cmbSupplierCountry.SelectedValue = supplier.SupplierCountry;
				this.txtSupplierPostal.Text = supplier.SupplierPostalCode;

				if (this.cmbSupplierCurrency.Items.Count == 0)
					this.cmbSupplierCurrency.DataBind();
				this.cmbSupplierCurrency.SelectedValue = supplier.SupplierCurrency;

				if (this.cmbSupplierDelTerm.Items.Count == 0)
					this.cmbSupplierDelTerm.DataBind();
				this.cmbSupplierDelTerm.SelectedValue = supplier.SupplierDelTerm;

				this.chkSupplierNews.Checked = supplier.SupplierNews;
				this.chkSupplierIncProdServ.Checked = supplier.SupplierIncProdServ;
				this.chkSupplierNotProdServ.Checked = supplier.SupplierNotProdServ;
				this.chkSupplierNotProdServ.Enabled = supplier.SupplierIncProdServ;
				#endregion

			}
		}

		protected void objSupplier_Updated(object sender, ObjectDataSourceStatusEventArgs e)
		{
			int retError = Convert.ToInt32(e.OutputParameters["retError"]);
			string errorMsg = e.OutputParameters["errorMsg"].ToString();

			// Checks if no error
			if (retError == B2BConstants.DB_STATUS_OK)
			{

				#region Shows bids confirmation
				ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Confirmation",
					String.Format("ShowSuccessMsg('{0}', 'Success', 'Your profile has been updated successfully.');",
					this.Master.AjaxMngr.ClientID), true);
				#endregion

			}

			else
			{

				// Checks for error message
				if (errorMsg.Length > 0)
					errorMsg = Server.HtmlEncode(errorMsg.Replace("\r\n", ""));

				else
					errorMsg = "An error occurred while updating your profile.<br />Please try again.";

				ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error",
					String.Format("ShowAlertMessage('{0}');", errorMsg), true);

			}
		}

		protected void objSupplierContact_Updated(object sender, ObjectDataSourceStatusEventArgs e)
		{
			int retError = Convert.ToInt32(e.OutputParameters["retError"]);
			string errorMsg = e.OutputParameters["errorMsg"].ToString();

			// Checks if no error
			if (retError == B2BConstants.DB_STATUS_OK)
			{
			}

			else
			{

				// Checks for error message
				if (errorMsg.Length > 0)
					errorMsg = Server.HtmlEncode(errorMsg.Replace("\r\n", ""));

				else
					errorMsg = "An error occurred while deleting the contact person.<br />Please try again.";

				ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error",
					String.Format("ShowAlertMessage('{0}');", errorMsg), true);

			}
		}

		protected void objSupplierProdServ_Selected(object sender, ObjectDataSourceStatusEventArgs e)
		{
			this.SupplierProductServiceList = e.ReturnValue as List<SupplierProdServItem>;
		}
		#endregion
	}
}