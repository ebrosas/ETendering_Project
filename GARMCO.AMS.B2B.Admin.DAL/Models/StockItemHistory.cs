using System;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class StockItemHistory
    {
        public DateTime StockItemDate { get; set; }
        public string StockItemDesc1 { get; set; }
        public string StockItemDesc2 { get; set; }
        public double StockItemDocRefNo { get; set; }
        public string StockItemDocType { get; set; }
        public string StockItemDocTypeDesc { get; set; }
        public double StockItemIssues { get; set; }
        public string StockItemNo { get; set; }
        public double StockItemRunningBal { get; set; }
        public string StockItemUOM { get; set; }
        public string StockItemWorkOrderNo { get; set; }
    }
}
