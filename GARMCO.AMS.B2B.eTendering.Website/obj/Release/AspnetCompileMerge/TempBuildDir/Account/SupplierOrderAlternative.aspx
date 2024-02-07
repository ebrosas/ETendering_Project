<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierOrderAlternative.aspx.cs" Inherits="GARMCO.AMS.B2B.eTendering.Website.Account.SupplierOrderAlternative" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<script type="text/javascript" src="../Scripts/script.js"></script>
	<script type="text/javascript" src="../Scripts/supplierOrderAlt.js"></script>
	<script type="text/javascript" src="../Scripts/rfqattachment.js"></script>
	<script type="text/javascript" src="../Scripts/warning.js"></script>
</head>
<body>
    <form id="form1" runat="server">
		<asp:ScriptManager ID="scriptMngr" runat="server" />
		<telerik:RadWindowManager ID="winMngr" runat="server" Skin="Windows7" VisibleStatusbar="false" />
		<telerik:RadFormDecorator ID="formDecor" runat="server" Skin="Windows7" />
		<asp:Panel ID="panEntry" runat="server" CssClass="GroupLayoutOrder" GroupingText="Alternative Offer" Width="100%">
			<asp:ValidationSummary ID="valSummary" runat="server" CssClass="ValidationError" HeaderText="Please enter values on the following fields:" />
			<table border="0" style="padding:2px; width: 100%;">
				<tr>
					<td class="LabelBold" style="width: 150px;">
						Item Code
					</td>
					<td style="width: 220px;">
						<asp:Literal ID="litOrderDetItemCode" runat="server" />
					</td>
					<td class="LabelBold" style="width: 120px;">
						Description
					</td>
					<td>
						<asp:Literal ID="litOrderDetDesc" runat="server" />
					</td>
				</tr>
				<tr>
					<td class="LabelBold">
						Required Quantity
					</td>
					<td>
						<asp:Literal ID="litOrderDetQuantity" runat="server" />
					</td>
					<td class="LabelBold">
						Unit of Measure
					</td>
					<td>
						<asp:Literal ID="litOrderDetUM" runat="server" />
					</td>
				</tr>
				<tr>
					<td class="LabelBold">
						Quoted Quantity
					</td>
					<td>
						<asp:Literal ID="litSODQuantity" runat="server" />
					</td>
					<td class="LabelBold">
						Quoted Unit Price
					</td>
					<td>
						<asp:Literal ID="litSODUnitCost" runat="server" />
					</td>
				</tr>
				<tr>
					<td class="LabelBold">
						Supplier's Item Code
					</td>
					<td>
						<asp:Literal ID="litSODSupplierCode" runat="server" />
					</td>
					<td class="LabelBold">
						Manufacturer's Code
					</td>
					<td>
						<asp:Literal ID="litSODManufacturerCode" runat="server" />
					</td>
				</tr>
				<tr>
					<td class="LabelBold">
						Remarks / Deviation
					</td>
					<td colspan="3">
						<asp:Literal ID="litSODRemarks" runat="server" />
					</td>
				</tr>
			</table>
			<br />
			<div class="LabelNote">
				Note: Please scroll to the right using the horizontal scrollbar if you cannot see the whole records.
			</div>
			<div class="SearchResult">
				<telerik:RadGrid ID="gvOrderAlt" runat="server" CellSpacing="0" GridLines="None" onitemcommand="gvOrderAlt_ItemCommand" onitemdatabound="gvOrderAlt_ItemDataBound" OnNeedDataSource="gvOrderAlt_NeedDataSource" onprerender="gvOrderAlt_PreRender" Skin="Windows7" Width="100%">
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
					<MasterTableView AutoGenerateColumns="False" CommandItemDisplay="Top" EditMode="InPlace" NoMasterRecordsText="No Alternative Offers Found" TableLayout="Fixed">
						<CommandItemSettings AddNewRecordText="Add new alternative offer" ExportToPdfText="Export to PDF" ShowRefreshButton="false" />
						<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
						</RowIndicatorColumn>
						<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
						</ExpandCollapseColumn>
						<Columns>
							<telerik:GridButtonColumn ConfirmText="Remove this alternative offer?<br />Please click Ok if yes, otherwise Cancel." ConfirmDialogType="RadWindow" ConfirmTitle="Warning" ButtonType="ImageButton" CommandName="Delete" Text="Remove" UniqueName="DeleteImageButton">
								<HeaderStyle HorizontalAlign="Center" Width="40px" />
								<ItemStyle HorizontalAlign="Center" />
							</telerik:GridButtonColumn>
							<telerik:GridBoundColumn DataField="SODAltID" DataType="System.Int32" Display="false" FilterControlAltText="Filter SODAltID column" HeaderText="SODAltID" SortExpression="SODAltID" UniqueName="SODAltID" />
							<telerik:GridBoundColumn DataField="SODAltLineNo" DataType="System.Int32" Display="false" FilterControlAltText="Filter SODAltLineNo column" HeaderText="SODAltLineNo" SortExpression="SODAltLineNo" UniqueName="SODAltLineNo" />
							<telerik:GridTemplateColumn DataField="SODAltDesc" FilterControlAltText="Filter SODAltDesc column" HeaderText="Description" SortExpression="SODAltDesc" UniqueName="SODAltDesc">
								<HeaderStyle Width="240px" />
								<ItemTemplate>
									<asp:TextBox ID="txtSODAltDesc" runat="server" MaxLength="60" SkinID="TextLeftMandatory" Text='<%# Eval("SODAltDesc") %>' />
									<asp:RequiredFieldValidator ID="reqSODAltDesc" runat="server" ControlToValidate="txtSODAltDesc" CssClass="LabelValidationError" ErrorMessage="Description cannot be blank" SetFocusOnError="true" Text="*" ToolTip="Description cannot be blank" />
								</ItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridTemplateColumn DataField="SODAltQuantity" DataType="System.Double" FilterControlAltText="Filter SODAltQuantity column" HeaderText="Quoted Quantity" SortExpression="SODAltQuantity" UniqueName="SODAltQuantity">
								<HeaderStyle HorizontalAlign="Right" Width="120px" />
								<ItemStyle HorizontalAlign="Right" />
								<ItemTemplate>
									<asp:TextBox ID="txtSODAltQuantity" runat="server" MaxLength="20" SkinID="TextLeftMandatory" Text='<%# Eval("SODAltQuantity").ToString().Equals("0") ? String.Empty : Eval("SODAltQuantity") %>' Width="80px" />
									<asp:RequiredFieldValidator ID="reqSODAltQuantity" runat="server" ControlToValidate="txtSODAltQuantity" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="Quoted quantity cannot be blank" SetFocusOnError="true" Text="*" ToolTip="Quoted quantity cannot be blank" />
									<asp:RegularExpressionValidator ID="regSODAltQuantity" runat="server" ControlToValidate="txtSODAltQuantity" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="Quoted quantity must be numeric and cannot be zero" SetFocusOnError="true" Text="*" ToolTip="Quoted quantity must be numeric and cannot be zero" ValidationExpression="^(([1-9]{1}(\d+)?)(\.\d+)?)|([0]\.(\d+)?([1-9]{1})(\d+)?)$" />
								</ItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridTemplateColumn DataField="SODAltUM" FilterControlAltText="Filter SODAltUM column" HeaderText="Unit of Measure" SortExpression="SODAltUM" UniqueName="SODAltUM">
								<HeaderStyle Width="230px" />
								<ItemTemplate>
									<telerik:RadComboBox ID="cmbSODAltUM" runat="server" DataSourceID="objUM" DataTextField="DRDL01" DataValueField="DRKY" DropDownWidth="250px" Height="300px" MarkFirstMatch="True" Skin="Windows7" Width="180px" AppendDataBoundItems="true">
										<Items>
											<telerik:RadComboBoxItem Text="Please select a UM..." />
										</Items>
									</telerik:RadComboBox>
									<asp:Literal ID="litSODAltUM" runat="server" Text='<%# Eval("SODAltUM") %>' Visible="false" />
									<asp:CompareValidator ID="comSODAltUM" runat="server" ControlToValidate="cmbSODAltUM" CssClass="LabelValidationError" ErrorMessage="Select a unit of measure" Operator="NotEqual" SetFocusOnError="True" Text="*" ToolTip="Select a unit of measure" ValueToCompare="Please select a UM..." />
								</ItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridTemplateColumn DataField="SODAltUnitCost" DataType="System.Double" FilterControlAltText="Filter SODAltUnitCost column" HeaderText="Quoted Unit Price" SortExpression="SODAltUnitCost" UniqueName="SODAltUnitCost">
								<HeaderStyle HorizontalAlign="Right" Width="120px" />
								<ItemStyle HorizontalAlign="Right" />
								<ItemTemplate>
									<asp:TextBox ID="txtSODAltUnitCost" runat="server" MaxLength="20" SkinID="TextLeftMandatory" Text='<%# Eval("SODAltUnitCost").ToString().Equals("0") ? String.Empty : Eval("SODAltUnitCost") %>' Width="80px" />
									<asp:RequiredFieldValidator ID="reqSODAltUnitCost" runat="server" ControlToValidate="txtSODAltUnitCost" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="Quoted unit price cannot be blank" SetFocusOnError="true" Text="*" ToolTip="Quoted unit price cannot be blank" />
									<asp:RegularExpressionValidator ID="regSODAltUnitCost" runat="server" ControlToValidate="txtSODAltUnitCost" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="Quoted Unit Price must be numeric and cannot be zero" SetFocusOnError="true" Text="*" ToolTip="Quoted Unit Price must be numeric and cannot be zero" ValidationExpression="^(([1-9]{1}(\d+)?)(\.\d+)?)|([0]\.(\d+)?([1-9]{1})(\d+)?)$" />
								</ItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridBoundColumn DataField="TotalSupplierOrderAlternativeAttachments" DataType="System.Int32" Display="false" FilterControlAltText="Filter TotalSupplierOrderAlternativeAttachments column" HeaderText="TotalSupplierOrderAlternativeAttachments" SortExpression="TotalSupplierOrderAlternativeAttachments" UniqueName="TotalSupplierOrderAlternativeAttachments" />
							<telerik:GridButtonColumn ButtonType="ImageButton" CommandName="SODAltAttachment" HeaderText="Supplier's Attachment" ImageUrl="~/includes/images/attach_small_f1.gif" UniqueName="SODAltAttachment">
								<HeaderStyle HorizontalAlign="Center" Width="100px" />
								<ItemStyle HorizontalAlign="Center" />
							</telerik:GridButtonColumn>
							<telerik:GridTemplateColumn DataField="SODAltCurrencyCode" FilterControlAltText="Filter SODAltCurrencyCode column" HeaderText="Quoted Currency" SortExpression="SODAltCurrencyCode" UniqueName="SODAltCurrencyCode">
								<HeaderStyle Width="230px" />
								<ItemTemplate>
									<telerik:RadComboBox ID="cmbSODAltCurrencyCode" runat="server" DataSourceID="objCurrency" DataTextField="CVDL01" DataValueField="CVCRCD" DropDownWidth="250px" Height="300px" MarkFirstMatch="True" Skin="Windows7" Width="190px" AppendDataBoundItems="true">
										<Items>
											<telerik:RadComboBoxItem Text="Please select a currency..." />
										</Items>
									</telerik:RadComboBox>
									<asp:Literal ID="litSODAltCurrencyCode" runat="server" Text='<%# Eval("SODAltCurrencyCode") %>' Visible="false" />
									<asp:CompareValidator ID="comSODAltCurrencyCode" runat="server" ControlToValidate="cmbSODAltCurrencyCode" CssClass="LabelValidationError" ErrorMessage="Select a currency" Operator="NotEqual" SetFocusOnError="True" Text="*" ToolTip="Select a currency" ValueToCompare="Please select a currency..." />
								</ItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridTemplateColumn DataField="SODAltValidityPeriod" DataType="System.DateTime" FilterControlAltText="Filter SODAltValidityPeriod column" HeaderText="Validity Period" SortExpression="SODAltValidityPeriod" UniqueName="SODAltValidityPeriod">
								<HeaderStyle HorizontalAlign="Right" Width="130px" />
								<ItemStyle HorizontalAlign="Right" />
								<ItemTemplate>
									<telerik:RadDatePicker ID="calSODAltValidityPeriod" runat="server" DateInput-DateFormat="dd/MM/yyyy" SelectedDate='<%# Eval("SODAltValidityPeriod") %>' Skin="Windows7" Width="100px" />
									<asp:RequiredFieldValidator ID="reqSODAltValidityPeriod" runat="server" ControlToValidate="calSODAltValidityPeriod" CssClass="LabelValidationError" ErrorMessage="Validity period cannot be blank" SetFocusOnError="true" Text="*" ToolTip="Validity period cannot be blank" />
								</ItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridTemplateColumn DataField="SODAltDeliveryTime" DataType="System.Int32" FilterControlAltText="Filter SODAltDeliveryTime column" HeaderText="Delivery Time (Days)" SortExpression="SODAltDeliveryTime" UniqueName="SODAltDeliveryTime">
								<HeaderStyle HorizontalAlign="Right" Width="120px" />
								<ItemStyle HorizontalAlign="Right" />
								<ItemTemplate>
									<asp:TextBox ID="txtSODAltDeliveryTime" runat="server" MaxLength="10" SkinID="TextLeftMandatory" Text='<%# Eval("SODAltDeliveryTime").ToString().Equals("0") ? String.Empty : Eval("SODAltDeliveryTime") %>' Width="80px" />
									<asp:RequiredFieldValidator ID="reqSODAltDeliveryTime" runat="server" ControlToValidate="txtSODAltDeliveryTime" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="Delivery time cannot be blank" SetFocusOnError="true" Text="*" ToolTip="Delivery time cannot be blank" />
									<asp:RegularExpressionValidator ID="regSODAltDeliveryTime" runat="server" ControlToValidate="txtSODAltDeliveryTime" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="Delivery time must be numeric and cannot be zero" SetFocusOnError="true" Text="*" ToolTip="Delivery time must be numeric and cannot be zero" ValidationExpression="^(([1-9]{1}(\d+)?)(\.\d+)?)|([0]\.(\d+)?([1-9]{1})(\d+)?)$" />
								</ItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridTemplateColumn DataField="SODAltDelTerm" FilterControlAltText="Filter SODAltUM column" HeaderText="Quoted Delivery Terms" SortExpression="SODAltDelTerm" UniqueName="SODAltDelTerm">
								<HeaderStyle Width="240px" />
								<ItemTemplate>
									<telerik:RadComboBox ID="cmbSODAltDelTerm" runat="server" DataSourceID="objDelTerm" DataTextField="UDCDesc1" DataValueField="UDCID" DropDownWidth="250px" Height="300px" MarkFirstMatch="True" Skin="Windows7" Width="205px" AppendDataBoundItems="true">
										<Items>
											<telerik:RadComboBoxItem Text="Please select a delivery terms..." Value="0" />
										</Items>
									</telerik:RadComboBox>
									<asp:Literal ID="litSODAltDelTerm" runat="server" Text='<%# Eval("SODAltDelTerm") %>' Visible="false" />
									<asp:CompareValidator ID="comSODAltDelTerm" runat="server" ControlToValidate="cmbSODAltDelTerm" CssClass="LabelValidationError" ErrorMessage="Select a delivery terms" Operator="NotEqual" SetFocusOnError="True" Text="*" ToolTip="Select a delivery terms" ValueToCompare="Please select a delivery terms..." />
								</ItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridTemplateColumn DataField="SODAltDeliveryPt" DataType="System.String" FilterControlAltText="Filter SODAltDeliveryPt column" HeaderText="Delivery Point" SortExpression="SODAltDeliveryPt" UniqueName="SODAltDeliveryPt">
								<HeaderStyle Width="220px" />
								<ItemTemplate>
									<asp:TextBox ID="txtSODAltDeliveryPt" runat="server" MaxLength="50" SkinID="TextLeft" Text='<%# Eval("SODAltDeliveryPt") %>' />
								</ItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridTemplateColumn DataField="SODAltSupplierCode" FilterControlAltText="Filter SODAltSupplierCode column" HeaderText="Supplier's Item Code" SortExpression="SODAltSupplierCode" UniqueName="SODAltSupplierCode">
								<HeaderStyle Width="150px" />
								<ItemTemplate>
									<asp:TextBox ID="txtSODAltSupplierCode" runat="server" MaxLength="30" SkinID="TextLeft" Text='<%# Eval("SODAltSupplierCode") %>' Width="120px" />
								</ItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridTemplateColumn DataField="SODAltManufacturerCode" FilterControlAltText="Filter SODAltManufacturerCode column" HeaderText="Manufacturer's Code" SortExpression="SODAltManufacturerCode" UniqueName="SODAltManufacturerCode">
								<HeaderStyle Width="150px" />
								<ItemTemplate>
									<asp:TextBox ID="txtSODAltManufacturerCode" runat="server" MaxLength="30" SkinID="TextLeft" Text='<%# Eval("SODAltManufacturerCode") %>' Width="120px" />
								</ItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridTemplateColumn DataField="SODAltModifiedName" FilterControlAltText="Filter SODAltModifiedName column" HeaderText="Last Modified By" SortExpression="SODAltModifiedName" UniqueName="SODAltModifiedName">
								<HeaderStyle Width="200px" />
								<ItemTemplate>
									<div class="columnEllipsis">
										<%# Eval("SODAltModifiedName") %>
									</div>
								</ItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridBoundColumn DataField="SODAltModifiedDate" DataFormatString="{0:dd MMM yyyy HH:mm}" DataType="System.DateTime" FilterControlAltText="Filter SODAltModifiedDate column" HeaderText="Last Modified Date" SortExpression="SODAltModifiedDate" UniqueName="SODAltModifiedDate">
								<HeaderStyle HorizontalAlign="Right" Width="120px" />
								<ItemStyle HorizontalAlign="Right" />
							</telerik:GridBoundColumn>
						</Columns>
						<EditFormSettings>
							<EditColumn FilterControlAltText="Filter EditCommandColumn column">
							</EditColumn>
						</EditFormSettings>
						<BatchEditingSettings EditType="Cell" />
						<PagerStyle PageSizeControlType="RadComboBox" />
					</MasterTableView>
					<ClientSettings AllowColumnsReorder="False" EnableRowHoverStyle="true" Selecting-AllowRowSelect="True" Selecting-UseClientSelectColumnOnly="True">
						<Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True" />
						<Scrolling AllowScroll="True" UseStaticHeaders="true" FrozenColumnsCount="1" SaveScrollPosition="true" ScrollHeight="" />
						<Resizing AllowColumnResize="true" />
					</ClientSettings>
					<HeaderStyle Height="40px" VerticalAlign="Bottom" />
				</telerik:RadGrid>
			</div>
			<asp:Panel ID="panAction" runat="server" Width="100%">
				<br />
				<asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submits the changes" Width="110px" onclick="btnSubmit_Click" />
				<asp:Button ID="btnReset" runat="server" CausesValidation="false" Text="Clear All" ToolTip="Removes all alternative offers" Width="110px" onclick="btnReset_Click" />
				<asp:Button ID="btnBack" runat="server" CausesValidation="false" Text="Back" ToolTip="Close the window" Width="110px" onclick="btnBack_Click" Height="26px" />
			</asp:Panel>
			<div style="display: none;">
				<telerik:RadCalendar ID="calShared" runat="server" CultureInfo="en-GB" EnableMultiSelect="False" SelectedDate="" Skin="Windows7" />
			</div>
		</asp:Panel>
		<input type="hidden" id="hidAjaxID" runat="server" />
		<input type="hidden" id="hidForViewing" runat="server" value="false" />
		<input type="hidden" id="hidSODAltCurrencyCode" runat="server" value="" />
		<input type="hidden" id="hidSODAltDelTerm" runat="server" value="" />
		<input type="hidden" id="hidSODAltValidityPeriod" runat="server" value="" />
		<input type="hidden" id="hidSODAltDeliveryTime" runat="server" value="0" />
		<input type="hidden" id="hidSODAltDeliveryPt" runat="server" value="" />
		<asp:Panel ID="panObjDS" runat="server">
			<asp:ObjectDataSource ID="objSupplierOrderAttach" runat="server" OldValuesParameterFormatString="" SelectMethod="GetSupplierOrderAttachmentList" TypeName="GARMCO.AMS.B2B.eTendering.DAL.SupplierOrderAttachmentRepository" OnSelected="objSupplierOrderAttach_Selected">
				<SelectParameters>
					<asp:Parameter DefaultValue="0" Name="mode" Type="Byte" />
					<asp:Parameter DefaultValue="0" Name="orderAttachNo" Type="Double" />
					<asp:Parameter DefaultValue="0" Name="orderAttachLineNo" Type="Double" />
					<asp:Parameter DefaultValue="0" Name="orderAttachSeq" Type="Decimal" />
					<asp:Parameter DefaultValue="-1" Name="orderAttachSODID" Type="Int32" />
					<asp:Parameter DefaultValue="-1" Name="orderAttachSODAltID" Type="Int32" />
					<asp:Parameter DefaultValue="true" Name="orderAttachSupplier" Type="Boolean" />
					<asp:Parameter DefaultValue="2" Name="orderAttachType" Type="Decimal" />
				</SelectParameters>
			</asp:ObjectDataSource>
			<asp:ObjectDataSource ID="objUM" runat="server" OldValuesParameterFormatString="" SelectMethod="GetAll" TypeName="GARMCO.AMS.B2B.DAL.UnitOfMeasureRepository" />
			<asp:ObjectDataSource ID="objCurrency" runat="server" OldValuesParameterFormatString="" SelectMethod="GetAll" TypeName="GARMCO.AMS.B2B.DAL.CurrencyRepository" />
			<asp:ObjectDataSource ID="objDelTerm" runat="server" OldValuesParameterFormatString="" SelectMethod="GetAll" TypeName="GARMCO.AMS.B2B.DAL.DeliveryTermRepository" />
		</asp:Panel>
		<telerik:RadAjaxManager ID="ajaxMngr" runat="server" OnAjaxRequest="ajaxMngr_AjaxRequest">
			<AjaxSettings>
				<telerik:AjaxSetting AjaxControlID="panEntry">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="panEntry" LoadingPanelID="loadingPanel" />
					</UpdatedControls>
				</telerik:AjaxSetting>
				<telerik:AjaxSetting AjaxControlID="ajaxMngr">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="panEntry" LoadingPanelID="loadingPanel" />
					</UpdatedControls>
				</telerik:AjaxSetting>
			</AjaxSettings>
		</telerik:RadAjaxManager>
		<telerik:RadAjaxLoadingPanel ID="loadingPanel" runat="server" Skin="Windows7" />
    </form>
</body>
</html>
