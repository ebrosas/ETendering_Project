<%@ Page
    AutoEventWireup="false"
    CodeBehind="PlaceTender.aspx.cs"
    Inherits="Tendering.Presentation.PlaceTender"
    Language="C#"
    MasterPageFile="~/CommonObject/Site.Master" %>

<%@ MasterType VirtualPath="~/CommonObject/Site.Master" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="main" runat="server" ContentPlaceHolderID="mainContent">
    <script src="../Scripts/rfqattachment.js"></script>
    <asp:Panel ID="panEntry" runat="server" CssClass="GroupLayoutOrder" GroupingText="Call For Tenders {0}">
        <div id="message" runat="server" style="background-color: red; color: yellow; font-weight: bold; margin: 0 0 20px 0; padding: 10px 20px; border: 2px solid grey; border-radius: 5px;">
            [Text]
        </div>
        <div style="margin: 0 0 20px 0; padding: 0 20px 0 20px;">
            <div>
                <label class="LabelBold" style="display: inline-block; font-weight: bold; width: 118px;">Currency</label>
                <telerik:RadComboBox ID="currency" runat="server" AppendDataBoundItems="true" MarkFirstMatch="True" Skin="Windows7">
                    <Items>
                        <telerik:RadComboBoxItem Text="Select a currency" />
                    </Items>
                </telerik:RadComboBox>
                <asp:CompareValidator
                    runat="server"
                    ControlToValidate="currency"
                    Display="Dynamic"
                    Operator="NotEqual"
                    SetFocusOnError="true"
                    Text="* Required"
                    ValidationGroup="TenderValidationGroup"
                    ValueToCompare="Select a currency" />
            </div>
            <div>
                <label class="LabelBold" style="display: inline-block; font-weight: bold; margin: 0 2px 0 0; width: 118px;">Delivery Point</label>
                <telerik:RadTextBox ID="deliveryPoint" runat="server" MaxLength="50" SkinID="TextLeft" />
            </div>
            <div>
                <label class="LabelBold" style="display: inline-block; font-weight: bold; width: 118px;">Delivery Terms</label>
                <telerik:RadComboBox ID="deliveryTerms" runat="server" AppendDataBoundItems="true" MarkFirstMatch="True" Skin="Windows7">
                    <Items>
                        <telerik:RadComboBoxItem Text="Select delivery terms" />
                    </Items>
                </telerik:RadComboBox>
                <asp:CompareValidator
                    runat="server"
                    ControlToValidate="deliveryTerms"
                    Display="Dynamic"
                    Operator="NotEqual"
                    SetFocusOnError="true"
                    Text="* Required"
                    ValidationGroup="TenderValidationGroup"
                    ValueToCompare="Select delivery terms" />
            </div>
            <div>
                <label class="LabelBold" style="display: inline-block; font-weight: bold; margin: 0 2px 0 0; width: 118px;">Delivery Time (days)</label>
                <telerik:RadTextBox ID="deliveryTime" runat="server" SkinID="TextLeftMandatory" />
                <span style="margin: 0 0 0 2px;">
                    <asp:RequiredFieldValidator
                        runat="server"
                        ControlToValidate="deliveryTime"
                        Display="Dynamic"
                        SetFocusOnError="true"
                        Text="* Required"
                        ValidationGroup="TenderValidationGroup" />
                    <asp:RegularExpressionValidator
                        runat="server"
                        ControlToValidate="deliveryTime"
                        Display="Dynamic"
                        SetFocusOnError="true"
                        Text="* Invalid"
                        ValidationExpression="^[1-9][0-9]{0,2}$"
                        ValidationGroup="TenderValidationGroup" />
                </span>
            </div>
            <div>
                <label class="LabelBold" style="display: inline-block; font-weight: bold; width: 118px;">Valid Until</label>
                <telerik:RadDatePicker ID="expiryDate" runat="server" Skin="Windows7">
                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" />
                </telerik:RadDatePicker>
                <asp:RequiredFieldValidator
                    runat="server"
                    ControlToValidate="expiryDate"
                    Display="Dynamic"
                    SetFocusOnError="true"
                    Text="* Required"
                    ValidationGroup="TenderValidationGroup" />
            </div>
            <div>
                <label class="LabelBold" style="display: inline-block; font-weight: bold; margin: 0 2px 0 0; width: 118px;">Payment Terms</label>
                <telerik:RadTextBox ID="paymentTerms" runat="server" MaxLength="50" SkinID="TextLeftMandatory" />
                <span style="margin: 0 0 0 2px;">
                    <asp:RequiredFieldValidator
                        runat="server"
                        ControlToValidate="paymentTerms"
                        Display="Dynamic"
                        SetFocusOnError="true"
                        Text="* Required"
                        ValidationGroup="TenderValidationGroup" />
                </span>
            </div>
        </div>
        <div class="grid" style="margin: 0 0 20px 0; overflow-x: scroll; padding: 0; width: 1880px;">
            <telerik:RadGrid ID="responses" runat="server" Skin="Windows7">
                <MasterTableView AutoGenerateColumns="false" DataKeyNames="Requirement.Number" EditMode="InPlace" Name="offers" HierarchyLoadMode="Client" TableLayout="Fixed">
                    <DetailTables>
                        <telerik:GridTableView AutoGenerateColumns="false" CommandItemDisplay="Top" DataKeyNames="Description" EditMode="InPlace" Name="options" TableLayout="Fixed">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderText="Edit">
                                    <HeaderStyle Width="55px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridTemplateColumn HeaderText="Description">
                                    <ItemTemplate>
                                        <%# Eval("Description") %>
                                    </ItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="description" runat="server" MaxLength="60" />
                                        <br />
                                        <asp:RequiredFieldValidator
                                            runat="server"
                                            ControlToValidate="description"
                                            Display="Static"
                                            SetFocusOnError="true"
                                            Text="* Required" />
                                    </InsertItemTemplate>
                                    <EditItemTemplate>
                                        <%# Eval("Description") %>
                                        <br />
                                        <br />
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Manufacturer Code">
                                    <ItemTemplate>
                                        <%# Eval("ManufacturerCode") %>
                                    </ItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox
                                            ID="manufacturerCode"
                                            runat="server"
                                            MaxLength="30"
                                            Width="96px" />
                                        <br />
                                        <br />
                                    </InsertItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox
                                            ID="manufacturerCode"
                                            runat="server"
                                            MaxLength="30"
                                            Text='<%# Eval("ManufacturerCode") %>'
                                            Width="96px" />
                                        <br />
                                        <br />
                                    </EditItemTemplate>
                                    <HeaderStyle Width="118px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Quantity">
                                    <ItemTemplate>
                                        <%# Eval("Quantity") %>
                                    </ItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="quantity" runat="server" Width="96px" />
                                        <br />
                                        <asp:RequiredFieldValidator
                                            runat="server"
                                            ControlToValidate="quantity"
                                            Display="Dynamic"
                                            SetFocusOnError="true"
                                            Text="* Required" />
                                        <asp:RegularExpressionValidator
                                            runat="server"
                                            ControlToValidate="quantity"
                                            Display="Static"
                                            SetFocusOnError="true"
                                            Text="* Invalid"
                                            ValidationExpression="^[1-9][0-9]{0,6}$" />
                                    </InsertItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox
                                            ID="quantity"
                                            runat="server"
                                            Text='<%# Eval("Quantity") %>'
                                            Width="96px" />
                                        <br />
                                        <asp:RequiredFieldValidator
                                            runat="server"
                                            ControlToValidate="quantity"
                                            Display="Dynamic"
                                            SetFocusOnError="true"
                                            Text="* Required" />
                                        <asp:RegularExpressionValidator
                                            runat="server"
                                            ControlToValidate="quantity"
                                            Display="Static"
                                            SetFocusOnError="true"
                                            Text="* Invalid"
                                            ValidationExpression="^[1-9][0-9]{0,6}$" />
                                    </EditItemTemplate>
                                    <HeaderStyle Width="118px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Supplier Code">
                                    <ItemTemplate>
                                        <%# Eval("SupplierCode") %>
                                    </ItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox
                                            ID="supplierCode"
                                            runat="server"
                                            MaxLength="30"
                                            Width="96px" />
                                        <br />
                                        <br />
                                    </InsertItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox
                                            ID="supplierCode"
                                            runat="server"
                                            MaxLength="30"
                                            Text='<%# Eval("SupplierCode") %>'
                                            Width="96px" />
                                        <br />
                                        <br />
                                    </EditItemTemplate>
                                    <HeaderStyle Width="118px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Unit Cost">
                                    <ItemTemplate>
                                        <%# Eval("UnitCost") %>
                                    </ItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="unitCost" runat="server" Width="96px" />
                                        <br />
                                        <asp:RequiredFieldValidator
                                            runat="server"
                                            ControlToValidate="unitCost"
                                            Display="Dynamic"
                                            SetFocusOnError="true"
                                            Text="* Required" />
                                        <asp:RegularExpressionValidator
                                            runat="server"
                                            ControlToValidate="unitCost"
                                            Display="Static"
                                            SetFocusOnError="true"
                                            Text="* Invalid"
                                            ValidationExpression="^[0-9]{0,10}(.[0-9]{1,3})?$" />
                                    </InsertItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox
                                            ID="unitCost"
                                            runat="server"
                                            Text='<%# Eval("UnitCost") %>'
                                            Width="96px" />
                                        <br />
                                        <asp:RequiredFieldValidator
                                            runat="server"
                                            ControlToValidate="unitCost"
                                            Display="Dynamic"
                                            SetFocusOnError="true"
                                            Text="* Required" />
                                        <asp:RegularExpressionValidator
                                            runat="server"
                                            ControlToValidate="unitCost"
                                            Display="Static"
                                            SetFocusOnError="true"
                                            Text="* Invalid"
                                            ValidationExpression="^[0-9]{0,10}(.[0-9]{1,3})?$" />
                                    </EditItemTemplate>
                                    <HeaderStyle Width="118px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Unit Of Measure">
                                    <ItemTemplate>
                                        <%# Eval("UnitOfMeasure.Value") %>
                                    </ItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList
                                            ID="unitOfMeasure"
                                            runat="server"
                                            AppendDataBoundItems="true"
                                            Width="160">
                                            <asp:ListItem Text="Select a unit of measure" />
                                        </asp:DropDownList>
                                        <br />
                                        <asp:CompareValidator
                                            runat="server"
                                            ControlToValidate="unitOfMeasure"
                                            Display="Static"
                                            Operator="NotEqual"
                                            SetFocusOnError="true"
                                            Text="* Required"
                                            ValueToCompare="Select a unit of measure" />
                                    </InsertItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList
                                            ID="unitOfMeasure"
                                            runat="server"
                                            AppendDataBoundItems="true"
                                            Width="160">
                                            <asp:ListItem Text="Select a unit of measure" />
                                        </asp:DropDownList>
                                        <br />
                                        <asp:CompareValidator
                                            runat="server"
                                            ControlToValidate="unitOfMeasure"
                                            Display="Static"
                                            Operator="NotEqual"
                                            SetFocusOnError="true"
                                            Text="* Required"
                                            ValueToCompare="Select a unit of measure" />
                                    </EditItemTemplate>
                                    <HeaderStyle Width="176" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Currency">
                                    <ItemTemplate>
                                        <%# Eval("Currency.Value") %>
                                    </ItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList
                                            ID="currency"
                                            runat="server"
                                            AppendDataBoundItems="true"
                                            Width="160">
                                            <asp:ListItem Text="Select a currency" />
                                        </asp:DropDownList>
                                        <br />
                                        <asp:CompareValidator
                                            runat="server"
                                            ControlToValidate="currency"
                                            Display="Static"
                                            Operator="NotEqual"
                                            SetFocusOnError="true"
                                            Text="* Required"
                                            ValueToCompare="Select a currency" />
                                    </InsertItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList
                                            ID="currency"
                                            runat="server"
                                            AppendDataBoundItems="true"
                                            Width="160">
                                            <asp:ListItem Text="Select a currency" />
                                        </asp:DropDownList>
                                        <br />
                                        <asp:CompareValidator
                                            runat="server"
                                            ControlToValidate="currency"
                                            Display="Static"
                                            Operator="NotEqual"
                                            SetFocusOnError="true"
                                            Text="* Required"
                                            ValueToCompare="Select a currency" />
                                    </EditItemTemplate>
                                    <HeaderStyle Width="176" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Delivery Point">
                                    <ItemTemplate>
                                        <%# Eval("DeliveryPoint") %>
                                    </ItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox
                                            ID="deliveryPoint"
                                            runat="server"
                                            MaxLength="50"
                                            Width="96px" />
                                        <br />
                                        <br />
                                    </InsertItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox
                                            ID="deliveryPoint"
                                            runat="server"
                                            MaxLength="50"
                                            Text='<%# Eval("DeliveryPoint") %>'
                                            Width="96px" />
                                        <br />
                                        <br />
                                    </EditItemTemplate>
                                    <HeaderStyle Width="118px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Delivery Terms">
                                    <ItemTemplate>
                                        <%# Eval("DeliveryTerms.Value") %>
                                    </ItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList
                                            ID="deliveryTerms"
                                            runat="server"
                                            AppendDataBoundItems="true"
                                            Width="160">
                                            <asp:ListItem Text="Select delivery terms" />
                                        </asp:DropDownList>
                                        <br />
                                        <asp:CompareValidator
                                            runat="server"
                                            ControlToValidate="deliveryTerms"
                                            Display="Static"
                                            Operator="NotEqual"
                                            SetFocusOnError="true"
                                            Text="* Required"
                                            ValueToCompare="Select delivery terms" />
                                    </InsertItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList
                                            ID="deliveryTerms"
                                            runat="server"
                                            AppendDataBoundItems="true"
                                            Width="160">
                                            <asp:ListItem Text="Select delivery terms" />
                                        </asp:DropDownList>
                                        <br />
                                        <asp:CompareValidator
                                            runat="server"
                                            ControlToValidate="deliveryTerms"
                                            Display="Static"
                                            Operator="NotEqual"
                                            SetFocusOnError="true"
                                            Text="* Required"
                                            ValueToCompare="Select delivery terms" />
                                    </EditItemTemplate>
                                    <HeaderStyle Width="176" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Delivery Time (days)">
                                    <ItemTemplate>
                                        <%# Eval("DeliveryTime") %>
                                    </ItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="DeliveryTime" runat="server" Width="96px" />
                                        <br />
                                        <asp:RequiredFieldValidator
                                            runat="server"
                                            ControlToValidate="deliveryTime"
                                            Display="Dynamic"
                                            SetFocusOnError="true"
                                            Text="* Required" />
                                        <asp:RegularExpressionValidator
                                            runat="server"
                                            ControlToValidate="deliveryTime"
                                            Display="Static"
                                            SetFocusOnError="true"
                                            Text="* Invalid"
                                            ValidationExpression="^[1-9][0-9]{0,2}$" />
                                    </InsertItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox
                                            ID="DeliveryTime"
                                            runat="server"
                                            Text='<%# Eval("DeliveryTime") %>'
                                            Width="96px" />
                                        <br />
                                        <asp:RequiredFieldValidator
                                            runat="server"
                                            ControlToValidate="deliveryTime"
                                            Display="Dynamic"
                                            SetFocusOnError="true"
                                            Text="* Required" />
                                        <asp:RegularExpressionValidator
                                            runat="server"
                                            ControlToValidate="deliveryTime"
                                            Display="Static"
                                            SetFocusOnError="true"
                                            Text="* Invalid"
                                            ValidationExpression="^[1-9][0-9]{0,2}$" />
                                    </EditItemTemplate>
                                    <HeaderStyle Width="118px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Valid Until">
                                    <ItemTemplate>
                                        <%# Eval("ExpiryDate", "{0:dd/MM/yyyy}") %>
                                    </ItemTemplate>
                                    <InsertItemTemplate>
                                        <telerik:RadDatePicker ID="expiryDate" runat="server" Width="113">
                                            <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" />
                                        </telerik:RadDatePicker>
                                        <br />
                                        <asp:RequiredFieldValidator
                                            runat="server"
                                            ControlToValidate="expiryDate"
                                            Display="Static"
                                            SetFocusOnError="true"
                                            Text="* Required" />
                                    </InsertItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadDatePicker
                                            ID="expiryDate"
                                            runat="server"
                                            SelectedDate='<%# Eval("ExpiryDate") %>'
                                            Width="113">
                                            <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" />
                                        </telerik:RadDatePicker>
                                        <br />
                                        <asp:RequiredFieldValidator
                                            runat="server"
                                            ControlToValidate="expiryDate"
                                            Display="Static"
                                            SetFocusOnError="true"
                                            Text="* Required" />
                                    </EditItemTemplate>
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
                                <telerik:GridButtonColumn Text="Delete" ButtonType="ImageButton" CommandName="Delete" HeaderText="Delete">
                                    <HeaderStyle HorizontalAlign="Center" Width="55px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <CommandItemSettings ShowRefreshButton="false" AddNewRecordText="Add an option" />
                        </telerik:GridTableView>
                    </DetailTables>
                    <Columns>
                        <telerik:GridTemplateColumn ColumnGroupName="Requirement" HeaderText="Description">
                            <ItemTemplate>
                                <%# Eval("Requirement.Description") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <%# Eval("Requirement.Description") %>
                                <br />
                                <br />
                            </EditItemTemplate>
                            <HeaderStyle Width="387px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn ColumnGroupName="Requirement" HeaderText="Quantity">
                            <ItemTemplate>
                                <%# Eval("Requirement.Quantity") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <%# Eval("Requirement.Quantity") %>
                                <br />
                                <br />
                            </EditItemTemplate>
                            <HeaderStyle Width="177px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn ColumnGroupName="Requirement" HeaderText="Unit of Measure">
                            <ItemTemplate>
                                <%# Eval("Requirement.UnitOfMeasure.Value") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <%# Eval("Requirement.UnitOfMeasure.Value") %>
                                <br />
                                <br />
                            </EditItemTemplate>
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
                        <telerik:GridEditCommandColumn ButtonType="ImageButton" ColumnGroupName="Offer" HeaderText="Edit">
                            <HeaderStyle HorizontalAlign="Center" Width="55px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridTemplateColumn ColumnGroupName="Offer" HeaderText="Manufacturer Code">
                            <ItemTemplate>
                                <%# Eval("ManufacturerCode") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox
                                    runat="server"
                                    ID="manufacturerCode"
                                    MaxLength="30"
                                    Text='<%# Eval("ManufacturerCode") %>'
                                    Width="214px" />
                                <br />
                                <br />
                            </EditItemTemplate>
                            <HeaderStyle Width="236px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn ColumnGroupName="Offer" HeaderText="Supplier Code">
                            <ItemTemplate>
                                <%# Eval("SupplierCode") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox
                                    runat="server"
                                    ID="supplierCode"
                                    MaxLength="30"
                                    Text='<%# Eval("SupplierCode") %>'
                                    Width="214px" />
                                <br />
                                <br />
                            </EditItemTemplate>
                            <HeaderStyle Width="236px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn ColumnGroupName="Offer" HeaderText="Unit Cost">
                            <ItemTemplate>
                                <%# Eval("UnitCost") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox
                                    runat="server"
                                    ID="unitCost"
                                    Text='<%# Eval("UnitCost") %>'
                                    Width="155px" />
                                <br />
                                <asp:RequiredFieldValidator
                                    runat="server"
                                    ControlToValidate="unitCost"
                                    Display="Dynamic"
                                    SetFocusOnError="true"
                                    Text="* Required" />
                                <asp:RegularExpressionValidator
                                    runat="server"
                                    ControlToValidate="unitCost"
                                    Display="Static"
                                    SetFocusOnError="true"
                                    Text="* Invalid"
                                    ValidationExpression="^[0-9]{0,10}(.[0-9]{1,3})?$" />
                            </EditItemTemplate>
                            <HeaderStyle Width="177px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn ColumnGroupName="Offer" HeaderText="Remarks">
                            <ItemTemplate>
                                <%# Eval("Remarks") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox
                                    runat="server"
                                    ID="remarks"
                                    MaxLength="200"
                                    Text='<%# Eval("Remarks") %>'
                                    Width="214px" />
                                <br />
                                <br />
                            </EditItemTemplate>
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
                <MasterTableView AutoGenerateColumns="false" CommandItemDisplay="Top" DataKeyNames="Description" EditMode="InPlace" TableLayout="Fixed">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderText="Edit">
                            <HeaderStyle Width="55px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridTemplateColumn HeaderText="Description">
                            <ItemTemplate>
                                <%# Eval("Description") %>
                            </ItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="description" runat="server" MaxLength="60" />
                                <br />
                                <asp:RequiredFieldValidator
                                    runat="server"
                                    ControlToValidate="description"
                                    Display="Static"
                                    SetFocusOnError="true"
                                    Text="* Required" />
                            </InsertItemTemplate>
                            <EditItemTemplate>
                                <%# Eval("Description") %>
                                <br />
                                <br />
                            </EditItemTemplate>
                            <HeaderStyle Width="620px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Amount">
                            <ItemTemplate>
                                <%# Eval("Amount") %>
                            </ItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="amount" runat="server" Width="155px" />
                                <br />
                                <asp:RequiredFieldValidator
                                    runat="server"
                                    ControlToValidate="amount"
                                    Display="Dynamic"
                                    SetFocusOnError="true"
                                    Text="* Required" />
                                <asp:RegularExpressionValidator
                                    runat="server"
                                    ControlToValidate="amount"
                                    Display="Static"
                                    SetFocusOnError="true"
                                    Text="* Invalid"
                                    ValidationExpression="^[0-9]{0,10}(.[0-9]{1,3})?$" />
                            </InsertItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox
                                    ID="amount"
                                    runat="server"
                                    Text='<%# Eval("Amount") %>'
                                    Width="155px" />
                                <br />
                                <asp:RequiredFieldValidator
                                    runat="server"
                                    ControlToValidate="amount"
                                    Display="Dynamic"
                                    SetFocusOnError="true"
                                    Text="* Required" />
                                <asp:RegularExpressionValidator
                                    runat="server"
                                    ControlToValidate="amount"
                                    Display="Static"
                                    SetFocusOnError="true"
                                    Text="* Invalid"
                                    ValidationExpression="^[0-9]{0,10}(.[0-9]{1,3})?$" />
                            </EditItemTemplate>
                            <HeaderStyle Width="620px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Currency">
                            <ItemTemplate>
                                <%# Eval("Currency.Value") %>
                            </ItemTemplate>
                            <InsertItemTemplate>
                                <asp:DropDownList
                                    ID="currency"
                                    runat="server"
                                    AppendDataBoundItems="true"
                                    Width="208">
                                    <asp:ListItem Text="Select a currency" />
                                </asp:DropDownList>
                                <br />
                                <asp:CompareValidator
                                    runat="server"
                                    ControlToValidate="currency"
                                    Display="Static"
                                    Operator="NotEqual"
                                    SetFocusOnError="true"
                                    Text="* Required"
                                    ValueToCompare="Select a currency" />
                            </InsertItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList
                                    ID="currency"
                                    runat="server"
                                    AppendDataBoundItems="true"
                                    Width="208">
                                    <asp:ListItem Text="Select a currency" />
                                </asp:DropDownList>
                                <br />
                                <asp:CompareValidator
                                    runat="server"
                                    ControlToValidate="currency"
                                    Display="Static"
                                    Operator="NotEqual"
                                    SetFocusOnError="true"
                                    Text="* Required"
                                    ValueToCompare="Select a currency" />
                            </EditItemTemplate>
                            <HeaderStyle Width="612px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn Text="Delete" ButtonType="ImageButton" CommandName="Delete" HeaderText="Delete">
                            <HeaderStyle Width="55px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <CommandItemSettings ShowRefreshButton="false" AddNewRecordText="Add a charge" />
                </MasterTableView>
            </telerik:RadGrid>
        </div>
        <div style="padding: 0;">
            <asp:Button ID="save" runat="server" Text="Save" ValidationGroup="TenderValidationGroup" />
            <asp:Button runat="server" Text="Cancel" PostBackUrl="~/Account/PublishedRFQView.aspx" />
        </div>
    </asp:Panel>
    <telerik:RadAjaxLoadingPanel ID="ajaxLoadingPanel" runat="server" Skin="Windows7" />
    <telerik:RadAjaxManager ID="ajaxManager" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="save">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="panEntry" LoadingPanelID="ajaxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
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
