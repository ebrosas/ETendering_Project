using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GARMCO.AMS.B2B.eTendering.DAL;
using GARMCO.AMS.B2B.Utility;
using GARMCO.AMS.Common.Web;

namespace GARMCO.AMS.B2B.eTendering.Website
{
	public partial class ForgotPassword : BaseWebForm
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{

				// Sets the default button and focus
				this.Master.DefaultButton = this.btnSubmit.UniqueID;
				this.txtEmail.Focus();

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
		protected void btnSubmit_Click(object sender, EventArgs e)
		{
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

				// Shows the error message if there is
				if (errorMsg.Length > 0)
					ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Error",
						String.Format("ShowAlertMessageWithDelay('{0}');", errorMsg), true);

				#region Sends the password to the user
				else
				{

					// Creates recipient
					List<MailAddress> toList = new List<MailAddress>{ new MailAddress(row.ContactEmail, row.ContactName) };

					// Sets the subject and body
					string subject = "GARMCO e-Tendering: Request Password";
					string body = String.Format(B2BFunctions.RetrieveXmlMessage(
						Server.MapPath("~/Messages/ForgotPassword.xml")).Replace("\r\n", "<br />"),
						row.ContactName, DateTime.Now.ToString("dd MMM yyyy HH:mm"), row.ContactEmail, B2BFunctions.Decrypt(row.ContactPassword)).Replace("\r\n", "<br />");

					// Send the email
					this.Master.SendEmail(toList, null, null, subject, body,
						null, ConfigurationManager.AppSettings["eTenderingAdminName"],
						ConfigurationManager.AppSettings["eTenderingAdminEmail"]);

					this.Session["msg"] = "Your password has been successfully sent to the e-maill address you have specified.";
					Response.Redirect("~/Success.aspx", false);

				}
				#endregion
			}

			else
			{

				ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Error",
					"ShowAlertMessageWithDelay('Sorry, the e-mail address you specified does not exists.<br />Please check and try again');", true);

			}
		}
		#endregion
	}
}