using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GARMCO.AMS.B2B.Admin.DAL;
using GARMCO.AMS.B2B.Admin.DAL.Entities;
using GARMCO.AMS.B2B.Admin.DAL.Helpers;
using GARMCO.AMS.B2B.eTendering.Website.Helpers;
using GARMCO.AMS.B2B.Utility;
using GARMCO.AMS.Common.Web;
using Telerik.Web.UI;
using Tendering;
using Tendering.Persistence;

namespace GARMCO.AMS.B2B.eTendering.Website.Account
{
	public partial class PublishedRFQView : BaseWebForm
	{
		#region Constants
		public enum TabID : int
		{
			OpenRFQ,
			QuotedRFQOpen,
			QuotedRFQClosed,
			ClosedRFQ
		};
		#endregion

		#region Private Data Members
		private RadGrid _gvOrderDet = null;
		#endregion

        #region Properties
        private List<SupplierEntity> SupplierBidList
        {
            get
            {
                List<SupplierEntity> list = ViewState["SupplierBidList"] as List<SupplierEntity>;
                if (list == null)
                    ViewState["SupplierBidList"] = list = new List<SupplierEntity>();

                return list;
            }
            set
            {
                ViewState["SupplierBidList"] = value;
            }
        }

        private int SupplierNo
        {
            get
            {
                return UILookup.ConvertObjectToInt(Session[B2BConstants.CONTACT_SUPPLIER_NO]);
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
                ClearForm();
                GetSupplierBids(this.SupplierNo);

				// Sets the selected menu
				this.Master.SetSelectedMenu(Request.Url.PathAndQuery);

				// Set the default button when enter key is pressed
				this.Master.DefaultButton = this.btnSearch.UniqueID;
				this.txtRFQNo.Focus();

			}
		}

