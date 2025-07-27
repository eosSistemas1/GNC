<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Blank.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="PetroleraManager.Web.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <center>
    <br />
    <h2>
        <PLs:PLImage ID="imgLogo" runat="server" ImageUrl="~/Imagenes/logo.png" Height="60px"/>
        <%--<PLs:PLLabel ID="lblEmpresa" runat="server" />--%>
    </h2>
    <br />
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false">
        <LayoutTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <div class="accountInfo">
                <fieldset class="login">
                    <legend>Ingrese su Usuario y contraseña.</legend>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Usuario:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                    CssClass="failureNotification" ErrorMessage="Ingrese Usuario." ToolTip="Ingrese Usuario."
                                    ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Contraseña:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                    CssClass="failureNotification" ErrorMessage="Ingrese contraseña." ToolTip="Ingrese contraseña."
                                    ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:CheckBox ID="RememberMe" runat="server" />
                                <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Recordarme.</asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <p class="submitButton">
                    <PLs:PLButton ID="btnLogin" runat="server" Text="Ingresar" ValidationGroup="LoginUserValidationGroup" onclick="btnLogin_Click" />
                </p>
                <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                ValidationGroup="LoginUserValidationGroup" />
            </div>
        </LayoutTemplate>
    </asp:Login>
    <asp:Label ID="msjError" runat="server" CssClass="failureNotification"></asp:Label>
    </center>
</asp:Content>
