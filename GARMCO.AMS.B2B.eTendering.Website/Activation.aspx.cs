using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using GARMCO.AMS.B2B.eTendering.DAL;
using GARMCO.AMS.B2B.Utility;
using GARMCO.AMS.Common.Web;
using Telerik.Web.UI;

namespace GARMCO.AMS.B2B.eTendering.Website
{
	public partial class Activation : BaseWebForm
	{
		#region Properties
		public int LoginOption
		{
			get
			{
				int logingOption = 2;
				if (ViewState["LoginOption"] != null)
					logingOption = Convert.ToInt32(ViewState["LoginOption"]);

				return logingOption;
			}

			set
			{
				ViewState["LoginOption"] = value;
			}
		}
		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{

				// Sets the default button and focus
				this.Master.DefaultButton = this.btnActivate.UniqueID;
				this.txtPassword.Focus();

				// Customizes the character set for the captcha control
				this.captchaImg.CaptchaImage.CharSet = "abcdefghijklmnpqrstuvwxyz123456789";
				this.captchaImg.CaptchaImage.TextChars = CaptchaPossibleChars.CustomCharSet;

				// Retrieves the email address thru activation link
				this.objContact.Select();

			}
		}

		#region Override Methods
		protected override void OnInit(EventArgs e)
		{
			// Don't check the session
			this.IsToCheckSession = false;

			base.OnInit(e);
		}
		#endregion

		#region Action Buttons
		protected void btnActivate_Click(object sender, EventArgs e)
		{
			this.Validate();
			if (this.IsValid)
			{

				this.objContact.SelectParameters["mode"].DefaultValue = "1";
				this.objContact.Select();

			}
		}
		#endregion

		#region Database Access
		protected void objContact_Selected(object sender, ObjectDataSourceStatusEventArgs e)
		{
			var dataTable = e.ReturnValue as IEnumerable<SupplierContact>;

			if (dataTable != null && dataTable.Count() > 0)
			{

				#region Retrieves the contact information
                var row = dataTable.First();
				string errorMsg = String.Empty;

				// Checks the current login option
				if (this.LoginOption == 2)
				{

					// Checks if the account has been activated already
					if (row.ContactActive)
						errorMsg = "Your account has been activated already.<br />Please proceed to login page.";

					else
					{

						// Sets the login option
						this.LoginOption = 1;

						this.litContactEmail.Text = row.ContactEmail;

						// Shows the activation button
						this.btnActivate.Visible = true;

					}
				}

				else if (this.LoginOption == 1)
				{

					// Checks the password
					if (!this.txtPassword.Text.Equals(B2BFunctions.Decrypt(row.ContactPassword)))
						errorMsg = "The password you have provided is incorrect, please re-enter your password.";

					#region Login the current user
					else
					{

						#region Stores the contact information
						Session[B2BConstants.CONTACT_ID] = row.ContactID;
						Session[B2BConstants.CONTACT_NAME] = row.ContactName;
						Session[B2BConstants.CONTACT_EMAIL] = row.ContactEmail;
						Session[B2BConstants.CONTACT_SUPPLIER_NO] = row.SupplierNo;
						Session[B2BConstants.CONTACT_PRIMARY] = row.ContactPrimary;
						#endregion

						#region Updates the contact info
						this.objContact.UpdateParameters["contactID"].DefaultValue = row.ContactID.ToString();
						this.objContact.UpdateParameters["contactCreatedModifiedBy"].DefaultValue = row.ContactID.ToString();
						this.objContact.UpdateParameters["contactCreatedModifiedName"].DefaultValue = row.ContactName;
						this.objContact.Update();
						#endregion

					}
					#endregion
				}
				#endregion

				// Shows the error message if there is
				if (errorMsg.Length > 0)
					ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Error",
						String.Format("ShowAlertMessageWithDelay('{0}');", errorMsg), true);

			}

			else
			{

				ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Error",
					"ShowAlertMessageWithDelay('Sorry, the activation key you have specified is invalid.');", true);

			}
		}

		protected void objContact_Updated(object sender, ObjectDataSourceStatusEventArgs e)
		{
			int retError = Convert.ToInt32(e.OutputParameters["retError"]);
			string errorMsg = e.OutputParameters["errorMsg"].ToString();

			if (retError == B2BConstants.DB_STATUS_OK)
			{

				// Authenticate the current user
				FormsAuthentication.RedirectFromLoginPage(this.litContactEmail.Text, false);

				// Redirects the default page
				Response.Redirect("~/Account/Default.aspx", false);

			}

			else
				ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Error",
					"ShowAlertMessageWithDelay('Sorry, an error occurred while activating your accout.<br />Please try again.');", true);
		}
		#endregion
	}
}