using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using GARMCO.AMS.B2B.eTendering.DAL;
using GARMCO.AMS.B2B.eTendering.Website.Helpers;
using GARMCO.AMS.B2B.Utility;
using GARMCO.AMS.Common.Web;
using Telerik.Web.UI;

namespace GARMCO.AMS.B2B.eTendering.Website.Account
{
	public partial class SupplierOrderAttachment : BaseWebForm
	{
		#region Constants
		private enum ToolButton : int
		{
			Text,
			Separator1,
			File,
			Separator2,
			Save,
			Separator3,
			Cancel,
			Separator4,
			Delete,
			NoSelection
		};
		#endregion

		#region Properties
		public SupplierOrderAttachItem CurrentSupplierOrderAttachItem
		{
			get
			{
				return ViewState["CurrentSupplierOrderAttachItem"] as SupplierOrderAttachItem;
			}

			set
			{
				ViewState["CurrentSupplierOrderAttachItem"] = value;
			}
		}

		public List<SupplierOrderAttachItem> SupplierOrderAttachmentList
		{
			get
			{
				List<SupplierOrderAttachItem> list = Session[B2BConstants.ITEM_LIST_SUPPLIER_ORDER_ATTACHMENT] as
					List<SupplierOrderAttachItem>;

				if (Session[B2BConstants.ITEM_LIST_SUPPLIER_ORDER_ATTACHMENT] == null)
					Session[B2BConstants.ITEM_LIST_SUPPLIER_ORDER_ATTACHMENT] = list = new List<SupplierOrderAttachItem>();

				return list;
			}

			set
			{
				Session[B2BConstants.ITEM_LIST_SUPPLIER_ORDER_ATTACHMENT] = value;
			}
		}

		public List<SupplierOrderAttachItem> SupplierOrderAttachmentListTemp
		{
			get
			{
				List<SupplierOrderAttachItem> list = Session[B2BConstants.ITEM_LIST_SUPPLIER_ORDER_ATTACHMENT_TEMP] as
					List<SupplierOrderAttachItem>;

				if (Session[B2BConstants.ITEM_LIST_SUPPLIER_ORDER_ATTACHMENT_TEMP] == null)
					Session[B2BConstants.ITEM_LIST_SUPPLIER_ORDER_ATTACHMENT_TEMP] = list = new List<SupplierOrderAttachItem>();

				return list;
			}

			set
			{
				Session[B2BConstants.ITEM_LIST_SUPPLIER_ORDER_ATTACHMENT_TEMP] = value;
			}
		}

		public bool IsForViewing
		{
			get
			{
				return Convert.ToBoolean(this.hidForViewing.Value);
			}

			set
			{
				this.hidForViewing.Value = value.ToString();
			}
		}

		private bool IsAlternateOrder
		{
			get
			{
				return Convert.ToBoolean(this.hidOrderAlternative.Value);
			}

			set
			{
				this.hidOrderAlternative.Value = value.ToString();
			}
		}

		public bool IsEditMode
		{
			get
			{
				bool isEditMode = false;
				if (ViewState["IsEditMode"] != null)
					isEditMode = Convert.ToBoolean(ViewState["IsEditMode"]);

				return isEditMode;
			}

			set
			{
				ViewState["IsEditMode"] = value;
			}
		}

		public B2BConstants.MediaObjectType SelectedMediaObjectType
		{
			get
			{
				return (B2BConstants.MediaObjectType)Enum.Parse(typeof(B2BConstants.MediaObjectType),
						this.hidSelectedMedObj.Value);
			}

			set
			{
				this.hidSelectedMedObj.Value = ((int)value).ToString();
			}
		}

		private string PreviousContent
		{
			get
			{
				string prevContent = String.Empty;
				if (ViewState["PreviousContent"] != null)
					prevContent = ViewState["PreviousContent"].ToString();

				return prevContent;
			}

			set
			{
				ViewState["PreviousContent"] = value;
			}
		}

