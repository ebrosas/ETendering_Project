using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GARMCO.AMS.B2B.eTendering.Website.CommonObject
{
	public partial class ConfirmDialog : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{

				// Sets the default button when enter key is pressed
				this.form1.DefaultButton = this.btnYes.UniqueID;

				// Sets the warning message
				this.litWarning.Text = Server.UrlDecode(Request.QueryString["warningMsg"]);

				// Sets the button events
				this.btnYes.OnClientClick = String.Format("OnCloseWarningMsg('{0}', '{1}', 'true'); return false;",
					Request.QueryString["ajaxID"], Request.QueryString["buttonID"]);
				this.btnNo.OnClientClick = String.Format("OnCloseWarningMsg('{0}', '{1}', 'false'); return false;",
					Request.QueryString["ajaxID"], Request.QueryString["buttonID"]);

			}
		}
	}
}