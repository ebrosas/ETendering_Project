<%@ Page Title="" Language="C#" MasterPageFile="~/CommonObject/Site.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="GARMCO.AMS.B2B.eTendering.Website.ContactUs" %>
<%@ MasterType VirtualPath="~/CommonObject/Site.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content" ContentPlaceHolderID="mainContent" runat="server">
	<asp:Panel ID="panEntry" runat="server" CssClass="GroupLayoutFeedback" GroupingText="Contact Us" Width="100%">
		<asp:ValidationSummary ID="valSummary" runat="server" CssClass="ValidationError" HeaderText="Please enter a value on the following field(s):" ValidationGroup="valPrimary" />
		<table border="0" style="padding: 2px; width: 100%;">
			<tr>
				<td class="LabelBold" style="width: 150px;">
					From (e-mail address)
				</td>
				<td>
					<asp:TextBox ID="txtFrom" runat="server" MaxLength="150" SkinID="TextLeftMandatory" />
					<asp:RequiredFieldValidator ID="reqFrom" runat="server" ControlToValidate="txtFrom" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="From (e-Mail address) cannot be empty" SetFocusOnError="true" Text="*" ToolTip="From (e-Mail address) cannot be empty" ValidationGroup="valPrimary" />
					<asp:RegularExpressionValidator ID="regFrom" runat="server" ControlToValidate="txtFrom" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="Invalid e-Mail Address format" SetFocusOnError="true" ValidationExpression="^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.(([0-9]{1,3})|([a-zA-Z]{2,3})|(aero|coop|info|museum|name))$" Text="*" ToolTip="Invalid e-Mail Address format" ValidationGroup="valPrimary" />
				</td>
			</tr>
			<tr>
				<td class="LabelBold">
					Subject
				</td>
				<td>
					<asp:TextBox ID="txtSubject" runat="server" MaxLength="150" SkinID="TextLeftMandatory" />
					<asp:RequiredFieldValidator ID="reqSubject" runat="server" ControlToValidate="txtSubject" CssClass="LabelValidationError" Display="Dynamic" ErrorMessage="Subject cannot be empty" SetFocusOnError="true" Text="*" ToolTip="Subject cannot be empty" ValidationGroup="valPrimary" />
				</td>
			</tr>
			<tr>
				<td class="LabelBold" style="vertical-align: top;">
					Message
				</td>
				<td>
					<telerik:RadEditor ID="txtMessage" runat="server" EditModes="Design" Height="300px" Skin="Windows7" ToolsFile="~/includes/TextToolFile.xml">
						<Content />
						<FontNames>
							<telerik:EditorFont Value="Courier New" />
						</FontNames>
					</telerik:RadEditor>
					<asp:RequiredFieldValidator ID="reqMessage" runat="server" ControlToValidate="txtMessage" CssClass="LabelValidationError" ErrorMessage="Message cannot be empty" SetFocusOnError="true" Text="*" ToolTip="Message cannot be empty" ValidationGroup="valPrimary" />
				</td>
			</tr>
			<tr>
				<td></td>
				<td>
					<telerik:RadCaptcha ID="captchaImg" runat="server" CaptchaTextBoxLabel=" Type the code from the image (it is not case-sensitive)" EnableRefreshImage="True" ErrorMessage="Type the code from the image" ValidationGroup="valPrimary" />
				</td>
			</tr>
			<tr>
				<td></td>
				<td>
					<asp:Button ID="btnSend" runat="server" Text="Send" Width="120px" onclick="btnSend_Click" ValidationGroup="valPrimary" />
					<asp:Button ID="btnReset" runat="server" CausesValidation="false" Text="Reset" Width="120px" onclick="btnReset_Click" />
				</td>
			</tr>
		</table>
	</asp:Panel>
	<telerik:RadAjaxManagerProxy ID="ajaxProxy" runat="server">
		<AjaxSettings>
			<telerik:AjaxSetting AjaxControlID="btnSend">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="panEntry" LoadingPanelID="loadingPanel" />
					<telerik:AjaxUpdatedControl ControlID="panObjDS" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="btnReset">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="panEntry" />
				</UpdatedControls>
			</telerik:AjaxSetting>
		</AjaxSettings>
	</telerik:RadAjaxManagerProxy>
	<telerik:RadAjaxLoadingPanel ID="loadingPanel" runat="server" Skin="Windows7" />
</asp:Content>
