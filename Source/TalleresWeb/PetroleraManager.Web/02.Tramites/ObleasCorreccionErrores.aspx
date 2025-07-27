<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ObleasCorreccionErrores.aspx.cs" Inherits="PetroleraManager.Web.Tramites.ObleasCorreccionErrores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h2>
        <PLs:PLLabel ID="lblTituloPagina" runat="server" Text="Corrección errores de obleas:"></PLs:PLLabel></h2>

    <fieldset>
        <legend>Obleas Con Error:</legend>
        <div style="max-height: 150px; overflow: auto;">
            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="100%"
                AlternatingRowStyle-CssClass="GridAlternateRow" HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow"
                DataKeyNames="ID" OnRowCommand="grd_RowCommand">
                <Columns>
                    <asp:BoundField HeaderText="Oblea Asignada" DataField="NroObleaNueva" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Fecha" DataField="FechaHabilitacion" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Taller" DataField="Taller" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Dominio" DataField="Dominio" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Operación" DataField="Operacion" ItemStyle-HorizontalAlign="Center" />
                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Iconos/seleccionar.png" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" CommandName="seleccionar" />
                </Columns>
            </asp:GridView>
        </div>
    </fieldset>

    <div style="position: fixed; left: 0; top: 0;">
        <PLs:PLModalPopupExtender ID="modalExt" runat="server" TargetControlID="btnTarget" PopupControlID="modal"
            BackgroundCssClass="modalBackground" DropShadow="true" CancelControlID="btnCancel"
            CacheDynamicResults="false">
        </PLs:PLModalPopupExtender>

        <div style="display: none;">
            <asp:Button ID="btnTarget" runat="server" Text="Cancelar" />
            <asp:HiddenField ID="hdnObleaSeleccionadaID" runat="server" />
        </div>

        <asp:Panel ID="modal" runat="server" CssClass="CajaDialogo" Style="display: none; width: 800px; height: 400px;">
            <div class="row">
                <h4 class="modal-title">
                    <asp:Label ID="lblTituloMsj" runat="server" Text="" />
                </h4>
                <hr />
                <div>
                    <fieldset>
                        <legend>Error <b>Dominio</b></legend>
                        <table>
                            <tr>
                                <td><asp:CheckBox ID="chkErrorDominio" runat="server" /></td>
                                <td>Nuevo Dominio:</td>
                                <td><asp:TextBox ID="txtDominio" runat="server" /></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend>Error <b>Regulador</b></legend>
                        <table>
                            <tr>
                                <td><asp:CheckBox ID="chkErrorREG" runat="server" /></td>
                                <td>Nuevo Cód. Homologación:</td>
                                <td><asp:TextBox ID="txtCodHomoREG" runat="server" /></td>
                                <td>Nuevo Nro. Serie:</td>
                                <td><asp:TextBox ID="txtSerieREG" runat="server" /></td>
                            </tr>
                        </table>
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend>Error <b>Cilindro</b></legend>
                        <table>
                            <tr>
                                <td><asp:CheckBox ID="chkErrorCIL" runat="server" /></td>
                                <td>Nuevo Cód. Homologación:</td>
                                <td><asp:TextBox ID="txtCodHomoCIL" runat="server" /></td>
                                <td>Nuevo Nro. Serie:</td>
                                <td><asp:TextBox ID="txtSerieCIL" runat="server" /></td>
                            </tr>
                        </table>
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend>Error <b>Válvula</b></legend>
                        <table>
                            <tr>
                                <td><asp:CheckBox ID="chkErrorVAL" runat="server" /></td>
                                <td>Nuevo Cód. Homologación:</td>
                                <td><asp:TextBox ID="txtCodHomoVAL" runat="server" /></td>
                                <td>Nuevo Nro. Serie:</td>
                                <td><asp:TextBox ID="txtSerieVAL" runat="server" /></td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </div>            
            <br />
            <div class="row center-block text-center" style="width:100%; text-align:center">               
                <Controls:BtnAceptar ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" />
                <Controls:BtnCancelar ID="btnCancel" runat="server" CssClass="close" />                    
            </div>
        </asp:Panel>
    </div>
</asp:Content>
