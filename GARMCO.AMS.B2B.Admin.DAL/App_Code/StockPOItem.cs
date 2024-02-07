using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GARMCO.Common.Object;

namespace GARMCO.AMS.B2B.Admin.DAL
{
	[Serializable]
	sealed public class StockPOItem : ObjectItem
	{
		#region Properties
		public double StockPONo { get; set; }
		public string StockPOCostCenter { get; set; }
		public double StockPOSupplierNo { get; set; }
		public string StockPOSupplierName { get; set; }
		public double StockPOUnitCost { get; set; }
		public double StockPOLeadTime { get; set; }
		public double StockPOBuyerNo { get; set; }
		public string StockPOBuyerName { get; set; }
		public string StockPODate { get; set; }
		public DateTime? StockPOFaxDate { get; set; }
		#endregion

		#region Constructors
		public StockPOItem() :
			base()
		{
			this.StockPONo = 0;
			this.StockPOCostCenter = String.Empty;
			this.StockPOSupplierNo = 0;
			this.StockPOSupplierName = String.Empty;
			this.StockPOUnitCost = 0;
			this.StockPOLeadTime = 0;
			this.StockPOBuyerNo = 0;
			this.StockPOBuyerName = String.Empty;
			this.StockPODate = String.Empty;
			this.StockPOFaxDate = DateTime.Now;
		}

		public StockPOItem(double stockPONo, double stockPOSupplierNo, string stockPOSupplierName, double stockPOUnitCost,
			double stockPOLeadTime, double stockPOBuyerNo, string stockPODate) :
			this()
		{
			this.StockPONo = stockPONo;
			this.StockPOSupplierNo = stockPOSupplierNo;
			this.StockPOSupplierName = stockPOSupplierName;
			this.StockPOUnitCost = stockPOUnitCost;
			this.StockPOLeadTime = stockPOLeadTime;
			this.StockPOBuyerNo = stockPOBuyerNo;
			this.StockPODate = stockPODate;
		}

		public StockPOItem(double stockPONo, string stockPOCostCenter,
			double stockPOSupplierNo, string stockPOSupplierName, double stockPOUnitCost,
			double stockPOLeadTime, double stockPOBuyerNo, string stockPODate, DateTime? stockPOFaxDate) :
			this()
		{
			this.Added = false;
			this.StockPONo = stockPONo;
			this.StockPOCostCenter = stockPOCostCenter;
			this.StockPOSupplierNo = stockPOSupplierNo;
			this.StockPOSupplierName = stockPOSupplierName;
			this.StockPOUnitCost = stockPOUnitCost;
			this.StockPOLeadTime = stockPOLeadTime;
			this.StockPOBuyerNo = stockPOBuyerNo;
			this.StockPODate = stockPODate;
			this.StockPOFaxDate = stockPOFaxDate;
		}

		public StockPOItem(double stockPONo, double stockPOBuyerNo, string stockPOBuyerName) :
			this()
		{
			this.Added = false;
			this.StockPONo = stockPONo;
			this.StockPOBuyerNo = stockPOBuyerNo;
			this.StockPOBuyerName = stockPOBuyerName;
		}
		#endregion

		#region Override Methods
		public override void AssignItem(DataRow row)
		{
		}
		#endregion
	}
}
