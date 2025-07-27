<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="red-de-talleres.aspx.cs" Inherits="TalleresWeb.Web.red_de_talleres" %>

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
    <div id="content">
        <h2 class="icon-herramienta">
            Red de Talleres</h2>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <h4>
                    Buscador de Talleres:</h4>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            Provincia:
                        </td>
                        <td>
                            <asp:DropDownList ID="cboProvincias" runat="server" Width="150px" OnSelectedIndexChanged="cboProvincias_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                ControlToValidate="cboProvincias" InitialValue="-1"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            Localidad:
                        </td>
                        <td>
                            <asp:DropDownList ID="cboLocalidad" runat="server" Width="150px">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                ControlToValidate="cboLocalidad" InitialValue="-1"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            Zona:
                        </td>
                        <td>
                            <asp:DropDownList ID="cboZonas" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center">
                            <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" CssClass="submit"
                                Text="Buscar" />
                        </td>
                    </tr>
                </table>
                <h4>
                    <asp:Label ID="lblResultados" runat="server" Text=""></asp:Label></h4>
                <div style="overflow: auto;">
                    <asp:GridView ID="grdResultados" runat="server" AutoGenerateColumns="False" Visible="False"
                        BorderWidth="1" Width="100%" OnRowDataBound="grdResultados_RowDataBound" DataKeyNames="IdTaller,MailTaller">
                        <Columns>
                            <asp:BoundField DataField="RazonSocialTaller" HeaderText="Taller" ItemStyle-BorderWidth="1" />
                            <asp:BoundField DataField="DomicilioTaller" HeaderText="Domicilio" ItemStyle-BorderWidth="1" />
                            <asp:BoundField DataField="TelefonoTaller" HeaderText="Teléfono" ItemStyle-BorderWidth="1" />
                            <asp:BoundField DataField="HorarioDeAtencion" HeaderText="Horario de Atención" ItemStyle-BorderWidth="1" />
                            <asp:TemplateField HeaderText="Enviar Consulta" ItemStyle-BorderWidth="1">
                                <ItemTemplate>
                                    <center>
                                        <asp:ImageButton ID="btnMail" runat="server" ImageUrl="~/Images/mailing.png" AlternateText="Enviar Mail" /></center>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
