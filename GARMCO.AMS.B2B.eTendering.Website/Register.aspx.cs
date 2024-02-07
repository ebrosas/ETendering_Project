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

namespace GARMCO.AMS.B2B.eTendering.Website
{
	public partial class Register : BaseWebForm
	{
		#region Constants
		public enum TabID : int
		{
			CompanyInfo,
			ContactPerson,
			ProductService,
			Preference,
			Confirmation
		};
		#endregion

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

		public string Password
		{
			get
			{
				string password = String.Empty;
				if (ViewState["Password"] != null)
					password = ViewState["Password"].ToString();

				return password;
			}

			set
			{
				ViewState["Password"] = value;
			}
		}

		public string ConfirmPassword
		{
			get
			{
				string password = String.Empty;
				if (ViewState["ConfirmPassword"] != null)
					password = ViewState["ConfirmPassword"].ToString();

				return password;
			}

			set
			{
				ViewState["ConfirmPassword"] = value;
			}
		}
		#endregion

		#region Private Data Members
		private string[] valGroup = { "valGroup0", "valGroup1", "valGroup2", "valGroup3", "valGroup4" };
		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{

				// Creates an onclick event
				this.chkSupplierIncProdServ.Attributes.Add("onchange", String.Format("javascript:updateNotification('{0}', '{1}');",
					this.chkSupplierIncProdServ.ClientID, this.chkSupplierNotProdServ.ClientID));

				// Customizes the character set for the captcha control
				this.captchaImg.CaptchaImage.CharSet = "abcdefghijkmnpqrstuvwxyz23456789";
				this.captchaImg.CaptchaImage.TextChars = CaptchaPossibleChars.CustomCharSet;

			}

			#region Stores the current content of the password
			else if (this.IsPostBack)
			{

				this.Password = this.txtContactPassword.Text;
				this.ConfirmPassword = this.txtContactPasswordConfirm.Text;

			}
			#endregion
		}

		#region Override Methods
		protected override void OnInit(EventArgs e)
		{
			// Don't check the session
			this.IsToCheckSession = false;

			if (!this.IsPostBack)
			{

				// Initializes the list
				this.SupplierProductServiceList = new List<SupplierProdServItem>();

			}

			#region Ajaxify the controls
			AjaxSetting ajaxSetting = new AjaxSetting(this.Master.AjaxMngr.ClientID);
			ajaxSetting.UpdatedControls.Add(new AjaxUpdatedControl(this.panEntry.ID, this.loadingPanel.ID));
			this.Master.AjaxMngr.AjaxSettings.Add(ajaxSetting);
			#endregion

			base.OnInit(e);
		}

		protected override void OnPreRender(EventArgs e)
		{
			#region Sets the value of password
			this.txtContactPassword.Attributes.Add("value", this.Password);
			this.txtContactPasswordConfirm.Attributes.Add("value", this.ConfirmPassword);
			#endregion

			base.OnPreRender(e);
		}

		public override void ajaxMngrBase_AjaxRequest(object sender, AjaxRequestEventArgs e)
		{
			string returnValue = e.Argument;

			#region JDE Supplier
			if (returnValue.LastIndexOf(B2BConstants.AJAX_RETURN_SUPPLIER_PROD_SERVICE_LOOKUP) > -1)
			{

				string[] argList = returnValue.Split(B2BConstants.LIST_SEPARATOR);

				#region Checks if parent node is checked or indeterminate
				bool isChecked = false;
				SupplierProdServItem item = this.SupplierProductServiceList.Find(tempItem =>
					tempItem.ProdServCode == argList[1] && !tempItem.MarkForDeletion);

				if (item != null)
					isChecked = true;

				// Find the checkbox control with the same unspsc code
				foreach (DataListItem lstItem in this.lstSupplierProdServ.Items)
				{

					CheckBox chkProdServ = lstItem.FindControl("chkProdServ") as CheckBox;
					if (chkProdServ != null && chkProdServ.Attributes["value"].Equals(argList[1]))
					{

						chkProdServ.Checked = isChecked;
						break;

					}
				}
				#endregion
			}
			#endregion
		}
		#endregion

