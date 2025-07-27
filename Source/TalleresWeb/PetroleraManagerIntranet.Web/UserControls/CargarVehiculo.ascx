<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CargarVehiculo.ascx.cs"
    Inherits="PetroleraManagerIntranet.Web.UserControls.CargarVehiculo" %>
<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="row">
            <div class="col-sm-12">
                <h3><strong>VEHICULO</strong></h3>
            </div>
            <hr>
        </div>

        <asp:Panel ID="Panel1" runat="server" Visible="true" DefaultButton="btnBuscarVehiculo">
            <input id="hddID" type="hidden" runat="server" />
            <div class="row">
                <div class="col-sm-2"><strong>Dominio:</strong></div>
                <div class="col-sm-6">
                    <asp:TextBox ID="txtDominioVehiculo" runat="server" ClientIDMode="Static" LabelText="DOMINIO:" MaxLenght="7" class="form-control nn" />
                </div>
                <div class="col-sm-2">
                     <Controls:BtnReEvaluar ID="btnBuscarOtroAuto" runat="server" OnClick="btnBuscarOtroAuto_Click"
                                CausesValidation="false" Visible="false" ToolTip="Cambiar Vehículo" />
                </div>
                <div class="col-sm-4">
                    <span style="visibility: hidden">
                        <asp:Button ID="btnBuscarVehiculo" runat="server" Text="Buscar" OnClick="btnBuscarVehiculo_Click" ClientIDMode="Static" CausesValidation="true" ValidationGroup="dominioVehiculo" BackColor="#EEEEEE" /></span>                   
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlVehiculo" runat="server" Visible="true">
            <div class="row">
                <div class="col-sm-2"><strong>MARCA:</strong></div>
                <div class="col-sm-4">
                    <asp:TextBox ID="txtMarcaAuto" runat="server" MaxLength="50" class="form-control nn" ></asp:TextBox>
                </div>
                <div class="col-sm-2"><strong>MODELO:</strong></div>
                <div class="col-sm-4">
                    <asp:TextBox ID="txtModeloAuto" runat="server" MaxLength="50" class="form-control nn" ></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-2"><strong>AÑO:</strong></div>
                <div class="col-sm-4">
                    <asp:TextBox ID="txtAnioAuto" runat="server" MaxLength="4" onKeyPress="return soloNumeros(event)" class="form-control nn" />
                </div>
                <div class="col-sm-2"><strong>INYECCION:</strong></div>
                <div class="col-sm-4">
                    <asp:CheckBox ID="chkBoxEsInyeccion" runat="server" TextAlign="Left" />
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-3">
                    <asp:RadioButton ID="chkTipoVPart" runat="server" GroupName="tipoVehiculo" Checked="true" Text="PARTICULAR" />
                </div>
                <div class="col-sm-3">
                    <asp:RadioButton ID="chkTipoVTaxi" runat="server" GroupName="tipoVehiculo" Text="TAXI" />
                </div>
                <div class="col-sm-3">
                    <asp:RadioButton ID="chkTipoVPickUp" runat="server" GroupName="tipoVehiculo" Text="PICK-UP" />
                </div>
                <div class="col-sm-3">
                    <asp:RadioButton ID="chkTipoVOficial" runat="server" GroupName="tipoVehiculo" Text="OFICIAL" />
                </div>
                <div class="col-sm-4">
                    <asp:RadioButton ID="chkTipoVOtros" runat="server" GroupName="tipoVehiculo" Text="OTROS" />
                </div>
                <div class="col-sm-4">
                    <asp:RadioButton ID="chkTipoVMoto" runat="server" GroupName="tipoVehiculo" Text="MOTO" />
                </div>
                <div class="col-sm-4">
                    <asp:RadioButton ID="chkTipoVAutoelevadores" runat="server" GroupName="tipoVehiculo" Text="AUTOELEVADOR" />
                </div>
            </div>                
        </asp:Panel>

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
        $('#<%=txtDominioVehiculo.ClientID %>').change(function () {
            if ($('#<%=txtDominioVehiculo.ClientID %>').val() != "") {
                $('#btnBuscarVehiculo').trigger("click");
            }
        });
    }

    function soloNumeros(e) {
        var key = window.Event ? e.which : e.keyCode
        return (key >= 48 && key <= 57)
    }

</script>
