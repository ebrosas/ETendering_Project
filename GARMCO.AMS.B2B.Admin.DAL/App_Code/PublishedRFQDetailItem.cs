using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GARMCO.AMS.B2B.Admin.DAL.App_Code;
using GARMCO.AMS.B2B.Utility;

namespace GARMCO.AMS.B2B.Admin.DAL
{
	[Serializable]
	sealed public class PublishedRFQDetailItem
	{
		#region Properties
		public string OrderDetCompany { get; set; }
		public double? OrderDetNo { get; set; }
		public string OrderDetType { get; set; }
		public string OrderDetSuffix { get; set; }
		public double? OrderDetLineNo { get; set; }
		public string OrderDetLineType { get; set; }
		public double? OrderDetQuantity { get; set; }
		public string OrderDetUM { get; set; }
		public string OrderDetCurrencyCode { get; set; }
		public string OrderDetDesc { get; set; }
		public string OrderDetFullDesc
		{
			get
			{
                try
                {
                    StringBuilder description = new StringBuilder();
                    //string htmlText = string.Empty;                    

                    // Adds the default description
                    description.Append(this.OrderDetDesc);

                    // Adds the media objects
                    if (this.OrderDetAttachList != null)
                    {

                        foreach (PublishedRFQDetailAttachItem item in this.OrderDetAttachList.FindAll(
                            tempItem => tempItem.MediaType == B2BConstants.MediaObjectType.Text))
                        {

                            description.Append(Environment.NewLine);
                            description.Append(item.MediaPlainText);

                        }
                    }

                    // Format any special characters
                    //htmlText = BLLookup.FormatSpecialCharToHTML(description.ToString());
                    //return htmlText;

                    return description.ToString();
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
			}
		}
		public string OrderDetUNSPSC { get; set; }
		public double? OrderDetStockShortItemNo { get; set; }
		public string OrderDetStockLongItemNo { get; set; }
		public string OrderDetStatus { get; set; }
		public string OrderDetPrintMsg { get; set; }

		public double? OrderBuyerEmpNo { get; set; }
		public string OrderBuyerEmpName { get; set; }
		public string OrderBuyerEmpEmail { get; set; }
		public DateTime? OrderTransactionDate { get; set; }
		public DateTime? OrderClosingDate { get; set; }
        public DateTime? OrderPublishedDate { get; set; }
		public string OrderPRNo { get; set; }
		public string OrderPriority { get; set; }

		public List<PublishedRFQInvitedSupplierItem> OrderDetInvitedSupplierList { get; set; }
		public List<PublishedRFQDetailAttachItem> OrderDetAttachList { get; set; }
		#endregion

		#region Constructors
		public PublishedRFQDetailItem()
		{
			this.OrderDetCompany = String.Empty;
			this.OrderDetNo = 0;
			this.OrderDetType = String.Empty;
			this.OrderDetSuffix = String.Empty;
			this.OrderDetLineNo = 0;
			this.OrderDetLineType = String.Empty;
			this.OrderDetQuantity = 0;
			this.OrderDetUM = String.Empty;
			this.OrderDetCurrencyCode = String.Empty;
			this.OrderDetDesc = String.Empty;
			this.OrderDetUNSPSC = String.Empty;
			this.OrderDetStockShortItemNo = 0;
			this.OrderDetStockLongItemNo = String.Empty;
			this.OrderDetStatus = String.Empty;
			this.OrderDetPrintMsg = String.Empty;

			this.OrderBuyerEmpNo = 0;
			this.OrderBuyerEmpName = String.Empty;
			this.OrderBuyerEmpEmail = String.Empty;
			this.OrderTransactionDate = DateTime.Now;
			this.OrderClosingDate = DateTime.Now;
			this.OrderPRNo = String.Empty;
			this.OrderPriority = String.Empty;

			this.OrderDetInvitedSupplierList = new List<PublishedRFQInvitedSupplierItem>();
			this.OrderDetAttachList = new List<PublishedRFQDetailAttachItem>();
		}

		public PublishedRFQDetailItem(PublishedRFQDetailItem item)
		{
			this.OrderDetCompany = item.OrderDetCompany;
			this.OrderDetNo = item.OrderDetNo;
			this.OrderDetType = item.OrderDetType;
			this.OrderDetSuffix = item.OrderDetSuffix;
			this.OrderDetLineNo = item.OrderDetLineNo;
			this.OrderDetLineType = item.OrderDetLineType;
			this.OrderDetQuantity = item.OrderDetQuantity;
			this.OrderDetUM = item.OrderDetUM;
			this.OrderDetCurrencyCode = item.OrderDetCurrencyCode;
			this.OrderDetDesc = item.OrderDetDesc;
			this.OrderDetUNSPSC = item.OrderDetUNSPSC;
			this.OrderDetStockShortItemNo = item.OrderDetStockShortItemNo;
			this.OrderDetStockLongItemNo = item.OrderDetStockLongItemNo;
			this.OrderDetStatus = item.OrderDetStatus;
			this.OrderDetPrintMsg = item.OrderDetPrintMsg;

			this.OrderBuyerEmpNo = item.OrderBuyerEmpNo;
			this.OrderBuyerEmpName = item.OrderBuyerEmpName;
			this.OrderBuyerEmpEmail = item.OrderBuyerEmpEmail;
			this.OrderTransactionDate = item.OrderTransactionDate;
			this.OrderClosingDate = item.OrderClosingDate;
			this.OrderPRNo = item.OrderPRNo;
			this.OrderPriority = item.OrderPriority;
		}

		public PublishedRFQDetailItem(PublishedRFQDetailItem item,
			double? orderBuyerEmpNo, string orderBuyerEmpName, string orderBuyerEmpEmail,
			DateTime? orderTransactionDate, DateTime? orderClosingDate, string orderPRNo,
            string orderPriority, bool isReport = false)
		{
			DateTime closingDate = Convert.ToDateTime(orderClosingDate);

			this.OrderDetCompany = item.OrderDetCompany;
			this.OrderDetNo = item.OrderDetNo;
			this.OrderDetType = item.OrderDetType;
			this.OrderDetSuffix = item.OrderDetSuffix;
			this.OrderDetLineNo = item.OrderDetLineNo;
			this.OrderDetLineType = item.OrderDetLineType;
			this.OrderDetQuantity = item.OrderDetQuantity;
			this.OrderDetUM = item.OrderDetUM;
			this.OrderDetCurrencyCode = item.OrderDetCurrencyCode;

            #region Set the Order Description
            if (item.OrderDetStockShortItemNo > 0)
            {
                if (!BLLookup.ConvertObjectToString(item.OrderDetDesc).Contains("GARMCO Part No"))
                {
                    if (!isReport)
                        this.OrderDetDesc = String.Format("GARMCO Part No: {0}, {1}", item.OrderDetStockLongItemNo, item.OrderDetDesc);
                    else
                        this.OrderDetDesc = String.Format("GARMCO Part No: {0}", item.OrderDetStockLongItemNo);
                }
                else
                {
                    if (!isReport)
                        this.OrderDetDesc = BLLookup.ConvertObjectToString(item.OrderDetDesc);
                    else
                        this.OrderDetDesc = String.Format("GARMCO Part No: {0}", item.OrderDetStockLongItemNo);
                }
            }
            else
                this.OrderDetDesc = BLLookup.ConvertObjectToString(item.OrderDetDesc);
            #endregion

			this.OrderDetUNSPSC = item.OrderDetUNSPSC;
			this.OrderDetStockShortItemNo = item.OrderDetStockShortItemNo;
			this.OrderDetStockLongItemNo = item.OrderDetStockLongItemNo;
			this.OrderDetStatus = item.OrderDetStatus;
			this.OrderDetPrintMsg = item.OrderDetPrintMsg;
			this.OrderBuyerEmpNo = orderBuyerEmpNo;
			this.OrderBuyerEmpName = orderBuyerEmpName;
			this.OrderBuyerEmpEmail = orderBuyerEmpEmail;
			this.OrderTransactionDate = orderTransactionDate;
			this.OrderClosingDate = closingDate.Hour == 0 && closingDate.Minute == 0 ? new DateTime(closingDate.Year, closingDate.Month, closingDate.Day, 23, 59, 0) :
				orderClosingDate;
			this.OrderPRNo = orderPRNo;
			this.OrderPriority = orderPriority;

            // Initialize the collection
            this.OrderDetAttachList = new List<PublishedRFQDetailAttachItem>();
		}

		public PublishedRFQDetailItem(PublishedRFQDetail row, bool isReport = false)
		{
			this.OrderDetCompany = row.OrderDetCompany;
			this.OrderDetNo = row.OrderDetNo;
			this.OrderDetType = row.OrderDetType;
			this.OrderDetSuffix = row.OrderDetSuffix;
			this.OrderDetLineNo = row.OrderDetLineNo / 1000;
			this.OrderDetLineType = row.OrderDetLineType;
			this.OrderDetQuantity = row.OrderDetQuantity / 1000;
			this.OrderDetUM = row.OrderDetUM;
			this.OrderDetCurrencyCode = row.OrderDetCurrencyCode;

            if (row.OrderDetStockShortItemNo > 0)
            {
                if (!BLLookup.ConvertObjectToString(row.OrderDetDesc1).Contains("GARMCO Part No"))
                {
                    if (!isReport)
                        this.OrderDetDesc = String.Format("GARMCO Part No: {0}, {1} {2}",
                            row.OrderDetStockLongItemNo,
                            BLLookup.ConvertObjectToString(row.OrderDetDesc1),
                            BLLookup.ConvertObjectToString(row.OrderDetDesc2));
                    else
                        this.OrderDetDesc = String.Format("GARMCO Part No: {0}", row.OrderDetStockLongItemNo);
                }
                else
                {
                    if (!isReport)
                        this.OrderDetDesc = String.Format("{0} {1}",
                            BLLookup.ConvertObjectToString(row.OrderDetDesc1),
                            BLLookup.ConvertObjectToString(row.OrderDetDesc2));
                    else
                        this.OrderDetDesc = String.Format("GARMCO Part No: {0}", row.OrderDetStockLongItemNo);
                }
            }
            else
            {
                this.OrderDetDesc = String.Format("{0} {1}", BLLookup.ConvertObjectToString(row.OrderDetDesc1), BLLookup.ConvertObjectToString(row.OrderDetDesc2));
            }
            
            //this.OrderDetDesc = row.OrderDetStockShortItemNo > 0 ? String.Format("GARMCO Part No: {0}, {1} {2}", row.OrderDetStockLongItemNo,
            //    row.OrderDetDesc1, row.OrderDetDesc2) : String.Format("{0} {1}", row.OrderDetDesc1, row.OrderDetDesc2);

			this.OrderDetUNSPSC = row.OrderDetUNSPSC;
			this.OrderDetStockShortItemNo = row.OrderDetStockShortItemNo;
			this.OrderDetStockLongItemNo = row.OrderDetStockLongItemNo;
			this.OrderDetStatus = row.OrderDetLastStatus.Equals("980") && row.OrderDetNextStatus.Equals("999") ? "206" : "200";
			this.OrderDetPrintMsg = row.OrderDetPrintMsg;
            this.OrderBuyerEmpNo = row.OrderBuyerEmpNo;
            this.OrderBuyerEmpName = row.OrderBuyerEmpName;
            this.OrderBuyerEmpEmail = row.OrderBuyerEmpEmail;

            try
            {
                this.OrderClosingDate = BLLookup.ConvertObjectToDate(row.OrderClosingDate);
            }
            catch (Exception)
            {
                this.OrderClosingDate = null;
            }

            try
            {
                this.OrderPublishedDate = BLLookup.ConvertObjectToDate(row.OrderPublishedDate);
            }
            catch(Exception)
            {
                this.OrderPublishedDate = null;
            }

			this.OrderDetInvitedSupplierList = new List<PublishedRFQInvitedSupplierItem>();
			this.OrderDetAttachList = new List<PublishedRFQDetailAttachItem>();
		}

		public PublishedRFQDetailItem(OrderRequisitionDetail detail, bool isReport = false, bool divideByThousand = true)
		{
			this.OrderDetCompany = detail.OrderDetCompany;
			this.OrderDetNo = detail.OrderDetNo;
			this.OrderDetType = detail.OrderDetType;
			this.OrderDetSuffix = detail.OrderDetSuffix;			
			this.OrderDetLineType = detail.OrderDetLineType;			
			this.OrderDetUM = detail.OrderDetUM;
			this.OrderDetCurrencyCode = detail.OrderDetCurrencyCode;

            if (divideByThousand)
            {
                this.OrderDetLineNo = detail.OrderDetLineNo / 1000;
                this.OrderDetQuantity = detail.OrderDetQuantity / 1000;
            }
            else
            {
                this.OrderDetLineNo = detail.OrderDetLineNo; 
                this.OrderDetQuantity = detail.OrderDetQuantity; 
            }

            if (detail.OrderDetStockShortItemNo > 0)
            {
                if (!BLLookup.ConvertObjectToString(detail.OrderDetDesc).Contains("GARMCO Part No"))
                {
                    if (!isReport)
                        this.OrderDetDesc = String.Format("GARMCO Part No: {0}, {1}", detail.OrderDetStockLongItemNo, detail.OrderDetDesc);
                    else
                        this.OrderDetDesc = String.Format("GARMCO Part No: {0}", detail.OrderDetStockLongItemNo);
                }
                else
                {
                    if (!isReport)
                        this.OrderDetDesc = BLLookup.ConvertObjectToString(detail.OrderDetDesc);
                    else
                        this.OrderDetDesc = String.Format("GARMCO Part No: {0}", detail.OrderDetStockLongItemNo);
                }
            }
            else
                this.OrderDetDesc = BLLookup.ConvertObjectToString(detail.OrderDetDesc);

			this.OrderDetUNSPSC = detail.OrderDetUNSPSC;
			this.OrderDetStockShortItemNo = detail.OrderDetStockShortItemNo;
			this.OrderDetStockLongItemNo = detail.OrderDetStockLongItemNo;
			this.OrderDetStatus = detail.OrderDetStatus;
			this.OrderDetPrintMsg = String.Empty;
            this.OrderBuyerEmpNo = detail.OrderBuyerEmpNo;
            this.OrderBuyerEmpName = detail.OrderBuyerEmpName;

            try
            {
                this.OrderClosingDate = BLLookup.ConvertObjectToDate(detail.OrderClosingDate);
            }
            catch (Exception)
            {
                this.OrderClosingDate = null;
            }

            try
            {
                this.OrderPublishedDate = BLLookup.ConvertObjectToDate(detail.OrderPublishedDate);
            }
            catch (Exception)
            {
                this.OrderPublishedDate = null;
            }

			this.OrderDetInvitedSupplierList = new List<PublishedRFQInvitedSupplierItem>();
			this.OrderDetAttachList = new List<PublishedRFQDetailAttachItem>();
		}
		#endregion
	}
}
