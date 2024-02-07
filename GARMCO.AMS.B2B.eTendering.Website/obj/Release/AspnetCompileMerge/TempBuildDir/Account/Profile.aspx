<%@ Page Title="" Language="C#" MasterPageFile="~/CommonObject/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="GARMCO.AMS.B2B.eTendering.Website.Account.Profile" %>
<%@ MasterType VirtualPath="~/CommonObject/Site.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content" ContentPlaceHolderID="mainContent" runat="server">
	<script type="text/javascript" src="../Scripts/script.js"></script>
	<script type="text/javascript" src="../Scripts/contact.js"></script>
	<script type="text/javascript" src="../Scripts/supplierProdServ.js"></script>
	<script type="text/javascript" src="../Scripts/warning.js"></script>
	<script type="text/javascript">
		function updateNotification(chkSupplierIncProdServ, chkSupplierNotProdServ) {

			var incProdServ = document.getElementById(chkSupplierIncProdServ);
			if (incProdServ != null) {

				var notProdServ = document.getElementById(chkSupplierNotProdServ);

				notProdServ.checked = incProdServ.checked && notProdServ.checked;
				notProdServ.disabled = !incProdServ.checked;

			}
		}
	</script>
	<asp:Panel ID="panEntry" runat="server" CssClass="GroupLayoutRegister" GroupingText="Supplier Profile" Width="100%" style="table-layout:fixed;">
		<asp:ValidationSummary ID="valSummary" runat="server" CssClass="ValidationError" HeaderText="Please enter values on the following fields:" />
		<table border="0" style="padding: 2px; width: 100%;">
			<tr>
				<td class="LabelBold" style="width: 120px;">
					Company Name
				</td>
				<td style="width: 220px;">
					<asp:TextBox ID="txtSupplierName" runat="server" MaxLength="40" SkinID="TextLeftMandatory" />
					<asp:RequiredFieldValidator ID="reqSupplierName" runat="server" ControlToValidate="txtSupplierName" CssClass="LabelValidationError" ErrorMessage="Company Name cannot be blank" SetFocusOnError="true" Text="*" ToolTip="Company Name cannot be blank" />
				</td>
				<td class="LabelBold" style="width: 145px;">
					Company URL
				</td>
				<td>
					<asp:TextBox ID="txtSupplierURL" runat="server" MaxLength="100" SkinID="TextLeft" />
				</td>
			</tr>
			<tr>
				<td class="LabelBold" rowspan="4" style="vertical-align: top;">
					Billing Address
				</td>
				<td rowspan="4">
					<asp:TextBox ID="txtSupplierAddr" runat="server" MaxLength="160" Rows="7" SkinID="TextLeftMandatory" TextMode="MultiLine" />
					<asp:RequiredFieldValidator ID="reqSupplierAdd" runat="server" ControlToValidate="txtSupplierAddr" CssClass="LabelValidationError" ErrorMessage="Billing Address cannot be blank" SetFocusOnError="true" Text="*" ToolTip="Billing Address cannot be blank" />
				</td>
				<td class="LabelBold">
					City
				</td>
				<td>
					<asp:TextBox ID="txtSupplierCity" runat="server" MaxLength="25" SkinID="TextLeft" />
				</td>
			</tr>
			<tr>
				<td class="LabelBold">
					State
				</td>
				<td>
					<asp:TextBox ID="txtSupplierState" runat="server" MaxLength="3" SkinID="TextUpper" />
				</td>
			</tr>
			<tr>
				<td class="LabelBold">
					Country
				</td>
				<td>
					<telerik:RadComboBox ID="cmbSupplierCountry" runat="server" DataSourceID="objSupplierCountry" DataTextField="DRDL01" DataValueField="DRKY" DropDownWidth="250px" Height="300px" MarkFirstMatch="true" Skin="Windows7" Width="205px" AppendDataBoundItems="true">
						<Items>
							<telerik:RadComboBoxItem Text="Please select a country..." />
						</Items>
					</telerik:RadComboBox>
					<asp:CompareValidator ID="comSupplierCountry" runat="server" ControlToValidate="cmbSupplierCountry" CssClass="LabelValidationError" ErrorMessage="Select a country" Operator="NotEqual" SetFocusOnError="True" Text="*" ToolTip="Select a country" ValueToCompare="Please select a country..." />
				</td>
			</tr>
			<tr>
				<td class="LabelBold">
					Postal Code
				</td>
				<td>
					<asp:TextBox ID="txtSupplierPostal" runat="server" MaxLength="12" SkinID="TextLeft" />
				</td>
			</tr>
		</table>
		<br />
		<div class="LabelNote">
			Note: Please scroll to the right using the horizontal scrollbar if you cannot see the whole records.
		</div>
		<asp:Panel ID="panResult" runat="server" CssClass="SearchResult">
			<telerik:RadTabStrip ID="tabControl" runat="server" CausesValidation="false" MultiPageID="multiPg" SelectedIndex="0" Skin="Windows7" Width="100%">
				<Tabs>
					<telerik:RadTab runat="server" PageViewID="pgContact" Selected="True" Text="Contact Persons" />
					<telerik:RadTab runat="server" PageViewID="pgProdService" Text="Products and Services" />
					<telerik:RadTab runat="server" PageViewID="pgPreference" Text="Preferences" />
				</Tabs>
			</telerik:RadTabStrip>
			<telerik:RadMultiPage ID="multiPg" runat="server" SelectedIndex="0" Width="100%">
				<telerik:RadPageView ID="pgContact" runat="server">
					<telerik:RadGrid ID="gvContact" runat="server" CellSpacing="0" DataSourceID="objSupplierContact" GridLines="None" onitemcommand="gvContact_ItemCommand" onitemdatabound="gvContact_ItemDataBound" onprerender="gvContact_PreRender" Skin="Windows7" Width="100%">
						<ExportSettings>
							<Pdf>
								<PageHeader>
									<LeftCell Text="" />
									<MiddleCell Text="" />
									<RightCell Text="" />
								</PageHeader>
								<PageFooter>
									<LeftCell Text="" />
									<MiddleCell Text="" />
									<RightCell Text="" />
								</PageFooter>
							</Pdf>
						</ExportSettings>
						<MasterTableView AutoGenerateColumns="False" CommandItemDisplay="Top" DataSourceID="objSupplierContact" EditMode="InPlace" NoMasterRecordsText="No Contact Person Found" TableLayout="Fixed" DataKeyNames="SupplierNo,ContactID">
							<CommandItemSettings AddNewRecordText="Add new contact person" ExportToPdfText="Export to PDF" ShowRefreshButton="false" />
							<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
							</RowIndicatorColumn>
							<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
							</ExpandCollapseColumn>
							<Columns>
								<telerik:GridButtonColumn ConfirmText="Are you sure you want to remove this contact person?<br />Please click Ok if yes, otherwise Cancel." ConfirmDialogType="RadWindow" ConfirmTitle="Warning" ButtonType="ImageButton" CommandName="Delete" Text="Remove" UniqueName="DeleteImageButton">
									<HeaderStyle HorizontalAlign="Center" Width="40px" />
									<ItemStyle HorizontalAlign="Center" />
								</telerik:GridButtonColumn>
								<telerik:GridButtonColumn ButtonType="LinkButton" CommandName="UpdateContact" Text="Update" UniqueName="UpdateContact">
									<HeaderStyle HorizontalAlign="Center" Width="100px" />
									<ItemStyle HorizontalAlign="Center" />
								</telerik:GridButtonColumn>
								<telerik:GridBoundColumn DataField="ContactID" DataType="System.Int32" Display="false" FilterControlAltText="Filter ContactID column" HeaderText="ContactID" ReadOnly="True" SortExpression="ContactID" UniqueName="ContactID" />
								<telerik:GridTemplateColumn DataField="ContactName" FilterControlAltText="Filter ContactName column" HeaderText="Name" SortExpression="ContactName" UniqueName="ContactName">
									<HeaderStyle Width="180px" />
									<ItemTemplate>
										<div class="columnEllipsis">
											<asp:Literal ID="litContactName" runat="server" Text='<%# Eval("ContactName") %>' />
										</div>
									</ItemTemplate>
								</telerik:GridTemplateColumn>
								<telerik:GridTemplateColumn DataField="ContactEmail" FilterControlAltText="Filter ContactEmail column" HeaderText="e-Mail Address" SortExpression="ContactEmail" UniqueName="ContactEmail">
									<HeaderStyle Width="180px" />
									<ItemTemplate>
										<div class="columnEllipsis">
											<asp:Literal ID="litContactEmail" runat="server" Text='<%# Eval("ContactEmail") %>' />
										</div>
									</ItemTemplate>
								</telerik:GridTemplateColumn>
								<telerik:GridBoundColumn DataField="ContactPassword" Display="false" FilterControlAltText="Filter ContactPassword column" HeaderText="ContactPassword" SortExpression="ContactPassword" UniqueName="ContactPassword" />
								<telerik:GridTemplateColumn DataField="ContactTelNo" FilterControlAltText="Filter ContactTelNo column" HeaderText="Telephone No." SortExpression="ContactTelNo" UniqueName="ContactTelNo">
									<HeaderStyle Width="140px" />
									<ItemTemplate>
										<div class="columnEllipsis">
											<asp:Literal ID="litContactTelNo" runat="server" Text='<%# Eval("ContactTelNo") %>' />
										</div>
									</ItemTemplate>
								</telerik:GridTemplateColumn>
								<telerik:GridTemplateColumn DataField="ContactMobNo" FilterControlAltText="Filter ContactMobNo column" HeaderText="Mobile No." SortExpression="ContactMobNo" UniqueName="ContactMobNo">
									<HeaderStyle Width="140px" />
									<ItemTemplate>
										<div class="columnEllipsis">
											<asp:Literal ID="litContactMobNo" runat="server" Text='<%# Eval("ContactMobNo") %>' />
										</div>
									</ItemTemplate>
								</telerik:GridTemplateColumn>
								<telerik:GridTemplateColumn DataField="ContactFaxNo" FilterControlAltText="Filter ContactFaxNo column" HeaderText="Fax No." SortExpression="ContactFaxNo" UniqueName="ContactFaxNo">
									<HeaderStyle Width="140px" />
									<ItemTemplate>
										<div class="columnEllipsis">
											<asp:Literal ID="litContactFaxNo" runat="server" Text='<%# Eval("ContactFaxNo") %>' />
										</div>
									</ItemTemplate>
								</telerik:GridTemplateColumn>
								<telerik:GridTemplateColumn DataField="ContactActive" DataType="System.Boolean" FilterControlAltText="Filter ContactActive column" HeaderText="Activated?" SortExpression="ContactActive" UniqueName="ContactActive">
									<HeaderStyle HorizontalAlign="Center" Width="80px" />
									<ItemStyle HorizontalAlign="Center" />
									<ItemTemplate>
										<%# Convert.ToBoolean(Eval("ContactActive")) ? "Yes" : "No" %>
									</ItemTemplate>
								</telerik:GridTemplateColumn>
								<telerik:GridBoundColumn DataField="ContactActiveKey" Display="false" FilterControlAltText="Filter ContactActiveKey column" HeaderText="ContactActiveKey" SortExpression="ContactActiveKey" UniqueName="ContactActiveKey" />
								<telerik:GridTemplateColumn DataField="ContactPrimary" DataType="System.Boolean" FilterControlAltText="Filter ContactPrimary column" HeaderText="Primary?" SortExpression="ContactPrimary" UniqueName="ContactPrimary">
									<HeaderStyle HorizontalAlign="Center" Width="80px" />
									<ItemStyle HorizontalAlign="Center" />
									<ItemTemplate>
										<asp:Literal ID="litContactPrimary" runat="server" Text='<%# Convert.ToBoolean(Eval("ContactPrimary")) ? "Yes" : "No" %>' />
									</ItemTemplate>
								</telerik:GridTemplateColumn>
								<telerik:GridBoundColumn DataField="ContactReviewed" DataType="System.Boolean" Display="false" FilterControlAltText="Filter ContactReviewed column" HeaderText="ContactReviewed" SortExpression="ContactReviewed" UniqueName="ContactReviewed" />
								<telerik:GridBoundColumn DataField="ContactRejected" DataType="System.Boolean" FilterControlAltText="Filter ContactRejected column" HeaderText="Rejected?" SortExpression="ContactRejected" UniqueName="ContactRejected">
									<HeaderStyle HorizontalAlign="Center" Width="80px" />
									<ItemStyle HorizontalAlign="Center" />
								</telerik:GridBoundColumn>
								<telerik:GridTemplateColumn DataField="ContactRejectReason" Display="false" FilterControlAltText="Filter ContactRejectReason column" HeaderText="Reason of Rejection" SortExpression="ContactRejectReason" UniqueName="ContactRejectReason" />
								<telerik:GridTemplateColumn DataField="ContactModifiedName" FilterControlAltText="Filter ContactModifiedName column" HeaderText="Last Modified By" SortExpression="ContactModifiedName" UniqueName="ContactModifiedName">
									<HeaderStyle Width="180px" />
									<ItemTemplate>
										<div class="columnEllipsis">
											<asp:Literal ID="litContactModifiedName" runat="server" Text='<%# Eval("ContactModifiedName") %>' />
										</div>
									</ItemTemplate>
								</telerik:GridTemplateColumn>
								<telerik:GridBoundColumn DataField="ContactModifiedDate" DataFormatString="{0:dd MMM yyyy HH:mm}" DataType="System.DateTime" FilterControlAltText="Filter ContactModifiedDate column" HeaderText="Last Modified Date" SortExpression="ContactModifiedDate" UniqueName="ContactModifiedDate">
									<HeaderStyle HorizontalAlign="Right" Width="120px" />
									<ItemStyle HorizontalAlign="Right" />
								</telerik:GridBoundColumn>
							</Columns>
							<EditFormSettings>
								<EditColumn FilterControlAltText="Filter EditCommandColumn column">
								</EditColumn>
							</EditFormSettings>
							<BatchEditingSettings EditType="Cell" />
						</MasterTableView>
						<ClientSettings AllowColumnsReorder="False" EnableRowHoverStyle="true" Selecting-AllowRowSelect="True" Selecting-UseClientSelectColumnOnly="True">
							<Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True" />
							<Scrolling AllowScroll="True" UseStaticHeaders="true" FrozenColumnsCount="4" SaveScrollPosition="true" ScrollHeight="" />
							<Resizing AllowColumnResize="true" />
						</ClientSettings>
					</telerik:RadGrid>
				</telerik:RadPageView>
				<telerik:RadPageView ID="pgProdService" runat="server">
					<div style="padding: 4px;">
						<b>Note:</b> Please add at least one Product/Service by clicking the box. Click on the description of each item to view a detailed list.
					</div>
					<asp:DataList ID="lstSupplierProdServ" runat="server" RepeatColumns="4" onitemdatabound="lstSupplierProdServ_ItemDataBound" DataSourceID="objProdServ" Width="100%">
						<ItemTemplate>
							<table border="0" style="padding: 2px; width: 100%">
								<tr>
									<td style="width: 30px; text-align: center;">
										<asp:CheckBox ID="chkProdServ" runat="server" AutoPostBack="true" OnCheckedChanged="chkProdServ_CheckedChanged" />
									</td>
									<td>
										<asp:LinkButton ID="lnkProdServ" runat="server" CausesValidation="false" Text='<%# Eval("ProdServCodeDesc") %>' OnClick="lnkProdServ_Click" ToolTip="Click to view"/>
									</td>
								</tr>
							</table>
						</ItemTemplate>
						<ItemStyle VerticalAlign="Top" />
					</asp:DataList>
				</telerik:RadPageView>
				<telerik:RadPageView ID="pgPreference" runat="server">
					<table border="0" style="padding: 2px; width: 100%;">
						<tr>
							<td class="LabelBold" style="width: 120px;">
								Currency
							</td>
							<td style="width: 220px;">
								<telerik:RadComboBox ID="cmbSupplierCurrency" runat="server" DataSourceID="objCurrency" DataTextField="CVDL01" DataValueField="CVCRCD" DropDownWidth="250px" Height="300px" MarkFirstMatch="True" Skin="Windows7" Width="205px" AppendDataBoundItems="true">
									<Items>
										<telerik:RadComboBoxItem Text="Please select a currency..." />
									</Items>
								</telerik:RadComboBox>
								<asp:CompareValidator ID="comSupplierCurrency" runat="server" ControlToValidate="cmbSupplierCurrency" CssClass="LabelValidationError" ErrorMessage="Select a currency" Operator="NotEqual" SetFocusOnError="True" Text="*" ToolTip="Select a currency" ValueToCompare="Please select a currency..." />
							</td>
							<td class="LabelBold" style="width: 145px;">
								Delivery Condition
							</td>
							<td>
								<telerik:RadComboBox ID="cmbSupplierDelTerm" runat="server" DataSourceID="objDelTerm" DataTextField="UDCDesc1" DataValueField="UDCID" DropDownWidth="250px" Height="300px" MarkFirstMatch="True" Skin="Windows7" Width="205px" AppendDataBoundItems="true">
									<Items>
										<telerik:RadComboBoxItem Text="Please select a delivery condition..." Value="0" />
									</Items>
								</telerik:RadComboBox>
								<asp:CompareValidator ID="comSupplierDelTerm" runat="server" ControlToValidate="cmbSupplierDelTerm" CssClass="LabelValidationError" ErrorMessage="Select a delivery condition" Operator="NotEqual" SetFocusOnError="True" Text="*" ToolTip="Select a delivery condition" ValueToCompare="Please select a delivery condition..." />
							</td>
						</tr>
						<tr>
							<td>
							</td>
							<td colspan="3">
								<asp:CheckBox ID="chkSupplierNews" runat="server" Text="Do you want to subscribe to our news letter?" />
							</td>
						</tr>
						<tr>
							<td>
							</td>
							<td colspan="3">
								<asp:CheckBox ID="chkSupplierIncProdServ" runat="server" Text="Do you want to receive an e-mail notification if an RFQ has been published that belongs to one of your Products/Services?" />
							</td>
						</tr>
						<tr>
							<td>
							</td>
							<td colspan="3">
								<asp:CheckBox ID="chkSupplierNotProdServ" runat="server" Enabled="false" Text="Do you want to receive an e-mail notification for all RFQs that doesn't belong to your Products/Services?" />
							</td>
						</tr>
					</table>
				</telerik:RadPageView>
			</telerik:RadMultiPage>
		</asp:Panel>
		<br />
		<asp:Button ID="btnUpdate" runat="server" Text="Update" Width="110px" OnClick="btnUpdate_Click" />
	</asp:Panel>
	<input type="hidden" id="hidSupplierNo" runat="server" value="0" />
	<asp:Panel ID="panObjDS" runat="server">
		<asp:ObjectDataSource ID="objSupplier" runat="server" OldValuesParameterFormatString="" SelectMethod="GetSupplier" TypeName="GARMCO.AMS.B2B.eTendering.DAL.SupplierRepository" OnSelected="objSupplier_Selected" OnUpdated="objSupplier_Updated" UpdateMethod="UpdateSupplier">
			<SelectParameters>
				<asp:Parameter DefaultValue="1" Name="mode" Type="Byte" />
				<asp:SessionParameter DefaultValue="0" Name="supplierNo" SessionField="CONTACT_SUPPLIER_NO" Type="Int32" />
				<asp:Parameter DefaultValue="" Name="supplierName" Type="String" />
				<asp:Parameter DefaultValue="0" Name="supplierContactID" Type="Int32" />
				<asp:Parameter DefaultValue="" Name="supplierContactName" Type="String" />
				<asp:Parameter DefaultValue="" Name="supplierContactEmail" Type="String" />
				<asp:Parameter DefaultValue="" Name="supplierCountry" Type="String" />
				<asp:Parameter DefaultValue="" Name="supplierStatus" Type="Byte" />
				<asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
				<asp:Parameter DefaultValue="10" Name="maximumRows" Type="Int32" />
				<asp:Parameter DefaultValue="" Name="sort" Type="String" />
			</SelectParameters>
			<UpdateParameters>
				<asp:Parameter DefaultValue="1" Name="mode" Type="Byte" />
				<asp:SessionParameter Direction="InputOutput" Name="supplierNo" SessionField="CONTACT_SUPPLIER_NO" Type="Object" />
				<asp:Parameter DefaultValue="0" Name="supplierJDERefNo" Type="Double" />
				<asp:ControlParameter ControlID="txtSupplierName" DefaultValue="" Name="supplierName" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="txtSupplierURL" DefaultValue="" Name="supplierURL" PropertyName="Text" Type="String" />
				<asp:Parameter DefaultValue="false" Name="supplierOld" Type="Boolean" />
				<asp:ControlParameter ControlID="txtSupplierAddr" DefaultValue="" Name="supplierAddress" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="txtSupplierCity" DefaultValue="" Name="supplierCity" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="txtSupplierState" DefaultValue="" Name="supplierState" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="cmbSupplierCountry" DefaultValue="" Name="supplierCountry" PropertyName="SelectedValue" Type="String" />
				<asp:ControlParameter ControlID="txtSupplierPostal" DefaultValue="" Name="supplierPostalCode" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="cmbSupplierCurrency" DefaultValue="" Name="supplierCurrency" PropertyName="SelectedValue" Type="String" />
				<asp:ControlParameter ControlID="cmbSupplierDelTerm" DefaultValue="" Name="supplierDelTerm" PropertyName="SelectedValue" Type="String" />
				<asp:Parameter DefaultValue="0" Name="supplierShipVia" Type="Int32" />
				<asp:ControlParameter ControlID="chkSupplierNews" DefaultValue="false" Name="supplierNews" PropertyName="Checked" Type="Boolean" />
				<asp:ControlParameter ControlID="chkSupplierIncProdServ" DefaultValue="false" Name="supplierIncProdServ" PropertyName="Checked" Type="Boolean" />
				<asp:ControlParameter ControlID="chkSupplierNotProdServ" DefaultValue="false" Name="supplierNotProdServ" PropertyName="Checked" Type="Boolean" />
				<asp:SessionParameter Name="list" SessionField="ItemListSupplierProdService" Type="Object" />
				<asp:SessionParameter DefaultValue="0" Name="supplierCreatedModifiedBy" SessionField="CONTACT_ID" Type="Int32" />
				<asp:SessionParameter DefaultValue="" Name="supplierCreatedModifiedName" SessionField="CONTACT_NAME" Type="String" />
				<asp:Parameter DefaultValue="0" Direction="InputOutput" Name="retError" Type="Object" />
				<asp:Parameter DefaultValue="" Direction="InputOutput" Name="errorMsg" Type="String" />
			</UpdateParameters>
		</asp:ObjectDataSource>
		<asp:ObjectDataSource ID="objSupplierProdServ" runat="server" OldValuesParameterFormatString="" SelectMethod="GetSupplierProductServiceList" TypeName="GARMCO.AMS.B2B.eTendering.DAL.SupplierProductServiceRepository" OnSelected="objSupplierProdServ_Selected">
			<SelectParameters>
				<asp:Parameter DefaultValue="1" Name="mode" Type="Byte" />
				<asp:SessionParameter DefaultValue="0" Name="prodServSupplierNo" SessionField="CONTACT_SUPPLIER_NO" Type="Int32" />
			</SelectParameters>
		</asp:ObjectDataSource>
		<asp:ObjectDataSource ID="objSupplierContact" runat="server" OldValuesParameterFormatString="" SelectMethod="GetSupplierContact" TypeName="GARMCO.AMS.B2B.eTendering.DAL.SupplierContactRepository" UpdateMethod="InsertUpdateDeleteSupplierContact">
			<SelectParameters>
				<asp:Parameter DefaultValue="3" Name="mode" Type="Byte" />
				<asp:Parameter DefaultValue="0" Name="contactID" Type="Int32" />
				<asp:Parameter DefaultValue="" Name="contactEmail" Type="String" />
				<asp:Parameter DefaultValue="" Name="contactActiveKey" Type="String" />
				<asp:SessionParameter DefaultValue="0" Name="contactSupplierNo" SessionField="CONTACT_SUPPLIER_NO" Type="Int32" />
			</SelectParameters>
			<UpdateParameters>
				<asp:Parameter DefaultValue="2" Name="mode" Type="Byte" />
				<asp:Parameter DefaultValue="0" Direction="InputOutput" Name="contactID" Type="Int32" />
				<asp:SessionParameter DefaultValue="0" Name="contactSupplierNo" SessionField="CONTACT_SUPPLIER_NO" Type="Int32" />
				<asp:Parameter DefaultValue="" Name="contactName" Type="String" />
				<asp:Parameter DefaultValue="" Name="contactEmail" Type="String" />
				<asp:Parameter DefaultValue="" Name="contactPassword" Type="String" />
				<asp:Parameter DefaultValue="" Name="contactTelNo" Type="String" />
				<asp:Parameter DefaultValue="" Name="contactMobNo" Type="String" />
				<asp:Parameter DefaultValue="" Name="contactFaxNo" Type="String" />
				<asp:Parameter DefaultValue="" Name="contactActiveKey" Type="String" />
				<asp:Parameter DefaultValue="false" Name="contactPrimary" Type="Boolean" />
				<asp:SessionParameter DefaultValue="0" Name="contactCreatedModifiedBy" SessionField="CONTACT_ID" Type="Int32" />
				<asp:SessionParameter DefaultValue="" Name="contactCreatedModifiedName" SessionField="CONTACT_NAME" Type="String" />
				<asp:Parameter Direction="InputOutput" Name="retError" Type="Object" />
				<asp:Parameter Direction="InputOutput" Name="errorMsg" Type="String" />
			</UpdateParameters>
		</asp:ObjectDataSource>
		<asp:ObjectDataSource ID="objSupplierCountry" runat="server" OldValuesParameterFormatString="" SelectMethod="GetAll" TypeName="GARMCO.AMS.B2B.DAL.CountryRepository" />
		<asp:ObjectDataSource ID="objCurrency" runat="server" OldValuesParameterFormatString="" SelectMethod="GetAll" TypeName="GARMCO.AMS.B2B.DAL.CurrencyRepository" />
		<asp:ObjectDataSource ID="objDelTerm" runat="server" OldValuesParameterFormatString="" SelectMethod="GetAll" TypeName="GARMCO.AMS.B2B.DAL.DeliveryTermRepository" />
		<asp:ObjectDataSource ID="objProdServ" runat="server" OldValuesParameterFormatString="" SelectMethod="GetSupplierProductService" TypeName="GARMCO.AMS.B2B.eTendering.DAL.SupplierProductServiceRepository">
			<SelectParameters>
				<asp:Parameter DefaultValue="2" Name="mode" Type="Byte" />
				<asp:Parameter DefaultValue="0" Name="prodServSupplierNo" Type="Int32" />
				<asp:Parameter DefaultValue="" Name="prodServCode" Type="String" />
				<asp:Parameter DefaultValue="" Name="prodServCodeDesc" Type="String" />
				<asp:Parameter DefaultValue="1" Name="prodServParentLevel" Type="Byte" />
				<asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
				<asp:Parameter DefaultValue="10" Name="maximumRows" Type="Int32" />
				<asp:Parameter DefaultValue="" Name="sort" Type="String" />
			</SelectParameters>
		</asp:ObjectDataSource>
	</asp:Panel>
	<telerik:RadAjaxManagerProxy ID="ajaxProxy" runat="server">
		<AjaxSettings>
			<telerik:AjaxSetting AjaxControlID="gvContact">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="panEntry" LoadingPanelID="loadingPanel" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="lstSupplierProdServ">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="panEntry" LoadingPanelID="loadingPanel" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="btnUpdate">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="panEntry" LoadingPanelID="loadingPanel" />
				</UpdatedControls>
			</telerik:AjaxSetting>
		</AjaxSettings>
	</telerik:RadAjaxManagerProxy>
	<telerik:RadAjaxLoadingPanel ID="loadingPanel" runat="server" Skin="Windows7" />
</asp:Content>
