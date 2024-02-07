<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RFQDetailAttachment.aspx.cs" Inherits="GARMCO.AMS.B2B.eTendering.Website.CommonObject.RFQDetailAttachment" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RFQ Attachment</title>
	<script type="text/javascript" src="../Scripts/script.js"></script>
</head>
<body>
    <form id="form1" runat="server">
		<asp:ScriptManager ID="scriptMngr" runat="server" />
		<telerik:RadAjaxManager ID="ajaxMngr" runat="server">
			<AjaxSettings>
				<telerik:AjaxSetting AjaxControlID="lstAttachment">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="panAttachment" LoadingPanelID="loadingPanel" />
						<telerik:AjaxUpdatedControl ControlID="panObjDS" />
					</UpdatedControls>
				</telerik:AjaxSetting>
			</AjaxSettings>
		</telerik:RadAjaxManager>
		<telerik:RadAjaxLoadingPanel ID="loadingPanel" runat="server" Skin="Windows7" Height="100%" Width="100%" />
		<telerik:RadWindowManager ID="winMngr" runat="server" Skin="Windows7" />
		<telerik:RadFormDecorator ID="formDecor" runat="server" Skin="Windows7" />
		<asp:Panel ID="panAttachment" runat="server" Width="100%">
			<div>Please click the item below to view the full details of the attachment</div>
			<table border="0" style="padding: 2px; width: 100%;">
				<tr>
					<td style="width: 200px; vertical-align: top;">
						<telerik:RadListView ID="lstAttachment" runat="server" ItemPlaceholderID="panOrderAttach" Width="100%" onitemdatabound="lstAttachment_ItemDataBound">
							<LayoutTemplate>
								<asp:PlaceHolder ID="panOrderAttach" runat="server" />
							</LayoutTemplate>
							<ItemTemplate>
								<table border="0" style="padding: 2px; width: 100%;">
									<tr>
										<td style="width: 65px; vertical-align: bottom;">
											<asp:Image ID="imgAttachIcon" runat="server" Height="60" Width="60" />
										</td>
										<td class="LabelBold" style="width: 60px; vertical-align: bottom;">
											Filename
										</td>
										<td style="text-align: left; vertical-align: bottom;">
											<asp:LinkButton ID="lnkAttachDisplayName" runat="server" Text='<%# Eval("OrderAttachDisplayName") %>' CausesValidation="False" Visible="false" onclick="lnkAttachDisplayName_Click" />
											<asp:HyperLink ID="lnkAttachFilename" runat="server" Target="_blank" Text='<%# Eval("OrderAttachDisplayName") %>' ToolTip="Click the file to view it" Visible="false" />
											<asp:Literal ID="litOrderAttachSeq" runat="server" Text='<%# Eval("OrderAttachSeq") %>' Visible="false" />
											<asp:Literal ID="litOrderAttachType" runat="server" Text='<%# Eval("OrderAttachType") %>' Visible="false" />
											<asp:Literal ID="litOrderAttachFilename" runat="server" Text='<%# Eval("OrderAttachFilename") %>' Visible="false" />
										</td>
									</tr>
								</table>
							</ItemTemplate>
						</telerik:RadListView>
					</td>
					<td style="vertical-align: top;">
						<asp:Panel ID="panDisplayText" runat="server" Width="100%" style="display: none;">
							<telerik:RadEditor ID="txtOrderAttachText" runat="server" EditModes="Preview" Height="300px" Skin="Windows7" ToolsFile="~/includes/TextToolFile.xml">
								<Content />
							</telerik:RadEditor>
						</asp:Panel>
						<asp:Panel ID="panDisplayFile" runat="server" Width="100%" style="display: none;">
						</asp:Panel>
					</td>
				</tr>
			</table>
			<br />
			<asp:Button ID="btnBack" runat="server" CausesValidation="false" Text="<< Back" Width="120px" OnClientClick="OnCloseWindow();" />
		</asp:Panel>
		<input type="hidden" id="hidOrderNo" runat="server" value="0" />
		<input type="hidden" id="hidOrderLineNo" runat="server" value="0" />
		<input type="hidden" id="hidOrderAttachSODID" runat="server" value="-1" />
		<input type="hidden" id="hidOrderAttachSODAltID" runat="server" value="-1" />
		<input type="hidden" id="hidOrderSupplier" runat="server" value="false" />
		<asp:Panel ID="panObjDS" runat="server">
			<asp:ObjectDataSource ID="objOrderDetAttach" runat="server" OldValuesParameterFormatString="" SelectMethod="GetOrderRequisitionDetailAttachment" TypeName="GARMCO.AMS.B2B.Admin.DAL.OrderRequisitionDetailAttachmentRepository" OnSelected="objOrderDetAttach_Selected">
				<SelectParameters>
					<asp:Parameter DefaultValue="0" Name="mode" Type="Byte" />
					<asp:ControlParameter ControlID="hidOrderNo" DefaultValue="0" Name="orderAttachNo" PropertyName="Value" Type="Double" />
					<asp:ControlParameter ControlID="hidOrderLineNo" DefaultValue="0" Name="orderAttachLineNo" PropertyName="Value" Type="Double" />
					<asp:Parameter DefaultValue="0" Name="orderAttachSeq" Type="Decimal" />
					<asp:ControlParameter ControlID="hidOrderAttachSODID" DefaultValue="-1" Name="orderAttachSODID" PropertyName="Value" Type="Int32" />
					<asp:ControlParameter ControlID="hidOrderAttachSODAltID" DefaultValue="-1" Name="orderAttachSODAltID" PropertyName="Value" Type="Int32" />
					<asp:ControlParameter ControlID="hidOrderSupplier" DefaultValue="false" Name="orderAttachSupplier" PropertyName="Value" Type="Boolean" />
					<asp:Parameter DefaultValue="2" Name="orderAttachType" Type="Decimal" />
				</SelectParameters>
			</asp:ObjectDataSource>
			<asp:ObjectDataSource ID="objOrderDetAttachDet" runat="server" OldValuesParameterFormatString="" SelectMethod="GetOrderRequisitionDetailAttachment" TypeName="GARMCO.AMS.B2B.Admin.DAL.OrderRequisitionDetailAttachmentRepository" OnSelected="objOrderDetAttachDet_Selected">
				<SelectParameters>
					<asp:Parameter DefaultValue="1" Name="mode" Type="Byte" />
					<asp:ControlParameter ControlID="hidOrderNo" DefaultValue="0" Name="orderAttachNo" PropertyName="Value" Type="Double" />
					<asp:ControlParameter ControlID="hidOrderLineNo" DefaultValue="0" Name="orderAttachLineNo" PropertyName="Value" Type="Double" />
					<asp:Parameter DefaultValue="0" Name="orderAttachSeq" Type="Decimal" />
					<asp:ControlParameter ControlID="hidOrderAttachSODID" DefaultValue="-1" Name="orderAttachSODID" PropertyName="Value" Type="Int32" />
					<asp:ControlParameter ControlID="hidOrderAttachSODAltID" DefaultValue="-1" Name="orderAttachSODAltID" PropertyName="Value" Type="Int32" />
					<asp:ControlParameter ControlID="hidOrderSupplier" DefaultValue="false" Name="orderAttachSupplier" PropertyName="Value" Type="Boolean" />
					<asp:Parameter DefaultValue="2" Name="orderAttachType" Type="Decimal" />
				</SelectParameters>
			</asp:ObjectDataSource>
		</asp:Panel>
    </form>
</body>
</html>
