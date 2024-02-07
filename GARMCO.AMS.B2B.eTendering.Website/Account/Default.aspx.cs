using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GARMCO.AMS.Common.Web;

namespace GARMCO.AMS.B2B.eTendering.Website.Account
{
	public partial class Default : BaseWebForm
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{

				// Sets the selected menu
				this.Master.SetSelectedMenu(Request.Url.PathAndQuery);

			}
		}
	}
}