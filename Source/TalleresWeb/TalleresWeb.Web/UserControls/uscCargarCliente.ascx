<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscCargarCliente.ascx.cs" Inherits="TalleresWeb.Web.UserControls.uscCargarCliente" %>
              
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <input id="hddID" type="hidden" runat="server" />
        <fieldset class="aField">
            <legend>CLIENTE</legend>
            <PLs:PLPanel ID="panel" runat="server" DefaultButton="btnBuscarCliente">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td width="40%">
                        <PEARGNC:CboTiposDocumentos ID="cboDocCliente" runat="server" AutomaticLoad="true"
                            LabelText="Tipo y Nro. Doc:" Width="100%" ValidationGroup="dni" />
                    </td>
                    <td width="20%">
                        <PLs:PLTextBoxMasked ID="txtNroDocCliente" runat="server" MaskType="None" Mask="99999999" />
                    </td>
                    <td width="40%">
                        <PLs:PLButton ID="btnBuscarCliente" runat="server" Text="Buscar" OnClick="btnBuscarCliente_Click" CausesValidation="false" />
                        &nbsp;<PLs:PLLinkButton ID="btnBuscarOtroCliente" runat="server" OnClick="btnBuscarOtroCliente_Click"
                            CausesValidation="false" Visible="False">Otro Cliente</PLs:PLLinkButton>
                    </td>
                </tr>
            </table>
            </PLs:PLPanel>
            <hr />
            <asp:Panel ID="pnlCliente" runat="server" Visible="true" Enabled="false" Height="100px">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="2">
                            <PLs:PLTextBox ID="txtNom" runat="server" LabelText="Nombre y Apellido:" Required="true"
                                ValidationGroup="cliente" WidthTxt="300px" Width="440px" />
                        </td>
                    </tr>
                    <tr>
                        <td width="50%">
                            <PLs:PLTextBox ID="txtCalle" runat="server" LabelText="Domicilio:" Required="true"
                                ValidationGroup="cliente" />
                        </td>
                        <td width="50%">
                            <PLs:PLTextBox ID="txtTelefono" runat="server" LabelText="Teléfono:" Required="true"
                                ValidationGroup="cliente" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <PEARGNC:CboLocalidades ID="cboCiudades" runat="server" AutomaticLoad="true" LabelText="Localidad:"
                                Width="50%" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
    </ContentTemplate>
</asp:UpdatePanel>
