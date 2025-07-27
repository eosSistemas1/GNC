<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBlank.Master" AutoEventWireup="true"
    CodeBehind="PhverResumen.aspx.cs" Inherits="PetroleraManager.Web.Tramites.PhverResumen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="background-color: White; text-align:center;">
        <table border="1" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="50%">
                    <PLs:PLLabelLabel ID="txtDatosNroOperacion" runat="server" LabelText="Nro. Operación: " />
                </td>
                <td width="50%">
                    <PLs:PLLabelLabel ID="txtDatosCodHomCilindro" runat="server" LabelText="Cod. Hom. Cil.: " />
                </td>
            </tr>
            <tr>
                <td width="50%">
                    <PLs:PLLabelLabel ID="txtDatosCliente" runat="server" LabelText="Cliente: " />
                </td>
                <td width="50%">
                    <PLs:PLLabelLabel ID="txtDatosSerieCilindro" runat="server" LabelText="Nro. Serie Cil.: " />
                </td>
            </tr>
            <tr>
                <td width="50%">
                    <PLs:PLLabelLabel ID="txtDatosDominio" runat="server" LabelText="Dominio: " />
                </td>
                <td width="50%">
                    <PLs:PLLabelLabel ID="txtDatosCodHomValvula" runat="server" LabelText="Cod. Hom. Val.: " />
                </td>
            </tr>
            <tr>
                <td width="50%">
                    <PLs:PLLabelLabel ID="txtDatosFecha" runat="server" LabelText="Fecha: " />
                </td>
                <td width="50%">
                    <PLs:PLLabelLabel ID="txtDatosSerieValvula" runat="server" LabelText="Nro. Serie Val.: " />
                </td>
            </tr>
            <tr>
                <td width="50%">
                    <PLs:PLLabelLabel ID="txtDatosTaller" runat="server" LabelText="Taller: " />
                </td>
                <td width="50%">
                    <PLs:PLLabelLabel ID="txtDatosObservacion" runat="server" LabelText="Observación: " />
                </td>
            </tr>
        </table>
        <br />        
        <table width="100%" border="1" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4">
                    Registro de pesos
                </td>
            </tr>
            <tr>
                <td width="25%" align="right">
                    &nbsp;
                </td>
                <td width="25%" align="center">
                    MARCADO
                </td>
                <td width="25%" align="right">
                    &nbsp;
                </td>
                <td width="25%" align="center">
                    ACTUAL
                </td>
            </tr>
            <tr>
                <td width="25%" align="right">
                    Peso vacío:
                </td>
                <td width="25%" align="left">
                    <asp:Label ID="lblPesoVacioMarcado" runat="server" />
                </td>
                <td width="25%" align="right">
                    Peso vacío:
                </td>
                <td width="25%" align="left">
                    <asp:Label ID="lblPesoVacioActual" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="25%" align="right">
                    &nbsp;
                </td>
                <td width="25%" align="left">
                    &nbsp;
                </td>
                <td width="25%" align="right">
                    Peso c/agua:
                </td>
                <td width="25%" align="left">
                    <asp:Label ID="lblPesoConAgua" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="25%" align="right">
                    Capacidad:
                </td>
                <td width="25%" align="left">
                    <asp:Label ID="lblCapacidadMarcado" runat="server" />
                </td>
                <td width="25%" align="right">
                    &nbsp;
                </td>
                <td width="25%" align="left">
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />        
        <table width="100%" border="1" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4">
                    Medición de espesores
                </td>
            </tr>
            <tr>
                <td width="25%" align="right">
                    Lectura Pared mín.:
                </td>
                <td width="25%" align="left">
                    <asp:Label ID="lblLectParedMin" runat="server" />
                </td>
                <td width="25%" align="right">
                    Lectura Fondo:
                </td>
                <td width="25%" align="left">
                    <asp:Label ID="lblLectFondo" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="25%" align="right">
                    Lectura Pared MÁX.:
                </td>
                <td width="25%" align="left">
                    <asp:Label ID="lblLectParedMax" runat="server" />
                </td>
                <td width="25%" align="right">
                    Tipo Fondo:
                </td>
                <td width="25%" align="left">
                    <asp:Label ID="lblFondo" runat="server" />
                </td>
            </tr>
        </table>
        <br />  
        <table width="100%" border="1" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4">
                    Prueba Hidráulica
                </td>
            </tr>
            <tr>
                <td width="25%" align="right">
                    Lectura mín. Bureta:
                </td>
                <td width="25%" align="left">
                    <asp:Label ID="lblLecturaBuretaMin" runat="server" />
                </td>
                <td width="25%" align="right">
                    Nro. Bureta:
                </td>
                <td width="25%" align="left">
                    <asp:Label ID="lblNroBureta" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="25%" align="right">
                    Lectura MÁX. Bureta:
                </td>
                <td width="25%" align="left">
                    <asp:Label ID="lblLecturaBuretaMAX" runat="server" />
                </td>
                <td width="25%" align="right">
                    Presion Prueba Cilindro:
                </td>
                <td width="25%" align="left">
                    <asp:Label ID="lblPresionPruebaCilindro" runat="server" />
                </td>
            </tr>
        </table>
        <br />        
        <table width="100%" border="1" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2" align="right" width="50%">
                    Estado:
                </td>
                <td colspan="2" align="left">
                    <b><asp:Label ID="lblEstado" runat="server" /></b>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right" width="50%">
                    <a href="#" onclick="window.print();">Imprimir</a>
                </td>
                <td colspan="2" align="left">
                    <a href="#" onclick="window.close();">Cerrar</a>
                </td>
            </tr>
        </table>
       
    </div>
</asp:Content>
