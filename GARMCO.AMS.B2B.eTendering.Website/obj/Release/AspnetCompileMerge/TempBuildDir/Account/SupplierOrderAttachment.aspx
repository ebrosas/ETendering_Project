<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierOrderAttachment.aspx.cs" Inherits="GARMCO.AMS.B2B.eTendering.Website.Account.SupplierOrderAttachment" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RFQ Attachment</title>
	<script type="text/javascript" src="../Scripts/script.js"></script>
	<script type="text/javascript" src="../Scripts/rfqattachment.js"></script>
	<script type="text/javascript" src="../Scripts/warning.js"></script>
	<script type="text/javascript">
		function ConfigureFolderContentHeight() {
			var fc;
			if (navigator.appName == "Microsoft Internet Explorer") {

				var currentClientHeight = document.documentElement.clientHeight;

				// Modify the folder window
				fc = document.getElementById("fc");
				if (fc != null && currentClientHeight >= 400)
					fc.style.height = String(currentClientHeight - 150) + "px";

			}

			else if (navigator.appName == "Netscape") {

				currentClientHeight = window.innerHeight;

				// Modify the folder window
				fc = document.getElementById("fc");
				if (fc != null && currentClientHeight >= 400)
					fc.style.height = String(currentClientHeight - 150) + "px";

			}
		}

		function validationFailed(sender, eventArgs) {
			alert(eventArgs.get_fileName() + " exceeded maximum file size.");
		}
	</script>
