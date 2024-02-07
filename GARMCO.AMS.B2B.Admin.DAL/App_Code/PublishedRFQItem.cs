using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GARMCO.AMS.B2B.Admin.DAL.App_Code;

namespace GARMCO.AMS.B2B.Admin.DAL
{
	[Serializable]
	sealed public class PublishedRFQItem
	{
		#region Properties
		public string OrderCompany { get; set; }
		public double? OrderNo { get; set; }
		public string OrderType { get; set; }
		public string OrderSuffix { get; set; }
		public double? OrderEmpNo { get; set; }
		public string OrderEmpName { get; set; }
		public double? OrderBuyerEmpNo { get; set; }
		public string OrderBuyerEmpName { get; set; }
		public string OrderBuyerEmpEmail { get; set; }
		public DateTime? OrderTransactionDate { get; set; }
		public DateTime? OrderClosingDate { get; set; }
		public string OrderPRNo { get; set; }
		public string OrderCategory { get; set; }
		public string OrderPriority { get; set; }
		public bool OrderSpecificSupplier { get; set; }
		public bool OrderNeedTenderCommittee { get; set; }

		public List<PublishedRFQInvitedSupplierItem> OrderInvitedSupplierList { get; set; }
		public List<PublishedRFQDetailItem> OrderDetailList { get; set; }
		#endregion

		#region Constructors
		public PublishedRFQItem()
		{
			this.OrderCompany = String.Empty;
			this.OrderNo = 0;
			this.OrderType = String.Empty;
			this.OrderSuffix = String.Empty;
			this.OrderEmpNo = 0;
			this.OrderEmpName = String.Empty;
			this.OrderBuyerEmpNo = 0;
			this.OrderBuyerEmpName = String.Empty;
			this.OrderBuyerEmpEmail = String.Empty;
			this.OrderTransactionDate = DateTime.Now;
			this.OrderClosingDate = DateTime.Now;
			this.OrderPRNo = String.Empty;
			this.OrderCategory = String.Empty;
			this.OrderPriority = String.Empty;
			this.OrderSpecificSupplier = false;
			this.OrderNeedTenderCommittee = false;

			this.OrderInvitedSupplierList = new List<PublishedRFQInvitedSupplierItem>();
			this.OrderDetailList = new List<PublishedRFQDetailItem>();
		}

		public PublishedRFQItem(string orderCompany, double? orderNo, string orderType, string orderSuffix,
			double? orderEmpNo, string orderEmpName, double? orderBuyerEmpNo, string orderBuyerEmpName, string orderBuyerEmpEmail,
			DateTime? orderTransactionDate, DateTime? orderClosingDate, string orderPRNo, string orderCategory, string orderPriority,
			bool orderSpecificSupplier)
		{
			this.OrderCompany = orderCompany;
			this.OrderNo = orderNo;
			this.OrderType = orderType;
			this.OrderSuffix = orderSuffix;
			this.OrderEmpNo = orderEmpNo;
			this.OrderEmpName = orderEmpName;
			this.OrderBuyerEmpNo = orderBuyerEmpNo;
			this.OrderBuyerEmpName = orderBuyerEmpName;
			this.OrderBuyerEmpEmail = orderBuyerEmpEmail;
			this.OrderTransactionDate = orderTransactionDate;
			this.OrderClosingDate = orderClosingDate;
			this.OrderPRNo = orderPRNo;
			this.OrderCategory = orderCategory;
			this.OrderPriority = orderPriority;
			this.OrderSpecificSupplier = orderSpecificSupplier;

			this.OrderNeedTenderCommittee = false;

			this.OrderInvitedSupplierList = new List<PublishedRFQInvitedSupplierItem>();
			this.OrderDetailList = new List<PublishedRFQDetailItem>();
		}

