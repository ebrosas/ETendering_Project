<%@ Page
    AutoEventWireup="false"
    CodeBehind="ClosedCallForTenders.aspx.cs"
    Inherits="Tendering.Presentation.ClosedCallForTenders"
    Language="C#"
    MasterPageFile="~/CommonObject/Site.Master" %>

<%@ MasterType VirtualPath="~/CommonObject/Site.Master" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="main" runat="server" ContentPlaceHolderID="mainContent">
    <script src="../Scripts/rfqattachment.js"></script>
    <asp:Panel ID="panEntry" runat="server" CssClass="GroupLayoutOrder" GroupingText="Call For Tenders {0}">
        <div id="message" runat="server" style="background-color: red; color: yellow; font-weight: bold; margin: 0 0 20px 0; padding: 10px 20px; border: 2px solid grey; border-radius: 5px;">
            The call for tenders you are tring to place a tender for is closed.
        </div>
    </asp:Panel>
</asp:Content>
