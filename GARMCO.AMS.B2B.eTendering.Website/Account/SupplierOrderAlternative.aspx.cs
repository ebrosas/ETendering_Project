using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GARMCO.AMS.B2B.DAL;
using GARMCO.AMS.B2B.eTendering.DAL;
using GARMCO.AMS.B2B.Utility;
using GARMCO.AMS.Common.Web;
using Telerik.Web.UI;

namespace GARMCO.AMS.B2B.eTendering.Website.Account
{
	public partial class SupplierOrderAlternative : BaseWebForm
	{
		#region Constants
		public enum AlterOfferColumn : int
		{
			DeleteCol
		};
		#endregion

		#region Properties
		public SupplierOrderDetailItem SupplierOrderDetailCurrent
		{
			get
			{
				return Session[B2BConstants.ITEM_LIST_SUPPLIER_ORDER_DETAIL_CURRENT] as SupplierOrderDetailItem;
			}
		}

		public List<SupplierOrderDetailAltItem> SupplierOrderDetailAlternativeListTemp
		{
			get
			{
				List<SupplierOrderDetailAltItem> list = Session[B2BConstants.ITEM_LIST_SUPPLIER_ORDER_ALTERNATIVE_TEMP] as
					List<SupplierOrderDetailAltItem>;
				if (list == null)
					Session[B2BConstants.ITEM_LIST_SUPPLIER_ORDER_ALTERNATIVE_TEMP] = list = new List<SupplierOrderDetailAltItem>();

				return list;
			}

			set
			{
				Session[B2BConstants.ITEM_LIST_SUPPLIER_ORDER_ALTERNATIVE_TEMP] = value;
			}
		}

		public List<SupplierOrderAttachItem> SupplierOrderAltAttachmentList
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