		#region Override Methods
		protected override void OnInit(EventArgs e)
		{
			if (!this.IsPostBack)
			{

				#region Checks if previous search filters were passed
				if (!String.IsNullOrEmpty(Request.QueryString["orderNo"]))
				{

					this.objOrderReq.SelectParameters["orderNo"].DefaultValue = Request.QueryString["orderNo"];
					this.txtRFQNo.Text = Request.QueryString["orderNo"];

				}

				if (!String.IsNullOrEmpty(Request.QueryString["orderCloseDateStart"]) && !String.IsNullOrEmpty(Request.QueryString["orderCloseDateEnd"]))
				{

					this.objOrderReq.SelectParameters["orderCloseDateStart"].DefaultValue = Request.QueryString["orderCloseDateStart"];
					this.objOrderReq.SelectParameters["orderCloseDateEnd"].DefaultValue = Request.QueryString["orderCloseDateEnd"];

					this.calRFQClosingDateStart.SelectedDate = Convert.ToDateTime(Request.QueryString["orderCloseDateStart"]);
					this.calRFQClosingDateEnd.SelectedDate = Convert.ToDateTime(Request.QueryString["orderCloseDateEnd"]);

				}

				if (!String.IsNullOrEmpty(Request.QueryString["orderDesc"]))
				{

					this.objOrderReq.SelectParameters["orderDesc"].DefaultValue = Request.QueryString["orderDesc"];
					this.txtRFQDesc.Text = Request.QueryString["orderDesc"];

				}

				if (!String.IsNullOrEmpty(Request.QueryString["viewOption"]))
				{

					this.tabControl.SelectedIndex = Convert.ToInt32(Request.QueryString["viewOption"]);

					TabID currentTab = (TabID)(this.tabControl.SelectedIndex);
					this.multiPg.SelectedIndex = this.tabControl.SelectedIndex;

					if (!String.IsNullOrEmpty(Request.QueryString["startRowIndex"]))
					{

						if (currentTab == TabID.OpenRFQ)
						{

							this.gvList.CurrentPageIndex = Convert.ToInt32(Request.QueryString["startRowIndex"]);

							this.objOrderReq.SelectParameters["mode"].DefaultValue = "0";
							this.objOrderReq.SelectParameters["orderSupplierNo"].DefaultValue = "0";
							this.objOrderReq.SelectParameters["orderStatus"].DefaultValue = Convert.ToInt32(B2BConstants.RequestTypeStatusCode.OpenForBidding).ToString();

						}

						else if (currentTab == TabID.QuotedRFQOpen)
						{

							this.gvQuotedRFQOpen.CurrentPageIndex = Convert.ToInt32(Request.QueryString["startRowIndex"]);

							this.objOrderReq.SelectParameters["mode"].DefaultValue = "3";
							this.objOrderReq.SelectParameters["orderSupplierNo"].DefaultValue = Session[B2BConstants.CONTACT_SUPPLIER_NO].ToString();
							this.objOrderReq.SelectParameters["orderStatus"].DefaultValue = Convert.ToInt32(B2BConstants.RequestTypeStatusCode.OpenForBidding).ToString();

						}

						else if (currentTab == TabID.QuotedRFQClosed)
						{

							this.gvQuotedRFQClosed.CurrentPageIndex = Convert.ToInt32(Request.QueryString["startRowIndex"]);

							this.objOrderReq.SelectParameters["mode"].DefaultValue = "3";
							this.objOrderReq.SelectParameters["orderSupplierNo"].DefaultValue = Session[B2BConstants.CONTACT_SUPPLIER_NO].ToString();
							this.objOrderReq.SelectParameters["orderStatus"].DefaultValue = B2BConstants.REQUEST_TYPE_SPECIAL_CLOSED;

						}

						else if (currentTab == TabID.ClosedRFQ)
						{

							this.gvClosedRFQ.CurrentPageIndex = Convert.ToInt32(Request.QueryString["startRowIndex"]);

							this.objOrderReq.SelectParameters["mode"].DefaultValue = "0";
							this.objOrderReq.SelectParameters["orderSupplierNo"].DefaultValue = "0";
							this.objOrderReq.SelectParameters["orderStatus"].DefaultValue = B2BConstants.REQUEST_TYPE_SPECIAL_CLOSED;

						}
					}

					if (!String.IsNullOrEmpty(Request.QueryString["pageSize"]))
					{

						if (currentTab == TabID.OpenRFQ)
							this.gvList.PageSize = Convert.ToInt32(Request.QueryString["pageSize"]);

						else if (currentTab == TabID.QuotedRFQOpen)
							this.gvQuotedRFQOpen.PageSize = Convert.ToInt32(Request.QueryString["pageSize"]);

						else if (currentTab == TabID.QuotedRFQClosed)
							this.gvQuotedRFQClosed.PageSize = Convert.ToInt32(Request.QueryString["pageSize"]);

						else if (currentTab == TabID.ClosedRFQ)
							this.gvClosedRFQ.PageSize = Convert.ToInt32(Request.QueryString["pageSize"]);

					}
				}

				if (!String.IsNullOrEmpty(Request.QueryString["sort"]))
					this.objOrderReq.SelectParameters["sort"].DefaultValue = Request.QueryString["sort"];

				#endregion

			}

			base.OnInit(e);
		}
		#endregion

