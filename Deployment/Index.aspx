<%@ Page Title="" Language="C#" MasterPageFile="~/CommonObject/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="GARMCO.AMS.B2B.eTendering.Website.Index" %>
<%@ MasterType VirtualPath="~/CommonObject/Site.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content" ContentPlaceHolderID="mainContent" runat="server">
	<asp:ValidationSummary ID="valSummary" runat="server" CssClass="ValidationError" HeaderText="Please enter a value on the following field(s):" ValidationGroup="valPrimary" />
    <p class="form-row">
        <asp:TextBox ID="txtEmail" runat="server" placeholder="Email Address*" required class="input-text" />
        <asp:RequiredFieldValidator ID="reqtxtEmail" runat="server" ControlToValidate="txtEmail" CssClass="LabelValidationError" ErrorMessage="e-Mail Address cannot be blank." SetFocusOnError="true" Text="*" ToolTip="e-Mail Address cannot be blank." ValidationGroup="valPrimary" />
    </p>
    <p class="form-row">
        <asp:TextBox ID="txtPassword" runat="server" CssClass="input-text" TextMode="Password" placeholder="Password*" required name="password" />
        <asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="txtPassword" CssClass="LabelValidationError" ErrorMessage="Password cannot be blank." SetFocusOnError="true" Text="*" ToolTip="Password cannot be blank." ValidationGroup="valPrimary" />
    </p>
    <p class="form-row">
        <asp:CheckBox ID="chkRememberMe" runat="server" Text="Remember me" />
        <span class="remember lost_password">Remember me</span>
    </p>
    <telerik:RadCaptcha ID="captchaImg" runat="server" CaptchaTextBoxLabel=" Type the code from the image (it is not case-sensitive)" EnableRefreshImage="True" ErrorMessage="Type the code from the image" ValidationGroup="valPrimary" />
    <p class="form-row submit-button" style="margin-top: 30px;">
        <asp:Button ID="btnLogin" runat="server" CssClass="woocommerce-Button button" Text="Sign In" ValidationGroup="valPrimary" OnClick="btnLogin_Click" />
    </p>
    <p class="lost_password">
        <asp:HyperLink ID="lnkForgotPassword" runat="server" CssClass="Link" NavigateUrl="~/ForgotPassword.aspx" Text="Lost your password?" />
    </p>
    <asp:Panel ID="panObjDS" runat="server">
        <asp:ObjectDataSource ID="objContact" runat="server" OldValuesParameterFormatString="" SelectMethod="GetSupplierContact" TypeName="GARMCO.AMS.B2B.eTendering.DAL.SupplierContactRepository" OnSelected="objContact_Selected">
            <SelectParameters>
                <asp:Parameter DefaultValue="1" Name="mode" Type="Byte" />
                <asp:Parameter DefaultValue="0" Name="contactID" Type="Int32" />
                <asp:ControlParameter ControlID="txtEmail" DefaultValue="" Name="contactEmail" PropertyName="Text" Type="String" />
                <asp:Parameter DefaultValue="" Name="contactActiveKey" Type="String" />
                <asp:Parameter DefaultValue="0" Name="contactSupplierNo" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </asp:Panel>
</asp:Content>
