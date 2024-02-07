<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierProdServLookup.aspx.cs" Inherits="GARMCO.AMS.B2B.eTendering.Website.CommonObject.SupplierProdServLookup" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Supplier Products/Services Lookup...</title>
	<script type="text/javascript" src="../Scripts/script.js"></script>
	<script type="text/javascript" src="../Scripts/supplierProdServ.js"></script>
</head>
<body>
    <form id="form1" runat="server">
		<asp:ScriptManager ID="scriptMngr" runat="server" />
		<telerik:RadAjaxManager ID="ajaxMngr" runat="server" >
			<AjaxSettings>
				<telerik:AjaxSetting AjaxControlID="treeSupplierProdServ">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="panEntry" />
					</UpdatedControls>
				</telerik:AjaxSetting>
			</AjaxSettings>
		</telerik:RadAjaxManager>
		<telerik:RadFormDecorator ID="formDecor" runat="server" Skin="Windows7" />
		<asp:Panel ID="panEntry" runat="server" Width="100%">
			<telerik:RadTreeView ID="treeSupplierProdServ" runat="server" CausesValidation="false" CheckBoxes="true" CheckChildNodes="true" Skin="Windows7" TriStateCheckBoxes="true" onnodedatabound="treeSupplierProdServ_NodeDataBound" OnNodeExpand="treeSupplierProdServ_NodeExpand" />
			<br />
			<asp:Button ID="btnSubmit" runat="server" CausesValidation="false" Text="Submit" ToolTip="Submits the selected products/services" Width="110px" onclick="btnSubmit_Click" />
			<asp:Button ID="btnBack" runat="server" CausesValidation="false" Text="<< Back" ToolTip="" Width="110px" OnClientClick="OnCloseWindow();" />
		</asp:Panel>
		<asp:Panel ID="panObjDS" runat="server">
			<asp:ObjectDataSource ID="objProdServ" runat="server" OldValuesParameterFormatString="" SelectMethod="GetSupplierProductService" TypeName="GARMCO.AMS.B2B.eTendering.DAL.SupplierProductServiceRepository" OnSelected="objProdServ_Selected">
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
		<input type="hidden" id="hidAjaxID" runat="server" value="" />
    </form>
</body>
</html>
