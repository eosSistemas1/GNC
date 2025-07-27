<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTalleres.Master" AutoEventWireup="true" CodeBehind="ConsultaEstadoTramite.aspx.cs" Inherits="TalleresWeb.Web.ConsultaEstadoTramite" %>
<%@ Register src="../../UserControls/MessageBoxCtrl.ascx" tagname="MessageBoxCtrl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <h2>
        Trámites Pendientes.</h2>
    <p>
        Haga Clic en el Título para ver los trámites.
    </p>
    <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <fieldset>
                <asp:Accordion ID="MyAccordion" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                    <Panes>
                        <asp:AccordionPane ID="paneAnteriores" runat="server">
                            <Header>
                                <asp:Label ID="lblTituloAnteriores" runat="server" Text=""></asp:Label>
                            </Header>
                            <Content>
                                <div>
                                    <asp:GridView ID="grdAnteriores" runat="server" Width="100%" AutoGenerateColumns="False"
                                        OnRowCommand="grd_RowCommand" OnRowDataBound="grd_RowDataBound"
                                        DataKeyNames="ID, IdEstadoFicha, Observacion">
                                        <Columns>
                                            <asp:BoundField DataField="FechaHabilitacion" ItemStyle-HorizontalAlign="center" HeaderText="Fecha Habilitación" />
                                            <asp:BoundField DataField="Descripcion" ItemStyle-HorizontalAlign="center" HeaderText="Nro. Oblea Ant." />
                                            <asp:BoundField DataField="Dominio" ItemStyle-HorizontalAlign="center" HeaderText="Dominio" />
                                            <asp:BoundField DataField="NombreyApellido" ItemStyle-HorizontalAlign="Left" HeaderText="Nombre y Apellido" />
                                            <asp:BoundField DataField="Telefono" HeaderText="Teléfono" ItemStyle-HorizontalAlign="center" />
                                            <asp:TemplateField HeaderText="Obs.">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:ImageButton ID="btnObs" CommandName="observaciones" runat="server" CommandArgument='<%# Bind("Observacion") %>'
                                                            BackColor="Transparent" AlternateText="Ver Observaciones" ImageUrl="~/Images/Iconos/Observaciones.png"
                                                            Width="20px" /></center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Modificar">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:ImageButton ID="btnModifica" runat="server" BackColor="Transparent" AlternateText="Modificar"
                                                            ImageUrl="~/Images/Iconos/Modificar2.png" Width="20px" Enabled="false" /></center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Eliminar">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:ImageButton ID="btnEliminar" CommandName="eliminar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                            BackColor="Transparent" AlternateText="Eliminar" OnClientClick="return confirm ('Desea eliminar la ficha seleccionada?');"
                                                            ImageUrl="~/Images/Iconos/Eliminar2.png" Width="20px"/></center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reimprimir">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:ImageButton ID="btnReimprimir" runat="server" AlternateText="Reimprimir Ficha"
                                                            BackColor="Transparent" ImageUrl="~/Images/Iconos/imprimir.png" Width="20px" /></center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Estado" ItemStyle-HorizontalAlign="center" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </Content>
                        </asp:AccordionPane>
                        <asp:AccordionPane ID="paneAyer" runat="server">
                            <Header>
                                <asp:Label ID="lblTituloAyer" runat="server" Text=""></asp:Label>
                            </Header>
                            <Content>
                                <div>
                                    <asp:GridView ID="grdAyer" runat="server" Width="100%" AutoGenerateColumns="False"
                                        OnRowCommand="grd_RowCommand" OnRowDataBound="grd_RowDataBound" 
                                        DataKeyNames="ID, IdEstadoFicha, Observacion">
                                        <Columns>
                                            <asp:BoundField DataField="FechaHabilitacion" ItemStyle-HorizontalAlign="center"
                                                HeaderText="Fecha Habilitación" />
                                            <asp:BoundField DataField="NroObleaAnterior" ItemStyle-HorizontalAlign="center" HeaderText="Nro. Oblea Ant." />
                                            <asp:BoundField DataField="IdVehiculo" ItemStyle-HorizontalAlign="center" HeaderText="Dominio" />
                                            <asp:BoundField DataField="IdCliente" ItemStyle-HorizontalAlign="Left" HeaderText="Nombre y Apellido" />
                                            <asp:BoundField HeaderText="Teléfono" ItemStyle-HorizontalAlign="center" />
                                            <asp:TemplateField HeaderText="Obs.">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:ImageButton ID="btnObs" CommandName="observaciones" runat="server" CommandArgument='<%# Bind("Observacion") %>'
                                                            BackColor="Transparent" AlternateText="Ver Observaciones" ImageUrl="~/Images/Iconos/Observaciones.png"
                                                            Width="20px" /></center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Modificar">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:ImageButton ID="btnModifica" runat="server" BackColor="Transparent" AlternateText="Modificar"
                                                            ImageUrl="~/Images/Iconos/Modificar2.png" Width="20px" /></center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Eliminar">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:ImageButton ID="btnEliminar" CommandName="eliminar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                            BackColor="Transparent" AlternateText="Eliminar" OnClientClick="return confirm ('Desea eliminar la ficha seleccionada?');"
                                                            ImageUrl="~/Images/Iconos/Eliminar2.png" Width="20px" /></center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reimprimir">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:ImageButton ID="btnReimprimir" runat="server" AlternateText="Reimprimir Ficha"
                                                            BackColor="Transparent" ImageUrl="~/Images/Iconos/imprimir.png" Width="20px" /></center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Estado" ItemStyle-HorizontalAlign="center" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </Content>
                        </asp:AccordionPane>
                        <asp:AccordionPane ID="paneHoy" runat="server">
                            <Header>
                                <asp:Label ID="lblTituloHoy" runat="server" Text=""></asp:Label>
                            </Header>
                            <Content>
                                <div>
                                    <asp:GridView ID="grdHoy" runat="server" Width="100%" AutoGenerateColumns="False"
                                        OnRowCommand="grd_RowCommand" OnRowDataBound="grd_RowDataBound" 
                                        DataKeyNames="ID, IdEstadoFicha, Observacion">
                                        <Columns>
                                            <asp:BoundField DataField="FechaHabilitacion" ItemStyle-HorizontalAlign="center"
                                                HeaderText="Fecha Habilitación" />
                                            <asp:BoundField DataField="NroObleaAnterior" ItemStyle-HorizontalAlign="center" HeaderText="Nro. Oblea Ant." />
                                            <asp:BoundField DataField="IdVehiculo" ItemStyle-HorizontalAlign="center" HeaderText="Dominio" />
                                            <asp:BoundField DataField="IdCliente" ItemStyle-HorizontalAlign="Left" HeaderText="Nombre y Apellido" />
                                            <asp:BoundField HeaderText="Teléfono" ItemStyle-HorizontalAlign="center" />
                                            <asp:TemplateField HeaderText="Obs.">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:ImageButton ID="btnObs" CommandName="observaciones" runat="server" 
                                                        CommandArgument='<%# Bind("Observacion") %>' 
                                                        BackColor="Transparent" AlternateText="Ver Observaciones"
                                                        ImageUrl="~/Images/Iconos/Observaciones.png" Width="20px" /></center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Modificar">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:ImageButton ID="btnModifica" runat="server" 
                                                        BackColor="Transparent"  AlternateText="Modificar"
                                                        ImageUrl="~/Images/Iconos/Modificar2.png" Width="20px" /></center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Eliminar">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:ImageButton ID="btnEliminar" CommandName="eliminar" runat="server"
                                                        CommandArgument="<%# Container.DataItemIndex %>" BackColor="Transparent"
                                                        AlternateText="Eliminar" 
                                                        OnClientClick="return confirm ('Desea eliminar la ficha seleccionada?');"
                                                                 ImageUrl="~/Images/Iconos/Eliminar2.png" Width="20px"/></center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reimprimir">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:ImageButton ID="btnReimprimir" runat="server" 
                                                        AlternateText="Reimprimir Ficha" BackColor="Transparent"
                                                        ImageUrl="~/Images/Iconos/imprimir.png" Width="20px" /></center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Estado" ItemStyle-HorizontalAlign="center" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </Content>
                        </asp:AccordionPane>
                    </Panes>
                </asp:Accordion>
            </fieldset>
            <asp:Panel ID="pnlPopUp" runat="server" CssClass="CajaDialogo" Style="display: none;">
                <div style="width: 200px; height: 100px; background-color: White;">
                    Observaciones:<br />
                    <br />
                    <asp:Label ID="lblObs" runat="server" Text=""></asp:Label><br />
                    <br />
                    <p style="text-align: right;">
                        <asp:LinkButton ID="lnkCancelPopUp" runat="server">Cerrar</asp:LinkButton></p>
                </div>
            </asp:Panel>
            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                DropShadow="false" PopupControlID="pnlPopUp" CancelControlID="lnkCancelPopUp"
                TargetControlID="btnTrigger" PopupDragHandleControlID="pnlPopUp">
            </asp:ModalPopupExtender>
            <asp:Button ID="btnTrigger" runat="server" Style="display: none" />
        <uc1:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
