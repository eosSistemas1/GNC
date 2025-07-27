<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FiltrosFechas.ascx.cs" Inherits="TalleresWeb.Web.UI.UserControls.FiltrosFechas" %>
<fieldset>
<asp:UpdatePanel runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <table style="width:100%;">
        <tr>
            <td colspan="3"><asp:CheckBox ID="chkPorRangoFechas" runat="server" OnCheckedChanged="CheckedChanged" AutoPostBack="true" Text="Por Rango:"  />
            <asp:CheckBox ID="chkPorQuincenas" runat="server" OnCheckedChanged="CheckedChanged" AutoPostBack="true" Text="Por Período:" /></td>
        </tr>
        <tr>            
            <td><CONTROLS:DateControl ID="calFechaD" runat="server" LabelText="Fecha Desde:" /></td>
            <td></td>
            <td><CONTROLS:DateControl ID="calFechaH" runat="server" LabelText="Fecha Hasta:" /></td>
        </tr>
        <tr>
            <td><CONTROLS:CboQuincenas ID="cboQuincena" runat="server" LabelText="Período:" AutomaticLoad="true" /></td>
            <td><CONTROLS:CboMes ID="cboMes" runat="server" LabelText="Mes:" AutomaticLoad="true" /></td>
            <td><CONTROLS:CboAnio ID="cboAnio" runat="server" LabelText="Año:" AutomaticLoad="true" /></td>
        </tr>
    </table>
    </ContentTemplate>
</asp:UpdatePanel>
</fieldset>