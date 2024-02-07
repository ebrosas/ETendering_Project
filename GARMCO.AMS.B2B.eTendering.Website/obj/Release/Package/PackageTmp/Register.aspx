<%@ Page Title="" Language="C#" MasterPageFile="~/CommonObject/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="GARMCO.AMS.B2B.eTendering.Website.Register" %>
<%@ MasterType VirtualPath="~/CommonObject/Site.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content" ContentPlaceHolderID="mainContent" runat="server">
	<script type="text/javascript" src="Scripts/supplierProdServ.js"></script>
	<script type="text/javascript">
		function updateNotification(chkSupplierIncProdServ, chkSupplierNotProdServ) {

			var incProdServ = document.getElementById(chkSupplierIncProdServ);
			if (incProdServ != null) {

				var notProdServ = document.getElementById(chkSupplierNotProdServ);

				notProdServ.checked = incProdServ.checked && notProdServ.checked;
				notProdServ.disabled = !incProdServ.checked;

			}
		}

		function validationFailed(sender, eventArgs) {
			alert(eventArgs.get_fileName() + " exceeded maximum file size.");
		}
	</script>
	<asp:Panel ID="panEntry" runat="server" CssClass="GroupLayoutRegister" GroupingText="Registration" Width="100%">
		<asp:ValidationSummary ID="valSummary" runat="server" CssClass="ValidationError" HeaderText="Please enter values on the following fields:" ValidationGroup="valGroup0" />
		<telerik:RadTabStrip ID="tabControl" runat="server" CausesValidation="false" MultiPageID="multiPg" SelectedIndex="0" Skin="Windows7" Width="100%">
			<Tabs>
				<telerik:RadTab runat="server" PageViewID="pgCompanyInfo" Selected="True" Text="Company Information"/>
				<telerik:RadTab runat="server" Enabled="false" PageViewID="pgContact" Text="Contact Person" />
				<telerik:RadTab runat="server" Enabled="false" PageViewID="pgProductServ" Text="Products and Services" />
				<telerik:RadTab runat="server" Enabled="false" PageViewID="pgPreference" Text="Preferences" />
				<telerik:RadTab runat="server" Enabled="false" PageViewID="pgConfirmation" Text="Confirmation of Registration" />
			</Tabs>
		</telerik:RadTabStrip>
		<telerik:RadMultiPage ID="multiPg" runat="server" SelectedIndex="0" Width="100%">
			<telerik:RadPageView ID="pgCompanyInfo" runat="server">
				<table border="0" style="padding: 2px; width: 100%;">
					<tr>
						<td class="LabelBold" style="width: 120px;">
							Company Name
						</td>
						<td style="width: 220px;">
							<asp:TextBox ID="txtSupplierName" runat="server" MaxLength="40" SkinID="TextLeftMandatory" />
							<asp:RequiredFieldValidator ID="reqSupplierName" runat="server" ControlToValidate="txtSupplierName" CssClass="LabelValidationError" ErrorMessage="Company Name cannot be blank" SetFocusOnError="true" Text="*" ToolTip="Company Name cannot be blank" ValidationGroup="valGroup0" />
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
							<asp:RequiredFieldValidator ID="reqSupplierAdd" runat="server" ControlToValidate="txtSupplierAddr" CssClass="LabelValidationError" ErrorMessage="Billing Address cannot be blank" SetFocusOnError="true" Text="*" ToolTip="Billing Address cannot be blank" ValidationGroup="valGroup0" />
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
							<asp:CompareValidator ID="comSupplierCountry" runat="server" ControlToValidate="cmbSupplierCountry" CssClass="LabelValidationError" ErrorMessage="Select a country" Operator="NotEqual" SetFocusOnError="True" Text="*" ToolTip="Select a country" ValueToCompare="Please select a country..." ValidationGroup="valGroup0" />
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
					<tr>
						<td class="LabelBold">
							Company Profile
						</td>
						<td colspan="3">
							<telerik:RadAsyncUpload ID="uploadSupplierProfile" runat="server" InputSize="50" MaxFileInputsCount="1" Skin="Windows7" TemporaryFolder="~/Temp" onclientvalidationfailed="validationFailed" />
							<asp:HyperLink ID="lnkSupplierProfile" runat="server" Target="_blank" Visible="false" /><asp:LinkButton ID="lnkRemove" runat="server" CausesValidation="false" ForeColor="Red" Text=" (Remove)" Visible="false" OnClick="lnkRemove_Click" />
							<asp:CustomValidator ID="cusSupplierProfile" runat="server" CssClass="LabelValidationError" ErrorMessage="Please upload your company profile" SetFocusOnError="true" Text="*" ToolTip="Please upload your company profile" OnServerValidate="cusSupplierProfile_ServerValidate" ValidationGroup="valGroup0" />
						</td>
					</tr>
					<tr>
						<td></td>
						<td colspan="3">
							<asp:CheckBox ID="chkSupplierExist" runat="server" Text="Have you done any transactions with GARMCO before?" />
						</td>
					</tr>
				</table>
			</telerik:RadPageView>
			<telerik:RadPageView ID="pgContact" runat="server">
				<div class="LabelNote">
					Password must be numeric only with 6-10 in length
				</div>
				<table border="0" style="padding: 2px; width: 100%;">
					<tr>
						<td class="LabelBold" style="width: 120px; height: 43px;">
							e-Mail Address
						</td>
						<td style="width: 220px; height: 43px;">
							<asp:TextBox ID="txtContactEmail" runat="server" MaxLength="150" SkinID="TextLeftMandatory" />
							<asp:RequiredFieldValidator ID="reqContactEmail" runat="server" ControlToValidate="txtContactEmail" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="e-Mail Address cannot be blank" SetFocusOnError="true" Text="*" ToolTip="e-Mail Address cannot be blank" ValidationGroup="valGroup1" />
							<asp:RegularExpressionValidator ID="regContactEmail" runat="server" ControlToValidate="txtContactEmail" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="Invalid e-Mail Address format" SetFocusOnError="true" ValidationExpression="^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.(([0-9]{1,3})|([a-zA-Z]{2,3})|(aero|coop|info|museum|name))$" Text="*" ToolTip="Invalid e-Mail Address format" ValidationGroup="valGroup1" />
						</td>
						<td class="LabelBold" style="width: 145px; height: 43px;">
							Confirm e-Maill Address
						</td>
						<td style="height: 43px">
							<asp:TextBox ID="txtContactEmailConfirm" runat="server" MaxLength="150" SkinID="TextLeftMandatory" />
							<asp:RequiredFieldValidator ID="reqContactEmailConfirm" runat="server" ControlToValidate="txtContactEmailConfirm" CssClass="LabelValidationError" ErrorMessage="Confirm e-Mail Address cannot be blank" SetFocusOnError="true" Text="*" ToolTip="Confirm e-Mail Address cannot be blank" ValidationGroup="valGroup1" />
							<asp:CompareValidator ID="comContactEmailConfirm" runat="server" ControlToCompare="txtContactEmail" CssClass="LabelValidationError" ControlToValidate="txtContactEmailConfirm" Display="Dynamic" ErrorMessage="e-Mail Address doesn't match" SetFocusOnError="true" Text="*" ToolTip="e-Mail Address doesn't match" ValidationGroup="valGroup1" />
						</td>
					</tr>
					<tr>
						<td class="LabelBold">
							Password
						</td>
						<td>
							<asp:TextBox ID="txtContactPassword" runat="server" MaxLength="20" TextMode="Password" SkinID="TextLeftMandatory" />
							<asp:RequiredFieldValidator ID="reqContactPassword" runat="server" ControlToValidate="txtContactPassword" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="Password cannot be blank" SetFocusOnError="true" Text="*" ToolTip="Password cannot be blank" ValidationGroup="valGroup1" />
							<asp:RegularExpressionValidator ID="regContactPassword" runat="server" ControlToValidate="txtContactPassword" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="Password must be numeric only with 6-10 characters only" SetFocusOnError="true" ValidationExpression="^(\d{6,10})$" Text="*" ToolTip="Password must be numeric only with 6-10 characters only" ValidationGroup="valGroup1" />
						</td>
						<td class="LabelBold">
							Confirm Password
						</td>
						<td>
							<asp:TextBox ID="txtContactPasswordConfirm" runat="server" MaxLength="20" TextMode="Password" SkinID="TextLeftMandatory" />
							<asp:RequiredFieldValidator ID="reqContactPasswordConfirm" runat="server" ControlToValidate="txtContactPasswordConfirm" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="Confirm Password cannot be blank" SetFocusOnError="true" Text="*" ToolTip="Confirm Password cannot be blank" ValidationGroup="valGroup1" />
							<asp:CompareValidator ID="comContactPasswordConfirm" runat="server" ControlToCompare="txtContactPassword" CssClass="LabelValidationError" ControlToValidate="txtContactPasswordConfirm" Display="Dynamic" ErrorMessage="Password doesn't match" SetFocusOnError="true" Text="*" ToolTip="Password doesn't match" ValidationGroup="valGroup1" />
						</td>
					</tr>
					<tr>
						<td class="LabelBold">
							Contact Person
						</td>
						<td>
							<asp:TextBox ID="txtContactName" runat="server" MaxLength="50" SkinID="TextLeftMandatory" />
							<asp:RequiredFieldValidator ID="reqContactName" runat="server" ControlToValidate="txtContactName" CssClass="LabelValidationError" ErrorMessage="Contact Person cannot be blank" SetFocusOnError="true" Text="*" ToolTip="Contact Person cannot be blank" ValidationGroup="valGroup1" />
						</td>
						<td class="LabelBold">
							Office Tel. No.
						</td>
						<td>
							<asp:TextBox ID="txtContactTelNo" runat="server" MaxLength="20" SkinID="TextLeft" />
						</td>
					</tr>
					<tr>
						<td class="LabelBold">
							Mobile No.
						</td>
						<td>
							<asp:TextBox ID="txtContactMobNo" runat="server" MaxLength="20" SkinID="TextLeft" />
						</td>
						<td class="LabelBold">
							Fax No.
						</td>
						<td>
							<asp:TextBox ID="txtContactFaxNo" runat="server" MaxLength="20" SkinID="TextLeft" />
						</td>
					</tr>
				</table>
			</telerik:RadPageView>
			<telerik:RadPageView ID="pgProductServ" runat="server">
				<div style="padding: 4px;">
					<b>Note:</b> Please add at least one Product/Service by clicking the box. Click on the description of each item to view a detailed list.
					<asp:CustomValidator ID="cusSupplierProdServ" runat="server" CssClass="LabelValidationError" ErrorMessage="Please select at least one product or service" SetFocusOnError="true" Text="*" ToolTip="Please select at least one product or service" OnServerValidate="cusSupplierProdServ_ServerValidate" ValidationGroup="valGroup2" />
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
							<asp:CompareValidator ID="comSupplierCurrency" runat="server" ControlToValidate="cmbSupplierCurrency" CssClass="LabelValidationError" ErrorMessage="Select a currency" Operator="NotEqual" SetFocusOnError="True" Text="*" ToolTip="Select a currency" ValueToCompare="Please select a currency..." ValidationGroup="valGroup3" />
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
							<asp:CompareValidator ID="comSupplierDelTerm" runat="server" ControlToValidate="cmbSupplierDelTerm" CssClass="LabelValidationError" ErrorMessage="Select a delivery condition" Operator="NotEqual" SetFocusOnError="True" Text="*" ToolTip="Select a delivery condition" ValueToCompare="Please select a delivery condition..." ValidationGroup="valGroup3" />
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
			<telerik:RadPageView ID="pgConfirmation" runat="server">
				<table border="0" style="padding: 2px; width: 100%;">
					<tr>
						<td>
							<telerik:RadCaptcha ID="captchaImg" runat="server" CaptchaTextBoxLabel=" Type the code from the image (it is not case-sensitive)" EnableRefreshImage="True" ErrorMessage="Type the code from the image" ValidationGroup="valGroup4" />
						</td>
					</tr>
					<tr>
						<td>
							<asp:CheckBox ID="chkSupplierTerms" runat="server" Text="I have read and agree to the " />&nbsp;<asp:HyperLink ID="lnkSupplerTerms" runat="server" NavigateUrl="~/garmcotc.pdf" Text="Terms and Condition." Target="_blank" />
							<asp:CustomValidator ID="cusSupplierTerms" runat="server" CssClass="LabelValidationError" ErrorMessage="Please read and agree to the terms and condition" SetFocusOnError="true" Text="*" ToolTip="Please read and agree to the terms and condition" OnServerValidate="cusSupplierTerms_ServerValidate" ValidationGroup="valGroup4" />
						</td>
					</tr>
				</table>
			</telerik:RadPageView>
		</telerik:RadMultiPage>
		<br />
		<asp:Button ID="btnPrev" runat="server" CausesValidation="false" Text="<< Previous" ToolTip="Go to previous page" Visible="false" Width="110px" OnClick="btnPrev_Click" />
		<asp:Button ID="btnNext" runat="server" Text="Next >>" ToolTip="Go to next page" Width="110px" ValidationGroup="valGroup0" OnClick="btnNext_Click" />
		<asp:Button ID="btnRegister" runat="server" Text="Register" ToolTip="Submits the registration form" Visible="false" Width="110px" ValidationGroup="valGroup4" onclick="btnRegister_Click" />
		<input type="hidden" id="hidCompanyProfile" runat="server" value="" />
	</asp:Panel>
	<asp:Panel ID="panObjDS" runat="server">
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
	<telerik:RadProgressManager ID="progressMngr" runat="server" />
	<telerik:RadProgressArea ID="progressArea" runat="server" Skin="Windows7" />
	<telerik:RadAjaxManagerProxy ID="ajaxProxy" runat="server">
		<AjaxSettings>
			<telerik:AjaxSetting AjaxControlID="btnPrev">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="panEntry" LoadingPanelID="loadingPanel" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="btnNext">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="panEntry" LoadingPanelID="loadingPanel" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="btnRegister">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="panEntry" LoadingPanelID="loadingPanel" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="lnkRemove">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="panEntry" LoadingPanelID="loadingPanel" />
				</UpdatedControls>
			</telerik:AjaxSetting>
		</AjaxSettings>
	</telerik:RadAjaxManagerProxy>
	<telerik:RadAjaxLoadingPanel ID="loadingPanel" runat="server" Skin="Windows7" />
</asp:Content>
