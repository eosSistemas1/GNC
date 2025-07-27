<%@ Page Title="" Language="C#" MasterPageFile="~/PopUpMaster.Master" AutoEventWireup="true" CodeBehind="DetalleConsulta.aspx.cs" Inherits="TalleresWeb.Web.DetalleConsulta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table border="0" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right">
                <b><a onclick="window.close();" onmouseover="this.style.cursor='hand';">(x)Cerrar</a></b>&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td>
               <h3>Oblea: <asp:Label ID="lblOblea" runat="server" Text=""></asp:Label></h3>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="15%">
                            <b><asp:Label ID="Label28" runat="server" Text="Fec. Hab:"></asp:Label></b>
                        </td>
                        <td width="35%">
                            <asp:Label ID="lblHabilitacion" runat="server"></asp:Label>
                        </td>
                        <td width="15%">
                            <b><asp:Label ID="Label30" runat="server" Text="Fec. Venc.:"></asp:Label></b>
                        </td>
                        <td width="35%">
                            <asp:Label ID="lblVencimiento" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="15%">
                            <b><asp:Label ID="Label1" runat="server" Text="Taller:"></asp:Label></b>
                        </td>
                        <td width="35%">
                            <asp:Label ID="lblTaller" runat="server"></asp:Label>
                        </td>
                        <td width="15%">
                           <b><asp:Label ID="Label3" runat="server" Text="Matricula:"></asp:Label></b>
                        </td>
                        <td width="35%">
                            <asp:Label ID="lblTallerMatricula" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <h5><b>Vehiculo:</b></h5>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="15%">
                            <b><asp:Label ID="Label4" runat="server" Text="Dominio:"></asp:Label></b>
                        </td>
                        <td width="35%">
                            <asp:Label ID="lblDominio" runat="server"></asp:Label>
                        </td>
                        <td width="15%">
                            <b><asp:Label ID="Label6" runat="server" Text="Marca:"></asp:Label></b>
                        </td>
                        <td width="35%">
                            <asp:Label ID="lblMarca" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="15%">
                            <b><asp:Label ID="Label2" runat="server" Text="Modelo:"></asp:Label></b>
                        </td>
                        <td width="35%">
                            <asp:Label ID="lblModelo" runat="server"></asp:Label>
                        </td>
                        <td width="15%">
                            <b><asp:Label ID="Label31" runat="server" Text="Año:"></asp:Label></b>
                        </td>
                        <td width="35%">
                            <asp:Label ID="lblAnio" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <h5><b>Cliente:</b></h5>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="15%">
                           <b><asp:Label ID="Label9" runat="server" Text="Documento:"></asp:Label></b>
                        </td>
                        <td width="35%">
                            <asp:Label ID="lblDocumento" runat="server"></asp:Label>
                        </td>
                        <td width="15%">
                            <b><asp:Label ID="Label11" runat="server" Text="Cliente:"></asp:Label></b>
                        </td>
                        <td width="35%">
                            <asp:Label ID="lblNomApe" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="15%">
                            <b><asp:Label ID="Label5" runat="server" Text="Domicilio:"></asp:Label></b>
                        </td>
                        <td width="35%">
                            <asp:Label ID="lblDomicilio" runat="server"></asp:Label>
                        </td>
                        <td width="15%">
                            <b><asp:Label ID="Label8" runat="server" Text="Telefono:"></asp:Label></b>
                        </td>
                        <td width="35%">
                            <asp:Label ID="lblTelefono" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <h5><b>Regulador:</b></h5>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="4" width="100%">
                            <asp:GridView ID="grdReguladores" runat="server" Width="100%" AutoGenerateColumns="false"
                                OnRowDataBound="grdRegulador_RowDataBound" BorderWidth="1">
                                <Columns>
                                    <asp:BoundField DataField="IdReguladorUnidad" HeaderText="CODIGO" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="" HeaderText="SERIE" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="IdOperacion" HeaderText="MSDB" ItemStyle-HorizontalAlign="Center"/>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <h5><b>Cilindros:</b></h5>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="4" width="100%">
                            <asp:GridView ID="grdCilindros" runat="server" Width="100%" AutoGenerateColumns="false"
                                OnRowDataBound="grdCilindros_RowDataBound" BorderWidth="1">
                                <Columns>
                                    <asp:BoundField DataField="IdCilindroUnidad" HeaderText="CODIGO" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="" HeaderText="SERIE" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="" HeaderText="FAB. MES" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="" HeaderText="FAB. AÑO" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="MesUltimaRevisionCil" HeaderText="REV. MES" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="AnioUltimaRevisionCil" HeaderText="REV. AÑO" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="IdCRPC" HeaderText="CRPC" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="IdOperacion" HeaderText="MSDB" ItemStyle-HorizontalAlign="Center"/>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <h5><b>Valvulas:</b></h5>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="4" width="100%">
                            <asp:GridView ID="grdValvulas" runat="server" Width="100%" AutoGenerateColumns="false"
                                OnRowDataBound="grdValvulas_RowDataBound" BorderWidth="1">
                                <Columns>
                                    <asp:BoundField DataField="IdValvulaUnidad" HeaderText="CODIGO" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="" HeaderText="SERIE" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="IdOperacion" HeaderText="MSDB" ItemStyle-HorizontalAlign="Center"/>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                
                <b><a id="lnkSolicitarOblea" runat="Server"  style="cursor:pointer">Solicitar Oblea</a></b>&nbsp;&nbsp;&nbsp;
                <b><a onclick="window.print();" onmouseover="this.style.cursor='hand';">Imprimir</a></b>&nbsp;&nbsp;&nbsp;
                <b><a onclick="window.close();" onmouseover="this.style.cursor='hand';">(x)Cerrar</a></b>&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
