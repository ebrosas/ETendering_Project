<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierContact.aspx.cs" Inherits="GARMCO.AMS.B2B.eTendering.Website.Account.SupplierContact" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<script type="text/javascript" src="../Scripts/script.js"></script>
	<script type="text/javascript" src="../Scripts/contact.js"></script>
	<script type="text/javascript" src="../Scripts/warning.js"></script>
</head>
<body>
    <form id="form1" runat="server">
		<asp:ScriptManager ID="scriptMngr" runat="server" />
		<telerik:RadWindowManager ID="winMngr" runat="server" Skin="Windows7" VisibleStatusbar="false" />
		<telerik:RadFormDecorator ID="formDecor" runat="server" Skin="Windows7" />
		<asp:Panel ID="panEntry" runat="server" CssClass="GroupLayoutRegister" GroupingText="Contact Person" Width="100%">
			<asp:ValidationSummary ID="valSummaryContact" runat="server" CssClass="ValidationError" HeaderText="Please enter values on the following fields:" ValidationGroup="valContact" />
			<asp:ValidationSummary ID="valSummaryPwd" runat="server" CssClass="ValidationError" HeaderText="Please enter values on the following fields:" ValidationGroup="valPassword" />
			<div class="LabelNote">
				<asp:Literal ID="litNote" runat="server" />
			</div>
			<telerik:RadTabStrip ID="tabControl" runat="server" CausesValidation="false" MultiPageID="multiPg" SelectedIndex="0" Skin="Windows7" Width="100%">
				<Tabs>
					<telerik:RadTab runat="server" PageViewID="pgContact" Selected="True" Text="Details" />
					<telerik:RadTab runat="server" PageViewID="pgChangePwd" Text="Change Password" />
				</Tabs>
			</telerik:RadTabStrip>
			<telerik:RadMultiPage ID="multiPg" runat="server" SelectedIndex="0" Width="100%">
				<telerik:RadPageView ID="pgContact" runat="server">
					<table border="0" style="padding: 2px; width: 100%;">
						<tr>
							<td class="LabelBold" style="width: 120px;">
								e-Mail Address
							</td>
							<td style="width: 220px;">
								<asp:TextBox ID="txtContactEmail" runat="server" MaxLength="150" SkinID="TextLeftMandatory" />
								<asp:RequiredFieldValidator ID="reqContactEmail" runat="server" ControlToValidate="txtContactEmail" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="e-Mail Address cannot be blank" SetFocusOnError="true" Text="*" ToolTip="e-Mail Address cannot be blank" ValidationGroup="valContact" />
								<asp:RegularExpressionValidator ID="regContactEmail" runat="server" ControlToValidate="txtContactEmail" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="Invalid e-Mail Address format" SetFocusOnError="true" ValidationExpression="^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.(([0-9]{1,3})|([a-zA-Z]{2,3})|(aero|coop|info|museum|name))$" Text="*" ToolTip="Invalid e-Mail Address format" ValidationGroup="valContact" />
							</td>
							<td class="LabelBold" style="width: 145px;">
								Confirm e-Maill Address
							</td>
							<td>
								<asp:TextBox ID="txtContactEmailConfirm" runat="server" MaxLength="150" SkinID="TextLeftMandatory" />
								<asp:RequiredFieldValidator ID="reqContactEmailConfirm" runat="server" ControlToValidate="txtContactEmailConfirm" CssClass="LabelValidationError" ErrorMessage="Confirm e-Mail Address cannot be blank" SetFocusOnError="true" Text="*" ToolTip="Confirm e-Mail Address cannot be blank" ValidationGroup="valContact" />
								<asp:CompareValidator ID="comContactEmailConfirm" runat="server" ControlToCompare="txtContactEmail" CssClass="LabelValidationError" ControlToValidate="txtContactEmailConfirm" Display="Dynamic" ErrorMessage="e-Mail Address doesn't match" SetFocusOnError="true" Text="*" ToolTip="e-Mail Address doesn't match" ValidationGroup="valContact" />
							</td>
						</tr>
						<tr>
							<td id="tdPassword" runat="server" colspan="4" style="padding: 0px; display: none;">
								<table border="0" style="padding: 2px; width: 100%;">
									<tr>
										<td class="LabelNote" colspan="4">
											Password must be numeric only with 6-10 in length
										</td>
									</tr>
									<tr>
										<td class="LabelBold" style="width: 115px;">
											Password
										</td>
										<td style="width: 220px;">
											<asp:TextBox ID="txtContactPassword" runat="server" MaxLength="20" TextMode="Password" SkinID="TextLeftMandatory" />
											<asp:RequiredFieldValidator ID="reqContactPassword" runat="server" ControlToValidate="txtContactPassword" CssClass="LabelValidationError" Display="Dynamic" Enabled="false" ErrorMessage="Password cannot be blank" SetFocusOnError="true" Text="*" ToolTip="Password cannot be blank" ValidationGroup="valContact" />
											<asp:RegularExpressionValidator ID="regContactPassword" runat="server" ControlToValidate="txtContactPassword" CssClass="LabelValidationError" Display="Dynamic" Enabled="false" ErrorMessage="Password must be numeric only with 6-10 in length" SetFocusOnError="true" ValidationExpression="^(\d{6,10})$" Text="*" ToolTip="Password must be numeric only with 6-10 in length" ValidationGroup="valContact" />
										</td>
										<td class="LabelBold" style="width: 145px;">
											Confirm Password
										</td>
										<td>
											<asp:TextBox ID="txtContactPasswordConfirm" runat="server" MaxLength="20" TextMode="Password" SkinID="TextLeftMandatory" />
											<asp:RequiredFieldValidator ID="reqContactPasswordConfirm" runat="server" ControlToValidate="txtContactPasswordConfirm" CssClass="LabelValidationError" Display="Dynamic" Enabled="false" ErrorMessage="Confirm Password cannot be blank" SetFocusOnError="true" Text="*" ToolTip="Confirm Password cannot be blank" ValidationGroup="valContact" />
											<asp:CompareValidator ID="comContactPasswordConfirm" runat="server" ControlToCompare="txtContactPassword" CssClass="LabelValidationError" ControlToValidate="txtContactPasswordConfirm" Display="Dynamic" Enabled="false" ErrorMessage="Password doesn't match" SetFocusOnError="true" Text="*" ToolTip="Password doesn't match" ValidationGroup="valContact" />
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td class="LabelBold">
								Contact Person
							</td>
							<td>
								<asp:TextBox ID="txtContactName" runat="server" MaxLength="50" SkinID="TextLeftMandatory" />
								<asp:RequiredFieldValidator ID="reqContactName" runat="server" ControlToValidate="txtContactName" CssClass="LabelValidationError" ErrorMessage="Contact Person cannot be blank" SetFocusOnError="true" Text="*" ToolTip="Contact Person cannot be blank" ValidationGroup="valContact" />
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
						<tr>
							<td></td>
							<td colspan="3">
								<asp:CheckBox ID="chkContactPrimary" runat="server" Text="Set as primary contact?" Visible="false" />
							</td>
						</tr>
					</table>
				</telerik:RadPageView>
				<telerik:RadPageView ID="pgChangePwd" runat="server">
					<div class="LabelNote">
						Password must be numeric only with 6-10 in length
					</div>
					<table border="0" style="padding: 2px; width: 100%;">
						<tr>
							<td class="LabelBold" style="width: 120px;">
								Current Password
							</td>
							<td style="width: 220px;">
								<asp:TextBox ID="txtContactPasswordCurrent" runat="server" MaxLength="20" TextMode="Password" SkinID="TextLeftMandatory" />
								<asp:RequiredFieldValidator ID="reqContactPasswordCurrent" runat="server" ControlToValidate="txtContactPasswordCurrent" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="Current password cannot be blank" SetFocusOnError="true" Text="*" ToolTip="Current password cannot be blank" ValidationGroup="valPassword" />
							</td>
							<td class="LabelBold" style="width: 145px;">
							</td>
							<td>
							</td>
						</tr>
						<tr>
							<td class="LabelBold">
								New Password
							</td>
							<td>
								<asp:TextBox ID="txtContactPasswordUpdate" runat="server" MaxLength="20" TextMode="Password" SkinID="TextLeftMandatory" />
								<asp:RequiredFieldValidator ID="reqContactPasswordUpdate" runat="server" ControlToValidate="txtContactPasswordUpdate" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="New password cannot be blank" SetFocusOnError="true" Text="*" ToolTip="New password cannot be blank" ValidationGroup="valPassword" />
								<asp:RegularExpressionValidator ID="regContactPasswordUpdate" runat="server" ControlToValidate="txtContactPasswordUpdate" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="New password must be numeric only with 6-10 in length" SetFocusOnError="true" ValidationExpression="^(\d{6,10})$" Text="*" ToolTip="New password must be numeric only with 6-10 in length" ValidationGroup="valPassword" />
							</td>
							<td class="LabelBold">
								Confirm New Password
							</td>
							<td>
								<asp:TextBox ID="txtContactPasswordConfirmUpdate" runat="server" MaxLength="20" TextMode="Password" SkinID="TextLeftMandatory" />
								<asp:RequiredFieldValidator ID="reqContactPasswordConfirmUpdate" runat="server" ControlToValidate="txtContactPasswordConfirmUpdate" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="Confirm Password cannot be blank" SetFocusOnError="true" Text="*" ToolTip="Confirm Password cannot be blank" ValidationGroup="valPassword" />
								<asp:CompareValidator ID="comContactPasswordConfirmUpdate" runat="server" ControlToCompare="txtContactPasswordUpdate" CssClass="LabelValidationError" ControlToValidate="txtContactPasswordConfirmUpdate" Display="Dynamic" ErrorMessage="Password doesn't match" SetFocusOnError="true" Text="*" ToolTip="Password doesn't match" ValidationGroup="valPassword" />
							</td>
						</tr>
					</table>
				</telerik:RadPageView>
			</telerik:RadMultiPage>
			<br />
			<asp:Button ID="btnUpdate" runat="server" Text="Update" Width="110px" OnClick="btnUpdate_Click" />
			<asp:Button ID="btnBack" runat="server" CausesValidation="false" Text="<< Back" Width="110px" OnClientClick="OnCloseWindow();" />
			<input type="hidden" id="hidMode" runat="server" value="0" />
			<input type="hidden" id="hidContactID" runat="server" value="0" />
			<input type="hidden" id="hidAjaxID" runat="server" value="" />
		</asp:Panel>
		<asp:Panel ID="panObjDS" runat="server">
			<asp:ObjectDataSource ID="objSupplierContact" runat="server" OldValuesParameterFormatString="" SelectMethod="GetSupplierContact" TypeName="GARMCO.AMS.B2B.eTendering.DAL.SupplierContactRepository" OnSelected="objSupplierContact_Selected" UpdateMethod="InsertUpdateDeleteSupplierContact" OnUpdated="objSupplierContact_Updated">
			<SelectParameters>
				<asp:Parameter DefaultValue="0" Name="mode" Type="Byte" />
				<asp:Parameter DefaultValue="0" Direction="InputOutput" Name="contactID" Type="Int32" />
				<asp:Parameter DefaultValue="" Name="contactEmail" Type="String" />
				<asp:Parameter DefaultValue="" Name="contactActiveKey" Type="String" />
				<asp:SessionParameter DefaultValue="0" Name="contactSupplierNo" SessionField="CONTACT_SUPPLIER_NO" Type="Int32" />
			</SelectParameters>
			<UpdateParameters>
				<asp:ControlParameter ControlID="hidMode" DefaultValue="0" Name="mode" PropertyName="Value" Type="Byte" />
				<asp:ControlParameter ControlID="hidContactID" DefaultValue="0" Direction="InputOutput" Name="contactID" PropertyName="Value" Type="Int32" />
				<asp:SessionParameter DefaultValue="0" Name="contactSupplierNo" SessionField="CONTACT_SUPPLIER_NO" Type="Int32" />
				<asp:ControlParameter ControlID="txtContactName" DefaultValue="" Name="contactName" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="txtContactEmail" DefaultValue="" Name="contactEmail" PropertyName="Text" Type="String" />
				<asp:Parameter DefaultValue="" Name="contactPassword" Type="String" />
				<asp:ControlParameter ControlID="txtContactTelNo" DefaultValue="" Name="contactTelNo" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="txtContactMobNo" DefaultValue="" Name="contactMobNo" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="txtContactFaxNo" DefaultValue="" Name="contactFaxNo" PropertyName="Text" Type="String" />
				<asp:Parameter DefaultValue="" Name="contactActiveKey" Type="String" />
				<asp:ControlParameter ControlID="chkContactPrimary" DefaultValue="false" Name="contactPrimary" PropertyName="Checked" Type="Boolean" />
				<asp:SessionParameter DefaultValue="0" Name="contactCreatedModifiedBy" SessionField="CONTACT_ID" Type="Int32" />
				<asp:SessionParameter DefaultValue="" Name="contactCreatedModifiedName" SessionField="CONTACT_NAME" Type="String" />
				<asp:Parameter Direction="InputOutput" Name="retError" Type="Object" />
				<asp:Parameter Direction="InputOutput" Name="errorMsg" Type="String" />
			</UpdateParameters>
		</asp:ObjectDataSource>
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