		public string SODAltCurrencyCode
		{
			get
			{
				return this.hidSODAltCurrencyCode.Value;
			}

			set
			{
				this.hidSODAltCurrencyCode.Value = value;
			}
		}
		public string SODAltDelTerm
		{
			get
			{
				return this.hidSODAltDelTerm.Value;
			}

			set
			{
				this.hidSODAltDelTerm.Value = value;
			}
		}
		public DateTime? SODAltValidityPeriod
		{
			get
			{

				DateTime? sodAltValidityPeriod = null;
				if (!String.IsNullOrEmpty(this.hidSODAltValidityPeriod.Value))
					sodAltValidityPeriod = Convert.ToDateTime(this.hidSODAltValidityPeriod.Value);

				return sodAltValidityPeriod;
			}

			set
			{
				this.hidSODAltValidityPeriod.Value = value == null ? String.Empty : value.ToString();
			}
		}
		public int SODAltDeliveryTime
		{
			get
			{
				int sodAltDeliveryTime = 0;
				if (!String.IsNullOrEmpty(this.hidSODAltDeliveryTime.Value))
					sodAltDeliveryTime = Convert.ToInt32(this.hidSODAltDeliveryTime.Value);

				return sodAltDeliveryTime;
			}

			set
			{
				this.hidSODAltDeliveryTime.Value = value.ToString();
			}
		}
		public string SODAltDeliveryPt
		{
			get
			{
				return this.hidSODAltDeliveryPt.Value;
			}

			set
			{
				this.hidSODAltDeliveryPt.Value = value;
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

		#region Private Data Members
		private List<SupplierOrderAttachItem> _tempAttachList = null;
		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			if (this.IsPostBack)
			{
			}
		}

		#region Override Methods
		protected override void OnInit(EventArgs e)
		{
			if (!this.IsPostBack)
			{

				#region Set the current order detail and copies current order alternatives
				if (this.SupplierOrderDetailCurrent != null)
				{

					this.hidAjaxID.Value = Request.QueryString["ajaxID"];
					this.IsForViewing = Convert.ToBoolean(Request.QueryString["forViewing"]);

					this.hidSODAltCurrencyCode.Value = Request.QueryString["sodAltCurrencyCode"];
					this.hidSODAltDelTerm.Value = Request.QueryString["sodAltDelTerm"];
					this.hidSODAltValidityPeriod.Value = Request.QueryString["sodAltValidityPeriod"];
					this.hidSODAltDeliveryTime.Value = Request.QueryString["sodAltDeliveryTime"];
					this.hidSODAltDeliveryPt.Value = Server.UrlDecode(Request.QueryString["sodAltDeliveryPt"]);

					#region Sets the supplier order detail
					this.litOrderDetItemCode.Text = this.SupplierOrderDetailCurrent.OrderDetItemCode;
					this.litOrderDetDesc.Text = this.SupplierOrderDetailCurrent.OrderDetDesc;
					this.litOrderDetQuantity.Text = this.SupplierOrderDetailCurrent.OrderDetQuantity.ToString();
					this.litOrderDetUM.Text = this.SupplierOrderDetailCurrent.OrderDetUM;
					this.litSODQuantity.Text = this.SupplierOrderDetailCurrent.SODQuantity.ToString();
					this.litSODUnitCost.Text = this.SupplierOrderDetailCurrent.SODUnitCost.ToString();
					this.litSODSupplierCode.Text = this.SupplierOrderDetailCurrent.SODSupplierCode;
					this.litSODManufacturerCode.Text = this.SupplierOrderDetailCurrent.SODManufacturerCode;
					this.litSODRemarks.Text = this.SupplierOrderDetailCurrent.SODRemarks;
					#endregion

					this.SupplierOrderDetailAlternativeListTemp.Clear();
					foreach (SupplierOrderDetailAltItem altItem in this.SupplierOrderDetailCurrent.SupplierOrderDetAltList)
						this.SupplierOrderDetailAlternativeListTemp.Add(new SupplierOrderDetailAltItem(altItem));

					#region Updates the action buttons
					if (this.IsForViewing)
					{

						this.btnSubmit.Visible = false;
						this.btnReset.Visible = false;

					}
					#endregion
				}
				#endregion
			}

			base.OnInit(e);
		}
		#endregion

		#region Private Methods
		private void UpdateAlternativeOfferEntry()
		{
			foreach (GridDataItem item in this.gvOrderAlt.MasterTableView.GetItems(GridItemType.AlternatingItem, GridItemType.Item))
			{

				// Retrieves the item
				double sodAltLineNo = Convert.ToDouble(item["SODAltLineNo"].Text);
				SupplierOrderDetailAltItem supOrderAltItem = this.SupplierOrderDetailAlternativeListTemp.Find(tempItem => tempItem.SODAltLineNo == sodAltLineNo);
				if (supOrderAltItem == null)
				{

					supOrderAltItem = new SupplierOrderDetailAltItem();
					this.SupplierOrderDetailAlternativeListTemp.Add(supOrderAltItem);

				}

				#region Updates the list
				supOrderAltItem.Modified = true;

				supOrderAltItem.SODAltDesc = (item["SODAltDesc"].FindControl("txtSODAltDesc") as TextBox).Text.Trim();
				supOrderAltItem.SODAltQuantity = Convert.ToDouble((item["SODAltQuantity"].FindControl("txtSODAltQuantity") as TextBox).Text);
				supOrderAltItem.SODAltUM = (item["SODAltUM"].FindControl("cmbSODAltUM") as RadComboBox).SelectedValue;
				supOrderAltItem.SODAltUnitCost = Convert.ToDouble((item["SODAltUnitCost"].FindControl("txtSODAltUnitCost") as TextBox).Text);
				supOrderAltItem.SODAltCurrencyCode = (item["SODAltCurrencyCode"].FindControl("cmbSODAltCurrencyCode") as RadComboBox).SelectedValue;
				supOrderAltItem.SODAltValidityPeriod = (item["SODAltValidityPeriod"].FindControl("calSODAltValidityPeriod") as RadDatePicker).SelectedDate;
				supOrderAltItem.SODAltDeliveryTime = Convert.ToInt32((item["SODAltDeliveryTime"].FindControl("txtSODAltDeliveryTime") as TextBox).Text);
				supOrderAltItem.SODAltDelTerm = (item["SODAltDelTerm"].FindControl("cmbSODAltDelTerm") as RadComboBox).SelectedValue;
				supOrderAltItem.SODAltDeliveryPt = (item["SODAltDeliveryPt"].FindControl("txtSODAltDeliveryPt") as TextBox).Text.Trim();
				supOrderAltItem.SODAltSupplierCode = (item["SODAltSupplierCode"].FindControl("txtSODAltSupplierCode") as TextBox).Text.Trim();
				supOrderAltItem.SODAltManufacturerCode = (item["SODAltManufacturerCode"].FindControl("txtSODAltManufacturerCode") as TextBox).Text.Trim();
				#endregion

			}
		}
		#endregion

		#region General Events
		protected void ajaxMngr_AjaxRequest(object sender, AjaxRequestEventArgs e)
		{
			string returnValue = e.Argument;
			if (returnValue.IndexOf(B2BConstants.AJAX_RETURN_CONFIRMED) > -1)
			{

				// Parse the argument
				string[] argList = returnValue.Split(B2BConstants.LIST_SEPARATOR);
				bool confirmed = Convert.ToBoolean(argList[2]);

				#region Checks which button
				// Clear
				if (argList[1] == this.btnReset.ID && confirmed)
				{

					#region Removes all items
					for (int i = this.SupplierOrderDetailAlternativeListTemp.Count - 1; i > -1; i--)
					{

						SupplierOrderDetailAltItem altItem = this.SupplierOrderDetailAlternativeListTemp[i];
						if (altItem.SODAltID > 0)
							altItem.MarkForDeletion = true;

						else
						{

							#region Deletes the file permanently
							if (this.ImpersonateValidUser(ConfigurationManager.AppSettings["impersonateUser"], ConfigurationManager.AppSettings["impersonatePassword"],
								ConfigurationManager.AppSettings["impersonateDomain"]))
							{

								foreach (SupplierOrderAttachItem attachItem in altItem.SupplierOrderAlternativeAttachList)
								{

									if (attachItem.OrderAttachType == B2BConstants.MediaObjectType.File)
									{

										if (File.Exists(Server.MapPath(Path.Combine(
											ConfigurationManager.AppSettings["SUPPLIER_SOURCE_PATH"], attachItem.OrderAttachFilename))))
											File.Delete(Server.MapPath(Path.Combine(
												ConfigurationManager.AppSettings["SUPPLIER_SOURCE_PATH"], attachItem.OrderAttachFilename)));

									}
								}

								// Resets the impersonation
								this.UndoImpersonation();

							}
							#endregion

							this.SupplierOrderDetailAlternativeListTemp.Remove(altItem);

						}
					}
					#endregion

					// Sets the modification flag
					this.IsRecordModified = true;

					// Rebind the list
					this.gvOrderAlt.Rebind();

				}

				// Back
				else if (argList[1] == this.btnBack.ID)
				{

					// Checks if confirmed
					if (confirmed)
						this.btnSubmit_Click(null, null);

					else
						ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Back", "OnCloseWindow();", true);

				}
				#endregion

			}

			// Supplier alternative offer attachments
			else if (returnValue.IndexOf(B2BConstants.AJAX_RETURN_SUPPLIER_ORDER_ATTACHMENT_LOOKUP) > -1)
				this.gvOrderAlt.Rebind();
		}
		#endregion

		#region Action Buttons
		protected void btnSubmit_Click(object sender, EventArgs e)
		{
			// Validates first
			this.Validate();
			if (this.IsValid)
			{

				// Updates the entry
				this.UpdateAlternativeOfferEntry();

				// Updates the supplier order alternative list
				this.SupplierOrderDetailCurrent.SupplierOrderDetAltList.Clear();
				foreach (SupplierOrderDetailAltItem item in this.SupplierOrderDetailAlternativeListTemp)
					this.SupplierOrderDetailCurrent.SupplierOrderDetAltList.Add(new SupplierOrderDetailAltItem(item));

				// Release the temporary
				this.SupplierOrderDetailAlternativeListTemp.Clear();

				ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Back",
					String.Format("OnCloseSupplierOrderAlternativeLookup('{0}');", this.hidAjaxID.Value), true);

			}
		}

