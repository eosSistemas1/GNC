<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTalleres.Master" AutoEventWireup="true" CodeBehind="ConsultaEnBase.aspx.cs" Inherits="TalleresWeb.Web.TalleresWeb.Consultas.ConsultaEnBase" %>

 <%@ MasterType VirtualPath="~/MasterTalleres.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
    var newwindow;
    function popup(url) {
        newwindow = window.open(url, 'name', 'height=500,width=500,scrollbars=yes');
        if (window.focus) { newwindow.focus() }
    }
    </script>

  <h2>Consulta en base</h2>
    <asp:UpdatePanel ID="updatePanel" runat="server">
        <ContentTemplate>
            <fieldset>
                <legend>Busca Por:</legend>
                <table width="100%" border="0">
                    <tr>
                        <td align="center" width="50%">
                            &nbsp;&nbsp; - &nbsp;&nbsp;<asp:LinkButton ID="lnkOblea" runat="server" CausesValidation="False"
                                OnClick="lnkOblea_Click">Oblea</asp:LinkButton>
                        </td>
                        <td align="center" width="50%">
                            &nbsp;&nbsp; - &nbsp;&nbsp;<asp:LinkButton ID="lnkRegulador" runat="server" CausesValidation="False"
                                OnClick="lnkRegulador_Click">Regulador</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">
                            &nbsp;&nbsp; - &nbsp;&nbsp;<asp:LinkButton ID="lnkCliente" runat="server" CausesValidation="False"
                                OnClick="lnkCliente_Click">Cliente</asp:LinkButton>
                        </td>
                        <td align="center" width="50%">
                            &nbsp;&nbsp; - &nbsp;&nbsp;<asp:LinkButton ID="lnkCilindro" runat="server" CausesValidation="False"
                                OnClick="lnkCilindro_Click">Cilindro</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">
                            &nbsp;&nbsp; - &nbsp;&nbsp;<asp:LinkButton ID="lnkDominio" runat="server" CausesValidation="False"
                                OnClick="lnkDominio_Click">Dominio</asp:LinkButton>
                        </td>
                        <td align="center" width="50%">
                            &nbsp;&nbsp; - &nbsp;&nbsp;<asp:LinkButton ID="lnkValvula" runat="server" CausesValidation="False"
                                OnClick="lnkValvula_Click">Válvula</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <fieldset>
                <table width="100%" border="0">
                    <tr id="tdOblea" runat="server" visible="false">
                        <td>
                            Oblea:
                        </td>
                        <td>
                            <asp:TextBox ID="txtIdOblea" runat="server" AutoPostBack="true" MaxLength="8" OnTextChanged="txt_TextChanged"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="valOblea" runat="server" ControlToValidate="txtIdOblea"
                                Display="Dynamic" Text="*" ErrorMessage="Debe ingresar dato a buscar." SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="valRangoOblea" runat="server" Type="Integer" MinimumValue="0"
                                MaximumValue="99999999" ControlToValidate="txtIdOblea" Display="Dynamic" Text="*"
                                ErrorMessage="El nro de oblea no es válido." SetFocusOnError="True"></asp:RangeValidator>
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr id="tdCliente" runat="server" visible="false">
                        <td>
                            Tipo doc:
                        </td>
                        <td>
                            <PEARGNC:CboTiposDocumentos ID="cboTipoDoc" runat="server" AutomaticLoad="true"/>
                        </td>
                        <td>
                            Nro doc.:
                        </td>
                        <td>
                            <asp:TextBox ID="txtNroDoc" runat="server" MaxLength="8"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNroDoc"
                                Display="Dynamic" Text="*" ErrorMessage="Debe ingresar dato a buscar." SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" Type="Integer" MinimumValue="0"
                                MaximumValue="99999999" ControlToValidate="txtNroDoc" Display="Dynamic" Text="*"
                                ErrorMessage="El nro de documento no es válido." SetFocusOnError="True"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr id="tdRegulador" runat="server" visible="false">
                        <td width="50%" colspan="2">
                            Serie Regulador:
                        </td>
                        <td width="50%" colspan="2">
                            <asp:TextBox ID="txtSerieReg" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="valSerie" runat="server" ControlToValidate="txtSerieReg"
                                Display="Dynamic" Text="*" ErrorMessage="Debe ingresar dato a buscar." SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <br />
                            <PEARGNC:CboMarcasREG ID="cboMarcaReg" runat="server" AutomaticLoad="true" IsComboFilter="true"/>
                        </td>
                    </tr>
                    <tr id="tdCilindro" runat="server" visible="false">
                        <td width="50%" colspan="2">
                            Serie Cilindro:
                        </td>
                        <td width="50%" colspan="2">
                            <asp:TextBox ID="txtSerieCil" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSerieCil"
                                Display="Dynamic" Text="*" ErrorMessage="Debe ingresar dato a buscar." SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <br />
                            <PEARGNC:CboMarcasCIL ID="cboMarcaCil" runat="server" AutomaticLoad="true" IsComboFilter="true"/>
                        </td>
                    </tr>
                    <tr id="tdValvula" runat="server" visible="false">
                        <td width="50%" colspan="2">
                            Serie Válvula:
                        </td>
                        <td width="50%" colspan="2">
                            <asp:TextBox ID="txtSerieVal" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSerieVal"
                                Display="Dynamic" Text="*" ErrorMessage="Debe ingresar dato a buscar." SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <br />
                            <PEARGNC:CboMarcasVAL ID="cboMarcaVal" runat="server" AutomaticLoad="true" IsComboFilter="true"/>
                        </td>
                    </tr>
                    <tr id="tdDominio" runat="server" visible="false">
                        <td>
                            Dominio:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDomino" MaxLength="6" runat="server" AutoPostBack="true" OnTextChanged="txt_TextChanged"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDomino"
                                Display="Dynamic" Text="*" ErrorMessage="Debe ingresar dato a buscar." SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="valFormatoDominio" runat="server" ErrorMessage="El Formato del Dominio es incorrecto."
                                Text="*" ControlToValidate="txtDomino" Display="Dynamic" ValidationExpression="^([a-zA-Z]{3})([0-9-]{3})"></asp:RegularExpressionValidator>
                            &nbsp;
                        </td>
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="4">
                            <asp:Button ID="btnNuevaBusq" runat="server" Text="Nueva Búsqueda" Visible="false"
                                OnClick="btnNuevaBusq_Click" CausesValidation="False" />
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" Visible="false" CausesValidation="true"
                                OnClick="btnAceptar_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <table width="100%" border="0">
                    <tr id="tdResultados" runat="server" visible="false">
                        <td>
                            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                DataKeyNames="ID" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:BoundField HeaderText="Fecha Operación" DataField="FechaHabilitacion" HeaderStyle-BorderWidth="1">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Dominio" DataField="Dominio" HeaderStyle-BorderWidth="1">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Cliente" DataField="NombreyApellido" HeaderStyle-BorderWidth="1">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Nro Oblea Anterior" DataField="NroObleaAnterior" HeaderStyle-BorderWidth="1">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Nro Oblea Nueva" DataField="NroObleaNueva" HeaderStyle-BorderWidth="1">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Ver Detalle" HeaderStyle-BorderWidth="1">
                                        <ItemTemplate>
                                            <center>
                                                <asp:ImageButton ID="btnDetalle" runat="server" ImageUrl="~/Images/info.png" /></center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="trMensaje" align="center" runat="server" visible="false">
                        <td>
                            <asp:Label ID="lblMsjSinResultados" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
