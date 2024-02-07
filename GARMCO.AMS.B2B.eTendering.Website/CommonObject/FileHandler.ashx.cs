using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using GARMCO.AMS.B2B.eTendering.Website.Helpers;
using GARMCO.AMS.B2B.Admin.DAL.Helpers;

namespace GARMCO.AMS.B2B.eTendering.Website.CommonObject
{
	/// <summary>
	/// Summary description for FileHandler
	/// </summary>
	public class FileHandler : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
            if (!this.GetAllowedExtensions().Contains(this.GetExtension(this.GetPath(context))))
            {
                context.Response.Write(string.Format("Files with extensions {0} are allowed only!", this.GetAllowedExtensions().Aggregate((x, y) => x + ", " + y)));
                return;
            }

			// Added to allow firefox, chrome browers that it's dealing with css
			context.Response.ContentType = "text/css";

			// Retrieves the file and display it
			if (!String.IsNullOrEmpty(context.Request.QueryString["filename"]))
			{
                bool isTestMode = UILookup.ConvertNumberToBolean(ConfigurationManager.AppSettings["TestMode"]);
				string filename = String.Empty;

				if (UILookup.ConvertObjectToString(context.Request.QueryString["type"]).Equals("profile"))
				{

					filename = HttpContext.Current.Server.MapPath(String.Format("~/Account/Profile/{0}",
						context.Request.QueryString["filename"]));

				}

                else if (UILookup.ConvertObjectToString(context.Request.QueryString["type"]).Equals("j"))
                {
                    if (!isTestMode)
                    {
                        filename = HttpContext.Current.Server.MapPath(String.Format("{0}{1}",
                            ConfigurationManager.AppSettings["JDE_SOURCE_PATH"], context.Request.QueryString["filename"]));
                    }
                    else
                    {
                        filename = string.Concat(UILookup.ConvertObjectToString(ConfigurationManager.AppSettings["JDE_SOURCE_PATH"]),
                            UILookup.ConvertObjectToString(context.Request.QueryString["filename"]));
                    }
                }
                else
                {
                    if (!isTestMode)
                    {
                        filename = HttpContext.Current.Server.MapPath(String.Format("{0}{1}",
                            ConfigurationManager.AppSettings["SUPPLIER_SOURCE_PATH"], context.Request.QueryString["filename"]));
                    }
                    else
                    {
                        filename = string.Concat(UILookup.ConvertObjectToString(ConfigurationManager.AppSettings["SUPPLIER_SOURCE_PATH"]),
                            UILookup.ConvertObjectToString(context.Request.QueryString["filename"]));
                    }
                }

                if (File.Exists(filename))
                {

                    #region Sets the mime type
                    // Get the mimetype
                    string mimeType = GetMimeType(filename);

                    // Render the file
                    WebClient client = new WebClient();
                    Byte[] buffer = client.DownloadData(filename);
                    if (buffer != null && mimeType != string.Empty)
                    {

                        context.Response.ContentType = mimeType;
                        context.Response.AddHeader("content-length", buffer.Length.ToString());
                        context.Response.BinaryWrite(buffer);

                    }
                    #endregion
                }
                else
                {
                    #region This is a media object file
                    Byte[] fileData = ADONetDataService.GetMediaObjectFileFromDatabase(context.Request.QueryString["filename"].ToString().Replace("|", @"\"));
                    if (fileData != null)
                    {
                        context.Response.Buffer = true;
                        context.Response.Charset = "";
                        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        context.Response.ContentType = this.GetMimeType(context.Request.QueryString["filename"].ToString());
                        context.Response.AddHeader("content-disposition", "attachment;filename="
                        + context.Request.QueryString["filename"].Replace("|", @"\").ToString());
                        context.Response.BinaryWrite(fileData);
                        context.Response.Flush();
                        context.Response.End();
                    }
                    else
                    {
                        context.Response.Write("Sorry, the file cannot be found.");
                    }
                    #endregion                    
                }
			}
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		private string GetMimeType(string fileName)
		{
			try
			{
				//Initialize to default mime-type
				string mimeType = "application/base64";

				string ext = Path.GetExtension(fileName).ToLower();
				Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);

				if (regKey != null && regKey.GetValue("Content Type") != null)
					mimeType = regKey.GetValue("Content Type").ToString();
				else if (ext == ".png")
					mimeType = "image/png";
				else if (ext == ".flv")
					mimeType = "video/x-flv";

				else if (ext == ".pdf")
					mimeType = "application/pdf";

				return mimeType;
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}

        private string[] GetAllowedExtensions()
        {
            return ConfigurationManager.AppSettings["extensions"].Split(';');
        }
        private string GetExtension(string path)
        {
            return Path.GetExtension(path).Replace(".", "");
        }
        private string GetPath(HttpContext context)
        {
            return context.Request.QueryString["filename"];
        }
	}
}