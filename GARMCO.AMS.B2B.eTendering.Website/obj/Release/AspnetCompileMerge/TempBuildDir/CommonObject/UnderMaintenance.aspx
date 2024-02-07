<%@ Page Title="" Language="C#" MasterPageFile="~/CommonObject/SiteOffline.Master" AutoEventWireup="true" CodeBehind="UnderMaintenance.aspx.cs" 
    Inherits="GARMCO.AMS.B2B.eTendering.Website.CommonObject.UnderMaintenance" %>

<%@ MasterType VirtualPath="~/CommonObject/SiteOffline.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <asp:Panel ID="panError" runat="server" style="margin-left: auto; margin-right: auto; vertical-align: middle; width: 900px;">
		<br /><br /><br /><br /> 
		<table border="0" style="vertical-align: top; width: 100%">
			<tr>
                <td style="vertical-align: top; text-align: center; width: auto;">
					<asp:Image ID="imgError" runat="server" 
                        ImageUrl="~/includes/images/under_maintenance.gif" />
				</td>
			</tr>
            <tr>                
                <td style="text-align: center;">
                    <asp:Panel ID="panStackError" runat="server" Visible="true" Width="100%">
						<br style="font: Verdana; font-size: 9pt; font-weight: bold;" />
                        We sincerely apologize for the inconvenience.
                        <br />
                        The E-Tendering site is currently undergoing system maintenance and upgrades, but will return shortly.
                        <br />
                        Thank you for your patience.
                        <br />
						<table border="0" style="width: 100%; display: none;">
							<tr>
								<td class="LabelBold" style="width: 100px; vertical-align: top;">
									Offending URL
								</td>
								<td class="RowStyle">
									<asp:Literal ID="litURL" runat="server" />
								</td>
							</tr>
							<tr>
								<td class="LabelBold" style="vertical-align: top;">
									Source
								</td>
								<td class="RowStyle">
									<asp:Literal ID="litSource" runat="server" />
								</td>
							</tr>
							<tr>
								<td class="LabelBold" style="vertical-align: top;">
									Message
								</td>
								<td class="RowStyle">
									<asp:Literal ID="litMessage" runat="server" />
								</td>
							</tr>
							<tr>
								<td class="LabelBold" style="vertical-align: top;">
									Inner Message
								</td>
								<td class="RowStyle">
									<asp:Literal ID="litInnerMsg" runat="server" />
								</td>
							</tr>
							<tr>
								<td class="LabelBold" style="vertical-align: top;">
									Stack Trace
								</td>
								<td class="RowStyle">
									<asp:Literal ID="litStackTrace" runat="server" />
								</td>
							</tr>
						</table>
					</asp:Panel>
                </td>
            </tr>
		</table>
	</asp:Panel>
</asp:Content>
