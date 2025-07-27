<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTalleres.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="TalleresWeb.Web.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--/content-->
                <div id="content">
                    <h2 class="icon-herramienta">
                        Login - Red de Talleres</h2>
                    <p>
                        Por favor complete los siguientes campos para ingresar a la Red de Talleres:</p>
                    <table width="100%" border="0" cellspacing="3" cellpadding="3">
                        <tr>
                            <td width="30%">
                                Tipo de Documento:
                            </td>
                            <td width="70%">
                                <PEARGNC:CboTiposDocumentos ID="cboTipoDoc" runat="server" CssClass="tipodocumento" AutomaticLoad="true"></PEARGNC:CboTiposDocumentos>
                            </td>
                        </tr>
                        <tr>
                            <td width="30%">
                                N&uacute;mero de Documento:
                            </td>
                            <td width="70%">
                                <asp:TextBox ID="txtNroDocumento" runat="server" CssClass="input" Width="150px"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txtNroDocumento" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="30%">
                                Nombre de Usuario:
                            </td>
                            <td width="70%">
                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="TextBoxComun" Width="150px" ></asp:TextBox>
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="txtUsuario" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="30%">
                                Contrase&ntilde;a:
                            </td>
                            <td width="70%">
                                <asp:TextBox ID="txtPass" runat="server" CssClass="input" Width="150px" 
                                    TextMode="Password"></asp:TextBox>
                                &nbsp;
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ErrorMessage="*" ControlToValidate="txtPass"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Nombre de Usuario y/o contraseña incorrecta"
                                    Visible="False"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td>Recordar usuario:</td>
                            <td><ASP:CheckBox id="chkPersistCookie" runat="server" autopostback="false" /></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td width="30%">
                                &nbsp;
                            </td>
                            <td width="70%">
                                <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="submit" 
                                    OnClick="btnIngresar_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:Label ID="lblMsj" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <!--/content-->
</asp:Content>
