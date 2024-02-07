using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using GARMCO.AMS.B2B.eTendering.DAL;
using GARMCO.AMS.B2B.eTendering.Website.Helpers;
using GARMCO.AMS.B2B.Utility;
using GARMCO.Common.DAL.Employee;
using GARMCO.Common.Object;
using Telerik.Web.UI;

namespace GARMCO.AMS.Common.Web
{
	public class BaseWebForm : System.Web.UI.Page
	{
		#region Constants
		public const int LOGON32_LOGON_INTERACTIVE = 2;
		public const int LOGON32_PROVIDER_DEFAULT = 0;
		#endregion

		#region Properties
		public bool IsRetrieveUserInfo
		{
			get
			{
				bool viewPage = false;
				if (ViewState["IsRetrieveUserInfo"] != null)
					viewPage = Convert.ToBoolean(ViewState["IsRetrieveUserInfo"]);

				return viewPage;
			}

			set
			{
				ViewState["IsRetrieveUserInfo"] = value;
			}
		}

		public bool IsToCheckSession
		{
			get
			{
				bool isToCheckSession = true;
				if (ViewState["IsToCheckSession"] != null)
					isToCheckSession = Convert.ToBoolean(ViewState["IsToCheckSession"]);

				return isToCheckSession;
			}

			set
			{
				ViewState["IsToCheckSession"] = value;
			}
		}
		#endregion

		#region Private Data Members
		private WindowsImpersonationContext _impersonationContext;
		#endregion

		#region Import DLLs
		[DllImport("advapi32.dll")]
		public static extern int LogonUserA(String lpszUserName,
			String lpszDomain,
			String lpszPassword,
			int dwLogonType,
			int dwLogonProvider,
			ref IntPtr phToken);
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int DuplicateToken(IntPtr hToken,
			int impersonationLevel,
			ref IntPtr hNewToken);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool RevertToSelf();

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern bool CloseHandle(IntPtr handle);
		#endregion