</head>
<body>
    <form id="form1" runat="server">
		<asp:ScriptManager ID="scriptMngr" runat="server" />
		<telerik:RadWindowManager ID="winMngr" runat="server" Skin="Windows7" VisibleStatusbar="false" />
		<telerik:RadFormDecorator ID="formDecor" runat="server" Skin="Windows7" />
		<table border="0" style="padding: 2px; height: 100%; width: 100%;">
			<tr>
				<td style="width: 100%;">
					<telerik:RadToolBar ID="mainToolBar" runat="server" Skin="Office2007" Width="100%" onbuttonclick="mainToolBar_ButtonClick" onprerender="mainToolBar_PreRender">
						<Items>
							<telerik:RadToolBarButton runat="server" CommandName="Text" Text="Create Text">
							</telerik:RadToolBarButton>
							<telerik:RadToolBarButton runat="server" IsSeparator="True" />
							<telerik:RadToolBarButton runat="server" CommandName="File" Text="Upload a File">
							</telerik:RadToolBarButton>
							<telerik:RadToolBarButton runat="server" IsSeparator="True" />
							<telerik:RadToolBarButton runat="server" CommandName="Save" Enabled="false" Text="Save">
							</telerik:RadToolBarButton>
							<telerik:RadToolBarButton runat="server" IsSeparator="True" />
							<telerik:RadToolBarButton runat="server" CommandName="Cancel" Enabled="false" Text="Cancel">
							</telerik:RadToolBarButton>
							<telerik:RadToolBarButton runat="server" IsSeparator="True" />
							<telerik:RadToolBarButton runat="server" CommandName="Delete" Enabled="false" Text="Delete">
							</telerik:RadToolBarButton>
						</Items>
					</telerik:RadToolBar>
				</td>
			</tr>
			<tr>
				<td id="fc" style="height: 350px; vertical-align: top;">
					<asp:ValidationSummary ID="valSummaryEdit" runat="server" CssClass="ValidationError" HeaderText="Please enter a value on the following field(s):" ValidationGroup="valEdit" />
					<telerik:RadSplitter ID="mainSplitter" runat="server" Height="100%" Orientation="Vertical" Skin="Office2007" Width="100%">
						<telerik:RadPane ID="panLeft" runat="server" AllowNaturalSort="True" Height="100%" Width="120px">
							<telerik:RadListView ID="lstMediaObj" runat="server" Width="100%" onitemdatabound="lstMediaObj_ItemDataBound" onselectedindexchanged="lstMediaObj_SelectedIndexChanged" onneeddatasource="lstMediaObj_NeedDataSource">
								<LayoutTemplate>
									<div class="RadListView RadListView_Windows7">
										<table style="width:100%;">
											<thead>
												<tr class="rlvHeader">
													<th>
													</th>
												</tr>
											</thead>
											<tbody>
												<tr id="itemPlaceholder" runat="server">
												</tr>
											</tbody>
										</table>
									</div>
								</LayoutTemplate>
								<ItemTemplate>
									<tr class="rlvI">
										<td style="text-align: center;">
											<asp:LinkButton ID="lnkSelect" runat="server" CausesValidation="false" CommandName="Select">
												<img id="imgMediaObjectType" runat="server" alt="" border="0" /><br />
												<asp:Literal ID="litMediaObjectDocName" runat="server" Text='<%# Bind("OrderAttachDisplayName") %>' />
											</asp:LinkButton><asp:Literal ID="litMediaObjectSequence" runat="server" Text='<%# Bind("OrderAttachSeq") %>' Visible="false" />
											<asp:Literal ID="litMediaObjectType" runat="server" Text='<%# Bind("OrderAttachType") %>' Visible="false" />
										</td>
									</tr>
								</ItemTemplate>
								<AlternatingItemTemplate>
									<tr class="rlvA">
										<td style="text-align: center;">
											<asp:LinkButton ID="lnkSelect" runat="server" CausesValidation="false" CommandName="Select">
												<img id="imgMediaObjectType" runat="server" alt="" border="0" /><br />
												<asp:Literal ID="litMediaObjectDocName" runat="server" Text='<%# Bind("OrderAttachDisplayName") %>' />
											</asp:LinkButton><asp:Literal ID="litMediaObjectSequence" runat="server" Text='<%# Bind("OrderAttachSeq") %>' Visible="false" />
											<asp:Literal ID="litMediaObjectType" runat="server" Text='<%# Bind("OrderAttachType") %>' Visible="false" />
										</td>
									</tr>
								</AlternatingItemTemplate>
								<SelectedItemTemplate>
									<tr class="rlvISel">
										<td style="text-align: center;">
											<asp:LinkButton ID="lnkSelect" runat="server" CausesValidation="false" CommandName="Select">
												<img id="imgMediaObjectType" runat="server" alt="" border="0" /><br />
												<asp:Literal ID="litMediaObjectDocName" runat="server" Text='<%# Bind("OrderAttachDisplayName") %>' />
											</asp:LinkButton><asp:Literal ID="litMediaObjectSequence" runat="server" Text='<%# Bind("OrderAttachSeq") %>' Visible="false" />
											<asp:Literal ID="litMediaObjectType" runat="server" Text='<%# Bind("OrderAttachType") %>' Visible="false" />
										</td>
									</tr>
								</SelectedItemTemplate>
								<EmptyDataTemplate>
									<div class="RadListView RadListView_Windows7">
										<div class="rlvEmpty">No Media Object Found</div>
									</div>
								</EmptyDataTemplate>
							</telerik:RadListView>
						</telerik:RadPane><telerik:RadSplitBar ID="splitBar" runat="server" CollapseMode="Forward" />
						<telerik:RadPane ID="panContent" runat="server">
							<asp:Panel ID="panText" runat="server" Width="100%" Visible="false" style="padding: 15px 15px 15px 15px;">
								<telerik:RadEditor ID="txtMediaObjText" runat="server" EditModes="Design" Height="300px" Skin="Windows7" ToolsFile="~/includes/TextToolFile.xml">
									<Content />
									<CssFiles>
										<telerik:EditorCssFile Value="~/App_Themes/Standard/mediaText.css" />
									</CssFiles>
									<FontNames>
										<telerik:EditorFont Value="Courier New" />
									</FontNames>
								</telerik:RadEditor>
							</asp:Panel>
							<asp:Panel ID="panFile" runat="server" Width="100%" Visible="false" style="padding: 15px 15px 15px 15px;">
								<asp:Panel ID="panFileIput" runat="server">
									<telerik:RadProgressManager ID="progressMngr" runat="server" />
									<div class="LabelBold" style="text-align: left;"><asp:Literal ID="litMaxFileSize" runat="server" Text="" /></div>
									<telerik:RadProgressArea ID="progressArea" runat="server" Skin="Windows7" />
									<telerik:RadAsyncUpload ID="uploadMediaObjFile" runat="server" AllowedFileExtensions="doc,docx,gif,jpeg,jpg,pdf,png,ppt,pptx,txt,xls,xlsx" InputSize="50" MaxFileInputsCount="1" Skin="Windows7" TemporaryFolder="~/Temp" onclientvalidationfailed="validationFailed" />
									<asp:Button ID="btnUploadFile" runat="server" CausesValidation="false" Text="Upload" Width="100px" onclick="btnUploadFile_Click" />
									<div>
										<br /><br />
										<b>Procedure to upload a file:</b><br />
										<ul>
											<li>Select a file using the Select Button</li>
											<li>A message will prompt if the file exceeded the allowed maximum filesize. Click the Remove link to clear the unsuccessful uploaded file and start all over again</li>
											<li>Click the Upload button</li><li>You may also click the Remove link if you decide to change the file that you are going to upload</li>
										</ul>
									</div>
								</asp:Panel>
								<asp:Panel ID="panFileDisplay" runat="server" Visible="false" style="padding: 15px 15px 15px 15px;">
									<asp:HyperLink ID="lnkFile" runat="server" Target="_blank" />
								</asp:Panel>
							</asp:Panel>
						</telerik:RadPane>
					</telerik:RadSplitter>
				</td>
			</tr>
			<tr>
				<td>
					<asp:Panel ID="panAction" runat="server" Width="100%">
						<br />
						<asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submits the changes" ValidationGroup="valPrimary" Width="110px" onclick="btnSubmit_Click" />
						<asp:Button ID="btnReset" runat="server" CausesValidation="false" Text="Clear All" ToolTip="Removes all media objects" Width="110px" onclick="btnReset_Click" />
						<asp:Button ID="btnBack" runat="server" CausesValidation="false" Text="Back" ToolTip="Close the window" Width="110px" onclick="btnBack_Click" />
					</asp:Panel>
				</td>
			</tr>
		</table>
		<input type="hidden" id="hidAjaxID" runat="server" value="" />
		<input type="hidden" id="hidOrderAlternative" runat="server" value="false" />
		<input type="hidden" id="hidForViewing" runat="server" value="false" />
		<input type="hidden" id="hidSelectedMedObj" runat="server" value="-1" />
		<telerik:RadAjaxManager ID="ajaxMngr" runat="server" onajaxrequest="ajaxMngr_AjaxRequest">
			<AjaxSettings>
				<telerik:AjaxSetting AjaxControlID="mainToolBar">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="mainToolBar" />
						<telerik:AjaxUpdatedControl ControlID="mainSplitter" LoadingPanelID="loadingPanel" />
						<telerik:AjaxUpdatedControl ControlID="panAction" />
					</UpdatedControls>
				</telerik:AjaxSetting>
				<telerik:AjaxSetting AjaxControlID="lstMediaObj">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="mainToolBar" />
						<telerik:AjaxUpdatedControl ControlID="mainSplitter" LoadingPanelID="loadingPanel" />
						<telerik:AjaxUpdatedControl ControlID="panAction" />
					</UpdatedControls>
				</telerik:AjaxSetting>
				<telerik:AjaxSetting AjaxControlID="btnUploadFile">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="mainToolBar" />
						<telerik:AjaxUpdatedControl ControlID="mainSplitter" LoadingPanelID="loadingPanel" />
						<telerik:AjaxUpdatedControl ControlID="panAction" />
					</UpdatedControls>
				</telerik:AjaxSetting>
				<telerik:AjaxSetting AjaxControlID="ajaxMngr">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="mainToolBar" />
						<telerik:AjaxUpdatedControl ControlID="mainSplitter" LoadingPanelID="loadingPanel" />
						<telerik:AjaxUpdatedControl ControlID="panAction" />
					</UpdatedControls>
				</telerik:AjaxSetting>
				<telerik:AjaxSetting AjaxControlID="panAction">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="mainToolBar" />
						<telerik:AjaxUpdatedControl ControlID="mainSplitter" LoadingPanelID="loadingPanel" />
						<telerik:AjaxUpdatedControl ControlID="panAction" />
					</UpdatedControls>
				</telerik:AjaxSetting>
			</AjaxSettings>
		</telerik:RadAjaxManager>
		<telerik:RadAjaxLoadingPanel ID="loadingPanel" runat="server" Skin="Windows7" />
    </form>
</body>
</html>