		#region Private Methods
		private void UpdatePageWizard(int pageIndex)
		{
			string validationGroup = this.valGroup[pageIndex];

			// Disables first all tabs
			foreach (RadTab tab in this.tabControl.Tabs)
				tab.Enabled = false;

			// Sets the tabs to be shown
			this.tabControl.Tabs[pageIndex].Enabled = true;
			this.tabControl.SelectedIndex = this.multiPg.SelectedIndex = pageIndex;

			#region Updates the buttons and validation summary
			this.btnPrev.Visible = pageIndex > 0;
			this.btnNext.Visible = pageIndex < (int)TabID.Confirmation;
			this.btnRegister.Visible = pageIndex == (int)TabID.Confirmation;

			this.btnNext.ValidationGroup = validationGroup;
			this.btnRegister.ValidationGroup = validationGroup;

			this.valSummary.ValidationGroup = validationGroup;
			#endregion
		}
		#endregion

		#region General Events
		protected void cusSupplierProfile_ServerValidate(object source, ServerValidateEventArgs args)
		{
			bool isValid = true;

			if (this.uploadSupplierProfile.Visible && this.uploadSupplierProfile.UploadedFiles.Count == 0)
				isValid = false;

			else if (this.uploadSupplierProfile.Visible)
			{

				#region Copies the uploaded file
				string uploadFolder = "~/Account/Profile/";
				UploadedFile uploadedFile = this.uploadSupplierProfile.UploadedFiles[0];

				string targetFilename = uploadedFile.GetName().Replace("#", "No.");

				if (File.Exists(Server.MapPath(Path.Combine(uploadFolder, targetFilename))))
				{

					// Rename the file
					targetFilename = String.Format("{0}_{1}{2}",
						uploadedFile.GetNameWithoutExtension(), DateTime.Now.Ticks, uploadedFile.GetExtension());

				}

				// Save the file
				uploadedFile.SaveAs(Server.MapPath(Path.Combine(uploadFolder, targetFilename)));

				// Shows the link to it
				this.lnkSupplierProfile.NavigateUrl = String.Format("~/CommonObject/FileHandler.ashx?filename={0}&type=profile", targetFilename);
				this.lnkSupplierProfile.Text = uploadedFile.GetName();
				this.lnkSupplierProfile.Visible = true;
				this.lnkRemove.Visible = true;
				this.hidCompanyProfile.Value = targetFilename;

				this.uploadSupplierProfile.Visible = false;
				#endregion

			}

			args.IsValid = isValid;
		}