		public bool IsRecordModified
		{
			get
			{
				bool isModified = false;
				if (ViewState["IsRecordModified"] != null)
					isModified = Convert.ToBoolean(ViewState["IsRecordModified"]);

				return isModified;
			}

			set
			{
				ViewState["IsRecordModified"] = value;
			}
		}
		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{

				// Configure splitter height
				ScriptManager.RegisterStartupScript(this, this.GetType(), "Height", "Sys.Application.add_load(ConfigureFolderContentHeight);", true);

			}
		}

		#region Override Methods
		protected override void OnInit(EventArgs e)
		{
			if (!this.IsPostBack)
			{

				#region Sets the parameters
				this.hidAjaxID.Value = Request.QueryString["ajaxID"];
				this.IsAlternateOrder = Convert.ToBoolean(Request.QueryString["orderDetAlt"]);
				this.IsForViewing = Convert.ToBoolean(Request.QueryString["forViewing"]);
				#endregion

				// Sets the maximum file that can be uploaded
				int maxFileUpload = Convert.ToInt32(ConfigurationManager.AppSettings["MAX_FILE_UPLOAD"]);
				this.litMaxFileSize.Text = String.Format("Maximum size per file is {0}MB ({1}KB) only.", maxFileUpload / 1000000, maxFileUpload / 1000);
				this.uploadMediaObjFile.MaxFileSize = maxFileUpload;

				// Sets the media object type
				if (!String.IsNullOrEmpty(Request.QueryString["orderDetAttachType"]) && !Request.QueryString["orderDetAttachType"].Equals("2"))
					this.SelectedMediaObjectType = (B2BConstants.MediaObjectType)Enum.Parse(typeof(B2BConstants.MediaObjectType),
						Request.QueryString["orderDetAttachType"]);

				if (this.IsForViewing)
				{

					this.mainToolBar.Enabled = false;

					this.btnSubmit.Visible = false;
					this.btnReset.Visible = false;

				}

				else if (this.SelectedMediaObjectType == B2BConstants.MediaObjectType.File)
					this.mainToolBar.Items[(int)ToolButton.Text].Enabled = false;

				else if (this.SelectedMediaObjectType == B2BConstants.MediaObjectType.Text)
					this.mainToolBar.Items[(int)ToolButton.File].Enabled = false;

				#region Sets the temporary list
				this.SupplierOrderAttachmentListTemp = new List<SupplierOrderAttachItem>();
				foreach (SupplierOrderAttachItem item in this.SupplierOrderAttachmentList)
					this.SupplierOrderAttachmentListTemp.Add(new SupplierOrderAttachItem(item));
				#endregion

			}

			base.OnInit(e);
		}
		#endregion

