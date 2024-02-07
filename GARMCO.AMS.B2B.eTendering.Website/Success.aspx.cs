using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GARMCO.AMS.B2B.eTendering.Website
{
	public partial class Success : System.Web.UI.Page
	{
		protected string Message
		{
			get
			{
				return
					this.Session["msg"] != null
					? (string)this.Session["msg"]
					: string.Empty;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{

				// Sets the success message
				this.litSuccessMsg.Text = this.Message;
			}
		}
	}
}