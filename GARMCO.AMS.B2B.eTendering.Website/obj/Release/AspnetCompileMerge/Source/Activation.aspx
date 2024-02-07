<%@ Page Title="" Language="C#" MasterPageFile="~/CommonObject/Site.Master" AutoEventWireup="true" CodeBehind="Activation.aspx.cs" Inherits="GARMCO.AMS.B2B.eTendering.Website.Activation" %>
<%@ MasterType VirtualPath="~/CommonObject/Site.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content" ContentPlaceHolderID="mainContent" runat="server">
	<asp:Panel ID="panEntry" runat="server" GroupingText="Account Activation" Width="100%">
		<asp:ValidationSummary ID="valSummary" runat="server" CssClass="ValidationError" HeaderText="Please enter a value on the following field(s):" ValidationGroup="valPrimary" />
		<div class="LabelNote">
			Please enter your password and the captcha code, then click the Activate button to complete the process.
		</div>
		<table border="0" style="padding: 2px; width: 100%;">
			<tr>
				<td class="LabelBold" style="width: 120px;">
					e-Mail Address
				</td>
				<td>
					<asp:Literal ID="litContactEmail" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="LabelBold">
					Password
				</td>
				<td>
					<asp:TextBox ID="txtPassword" runat="server" MaxLength="50" TextMode="Password" SkinID="TextLeftMandatory" />
					<asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="txtPassword" CssClass="LabelValidationError" ErrorMessage="Password cannot be blank." SetFocusOnError="true" Text="*" ToolTip="Password cannot be blank." ValidationGroup="valPrimary" />
				</td>
			</tr>
			<tr>
				<td colspan="2">
					<telerik:RadCaptcha ID="captchaImg" runat="server" CaptchaTextBoxLabel=" Type the code from the image (it is not case-sensitive)" EnableRefreshImage="True" ErrorMessage="Type the code from the image" ValidationGroup="valPrimary" />
				</td>
			</tr>
			<tr>
				<td colspan="2">
					<asp:Button ID="btnActivate" runat="server" Text="Activate" Width="110px" ValidationGroup="valPrimary" Visible="false" OnClick="btnActivate_Click" />
				</td>
			</tr>
		</table>
	</asp:Panel>
	<asp:Panel ID="panObjDS" runat="server">
		<asp:ObjectDataSource ID="objContact" runat="server" OldValuesParameterFormatString="" SelectMethod="GetSupplierContact" TypeName="GARMCO.AMS.B2B.eTendering.DAL.SupplierContactRepository" OnSelected="objContact_Selected" OnUpdated="objContact_Updated" UpdateMethod="InsertUpdateDeleteSupplierContact">
			<SelectParameters>
				<asp:Parameter DefaultValue="2" Name="mode" Type="Byte" />
				<asp:Parameter DefaultValue="0" Name="contactID" Type="Int32" />
				<asp:ControlParameter ControlID="litContactEmail" DefaultValue="" Name="contactEmail" PropertyName="Text" Type="String" />
				<asp:QueryStringParameter DefaultValue="" Name="contactActiveKey" QueryStringField="actid" Type="String" />
				<asp:Parameter DefaultValue="0" Name="contactSupplierNo" Type="Int32" />
			</SelectParameters>
			<UpdateParameters>
				<asp:Parameter DefaultValue="3" Name="mode" Type="Byte" />
				<asp:Parameter DefaultValue="0" Direction="InputOutput" Name="contactID" Type="Int32" />
				<asp:Parameter DefaultValue="0" Name="contactSupplierNo" Type="Int32" />
				<asp:Parameter DefaultValue="" Name="contactName" Type="String" />
				<asp:Parameter DefaultValue="" Name="contactEmail" Type="String" />
				<asp:Parameter DefaultValue="" Name="contactPassword" Type="String" />
				<asp:Parameter DefaultValue="" Name="contactTelNo" Type="String" />
				<asp:Parameter DefaultValue="" Name="contactMobNo" Type="String" />
				<asp:Parameter DefaultValue="" Name="contactFaxNo" Type="String" />
				<asp:Parameter DefaultValue="" Name="contactActiveKey" Type="String" />
				<asp:Parameter DefaultValue="false" Name="contactPrimary" Type="Boolean" />
				<asp:Parameter DefaultValue="0" Name="contactCreatedModifiedBy" Type="Int32" />
				<asp:Parameter DefaultValue="" Name="contactCreatedModifiedName" Type="String" />
				<asp:Parameter DefaultValue="0" Direction="InputOutput" Name="retError" Type="Object" />
				<asp:Parameter DefaultValue="" Direction="InputOutput" Name="errorMsg" Type="String" />
			</UpdateParameters>
		</asp:ObjectDataSource>
	</asp:Panel>
</asp:Content>
