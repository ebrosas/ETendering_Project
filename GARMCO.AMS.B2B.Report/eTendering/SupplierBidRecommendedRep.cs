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
	/// Summary description for SupplierBidRecommendedRep.
	/// </summary>
	public partial class SupplierBidRecommendedRep : Telerik.Reporting.Report
	{
		#region Private Data Members
		private double _totalOrdersBD = 0;
		#endregion

		public SupplierBidRecommendedRep()
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
		private void reportHeaderSection_ItemDataBound(object sender, EventArgs e)
		{
			// Resets the totals
			this._totalOrdersBD = 0;
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
					if (supOrderPrimary == B2BConstants.SupplierOrderLineType.SupplierOrderDetailAlt)
						panDetail.Style.BackgroundColor = Color.FromArgb(255, 207, 136);

					else if (supOrderPrimary == B2BConstants.SupplierOrderLineType.SupplierOtherCharge)
					{

						panDetail.Style.BackgroundColor = Color.FromArgb(204, 255, 255);
						(panDetail.ChildElements["txtSupOrderDetLineNo"] as Telerik.Reporting.Processing.TextBox).Value = String.Empty;

					}
					#endregion

					// Processes the Supplier Order
					double supOrderDetTotalUnitCostBD = Convert.ToDouble((panDetail.ChildElements["txtSupOrderDetTotalUnitCostBD"] as
						Telerik.Reporting.Processing.TextBox).Value);
					this._totalOrdersBD += supOrderDetTotalUnitCostBD;

				}
			}
		}

		private void reportFooterSection_ItemDataBound(object sender, EventArgs e)
		{
			Telerik.Reporting.Processing.ReportSection section = sender as
				Telerik.Reporting.Processing.ReportSection;
			if (section != null)
			{

				(section.ChildElements["txtGrantTotal"] as Telerik.Reporting.Processing.TextBox).Value =
					String.Format("BD {0:#,##0.000}", this._totalOrdersBD);

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