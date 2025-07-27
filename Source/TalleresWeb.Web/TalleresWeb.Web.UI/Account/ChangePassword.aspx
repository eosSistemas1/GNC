<%@ Page Title="Cambiar contraseña" Language="C#" MasterPageFile="~/Blank.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="ChangePassword.aspx.cs" Inherits="TalleresWeb.Web.UI.Account.ChangePassword" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <div class="container">
        <div class="row" style="margin-top: 20px;">
            <div class="col-lg-3"></div>
            <div class="panel panel-default col-lg-6" style="padding:0px;">
                <div class="panel-heading">
                    <strong class="text-primary">Cambiar Contraseña</strong>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Contraseña actual:</asp:Label>
                        <asp:TextBox ID="CurrentPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword"
                            CssClass="failureNotification" ErrorMessage="Debe ingresar una contraseña." ToolTip="Ingrese contraseña actual."
                            ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" AssociatedControlID="NewPassword">Contraseña nueva:</asp:Label>
                        <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                            CssClass="failureNotification" ErrorMessage="Debe ingresar nueva contraseña." ToolTip="Ingrese contraseña nueva."
                            ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" AssociatedControlID="ConfirmNewPassword">Confirmación Contraseña:</asp:Label>
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ConfirmNewPassword"
                            CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirme contraseña nueva."
                            ToolTip="Ingrese confirmación de contraseña." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
                            CssClass="failureNotification" Display="Dynamic" ErrorMessage="La contraseña nueva y su confirmación deben coincidir."
                            ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>
                    </div>                 
                    <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification" ValidationGroup="ChangeUserPasswordValidationGroup" />
                </div>
                <div class="panel-footer text-right">
                    <CONTROLS:BtnAceptar ID="ChangePasswordPushButton" runat="server" OnClick="btnAceptar_Click" CommandName="ChangePassword" Text="Cambiar" DisableOnClick="false" ValidationGroup="ChangeUserPasswordValidationGroup" />
                    <CONTROLS:BtnCancelar ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar" OnClick="CancelPushButton_Click" />
                </div>
            </div>
            <div class="col-lg-3"></div>
        </div>
    </div>
    <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl1" />
</asp:Content>
