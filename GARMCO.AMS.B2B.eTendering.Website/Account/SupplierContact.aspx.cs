using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GARMCO.AMS.B2B.eTendering.DAL;
using GARMCO.AMS.B2B.Report.eTendering;
using GARMCO.AMS.B2B.Utility;
using GARMCO.AMS.Common.Web;
using Telerik.Web.UI;

namespace GARMCO.AMS.B2B.eTendering.Website.Account
{
	public partial class SupplierContact : BaseWebForm
	{
		#region Constants
		public enum TabID : int
		{
			Contact,
			Password
		};
		#endregion

		#region Properties
		public int TransactionMode
		{
			get
			{
				return Convert.ToInt32(this.hidMode.Value);
			}

			set
			{
				this.hidMode.Value = value.ToString();
			}
		}

		public string ContactPassword
		{
			get
			{
				string password = String.Empty;

				if (ViewState["ContactPassword"] != null)
					password = ViewState["ContactPassword"].ToString();

				return password;
			}

			set
			{
				ViewState["ContactPassword"] = value;
			}

		}
		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{

				// Checks the transaction mode
				this.TransactionMode = Convert.ToInt32(Request.QueryString["mode"]);
				this.hidAjaxID.Value = Request.QueryString["ajaxID"];
				if (this.TransactionMode == B2BConstants.DB_UPDATE_RECORD)
				{

					this.objSupplierContact.SelectParameters["contactID"].DefaultValue = Request.QueryString["contactID"];
					this.objSupplierContact.Select();

				}

				else
				{

					// Hides the other tab
					this.tabControl.Tabs[(int)TabID.Password].Visible = false;

					#region Shows the password entry
					this.tdPassword.Style[HtmlTextWriterStyle.Display] = String.Empty;

					this.reqContactPassword.Enabled = true;
					this.regContactPassword.Enabled = true;
					this.reqContactPasswordConfirm.Enabled = true;
					this.comContactPasswordConfirm.Enabled = true;
					#endregion

					this.btnUpdate.Text = "Submit";

				}

				// Set the default button when enter key is pressed
				this.form1.DefaultButton = this.btnUpdate.UniqueID;
				this.txtContactEmail.Focus();

			}
		}

