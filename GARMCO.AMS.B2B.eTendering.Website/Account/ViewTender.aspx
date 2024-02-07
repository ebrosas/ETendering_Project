<%@ Page
    AutoEventWireup="false"
    CodeBehind="ViewTender.aspx.cs"
    Inherits="Tendering.Presentation.ViewTender"
    Language="C#"
    MasterPageFile="~/CommonObject/Site.Master" %>

<%@ MasterType VirtualPath="~/CommonObject/Site.Master" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="main" runat="server" ContentPlaceHolderID="mainContent">
    <script src="../Scripts/rfqattachment.js"></script>
    <asp:Panel ID="panEntry" runat="server" CssClass="GroupLayoutOrder" GroupingText="Call For Tenders {0}">
        <div style="margin: 0 0 20px 0; padding: 0 20px 0 20px;">
            <div>
                <label class="LabelBold" style="display: inline-block; font-weight: bold; width: 118px;">Currency</label>
                <span style="line-height: 25px;">
                    <asp:Literal ID="currency" runat="server" />
                </span>
            </div>
            <div>
                <label class="LabelBold" style="display: inline-block; font-weight: bold; margin: 0 2px 0 0; width: 118px;">Delivery Point</label>
                <span style="line-height: 25px;">
                    <asp:Literal ID="deliveryPoint" runat="server" />
                </span>
            </div>
            <div>
                <label class="LabelBold" style="display: inline-block; font-weight: bold; width: 118px;">Delivery Terms</label>
                <span style="line-height: 25px;">
                    <asp:Literal ID="deliveryTerms" runat="server" />
                </span>
            </div>
            <div>
                <label class="LabelBold" style="display: inline-block; font-weight: bold; margin: 0 2px 0 0; width: 118px;">Delivery Time (days)</label>
                <span style="line-height: 25px;">
                    <asp:Literal ID="deliveryTime" runat="server" />
                </span>
            </div>
            <div>
                <label class="LabelBold" style="display: inline-block; font-weight: bold; width: 118px;">Valid Until</label>
                <span style="line-height: 25px;">
                    <asp:Literal ID="expiryDate" runat="server" />
                </span>
            </div>
            <div>
                <label class="LabelBold" style="display: inline-block; font-weight: bold; margin: 0 2px 0 0; width: 118px;">Payment Terms</label>
                <span style="line-height: 25px;">
                    <asp:Literal ID="paymentTerms" runat="server" />
                </span>
            </div>
        </div>
        <div class="grid" style="margin: 0 0 20px 0; overflow-x: scroll; padding: 0; width: 1880px;">
            <telerik:RadGrid ID="responses" runat="server" Skin="Windows7">
                <MasterTableView AutoGenerateColumns="false" DataKeyNames="Requirement.Number" Name="offers" HierarchyLoadMode="Client" TableLayout="Fixed">
                    <DetailTables>
                        <telerik:GridTableView AutoGenerateColumns="false" DataKeyNames="Description" Name="options" TableLayout="Fixed">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Description">
                                    <ItemTemplate>
                                        <%# Eval("Description") %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Manufacturer Code">
                                    <ItemTemplate>
                                        <%# Eval("ManufacturerCode") %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="118px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Quantity">
                                    <ItemTemplate>
                                        <%# Eval("Quantity") %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="118px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Supplier Code">
                                    <ItemTemplate>
                                        <%# Eval("SupplierCode") %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="118px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Unit Cost">
                                    <ItemTemplate>
                                        <%# Eval("UnitCost") %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="118px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Unit Of Measure">
                                    <ItemTemplate>
                                        <%# Eval("UnitOfMeasure.Value") %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="176" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Currency">
                                    <ItemTemplate>
                                        <%# Eval("Currency.Value") %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="176" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Delivery Point">
                                    <ItemTemplate>
                                        <%# Eval("DeliveryPoint") %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="118px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Delivery Terms">
                                    <ItemTemplate>
                                        <%# Eval("DeliveryTerms.Value") %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="176" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Delivery Time (days)">
                                    <ItemTemplate>
                                        <%# Eval("DeliveryTime") %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="118px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Valid Until">
                                    <ItemTemplate>
                                        <%# Eval("ExpiryDate", "{0:dd/MM/yyyy}") %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="133" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridButtonColumn
                                    ButtonType="ImageButton"
                                    CommandName="ViewSupplierAttachments"
                                    HeaderText="Supplier Attachments"
                                    ImageUrl="~/includes/images/attach_small_f1.gif">
                                    <HeaderStyle HorizontalAlign="Center" Width="87px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridButtonColumn>
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                    <Columns>
                        <telerik:GridTemplateColumn ColumnGroupName="Requirement" HeaderText="Description">
                            <ItemTemplate>
                                <%# Eval("Requirement.Description") %>
                            </ItemTemplate>
                            <HeaderStyle Width="442px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn ColumnGroupName="Requirement" HeaderText="Quantity">
                            <ItemTemplate>
                                <%# Eval("Requirement.Quantity") %>
                            </ItemTemplate>
                            <HeaderStyle Width="177px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn ColumnGroupName="Requirement" HeaderText="Unit of Measure">
                            <ItemTemplate>
                                <%# Eval("Requirement.UnitOfMeasure.Value") %>
                            </ItemTemplate>
                            <HeaderStyle Width="177px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn
                            ButtonType="ImageButton"
                            ColumnGroupName="Requirement"
                            HeaderText="File Attachments"
                            UniqueName="FileAttachments">
                            <HeaderStyle HorizontalAlign="Center" Width="87px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridButtonColumn>
                        <telerik:GridButtonColumn
                            ButtonType="ImageButton"
                            ColumnGroupName="Requirement"
                            HeaderText="Text Attachments"
                            UniqueName="TextAttachments">
                            <HeaderStyle HorizontalAlign="Center" Width="87px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridButtonColumn>
                        <telerik:GridTemplateColumn ColumnGroupName="Offer" HeaderText="Manufacturer Code">
                            <ItemTemplate>
                                <%# Eval("ManufacturerCode") %>
                            </ItemTemplate>
                            <HeaderStyle Width="236px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn ColumnGroupName="Offer" HeaderText="Supplier Code">
                            <ItemTemplate>
                                <%# Eval("SupplierCode") %>
                            </ItemTemplate>
                            <HeaderStyle Width="236px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn ColumnGroupName="Offer" HeaderText="Unit Cost">
                            <ItemTemplate>
                                <%# Eval("UnitCost") %>
                            </ItemTemplate>
                            <HeaderStyle Width="177px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn ColumnGroupName="Offer" HeaderText="Remarks">
                            <ItemTemplate>
                                <%# Eval("Remarks") %>
                            </ItemTemplate>
                            <HeaderStyle Width="236px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn
                            ButtonType="ImageButton"
                            ColumnGroupName="Offer"
                            CommandName="ViewSupplierAttachments"
                            HeaderText="Supplier Attachments"
                            ImageUrl="~/includes/images/attach_small_f1.gif">
                            <HeaderStyle HorizontalAlign="Center" Width="87px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <ColumnGroups>
                        <telerik:GridColumnGroup Name="Requirement" HeaderText="Requirement" />
                        <telerik:GridColumnGroup Name="Offer" HeaderText="Offer" />
                    </ColumnGroups>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
        <div class="grid" style="margin: 0 0 20px 0; overflow-x: scroll; padding: 0; width: 1880px;">
            <telerik:RadGrid ID="charges" runat="server" Skin="Windows7">
                <MasterTableView AutoGenerateColumns="false" DataKeyNames="Description" TableLayout="Fixed">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="Description">
                            <ItemTemplate>
                                <%# Eval("Description") %>
                            </ItemTemplate>
                            <HeaderStyle Width="620px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Amount">
                            <ItemTemplate>
                                <%# Eval("Amount") %>
                            </ItemTemplate>
                            <HeaderStyle Width="620px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Currency">
                            <ItemTemplate>
                                <%# Eval("Currency.Value") %>
                            </ItemTemplate>
                            <HeaderStyle Width="722px" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
        <div style="padding: 0;">
            <asp:Button runat="server" Text="Home" PostBackUrl="~/Account/PublishedRFQView.aspx" />
        </div>
    </asp:Panel>
    <telerik:RadAjaxLoadingPanel ID="ajaxLoadingPanel" runat="server" Skin="Windows7" />
    <telerik:RadAjaxManager ID="ajaxManager" runat="server" />
    <script src="../Scripts/jquery-3.1.1.slim.js"></script>
    <script>
        (function (jQuery, window) {
            jQuery(adjustWidth);
            jQuery(window).resize(adjustWidth);

            function adjustWidth() {
                jQuery(".grid").each(function (x, y) {
                    jQuery(y).width(jQuery(window).width() - 40);
                });
            }
        })($, window);
    </script>
</asp:Content>
