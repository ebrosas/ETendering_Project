using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;
using GARMCO.AMS.B2B.Utility;
using GARMCO.AMS.B2B.Admin.DAL;
using System.Collections.Generic;

namespace GARMCO.AMS.B2B.Report.eTendering
{
	/// <summary>
	/// Summary description for SupplierBidTenderCommitteeRep.
	/// </summary>
	public partial class SupplierBidTenderCommitteeRep : Telerik.Reporting.Report
	{
		#region Properties
		public List<SupplierOrderRequisitionItem> SupplierBidList { get; set; }
		#endregion

		#region Private Data Members
		private double _totalOrdersBD = 0;
		private double _totalOtherChargesBD = 0;
		#endregion

		public SupplierBidTenderCommitteeRep()
		{
			//
			// Required for telerik Reporting designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		#region Data Binding
		private void groupHeaderSection_ItemDataBound(object sender, EventArgs e)
		{
			// Resets the totals
			this._totalOrdersBD = 0;
			this._totalOtherChargesBD = 0;

			Telerik.Reporting.Processing.ReportSection section = sender as
				Telerik.Reporting.Processing.ReportSection;
			if (section != null)
			{

				int totalLines = 0;
				int totalBids = 0;
				double totalCost = 0;
				double totalOtherCharges = 0;
				int supOrderSupplierNo = Convert.ToInt32((section.ChildElements["txtSupOrderSupplierNo"] as Telerik.Reporting.Processing.TextBox).Value);
				List<SupplierOrderRequisitionItem> supOrderDetailList = this.SupplierBidList.FindAll(tempItem => tempItem.SupOrderSupplierNo == supOrderSupplierNo &&
						tempItem.OrderPrimary == (int)B2BConstants.SupplierOrderLineType.SupplierOrderDetail);
				List<SupplierOrderRequisitionItem> supOrderDetailAltList = this.SupplierBidList.FindAll(tempItem => tempItem.SupOrderSupplierNo == supOrderSupplierNo &&
					tempItem.OrderPrimary == (int)B2BConstants.SupplierOrderLineType.SupplierOrderDetailAlt);
				List<SupplierOrderRequisitionItem> supOrderOtherChgList = this.SupplierBidList.FindAll(tempItem => tempItem.SupOrderSupplierNo == supOrderSupplierNo &&
					tempItem.OrderPrimary == (int)B2BConstants.SupplierOrderLineType.SupplierOtherCharge);

				// Retrieves the total lines supplier bidded
				foreach (SupplierOrderRequisitionItem supOrderItem in supOrderDetailList)
				{

					// Computes the total lines
					if (supOrderItem.OrderPrimary == (int)B2BConstants.SupplierOrderLineType.SupplierOrderDetail)
						totalLines++;

					// Computes the total lines supplier bidded
					if (supOrderItem.SupOrderDetUnitCostBD > 0 ||
						supOrderDetailAltList.Exists(tempItem => tempItem.SupOrderDetUnitCostBD > 0 && tempItem.SupOrderDetSODID == supOrderItem.SupOrderDetSODID))
						totalBids++;

					#region Computes for the total cost
					if (supOrderItem.SupOrderDetUnitCostBD > 0 && supOrderDetailAltList.Count == 0)
						totalCost += (supOrderItem.SupOrderDetUnitCostBD * supOrderItem.SupOrderDetQuantity);

					else
					{

						double minCost = supOrderItem.SupOrderDetUnitCostBD * supOrderItem.SupOrderDetQuantity;
						foreach (SupplierOrderRequisitionItem minItem in supOrderDetailAltList.FindAll(tempItem => tempItem.SupOrderDetSODID == supOrderItem.SupOrderDetSODID))
						{

							if (minCost == 0)
								minCost = minItem.SupOrderDetUnitCostBD * minItem.SupOrderDetQuantity;

							else if (minCost > (minItem.SupOrderDetUnitCostBD * minItem.SupOrderDetQuantity))
								minCost = minItem.SupOrderDetUnitCostBD * minItem.SupOrderDetQuantity;

						}

						totalCost += minCost;

					}

					// Computes total selected
					if (supOrderItem.SupOrderDetSelected)
						this._totalOrdersBD += (supOrderItem.SupOrderDetUnitCostBD * supOrderItem.SupOrderDetQuantity);
					#endregion

				}

				// Computes all selected alternative offers
				if (supOrderDetailAltList.Count > 0)
					this._totalOrdersBD += supOrderDetailAltList.FindAll(tempItem =>
						tempItem.SupOrderDetSelected).Sum(tempItem => (tempItem.SupOrderDetUnitCostBD * tempItem.SupOrderDetQuantity));

				// Computes for the total charges
				if (supOrderOtherChgList.Count > 0)
					totalOtherCharges = supOrderOtherChgList.Sum(tempItem => tempItem.SupOrderDetUnitCostBD);

				// Computes all selected other charges
				if (supOrderOtherChgList.Count > 0)
					this._totalOtherChargesBD = supOrderOtherChgList.FindAll(
						tempItem => tempItem.SupOrderDetSelected).Sum(tempItem => tempItem.SupOrderDetUnitCostBD);

				Telerik.Reporting.Processing.TextBox txtSupOrderTotalBids = section.ChildElements["txtSupOrderTotalBids"]  as
					Telerik.Reporting.Processing.TextBox;
				txtSupOrderTotalBids.Value = String.Format("{0}/{1}", totalBids, totalLines);
				if (totalBids < totalLines)
					txtSupOrderTotalBids.Style.Color = Color.Red;

				(section.ChildElements["txtSupOrderTotalCost"] as Telerik.Reporting.Processing.TextBox).Value = totalCost.ToString("#,##0.000");
				(section.ChildElements["txtSupOrderOtherCharge"] as Telerik.Reporting.Processing.TextBox).Value = totalOtherCharges.ToString("#,##0.000");
				(section.ChildElements["txtSupOrderGrandTotal"] as Telerik.Reporting.Processing.TextBox).Value = (totalCost + totalOtherCharges).ToString("#,##0.000");

				if (supOrderDetailAltList.Count > 0)
				{

					(section.ChildElements["txtRemarks"] as Telerik.Reporting.Processing.TextBox).Visible = true;
					(section.ChildElements["txtWithAlternative"] as Telerik.Reporting.Processing.TextBox).Visible = true;

				}
			}
		}

		private void detail_ItemDataBound(object sender, EventArgs e)
		{
			Telerik.Reporting.Processing.ReportSection section = sender as
				Telerik.Reporting.Processing.ReportSection;
			if (section != null)
			{

				Telerik.Reporting.Processing.Panel panDetail = section.ChildElements["panDetail"] as
					Telerik.Reporting.Processing.Panel;
				if (panDetail != null)
				{

					#region Checks if order line is the lowest and selected
					Telerik.Reporting.Processing.TextBox txtSupOrderDetLowest = panDetail.ChildElements["txtSupOrderDetLowest"] as
						Telerik.Reporting.Processing.TextBox;
					if (Convert.ToBoolean(txtSupOrderDetLowest.Value))
					{

						Telerik.Reporting.Processing.PictureBox imgLowest = panDetail.ChildElements["imgLowest"] as
							Telerik.Reporting.Processing.PictureBox;
						Telerik.Reporting.Processing.PictureBox imgLowestBlank = panDetail.ChildElements["imgLowestBlank"] as
							Telerik.Reporting.Processing.PictureBox;

						imgLowest.Visible = true;
						imgLowestBlank.Visible = false;

					}

					Telerik.Reporting.Processing.TextBox txtSupOrderDetSelected = panDetail.ChildElements["txtSupOrderDetSelected"] as
						Telerik.Reporting.Processing.TextBox;
					bool supOrderDetSelected = Convert.ToBoolean(txtSupOrderDetSelected.Value);
					if (supOrderDetSelected)
						panDetail.Style.BackgroundColor = Color.FromArgb(197, 253, 170);
					#endregion

					#region Checks if primary, alternative or other charges
					B2BConstants.SupplierOrderLineType supOrderPrimary = (B2BConstants.SupplierOrderLineType)Enum.Parse(typeof(B2BConstants.SupplierOrderLineType),
						(panDetail.ChildElements["txtOrderPrimary"] as Telerik.Reporting.Processing.TextBox).Value.ToString());
					if (supOrderPrimary == B2BConstants.SupplierOrderLineType.SupplierOrderDetailAlt && !supOrderDetSelected)
						panDetail.Style.BackgroundColor = Color.FromArgb(255, 207, 136);

					else if (supOrderPrimary == B2BConstants.SupplierOrderLineType.SupplierOtherCharge)
					{

						panDetail.Style.BackgroundColor = Color.FromArgb(204, 255, 255);
						(panDetail.ChildElements["txtSupOrderDetLineNo"] as Telerik.Reporting.Processing.TextBox).Value = String.Empty;

					}
					#endregion

					#region Processes the Supplier Order
					double supOrderDetTotalUnitCostBD = Convert.ToDouble((panDetail.ChildElements["txtSupOrderDetTotalUnitCostBD"] as
						Telerik.Reporting.Processing.TextBox).Value);
					int supOrderDetSODID = Convert.ToInt32((panDetail.ChildElements["txtSupOrderDetSODID"] as
						Telerik.Reporting.Processing.TextBox).Value);
					if (supOrderDetTotalUnitCostBD == 0 && !this.SupplierBidList.Exists(tempItem => tempItem.OrderPrimary == (int)B2BConstants.SupplierOrderLineType.SupplierOrderDetailAlt &&
						tempItem.SupOrderDetSODID == supOrderDetSODID && tempItem.SupOrderDetUnitCostBD > 0))
					{

						(panDetail.ChildElements["txtSupOrderDetRemarks"] as Telerik.Reporting.Processing.TextBox).Value = "Supplier did not bid";
						panDetail.Style.Color = Color.Red;

					}
					#endregion
				}
			}
		}

		private void groupFooterSection_ItemDataBound(object sender, EventArgs e)
		{
			Telerik.Reporting.Processing.ReportSection section = sender as
				Telerik.Reporting.Processing.ReportSection;
			if (section != null)
			{

				(section.ChildElements["txtGrantTotal"] as Telerik.Reporting.Processing.TextBox).Value =
					String.Format("BD {0:#,##0.000} (BD {1:#,##0.000} including other charges)",
						this._totalOrdersBD, (this._totalOrdersBD + this._totalOtherChargesBD));

			}
		}

		private void reportFooterSection_ItemDataBound(object sender, EventArgs e)
		{
			Telerik.Reporting.Processing.ReportSection section = sender as
				Telerik.Reporting.Processing.ReportSection;
			if (section != null)
			{

				#region Updates the legend
				(section.ChildElements["imgRecommended"] as Telerik.Reporting.Processing.Shape).Style.BackgroundColor = Color.FromArgb(197, 253, 170);
				(section.ChildElements["imgAlternative"] as Telerik.Reporting.Processing.Shape).Style.BackgroundColor = Color.FromArgb(255, 207, 136);
				(section.ChildElements["imgOtherCharges"] as Telerik.Reporting.Processing.Shape).Style.BackgroundColor = Color.FromArgb(204, 255, 255);
				(section.ChildElements["imgDidNotBid"] as Telerik.Reporting.Processing.Shape).Style.BackgroundColor = Color.Red;
				#endregion

			}
		}
		#endregion
	}
}