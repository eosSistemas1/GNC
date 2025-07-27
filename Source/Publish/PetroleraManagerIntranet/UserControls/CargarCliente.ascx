<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CargarCliente.ascx.cs" Inherits="PetroleraManagerIntranet.Web.UserControls.CargarCliente" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="row">
            <div class="col-sm-12">
                <h3><strong>CLIENTE</strong></h3>
            </div>
            <hr>
        </div>

        <asp:Panel ID="Panel1" runat="server" Visible="true" DefaultButton="btnBuscarCliente">
            <input id="hddID" type="hidden" runat="server" />
            <div class="row">
                <div class="col-sm-3"><strong>Tipo Doc.:</strong></div>
                <div class="col-sm-3">
                    <asp:DropDownList ID="cboDocCliente" runat="server" class="form-control nn"  />
                </div>
                <div class="col-sm-2"><strong>Nro Doc.:</strong></div>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtNroDocCliente" runat="server" MaxLenghtTxt="11" ClientIDMode="Static" class="form-control nn" onKeyPress="return soloNumeros(event)" />
                    <span style="visibility: hidden">
                    <asp:Button ID="btnBuscarCliente" runat="server" BackColor="#EEEEEE" Text="Buscar" ClientIDMode="Static"
                        OnClick="btnBuscarCliente_Click" CausesValidation="false" /></span>                        
                </div>         
                <div class="col-sm-1">
                    <Controls:BtnReEvaluar ID="btnBuscarOtroCliente" runat="server" OnClick="btnBuscarOtroCliente_Click"
                            CausesValidation="false" Visible="false" ToolTip="Cambiar Cliente" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlCliente" runat="server" Visible="true">
            <div class="row">
                <div class="col-sm-4"><strong>Nombre y Apellido:</strong></div>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtNom" runat="server" class="form-control nn" MaxLength="34" />
                </div>
            </div>
            <div class="row">
                <div class="col-sm-2"><strong>Domicilio:</strong></div>
                <div class="col-sm-10">
                    <asp:TextBox ID="txtCalle" runat="server" MaxLength="40" class="form-control nn" />
                </div>                
            </div>
            <div class="row">
                <div class="col-sm-2"><strong>Localidad:</strong></div>
                <div class="col-sm-10">
                    <Controls:CboLocalidades ID="cboCiudades" runat="server" />
                </div>
            </div>
            <div class="row">
                <div class="col-sm-2"><strong>Teléfono:</strong></div>
                <div class="col-sm-10">
                   <asp:TextBox ID="txtTelefono" runat="server" MaxLength="10" class="form-control nn" />
                </div>
            </div>
        </asp:Panel>

    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">

    $(document).ready(function () {
        inicializar();
    });

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_pageLoaded(inicializar);

    function inicializar() {
        $('#<%=txtNroDocCliente.ClientID %>').focusout(function () {
            if ($('#<%=txtNroDocCliente.ClientID %>').val() != "") {
                $('#btnBuscarCliente').trigger("click");
            }
        });
    }

    function soloNumeros(e) {
        var key = window.Event ? e.which : e.keyCode
        return (key >= 48 && key <= 57)
    }
</script>
