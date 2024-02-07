using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GARMCO.AMS.B2B.eTendering.Website.CommonObject
{
	public partial class SuccessDialog : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{

				// Sets the default button when enter key is pressed
				this.form1.DefaultButton = this.btnOK.UniqueID;

				// Sets the warning message
				this.litSuccess.Text = Server.UrlDecode(Request.QueryString["successMsg"]);

				// Sets the button events
				this.btnOK.OnClientClick = String.Format("OnCloseSuccessMsg('{0}', '{1}', 'true'); return false;",
					Request.QueryString["ajaxID"], Request.QueryString["buttonID"]);

			}
		}
	}
}