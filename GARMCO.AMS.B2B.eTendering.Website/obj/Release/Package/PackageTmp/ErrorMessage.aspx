<%@ Page Title="" Language="C#" MasterPageFile="~/CommonObject/Site.Master" AutoEventWireup="true" CodeBehind="ErrorMessage.aspx.cs" Inherits="GARMCO.AMS.B2B.eTendering.Website.ErrorMessage" %>
<%@ MasterType VirtualPath="~/CommonObject/Site.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="content" ContentPlaceHolderID="mainContent" runat="server">
	<asp:Panel ID="panError" runat="server" style="margin-left: auto; margin-right: auto; vertical-align: middle; width: 800px;">
			<br /><br /><br />
			<table border="0" style="padding: 5px; vertical-align: top; width: 500px;">
				<tr>
					<td style="vertical-align: top;">
						<asp:Image ID="imgError" runat="server" ImageUrl="~/includes/images/exception.jpg" />
					</td>
					<td>
						<asp:Panel ID="panStackError" runat="server" Visible="false" Width="100%">
							<br />We apologize, an error occurred while processing your request. Our support team has been informed already to look into the issue.<br /><br />
							<table border="0" style="padding: 5px; width: 100%;">
								<tr>
									<td class="LabelBold" style="width: 120px; vertical-align: top;">
										Offending URL
									</td>
									<td>
										<asp:Literal ID="litURL" runat="server" />
									</td>
								</tr>
								<tr>
									<td class="LabelBold" style="vertical-align: top;">
										Source
									</td>
									<td>
										<asp:Literal ID="litSource" runat="server" />
									</td>
								</tr>
								<tr>
									<td class="LabelBold" style="vertical-align: top;">
										Message
									</td>
									<td>
										<asp:Literal ID="litMessage" runat="server" />
									</td>
								</tr>
								<tr>
									<td class="LabelBold" style="vertical-align: top;">
										Inner Message
									</td>
									<td>
										<asp:Literal ID="litInnerMsg" runat="server" />
									</td>
								</tr>
								<tr>
									<td class="LabelBold" style="vertical-align: top;">
										Stack Trace
									</td>
									<td>
										<asp:Literal ID="litStackTrace" runat="server" />
									</td>
								</tr>
							</table>
						</asp:Panel>
						<asp:Literal ID="litError" runat="server" />
					</td>
				</tr>
			</table>
		</asp:Panel>
</asp:Content>