		#region Private Methods
		private void UpdateSupplierOrderAttachmentList()
		{
			// Copy back all media objects
			this.SupplierOrderAttachmentList.Clear();
			foreach (SupplierOrderAttachItem item in this.SupplierOrderAttachmentListTemp)
				this.SupplierOrderAttachmentList.Add(new SupplierOrderAttachItem(item));

			// Release the temporary
			this.SupplierOrderAttachmentListTemp.Clear();

			ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Back",
				String.Format("OnCloseSupplierOrderAttachmentLookup('{0}');", this.hidAjaxID.Value), true);
		}

		private void SetSupplierOrderAttachType(ToolButton toolButton, bool isNewMediaObject = true)
		{
			#region Text or File has been clicked
			if (toolButton == ToolButton.Text || toolButton == ToolButton.File)
			{

				this.mainToolBar.Items[(int)ToolButton.Text].Enabled = false;
				this.mainToolBar.Items[(int)ToolButton.File].Enabled = false;

				#region Checks if for viewing only
				if (this.IsForViewing)
				{

					// Hide both panels
					this.panText.Visible = false;
					this.panFile.Visible = false;

				}

				else
				{

					this.mainToolBar.Items[(int)ToolButton.Save].Enabled = true && toolButton == ToolButton.Text &&
						(this.SelectedMediaObjectType == B2BConstants.MediaObjectType.NoMediaObjType || this.SelectedMediaObjectType == B2BConstants.MediaObjectType.Text);
					this.mainToolBar.Items[(int)ToolButton.Cancel].Enabled = true;

					// Sets a new current media object file and sequence number
					if (isNewMediaObject)
					{

						this.CurrentSupplierOrderAttachItem = new SupplierOrderAttachItem();
						this.CurrentSupplierOrderAttachItem.OrderAttachSeq = this.SupplierOrderAttachmentListTemp.Count > 0 ?
							this.SupplierOrderAttachmentListTemp.Count + 1 : 1;

						// Resets previous content
						this.PreviousContent = String.Empty;

					}

					else
					{

						// Enable delete toolbar
						this.mainToolBar.Items[(int)ToolButton.Delete].Enabled = this.SelectedMediaObjectType == B2BConstants.MediaObjectType.NoMediaObjType ||
							(toolButton == ToolButton.Text && this.SelectedMediaObjectType == B2BConstants.MediaObjectType.Text) ||
							(toolButton == ToolButton.File && this.SelectedMediaObjectType == B2BConstants.MediaObjectType.File);

					}

					// Disables the action buttons
					this.EnableObjects(false);

				}
				#endregion

				if (toolButton == ToolButton.Text)
					this.panText.Visible = true;

				else
				{

					this.panFile.Visible = true;
					this.panFileIput.Visible = isNewMediaObject;
					this.panFileDisplay.Visible = !isNewMediaObject;

				}
			}
			#endregion

			#region Cancel the action
			else if (toolButton == ToolButton.Cancel)
			{

				// Checks if any changes
				if (this.CurrentSupplierOrderAttachItem.OrderAttachType == B2BConstants.MediaObjectType.Text &&
					!this.PreviousContent.Equals(this.txtMediaObjText.Content))
				{

					StringBuilder script = new StringBuilder();

					script.Append("ShowConfirmMessageWithuBttonIndicatorRetArg('Are you sure you want to cancel the modification on this media object?<br />Please click Ok if yes, otherwise Cancel.', '");
					script.Append(this.ajaxMngr.ClientID);
					script.Append("', '");
					script.Append(B2BConstants.ButtonClickEvent.ButtonCancel);
					script.Append("', 1);");

					ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", script.ToString(), true);

				}

				else
				{

					this.mainToolBar.Items[(int)ToolButton.Text].Enabled = this.SelectedMediaObjectType == B2BConstants.MediaObjectType.NoMediaObjType ||
						this.SelectedMediaObjectType == B2BConstants.MediaObjectType.Text;
					this.mainToolBar.Items[(int)ToolButton.File].Enabled = this.SelectedMediaObjectType == B2BConstants.MediaObjectType.NoMediaObjType ||
						this.SelectedMediaObjectType == B2BConstants.MediaObjectType.File;

					this.mainToolBar.Items[(int)ToolButton.Save].Enabled = false;
					this.mainToolBar.Items[(int)ToolButton.Cancel].Enabled = false;
					this.mainToolBar.Items[(int)ToolButton.Delete].Enabled = false;

					this.panText.Visible = false;
					this.panFile.Visible = false;

					// Clear the content
					this.txtMediaObjText.Content = String.Empty;

					// Enables the action buttons
					this.EnableObjects(true);

				}
			}
			#endregion

			#region Delete the item
			else if (toolButton == ToolButton.Delete)
			{

				StringBuilder script = new StringBuilder();

				script.Append("ShowConfirmMessageWithuBttonIndicatorRetArg('Are you sure you want to delete this media object?<br />Please click Ok if yes, otherwise Cancel.', '");
				script.Append(this.ajaxMngr.ClientID);
				script.Append("', '");
				script.Append(B2BConstants.ButtonClickEvent.ButtonDelete);
				script.Append("', 1);");

				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", script.ToString(), true);

			}
			#endregion

			#region Save the text content
			else if (toolButton == ToolButton.Save)
			{

				#region Store the contect in the media object item
				this.CurrentSupplierOrderAttachItem.OrderAttachType = B2BConstants.MediaObjectType.Text;
				this.CurrentSupplierOrderAttachItem.OrderAttachContentHtml = this.txtMediaObjText.Content;
				this.CurrentSupplierOrderAttachItem.IsMediaObjectConvertHtmlOK = true;

				// Clear the content
				this.txtMediaObjText.Content = String.Empty;
				#endregion

				#region Add to the list
				if (this.CurrentSupplierOrderAttachItem.OrderAttachSODID == -1 && this.CurrentSupplierOrderAttachItem.OrderAttachSODAltID == -1)
				{

					if (this.IsAlternateOrder)
						this.CurrentSupplierOrderAttachItem.OrderAttachSODAltID = 0;

					else
						this.CurrentSupplierOrderAttachItem.OrderAttachSODID = 0;

					this.CurrentSupplierOrderAttachItem.Added = true;
					this.CurrentSupplierOrderAttachItem.OrderAttachSupplier = true;
					this.CurrentSupplierOrderAttachItem.OrderAttachDisplayName = String.Format("Text{0}",
						this.SupplierOrderAttachmentListTemp.FindAll(tempItem =>
							tempItem.OrderAttachType == B2BConstants.MediaObjectType.Text).Count + 1);
					this.CurrentSupplierOrderAttachItem.OrderAttachSeq = this.SupplierOrderAttachmentListTemp.Count == 0 ? 1 :
						this.SupplierOrderAttachmentListTemp.Max(tempItem => tempItem.OrderAttachSeq) + 1;
					this.SupplierOrderAttachmentListTemp.Add(this.CurrentSupplierOrderAttachItem);

				}
				#endregion

				#region Update the record
				else
				{

					SupplierOrderAttachItem medObjItem = this.SupplierOrderAttachmentListTemp.Find(tempItem => !tempItem.MarkForDeletion &&
						tempItem.OrderAttachType == this.CurrentSupplierOrderAttachItem.OrderAttachType &&
						tempItem.OrderAttachSeq == this.CurrentSupplierOrderAttachItem.OrderAttachSeq);
					if (medObjItem != null)
					{

						this.CurrentSupplierOrderAttachItem.Modified = true;
						medObjItem.SetSupplierOrderAttachItem(this.CurrentSupplierOrderAttachItem);

					}
				}
				#endregion

				this.mainToolBar.Items[(int)ToolButton.Text].Enabled = true;
				this.mainToolBar.Items[(int)ToolButton.File].Enabled = true;

				this.mainToolBar.Items[(int)ToolButton.Save].Enabled = false;
				this.mainToolBar.Items[(int)ToolButton.Cancel].Enabled = false;
				this.mainToolBar.Items[(int)ToolButton.Delete].Enabled = false;

				this.panText.Visible = false;
				this.panFile.Visible = false;

				// Sets the modification flag
				this.IsRecordModified = true;

				// Enables the action buttons
				this.EnableObjects(true);

			}
			#endregion

			#region Reset
			else
			{

				this.mainToolBar.Items[(int)ToolButton.Text].Enabled = true;
				this.mainToolBar.Items[(int)ToolButton.File].Enabled = true;

				this.mainToolBar.Items[(int)ToolButton.Save].Enabled = false;
				this.mainToolBar.Items[(int)ToolButton.Cancel].Enabled = false;
				this.mainToolBar.Items[(int)ToolButton.Delete].Enabled = false;

				this.panText.Visible = false;
				this.panFile.Visible = false;

				this.txtMediaObjText.Content = String.Empty;
				this.CurrentSupplierOrderAttachItem = null;

				// Enables the action buttons
				this.EnableObjects(true);

			}
			#endregion

			// Rebind the data
			this.lstMediaObj.Rebind();
		}

		private void EnableObjects(bool enable)
		{
			this.IsEditMode = !enable;

			// Enables or disables action buttons
			this.btnSubmit.Enabled = enable;
			this.btnReset.Enabled = enable;
			this.btnBack.Enabled = enable;
		}
		#endregion

		#region General Events
		protected void mainToolBar_PreRender(object sender, EventArgs e)
		{

		}

		protected void ajaxMngr_AjaxRequest(object sender, AjaxRequestEventArgs e)
		{
			string returnValue = e.Argument;
			if (returnValue.IndexOf(B2BConstants.AJAX_RETURN_CONFIRMED) > -1)
			{

				// Parse the argument
				string[] argList = returnValue.Split(B2BConstants.LIST_SEPARATOR);
				bool confirmed = Convert.ToBoolean(argList[2]);

				#region Checks which button
				// Delete
				if (argList[1] == B2BConstants.ButtonClickEvent.ButtonDelete.ToString() && confirmed)
				{

					#region Delete the item
					SupplierOrderAttachItem medObjItem = this.SupplierOrderAttachmentListTemp.Find(tempItem => !tempItem.MarkForDeletion &&
						tempItem.OrderAttachType == this.CurrentSupplierOrderAttachItem.OrderAttachType &&
						tempItem.OrderAttachSeq == this.CurrentSupplierOrderAttachItem.OrderAttachSeq);
					if (medObjItem != null)
					{

						// Checks if already in the database
						if (medObjItem.OrderAttachSODID > 0 || medObjItem.OrderAttachSODAltID > 0)
							medObjItem.MarkForDeletion = true;

						else
						{

							#region Deletes the file permanently
							if (medObjItem.OrderAttachType == B2BConstants.MediaObjectType.File)
							{                                
								if (this.ImpersonateValidUser(ConfigurationManager.AppSettings["impersonateUser"], ConfigurationManager.AppSettings["impersonatePassword"],
									ConfigurationManager.AppSettings["impersonateDomain"]))
								{
                                    bool isTestMode = UILookup.ConvertNumberToBolean(ConfigurationManager.AppSettings["TestMode"]);

                                    if (!isTestMode)
                                    {
                                        if (File.Exists(Server.MapPath(Path.Combine(
                                            ConfigurationManager.AppSettings["SUPPLIER_SOURCE_PATH"], medObjItem.OrderAttachFilename))))
                                        {
                                            File.Delete(Server.MapPath(Path.Combine(
                                                ConfigurationManager.AppSettings["SUPPLIER_SOURCE_PATH"], medObjItem.OrderAttachFilename)));
                                        }
                                    }
                                    else
                                    {
                                        if (File.Exists(string.Concat(ConfigurationManager.AppSettings["SUPPLIER_SOURCE_PATH"], medObjItem.OrderAttachFilename)))
                                        {
                                            File.Delete(string.Concat(ConfigurationManager.AppSettings["SUPPLIER_SOURCE_PATH"], medObjItem.OrderAttachFilename));
                                        }
                                    }

									// Resets the impersonation
									this.UndoImpersonation();
								}
							}
							#endregion

							this.SupplierOrderAttachmentListTemp.Remove(medObjItem);

						}

						// Sets the modification flag
						this.IsRecordModified = true;

					}

					this.SetSupplierOrderAttachType(ToolButton.NoSelection);
					#endregion

				}

				// Cancel
				else if (argList[1] == B2BConstants.ButtonClickEvent.ButtonCancel.ToString() && confirmed)
					this.SetSupplierOrderAttachType(ToolButton.NoSelection);

				// Clear
				else if (argList[1] == this.btnReset.ID && confirmed)
				{

					#region Removes all items
					for (int i = this.SupplierOrderAttachmentListTemp.Count - 1; i > -1; i--)
					{

						SupplierOrderAttachItem medObjItem = this.SupplierOrderAttachmentListTemp[i];
						if (medObjItem.OrderAttachSODID > 0 || medObjItem.OrderAttachSODAltID > 0)
							medObjItem.MarkForDeletion = true;

						else
						{

							#region Deletes the file permanently
							if (medObjItem.OrderAttachType == B2BConstants.MediaObjectType.File)
							{

								if (this.ImpersonateValidUser(ConfigurationManager.AppSettings["impersonateUser"], ConfigurationManager.AppSettings["impersonatePassword"],
									ConfigurationManager.AppSettings["impersonateDomain"]))
								{
                                    bool isTestMode = UILookup.ConvertNumberToBolean(ConfigurationManager.AppSettings["TestMode"]);

                                    if (!isTestMode)
                                    {
                                        if (File.Exists(Server.MapPath(Path.Combine(
                                            ConfigurationManager.AppSettings["SUPPLIER_SOURCE_PATH"], medObjItem.OrderAttachFilename))))
                                        {
                                            File.Delete(Server.MapPath(Path.Combine(
                                                ConfigurationManager.AppSettings["SUPPLIER_SOURCE_PATH"], medObjItem.OrderAttachFilename)));
                                        }
                                    }
                                    else
                                    {
                                        if (File.Exists(string.Concat(ConfigurationManager.AppSettings["SUPPLIER_SOURCE_PATH"], medObjItem.OrderAttachFilename)))
                                        {
                                            File.Delete(string.Concat(ConfigurationManager.AppSettings["SUPPLIER_SOURCE_PATH"], medObjItem.OrderAttachFilename));
                                        }
                                    }

									// Resets the impersonation
									this.UndoImpersonation();
								}
							}
							#endregion

							this.SupplierOrderAttachmentListTemp.Remove(medObjItem);

						}
					}
					#endregion

					// Sets the modification flag
					this.IsRecordModified = true;

					// Rebind the list
					this.lstMediaObj.Rebind();

				}

				// Back
				else if (argList[1] == this.btnBack.ID)
				{

					// Checks if confirmed
					if (confirmed)
						this.UpdateSupplierOrderAttachmentList();

					else
						ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Back", "OnCloseWindow();", true);

				}
				#endregion
			}
		}
		#endregion

		#region Action Buttons
		protected void mainToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
		{
			// Checks which button was clicked
			this.SetSupplierOrderAttachType((ToolButton)Enum.Parse(typeof(ToolButton), (e.Item as RadToolBarButton).CommandName));
		}

		protected void btnSubmit_Click(object sender, EventArgs e)
		{
			this.UpdateSupplierOrderAttachmentList();
		}

		protected void btnReset_Click(object sender, EventArgs e)
		{
			if (this.SupplierOrderAttachmentListTemp.FindAll(tempItem => !tempItem.MarkForDeletion).Count > 0)
			{

				StringBuilder script = new StringBuilder();

				script.Append("ShowConfirmMessageWithuBttonIndicatorRetArg('Are you sure you want to remove all the items in the list?<br />Please click Ok if yes, otherwise Cancel.', '");
				script.Append(this.ajaxMngr.ClientID);
				script.Append("', '");
				script.Append(this.btnReset.ID);
				script.Append("', 1);");

				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Reset", script.ToString(), true);

			}
		}

		protected void btnBack_Click(object sender, EventArgs e)
		{
			// Checks if record has been modified
			if (this.IsRecordModified)
			{

				StringBuilder script = new StringBuilder();

				script.Append("ShowWarningMsg('");
				script.Append(this.ajaxMngr.ClientID);
				script.Append("', '");
				script.Append(this.btnBack.ID);
				script.Append("', '");
				script.Append(Server.UrlEncode("Do you want to save first all modifications you have made before closing?"));
				script.Append("');");

				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Back", script.ToString(), true);

			}

			else
				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Back", "OnCloseWindow();", true);
		}

		protected void btnUploadFile_Click(object sender, EventArgs e)
		{
			if (this.uploadMediaObjFile.UploadedFiles.Count > 0)
			{
                try
                {
                    bool isTestMode = UILookup.ConvertNumberToBolean(ConfigurationManager.AppSettings["TestMode"]);

                    // Retrieve the folder where the files will be saved
                    string uploadFolder = ConfigurationManager.AppSettings["SUPPLIER_SOURCE_PATH"];

                    if (isTestMode)
                    {
                        #region Impersonate other user to upload to a shared folder
                        if (this.ImpersonateValidUser(ConfigurationManager.AppSettings["impersonateUser"], ConfigurationManager.AppSettings["impersonatePassword"],
                            ConfigurationManager.AppSettings["impersonateDomain"]))
                        {
                            foreach (UploadedFile file in this.uploadMediaObjFile.UploadedFiles)
                            {
                                // Checks if the file does exists already, rename any special characters that will cause some issue when opening
                                string targetFilename = file.GetName().Replace("#", "No.");

                                if (File.Exists(string.Concat(uploadFolder, targetFilename)))
                                {
                                    // Rename the file
                                    targetFilename = String.Format("{0}_{1}{2}",
                                        file.GetNameWithoutExtension(), DateTime.Now.Ticks, file.GetExtension());
                                }

                                // Save the file
                                file.SaveAs(string.Concat(uploadFolder, targetFilename));

                                #region Sets the file info to the supplier order attachment item
                                this.CurrentSupplierOrderAttachItem.Added = true;
                                if (this.IsAlternateOrder)
                                    this.CurrentSupplierOrderAttachItem.OrderAttachSODAltID = 0;

                                else
                                    this.CurrentSupplierOrderAttachItem.OrderAttachSODID = 0;

                                this.CurrentSupplierOrderAttachItem.OrderAttachSupplier = true;
                                this.CurrentSupplierOrderAttachItem.OrderAttachType = B2BConstants.MediaObjectType.File;
                                this.CurrentSupplierOrderAttachItem.OrderAttachDisplayName = file.GetName();
                                this.CurrentSupplierOrderAttachItem.OrderAttachFilename = targetFilename;
                                #endregion

                            }

                            // Resets the impersonation
                            this.UndoImpersonation();

                            // Add the item to the list
                            this.SupplierOrderAttachmentListTemp.Add(this.CurrentSupplierOrderAttachItem);

                            // Resets the toolbar
                            this.SetSupplierOrderAttachType(ToolButton.Cancel);

                        }
                        #endregion
                    }
                    else
                    {
                        #region Do not impersonate
                        
                        foreach (UploadedFile file in this.uploadMediaObjFile.UploadedFiles)
                        {
                            // Checks if the file does exists already, rename any special characters that will cause some issue when opening
                            string targetFilename = file.GetName().Replace("#", "No.");

                            if (File.Exists(Server.MapPath(Path.Combine(uploadFolder, targetFilename))))
                            {
                                // Rename the file
                                targetFilename = String.Format("{0}_{1}{2}",
                                    file.GetNameWithoutExtension(), DateTime.Now.Ticks, file.GetExtension());
                            }

                            // Save the file
                            file.SaveAs(Server.MapPath(Path.Combine(uploadFolder, targetFilename)));

                            #region Sets the file info to the supplier order attachment item
                            this.CurrentSupplierOrderAttachItem.Added = true;
                            if (this.IsAlternateOrder)
                                this.CurrentSupplierOrderAttachItem.OrderAttachSODAltID = 0;

                            else
                                this.CurrentSupplierOrderAttachItem.OrderAttachSODID = 0;

                            this.CurrentSupplierOrderAttachItem.OrderAttachSupplier = true;
                            this.CurrentSupplierOrderAttachItem.OrderAttachType = B2BConstants.MediaObjectType.File;
                            this.CurrentSupplierOrderAttachItem.OrderAttachDisplayName = file.GetName();
                            this.CurrentSupplierOrderAttachItem.OrderAttachFilename = targetFilename;
                            #endregion

                        }

                        // Add the item to the list
                        this.SupplierOrderAttachmentListTemp.Add(this.CurrentSupplierOrderAttachItem);

                        // Resets the toolbar
                        this.SetSupplierOrderAttachType(ToolButton.Cancel);
                        
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex);
                }
			}
		}
		#endregion

		#region Data Binding
		protected void lstMediaObj_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
		{
			this.lstMediaObj.DataSource = this.SupplierOrderAttachmentListTemp.FindAll(tempItem => !tempItem.MarkForDeletion);
		}

		protected void lstMediaObj_ItemDataBound(object sender, RadListViewItemEventArgs e)
		{
			if (e.Item.ItemType == RadListViewItemType.AlternatingItem || e.Item.ItemType == RadListViewItemType.DataItem ||
				e.Item.ItemType == RadListViewItemType.SelectedItem)
			{

				RadListViewDataItem item = e.Item as RadListViewDataItem;
				if (item != null)
				{

					#region Formats the display
					HtmlImage imgMediaObjectType = item.FindControl("imgMediaObjectType") as HtmlImage;
					B2BConstants.MediaObjectType medObjType = (B2BConstants.MediaObjectType)Enum.Parse(typeof(B2BConstants.MediaObjectType),
						(item.FindControl("litMediaObjectType") as Literal).Text);

					if (medObjType == B2BConstants.MediaObjectType.Text)
						imgMediaObjectType.Src = this.IsForViewing || !this.IsEditMode || this.SelectedMediaObjectType == B2BConstants.MediaObjectType.Text ? "~/includes/images/toolText_f1.gif" : "~/includes/images/toolText_f3.gif";

					else
						imgMediaObjectType.Src = this.IsForViewing || !this.IsEditMode || this.SelectedMediaObjectType == B2BConstants.MediaObjectType.Text ? "~/includes/images/toolFile_f1.gif" : "~/includes/images/toolFile_f3.gif";

					(item.FindControl("lnkSelect") as LinkButton).Enabled = this.IsForViewing || !this.IsEditMode;
					#endregion

				}
			}
		}

		protected void lstMediaObj_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Checks if a record is selected
			if (this.lstMediaObj.SelectedItems.Count > 0)
			{

				RadListViewDataItem item = this.lstMediaObj.SelectedItems[0];

				B2BConstants.MediaObjectType medObjType = (B2BConstants.MediaObjectType)Enum.Parse(typeof(B2BConstants.MediaObjectType),
					(item.FindControl("litMediaObjectType") as Literal).Text);
				decimal medObjSeqNo = Convert.ToDecimal((item.FindControl("litMediaObjectSequence") as Literal).Text);

				// Retrieve the Media Object Item
				this.CurrentSupplierOrderAttachItem = this.SupplierOrderAttachmentListTemp.Find(tempItem => !tempItem.MarkForDeletion &&
					tempItem.OrderAttachType == medObjType && tempItem.OrderAttachSeq == medObjSeqNo);

				// Checks if found
				if (this.CurrentSupplierOrderAttachItem != null)
				{

					// Checks the media object type
					if (medObjType == B2BConstants.MediaObjectType.Text)
					{

						// Sets the text and store as previous
						this.PreviousContent = this.txtMediaObjText.Content = this.CurrentSupplierOrderAttachItem.IsMediaObjectConvertHtmlOK ?
							this.CurrentSupplierOrderAttachItem.OrderAttachContentHtml : this.CurrentSupplierOrderAttachItem.GetMediaObjectTextHtml(ConfigurationManager.AppSettings["RTF_HTML_KEY"]);
						this.txtMediaObjText.Enabled = !this.IsForViewing &&
							(this.SelectedMediaObjectType == B2BConstants.MediaObjectType.NoMediaObjType || this.SelectedMediaObjectType == B2BConstants.MediaObjectType.Text);

						this.SetSupplierOrderAttachType(ToolButton.Text, false);

					}

					else
					{
                        bool isTestMode = UILookup.ConvertNumberToBolean(ConfigurationManager.AppSettings["TestMode"]);
						this.lnkFile.Text = this.CurrentSupplierOrderAttachItem.OrderAttachDisplayName;

                        if (this.CurrentSupplierOrderAttachItem.OrderAttachType == B2BConstants.MediaObjectType.File)
                        {
                            if (!isTestMode)
                            {
                                this.lnkFile.NavigateUrl = (ConfigurationManager.AppSettings["SUPPLIER_SOURCE_PATH"] +
                                    this.CurrentSupplierOrderAttachItem.OrderAttachFilename).Replace("\\\\", "\\");
                            }
                            else
                            {
                                this.lnkFile.NavigateUrl = String.Format("~/CommonObject/FileHandler.ashx?filename={0}",
                                    Server.UrlEncode(this.CurrentSupplierOrderAttachItem.OrderAttachFilename));
                            }
                        }
                        else if (this.CurrentSupplierOrderAttachItem.OrderAttachType == B2BConstants.MediaObjectType.HtmlUpload)
                        {
                            this.lnkFile.NavigateUrl = this.CurrentSupplierOrderAttachItem.OrderAttachFilename;
                        }

						this.SetSupplierOrderAttachType(ToolButton.File, false);

					}
				}
			}
		}
		#endregion
	}
}