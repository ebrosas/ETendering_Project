<%@ Page Title="" Language="C#" MasterPageFile="~/CommonObject/Site.Master" AutoEventWireup="true" CodeBehind="PublishedRFQView.aspx.cs" Inherits="GARMCO.AMS.B2B.eTendering.Website.Account.PublishedRFQView" %>
<%@ MasterType VirtualPath="~/CommonObject/Site.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content" ContentPlaceHolderID="mainContent" runat="server">
	<script type="text/javascript" src="../Scripts/rfqattachment.js"></script>
	<asp:Panel ID="panSearch" runat="server" CssClass="GroupLayoutSearch" GroupingText="Search Filters" Width="100%">
		<asp:ValidationSummary ID="valSummary" runat="server" CssClass="ValidationError" HeaderText="Please enter values on the following fields:" ValidationGroup="valSearch" />
		<table border="0" style="padding: 2px; width: 100%">
			<tr>
				<td class="LabelBold" style="width: 120px;">
					RFQ No.
				</td>
				<td style="width: 240px;">
					<asp:TextBox ID="txtRFQNo" runat="server" MaxLength="10" SkinID="TextLeft" />
					<asp:RegularExpressionValidator ID="regRFQNo" runat="server" ControlToValidate="txtRFQNo" CssClass="LabelValidationError" ErrorMessage="RFQ No. must be numeric" SetFocusOnError="true" Text="*" ToolTip="RFQ No. must be numeric" ValidationExpression="(^[ ]*?\d+[ ]*?$)" ValidationGroup="valSearch" />
				</td>
				<td class="LabelBold" style="width: 120px;">
					Closing Date
				</td>
				<td>
					<telerik:RadDatePicker ID="calRFQClosingDateStart" runat="server" DateInput-DateFormat="dd/MM/yyyy" SharedCalendarID="calShared" Skin="Windows7" Width="100px" />&nbsp;~&nbsp;
					<telerik:RadDatePicker ID="calRFQClosingDateEnd" runat="server" DateInput-DateFormat="dd/MM/yyyy" SharedCalendarID="calShared" Skin="Windows7" Width="100px" />
					<asp:CompareValidator ID="comRFQClosingDateEnd" runat="server" ControlToCompare="calRFQClosingDateStart" ControlToValidate="calRFQClosingDateEnd" CssClass="LabelValidationError" ErrorMessage="Starting date must be equal or greater than the ending date." Operator="GreaterThanEqual" SetFocusOnError="true" Text="*" ToolTip="Starting date must be equal or greater than the ending date." ValidationGroup="valSearch" />
				</td>
			</tr>
			<tr>
				<td class="LabelBold">
					Description
				</td>
				<td colspan="3">
					<asp:TextBox ID="txtRFQDesc" runat="server" MaxLength="300" SkinID="TextLeft" />
				</td>
			</tr>
			<tr>
				<td></td>
				<td colspan="3">
					<asp:Button ID="btnSearch" runat="server" Text="Search" ToolTip="Search the record based on the set criteria" Width="110px" onclick="btnSearch_Click" ValidationGroup="valSearch" style="height: 26px" />
					<asp:Button ID="btnReset" runat="server" CausesValidation="false" Text="Reset Criteria" ToolTip="Reset the search criteria to its default values" Width="110px" onclick="btnReset_Click" />
				</td>
			</tr>
		</table>
		<telerik:RadCalendar ID="calShared" runat="server" CultureInfo="en-GB" EnableMultiSelect="False" SelectedDate="" Skin="Windows7" />
	</asp:Panel>
	<br />
	<div class="LabelNote">
		Please use the tabs below to navigate to the different RFQs that were published as well as you have participated with.
	</div>
	<asp:Panel ID="panResult" runat="server" CssClass="SearchResult">
		<telerik:RadTabStrip ID="tabControl" runat="server" AutoPostBack="true" CausesValidation="false" MultiPageID="multiPg" SelectedIndex="0" Skin="Windows7" Width="100%" OnTabClick="tabControl_TabClick">
			<Tabs>
				<telerik:RadTab runat="server" PageViewID="pgOpenRFQ" Selected="True" Text="Open RFQs" />
				<telerik:RadTab runat="server" PageViewID="pgQuotedRFQOpen" Text="Quoted RFQs (Open for Bidding)" />
				<telerik:RadTab runat="server" PageViewID="pgQuotedRFQClosed" Text="Quoted RFQs (Closed for Bidding)" />
				<telerik:RadTab runat="server" PageViewID="pgClosedRFQ" Text="Closed RFQs" Visible="false" />
			</Tabs>
		</telerik:RadTabStrip>
		<telerik:RadMultiPage ID="multiPg" runat="server" SelectedIndex="0" Width="100%">
			<telerik:RadPageView ID="pgOpenRFQ" runat="server">
				<telerik:RadGrid ID="gvList" runat="server" AllowPaging="true" AllowSorting="false" CellSpacing="0" DataSourceID="objOrderReq" GridLines="None" onitemcommand="gvList_ItemCommand" onitemdatabound="gvList_ItemDataBound" onprerender="gvList_PreRender" onsortcommand="gvList_SortCommand" Skin="Windows7" Width="100%">
					<ExportSettings>
						<Pdf>
							<PageHeader>
								<LeftCell Text="" />
								<MiddleCell Text="" />
								<RightCell Text="" />
							</PageHeader>
							<PageFooter>
								<LeftCell Text="" />
								<MiddleCell Text="" />
								<RightCell Text="" />
							</PageFooter>
						</Pdf>
					</ExportSettings>
					<MasterTableView AutoGenerateColumns="False" DataKeyNames="OrderCompany,OrderNo,OrderType,OrderSuffix" DataSourceID="objOrderReq" NoMasterRecordsText="No Published RFQ Found" PagerStyle-AlwaysVisible="true" TableLayout="Fixed" Width="100%">
						<CommandItemSettings ExportToPdfText="Export to PDF" />
						<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
							<HeaderStyle Width="20px" />
						</RowIndicatorColumn>
						<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
							<HeaderStyle Width="20px" />
						</ExpandCollapseColumn>
						<Columns>
							<telerik:GridButtonColumn ButtonType="LinkButton" CommandName="Select" Text="Bid" UniqueName="Bid">
								<HeaderStyle Width="70px" />
								<ItemStyle HorizontalAlign="Center" Width="70px" />
							</telerik:GridButtonColumn>
							<telerik:GridBoundColumn DataField="OrderCompany" FilterControlAltText="Filter OrderCompany column" HeaderText="OrderCompany" ReadOnly="True" SortExpression="OrderCompany" UniqueName="OrderCompany" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderNo" DataType="System.Double" FilterControlAltText="Filter OrderNo column" HeaderText="RFQ No." 
                                SortExpression="OrderNo" UniqueName="OrderNo">
								<HeaderStyle HorizontalAlign="Right" Width="100px" />
								<ItemStyle HorizontalAlign="Right" Width="100px"  />
							</telerik:GridBoundColumn>		
                            <telerik:GridTemplateColumn DataField="OrderBuyerEmpName" FilterControlAltText="Filter OrderBuyerEmpName column" HeaderText="Buyer" 
                                SortExpression="OrderBuyerEmpName" UniqueName="OrderBuyerEmpName">
								<HeaderStyle Width="220px" HorizontalAlign="Left" />
								<ItemTemplate>
									<div class="columnEllipsis" style="width: 220px; text-align: left;">
										<asp:Literal ID="litOrderBuyerEmpName" runat="server" Text='<%# Eval("OrderBuyerEmpName") %>' />
									</div>
								</ItemTemplate>
							</telerik:GridTemplateColumn>					
							<telerik:GridBoundColumn DataField="OrderPublishedDate" DataFormatString="{0:dd MMM yyyy}" DataType="System.DateTime" 
                                FilterControlAltText="Filter OrderPublishedDate column" HeaderText="Date Published" SortExpression="OrderPublishedDate" 
                                UniqueName="OrderPublishedDate">
								<HeaderStyle HorizontalAlign="Right" Width="130px" />
								<ItemStyle HorizontalAlign="Right" Width="130px" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="OrderClosingDate" DataFormatString="{0:dd MMM yyyy HH:mm}" DataType="System.DateTime" 
                                FilterControlAltText="Filter OrderClosingDate column" HeaderText="Closing Date (GMT +3)" SortExpression="OrderClosingDate" 
                                UniqueName="OrderClosingDate">
								<HeaderStyle HorizontalAlign="Right" Width="140px" />
								<ItemStyle HorizontalAlign="Right" Width="140px" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="TotalOrderLine" DataType="System.Int32" FilterControlAltText="Filter TotalOrderLine column" 
                                HeaderText="Total Order Lines" ReadOnly="True" SortExpression="TotalOrderLine" UniqueName="TotalOrderLine">
								<HeaderStyle HorizontalAlign="Right" Width="120px" />
								<ItemStyle HorizontalAlign="Right" Width="120px" />
							</telerik:GridBoundColumn>                           
							<telerik:GridTemplateColumn DataField="OrderStatusDesc" FilterControlAltText="Filter OrderStatusDesc column" HeaderText="Status" SortExpression="OrderStatusDesc" UniqueName="OrderStatusDesc">
								<HeaderStyle Width="220px" HorizontalAlign="Left" />
								<ItemTemplate>
									<div class="columnEllipsis" style="width: 220px; text-align: left;">
										<asp:Literal ID="litOrderStatusDesc" runat="server" Text='<%# Eval("OrderStatusDesc") %>' />
									</div>
								</ItemTemplate>
							</telerik:GridTemplateColumn>                            
							<telerik:GridBoundColumn DataField="OrderType" FilterControlAltText="Filter OrderType column" HeaderText="OrderType" ReadOnly="True" SortExpression="OrderType" UniqueName="OrderType" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderSuffix" FilterControlAltText="Filter OrderSuffix column" HeaderText="OrderSuffix" ReadOnly="True" SortExpression="OrderSuffix" UniqueName="OrderSuffix" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderDescription" FilterControlAltText="Filter OrderDescription column" HeaderText="Description" SortExpression="OrderDescription" UniqueName="OrderDescription" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderBuyerEmpNo" DataType="System.Int32" FilterControlAltText="Filter OrderBuyerEmpNo column" HeaderText="OrderBuyerEmpNo" SortExpression="OrderBuyerEmpNo" UniqueName="OrderBuyerEmpNo" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderBuyerEmpEmail" FilterControlAltText="Filter OrderBuyerEmpEmail column" HeaderText="OrderBuyerEmpEmail" SortExpression="OrderBuyerEmpEmail" UniqueName="OrderBuyerEmpEmail" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderCategory" FilterControlAltText="Filter OrderCategory column" HeaderText="OrderCategory" SortExpression="OrderCategory" UniqueName="OrderCategory" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderStatus" FilterControlAltText="Filter OrderStatus column" HeaderText="OrderStatus" SortExpression="OrderStatus" UniqueName="OrderStatus" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderSupplierParticipated" Display="false" FilterControlAltText="Filter OrderSupplierParticipated column" 
                                HeaderText="OrderSupplierParticipated" SortExpression="OrderSupplierParticipated" UniqueName="OrderSupplierParticipated" />                            
						</Columns>
						<EditFormSettings>
							<EditColumn FilterControlAltText="Filter EditCommandColumn column">
							</EditColumn>
						</EditFormSettings>
						<NestedViewTemplate>
							<div id="nested" class="SearchResult" style="padding: 15px 15px 15px 15px; table-layout: fixed; width: 100%;">
								<telerik:RadGrid ID="gvOrderDet" runat="server" Skin="Web20" Width="90%" CellSpacing="0" GridLines="None" onitemcommand="gvOrderDet_ItemCommand" onitemdatabound="gvOrderDet_ItemDataBound">
									<MasterTableView AutoGenerateColumns="False" NoMasterRecordsText="No Order Details Found" TableLayout="Fixed">
										<CommandItemSettings ExportToPdfText="Export to PDF" />
										<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
											<HeaderStyle Width="20px" />
										</RowIndicatorColumn>
										<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
											<HeaderStyle Width="20px" />
										</ExpandCollapseColumn>
										<Columns>
											<telerik:GridTemplateColumn DataField="OrderDetDesc" FilterControlAltText="Filter OrderDetDesc column" HeaderText="Description" UniqueName="OrderDetDesc">
												<HeaderStyle Width="200px" />
												<ItemTemplate>
													<div class="columnEllipsis">
														<asp:Label ID="lblOrderDetDesc" runat="server" Text='<%# Eval("OrderDetDesc")%>' ToolTip='<%# Eval("OrderDetDesc")%>' />
													</div>
												</ItemTemplate>
											</telerik:GridTemplateColumn>
											<telerik:GridBoundColumn DataField="OrderDetQuantity" DataType="System.Double" FilterControlAltText="Filter OrderDetQuantity column" HeaderText="Required Quantity" UniqueName="OrderDetQuantity">
												<HeaderStyle HorizontalAlign="Right" Width="120px" />
												<ItemStyle HorizontalAlign="Right" />
											</telerik:GridBoundColumn>
											<telerik:GridBoundColumn DataField="OrderDetUM" FilterControlAltText="Filter OrderDetUM column" HeaderText="Unit of Measure" UniqueName="OrderDetUM">
												<HeaderStyle HorizontalAlign="Center" Width="120px" />
												<ItemStyle HorizontalAlign="Center" />
											</telerik:GridBoundColumn>
											<telerik:GridButtonColumn ButtonType="ImageButton" CommandName="OrderDetAttachText" HeaderText="Extended Description" ImageUrl="~/includes/images/attach_small_f1.gif" UniqueName="OrderDetAttachText">
												<HeaderStyle HorizontalAlign="Center" Width="80px" />
												<ItemStyle HorizontalAlign="Center" />
											</telerik:GridButtonColumn>
											<telerik:GridButtonColumn ButtonType="ImageButton" CommandName="OrderDetAttachFile" HeaderText="File Attachment" ImageUrl="~/includes/images/attach_small_f1.gif" UniqueName="OrderDetAttachFile">
												<HeaderStyle HorizontalAlign="Center" Width="80px" />
												<ItemStyle HorizontalAlign="Center" />
											</telerik:GridButtonColumn>
											<telerik:GridBoundColumn DataField="OrderDetCompany" FilterControlAltText="Filter OrderDetCompany column" HeaderText="OrderDetCompany" ReadOnly="True" SortExpression="OrderDetCompany" UniqueName="OrderDetCompany" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetNo" DataType="System.Double" Display="false" FilterControlAltText="Filter OrderDetNo column" HeaderText="OrderDetNo" SortExpression="OrderDetNo" UniqueName="OrderDetNo" />
											<telerik:GridBoundColumn DataField="OrderDetType" FilterControlAltText="Filter OrderDetType column" HeaderText="OrderDetType" ReadOnly="True" SortExpression="OrderDetType" UniqueName="OrderDetType" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetSuffix" FilterControlAltText="Filter OrderDetSuffix column" HeaderText="OrderDetSuffix" ReadOnly="True" SortExpression="OrderDetSuffix" UniqueName="OrderDetSuffix" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetLineNo" DataType="System.Double" Display="false" FilterControlAltText="Filter OrderDetLineNo column" HeaderText="OrderDetLineNo" SortExpression="OrderDetLineNo" UniqueName="OrderDetLineNo" />
											<telerik:GridBoundColumn DataField="OrderDetLineType" Display="false" FilterControlAltText="Filter OrderDetLineType column" HeaderText="OrderDetLineType" SortExpression="OrderDetLineType" UniqueName="OrderDetLineType" />
											<telerik:GridBoundColumn DataField="OrderDetUNSPSC" FilterControlAltText="Filter OrderDetUNSPSC column" HeaderText="OrderDetUNSPSC" SortExpression="OrderDetUNSPSC" UniqueName="OrderDetUNSPSC" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetStockShortItemNo" DataType="System.Double" FilterControlAltText="Filter OrderDetStockShortItemNo column" HeaderText="OrderDetStockShortItemNo" SortExpression="OrderDetStockShortItemNo" UniqueName="OrderDetStockShortItemNo" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetStockLongItemNo" FilterControlAltText="Filter OrderDetStockLongItemNo column" HeaderText="OrderDetStockLongItemNo" SortExpression="OrderDetStockLongItemNo" UniqueName="OrderDetStockLongItemNo" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetStatus" FilterControlAltText="Filter OrderDetStatus column" HeaderText="OrderDetStatus" SortExpression="OrderDetStatus" UniqueName="OrderDetStatus" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetTotalExtAttachment" DataType="System.Int32" Display="false" FilterControlAltText="Filter OrderDetTotalExtAttachment column" HeaderText="OrderDetTotalExtAttachment" ReadOnly="True" SortExpression="OrderDetTotalExtAttachment" UniqueName="OrderDetTotalExtAttachment" />
											<telerik:GridBoundColumn DataField="OrderDetTotalFileAttachment" DataType="System.Int32" Display="false" FilterControlAltText="Filter OrderDetTotalFileAttachment column" HeaderText="OrderDetTotalFileAttachment" ReadOnly="True" SortExpression="OrderDetTotalFileAttachment" UniqueName="OrderDetTotalFileAttachment" />
											<telerik:GridBoundColumn DataField="OrderDetStatusSpecialHandlingCode" FilterControlAltText="Filter OrderDetStatusSpecialHandlingCode column" HeaderText="OrderDetStatusSpecialHandlingCode" SortExpression="OrderDetStatusSpecialHandlingCode" UniqueName="OrderDetStatusSpecialHandlingCode" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetTotalSupplierInvited" DataType="System.Int32" FilterControlAltText="Filter OrderDetTotalSupplierInvited column" HeaderText="OrderDetTotalSupplierInvited" ReadOnly="True" SortExpression="OrderDetTotalSupplierInvited" UniqueName="OrderDetTotalSupplierInvited" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetTotalSupplierBidded" DataType="System.Int32" FilterControlAltText="Filter OrderDetTotalSupplierBidded column" HeaderText="OrderDetTotalSupplierBidded" ReadOnly="True" SortExpression="OrderDetTotalSupplierBidded" UniqueName="OrderDetTotalSupplierBidded" Visible="false" />
										</Columns>
										<EditFormSettings>
											<EditColumn FilterControlAltText="Filter EditCommandColumn column">
											</EditColumn>
										</EditFormSettings>
										<BatchEditingSettings EditType="Cell" />
										<PagerStyle PageSizeControlType="RadComboBox" />
									</MasterTableView>
									<ClientSettings AllowColumnsReorder="False" EnableRowHoverStyle="true" Selecting-AllowRowSelect="True" Selecting-UseClientSelectColumnOnly="True">
										<Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True" />
										<Scrolling AllowScroll="True" UseStaticHeaders="true" FrozenColumnsCount="2" SaveScrollPosition="true" ScrollHeight="" />
										<Resizing AllowColumnResize="true" />
									</ClientSettings>
									<PagerStyle PageSizeControlType="RadComboBox" />
								</telerik:RadGrid>
							</div>
						</NestedViewTemplate>
						<BatchEditingSettings EditType="Cell" />
						<PagerStyle PageSizeControlType="RadComboBox" />
					</MasterTableView>
					<ClientSettings AllowColumnsReorder="False" EnableRowHoverStyle="true" Selecting-AllowRowSelect="True" Selecting-UseClientSelectColumnOnly="True">
						<Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True" />
						<Scrolling AllowScroll="True" UseStaticHeaders="true" FrozenColumnsCount="2" SaveScrollPosition="true" ScrollHeight="" />
						<Resizing AllowColumnResize="true" />
					</ClientSettings>
					<PagerStyle PageSizeControlType="RadComboBox" />
				</telerik:RadGrid>
			</telerik:RadPageView>
			<telerik:RadPageView ID="pgQuotedRFQOpen" runat="server">
				<telerik:RadGrid ID="gvQuotedRFQOpen" runat="server" AllowPaging="true" AllowSorting="false" CellSpacing="0" DataSourceID="objOrderReq" GridLines="None" onitemcommand="gvList_ItemCommand" onitemdatabound="gvList_ItemDataBound" onprerender="gvList_PreRender" onsortcommand="gvList_SortCommand" Skin="Windows7" Width="100%">
					<ExportSettings>
						<Pdf>
							<PageHeader>
								<LeftCell Text="" />
								<MiddleCell Text="" />
								<RightCell Text="" />
							</PageHeader>
							<PageFooter>
								<LeftCell Text="" />
								<MiddleCell Text="" />
								<RightCell Text="" />
							</PageFooter>
						</Pdf>
					</ExportSettings>
					<MasterTableView AutoGenerateColumns="False" DataKeyNames="OrderCompany,OrderNo,OrderType,OrderSuffix" DataSourceID="objOrderReq" NoMasterRecordsText="No Quoted RFQ (Open for Bidding) Found" PagerStyle-AlwaysVisible="true" TableLayout="Fixed">
						<CommandItemSettings ExportToPdfText="Export to PDF" />
						<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
							<HeaderStyle Width="20px" />
						</RowIndicatorColumn>
						<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
							<HeaderStyle Width="20px" />
						</ExpandCollapseColumn>
						<Columns>
							<telerik:GridButtonColumn ButtonType="LinkButton" CommandName="Select" Text="Re-Bid" UniqueName="Bid">
								<HeaderStyle Width="70px" />
								<ItemStyle HorizontalAlign="Center" />
							</telerik:GridButtonColumn>
							<telerik:GridBoundColumn DataField="OrderNo" DataType="System.Double" FilterControlAltText="Filter OrderNo column" HeaderText="RFQ No." SortExpression="OrderNo" UniqueName="OrderNo">
								<HeaderStyle HorizontalAlign="Right" Width="100px" />
								<ItemStyle HorizontalAlign="Right" />
							</telerik:GridBoundColumn>
							<telerik:GridTemplateColumn DataField="OrderBuyerEmpName" FilterControlAltText="Filter OrderBuyerEmpName column" HeaderText="Buyer" SortExpression="OrderBuyerEmpName" UniqueName="OrderBuyerEmpName">
								<HeaderStyle Width="220px" />
								<ItemTemplate>
									<div class="columnEllipsis">
										<asp:Literal ID="litOrderBuyerEmpName" runat="server" Text='<%# Eval("OrderBuyerEmpName") %>' />
									</div>
								</ItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridBoundColumn DataField="OrderPublishedDate" DataFormatString="{0:dd MMM yyyy}" DataType="System.DateTime" FilterControlAltText="Filter OrderPublishedDate column" HeaderText="Date Published" SortExpression="OrderPublishedDate" UniqueName="OrderPublishedDate">
								<HeaderStyle HorizontalAlign="Right" Width="130px" />
								<ItemStyle HorizontalAlign="Right" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="OrderClosingDate" DataFormatString="{0:dd MMM yyyy HH:mm}" DataType="System.DateTime" FilterControlAltText="Filter OrderClosingDate column" HeaderText="Closing Date (GMT +3)" SortExpression="OrderClosingDate" UniqueName="OrderClosingDate">
								<HeaderStyle HorizontalAlign="Right" Width="140px" />
								<ItemStyle HorizontalAlign="Right" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="TotalOrderLine" DataType="System.Int32" FilterControlAltText="Filter TotalOrderLine column" HeaderText="Total Order Lines" ReadOnly="True" SortExpression="TotalOrderLine" UniqueName="TotalOrderLine">
								<HeaderStyle HorizontalAlign="Right" Width="120px" />
								<ItemStyle HorizontalAlign="Right" />
							</telerik:GridBoundColumn>
							<telerik:GridTemplateColumn DataField="OrderStatusDesc" FilterControlAltText="Filter OrderStatusDesc column" HeaderText="Status" SortExpression="OrderStatusDesc" UniqueName="OrderStatusDesc">
								<HeaderStyle Width="220px" />
								<ItemTemplate>
									<div class="columnEllipsis">
										<asp:Literal ID="litOrderStatusDesc" runat="server" Text='<%# Eval("OrderStatusDesc") %>' />
									</div>
								</ItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridBoundColumn DataField="OrderCompany" FilterControlAltText="Filter OrderCompany column" HeaderText="OrderCompany" ReadOnly="True" SortExpression="OrderCompany" UniqueName="OrderCompany" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderType" FilterControlAltText="Filter OrderType column" HeaderText="OrderType" ReadOnly="True" SortExpression="OrderType" UniqueName="OrderType" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderSuffix" FilterControlAltText="Filter OrderSuffix column" HeaderText="OrderSuffix" ReadOnly="True" SortExpression="OrderSuffix" UniqueName="OrderSuffix" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderDescription" FilterControlAltText="Filter OrderDescription column" HeaderText="Description" SortExpression="OrderDescription" UniqueName="OrderDescription" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderBuyerEmpNo" DataType="System.Int32" FilterControlAltText="Filter OrderBuyerEmpNo column" HeaderText="OrderBuyerEmpNo" SortExpression="OrderBuyerEmpNo" UniqueName="OrderBuyerEmpNo" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderBuyerEmpEmail" FilterControlAltText="Filter OrderBuyerEmpEmail column" HeaderText="OrderBuyerEmpEmail" SortExpression="OrderBuyerEmpEmail" UniqueName="OrderBuyerEmpEmail" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderCategory" FilterControlAltText="Filter OrderCategory column" HeaderText="OrderCategory" SortExpression="OrderCategory" UniqueName="OrderCategory" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderStatus" FilterControlAltText="Filter OrderStatus column" HeaderText="OrderStatus" SortExpression="OrderStatus" UniqueName="OrderStatus" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderSupplierParticipated" Display="false" FilterControlAltText="Filter OrderSupplierParticipated column" HeaderText="OrderSupplierParticipated" SortExpression="OrderSupplierParticipated" UniqueName="OrderSupplierParticipated" />
						</Columns>
						<EditFormSettings>
							<EditColumn FilterControlAltText="Filter EditCommandColumn column">
							</EditColumn>
						</EditFormSettings>
						<NestedViewTemplate>
							<div id="nested" class="SearchResult" style="padding: 15px 15px 15px 15px; table-layout: fixed; width: 100%;">
								<telerik:RadGrid ID="gvOrderDet" runat="server" Skin="Web20" Width="90%" CellSpacing="0" GridLines="None" onitemcommand="gvOrderDet_ItemCommand" onitemdatabound="gvOrderDet_ItemDataBound">
									<MasterTableView AutoGenerateColumns="False" NoMasterRecordsText="No Order Details Found" TableLayout="Fixed">
										<CommandItemSettings ExportToPdfText="Export to PDF" />
										<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
											<HeaderStyle Width="20px" />
										</RowIndicatorColumn>
										<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
											<HeaderStyle Width="20px" />
										</ExpandCollapseColumn>
										<Columns>
											<telerik:GridTemplateColumn DataField="OrderDetDesc" FilterControlAltText="Filter OrderDetDesc column" HeaderText="Description" UniqueName="OrderDetDesc">
												<HeaderStyle Width="200px" />
												<ItemTemplate>
													<div class="columnEllipsis">
														<asp:Label ID="lblOrderDetDesc" runat="server" Text='<%# Eval("OrderDetDesc")%>' ToolTip='<%# Eval("OrderDetDesc")%>' />
													</div>
												</ItemTemplate>
											</telerik:GridTemplateColumn>
											<telerik:GridBoundColumn DataField="OrderDetQuantity" DataType="System.Double" FilterControlAltText="Filter OrderDetQuantity column" HeaderText="Required Quantity" UniqueName="OrderDetQuantity">
												<HeaderStyle HorizontalAlign="Right" Width="120px" />
												<ItemStyle HorizontalAlign="Right" />
											</telerik:GridBoundColumn>
											<telerik:GridBoundColumn DataField="OrderDetUM" FilterControlAltText="Filter OrderDetUM column" HeaderText="Unit of Measure" UniqueName="OrderDetUM">
												<HeaderStyle HorizontalAlign="Center" Width="120px" />
												<ItemStyle HorizontalAlign="Center" />
											</telerik:GridBoundColumn>
											<telerik:GridButtonColumn ButtonType="ImageButton" CommandName="OrderDetAttachText" HeaderText="Extended Description" ImageUrl="~/includes/images/attach_small_f1.gif" UniqueName="OrderDetAttachText">
												<HeaderStyle HorizontalAlign="Center" Width="80px" />
												<ItemStyle HorizontalAlign="Center" />
											</telerik:GridButtonColumn>
											<telerik:GridButtonColumn ButtonType="ImageButton" CommandName="OrderDetAttachFile" HeaderText="File Attachment" ImageUrl="~/includes/images/attach_small_f1.gif" UniqueName="OrderDetAttachFile">
												<HeaderStyle HorizontalAlign="Center" Width="80px" />
												<ItemStyle HorizontalAlign="Center" />
											</telerik:GridButtonColumn>
											<telerik:GridBoundColumn DataField="OrderDetCompany" FilterControlAltText="Filter OrderDetCompany column" HeaderText="OrderDetCompany" ReadOnly="True" SortExpression="OrderDetCompany" UniqueName="OrderDetCompany" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetNo" DataType="System.Double" Display="false" FilterControlAltText="Filter OrderDetNo column" HeaderText="OrderDetNo" SortExpression="OrderDetNo" UniqueName="OrderDetNo" />
											<telerik:GridBoundColumn DataField="OrderDetType" FilterControlAltText="Filter OrderDetType column" HeaderText="OrderDetType" ReadOnly="True" SortExpression="OrderDetType" UniqueName="OrderDetType" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetSuffix" FilterControlAltText="Filter OrderDetSuffix column" HeaderText="OrderDetSuffix" ReadOnly="True" SortExpression="OrderDetSuffix" UniqueName="OrderDetSuffix" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetLineNo" DataType="System.Double" Display="false" FilterControlAltText="Filter OrderDetLineNo column" HeaderText="OrderDetLineNo" SortExpression="OrderDetLineNo" UniqueName="OrderDetLineNo" />
											<telerik:GridBoundColumn DataField="OrderDetLineType" Display="false" FilterControlAltText="Filter OrderDetLineType column" HeaderText="OrderDetLineType" SortExpression="OrderDetLineType" UniqueName="OrderDetLineType" />
											<telerik:GridBoundColumn DataField="OrderDetUNSPSC" FilterControlAltText="Filter OrderDetUNSPSC column" HeaderText="OrderDetUNSPSC" SortExpression="OrderDetUNSPSC" UniqueName="OrderDetUNSPSC" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetStockShortItemNo" DataType="System.Double" FilterControlAltText="Filter OrderDetStockShortItemNo column" HeaderText="OrderDetStockShortItemNo" SortExpression="OrderDetStockShortItemNo" UniqueName="OrderDetStockShortItemNo" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetStockLongItemNo" FilterControlAltText="Filter OrderDetStockLongItemNo column" HeaderText="OrderDetStockLongItemNo" SortExpression="OrderDetStockLongItemNo" UniqueName="OrderDetStockLongItemNo" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetStatus" FilterControlAltText="Filter OrderDetStatus column" HeaderText="OrderDetStatus" SortExpression="OrderDetStatus" UniqueName="OrderDetStatus" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetTotalExtAttachment" DataType="System.Int32" Display="false" FilterControlAltText="Filter OrderDetTotalExtAttachment column" HeaderText="OrderDetTotalExtAttachment" ReadOnly="True" SortExpression="OrderDetTotalExtAttachment" UniqueName="OrderDetTotalExtAttachment" />
											<telerik:GridBoundColumn DataField="OrderDetTotalFileAttachment" DataType="System.Int32" Display="false" FilterControlAltText="Filter OrderDetTotalFileAttachment column" HeaderText="OrderDetTotalFileAttachment" ReadOnly="True" SortExpression="OrderDetTotalFileAttachment" UniqueName="OrderDetTotalFileAttachment" />
											<telerik:GridBoundColumn DataField="OrderDetStatusSpecialHandlingCode" FilterControlAltText="Filter OrderDetStatusSpecialHandlingCode column" HeaderText="OrderDetStatusSpecialHandlingCode" SortExpression="OrderDetStatusSpecialHandlingCode" UniqueName="OrderDetStatusSpecialHandlingCode" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetTotalSupplierInvited" DataType="System.Int32" FilterControlAltText="Filter OrderDetTotalSupplierInvited column" HeaderText="OrderDetTotalSupplierInvited" ReadOnly="True" SortExpression="OrderDetTotalSupplierInvited" UniqueName="OrderDetTotalSupplierInvited" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetTotalSupplierBidded" DataType="System.Int32" FilterControlAltText="Filter OrderDetTotalSupplierBidded column" HeaderText="OrderDetTotalSupplierBidded" ReadOnly="True" SortExpression="OrderDetTotalSupplierBidded" UniqueName="OrderDetTotalSupplierBidded" Visible="false" />
										</Columns>
										<EditFormSettings>
											<EditColumn FilterControlAltText="Filter EditCommandColumn column">
											</EditColumn>
										</EditFormSettings>
										<BatchEditingSettings EditType="Cell" />
										<PagerStyle PageSizeControlType="RadComboBox" />
									</MasterTableView>
									<ClientSettings AllowColumnsReorder="False" EnableRowHoverStyle="true" Selecting-AllowRowSelect="True" Selecting-UseClientSelectColumnOnly="True">
										<Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True" />
										<Scrolling AllowScroll="True" UseStaticHeaders="true" FrozenColumnsCount="2" SaveScrollPosition="true" ScrollHeight="" />
										<Resizing AllowColumnResize="true" />
									</ClientSettings>
									<PagerStyle PageSizeControlType="RadComboBox" />
								</telerik:RadGrid>
							</div>
						</NestedViewTemplate>
						<BatchEditingSettings EditType="Cell" />
						<PagerStyle PageSizeControlType="RadComboBox" />
					</MasterTableView>
					<ClientSettings AllowColumnsReorder="False" EnableRowHoverStyle="true" Selecting-AllowRowSelect="True" Selecting-UseClientSelectColumnOnly="True">
						<Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True" />
						<Scrolling AllowScroll="True" UseStaticHeaders="true" FrozenColumnsCount="2" SaveScrollPosition="true" ScrollHeight="" />
						<Resizing AllowColumnResize="true" />
					</ClientSettings>
					<PagerStyle PageSizeControlType="RadComboBox" />
				</telerik:RadGrid>
			</telerik:RadPageView>
			<telerik:RadPageView ID="pgQuotedRFQClosed" runat="server">
				<telerik:RadGrid ID="gvQuotedRFQClosed" runat="server" AllowPaging="true" AllowSorting="false" CellSpacing="0" DataSourceID="objOrderReq" GridLines="None" onitemcommand="gvList_ItemCommand" onitemdatabound="gvList_ItemDataBound" onprerender="gvList_PreRender" onsortcommand="gvList_SortCommand" Skin="Windows7" Width="100%">
					<ExportSettings>
						<Pdf>
							<PageHeader>
								<LeftCell Text="" />
								<MiddleCell Text="" />
								<RightCell Text="" />
							</PageHeader>
							<PageFooter>
								<LeftCell Text="" />
								<MiddleCell Text="" />
								<RightCell Text="" />
							</PageFooter>
						</Pdf>
					</ExportSettings>
					<MasterTableView AutoGenerateColumns="False" DataKeyNames="OrderCompany,OrderNo,OrderType,OrderSuffix" DataSourceID="objOrderReq" NoMasterRecordsText="No Quoted RFQ (Closed for Bidding) Found" PagerStyle-AlwaysVisible="true" TableLayout="Fixed">
						<CommandItemSettings ExportToPdfText="Export to PDF" />
						<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
							<HeaderStyle Width="20px" />
						</RowIndicatorColumn>
						<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
							<HeaderStyle Width="20px" />
						</ExpandCollapseColumn>
						<Columns>
							<telerik:GridButtonColumn ButtonType="LinkButton" CommandName="Select" Text="View" UniqueName="Bid">
								<HeaderStyle Width="70px" />
								<ItemStyle HorizontalAlign="Center" />
							</telerik:GridButtonColumn>
							<telerik:GridBoundColumn DataField="OrderNo" DataType="System.Double" FilterControlAltText="Filter OrderNo column" HeaderText="RFQ No." SortExpression="OrderNo" UniqueName="OrderNo">
								<HeaderStyle HorizontalAlign="Right" Width="100px" />
								<ItemStyle HorizontalAlign="Right" />
							</telerik:GridBoundColumn>
							<telerik:GridTemplateColumn DataField="OrderBuyerEmpName" FilterControlAltText="Filter OrderBuyerEmpName column" HeaderText="Buyer" SortExpression="OrderBuyerEmpName" UniqueName="OrderBuyerEmpName">
								<HeaderStyle Width="220px" />
								<ItemTemplate>
									<div class="columnEllipsis">
										<asp:Literal ID="litOrderBuyerEmpName" runat="server" Text='<%# Eval("OrderBuyerEmpName") %>' />
									</div>
								</ItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridBoundColumn DataField="OrderPublishedDate" DataFormatString="{0:dd MMM yyyy}" DataType="System.DateTime" FilterControlAltText="Filter OrderPublishedDate column" HeaderText="Date Published" SortExpression="OrderPublishedDate" UniqueName="OrderPublishedDate">
								<HeaderStyle HorizontalAlign="Right" Width="130px" />
								<ItemStyle HorizontalAlign="Right" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="OrderClosingDate" DataFormatString="{0:dd MMM yyyy HH:mm}" DataType="System.DateTime" FilterControlAltText="Filter OrderClosingDate column" HeaderText="Closing Date (GMT +3)" SortExpression="OrderClosingDate" UniqueName="OrderClosingDate">
								<HeaderStyle HorizontalAlign="Right" Width="140px" />
								<ItemStyle HorizontalAlign="Right" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="TotalOrderLine" DataType="System.Int32" FilterControlAltText="Filter TotalOrderLine column" HeaderText="Total Order Lines" ReadOnly="True" SortExpression="TotalOrderLine" UniqueName="TotalOrderLine">
								<HeaderStyle HorizontalAlign="Right" Width="120px" />
								<ItemStyle HorizontalAlign="Right" />
							</telerik:GridBoundColumn>
							<telerik:GridTemplateColumn DataField="OrderStatusDesc" FilterControlAltText="Filter OrderStatusDesc column" HeaderText="Status" SortExpression="OrderStatusDesc" UniqueName="OrderStatusDesc">
								<HeaderStyle Width="220px" />
								<ItemTemplate>
									<div class="columnEllipsis">
										<asp:Literal ID="litOrderStatusDesc" runat="server" Text='<%# Eval("OrderStatusDesc") %>' />
									</div>
								</ItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridBoundColumn DataField="OrderCompany" FilterControlAltText="Filter OrderCompany column" HeaderText="OrderCompany" ReadOnly="True" SortExpression="OrderCompany" UniqueName="OrderCompany" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderType" FilterControlAltText="Filter OrderType column" HeaderText="OrderType" ReadOnly="True" SortExpression="OrderType" UniqueName="OrderType" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderSuffix" FilterControlAltText="Filter OrderSuffix column" HeaderText="OrderSuffix" ReadOnly="True" SortExpression="OrderSuffix" UniqueName="OrderSuffix" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderDescription" FilterControlAltText="Filter OrderDescription column" HeaderText="Description" SortExpression="OrderDescription" UniqueName="OrderDescription" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderBuyerEmpNo" DataType="System.Int32" FilterControlAltText="Filter OrderBuyerEmpNo column" HeaderText="OrderBuyerEmpNo" SortExpression="OrderBuyerEmpNo" UniqueName="OrderBuyerEmpNo" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderBuyerEmpEmail" FilterControlAltText="Filter OrderBuyerEmpEmail column" HeaderText="OrderBuyerEmpEmail" SortExpression="OrderBuyerEmpEmail" UniqueName="OrderBuyerEmpEmail" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderCategory" FilterControlAltText="Filter OrderCategory column" HeaderText="OrderCategory" SortExpression="OrderCategory" UniqueName="OrderCategory" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderStatus" FilterControlAltText="Filter OrderStatus column" HeaderText="OrderStatus" SortExpression="OrderStatus" UniqueName="OrderStatus" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderSupplierParticipated" Display="false" FilterControlAltText="Filter OrderSupplierParticipated column" HeaderText="OrderSupplierParticipated" SortExpression="OrderSupplierParticipated" UniqueName="OrderSupplierParticipated" />
						</Columns>
						<EditFormSettings>
							<EditColumn FilterControlAltText="Filter EditCommandColumn column">
							</EditColumn>
						</EditFormSettings>
						<NestedViewTemplate>
							<div id="nested" class="SearchResult" style="padding: 15px 15px 15px 15px; table-layout: fixed; width: 100%;">
								<telerik:RadGrid ID="gvOrderDet" runat="server" Skin="Web20" Width="90%" CellSpacing="0" GridLines="None" onitemcommand="gvOrderDet_ItemCommand" onitemdatabound="gvOrderDet_ItemDataBound">
									<MasterTableView AutoGenerateColumns="False" NoMasterRecordsText="No Order Details Found" TableLayout="Fixed">
										<CommandItemSettings ExportToPdfText="Export to PDF" />
										<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
											<HeaderStyle Width="20px" />
										</RowIndicatorColumn>
										<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
											<HeaderStyle Width="20px" />
										</ExpandCollapseColumn>
										<Columns>
											<telerik:GridTemplateColumn DataField="OrderDetDesc" FilterControlAltText="Filter OrderDetDesc column" HeaderText="Description" UniqueName="OrderDetDesc">
												<HeaderStyle Width="200px" />
												<ItemTemplate>
													<div class="columnEllipsis">
														<asp:Label ID="lblOrderDetDesc" runat="server" Text='<%# Eval("OrderDetDesc")%>' ToolTip='<%# Eval("OrderDetDesc")%>' />
													</div>
												</ItemTemplate>
											</telerik:GridTemplateColumn>
											<telerik:GridBoundColumn DataField="OrderDetQuantity" DataType="System.Double" FilterControlAltText="Filter OrderDetQuantity column" HeaderText="Required Quantity" UniqueName="OrderDetQuantity">
												<HeaderStyle HorizontalAlign="Right" Width="120px" />
												<ItemStyle HorizontalAlign="Right" />
											</telerik:GridBoundColumn>
											<telerik:GridBoundColumn DataField="OrderDetUM" FilterControlAltText="Filter OrderDetUM column" HeaderText="Unit of Measure" UniqueName="OrderDetUM">
												<HeaderStyle HorizontalAlign="Center" Width="120px" />
												<ItemStyle HorizontalAlign="Center" />
											</telerik:GridBoundColumn>
											<telerik:GridButtonColumn ButtonType="ImageButton" CommandName="OrderDetAttachText" HeaderText="Extended Description" ImageUrl="~/includes/images/attach_small_f1.gif" UniqueName="OrderDetAttachText">
												<HeaderStyle HorizontalAlign="Center" Width="80px" />
												<ItemStyle HorizontalAlign="Center" />
											</telerik:GridButtonColumn>
											<telerik:GridButtonColumn ButtonType="ImageButton" CommandName="OrderDetAttachFile" HeaderText="File Attachment" ImageUrl="~/includes/images/attach_small_f1.gif" UniqueName="OrderDetAttachFile">
												<HeaderStyle HorizontalAlign="Center" Width="80px" />
												<ItemStyle HorizontalAlign="Center" />
											</telerik:GridButtonColumn>
											<telerik:GridBoundColumn DataField="OrderDetCompany" FilterControlAltText="Filter OrderDetCompany column" HeaderText="OrderDetCompany" ReadOnly="True" SortExpression="OrderDetCompany" UniqueName="OrderDetCompany" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetNo" DataType="System.Double" Display="false" FilterControlAltText="Filter OrderDetNo column" HeaderText="OrderDetNo" SortExpression="OrderDetNo" UniqueName="OrderDetNo" />
											<telerik:GridBoundColumn DataField="OrderDetType" FilterControlAltText="Filter OrderDetType column" HeaderText="OrderDetType" ReadOnly="True" SortExpression="OrderDetType" UniqueName="OrderDetType" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetSuffix" FilterControlAltText="Filter OrderDetSuffix column" HeaderText="OrderDetSuffix" ReadOnly="True" SortExpression="OrderDetSuffix" UniqueName="OrderDetSuffix" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetLineNo" DataType="System.Double" Display="false" FilterControlAltText="Filter OrderDetLineNo column" HeaderText="OrderDetLineNo" SortExpression="OrderDetLineNo" UniqueName="OrderDetLineNo" />
											<telerik:GridBoundColumn DataField="OrderDetLineType" Display="false" FilterControlAltText="Filter OrderDetLineType column" HeaderText="OrderDetLineType" SortExpression="OrderDetLineType" UniqueName="OrderDetLineType" />
											<telerik:GridBoundColumn DataField="OrderDetUNSPSC" FilterControlAltText="Filter OrderDetUNSPSC column" HeaderText="OrderDetUNSPSC" SortExpression="OrderDetUNSPSC" UniqueName="OrderDetUNSPSC" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetStockShortItemNo" DataType="System.Double" FilterControlAltText="Filter OrderDetStockShortItemNo column" HeaderText="OrderDetStockShortItemNo" SortExpression="OrderDetStockShortItemNo" UniqueName="OrderDetStockShortItemNo" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetStockLongItemNo" FilterControlAltText="Filter OrderDetStockLongItemNo column" HeaderText="OrderDetStockLongItemNo" SortExpression="OrderDetStockLongItemNo" UniqueName="OrderDetStockLongItemNo" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetStatus" FilterControlAltText="Filter OrderDetStatus column" HeaderText="OrderDetStatus" SortExpression="OrderDetStatus" UniqueName="OrderDetStatus" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetTotalExtAttachment" DataType="System.Int32" Display="false" FilterControlAltText="Filter OrderDetTotalExtAttachment column" HeaderText="OrderDetTotalExtAttachment" ReadOnly="True" SortExpression="OrderDetTotalExtAttachment" UniqueName="OrderDetTotalExtAttachment" />
											<telerik:GridBoundColumn DataField="OrderDetTotalFileAttachment" DataType="System.Int32" Display="false" FilterControlAltText="Filter OrderDetTotalFileAttachment column" HeaderText="OrderDetTotalFileAttachment" ReadOnly="True" SortExpression="OrderDetTotalFileAttachment" UniqueName="OrderDetTotalFileAttachment" />
											<telerik:GridBoundColumn DataField="OrderDetStatusSpecialHandlingCode" FilterControlAltText="Filter OrderDetStatusSpecialHandlingCode column" HeaderText="OrderDetStatusSpecialHandlingCode" SortExpression="OrderDetStatusSpecialHandlingCode" UniqueName="OrderDetStatusSpecialHandlingCode" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetTotalSupplierInvited" DataType="System.Int32" FilterControlAltText="Filter OrderDetTotalSupplierInvited column" HeaderText="OrderDetTotalSupplierInvited" ReadOnly="True" SortExpression="OrderDetTotalSupplierInvited" UniqueName="OrderDetTotalSupplierInvited" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetTotalSupplierBidded" DataType="System.Int32" FilterControlAltText="Filter OrderDetTotalSupplierBidded column" HeaderText="OrderDetTotalSupplierBidded" ReadOnly="True" SortExpression="OrderDetTotalSupplierBidded" UniqueName="OrderDetTotalSupplierBidded" Visible="false" />
										</Columns>
										<EditFormSettings>
											<EditColumn FilterControlAltText="Filter EditCommandColumn column">
											</EditColumn>
										</EditFormSettings>
										<BatchEditingSettings EditType="Cell" />
										<PagerStyle PageSizeControlType="RadComboBox" />
									</MasterTableView>
									<ClientSettings AllowColumnsReorder="False" EnableRowHoverStyle="true" Selecting-AllowRowSelect="True" Selecting-UseClientSelectColumnOnly="True">
										<Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True" />
										<Scrolling AllowScroll="True" UseStaticHeaders="true" FrozenColumnsCount="2" SaveScrollPosition="true" ScrollHeight="" />
										<Resizing AllowColumnResize="true" />
									</ClientSettings>
									<PagerStyle PageSizeControlType="RadComboBox" />
								</telerik:RadGrid>
							</div>
						</NestedViewTemplate>
						<BatchEditingSettings EditType="Cell" />
						<PagerStyle PageSizeControlType="RadComboBox" />
					</MasterTableView>
					<ClientSettings AllowColumnsReorder="False" EnableRowHoverStyle="true" Selecting-AllowRowSelect="True" Selecting-UseClientSelectColumnOnly="True">
						<Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True" />
						<Scrolling AllowScroll="True" UseStaticHeaders="true" FrozenColumnsCount="2" SaveScrollPosition="true" ScrollHeight="" />
						<Resizing AllowColumnResize="true" />
					</ClientSettings>
					<PagerStyle PageSizeControlType="RadComboBox" />
				</telerik:RadGrid>
			</telerik:RadPageView>
			<telerik:RadPageView ID="pgClosedRFQ" runat="server">
				<telerik:RadGrid ID="gvClosedRFQ" runat="server" AllowPaging="true" AllowSorting="false" CellSpacing="0" DataSourceID="objOrderReq" GridLines="None" onitemcommand="gvList_ItemCommand" onitemdatabound="gvList_ItemDataBound" onprerender="gvList_PreRender" onsortcommand="gvList_SortCommand" Skin="Windows7" Width="100%">
					<ExportSettings>
						<Pdf>
							<PageHeader>
								<LeftCell Text="" />
								<MiddleCell Text="" />
								<RightCell Text="" />
							</PageHeader>
							<PageFooter>
								<LeftCell Text="" />
								<MiddleCell Text="" />
								<RightCell Text="" />
							</PageFooter>
						</Pdf>
					</ExportSettings>
					<MasterTableView AutoGenerateColumns="False" DataKeyNames="OrderCompany,OrderNo,OrderType,OrderSuffix" DataSourceID="objOrderReq" NoMasterRecordsText="No Closed RFQ Found" PagerStyle-AlwaysVisible="true" TableLayout="Fixed">
						<CommandItemSettings ExportToPdfText="Export to PDF" />
						<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
							<HeaderStyle Width="20px" />
						</RowIndicatorColumn>
						<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
							<HeaderStyle Width="20px" />
						</ExpandCollapseColumn>
						<Columns>
							<telerik:GridButtonColumn ButtonType="LinkButton" CommandName="Select" Text="View" UniqueName="Bid">
								<HeaderStyle Width="70px" />
								<ItemStyle HorizontalAlign="Center" />
							</telerik:GridButtonColumn>
							<telerik:GridBoundColumn DataField="OrderNo" DataType="System.Double" FilterControlAltText="Filter OrderNo column" HeaderText="RFQ No." SortExpression="OrderNo" UniqueName="OrderNo">
								<HeaderStyle HorizontalAlign="Right" Width="100px" />
								<ItemStyle HorizontalAlign="Right" />
							</telerik:GridBoundColumn>
							<telerik:GridTemplateColumn DataField="OrderBuyerEmpName" FilterControlAltText="Filter OrderBuyerEmpName column" HeaderText="Buyer" SortExpression="OrderBuyerEmpName" UniqueName="OrderBuyerEmpName">
								<HeaderStyle Width="220px" />
								<ItemTemplate>
									<div class="columnEllipsis">
										<asp:Literal ID="litOrderBuyerEmpName" runat="server" Text='<%# Eval("OrderBuyerEmpName") %>' />
									</div>
								</ItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridBoundColumn DataField="OrderPublishedDate" DataFormatString="{0:dd MMM yyyy}" DataType="System.DateTime" FilterControlAltText="Filter OrderPublishedDate column" HeaderText="Date Published" SortExpression="OrderPublishedDate" UniqueName="OrderPublishedDate">
								<HeaderStyle HorizontalAlign="Right" Width="130px" />
								<ItemStyle HorizontalAlign="Right" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="OrderClosingDate" DataFormatString="{0:dd MMM yyyy HH:mm}" DataType="System.DateTime" FilterControlAltText="Filter OrderClosingDate column" HeaderText="Closing Date (GMT +3)" SortExpression="OrderClosingDate" UniqueName="OrderClosingDate">
								<HeaderStyle HorizontalAlign="Right" Width="140px" />
								<ItemStyle HorizontalAlign="Right" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="TotalOrderLine" DataType="System.Int32" FilterControlAltText="Filter TotalOrderLine column" HeaderText="Total Order Lines" ReadOnly="True" SortExpression="TotalOrderLine" UniqueName="TotalOrderLine">
								<HeaderStyle HorizontalAlign="Right" Width="120px" />
								<ItemStyle HorizontalAlign="Right" />
							</telerik:GridBoundColumn>
							<telerik:GridTemplateColumn DataField="OrderStatusDesc" FilterControlAltText="Filter OrderStatusDesc column" HeaderText="Status" SortExpression="OrderStatusDesc" UniqueName="OrderStatusDesc">
								<HeaderStyle Width="220px" />
								<ItemTemplate>
									<div class="columnEllipsis">
										<asp:Literal ID="litOrderStatusDesc" runat="server" Text='<%# Eval("OrderStatusDesc") %>' />
									</div>
								</ItemTemplate>
							</telerik:GridTemplateColumn>
							<telerik:GridBoundColumn DataField="OrderCompany" FilterControlAltText="Filter OrderCompany column" HeaderText="OrderCompany" ReadOnly="True" SortExpression="OrderCompany" UniqueName="OrderCompany" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderType" FilterControlAltText="Filter OrderType column" HeaderText="OrderType" ReadOnly="True" SortExpression="OrderType" UniqueName="OrderType" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderSuffix" FilterControlAltText="Filter OrderSuffix column" HeaderText="OrderSuffix" ReadOnly="True" SortExpression="OrderSuffix" UniqueName="OrderSuffix" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderDescription" FilterControlAltText="Filter OrderDescription column" HeaderText="Description" SortExpression="OrderDescription" UniqueName="OrderDescription" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderBuyerEmpNo" DataType="System.Int32" FilterControlAltText="Filter OrderBuyerEmpNo column" HeaderText="OrderBuyerEmpNo" SortExpression="OrderBuyerEmpNo" UniqueName="OrderBuyerEmpNo" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderBuyerEmpEmail" FilterControlAltText="Filter OrderBuyerEmpEmail column" HeaderText="OrderBuyerEmpEmail" SortExpression="OrderBuyerEmpEmail" UniqueName="OrderBuyerEmpEmail" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderCategory" FilterControlAltText="Filter OrderCategory column" HeaderText="OrderCategory" SortExpression="OrderCategory" UniqueName="OrderCategory" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderStatus" FilterControlAltText="Filter OrderStatus column" HeaderText="OrderStatus" SortExpression="OrderStatus" UniqueName="OrderStatus" Visible="false" />
							<telerik:GridBoundColumn DataField="OrderSupplierParticipated" Display="false" FilterControlAltText="Filter OrderSupplierParticipated column" HeaderText="OrderSupplierParticipated" SortExpression="OrderSupplierParticipated" UniqueName="OrderSupplierParticipated" />
						</Columns>
						<EditFormSettings>
							<EditColumn FilterControlAltText="Filter EditCommandColumn column">
							</EditColumn>
						</EditFormSettings>
						<NestedViewTemplate>
							<div id="nested" class="SearchResult" style="padding: 15px 15px 15px 15px; table-layout: fixed; width: 100%;">
								<telerik:RadGrid ID="gvOrderDet" runat="server" Skin="Web20" Width="90%" CellSpacing="0" GridLines="None" onitemcommand="gvOrderDet_ItemCommand" onitemdatabound="gvOrderDet_ItemDataBound">
									<MasterTableView AutoGenerateColumns="False" NoMasterRecordsText="No Order Details Found" TableLayout="Fixed">
										<CommandItemSettings ExportToPdfText="Export to PDF" />
										<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
											<HeaderStyle Width="20px" />
										</RowIndicatorColumn>
										<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
											<HeaderStyle Width="20px" />
										</ExpandCollapseColumn>
										<Columns>
											<telerik:GridTemplateColumn DataField="OrderDetDesc" FilterControlAltText="Filter OrderDetDesc column" HeaderText="Description" UniqueName="OrderDetDesc">
												<HeaderStyle Width="200px" />
												<ItemTemplate>
													<div class="columnEllipsis">
														<asp:Label ID="lblOrderDetDesc" runat="server" Text='<%# Eval("OrderDetDesc")%>' ToolTip='<%# Eval("OrderDetDesc")%>' />
													</div>
												</ItemTemplate>
											</telerik:GridTemplateColumn>
											<telerik:GridBoundColumn DataField="OrderDetQuantity" DataType="System.Double" FilterControlAltText="Filter OrderDetQuantity column" HeaderText="Required Quantity" UniqueName="OrderDetQuantity">
												<HeaderStyle HorizontalAlign="Right" Width="120px" />
												<ItemStyle HorizontalAlign="Right" />
											</telerik:GridBoundColumn>
											<telerik:GridBoundColumn DataField="OrderDetUM" FilterControlAltText="Filter OrderDetUM column" HeaderText="Unit of Measure" UniqueName="OrderDetUM">
												<HeaderStyle HorizontalAlign="Center" Width="120px" />
												<ItemStyle HorizontalAlign="Center" />
											</telerik:GridBoundColumn>
											<telerik:GridButtonColumn ButtonType="ImageButton" CommandName="OrderDetAttachText" HeaderText="Extended Description" ImageUrl="~/includes/images/attach_small_f1.gif" UniqueName="OrderDetAttachText">
												<HeaderStyle HorizontalAlign="Center" Width="80px" />
												<ItemStyle HorizontalAlign="Center" />
											</telerik:GridButtonColumn>
											<telerik:GridButtonColumn ButtonType="ImageButton" CommandName="OrderDetAttachFile" HeaderText="File Attachment" ImageUrl="~/includes/images/attach_small_f1.gif" UniqueName="OrderDetAttachFile">
												<HeaderStyle HorizontalAlign="Center" Width="80px" />
												<ItemStyle HorizontalAlign="Center" />
											</telerik:GridButtonColumn>
											<telerik:GridBoundColumn DataField="OrderDetCompany" FilterControlAltText="Filter OrderDetCompany column" HeaderText="OrderDetCompany" ReadOnly="True" SortExpression="OrderDetCompany" UniqueName="OrderDetCompany" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetNo" DataType="System.Double" Display="false" FilterControlAltText="Filter OrderDetNo column" HeaderText="OrderDetNo" SortExpression="OrderDetNo" UniqueName="OrderDetNo" />
											<telerik:GridBoundColumn DataField="OrderDetType" FilterControlAltText="Filter OrderDetType column" HeaderText="OrderDetType" ReadOnly="True" SortExpression="OrderDetType" UniqueName="OrderDetType" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetSuffix" FilterControlAltText="Filter OrderDetSuffix column" HeaderText="OrderDetSuffix" ReadOnly="True" SortExpression="OrderDetSuffix" UniqueName="OrderDetSuffix" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetLineNo" DataType="System.Double" Display="false" FilterControlAltText="Filter OrderDetLineNo column" HeaderText="OrderDetLineNo" SortExpression="OrderDetLineNo" UniqueName="OrderDetLineNo" />
											<telerik:GridBoundColumn DataField="OrderDetLineType" Display="false" FilterControlAltText="Filter OrderDetLineType column" HeaderText="OrderDetLineType" SortExpression="OrderDetLineType" UniqueName="OrderDetLineType" />
											<telerik:GridBoundColumn DataField="OrderDetUNSPSC" FilterControlAltText="Filter OrderDetUNSPSC column" HeaderText="OrderDetUNSPSC" SortExpression="OrderDetUNSPSC" UniqueName="OrderDetUNSPSC" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetStockShortItemNo" DataType="System.Double" FilterControlAltText="Filter OrderDetStockShortItemNo column" HeaderText="OrderDetStockShortItemNo" SortExpression="OrderDetStockShortItemNo" UniqueName="OrderDetStockShortItemNo" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetStockLongItemNo" FilterControlAltText="Filter OrderDetStockLongItemNo column" HeaderText="OrderDetStockLongItemNo" SortExpression="OrderDetStockLongItemNo" UniqueName="OrderDetStockLongItemNo" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetStatus" FilterControlAltText="Filter OrderDetStatus column" HeaderText="OrderDetStatus" SortExpression="OrderDetStatus" UniqueName="OrderDetStatus" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetTotalExtAttachment" DataType="System.Int32" Display="false" FilterControlAltText="Filter OrderDetTotalExtAttachment column" HeaderText="OrderDetTotalExtAttachment" ReadOnly="True" SortExpression="OrderDetTotalExtAttachment" UniqueName="OrderDetTotalExtAttachment" />
											<telerik:GridBoundColumn DataField="OrderDetTotalFileAttachment" DataType="System.Int32" Display="false" FilterControlAltText="Filter OrderDetTotalFileAttachment column" HeaderText="OrderDetTotalFileAttachment" ReadOnly="True" SortExpression="OrderDetTotalFileAttachment" UniqueName="OrderDetTotalFileAttachment" />
											<telerik:GridBoundColumn DataField="OrderDetStatusSpecialHandlingCode" FilterControlAltText="Filter OrderDetStatusSpecialHandlingCode column" HeaderText="OrderDetStatusSpecialHandlingCode" SortExpression="OrderDetStatusSpecialHandlingCode" UniqueName="OrderDetStatusSpecialHandlingCode" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetTotalSupplierInvited" DataType="System.Int32" FilterControlAltText="Filter OrderDetTotalSupplierInvited column" HeaderText="OrderDetTotalSupplierInvited" ReadOnly="True" SortExpression="OrderDetTotalSupplierInvited" UniqueName="OrderDetTotalSupplierInvited" Visible="false" />
											<telerik:GridBoundColumn DataField="OrderDetTotalSupplierBidded" DataType="System.Int32" FilterControlAltText="Filter OrderDetTotalSupplierBidded column" HeaderText="OrderDetTotalSupplierBidded" ReadOnly="True" SortExpression="OrderDetTotalSupplierBidded" UniqueName="OrderDetTotalSupplierBidded" Visible="false" />
										</Columns>
										<EditFormSettings>
											<EditColumn FilterControlAltText="Filter EditCommandColumn column">
											</EditColumn>
										</EditFormSettings>
										<BatchEditingSettings EditType="Cell" />
										<PagerStyle PageSizeControlType="RadComboBox" />
									</MasterTableView>
									<ClientSettings AllowColumnsReorder="False" EnableRowHoverStyle="true" Selecting-AllowRowSelect="True" Selecting-UseClientSelectColumnOnly="True">
										<Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True" />
										<Scrolling AllowScroll="True" UseStaticHeaders="true" FrozenColumnsCount="2" SaveScrollPosition="true" ScrollHeight="" />
										<Resizing AllowColumnResize="true" />
									</ClientSettings>
									<PagerStyle PageSizeControlType="RadComboBox" />
								</telerik:RadGrid>
							</div>
						</NestedViewTemplate>
						<BatchEditingSettings EditType="Cell" />
						<PagerStyle PageSizeControlType="RadComboBox" />
					</MasterTableView>
					<ClientSettings AllowColumnsReorder="False" EnableRowHoverStyle="true" Selecting-AllowRowSelect="True" Selecting-UseClientSelectColumnOnly="True">
						<Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True" />
						<Scrolling AllowScroll="True" UseStaticHeaders="true" FrozenColumnsCount="2" SaveScrollPosition="true" ScrollHeight="" />
						<Resizing AllowColumnResize="true" />
					</ClientSettings>
					<PagerStyle PageSizeControlType="RadComboBox" />
				</telerik:RadGrid>
			</telerik:RadPageView>
		</telerik:RadMultiPage>
	</asp:Panel>
	<asp:Panel ID="panObjDS" runat="server">
		<asp:ObjectDataSource ID="objOrderReq" runat="server" EnablePaging="True" OldValuesParameterFormatString="" SelectCountMethod="GetOrderRequisitionTotal" SelectMethod="GetOrderRequisition" TypeName="GARMCO.AMS.B2B.Admin.DAL.OrderRequisitionRepository">
			<SelectParameters>
				<asp:Parameter DefaultValue="0" Name="mode" Type="Byte" />
				<asp:Parameter DefaultValue="0" Name="orderNo" Type="Double" />
				<asp:Parameter DefaultValue="" Name="orderCloseDateStart" Type="DateTime" />
				<asp:Parameter DefaultValue="" Name="orderCloseDateEnd" Type="DateTime" />
				<asp:Parameter DefaultValue="0" Name="orderOriginatorEmpNo" Type="Int32" />
				<asp:Parameter DefaultValue="" Name="orderOriginatorEmpName" Type="String" />
				<asp:Parameter DefaultValue="0" Name="orderBuyerEmpNo" Type="Int32" />
				<asp:Parameter DefaultValue="" Name="orderBuyerEmpName" Type="String" />
				<asp:Parameter DefaultValue="0" Name="orderSupplierNo" Type="Int32" />
				<asp:Parameter DefaultValue="" Name="orderSupplierName" Type="String" />
				<asp:Parameter DefaultValue="" Name="orderPriority" Type="String" />
				<asp:Parameter DefaultValue="200" Name="orderStatus" Type="String" />
				<asp:Parameter DefaultValue="" Name="orderDesc" Type="String" />
				<asp:Parameter DefaultValue="2" Name="orderCurrentlyAssignedTo" Type="Byte" />
				<asp:Parameter DefaultValue="0" Name="orderCurrentUser" Type="Int32" />
				<asp:Parameter DefaultValue="" Name="sort" Type="String" />
			</SelectParameters>
		</asp:ObjectDataSource>
		<asp:ObjectDataSource ID="objOrderReqDet" runat="server" OldValuesParameterFormatString="" SelectMethod="GetOrderRequisitionDetail" TypeName="GARMCO.AMS.B2B.Admin.DAL.OrderRequisitionDetailRepository" OnSelected="objOrderReqDet_Selected">
			<SelectParameters>
				<asp:Parameter DefaultValue="0" Name="orderDetNo" Type="Double" />
			</SelectParameters>
		</asp:ObjectDataSource>
	</asp:Panel>
	<%--
	<telerik:RadAjaxManagerProxy ID="ajaxProxy" runat="server">
		<AjaxSettings>
			<telerik:AjaxSetting AjaxControlID="btnSearch">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="panResult" LoadingPanelID="loadingPanel" UpdatePanelCssClass="" />
					<telerik:AjaxUpdatedControl ControlID="panObjDS" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="btnReset">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="panSearch" LoadingPanelID="loadingPanel" UpdatePanelCssClass="" />      
                    <telerik:AjaxUpdatedControl ControlID="panResult" />
					<telerik:AjaxUpdatedControl ControlID="panObjDS" />              
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="panResult">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="panResult" LoadingPanelID="loadingPanel" UpdatePanelCssClass="" />
					<telerik:AjaxUpdatedControl ControlID="calShared" />
				</UpdatedControls>
			</telerik:AjaxSetting>
		</AjaxSettings>
	</telerik:RadAjaxManagerProxy>
	<telerik:RadAjaxLoadingPanel ID="loadingPanel" runat="server" Skin="Windows7" />
	--%>
</asp:Content>