		#region Private Methods
		private string SearchCriteria()
		{
			#region Stores the search criteria
			StringBuilder search = new StringBuilder();

			if (!this.objOrderReq.SelectParameters["orderNo"].DefaultValue.Equals("0"))
			{

				search.Append("&orderNo=");
				search.Append(this.objOrderReq.SelectParameters["orderNo"].DefaultValue);

			}

			if (!String.IsNullOrEmpty(this.objOrderReq.SelectParameters["orderCloseDateStart"].DefaultValue) &&
				!String.IsNullOrEmpty(this.objOrderReq.SelectParameters["orderCloseDateEnd"].DefaultValue))
			{

				search.Append("&orderCloseDateStart=");
				search.Append(this.objOrderReq.SelectParameters["orderCloseDateStart"].DefaultValue);
				search.Append("&orderCloseDateEnd=");
				search.Append(this.objOrderReq.SelectParameters["orderCloseDateEnd"].DefaultValue);

			}

			if (!String.IsNullOrEmpty(this.objOrderReq.SelectParameters["orderPriority"].DefaultValue))
			{

				search.Append("&orderPriority=");
				search.Append(this.objOrderReq.SelectParameters["orderPriority"].DefaultValue);

			}

			if (!String.IsNullOrEmpty(this.objOrderReq.SelectParameters["orderDesc"].DefaultValue))
			{

				search.Append("&orderDesc=");
				search.Append(Server.UrlEncode(this.objOrderReq.SelectParameters["orderDesc"].DefaultValue));

			}

			if (!String.IsNullOrEmpty(this.objOrderReq.SelectParameters["sort"].DefaultValue))
			{

				search.Append("&sort=");
				search.Append(this.objOrderReq.SelectParameters["sort"].DefaultValue);

			}

			TabID currentTab = (TabID)this.tabControl.SelectedIndex;

			#region Start row index
			search.Append("&startRowIndex=");
			if (currentTab == TabID.OpenRFQ)
				search.Append(this.gvList.CurrentPageIndex.ToString());

			else if (currentTab == TabID.QuotedRFQOpen)
				search.Append(this.gvQuotedRFQOpen.CurrentPageIndex.ToString());

			else if (currentTab == TabID.QuotedRFQClosed)
				search.Append(this.gvQuotedRFQClosed.CurrentPageIndex.ToString());

			else if (currentTab == TabID.ClosedRFQ)
				search.Append(this.gvClosedRFQ.CurrentPageIndex.ToString());
			#endregion

			#region Page size
			search.Append("&pageSize=");
			if (currentTab == TabID.OpenRFQ)
				search.Append(this.gvList.PageSize.ToString());

			else if (currentTab == TabID.QuotedRFQOpen)
				search.Append(this.gvQuotedRFQOpen.PageSize.ToString());

			else if (currentTab == TabID.QuotedRFQClosed)
				search.Append(this.gvQuotedRFQClosed.PageSize.ToString());

			else if (currentTab == TabID.ClosedRFQ)
				search.Append(this.gvClosedRFQ.PageSize.ToString());
			#endregion

			search.Append("&viewOption=");
			search.Append(this.tabControl.SelectedIndex.ToString());
			#endregion

			return search.ToString();
		}

        private void ClearForm()
        {
            ViewState["SupplierBidList"] = null;
        }
		#endregion

		#region General Events
		protected void tabControl_TabClick(object sender, RadTabStripEventArgs e)
		{
			TabID selectedTab = (TabID)this.tabControl.SelectedIndex;
			switch (selectedTab)
			{

				case TabID.OpenRFQ:
					this.objOrderReq.SelectParameters["mode"].DefaultValue = "0";
					this.objOrderReq.SelectParameters["orderSupplierNo"].DefaultValue = "0";
					this.objOrderReq.SelectParameters["orderStatus"].DefaultValue = Convert.ToInt32(B2BConstants.RequestTypeStatusCode.OpenForBidding).ToString();
					break;

				case TabID.QuotedRFQOpen:
					this.objOrderReq.SelectParameters["mode"].DefaultValue = "3";
					this.objOrderReq.SelectParameters["orderSupplierNo"].DefaultValue = Session[B2BConstants.CONTACT_SUPPLIER_NO].ToString();
					this.objOrderReq.SelectParameters["orderStatus"].DefaultValue = Convert.ToInt32(B2BConstants.RequestTypeStatusCode.OpenForBidding).ToString();
					break;

				case TabID.QuotedRFQClosed:
					this.objOrderReq.SelectParameters["mode"].DefaultValue = "3";
					this.objOrderReq.SelectParameters["orderSupplierNo"].DefaultValue = Session[B2BConstants.CONTACT_SUPPLIER_NO].ToString();
					this.objOrderReq.SelectParameters["orderStatus"].DefaultValue = B2BConstants.REQUEST_TYPE_SPECIAL_CLOSED;
					break;

				case TabID.ClosedRFQ:
					this.objOrderReq.SelectParameters["mode"].DefaultValue = "0";
					this.objOrderReq.SelectParameters["orderSupplierNo"].DefaultValue = "0";
					this.objOrderReq.SelectParameters["orderStatus"].DefaultValue = B2BConstants.REQUEST_TYPE_SPECIAL_CLOSED;
					break;

			}

			// Binds the data
			this.objOrderReq.Select();
		}
		#endregion

