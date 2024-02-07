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

namespace GARMCO.AMS.B2B.eTendering.Website
{
	public partial class ErrorMessage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{

				bool sendToSupport = true;

				// Retrieve the last error
				Exception exception = Session[B2BConstants.EXCEPTION_ERROR] as Exception;
				if (exception != null)
				{

					// Show the stack panel
					this.panStackError.Visible = true;

					this.litURL.Text = Request.QueryString["url"];
					this.litSource.Text = exception.Source;
					this.litMessage.Text = exception.Message;
					this.litInnerMsg.Text = exception.InnerException != null ? exception.InnerException.Message : String.Empty;
					this.litStackTrace.Text = exception.StackTrace;

				}

				#region Checks the error code
				else if (!String.IsNullOrEmpty(Request.QueryString["error"]))
				{

					int errorCode = Convert.ToInt32(Request.QueryString["error"]);
					if (errorCode == 404)
						this.litError.Text = "The browser is able to connect to the website, but the webpage is not found. This error is sometimes caused because the webpage is temporarily unavailable or because the webpage has been deleted.";

					else if (errorCode == 500)
						this.litError.Text = "The website you are visiting had a server problem that prevented the webpage from displaying. It often occurs as a result of website maintenance or because of a programming error on interactive websites that use scripting.";

					else if (errorCode == B2BConstants.PAGE_ERROR_SESSION_END)
					{

						this.litError.Text = "Sorry, your session is already expired.<br />Please login again.";
						sendToSupport = false;

					}
				}
				#endregion

				#region Sends the information to technical support team
				if (sendToSupport)
				{

					#region Creates recipients
					List<MailAddress> toList = new List<MailAddress>();
					string[] supportList = ConfigurationManager.AppSettings["SUPPORT_TEAM"].Split(B2BConstants.LIST_SEPARATOR);
					foreach (string support in supportList)
					{

						if (support.Length > 0)
							toList.Add(new MailAddress(support));

					}
					#endregion

					#region Sets the subject and body
					string subject = "GARMCO e-Tendering: Exception Error";
					StringBuilder body = new StringBuilder();

					if (exception != null)
					{

						body.Append("<b>Offending URL: </b>");
						body.Append(this.litURL.Text);
						body.Append("<br />");

						body.Append("<b>Source: </b>");
						body.Append(this.litSource.Text);
						body.Append("<br />");

						body.Append("<b>Message: </b>");
						body.Append(this.litMessage.Text);
						body.Append("<br />");

						body.Append("<b>Inner Message: </b>");
						body.Append(this.litInnerMsg.Text);
						body.Append("<br />");

						body.Append("<b>Stack Trace: </b>");
						body.Append(this.litStackTrace.Text);
						body.Append("<br />");

					}

					else
						body.Append(this.litError.Text);

					// Includes addition information if available
					if (this.Page.User.Identity.IsAuthenticated && Session[B2BConstants.CONTACT_ID] != null)
					{

						body.Append("<br /><br />---- Current Supplier ----------------<br />");
						body.Append("<b>Supplier No: </b>");
						body.Append(Session[B2BConstants.CONTACT_SUPPLIER_NO].ToString());
						body.Append("<br />");

						body.Append("<b>Contact ID: </b>");
						body.Append(Session[B2BConstants.CONTACT_ID].ToString());
						body.Append("<br />");

						body.Append("<b>Contact Name: </b>");
						body.Append(Session[B2BConstants.CONTACT_NAME].ToString());
						body.Append("<br />");

						body.Append("<b>Contact e-Mail: </b>");
						body.Append(Session[B2BConstants.CONTACT_EMAIL].ToString());
						body.Append("<br />");

					}
					#endregion

					// Send the email
					this.Master.SendEmail(toList, null, null, subject, String.Format(B2BFunctions.RetrieveXmlMessage(
							Server.MapPath("~/Messages/TechnicalSupport.xml")).Replace("\r\n", "<br />"), body.ToString()).Replace("\r\n", "<br />"),
						null, ConfigurationManager.AppSettings["eTenderingAdminName"],
						ConfigurationManager.AppSettings["eTenderingAdminEmail"]);

				}
				#endregion
			}
		}
	}
}