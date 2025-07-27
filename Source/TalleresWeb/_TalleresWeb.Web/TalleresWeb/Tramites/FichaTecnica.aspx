<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTalleres.Master" AutoEventWireup="true"
    CodeBehind="FichaTecnica.aspx.cs" Inherits="TalleresWeb.Web.FichaTecnica" %>

<%@ Register Src="~/UserControls/uscCargarVehiculo.ascx" TagName="uscCargarVehiculo" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/uscCargarCliente.ascx" TagName="uscCargarCliente" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/uscCargarReguladores.ascx" TagName="uscCargarReguladores" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/uscCargarCilindrosValvulas.ascx" TagName="uscCargarCilindrosValvulas" TagPrefix="uc4" %>
<%@ Register src="~/UserControls/MessageBoxCtrl.ascx" tagname="MessageBoxCtrl" tagprefix="uc5" %>
<%@ Register src="~/UserControls/BuscarTaller.ascx" tagname="BuscarTaller" tagprefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc6:BuscarTaller ID="BuscarTaller1" runat="server"  OnGridTalleresButtonClick="Talleres_Click" Visible="false"/>

    <asp:Accordion ID="MyAccordion" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeaderOblea"
        HeaderSelectedCssClass="accordionHeaderSelectedOblea" ContentCssClass="accordionContent" Height="500px"
        FadeTransitions="true" FramesPerSecond="800" TransitionDuration="10" AutoSize="Limit"
        RequireOpenedPane="true" SuppressHeaderPostbacks="True">
         <Panes>
            <asp:AccordionPane ID="pane1" runat="server" >
                <Header>
                    <a href="">TIPO DE OPERACION, NRO. DE OBLEA Y FECHA</a>
                    <asp:Image ID="Image1" ImageUrl="~/Images/add.png" AlternateText="" runat="server" />
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </Header>
                <Content>
                   <table width="100%">
                        <tr>
                            <td colspan="2">
                                <PLs:PLTextBoxMasked ID="calFecha" Mask="99/99/9999" runat="server" MaskType="Date" LabelText="Fecha:" Width="100%" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <PEARGNC:CboTiposOperaciones ID="cboTipoOperacion" runat="server" AutomaticLoad="true" LabelText="Seleccione Tipo de Operación: " 
                                            AutoPostback="true" OnOnSelectedIndexChange="changeTipoOP" />
                            </td>
                        </tr>
                        <tr>
                            <td width="90%">
                                <PLs:PLTextBoxMasked ID="txtNroObleaAnterior" Mask="99999999" runat="server" MaskType="Number" LabelText="Nro. Anterior:" Visible="false" AutoPostBack="false" />
                            </td>
                            <td width="10%">
                                <asp:Button ID="btnBuscarOblea" runat="server" Text="Buscar" CausesValidation="false" Visible="false" OnClick="btnBuscarOblea_Click" />
                            </td>
                        </tr>
                   </table>
                </Content>
            </asp:AccordionPane>
            <asp:AccordionPane ID="pane2" runat="server">
                <Header>
                    <a href="">CLIENTE y VEHICULO</a>
                    <asp:Image ID="Image2" ImageUrl="~/Images/add.png" AlternateText="" runat="server" />
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                </Header>
                <Content>
                   <br />
                   <uc1:uscCargarVehiculo ID="uscCargarVehiculo1" runat="server" />
                   <br />
                   <uc2:uscCargarCliente ID="uscCargarCliente1" runat="server" />
                   <br />
                </Content>
            </asp:AccordionPane>
            <asp:AccordionPane ID="pane3" runat="server">
                <Header>
                    <a href="">REGULADORES</a>
                    <asp:Image ID="Image3" ImageUrl="~/Images/add.png" AlternateText="" runat="server" />
                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                </Header>
                <Content>
                    <uc3:uscCargarReguladores ID="uscCargarReguladores1" runat="server" />
                </Content>
            </asp:AccordionPane>
            <asp:AccordionPane ID="pane4" runat="server">
                <Header>
                    <a href="">CILINDROS Y VALVULAS</a>
                    <asp:Image ID="Image4" ImageUrl="~/Images/add.png" AlternateText="" runat="server" />
                    <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                </Header>
                <Content>
                    <uc4:uscCargarCilindrosValvulas ID="uscCargarCilindrosValvulas1" runat="server" />
                </Content>
            </asp:AccordionPane>
        </Panes>
    </asp:Accordion>
    <table width="100%" border="0">
        <tr>
            <td valign="top">
                <fieldset class="aField">
                <legend>OBSERVACIONES</legend>
                    <PLs:PLTextField ID="txtObservaciones" runat="server" Rows="5" WidthTxt="100%" />
                </fieldset>
            </td>
            <td align="center">             
                <fieldset class="aField" style="min-height:76px"><br /><br />
                    <PLs:PLCheckBox ID="chkRealizaPH" runat="server" Text="Realiza PH?" /> &nbsp;&nbsp;
                    <PLs:PLButton ID="lnkAceptar" runat="server" Text="       Enviar/Imprimir" CausesValidation="false"
                        OnClientClick="this.disabled=true" UseSubmitBehavior="false" OnClick="lnkAceptar_Click"
                        Height="35px" Style="background: transparent url(/Images/Iconos/correcta.png) center left no-repeat;" />
                    <PLs:PLButton ID="lnkCancelar" runat="server" Text="       Cancelar" CausesValidation="false"
                        OnClientClick="window.location='inicio.aspx';" Height="35px" 
                        Style="background: transparent url(/Images/Iconos/volver.png) center left no-repeat;" 
                        onclick="lnkCancelar_Click" />
                </fieldset>
            </td>
        </tr>
    </table>
    <uc5:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />
</asp:Content>