		#region Action Buttons
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			// Reset the paging index
			this.gvList.CurrentPageIndex = 0;

			#region Sets the criteria
			this.objOrderReq.SelectParameters["orderNo"].DefaultValue = this.txtRFQNo.Text;
			this.objOrderReq.SelectParameters["orderDesc"].DefaultValue = this.txtRFQDesc.Text;

			if (this.calRFQClosingDateStart.SelectedDate != null && this.calRFQClosingDateEnd.SelectedDate != null)
			{

				this.objOrderReq.SelectParameters["orderCloseDateStart"].DefaultValue = this.calRFQClosingDateStart.SelectedDate.ToString();
				this.objOrderReq.SelectParameters["orderCloseDateEnd"].DefaultValue = this.calRFQClosingDateEnd.SelectedDate.ToString();

			}

			else
			{

				this.objOrderReq.SelectParameters["orderCloseDateStart"].DefaultValue = String.Empty;
				this.objOrderReq.SelectParameters["orderCloseDateEnd"].DefaultValue = String.Empty;

			}
			#endregion

			// Checks which tab is active
			this.tabControl_TabClick(this.tabControl, null);

			//this.objOrderReq.Select();
		}

		protected void btnReset_Click(object sender, EventArgs e)
		{
			// Resets the criteria
			this.txtRFQNo.Text = String.Empty;
			this.txtRFQDesc.Text = String.Empty;
			this.calRFQClosingDateStart.Clear();
			this.calRFQClosingDateEnd.Clear();

            this.btnSearch_Click(this.btnSearch, new EventArgs());
		}
		#endregion

		#region Data Binding
		protected void gvList_PreRender(object sender, EventArgs e)
		{
			foreach (GridDataItem item in this.gvList.MasterTableView.GetItems(GridItemType.AlternatingItem, GridItemType.Item))
			{

				// Change the tooltip
				Button btnExpandCol = item["ExpandColumn"].Controls[0] as Button;
				if (btnExpandCol != null)
					btnExpandCol.ToolTip = item.Expanded ? "Hide Order Details" : "Show Order Details";

			}

            // Hide "OrderSupplierParticipated" column
            GridColumn orderSupplierParticipatedColumn = this.gvList.MasterTableView.RenderColumns.Where(a => a.UniqueName == "OrderSupplierParticipated").FirstOrDefault();
            if (orderSupplierParticipatedColumn != null)
            {
                orderSupplierParticipatedColumn.Display = false;
            }
		}

		protected void gvList_ItemDataBound(object sender, GridItemEventArgs e)
		{
			RadGrid gvBid = sender as RadGrid;

			if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
			{

				#region Formats the display
				GridDataItem item = e.Item as GridDataItem;
				if (item != null)
				{                    
					// Updates the link text
                    if (gvBid.ID.Equals("gvList") && UILookup.ConvertObjectToBolean(item["OrderSupplierParticipated"].Text))
					{

						LinkButton lnkRFQNo = item["Bid"].Controls[0] as LinkButton;
						if (lnkRFQNo != null)
							lnkRFQNo.Text = "Re-Bid";

                    }

                    #region Check if the supplier has quoted on each RFQ
                    if (this.SupplierBidList.Count > 0)
                    {
                        double rfqNo = UILookup.ConvertObjectToDouble(item["OrderNo"].Text);
                        SupplierEntity supplierBid = this.SupplierBidList.Where(a => a.RFQNo == rfqNo).FirstOrDefault();
                        if (supplierBid != null)
                        {
                            item.BackColor = System.Drawing.Color.Yellow;
                        }
                    }
                    #endregion
                }
				#endregion
			}
		}

