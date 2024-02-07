using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using GARMCO.AMS.B2B.eTendering.DAL;
using GARMCO.AMS.B2B.eTendering.Website.Helpers;
using GARMCO.AMS.B2B.Utility;
using GARMCO.AMS.Common.Web;
using Telerik.Web.UI;

namespace GARMCO.AMS.B2B.eTendering.Website
{
	public partial class Default : BaseWebForm
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
                bool isUnderMaintenance = ConfigurationManager.AppSettings["UnderMaintenance"].Trim() == "1" ? true : false;

                if (isUnderMaintenance)
                {
                    Response.Redirect(UILookup.PAGE_UNDER_MAINTENANCE, false);
                }
                else
                {
                    // Sets the default button and focus
                    //this.Master.DefaultButton = this.btnLogin.UniqueID;
                    this.txtEmail.Focus();

                    // Customizes the character set for the captcha control
                    this.captchaImg.CaptchaImage.CharSet = "abcdefghijkmnpqrstuvwxyz23456789";
                    this.captchaImg.CaptchaImage.TextChars = CaptchaPossibleChars.CustomCharSet;
                }
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
		protected void btnLogin_Click(object sender, EventArgs e)
		{
			// Checks the user
			this.Validate();
			if (this.IsValid)
				this.objContact.Select();
		}
		#endregion

		#region Database Access
		protected void objContact_Selected(object sender, ObjectDataSourceStatusEventArgs e)
		{
			var dataTable = e.ReturnValue as IEnumerable<SupplierContact>;

			if (dataTable != null && dataTable.Count() > 0)
			{

                var row = dataTable.First();
				string errorMsg = String.Empty;

				// Checks if registered and not yet reviewed
				if (!row.ContactReviewed)
					errorMsg = "Sorry, your account is still in-process at the moment. You will be receiving an email notification if the process has been completed.";

				// Checks if registered and not yet activated
				else if (!row.ContactActive)
					errorMsg = "Your account has not been activated yet, please activate your account from the e-mail that has been sent to you.";

				// Checks the password
				else if (!this.txtPassword.Text.Equals(B2BFunctions.Decrypt(row.ContactPassword)))
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

					// Authenticate the current user
					FormsAuthentication.RedirectFromLoginPage(row.ContactEmail, this.chkRememberMe.Checked);

					// Checks if url is blank
					if (String.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
						Response.Redirect("~/Account/PublishedRFQView.aspx", false);

					else
						Response.Redirect(Request.QueryString["ReturnUrl"], false);

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
					"ShowAlertMessageWithDelay('Incorrect username or password!<br />Please check and try again');", true);

			}
		}
		#endregion
	}
}