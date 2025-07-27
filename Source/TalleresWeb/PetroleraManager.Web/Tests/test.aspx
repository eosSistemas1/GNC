<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="PetroleraManager.Web.Tests.test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <table width="100%" border="0">
            <tr>
                <td width="350px">
                    <PLs:PLTextBox ID="txttest" runat="server" LabelText="Q va a ca:"  Required="true" />
                </td>
                
                </tr>
                <tr><td><PLs:PLTextBox ID="PLTextBox1" runat="server" LabelText="Q va a ca:"  Required="true" /> </td></tr>
                </table>
                
<PLs:PLButton ID="btn" runat="server" Text="but" />
</asp:Content>
