<%@ Page Title="" Language="C#" MasterPageFile="~/CommonObject/Site.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="GARMCO.AMS.B2B.eTendering.Website.ForgotPassword" %>
<%@ MasterType VirtualPath="~/CommonObject/Site.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content" ContentPlaceHolderID="mainContent" runat="server">
	<asp:Panel ID="panLogin" runat="server" CssClass="GroupLayoutLogin" GroupingText="Login">
		<asp:ValidationSummary ID="valSummary" runat="server" CssClass="ValidationError" HeaderText="Please enter a value on the following field(s):" />
		<table border="0" style="padding: 2px; width: 100%;">
			<tr>
				<td class="LabelBold" style="width: 120px;">
					e-Mail Address
				</td>
				<td>
					<asp:TextBox ID="txtEmail" runat="server" MaxLength="150" SkinID="TextLeftMandatory" />
					<asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="txtEmail" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="e-Mail Address cannot be blank" SetFocusOnError="true" Text="*" ToolTip="e-Mail Address cannot be blank" />
					<asp:RegularExpressionValidator ID="regEmail" runat="server" ControlToValidate="txtEmail" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="Invalid e-Mail Address format" SetFocusOnError="true" ValidationExpression="^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.(([0-9]{1,3})|([a-zA-Z]{2,3})|(aero|coop|info|museum|name))$" Text="*" ToolTip="Invalid e-Mail Address format" />
				</td>
			</tr>
			<tr>
				<td></td>
				<td>
					<asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Requests for the password" Width="110px" onclick="btnSubmit_Click" />
				</td>
			</tr>
		</table>
	</asp:Panel>
	<asp:Panel ID="panObjDS" runat="server">
		<asp:ObjectDataSource ID="objContact" runat="server" OldValuesParameterFormatString="" SelectMethod="GetSupplierContact" TypeName="GARMCO.AMS.B2B.eTendering.DAL.SupplierContactRepository" OnSelected="objContact_Selected">
			<SelectParameters>
				<asp:Parameter DefaultValue="1" Name="mode" Type="Byte" />
				<asp:Parameter DefaultValue="0" Name="contactID" Type="Int32" />
				<asp:ControlParameter ControlID="txtEmail" DefaultValue="" Name="contactEmail" PropertyName="Text" Type="String" />
				<asp:Parameter DefaultValue="" Name="contactActiveKey" Type="String" />
				<asp:Parameter DefaultValue="0" Name="contactSupplierNo" Type="Int32" />
			</SelectParameters>
		</asp:ObjectDataSource>
	</asp:Panel>
	<telerik:RadAjaxManagerProxy ID="ajaxProxy" runat="server">
		<AjaxSettings>
			<telerik:AjaxSetting AjaxControlID="btnSubmit">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="panLogin" LoadingPanelID="loadingPanel" />
				</UpdatedControls>
			</telerik:AjaxSetting>
		</AjaxSettings>
	</telerik:RadAjaxManagerProxy>
	<telerik:RadAjaxLoadingPanel ID="loadingPanel" runat="server" Skin="Windows7" />
</asp:Content>