		protected void btnReset_Click(object sender, EventArgs e)
		{
			if (this.SupplierOrderDetailAlternativeListTemp.FindAll(tempItem => !tempItem.MarkForDeletion).Count > 0)
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
		#endregion

		#region Data Binding
		protected void gvOrderAlt_PreRender(object sender, EventArgs e)
		{
			// Hides other columns if already closed
			if (this.IsForViewing)
			{

				this.gvOrderAlt.Columns[(int)AlterOfferColumn.DeleteCol].Visible = false;

			}

			foreach (GridDataItem item in this.gvOrderAlt.MasterTableView.GetItems(GridItemType.AlternatingItem, GridItemType.Item))
				(item["SODAltValidityPeriod"].FindControl("calSODAltValidityPeriod") as RadDatePicker).SharedCalendar = this.calShared;
		}

		protected void gvOrderAlt_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			this.gvOrderAlt.DataSource = this.SupplierOrderDetailAlternativeListTemp.FindAll(tempItem => !tempItem.MarkForDeletion);
		}

		protected void gvOrderAlt_ItemDataBound(object sender, GridItemEventArgs e)
		{
			if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
			{

				#region Formats the display
				GridDataItem item = e.Item as GridDataItem;
				if (item != null)
				{

					// Sets the selected unit of measure
					RadComboBox cmbSODAltUM = item["SODAltUM"].FindControl("cmbSODAltUM") as RadComboBox;
					cmbSODAltUM.SelectedValue = (item["SODAltUM"].FindControl("litSODAltUM") as Literal).Text;

					// Sets the selected currency
					RadComboBox cmbSODAltCurrencyCode = item["SODAltCurrencyCode"].FindControl("cmbSODAltCurrencyCode") as RadComboBox;
					cmbSODAltCurrencyCode.SelectedValue = (item["SODAltCurrencyCode"].FindControl("litSODAltCurrencyCode") as Literal).Text;

					// Sets the selected delivery terms
					RadComboBox cmbSODAltDelTerm = item["SODAltDelTerm"].FindControl("cmbSODAltDelTerm") as RadComboBox;
					cmbSODAltDelTerm.SelectedValue = (item["SODAltDelTerm"].FindControl("litSODAltDelTerm") as Literal).Text;

					RadDatePicker calSODAltValidityPeriod = item["SODAltValidityPeriod"].FindControl("calSODAltValidityPeriod") as RadDatePicker;

					#region Sets the supplier order alternative attachments
					int totalSupplierOrderAlternativeAttachments = Convert.ToInt32(item["TotalSupplierOrderAlternativeAttachments"].Text);
					ImageButton imgSODAltAttachment = item["SODAltAttachment"].Controls[0] as ImageButton;
					if (this.IsForViewing)
					{

						if (totalSupplierOrderAlternativeAttachments > 0)
							imgSODAltAttachment.ToolTip = String.Format("View attachments ({0})", totalSupplierOrderAlternativeAttachments);

						else
						{

							imgSODAltAttachment.ImageUrl = "~/includes/images/attach_small_f3.gif";
							imgSODAltAttachment.ToolTip = "No attachments";
							imgSODAltAttachment.Enabled = false;

						}
					}

					else
						imgSODAltAttachment.ToolTip = String.Format("View attachments ({0})", totalSupplierOrderAlternativeAttachments);
					#endregion

					#region Upates the data entry
					if (this.IsForViewing)
					{

						(item["SODAltDesc"].FindControl("txtSODAltDesc") as TextBox).ReadOnly = true;
						(item["SODAltDesc"].FindControl("txtSODAltDesc") as TextBox).CssClass = "TextLeft";

						(item["SODAltQuantity"].FindControl("txtSODAltQuantity") as TextBox).ReadOnly = true;
						(item["SODAltQuantity"].FindControl("txtSODAltQuantity") as TextBox).CssClass = "TextRight";

						cmbSODAltUM.Enabled = false;

						(item["SODAltUnitCost"].FindControl("txtSODAltUnitCost") as TextBox).ReadOnly = true;
						(item["SODAltUnitCost"].FindControl("txtSODAltUnitCost") as TextBox).CssClass = "TextRight";

						cmbSODAltCurrencyCode.Enabled = false;

						calSODAltValidityPeriod.Enabled = false;

						(item["SODAltDeliveryTime"].FindControl("txtSODAltDeliveryTime") as TextBox).ReadOnly = true;
						(item["SODAltDeliveryTime"].FindControl("txtSODAltDeliveryTime") as TextBox).CssClass = "TextRight";

						cmbSODAltDelTerm.Enabled = false;

						(item["SODAltDeliveryPt"].FindControl("txtSODAltDeliveryPt") as TextBox).ReadOnly = true;
						(item["SODAltSupplierCode"].FindControl("txtSODAltSupplierCode") as TextBox).ReadOnly = true;
						(item["SODAltManufacturerCode"].FindControl("txtSODAltManufacturerCode") as TextBox).ReadOnly = true;

					}

					else
						calSODAltValidityPeriod.MinDate = DateTime.Now;
					#endregion
				}
				#endregion
			}
		}

