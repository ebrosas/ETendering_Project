using System;

namespace GARMCO.AMS.B2B.Admin.DAL
{
	[Serializable]
	sealed public class StockMonthIssuedItem
	{
		#region Properties
		public string StockMonth { get; set; }
		public double StockIssued { get; set; }
		#endregion

		#region Constructors
		public StockMonthIssuedItem() :
			base()
		{
			this.StockMonth = String.Empty;
			this.StockIssued = 0;
		}

		public StockMonthIssuedItem(string stockMonth, double stockIssued)
		{
			this.StockMonth = stockMonth;
			this.StockIssued = stockIssued;
		}
		#endregion
	}
}
