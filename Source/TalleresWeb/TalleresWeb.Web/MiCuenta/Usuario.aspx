<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTalleres.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="TalleresWeb.Web.MiCuenta.Usuario" %>

<%@ Register src="~/UserControls/MessageBoxCtrl.ascx" tagname="MessageBoxCtrl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<PLs:PLTabContainer ID="tab" runat="server">
    <PLs:PLTabPanel ID="pnlUsuario" runat="server">
        <HeaderTemplate>Datos de Usuario</HeaderTemplate>
        <ContentTemplate>
            <fieldset>
            <PLs:PLHidden ID="hddUsuarioID" runat="server" />
            <PLs:PLTextBox ID="txtUsuario" runat="server" LabelText="Usuario" ReadOnlyTxt="True" />
            <PLs:PLTextBox ID="txtNombreApellido" runat="server" LabelText="Nombre y Apellido" WidthTxt="250px" /><br /> 
            <PEARGNC:CboTiposDocumentos ID="cboTipoDoc" runat="server" AutomaticLoad="True" LabelText="Tipo Documento:" /><br /> 
            <PLs:PLTextBox ID="txtNroDocumento" runat="server" LabelText="Nro. Documento" MaxLenghtTxt="8" /><br /> 
            <PLs:PLTextBox ID="txtEmail" runat="server" LabelText="Email:" WidthTxt="250px" />            
             <p style="text-align: right;">
                <PLs:PLButton ID="btnAceptarUsuario" runat="server" 
                     ImageUrl="/Images/Iconos/correcta.png" OnClick="btnAceptarUsuario_Click" 
                     Text="Aceptar" />

                <PLs:PLButton ID="btnCancelarUsuario" runat="server" CausesValidation="False" 
                     ImageUrl="/Images/Iconos/bloqueada.png" OnClick="btnCancelar_Click" 
                     Text="Cancelar"/>

            </p>
            </fieldset>
        </ContentTemplate>
    </PLs:PLTabPanel>
     <PLs:PLTabPanel ID="PLTabPanel1" runat="server">
        <HeaderTemplate>Cambiar contraseña</HeaderTemplate>
        <ContentTemplate>
         <fieldset>
                <PLs:PLTextBox ID="txtPwActual" runat="server" LabelText="Contraseña Actual:" /><br />     
                <PLs:PLTextBox ID="txtPwNueva" runat="server" LabelText="Contraseña Nueva:" /><br />
                <PLs:PLTextBox ID="txtPwConfirma" runat="server" LabelText="Confirmar Contraseña:" /><br />
                <p style="text-align: right;">
                    <PLs:PLButton ID="btnCambiarContrasenia" runat="server" Text="Cambiar Contraseña" OnClick="btnCambiarContrasenia_Click"
                    ImageUrl="/Images/Iconos/correcta.png" />
                </p>
            </fieldset>
        </ContentTemplate>
    </PLs:PLTabPanel>

    <PLs:PLTabPanel ID="pnlTaller" runat="server">
        <HeaderTemplate>Datos de Taller</HeaderTemplate>
        <ContentTemplate>
        <fieldset>
            <pls:PLHidden ID="hddTallerID" runat="server" />
            <PLs:PLTextBox ID="txtMatricula" runat="server" LabelText="Matrícula:" MaxLenghtTxt="10" /> <br /> 
            <PLs:PLTextBox ID="txtRazonSocial" runat="server" LabelText="Razón Social:" WidthTxt="250px"/> <br /> 
            <PLs:PLTextBox ID="txtDomicilio" runat="server" LabelText="Domicilio:" WidthTxt="250px"/> <br />
            <PEARGNC:CboLocalidades ID="cboLocalidad" runat="server" LabelText="Ciudad:" AutomaticLoad="true"/><br />
            <PLs:PLTextBoxMasked ID="txtCuit" runat="server" LabelText="Cuit:" Mask="99-99999999-9" MaskType="None"/> <br /> 
            <PLs:PLTextBox ID="txtTelefono" runat="server" LabelText="Teléfono:" MaxLenghtTxt="20" /> <br /> 
            <PLs:PLTextBox ID="txtFax" runat="server" LabelText="Fax:" MaxLenghtTxt="20" /> <br /> 
            <PLs:PLTextBox ID="txtEmailTaller" runat="server" LabelText="Email:" WidthTxt="250px" /> <br /> 
            <PLs:PLTextBox ID="txtContacto" runat="server" LabelText="Persona Contacto:" WidthTxt="250px" MaxLenghtTxt="30"/> <br /> 
            <PLs:PLTextBoxMasked ID="txtVenceContrato" runat="server" LabelText="Fecha Venc. Contrato:" Mask="99/99/9999" MaskType="Date"/> <br />
            <PEARGNC:CboZonas ID="cboZona" runat="server" LabelText="Zona:" AutomaticLoad="true" /> <br /> 
            <PLs:PLTextBox ID="txtHorarioAtencion" runat="server" LabelText="Horario Atención:" WidthTxt="250px" MaxLenghtTxt="50" /> <br /> 
             <p style="text-align: right;">
                <PLs:PLButton ID="btnAceptarTaller" runat="server" Text="Aceptar" OnClick="btnAceptarTaller_Click"
                    ImageUrl="/Images/Iconos/correcta.png" />
                <PLs:PLButton ID="btnCancelarTaller" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"
                    CausesValidation="false" ImageUrl="/Images/Iconos/bloqueada.png" />
            </p>
            </fieldset>
        </ContentTemplate>
    </PLs:PLTabPanel>

</PLs:PLTabContainer>

<uc1:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />

</asp:Content>
