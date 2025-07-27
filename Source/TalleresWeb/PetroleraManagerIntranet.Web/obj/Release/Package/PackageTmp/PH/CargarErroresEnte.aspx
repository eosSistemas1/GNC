<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="CargarErroresEnte.aspx.cs" Inherits="PetroleraManagerIntranet.Web.PH.CargarErroresEnte" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div id="contenido">
        <div class="row">
            <div class="col-sm-12">
                <h4>CARGAR ERRORES ENTE</h4>
            </div>
            <hr />
        </div>

        <div class="col-sm-12">
            <div class="col-sm-12">
                <h4>
                    <%--<asp:Label ID="lblTitulo" Text="text" runat="server" />--%></h4>
                <%--<table style="width: 100%">
                        <tr>
                            <td>
                                <fieldset>
                                    <legend>Arcvhivo ente <span style="font-weight: bold; color: green">OK</span></legend>
                                    <asp:FileUpload ID="fuArchivoOK" runat="server" />
                                </fieldset>
                            </td>
                            <td>--%>
                <fieldset>
                    <legend>Arcvhivo ente <span style="font-weight: bold; color: red">ERRORES</span></legend>
                    <asp:FileUpload ID="fuArchivoErrores" runat="server" />
                </fieldset>
                <%--</td>
                        </tr>
                    </table>--%>
            </div>
            <div class="col-sm-12 pull-right">
                <div class="col-sm-6 no-padding">
                    <button type="button" class="btn btn-primary btn-block nn" id="btnAceptar" runat="server" onserverclick="btnAceptar_ServerClick" title="Aceptar" alt="Aceptar"><i class="fa fa-check" aria-hidden="true"></i>&nbsp Aceptar</button>
                </div>
                <div class="col-sm-6 no-padding">
                    <button type="button" class="btn btn-danger btn-block nn" id="btnCancelar" runat="server" onserverclick="btnCancelar_ServerClick" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp Cancelar</button>
                </div>
            </div>
        </div>              

        <div class="clearfix"></div>        

        <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl1" />

    </div>




</asp:Content>