		protected void gvOrderAlt_ItemCommand(object sender, GridCommandEventArgs e)
		{
			#region Creates a new alternative offer
			if (e.CommandName == RadGrid.InitInsertCommandName)
			{

				// Validates first
				this.Validate();
				if (this.IsValid)
				{

					// Sets the flag
					this.IsRecordModified = true;

					// Updates the current list
					this.UpdateAlternativeOfferEntry();

					// Creates a new item
					double sodAltLineNo = (double)(this.SupplierOrderDetailAlternativeListTemp.Count() == 0 ? 1 :
						this.SupplierOrderDetailAlternativeListTemp.Max(tempItem => tempItem.SODAltLineNo) + 1);
					this.SupplierOrderDetailAlternativeListTemp.Add(new SupplierOrderDetailAltItem(sodAltLineNo, this.litOrderDetUM.Text,
						this.SODAltCurrencyCode, this.SODAltDelTerm, this.SODAltValidityPeriod, this.SODAltDeliveryTime, this.SODAltDeliveryPt));

					this.gvOrderAlt.Rebind();

				}

				// Cancels the operation
				e.Canceled = true;

			}
			#endregion

			#region Shows the supplier's attachment
			else if (e.CommandName.Equals("SODAltAttachment"))
			{

				// Validates first
				this.Validate();
				if (this.IsValid)
				{

					// Updates all supplier alternative offer entry
					this.UpdateAlternativeOfferEntry();

					GridDataItem item = e.Item as GridDataItem;
					if (item != null)
					{

						double sodAltLineNo = Convert.ToDouble(item["SODAltLineNo"].Text);
						SupplierOrderDetailAltItem supOrderAltItem = this.SupplierOrderDetailAlternativeListTemp.Find(tempItem => tempItem.SODAltLineNo == sodAltLineNo);
						if (supOrderAltItem != null)
						{

							#region Retrieves the attachments
							if (item["SODAltID"].Text.Equals("0"))
								supOrderAltItem.SODAltAttachDownloaded = true;

							else if (!supOrderAltItem.SODAltAttachDownloaded)
							{

								this.objSupplierOrderAttach.SelectParameters["orderAttachNo"].DefaultValue = this.SupplierOrderDetailCurrent.OrderDetNo.ToString();
								this.objSupplierOrderAttach.SelectParameters["orderAttachSODAltID"].DefaultValue = item["SODAltID"].Text;
								this.objSupplierOrderAttach.Select();

								// Copy all the attachments
								supOrderAltItem.CopyAllAttachments(this._tempAttachList);
								supOrderAltItem.SODAltAttachDownloaded = true;

							}

							this.SupplierOrderAltAttachmentList = supOrderAltItem.SupplierOrderAlternativeAttachList;
							#endregion

							#region Opens the supplier order alternative attachment lookup
							StringBuilder script = new StringBuilder();
							script.Append("ShowSupplierOrderAttachmentLookup('");
							script.Append(this.ajaxMngr.ClientID);
							script.Append("', 2, 'true', '");
							script.Append(this.IsForViewing.ToString());
							script.Append("');");

							ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SupplierAttachment", script.ToString(), true);
							#endregion

						}
					}
				}
			}
			#endregion

			#region Deletes alternative offer
			else if (e.CommandName == RadGrid.DeleteCommandName)
			{

				GridDataItem item = e.Item as GridDataItem;
				if (item != null)
				{

					// Retrieves the other charge item
					SupplierOrderDetailAltItem altItem = this.SupplierOrderDetailAlternativeListTemp.Find(tempItem => !tempItem.MarkForDeletion &&
						tempItem.SODAltLineNo == Convert.ToDouble(item["SODAltLineNo"].Text));
					if (altItem != null)
					{

						if (altItem.SODAltID > 0)
							altItem.MarkForDeletion = true;

						else
						{

							#region Deletes the file permanently
							if (this.ImpersonateValidUser(ConfigurationManager.AppSettings["impersonateUser"], ConfigurationManager.AppSettings["impersonatePassword"],
								ConfigurationManager.AppSettings["impersonateDomain"]))
							{

								foreach (SupplierOrderAttachItem attachItem in altItem.SupplierOrderAlternativeAttachList)
								{

									if (attachItem.OrderAttachType == B2BConstants.MediaObjectType.File)
									{

										if (File.Exists(Server.MapPath(Path.Combine(
											ConfigurationManager.AppSettings["SUPPLIER_SOURCE_PATH"], attachItem.OrderAttachFilename))))
											File.Delete(Server.MapPath(Path.Combine(
												ConfigurationManager.AppSettings["SUPPLIER_SOURCE_PATH"], attachItem.OrderAttachFilename)));

									}
								}

								// Resets the impersonation
								this.UndoImpersonation();

							}
							#endregion

							this.SupplierOrderDetailAlternativeListTemp.Remove(altItem);

						}
					}
				}
			}
			#endregion
		}
		#endregion

		#region Database Access
		protected void objSupplierOrderAttach_Selected(object sender, ObjectDataSourceStatusEventArgs e)
		{
			this._tempAttachList = e.ReturnValue as List<SupplierOrderAttachItem>;
		}
		#endregion
	}
}