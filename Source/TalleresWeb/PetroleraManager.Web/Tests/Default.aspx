<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="PetroleraManager.Web._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to ASP.NET!
    </h2>
    <p>
        To learn more about ASP.NET visit <a href="http://www.asp.net" title="ASP.NET Website">www.asp.net</a>.
    </p>
    <p>
        You can also find <a href="http://go.microsoft.com/fwlink/?LinkID=152368&amp;clcid=0x409"
            title="MSDN ASP.NET Docs">documentation on ASP.NET at MSDN</a>.
    </p>

    <asp:GridView ID="grd" runat="server" />
    <PLs:PLComboBox ID="cboTest" LabelText="Combito" runat="server" />

    <PLs:PLTextBox ID="jj" runat="server" LabelText="Peluquen" />

    <asp:Button ID="buscarExt" runat="server" Text="LLenar grid" OnClick="BuscarExt_Click"/>
    <asp:Button ID="addb" runat="server" Text="Add" OnClick="Add_Click"/>
    <asp:Button ID="delB" runat="server" Text ="Delete" OnClick="Delete_Click"/>
    <asp:Button ID="updB" runat="server" Text ="Update" OnClick="Update_Click"/>
</asp:Content>
