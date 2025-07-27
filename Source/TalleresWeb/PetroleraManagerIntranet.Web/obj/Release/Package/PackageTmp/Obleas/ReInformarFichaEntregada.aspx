<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReInformarFichaEntregada.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Obleas.ReInformarFichaEntregada" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagName="MessageBoxCtrl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main id="central" role="main" onscroll="document.getElementById('hdnSDetCobcrollTop').value = this.scrollTop;">
        <div id="contenido">
            <div class="row">
                <div class="col-sm-12">
                    <h4>REIMPRIMIR TARJETA VERDE</h4>
                </div>
                <hr>
            </div>
            <div class="row">
                <div class="col-sm-3">Número de oblea (nueva):</div>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtNroOblea" runat="server" onKeyPress="return soloNumeros(event)" />
                </div>
                <div class="col-sm-3">Dominio:</div>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtDominio" runat="server" />
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-8 text-right"></div>
                <div class="col-sm-2 text-right">
                    <button type="button" class="btn btn-primary btn-block nn" id="btnAceptar" runat="server" onserverclick="btnAceptar_Click" title="Aceptar" alt="Aceptar"><i class="fa fa-check" aria-hidden="true"></i>&nbsp Aceptar</button>
                </div>
                <div class="col-sm-2 text-right">
                    <button type="button" class="btn btn-danger btn-block nn" id="btnCancelar" runat="server" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp Cancelar</button>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <asp:GridView ID="grdObleas" runat="server" AutoGenerateColumns="False" Width="100%"
                        AlternatingRowStyle-CssClass="GridAlternateRow" HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow"
                        DataKeyNames="ID" OnRowDataBound="grdObleas_DataBound" OnRowCommand="grdObleas_RowCommand">
                        <Columns>
                            <asp:BoundField HeaderText="Dominio" DataField="Dominio" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Oblea Interno Op." DataField="NroInternoOpercion" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Oblea Nueva" DataField="NroObleaNueva" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Fecha Habilitación" DataField="FechaHabilitacion" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <Controls:BtnModificar ID="btnAceptarReInformar" runat="server" CausesValidation="false" CommandArgument='<%# Eval("ID") %>' CommandName="modificar" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </main>

    <uc1:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />

</asp:Content>