		public PublishedRFQItem(PublishedRFQ row,
			bool orderSpecificSupplier)
		{
			this.OrderCompany = row.OrderCompany;
			this.OrderNo = row.OrderNo;
			this.OrderType = row.OrderType;
			this.OrderSuffix = row.OrderSuffix;
			this.OrderEmpNo = row.OrderEmpNo;
			this.OrderEmpName = row.OrderEmpName;
			this.OrderBuyerEmpNo = row.OrderBuyerEmpNo;
			this.OrderBuyerEmpName = row.OrderBuyerEmpName;
			this.OrderBuyerEmpEmail = row.OrderBuyerEmpEmail;
			this.OrderTransactionDate = row.OrderTransactionDate;
			this.OrderClosingDate = row.OrderClosingDate.Hour == 0 && row.OrderClosingDate.Minute == 0 ?
				new DateTime(row.OrderClosingDate.Year, row.OrderClosingDate.Month, row.OrderClosingDate.Day, 23, 59, 0) : row.OrderClosingDate;
			this.OrderPRNo = row.OrderPRNo;
			this.OrderCategory = row.OrderCategory;
			this.OrderPriority = row.OrderPriority;
			this.OrderSpecificSupplier = orderSpecificSupplier;

			this.OrderNeedTenderCommittee = false;

			this.OrderInvitedSupplierList = new List<PublishedRFQInvitedSupplierItem>();
			this.OrderDetailList = new List<PublishedRFQDetailItem>();
		}

		public PublishedRFQItem(OrderRequisition orderRequisition)
		{
			this.OrderCompany = orderRequisition.OrderCompany;
			this.OrderNo = orderRequisition.OrderNo;
			this.OrderType = orderRequisition.OrderType;
			this.OrderSuffix = orderRequisition.OrderSuffix;
			this.OrderEmpNo = orderRequisition.OrderOriginatorEmpNo;
			this.OrderEmpName = orderRequisition.OrderOriginatorEmpName;
			this.OrderBuyerEmpNo = orderRequisition.OrderBuyerEmpNo;
			this.OrderBuyerEmpName = orderRequisition.OrderBuyerEmpName;
			this.OrderBuyerEmpEmail = orderRequisition.OrderBuyerEmpEmail;
			this.OrderTransactionDate = orderRequisition.OrderTransactionDate;
			this.OrderClosingDate = orderRequisition.OrderClosingDate;
			this.OrderPRNo = orderRequisition.OrderPRNo;
			this.OrderCategory = orderRequisition.OrderCategory;
			this.OrderPriority = orderRequisition.OrderPriority;
			this.OrderSpecificSupplier = false;

			this.OrderNeedTenderCommittee = false;

			this.OrderInvitedSupplierList = new List<PublishedRFQInvitedSupplierItem>();
			this.OrderDetailList = new List<PublishedRFQDetailItem>();
		}

        public PublishedRFQItem(PublishedRFQItem row, bool orderSpecificSupplier)
        {
            this.OrderCompany = row.OrderCompany;
            this.OrderNo = row.OrderNo;
            this.OrderType = row.OrderType;
            this.OrderSuffix = row.OrderSuffix;
            this.OrderEmpNo = row.OrderEmpNo;
            this.OrderEmpName = row.OrderEmpName;
            this.OrderBuyerEmpNo = row.OrderBuyerEmpNo;
            this.OrderBuyerEmpName = row.OrderBuyerEmpName;
            this.OrderBuyerEmpEmail = row.OrderBuyerEmpEmail;
            this.OrderTransactionDate = row.OrderTransactionDate;

            if (row.OrderClosingDate.HasValue)
            {
                this.OrderClosingDate = Convert.ToDateTime(row.OrderClosingDate).Hour == 0 && Convert.ToDateTime(row.OrderClosingDate).Minute == 0 ?
                    new DateTime(Convert.ToDateTime(row.OrderClosingDate).Year,
                        Convert.ToDateTime(row.OrderClosingDate).Month,
                        Convert.ToDateTime(row.OrderClosingDate).Day,
                        23, 59, 0) : row.OrderClosingDate;
            }
            else
                this.OrderClosingDate = null;

            this.OrderPRNo = row.OrderPRNo;
            this.OrderCategory = row.OrderCategory;
            this.OrderPriority = row.OrderPriority;
            this.OrderSpecificSupplier = orderSpecificSupplier;

            this.OrderNeedTenderCommittee = false;
            
            this.OrderInvitedSupplierList = new List<PublishedRFQInvitedSupplierItem>();
            this.OrderDetailList = new List<PublishedRFQDetailItem>();
        }
		#endregion
	}
}
