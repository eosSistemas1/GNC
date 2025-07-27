<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FiltrosFechas.ascx.cs" Inherits="PetroleraManager.Web.UserControls.FiltrosFechas" %>
<fieldset>
<asp:UpdatePanel runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <table style="width:100%;">
        <tr>
            <td colspan="3"><asp:CheckBox ID="chkPorRangoFechas" runat="server" OnCheckedChanged="CheckedChanged" AutoPostBack="true" Text="Por Rango:"  />
            <asp:CheckBox ID="chkPorQuincenas" runat="server" OnCheckedChanged="CheckedChanged" AutoPostBack="true" Text="Por Período:" /></td>
        </tr>
        <tr>            
            <td><PLs:PLCalendar ID="calFechaD" runat="server" LabelText="Fecha Desde:" /></td>
            <td></td>
            <td><PLs:PLCalendar ID="calFechaH" runat="server" LabelText="Fecha Hasta:" /></td>
        </tr>
        <tr>
            <td><Controls:CboQuincenas ID="cboQuincena" runat="server" LabelText="Período:" AutomaticLoad="true" /></td>
            <td><Controls:CboMes ID="cboMes" runat="server" LabelText="Mes:" AutomaticLoad="true" /></td>
            <td><Controls:CboAnio ID="cboAnio" runat="server" LabelText="Año:" AutomaticLoad="true" /></td>
        </tr>
    </table>
    </ContentTemplate>
</asp:UpdatePanel>
</fieldset>