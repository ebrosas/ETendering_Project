<%@ Page Title="" Language="C#" MasterPageFile="~/CommonObject/Site.Master" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="GARMCO.AMS.B2B.eTendering.Website.Success" %>
<%@ MasterType VirtualPath="~/CommonObject/Site.Master" %>
<asp:Content ID="content" ContentPlaceHolderID="mainContent" runat="server">
	<br /><br /><br />
	<asp:Panel ID="panSuccess" runat="server" CssClass="GroupLayoutSuccess" GroupingText="Success" Width="100%">
		<asp:Literal ID="litSuccessMsg" runat="server" />
	</asp:Panel>
</asp:Content>
