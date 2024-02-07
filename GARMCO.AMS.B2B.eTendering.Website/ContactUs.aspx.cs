using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GARMCO.AMS.B2B.Utility;
using GARMCO.AMS.Common.Web;

namespace GARMCO.AMS.B2B.eTendering.Website
{
	public partial class ContactUs : BaseWebForm
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{

				// Sets the default button and focus
				this.Master.DefaultButton = this.btnSend.UniqueID;
				this.txtFrom.Focus();

				// Checks if authenticated
				if (this.Page.User.Identity.IsAuthenticated && Session[B2BConstants.CONTACT_EMAIL] != null)
				{

					this.txtFrom.Text = Session[B2BConstants.CONTACT_EMAIL].ToString();
					this.txtFrom.Enabled = false;

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
		protected void btnSend_Click(object sender, EventArgs e)
		{
			this.Validate();

			if (!this.IsValid) return;

			string errorMsg = String.Empty;
			int retError = 0;

			#region Sends email to GARMCO
			#region Creates the recipient
			List<MailAddress> toList = new List<MailAddress>();
			string[] recipients = ConfigurationManager.AppSettings["PURCHASING_TEAM"].Split(';');
			foreach (string recipient in recipients)
			{

				if (!String.IsNullOrEmpty(recipient))
					toList.Add(new MailAddress(recipient));

			}

			List<MailAddress> ccList = new List<MailAddress>();
			ccList.Add(new MailAddress(this.txtFrom.Text));

			List<MailAddress> bccList = new List<MailAddress>();
			string[] bccRecipients = ConfigurationManager.AppSettings["SUPPORT_TEAM"].Split(';');
			foreach (string recipient in bccRecipients)
			{

				if (!String.IsNullOrEmpty(recipient))
					bccList.Add(new MailAddress(recipient));

			}
			#endregion

			#region Set the subject and body
			string subject = String.Format("GARMCO e-Tendering Feedback: {0}", this.txtSubject.Text.Trim());
			string body = this.txtMessage.Content;
			#endregion

			this.Master.SendEmail(toList, ccList, bccList, subject, body, null,
				ConfigurationManager.AppSettings["eTenderingAdminName"], ConfigurationManager.AppSettings["eTenderingAdminEmail"]);
			#endregion

			#region Sends an acknowledgement to the user
			toList.Clear();
			toList.Add(new MailAddress(this.txtFrom.Text));

			ccList.Clear();
			bccList.Clear();

			subject = String.Format("GARMCO e-Tendering Feedback Acknowledgement: {0}", this.txtSubject.Text.Trim());
			body = B2BFunctions.RetrieveXmlMessage(Server.MapPath("~/Messages/FeedbackAcknowledgement.xml")).Replace("\r\n", "<br />");

			this.Master.SendEmail(toList, ccList, bccList, subject, body, null,
				ConfigurationManager.AppSettings["eTenderingAdminName"], ConfigurationManager.AppSettings["eTenderingAdminEmail"]);
			#endregion

			this.Session["msg"] = "Thank you very much for your feedback.";
			Response.Redirect("Success.aspx", false);
		}

		protected void btnReset_Click(object sender, EventArgs e)
		{
			// Clears the current entry
			this.txtFrom.Text = String.Empty;
			this.txtSubject.Text = String.Empty;
			this.txtMessage.Content = String.Empty;
		}
		#endregion
	}
}