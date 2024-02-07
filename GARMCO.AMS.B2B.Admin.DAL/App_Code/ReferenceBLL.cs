using System;
using System.ComponentModel;
using System.Configuration;
using GARMCO.AMS.B2B.Admin.DAL.GrmEmployeeWebService;

namespace GARMCO.AMS.B2B.Admin.DAL
{
	[DataObject]
	sealed public class ReferenceBLL
	{
		#region Employee
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public void CheckCurrentUser(string loginName, string password, ref int retError, ref string errorMsg)
		{
			// Initializes return values
			retError = 0;
			errorMsg = String.Empty;

			Employee empWS = new Employee();
			empWS.Credentials = System.Net.CredentialCache.DefaultCredentials;
			empWS.Url = ConfigurationManager.AppSettings["GARMCOWebServicesEmployeeService"];

			// Checks if login name has GARMCO already
			if (loginName.IndexOf("\\") == -1)
				loginName = "GARMCO\\" + loginName;

			// Checks the username/password
			GrmEmployeeWebService.EmployeeInfo empWFInfo = empWS.Login(loginName, password, ref retError);
			if (empWFInfo == null || retError != 0)
			{

				retError = -1;
				errorMsg = "The username/password is invalid.<br /> Please try again.";

			}

			// Checks if user group
			else if (empWFInfo.EmployeeNo.Length < 4 || !empWFInfo.EmployeeNo.Substring(0, 4).Equals("1000"))
			{

				retError = -1;
				errorMsg = "Please use your own username and password and not the common username.";

			}
		}
		#endregion
	}
}
