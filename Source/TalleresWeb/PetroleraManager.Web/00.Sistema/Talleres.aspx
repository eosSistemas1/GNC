<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Talleres.aspx.cs" Inherits="PetroleraManager.Web.Sistema.Talleres" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Talleres</legend>
        <table width="100%" border="0">
                <tr>
                    <td width="350px">
                        <PLs:PLTextBox ID="txtFiltro" runat="server" LabelText="Buscar:" />
                    </td>
                    <td width="40px">
                        <PLs:PLImageButton ID="btnBuscar" runat="server" AlternateText="Buscar" ToolTip="Buscar"
                            OnClick="btnBuscar_Click" ImageUrl="~/Imagenes/Iconos/buscar.png" CausesValidation="False" />
                    </td>
                    <td align="center">
                        <PLs:PLButton ID="btnNuevo" runat="server" Text="Nuevo" CausesValidation="false"
                            OnClick="btnNuevo_Click" />
                    </td>
                </tr>
            <tr>
                <td colspan="3">
                    <table width="100%" border="0">
                        <tr>
                            <td width="60%" valign="top">
                                <fieldset>
                                    <div style="max-height: 150px; overflow: auto;">
                                        <PLs:PLGridView ID="grdFiltro" runat="server" AutoGenerateColumns="False" Width="100%"
                                            AlternatingRowStyle-CssClass="GridAlternateRow" HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow"
                                            DataKeyNames="ID" OnRowCommand="grdFiltro_RowCommand">
                                            <Columns>
                                                <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
                                                <asp:TemplateField HeaderText="Modificar" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <PLs:PLImageButton ID="btnModificar" runat="server" AlternateText="Modificar" ImageUrl="~/Imagenes/Iconos/modificar.png"
                                                            ToolTip="Modificar" Width="20px" CommandName="modificar" CommandArgument="<%# Container.DataItemIndex %>"
                                                            CausesValidation="false" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Eliminar" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <PLs:PLImageButton ID="btnEliminar" runat="server" AlternateText="Eliminar" ImageUrl="~/Imagenes/Iconos/eliminar.png"
                                                            ToolTip="Eliminar" Width="20px" CommandName="eliminar" OnClientClick="return confirm ('Desea eliminar el item seleccionado?');"
                                                            CommandArgument="<%# Container.DataItemIndex %>" CausesValidation="false" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </PLs:PLGridView>
                                    </div>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </fieldset>
    <asp:Panel ID="pnlDatos" runat="server">
        <fieldset>
            <p style="text-align: right;">                
                <PLs:PLButton ID="btnAceptar" runat="server" Text="       Aceptar" CausesValidation="false"
                            UseSubmitBehavior="true" OnClick="btnAceptar_Click"
                            Height="35px" Style="background: transparent url(/Imagenes/Iconos/correcta.png) center left no-repeat;" />

                <PLs:PLButton ID="btnCancelar" runat="server" Text="       Cancelar" CausesValidation="false"
                            OnClientClick="return confirm('Al cancelar se perderán los cambios realizados. Desea continuar?');" Height="35px"
                            Style="background: transparent url(/Imagenes/Iconos/volver.png) center left no-repeat;"
                            OnClick="btnCancelar_Click" />
            </p>
            <PLs:PLTabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%" style="height:150px">
                <PLs:PLTabPanel runat="server" HeaderText="Descripción" ID="TabPanel1">
                    <ContentTemplate>
                        <table width="100%" border="0" style="vertical-align: top;">
                            <tr>
                                <td>
                                    <PLs:PLHidden ID="txtID" runat="server" LabelText="ID:" Enabled="false" />
                                    <PLs:PLTextBox ID="txtMatricula" runat="server" LabelText="Matrícula:" MaxLenghtTxt="7" />
                                    <PLs:PLTextBox ID="txtRazonSocial" runat="server" LabelText="Razón Social:" MaxLenghtTxt="50" />
                                    <PLs:PLTextBox ID="txtDomicilio" runat="server" LabelText="Domicilio:" MaxLenghtTxt="50" />
                                    <PLs:PLTextBox ID="txtCuit" runat="server" LabelText="CUIT:" MaxLenghtTxt="13" />
                                    <Controls:CboLocalidades ID="cboLocalidad" runat="server" AutomaticLoad="true" LabelText="Localidad:" />                                    
                                    <Controls:CboZonas ID="cboZona" runat="server" LabelText="Zona:" AutomaticLoad="true" />
                                    <PLs:PLTextBox ID="txtHorarioAtencion" runat="server" LabelText="Persona Contacto:" MaxLenghtTxt="50" />
                                </td>
                                <td>
                                    <PLs:PLTextBox ID="txtTelefono" runat="server" LabelText="Teléfono:" MaxLenghtTxt="30" />
                                    <PLs:PLTextBox ID="txtFax" runat="server" LabelText="Fax:" MaxLenghtTxt="30" />
                                    <PLs:PLTextBox ID="txtMail" runat="server" LabelText="Mail:" MaxLenghtTxt="50" />
                                    <PLs:PLTextBox ID="txtContacto" runat="server" LabelText="Persona Contacto:" MaxLenghtTxt="10" />
                                    <span>Vencimiento Contrato:<input type="text" id="calFechaVencimientoContrato" runat="server" style="width: 90px" clientidmode="Static" maxlength="10"></span>
                                    <PLs:PLTextBox ID="txtUltimoNroIntOperacion" runat="server" LabelText="Último nro. inteno operación:" MaxLenghtTxt="10" ReadOnlyTxt="true" />
                                </td> 
                            </tr>                            
                        </table>
                    </ContentTemplate>
                </PLs:PLTabPanel>
                <PLs:PLTabPanel runat="server" HeaderText="Responsabe Técnico" ID="TabPanel2" Visible="false">
                    <ContentTemplate>
                        <table width="100%" border="0" style="vertical-align: top;">                                                        
                            <tr>
                                <td>
                                    Responsabe Técnico:
                                </td>
                                <td>
                                    <PLs:PLHidden ID="txtIDTaller" runat="server" LabelText="ID:" Enabled="false" />
                                    <PLs:PLHidden ID="txtIDTalleresRT" runat="server" LabelText="ID:" Enabled="false" />
                                    <asp:DropDownList ID="cboRT" runat="server" />
                                </td>
                                <td>
                                    <span>Fecha Desde RT:<input type="text" id="calFechaDRT" runat="server" style="width: 90px" clientidmode="Static" maxlength="10"></span>
                                </td>
                                <td>
                                    <span>Fecha Hasta RT:<input type="text" id="calFechaHRT" runat="server" style="width: 90px" clientidmode="Static" maxlength="10"></span>
                                </td>
                                <td>
                                    <asp:CheckBox ID="esPrincipal" Text="Principal:" runat="server" TextAlign="Left" />
                                </td>
                                <td>
                                    <Controls:BtnAceptar ID="btnGuardarRT" runat="server" OnClick="btnGuardarRT_Click" />
                                </td>
                            </tr>
                        </table>
                        <hr />
                        <div style="max-height: 100px; overflow: auto;">
                            <PLs:PLGridView ID="grdRT" runat="server" AutoGenerateColumns="False" Width="100%"
                                AlternatingRowStyle-CssClass="GridAlternateRow" HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow"
                                DataKeyNames="ID, TalleresID, EsRTPrincipal" OnRowCommand="grdRT_RowCommand" OnRowDataBound="grdRT_RowDataBound"
                                ShowHeader="true" ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
                                    <asp:BoundField HeaderText="Fecha Desde" DataField="FechaDesdeRTT" />
                                    <asp:BoundField HeaderText="Fecha Hasta" DataField="FechaHastaRTT" />
                                    <asp:TemplateField HeaderText="Modificar" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <PLs:PLImageButton ID="btnModificar" runat="server" AlternateText="Modificar" ImageUrl="~/Imagenes/Iconos/modificar.png"
                                                ToolTip="Modificar" Width="20px" CommandName="modificar" CommandArgument="<%# Container.DataItemIndex %>"
                                                CausesValidation="false" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>                                    
                                </Columns>
                            </PLs:PLGridView>
                        </div>                                               
                    </ContentTemplate>
                </PLs:PLTabPanel>
            </PLs:PLTabContainer>
         
        </fieldset>
    </asp:Panel>

    <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl1" />

    <script type="text/javascript">
        $(function () {                
            $("#calFechaVencimientoContrato").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#calFechaDRT").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#calFechaHRT").datepicker({ dateFormat: 'dd/mm/yy' });
        });
    </script>
</asp:Content>

