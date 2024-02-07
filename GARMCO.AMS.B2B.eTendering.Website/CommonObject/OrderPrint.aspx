<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderPrint.aspx.cs" Inherits="GARMCO.AMS.B2B.eTendering.Website.CommonObject.OrderPrint" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.ReportViewer.WebForms" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Preview</title>
	<script type="text/javascript" src="../Scripts/script.js"></script>
	<script type="text/javascript">
		function ConfigureFolderContentHeight() {
			var fc;
			if (navigator.appName == "Microsoft Internet Explorer") {

				var currentClientHeight = document.documentElement.clientHeight;

				// Modify the folder window
				fc = document.getElementById("fc");
				if (fc != null && currentClientHeight >= 400)
					fc.style.height = String(currentClientHeight - 50) + "px";

			}

			else if (navigator.appName == "Netscape") {

				currentClientHeight = window.innerHeight;

				// Modify the folder window
				fc = document.getElementById("fc");
				if (fc != null && currentClientHeight >= 400)
					fc.style.height = String(currentClientHeight - 50) + "px";

			}
		}
	</script>
</head>
<body>
    <form id="form1" runat="server">
		<asp:ScriptManager ID="scriptMngr" runat="server" />
		<telerik:RadWindowManager ID="winMngr" runat="server" Skin="Windows7" VisibleStatusbar="false" />
		<telerik:RadFormDecorator ID="formDecor" runat="server" Skin="Windows7" />
		<div id="fc" style="height: 300px; padding: 2px; width: 100%;">
			<telerik:ReportViewer ID="repViewer" runat="server" BorderStyle="Groove" Height="98%" Skin="Office2007" Width="100%" />
			<br />
			<asp:Button ID="btnBack" runat="server" CausesValidation="false" Text="<< Back" ToolTip="Close the window" Width="110px" OnClientClick="OnCloseWindow();" />
		</div>
    </form>
</body>
</html>
