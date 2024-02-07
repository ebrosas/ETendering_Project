using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace GARMCO.AMS.B2B.eTendering.Website.Helpers
{
    public static class UILookup
    {
        #region Page Navigation Constants
        public const string PAGE_UNDER_MAINTENANCE = @"~/CommonObject/UnderMaintenance.aspx";
        public const string PAGE_ERROR = @"~/ErrorMessage.aspx";        
        #endregion

        #region Other Constants
        public const string EXCEPTION_ERROR = "ExceptionError";
        #endregion

        #region Public Methods
        public static string GetUserFirstName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return string.Empty;

            try
            {
                if (userName.ToUpper().Trim() == "WATER TREATMENT TERMINAL")
                {
                    return userName;
                }

                string result = string.Empty;
                Match m = Regex.Match(userName, @"(\w*) (\w.*)");
                string firstName = m.Groups[1].ToString();

                if (!string.IsNullOrEmpty(firstName))
                {
                    System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                    System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
                    result = textInfo.ToTitleCase(firstName.ToLower().Trim());
                }

                return result;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string GetUserFirstName(string userName, bool isSpecialUser)
        {
            if (string.IsNullOrEmpty(userName))
                return string.Empty;

            if (isSpecialUser)
                return userName;

            try
            {
                string result = string.Empty;
                Match m = Regex.Match(userName, @"(\w*) (\w.*)");
                string firstName = m.Groups[1].ToString();

                if (!string.IsNullOrEmpty(firstName))
                {
                    System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                    System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
                    result = textInfo.ToTitleCase(firstName.ToLower().Trim());
                }

                return result;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string ConvertStringToTitleCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(input.ToLower().Trim());
        }

        public static int ConvertObjectToInt(object value)
        {
            int result;
            if (value != null && int.TryParse(value.ToString(), out result))
                return result;
            else
                return 0;
        }

        public static long ConvertObjectToLong(object value)
        {
            long result;
            if (value != null && long.TryParse(value.ToString(), out result))
                return result;
            else
                return 0;
        }

        public static double ConvertObjectToDouble(object value)
        {
            double result;
            if (value != null && double.TryParse(value.ToString(), out result))
                return result;
            else
                return 0;
        }

        public static bool ConvertObjectToBolean(object value)
        {
            bool result;
            if (value != null && bool.TryParse(value.ToString(), out result))
                return result;
            else
                return false;
        }

        public static bool ConvertNumberToBolean(object value)
        {
            if (value != null && Convert.ToInt32(value) == 1)
                return true;
            else
                return false;
        }

        public static DateTime? ConvertObjectToDate(object value)
        {
            try
            {
                if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name.Trim() != "en-GB")
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                }

                DateTime result;
                if (value != null && DateTime.TryParse(value.ToString(), out result))
                    return result;
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime? ConvertObjectToDate(object value, CultureInfo ci)
        {
            try
            {
                if (ci.Name.Trim() != "en-GB")
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                }

                DateTime result;
                if (value != null && DateTime.TryParse(value.ToString(), out result))
                    return result;
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string ConvertObjectToString(object value)
        {
            return value != null ? value.ToString().Trim() : string.Empty;
        }

        public static string ConvertObjectToDateString(object value)
        {
            DateTime result;

            try
            {
                if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name.Trim() != "en-GB")
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                }

                if (value != null && DateTime.TryParse(value.ToString(), out result))
                    return result.ToString("dd-MMM-yyyy");
                else
                    return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string ConvertObjectToDateTimeString(object value)
        {
            DateTime result;

            try
            {
                if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name.Trim() != "en-GB")
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                }

                if (value != null && DateTime.TryParse(value.ToString(), out result))
                    return result.ToString("dd-MMM-yyyy hh:mm tt");
                else
                    return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string ConvertDoubleToString(object value)
        {
            string result = string.Empty;

            try
            {
                double tempValue;
                if (value != null && double.TryParse(value.ToString(), out tempValue))
                {
                    result = string.Format("{0:N3}", tempValue);
                }

                return result;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static Dictionary<int, string> GetSQLDataTypes()
        {
            Dictionary<int, string> codes = new Dictionary<int, string>();
            codes.Add(0, "BigInt");
            codes.Add(1, "Binary");
            codes.Add(2, "Bit");
            codes.Add(3, "Char");
            codes.Add(4, "DateTime");
            codes.Add(5, "Decimal");
            codes.Add(6, "Float");
            codes.Add(7, "Image");
            codes.Add(8, "Int");
            codes.Add(9, "Money");
            codes.Add(10, "NChar");
            codes.Add(11, "NText");
            codes.Add(12, "NVarChar");
            codes.Add(13, "Real");
            codes.Add(14, "UniqueIdentifier");
            codes.Add(15, "SmallDateTime");
            codes.Add(16, "SmallInt");
            codes.Add(17, "SmallMoney");
            codes.Add(18, "Text");
            codes.Add(19, "Timestamp");
            codes.Add(20, "TinyInt");
            codes.Add(21, "VarBinary");
            codes.Add(22, "VarChar");
            codes.Add(23, "Variant");
            codes.Add(25, "Xml");
            codes.Add(29, "Udt");
            codes.Add(30, "Structured");
            codes.Add(31, "Date");
            codes.Add(32, "Time");
            codes.Add(33, "DateTime2");
            codes.Add(34, "DateTimeOffset");
            return codes;
        }

        public static string GetDictionaryValue(Dictionary<string, string> source, string key)
        {
            if (source == null || source.Count == 0)
                return string.Empty;

            if (source.ContainsKey(key))
            {
                string result;
                if (source.TryGetValue(key, out result))
                    return result;
            }
            return string.Empty;
        }

        public static string GetDictionaryKey(Dictionary<string, string> source, string value)
        {
            if (source == null || source.Count == 0)
                return string.Empty;

            if (source.ContainsValue(value))
            {
                string result = string.Empty;
                foreach (var item in source)
                {
                    if (item.Value.Trim() == value.Trim())
                    {
                        result = item.Key;
                        break;
                    }
                }
                return result;
            }
            return string.Empty;
        }

        public static string RemoveHTMLSpaceInText(string htmlText)
        {
            if (!htmlText.EndsWith("&nbsp;"))
                return htmlText;

            try
            {
                int pos = 0;
                while (htmlText.EndsWith("&nbsp;"))
                {
                    pos = htmlText.LastIndexOf("&nbsp;");
                    if (pos > 0)
                        htmlText += htmlText.Remove(pos);
                }
                return htmlText;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static void DisplayJavaScriptMessage(Control sender, string msg)
        {
            string promptTitle = "Information";
            string script = string.Format("DisplayAlert('{0}');", msg.Trim());
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), promptTitle, script, true);
        }

        public static void DisplayJSMessageWithPostback(Control sender, string msg, RadButton btnPostback, HiddenField hdnPostback, string actionCode)
        {
            string promptTitle = "Information";
            string script = string.Format("DisplayAlertWithPostback('{0}','{1}','{2}','{3}');", msg.Trim(), btnPostback.ClientID, hdnPostback.ClientID, actionCode);
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), promptTitle, script, true);
        }

        public static byte[] FromHex(string hex)
        {
            try
            {
                //hex = hex.Replace("-", "");
                byte[] raw = new byte[hex.Length / 2];
                for (int i = 0; i < raw.Length; i++)
                {
                    raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
                }
                return raw;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}