using System;
using System.ComponentModel;
using System.Drawing;
using System.Web;
using System.Windows.Forms;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;

namespace GARMCO.AMS.B2B.Report.eTendering
{
	/// <summary>
	/// Summary description for SupplierOrderRep.
	/// </summary>
	public partial class SupplierOrderRep : Telerik.Reporting.Report
	{
		public SupplierOrderRep()
		{
			//
			// Required for telerik Reporting designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		#region Users Functions
		public static string FormatHtmlString(string text)
		{
			return HttpUtility.HtmlEncode(text).Replace(" ", "&nbsp;").Replace("\r\n", "<br />").Replace("\n", "<br />");
		}

        public static string FormatSpecialCharToHTML(string inputText)
        {
            string outputText = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(inputText))
                    return string.Empty;

                #region Format to HTML text
                if (inputText.Contains(@"&"))
                {
                    inputText = inputText.Replace(@"&", @"&amp;");
                }

                if (inputText.Contains(@"€"))
                {
                    inputText = inputText.Replace(@"€", @"&euro;");
                }

                //if (inputText.Contains('"'))
                //{
                //    inputText = inputText.Replace('"', @"&quot;");
                //}

                if (inputText.Contains(@"<"))
                {
                    inputText = inputText.Replace(@"<", @"&lt;");
                }

                if (inputText.Contains(@">"))
                {
                    inputText = inputText.Replace(@">", @"&gt;");
                }

                if (inputText.Contains(@"¡"))
                {
                    inputText = inputText.Replace(@"¡", @"&iexcl;");
                }

                if (inputText.Contains(@"£"))
                {
                    inputText = inputText.Replace(@"£", @"&pound;");
                }

                if (inputText.Contains(@"©"))
                {
                    inputText = inputText.Replace(@"©", @"&copy;");
                }

                if (inputText.Contains(@"¼"))
                {
                    inputText = inputText.Replace(@"¼", @"&frac14;");
                }

                if (inputText.Contains(@"½"))
                {
                    inputText = inputText.Replace(@"½", @"&frac12;");
                }

                if (inputText.Contains(@"¾"))
                {
                    inputText = inputText.Replace(@"¾", @"&frac34;");
                }

                if (inputText.Contains(@"¼"))
                {
                    inputText = inputText.Replace(@"¼", @"&frac14;");
                }

                if (inputText.Contains(@"Ñ"))
                {
                    inputText = inputText.Replace(@"Ñ", @"&Ntilde;");
                }

                if (inputText.Contains(@"¶"))
                {
                    inputText = inputText.Replace(@"¶", @"&Ntilde");
                }

                if (inputText.Contains(@"®"))
                {
                    inputText = inputText.Replace(@"®", @"&reg;");
                }
                #endregion

                outputText = string.Format("<html><body><span>{0}</span></body></html>", inputText.Trim());

                return outputText;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
		#endregion
	}
}