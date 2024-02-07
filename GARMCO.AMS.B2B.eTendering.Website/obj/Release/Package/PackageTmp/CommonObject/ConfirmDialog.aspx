<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmDialog.aspx.cs" Inherits="GARMCO.AMS.B2B.eTendering.Website.CommonObject.ConfirmDialog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<script type="text/javascript" src="../Scripts/script.js"></script>
	<script type="text/javascript" src="../Scripts/warning.js"></script>
</head>
<body>
    <form id="form1" runat="server">
		<asp:ScriptManager ID="scriptMngr" runat="server" />
		<telerik:RadFormDecorator ID="formDecor" runat="server" Skin="Windows7" />
		<asp:Panel ID="panWarning" runat="server" CssClass="GroupLayoutWarning" GroupingText="Warning" Width="100%">
			<div class="LabelBold" style="text-align: left;">
				<asp:Literal ID="litWarning" runat="server" />
			</div><br />
			<asp:Button ID="btnYes" runat="server" CausesValidation="false" Text="Yes" Width="100px" />
			<asp:Button ID="btnNo" runat="server" CausesValidation="false" Text="No" Width="100px" />
			<asp:Button ID="btnCancel" runat="server" CausesValidation="false" Text="Cancel" Width="100px" OnClientClick="OnCloseWindow();" />
		</asp:Panel>
    </form>
</body>
</html>