		#region Override Methods
		protected override void OnInit(EventArgs e)
		{
			if (!this.IsPostBack)
			{

				#region Checks if already authenticated
				if (this.Page.User.Identity.IsAuthenticated && Session[B2BConstants.CONTACT_ID] == null)
				{

					var supplierContacts = new SupplierContactRepository().GetSupplierContact((byte)B2BConstants.DB_SELECT_SPECIFIC, 0, this.Page.User.Identity.Name, String.Empty, 0);
					if (supplierContacts != null && supplierContacts.Count() > 0)
					{

						var supplierContact = supplierContacts.First();

						#region Stores the contact information
						Session[B2BConstants.CONTACT_ID] = supplierContact.ContactID;
						Session[B2BConstants.CONTACT_NAME] = supplierContact.ContactName;
						Session[B2BConstants.CONTACT_EMAIL] = supplierContact.ContactEmail;
						Session[B2BConstants.CONTACT_SUPPLIER_NO] = supplierContact.SupplierNo;
						Session[B2BConstants.CONTACT_PRIMARY] = supplierContact.ContactPrimary;
						#endregion

						FormsAuthentication.RedirectFromLoginPage(this.Page.User.Identity.Name, true);

						// Checks if url is blank
						if (String.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
							Response.Redirect("~/Account/PublishedRFQView.aspx", false);

						else
							Response.Redirect(Request.QueryString["ReturnUrl"], false);

					}
				}
				#endregion
			}

			// Checks the session
			this.CheckSession();

			base.OnInit(e);
		}

		protected override void OnError(EventArgs e)
		{
			// Retrieve the last error
			HttpContext ctx = HttpContext.Current;
			Session[B2BConstants.EXCEPTION_ERROR] = ctx.Server.GetLastError();

			// Clear the error
			ctx.Server.ClearError();

			Response.Redirect("~/ErrorMessage.aspx?url=" + ctx.Request.Url.ToString(), false);

			base.OnError(e);
		}
		#endregion End of overriding methods

		#region Public Methods
		//public void RetrieveUserInfo(string currentUser)
		//{
		//	int? retError = 0;
		//	string errorMsg = String.Empty;

		//	EmployeeBLL empBLL = new EmployeeBLL();
		//	EmployeeInfo empInfo = empBLL.GetEmployeeInfo(currentUser, ref retError, ref errorMsg);

		//	if (retError == 0)
		//	{

		//		int index = currentUser.LastIndexOf("\\");

		//		#region Store the values to session
		//		Session[B2BConstants.GARMCO_USERID] = empInfo.EmployeeNo;
		//		Session[B2BConstants.GARMCO_USERNAME] = currentUser.Substring(index + 1);
		//		Session[B2BConstants.GARMCO_FULLNAME] = empInfo.FullName;
		//		Session[B2BConstants.GARMCO_USER_COST_CENTER] = empInfo.CostCenter;
		//		Session[B2BConstants.GARMCO_USER_COST_CENTER_NAME] = empInfo.CostCenterName;
		//		Session[B2BConstants.GARMCO_USER_EMAIL] = empInfo.Email;
		//		Session[B2BConstants.GARMCO_USER_EXT] = empInfo.ExtensionNo;
		//		Session[B2BConstants.GARMCO_USER_PAY_GRADE] = empInfo.PayGrade.ToString();
		//		Session[B2BConstants.GARMCO_USER_POSITION_ID] = empInfo.PositionID;
		//		Session[B2BConstants.GARMCO_USER_POSITION_DESC] = empInfo.PositionDesc;
		//		Session[B2BConstants.GARMCO_USER_EMP_CLASS] = empInfo.EmployeeClass;

		//		this.IsRetrieveUserInfo = true;
		//		#endregion

		//	}
		//}

		public void CheckSession()
		{
			string currentUser = this.Page.User.Identity.Name;

			if (this.IsToCheckSession)
			{

				if (Session[B2BConstants.CONTACT_ID] == null)
					Response.Redirect(String.Format("~/ErrorMessage.aspx?error={0}", B2BConstants.PAGE_ERROR_SESSION_END), false);

				// Sets the flag
				else
					this.IsRetrieveUserInfo = true;

			}
		}

        public void ShowErrorMessage(Exception error)
        {
            HttpContext ctx = HttpContext.Current;
            Session[B2BConstants.EXCEPTION_ERROR] = error;
            Response.Redirect(string.Format("{0}?url={1}", UILookup.PAGE_ERROR, ctx.Request.Url.ToString()), false);
        }
		#endregion

		#region Protected Methods
		protected bool ImpersonateValidUser(string username, string password, string domain)
		{
			bool impersonateSuccessful = false;

			WindowsIdentity tempWindowsIdentity;
			IntPtr token = IntPtr.Zero;
			IntPtr tokenDuplicate = IntPtr.Zero;

			if (RevertToSelf())
			{

				if (LogonUserA(username, domain, password, LOGON32_LOGON_INTERACTIVE,
						LOGON32_PROVIDER_DEFAULT, ref token) != 0)
				{
					if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
					{

						tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
						this._impersonationContext = tempWindowsIdentity.Impersonate();
						if (this._impersonationContext != null)
						{

							CloseHandle(token);
							CloseHandle(tokenDuplicate);
							impersonateSuccessful = true;

						}
					}
				}

				if (!impersonateSuccessful)
				{

					if (token != IntPtr.Zero)
						CloseHandle(token);

					if (tokenDuplicate != IntPtr.Zero)
						CloseHandle(tokenDuplicate);

				}
			}

			return impersonateSuccessful;
		}

		protected void UndoImpersonation()
		{
			this._impersonationContext.Undo();
		}

		protected string CopyFile(string username, string password, string domain, string uploadFolder, string filename)
		{
			string targetFilename = filename;

			#region Impersonate other user to upload to a shared folder
			if (this.ImpersonateValidUser(username, password, domain))
			{

				// Checks if the file does exist already
				if (File.Exists(Path.Combine(uploadFolder, targetFilename)))
				{

					// Rename the file
					int extIndex = targetFilename.LastIndexOf(".");
					if (extIndex > -1)
						targetFilename = String.Format("{0}_{1}{2}",
							targetFilename.Substring(0, extIndex), DateTime.Now.Ticks, targetFilename.Substring(extIndex));

					else
						targetFilename = String.Format("{0}_{1}", targetFilename, DateTime.Now.Ticks);

				}

				// Copies the file
				File.Copy(Path.Combine(uploadFolder, filename), Path.Combine(uploadFolder, targetFilename));

				// Resets the impersonation
				this.UndoImpersonation();

			}
			#endregion

			return targetFilename;
		}
		#endregion

		#region Virtual Methods
		public virtual void ajaxMngrBase_AjaxRequest(object sender, AjaxRequestEventArgs e)
		{
		}
		#endregion
	}
}