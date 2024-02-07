using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GARMCO.Common.Utility;

namespace GARMCO.AMS.B2B.Utility
{
	sealed public class B2BConstants : GARMCOConstants
	{
		#region Password Encryption
		public const string PASSWORD_KEY = "ilov3garmcov3rymuchf0r3ver200633";
		public const string INIT_VECTOR = "@1dlakd45*l(g3F*";
		#endregion

		#region Error Status
		public const int DB_STATUS_ERROR_INSERT = 1002;
		public const int DB_STATUS_ERROR_UPDATE = 1003;
		public const int DB_STATUS_ERROR_DELETE = 1004;
		#endregion

		#region Session variables and web config applications settings
		public const string CONTACT_ID = "CONTACT_ID";
		public const string CONTACT_NAME = "CONTACT_NAME";
		public const string CONTACT_EMAIL = "CONTACT_EMAIL";
		public const string CONTACT_SUPPLIER_NO = "CONTACT_SUPPLIER_NO";
		public const string CONTACT_PRIMARY = "CONTACT_PRIMARY";

		public const string HTML_RTF_KEY = "HTML_RTF_KEY";
		public const string RTF_HTML_KEY = "RTF_HTML_KEY";
		public const string ITEM_LIST_SUPPLIER_BID = "ItemListSupplierBid";
		public const string ITEM_LIST_LOWEST_BID = "ItemListLowestBid";
		public const string ITEM_LIST_TENDER_SESSION_ATTENDEE = "ItemListTenderSessionAttendee";

		public const string ITEM_LIST_SUPPLIER_PRODSERVICE = "ItemListSupplierProdService";
		public const string ITEM_LIST_SUPPLIER_PRODSERVICE_TEMP = "ItemListSupplierProdServiceTemp";
		public const string ITEM_LIST_SUPPLIER_ORDER_DETAIL = "ItemListSupplierOrderDetail";
		public const string ITEM_LIST_SUPPLIER_ORDER_DETAIL_CURRENT = "ItemListSupplierOrderDetailCurrent";
		public const string ITEM_LIST_SUPPLIER_ORDER_ALTERNATIVE = "ItemListSupplierOrderAlternative";
		public const string ITEM_LIST_SUPPLIER_ORDER_ALTERNATIVE_TEMP = "ItemListSupplierOrderAlternativeTemp";
		public const string ITEM_LIST_SUPPLIER_ORDER_OTHER_CHARGE = "ItemListSupplierOrderOtherCharge";
		public const string ITEM_LIST_SUPPLIER_ORDER_ATTACHMENT = "ItemListSupplierOrderAttach";
		public const string ITEM_LIST_SUPPLIER_ORDER_ATTACHMENT_TEMP = "ItemListSupplierOrderAttachTemp";
		#endregion

		#region Ajax Return Arguments
		public const string AJAX_RETURN_JDE_SUPPLIER_LOOKUP = "UpdateJDESupplierInquiryLookUp";
		public const string AJAX_RETURN_SUPPLIER_CONTACT_LOOKUP = "UpdateSupplierContactLookUp";
		public const string AJAX_RETURN_AUTHENTICATE_ATTENDEE_LOOKUP = "UpdateAuthenticateAttendeeLookUp";
		public const string AJAX_RETURN_SUPPLIER_REJECTED_LOOKUP = "UpdateSupplierRejectLookUp";
		public const string AJAX_RETURN_SUPPLIER_PROD_SERVICE_LOOKUP = "UpdateSupplierProductServiceLookUp";
		public const string AJAX_RETURN_SUPPLIER_ORDER_ALTERNATIVE_LOOKUP = "UpdateSupplierOrderAlternativeLookup";
		public const string AJAX_RETURN_SUPPLIER_ORDER_ATTACHMENT_LOOKUP = "UpdateSupplierOrderAttachmentLookup";
		public const string AJAX_RETURN_RFQ = "UpdateRFQLookUp";
		public const string AJAX_RETURN_RFQ_REOPEN = "UpdateRFQReOpenLookUp";
		#endregion

		#region Media Objects
		public enum MediaObjectType
		{
			NoMediaObjType = -1,
			Text = 0,
			File = 1,
			HtmlUpload = 5
		}
		#endregion

		#region Check State
		public enum CheckState
		{
			Uncheck,
			Checked,
			Indeterminate
		};
		#endregion

		#region Printing Type
		public enum PrintType : int
		{
			RFQDetail,
			SupplierOrder,
			SupplierOrderInq,
			SupplierBidsPerSupplier,
			SupplierBidsPerOrderLine,
			RecommendedBid
		};
		#endregion

		#region Supplier Order Line Type
		public enum SupplierOrderLineType : int
		{
			SupplierOrderDetail = 1,
			SupplierOrderDetailAlt = 2,
			SupplierOtherCharge = 3
		};
		#endregion
	}
}
