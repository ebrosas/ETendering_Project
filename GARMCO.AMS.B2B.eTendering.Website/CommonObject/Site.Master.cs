using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GARMCO.AMS.B2B.Utility;
using GARMCO.AMS.Common.Web;
using Telerik.Web.UI;

namespace GARMCO.AMS.B2B.eTendering.Website.CommonObject
{
	public partial class Site : System.Web.UI.MasterPage
	{
		#region Properties
		public RadAjaxManager AjaxMngr
		{
			get
			{
				return this.ajaxMngr;
			}
		}

		public string DefaultButton
		{
			set
			{
				this.form.DefaultButton = value;
			}
		}

		public string SearchUrl
		{
			get
			{
				return this.hidSearchUrl.Value;
			}

			set
			{
				this.hidSearchUrl.Value = value;
			}
		}

		public bool IsRecordModified
		{
			get
			{
				bool isModified = false;
				if (ViewState["IsRecordModified"] != null)
					isModified = Convert.ToBoolean(ViewState["IsRecordModified"]);

				return isModified;
			}

			set
			{
				ViewState["IsRecordModified"] = value;
			}
		}
		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
			}
		}

		#region Public Methods
		public void SetSelectedMenu(string pagefile)
		{
			RadMenu mainMenu = this.loginViewMenu.FindControl("mainMenu") as RadMenu;
			if (mainMenu != null)
			{

				RadMenuItem item = mainMenu.FindItemByUrl(pagefile);
				if (item != null)
					item.HighlightPath();

			}
		}

		public int SendEmail(List<MailAddress> toList, List<MailAddress> ccList, List<MailAddress> bccList,
			string subject, string body, List<string> attachments, string fromName, string fromEmail)
		{
			int retError = 0;

			try
			{

				// Create a new mail message
				MailMessage mailMessage = new MailMessage();

				// Set the originator
				mailMessage.From = new MailAddress(fromEmail, fromName);

				#region Set the recipients
				foreach (MailAddress to in toList)
					mailMessage.To.Add(to);

				if (ccList != null)
				{

					foreach (MailAddress cc in ccList)
						mailMessage.CC.Add(cc);

				}

				if (bccList != null)
				{

					foreach (MailAddress bcc in bccList)
						mailMessage.Bcc.Add(bcc);

				}
				#endregion

				#region Set the subject and body
				mailMessage.Subject = subject;

				StringBuilder bodyList = new StringBuilder();
				bodyList.Append("<div style='font-family: Tahoma; font-size: 10pt'>");
				bodyList.Append(System.Web.HttpUtility.HtmlDecode(body));
				bodyList.Append("</div>");
				mailMessage.Body = bodyList.ToString();
				mailMessage.IsBodyHtml = true;
				#endregion

				// Add attachments
				if (attachments != null)
				{

					foreach (string attach in attachments)
						mailMessage.Attachments.Add(new Attachment(attach));

				}

				// Create an smtp client and send the mail message
				SmtpClient smtpClient = new SmtpClient(
					ConfigurationManager.AppSettings[B2BConstants.GARMCO_SMTP_SERVER]);
				smtpClient.UseDefaultCredentials = true;

				// Send the mail message
				smtpClient.Send(mailMessage);

			}

			catch
			{
				retError = -1;
			}

			return retError;
		}
		#endregion

		#region General Events
		protected void ajaxMngr_AjaxRequest(object sender, AjaxRequestEventArgs e)
		{
			((BaseWebForm)this.mainContent.Page).ajaxMngrBase_AjaxRequest(sender, e);
		}
		#endregion

		#region Action Buttons
		protected void logOut_LoggingOut(object sender, LoginCancelEventArgs e)
		{

		}
		#endregion
	}
}