		protected void gvList_ItemCommand(object sender, GridCommandEventArgs e)
		{
			#region View the RFQ
			if (e.CommandName.Equals(RadGrid.SelectCommandName))
			{

				GridDataItem item = e.Item as GridDataItem;
				if (item != null)
				{

                    var callForTendersId = (int)((double)item.GetDataKeyValue("OrderNo"));
                    var supplierId = this.applicationUserRepository.GetByUsername(this.Username).SupplierId;
                    var tenderCompositeId = new
                    {
                        CallForTendersId = callForTendersId,
                        SupplierId = supplierId
                    };
                    var result = something.Find(tenderCompositeId);
                    Session["callForTendersId"] = callForTendersId;
                    Session["tenderId"] = result.TenderId;

                    if (!result.IsOpen)
                    {
                        if (result.TenderId == 0)
                            Response.Redirect("ClosedCallForTenders.aspx");
                        else
                            Response.Redirect("ViewTender.aspx");
                    }
                    else
                    {
                        if (result.TenderId == 0)
                            Response.Redirect("PlaceTender.aspx");
                        else
                            Response.Redirect("ReviseTender.aspx");
                    }

				}
			}
			#endregion

			#region Shows the order details
			else if (e.CommandName.Equals(RadGrid.ExpandCollapseCommandName))
			{

				if (!e.Item.Expanded)
				{

					GridDataItem item = e.Item as GridDataItem;
					if (item != null)
					{

						#region Collapse other items
                        foreach (GridItem otherItem in e.Item.OwnerTableView.Items)
                        {

                            if (otherItem.Expanded && otherItem != item)
                                otherItem.Expanded = false;

                        }
						#endregion

						// Sets the order detail grid view
						this._gvOrderDet = item.ChildItem.FindControl("gvOrderDet") as RadGrid;

						#region Retrieve Order Details
						this.objOrderReqDet.SelectParameters["orderDetNo"].DefaultValue = item["OrderNo"].Text;
						this.objOrderReqDet.Select();
						#endregion

					}
				}
			}
			#endregion
		}

		protected void gvList_SortCommand(object sender, GridSortCommandEventArgs e)
		{
			this.objOrderReq.SelectParameters["sort"].DefaultValue = String.Format("{0}{1}",
				e.CommandArgument.ToString(), e.NewSortOrder.ToString().Trim().Equals("Descending") ? " DESC" : String.Empty);
		}

		protected void gvOrderDet_PreRender(object sender, EventArgs e)
		{
		}

		protected void gvOrderDet_ItemDataBound(object sender, GridItemEventArgs e)
		{
			if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
			{

				GridDataItem item = e.Item as GridDataItem;
				if (item != null)
				{

					#region Formats the display
					#region Sets the order detail extended description
					int orderDetTotalAttachmentText = Convert.ToInt32(item["OrderDetTotalExtAttachment"].Text);
					ImageButton imgOrderDetAttachText = item["OrderDetAttachText"].Controls[0] as ImageButton;

					if (orderDetTotalAttachmentText == 0)
					{

						imgOrderDetAttachText.ImageUrl = "~/includes/images/attach_small_f3.gif";
						imgOrderDetAttachText.ToolTip = "No extended description";
						imgOrderDetAttachText.Enabled = false;

					}

					else
					{

						StringBuilder script = new StringBuilder();
						script.Append("ShowRFQDetailAttachmentLookup('");
						script.Append(this.Master.AjaxMngr.ClientID);
						script.Append("', ");
						script.Append(item["OrderDetNo"].Text);
						script.Append(", ");
						script.Append(item["OrderDetLineNo"].Text);
						script.Append(", -1, -1, 'false', 0, 'true'); return false;");


						imgOrderDetAttachText.ToolTip = "Click to view the extended description(s)";
						imgOrderDetAttachText.OnClientClick = script.ToString();

					}
					#endregion

					#region Sets the order detail file attachment
					int orderDetTotalAttachmentFile = Convert.ToInt32(item["OrderDetTotalFileAttachment"].Text);
					ImageButton imgOrderDetAttachFile = item["OrderDetAttachFile"].Controls[0] as ImageButton;

					if (orderDetTotalAttachmentFile == 0)
					{

						imgOrderDetAttachFile.ImageUrl = "~/includes/images/attach_small_f3.gif";
						imgOrderDetAttachFile.ToolTip = "No file attachment";
						imgOrderDetAttachFile.Enabled = false;

					}

					else
					{

						StringBuilder script = new StringBuilder();
						script.Append("ShowRFQDetailAttachmentLookup('");
						script.Append(this.Master.AjaxMngr.ClientID);
						script.Append("', ");
						script.Append(item["OrderDetNo"].Text);
						script.Append(", ");
						script.Append(item["OrderDetLineNo"].Text);
						script.Append(", -1, -1, 'false', 1, 'true'); return false;");

						imgOrderDetAttachFile.ToolTip = "Click to view the file attachment(s)";
						imgOrderDetAttachFile.OnClientClick = script.ToString();

					}
					#endregion
					#endregion

				}
			}
		}

