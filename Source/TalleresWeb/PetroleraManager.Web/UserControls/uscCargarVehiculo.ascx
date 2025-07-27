<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscCargarVehiculo.ascx.cs"
    Inherits="PetroleraManager.Web.UserControls.uscCargarVehiculo" %>
<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <fieldset class="aField">
            <legend>VEHICULO</legend>
            <asp:Panel ID="Panel1" runat="server" Visible="true" DefaultButton="btnBuscarVehiculo">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 10%">
                            <PLs:PLTextBox ID="txtDominioVehiculo" runat="server" ClientIDMode="Static" LabelText="DOMINIO:" WidthTxt="90px" MaxLenghtTxt="7" />
                        </td>
                        <td>
                            <span style="visibility: hidden">
                                <asp:Button ID="btnBuscarVehiculo" runat="server" Text="Buscar" OnClick="btnBuscarVehiculo_Click" ClientIDMode="Static" CausesValidation="true" ValidationGroup="dominioVehiculo" BackColor="#EEEEEE" /></span>
                            &nbsp;&nbsp;
                            <Controls:ImgBtnCambiar ID="btnBuscarOtroAuto" runat="server" OnClick="btnBuscarOtroAuto_Click"
                                                    CausesValidation="false" Visible="false" ToolTip="Cambiar Vehículo" />
                        </td>
                        <td></td>
                        <td>
                            <input id="hddID" type="hidden" runat="server" /></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlVehiculo" runat="server" Visible="true">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="10%">MARCA:</td>
                        <td width="30%">
                            <asp:TextBox ID="txtMarcaAuto" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="valMarcaVehiculo" runat="server" ControlToValidate="txtMarcaAuto"
                                Display="Dynamic" ErrorMessage="VEHÍCULO: Se requiere una marca." ValidationGroup="vehiculo">*</asp:RequiredFieldValidator>
                        </td>
                        <td width="10%">MODELO:</td>
                        <td width="30%">
                            <asp:TextBox ID="txtModeloAuto" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="valModeloAuto" runat="server" ControlToValidate="txtModeloAuto"
                                Display="Dynamic" ErrorMessage="VEHÍCULO: Se requiere un modelo de auto." ValidationGroup="vehiculo">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">AÑO:</td>
                        <td width="30%">
                            <PLs:PLTextBox ID="txtAnioAuto" runat="server" MaxLenghtTxt="4" onKeyPress="return soloNumeros(event)" />
                        </td>
                        <td width="20%">INYECCION:</td>
                        <td width="30%">
                            <asp:CheckBox ID="chkBoxEsInyeccion" runat="server" TextAlign="Left" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:RadioButton ID="chkTipoVPart" runat="server" GroupName="tipoVehiculo" Checked="true"
                                Text="PARTICULAR" />
                        </td>
                        <td align="center">
                            <asp:RadioButton ID="chkTipoVTaxi" runat="server" GroupName="tipoVehiculo" Text="TAXI" />
                        </td>
                        <td align="center">
                            <asp:RadioButton ID="chkTipoVPickUp" runat="server" GroupName="tipoVehiculo" Text="PICK-UP" />
                        </td>
                        <td align="center"></td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:RadioButton ID="chkTipoVOficial" runat="server" GroupName="tipoVehiculo" Text="OFICIAL" />
                        </td>
                        <td align="center">
                            <asp:RadioButton ID="chkTipoVOtros" runat="server" GroupName="tipoVehiculo" Text="OTROS" />
                        </td>
                        <td align="center">
                            <asp:RadioButton ID="chkTipoVMoto" runat="server" GroupName="tipoVehiculo" Text="MOTO" />
                        </td>
                        <td align="center">
                            <asp:RadioButton ID="chkTipoVAutoelevadores" runat="server" GroupName="tipoVehiculo" Text="AUTOELEVADOR" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>

        <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl1" />

    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">

    $(document).ready(function () {
        inicializar();
    });

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_pageLoaded(inicializar);

    function inicializar() {
        $("#txtDominioVehiculotxt").change(function () {
            if ($("#txtDominioVehiculotxt").val() != "") {
                $('#btnBuscarVehiculo').trigger("click");
            }
        });
    }

    function soloNumeros(e) {
        var key = window.Event ? e.which : e.keyCode
        return (key >= 48 && key <= 57)
    }

</script>