		protected void cusSupplierProdServ_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = this.SupplierProductServiceList.Count > 0;
		}

		protected void cusSupplierTerms_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = this.chkSupplierTerms.Checked;
		}
		#endregion

		#region Action Button
		protected void btnPrev_Click(object sender, EventArgs e)
		{
			int currentPage = this.tabControl.SelectedIndex;
			this.UpdatePageWizard(--currentPage);
		}

		protected void btnNext_Click(object sender, EventArgs e)
		{
			int currentPage = this.tabControl.SelectedIndex;

			if (this.IsValid)
				this.UpdatePageWizard(++currentPage);
		}

		protected void btnRegister_Click(object sender, EventArgs e)
		{
			int? retError = B2BConstants.DB_STATUS_OK;
			string errorMsg = String.Empty;

			this.Validate(this.valGroup[(int)TabID.Confirmation]);
			if (this.IsValid)
			{

				#region Retrieves all the entries
				int? supplierNo = 0;
				string supplierName = this.txtSupplierName.Text.Trim();
				string supplierURL = this.txtSupplierURL.Text.Trim();
				bool? supplierOld = this.chkSupplierExist.Checked;
				string supplierAddress = this.txtSupplierAddr.Text.Trim();
				string supplierCity = this.txtSupplierCity.Text.Trim();
				string supplierState = this.txtSupplierState.Text.Trim();
				string supplierCountry = this.cmbSupplierCountry.SelectedValue;
				string supplierPostalCode = this.txtSupplierPostal.Text.Trim();
				string supplierCurrency = this.cmbSupplierCurrency.SelectedValue.Trim();
				string supplierDelTerm = this.cmbSupplierDelTerm.SelectedValue.Trim();
				int? supplierShipVia = 0;
				bool? supplierNews = this.chkSupplierNews.Checked;
				bool? supplierIncProdServ = this.chkSupplierIncProdServ.Checked;
				bool? supplierNotProdServ = this.chkSupplierNotProdServ.Checked;

				string contactName = this.txtContactName.Text.Trim();
				string contactEmail = this.txtContactEmail.Text.Trim();
				string contactPassword = B2BFunctions.Encrypt(this.txtContactPassword.Text.Trim());
				string contactTelNo = this.txtContactTelNo.Text.Trim();
				string contactMobNo = this.txtContactMobNo.Text.Trim();
				string contactFaxNo = this.txtContactFaxNo.Text.Trim();

				// Generates an activation key
				string contactActiveKey = String.Format("{0}{1}{2}", (new Random()).Next(), Guid.NewGuid().ToString(), DateTime.Now.Ticks);
				#endregion

				#region Registers the new supplier
				var row = new SupplierRepository().InsertSupplier(B2BConstants.DB_INSERT_RECORD, ref supplierNo, 0, supplierName, supplierURL,
					supplierOld, supplierAddress, supplierCity, supplierState, supplierCountry,
					supplierPostalCode, supplierCurrency, supplierDelTerm, supplierShipVia, supplierNews, supplierIncProdServ, supplierNotProdServ,
					this.lnkSupplierProfile.Text, this.hidCompanyProfile.Value,
					contactName, contactEmail, contactPassword, contactTelNo, contactMobNo, contactFaxNo, contactActiveKey,
					this.SupplierProductServiceList, 0, contactName, ref retError, ref errorMsg);

				if (retError == B2BConstants.DB_STATUS_OK)
				{

					#region Sends notification of a new contact person to Purchasing
					#region Creates the report
					List<string> attachmentList = new List<string>();
					SupplierContactRep rep = new SupplierContactRep();

					// Sets the data source
					rep.DataSource = row;

					string mimeType = string.Empty;
					string ext = string.Empty;
					Encoding encoding = Encoding.Default;

					string filename = String.Format("{0}_{1}_Temp.pdf", supplierNo, DateTime.Now.ToString("yyyymmddHHmmss"));
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

					// Adds the company profile
					attachmentList.Add(Server.MapPath(Path.Combine("~/Account/Profile/", this.hidCompanyProfile.Value)));

					// Sets the subject and body
					string subject = "GARMCO e-Tendering New Contact Person";
					string body = B2BFunctions.RetrieveXmlMessage(
						Server.MapPath("~/Messages/Registration.xml")).Replace("\r\n", "<br />");

					// Send the email
					this.Master.SendEmail(toList, null, bccList, subject, body,
						attachmentList, ConfigurationManager.AppSettings["eTenderingAdminName"],
						ConfigurationManager.AppSettings["eTenderingAdminEmail"]);
					#endregion

					this.Session["msg"] = "Thank you for registering to GARMCO e-Tendering Website. Kindly allow our personnel to process your form within 3~5 business days.";
					Response.Redirect("~/Success.aspx", false);

				}

				else if (errorMsg.Length > 0)
					errorMsg = Server.HtmlEncode(errorMsg.Replace("\r\n", ""));

				// Duplicate record
				else if (retError == 1004)
					errorMsg = "Similar supplier name or contact person with the specified e-mail address is already registered.";

				else if (retError == -2)
					errorMsg = "Similiar supplier name or contact e-mail address already exists.";

				else
					errorMsg = "An error occurred while registering your entries.<br />Please try again.";
				#endregion

				#region Shows the error message if any
				if (errorMsg.Length > 0)
				{

					ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error",
						String.Format("ShowAlertMessage('{0}');", errorMsg), true);

				}
				#endregion
			}
		}

		protected void lnkRemove_Click(object sender, EventArgs e)
		{
			this.uploadSupplierProfile.UploadedFiles.Clear();
			this.uploadSupplierProfile.Visible = true;

			this.lnkSupplierProfile.Visible = false;
			this.lnkRemove.Visible = false;
		}
		#endregion

		#region Data Binding
		protected void lstSupplierProdServ_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			var row = e.Item.DataItem as SupplierProductService;

			// Add attribute to the checkbox
			CheckBox chkProdServ = e.Item.FindControl("chkProdServ") as CheckBox;
			chkProdServ.Attributes.Add("value", row.ProdServCode);

			// Add tooltip
			LinkButton lnkProdServ = e.Item.FindControl("lnkProdServ") as LinkButton;
			lnkProdServ.Attributes.Add("value", row.ProdServCode);
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
	}
}