		#region Override Methods
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
		}
		#endregion

		#region General Events
		protected void ajaxMngr_AjaxRequest(object sender, AjaxRequestEventArgs e)
		{
			string returnValue = e.Argument;
			if (returnValue.IndexOf(B2BConstants.AJAX_RETURN_CONFIRMED) > -1)
			{

				// Parse the argument
				string[] argList = returnValue.Split(B2BConstants.LIST_SEPARATOR);
				bool confirmed = Convert.ToBoolean(argList[2]);

				#region Checks which button
				// Update and set as primary
				if (argList[1] == this.btnUpdate.ID && confirmed)
					this.objSupplierContact.Update();

				#region Success
				else if (argList[1] == "Success" && confirmed)
				{

					StringBuilder script = new StringBuilder();
					script.Append("OnCloseSupplierContactLookup('");
					script.Append(this.hidAjaxID.Value);
					script.Append("', ");
					script.Append(this.hidContactID.Value);
					script.Append(", '");
					script.Append(this.chkContactPrimary.Checked.ToString());
					script.Append("', 'true');");

					ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Contact",
						script.ToString(), true);

				}
				#endregion
				#endregion

			}
		}
		#endregion

		#region Action Buttons
		protected void btnUpdate_Click(object sender, EventArgs e)
		{
			// Checks which tab is selected
			if (this.TransactionMode == B2BConstants.DB_INSERT_RECORD)
			{

				// Generates activation key
				this.objSupplierContact.UpdateParameters["contactActiveKey"].DefaultValue =
					String.Format("{0}{1}{2}", (new Random()).Next(), Guid.NewGuid().ToString(), DateTime.Now.Ticks);

				this.Validate(this.valSummaryContact.ValidationGroup);

			}

			else if (this.tabControl.SelectedIndex == (int)TabID.Contact)
			{

				this.TransactionMode = B2BConstants.DB_UPDATE_RECORD;
				this.Validate(this.valSummaryContact.ValidationGroup);

			}

			else if (this.tabControl.SelectedIndex == (int)TabID.Password)
			{

				this.TransactionMode = 5;
				this.Validate(this.valSummaryPwd.ValidationGroup);

			}

			// Checks if valid
			if (this.IsValid)
			{

				#region Sets the password
				if (this.TransactionMode == B2BConstants.DB_INSERT_RECORD)
					this.objSupplierContact.UpdateParameters["contactPassword"].DefaultValue =
						B2BFunctions.Encrypt(this.txtContactPassword.Text.Trim());

				else if (this.TransactionMode == 5)
					this.objSupplierContact.UpdateParameters["contactPassword"].DefaultValue =
						B2BFunctions.Encrypt(this.txtContactPasswordUpdate.Text.Trim());
				#endregion

				// Checks if setting others as primary
				if (this.chkContactPrimary.Checked && this.TransactionMode == B2BConstants.DB_UPDATE_RECORD &&
					!this.hidContactID.Value.Equals(Session[B2BConstants.CONTACT_ID].ToString()))
				{

					#region Builds the script
					StringBuilder script = new StringBuilder();

					script.Append("ShowConfirmMessageWithuBttonIndicatorRetArg('Are you sure you want to set this contact person as the primary instead of you?<br />Please click Ok if yes, otherwise Cancel.', '");
					script.Append(this.ajaxMngr.ClientID);
					script.Append("', '");
					script.Append(this.btnUpdate.ID);
					script.Append("', 1);");

					ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Update", script.ToString(), true);
					#endregion

				}

				#region Checks if current password is correct
				else if (this.TransactionMode == 5 && !this.txtContactPasswordCurrent.Text.Trim().Equals(B2BFunctions.Decrypt(this.ContactPassword)))
				{

					this.txtContactPasswordCurrent.Focus();
					ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error",
						"ShowAlertMessage('The current password is invalid, please try again.')", true);

				}
				#endregion

				else
					this.objSupplierContact.Update();

			}
		}
		#endregion

		#region Database Access
		protected void objSupplierContact_Selected(object sender, ObjectDataSourceStatusEventArgs e)
		{
			var dataTable = e.ReturnValue as IEnumerable<GARMCO.AMS.B2B.eTendering.DAL.SupplierContact>;

			if (dataTable != null)
			{

				#region Retrieves the details
				var row = dataTable.First();

				#region Sets general information
				this.hidMode.Value = B2BConstants.DB_UPDATE_RECORD.ToString();

				this.hidContactID.Value = row.ContactID.ToString();
				this.txtContactEmail.Text = row.ContactEmail;
				this.txtContactEmailConfirm.Text = row.ContactEmail;
				this.txtContactName.Text = row.ContactName;
				this.txtContactTelNo.Text = row.ContactTelNo;
				this.txtContactMobNo.Text = row.ContactMobNo;
				this.txtContactFaxNo.Text = row.ContactFaxNo;
				this.chkContactPrimary.Checked = row.ContactPrimary;
				this.ContactPassword = row.ContactPassword;
				#endregion

				// Checks if not yet activated
				if (!row.ContactActive)
				{

					this.chkContactPrimary.Visible = false;

					this.tabControl.Tabs[(int)TabID.Password].Visible = false;

					this.litNote.Text = "This contact is not yet activated";
					this.btnUpdate.Visible = false;

				}
				#endregion
			}
		}

		protected void objSupplierContact_Updated(object sender, ObjectDataSourceStatusEventArgs e)
		{
			int retError = Convert.ToInt32(e.OutputParameters["retError"]);
			string errorMsg = e.OutputParameters["errorMsg"].ToString();

			// Checks if no error
			if (retError == B2BConstants.DB_STATUS_OK)
			{

				StringBuilder script = new StringBuilder();

				if (this.TransactionMode == B2BConstants.DB_INSERT_RECORD)
				{

					// Updates the contact id
					this.hidContactID.Value = e.OutputParameters["contactID"].ToString();

					// Shows successful registration
					script.Append("ShowSuccessMsg('");
					script.Append(this.ajaxMngr.ClientID);
					script.Append("', 'Success', 'The new supplier contact has been registered successfully. Kindly allow our personnel to process your form within 3~5 business days.');");

				}

				#region Updates the current session
				else
				{

					if (this.hidContactID.Value.Equals(Session[B2BConstants.CONTACT_ID].ToString()) &&
						this.TransactionMode == B2BConstants.DB_UPDATE_RECORD)
					{

						Session[B2BConstants.CONTACT_NAME] = this.txtContactName.Text.Trim();
						Session[B2BConstants.CONTACT_EMAIL] = this.txtContactEmail.Text.Trim();

					}

					script.Append("OnCloseSupplierContactLookup('");
					script.Append(this.hidAjaxID.Value);
					script.Append("', ");
					script.Append(this.hidContactID.Value);
					script.Append(", '");
					script.Append(this.chkContactPrimary.Checked.ToString());
					script.Append("', 'false');");

				}
				#endregion

				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Success", script.ToString(), true);

			}

			else
			{

				// Checks for error message
				if (errorMsg.Length > 0)
					errorMsg = Server.HtmlEncode(errorMsg.Replace("\r\n", ""));

				else if (retError == -2)
					errorMsg = "The e-mail address you have specified already exists.";

				else if (this.TransactionMode == B2BConstants.DB_INSERT_RECORD)
					errorMsg = "An error occurred while submitting the registration.<br />Please try again.";

				else
					errorMsg = "An error occurred while updating your contact information.<br />Please try again.";

				ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error",
					String.Format("ShowAlertMessage('{0}');", errorMsg), true);

			}
		}
		#endregion
	}
}