		protected void gvOrderDet_ItemCommand(object sender, GridCommandEventArgs e)
		{
			if (e.CommandName.Equals("ViewAttachment"))
			{

				#region Shows the attachments
				GridDataItem item = e.Item as GridDataItem;
				if (item != null)
				{

					StringBuilder script = new StringBuilder();

					script.Append("ShowRFQDetailAttachmentLookup('");
					script.Append(this.Master.AjaxMngr.ClientID);
					script.Append("', ");
					script.Append(item["OrderDetNo"].Text);
					script.Append(", ");
					script.Append(item["OrderDetLineNo"].Text);
					script.Append(", -1, -1, 'false', 2);");

					ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OrderDetAttachment", script.ToString(), true);

				}
				#endregion
			}
		}
		#endregion

		#region Database Access
		protected void objOrderReqDet_Selected(object sender, ObjectDataSourceStatusEventArgs e)
		{
			// Binds the data
			if (this._gvOrderDet != null)
			{

				this._gvOrderDet.DataSource = (IEnumerable<OrderRequisitionDetail>)e.ReturnValue;
				this._gvOrderDet.DataBind();

			}
		}

        private void GetSupplierBids(double supplierNo, double rfqNo = 0)
        {
            try
            {
                string error = String.Empty;
                string innerError = string.Empty;

                // Initialize collection
                this.SupplierBidList.Clear();

                if (supplierNo == 0)
                    return;

                List<SupplierEntity> rawData = ADONetDataService.GetSupplierRFQBids(supplierNo, rfqNo, ref error, ref innerError);
                if (!string.IsNullOrEmpty(error))
                {
                    if (!string.IsNullOrEmpty(innerError))
                        throw new Exception(innerError);
                    else
                        throw new Exception(error);
                }
                else
                {
                    // Save to session
                    this.SupplierBidList = rawData;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error",
                    String.Format("ShowAlertMessage('{0}');", ex.Message.ToString()), true);
            }
        }
		#endregion

        private string Username
        {
            get
            {
                return this.User.Identity.Name;
            }
        }

        protected PublishedRFQView()
        {
            this.applicationUserRepository = new ApplicationUserRepository(Configuration.ConnectionString);
            this.something = new Something(Configuration.ConnectionString);
        }

        private readonly ApplicationUserRepository applicationUserRepository;
        private readonly Something something;
    }
}
namespace Tendering.Persistence
{
    using System.Data.SqlClient;
    using Dapper;

    class Something
    {
        const string Sql =
@"
SELECT
(CASE WHEN a.OrderStatus = '200' AND a.OrderClosingDate > GETDATE() THEN 1 ELSE 0 END)
FROM b2badminuser.OrderRequisition a
WHERE a.OrderNo = @CallForTendersId;
SELECT
a.SupOrderID
FROM b2badminuser.SupplierOrder a
WHERE a.SupOrderOrderNo = @CallForTendersId
AND a.SupOrderSupplierNo = @SupplierId;
";

        public dynamic Find(dynamic id)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var gridReader =
                    connection.QueryMultiple(
                    Sql,
                    new {
                        CallForTendersId = id.CallForTendersId,
                        SupplierId = id.SupplierId
                    });

                return new
                {
                    IsOpen = gridReader.ReadSingle<bool>(),
                    TenderId = gridReader.ReadSingleOrDefault<int>()
                };
            }
        }

        public Something(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private readonly string connectionString;
    }
}