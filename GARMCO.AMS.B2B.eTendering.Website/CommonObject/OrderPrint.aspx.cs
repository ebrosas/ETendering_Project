using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GARMCO.AMS.B2B.Admin.DAL;
using GARMCO.AMS.B2B.eTendering.DAL;
using GARMCO.AMS.B2B.Report.eTendering;
using GARMCO.AMS.B2B.Utility;

namespace GARMCO.AMS.B2B.eTendering.Website.CommonObject
{
	public partial class OrderPrint : System.Web.UI.Page
	{
		#region Properties
		public List<SupplierOrderDetailItem> SupplierOrderDetailList
		{
			get
			{
				List<SupplierOrderDetailItem> list = Session[B2BConstants.ITEM_LIST_SUPPLIER_ORDER_DETAIL] as
					List<SupplierOrderDetailItem>;
				if (list == null)
					Session[B2BConstants.ITEM_LIST_SUPPLIER_ORDER_DETAIL] = list = new List<SupplierOrderDetailItem>();

				return list;
			}
		}
		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{

				// Configure splitter height
				ScriptManager.RegisterStartupScript(this, this.GetType(), "Height", "Sys.Application.add_load(ConfigureFolderContentHeight);", true);

				// Declares a reporting variable
				Telerik.Reporting.Report rep = null;

				// Checks what to print
				B2BConstants.PrintType printType = (B2BConstants.PrintType)Enum.Parse(typeof(B2BConstants.PrintType),
					Request.QueryString["printType"]);

				#region RFQ Print-out
				if (printType == B2BConstants.PrintType.RFQDetail)
				{

					PublishedRFQSupplierRep rfqRep = new PublishedRFQSupplierRep();

					#region Sets the parameters
					var supplierDataTable = new SupplierRepository().GetSupplier(B2BConstants.DB_SELECT_SPECIFIC, Convert.ToInt32(Session[B2BConstants.CONTACT_SUPPLIER_NO]),
						String.Empty, 0, String.Empty, String.Empty, String.Empty, 0, 0, 10, String.Empty);

					if (supplierDataTable != null && supplierDataTable.Any())
					{

						var supplierRow = supplierDataTable.First();

						rfqRep.ReportParameters["supplierName"].Value = supplierRow.SupplierName;
						rfqRep.ReportParameters["supplierAddr"].Value = supplierRow.SupplierAddress;
						rfqRep.ReportParameters["supplierCity"].Value = supplierRow.SupplierCity;
						rfqRep.ReportParameters["supplierState"].Value = supplierRow.SupplierState;
						rfqRep.ReportParameters["supplierCountry"].Value = supplierRow.SupplierCountry;
						rfqRep.ReportParameters["supplierPostalCode"].Value = supplierRow.SupplierPostalCode;

					}
					#endregion

					#region Sets the data source
					List<PublishedRFQDetailItem> rfqDetailItemList = new List<PublishedRFQDetailItem>();
					var orderRequisitionDetails = new OrderRequisitionDetailRepository().GetOrderRequisitionDetail(Convert.ToDouble(Request.QueryString["orderNo"]));

					if (orderRequisitionDetails != null)
					{

						foreach (var orderRequitionDetail in orderRequisitionDetails)
						{

							PublishedRFQDetailItem item = new PublishedRFQDetailItem(orderRequitionDetail, true, false);

							// Adds the attachment
							var detailAttachments = new OrderRequisitionDetailAttachmentRepository().GetOrderRequisitionDetailAttachment(B2BConstants.DB_SELECT_ALL, orderRequitionDetail.OrderDetNo, orderRequitionDetail.OrderDetLineNo, 0, -1, -1, false, 0);

							if (detailAttachments != null)
							{

								foreach (var detailAttachment in detailAttachments)
									item.OrderDetAttachList.Add(new PublishedRFQDetailAttachItem(detailAttachment));

							}
                            
							// Adds to the list
							rfqDetailItemList.Add(item);

						}
					}

					rfqRep.DataSource = rfqDetailItemList;
					#endregion

					// Sets the report
					rep = rfqRep;

				}
				#endregion

				#region Prints the supplier's bids
				else if (printType == B2BConstants.PrintType.SupplierOrder)
				{

					SupplierOrderRep sodRep = new SupplierOrderRep();

					#region Sets the parameters
					sodRep.ReportParameters["sodOrderNo"].Value = Request.QueryString["sodOrderNo"];
					sodRep.ReportParameters["sodSupplierName"].Value = Server.UrlDecode(Request.QueryString["sodSupplierName"]);
					sodRep.ReportParameters["sodOrderClosingDate"].Value = Server.UrlDecode(Request.QueryString["sodOrderClosingDate"]).Replace("GMT  3", "GMT +3");
					sodRep.ReportParameters["sodCurrencyCode"].Value = Request.QueryString["sodCurrencyCode"];
					sodRep.ReportParameters["sodDeliveryTerm"].Value = Request.QueryString["sodDeliveryTerm"];
					sodRep.ReportParameters["sodValidityPeriod"].Value = Request.QueryString["sodValidityPeriod"];
					sodRep.ReportParameters["sodDeliveryTime"].Value = Request.QueryString["sodDeliveryTime"];
					sodRep.ReportParameters["sodDeliveryPt"].Value = Server.UrlDecode(Request.QueryString["sodDeliveryPt"]);
					sodRep.ReportParameters["sodPaymentTerm"].Value = Server.UrlDecode(Request.QueryString["sodPaymentTerm"]);
					sodRep.ReportParameters["sodBuyer"].Value = Server.UrlDecode(Request.QueryString["sodBuyer"]);
					sodRep.ReportParameters["sodOrderStatus"].Value = Request.QueryString["sodOrderStatus"];
					#endregion

					// Sets the data source
					sodRep.DataSource = this.SupplierOrderDetailList;

					// Sets the report
					rep = sodRep;

				}
				#endregion

				// Sets the report to the viewer
				this.repViewer.ReportSource = rep;

			}
		}
	}
}