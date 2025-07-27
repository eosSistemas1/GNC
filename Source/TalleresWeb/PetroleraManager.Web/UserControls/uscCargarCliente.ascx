<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscCargarCliente.ascx.cs" Inherits="PetroleraManager.Web.UserControls.uscCargarCliente" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
    <ContentTemplate>
        <fieldset class="aField">
            <legend>CLIENTE</legend>
            <asp:Panel ID="Panel1" runat="server" Visible="true" DefaultButton="btnBuscarCliente">
                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td valign="top">
                        <input id="hddID" type="hidden" runat="server" />
                        <asp:Label Text="Tipo y Nro. Doc:" runat="server" />
                        <asp:DropDownList ID="cboDocCliente" runat="server" Width="100px" />
                    </td>
                    <td valign="top">
                        <PLs:PLTextBox ID="txtNroDocCliente" runat="server" MaxLenghtTxt="11" ClientIDMode="Static" onKeyPress="return soloNumeros(event)"/>
                    </td>
                    <td valign="top">
                        <span style="visibility:hidden"><PLs:PLButton ID="btnBuscarCliente" runat="server" BackColor="#EEEEEE" Text="Buscar" ClientIDMode="Static"
                            OnClick="btnBuscarCliente_Click" CausesValidation="false"/></span>
                        &nbsp;
                        <Controls:ImgBtnCambiar ID="btnBuscarOtroCliente" runat="server" OnClick="btnBuscarOtroCliente_Click"
                                                CausesValidation="false" Visible="false" ToolTip="Cambiar Cliente" />                        
                    </td>
                </tr>

            </table>
            </asp:Panel>
            <asp:Panel ID="pnlCliente" runat="server" Visible="true" >
                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td colspan="2" width="30%">
                            <PLs:PLTextBox ID="txtNom" runat="server" LabelText="Nombre y Apellido:" Required="true"
                                ValidationGroup="cliente" WidthTxt="320px" Width="80%"  MaxLenghtTxt="34"/>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <PLs:PLTextBox ID="txtCalle" runat="server" LabelText="Domicilio:" Required="true" ValidationGroup="cliente" />
                        </td>
                        <td width="20%">
                            <PLs:PLTextBox ID="txtTelefono" runat="server" LabelText="Teléfono:" Required="true" Text="0" ValidationGroup="cliente" MaxLenghtTxt="10" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="label" runat="server" Text="Localidad:" />
                            <asp:DropDownList ID="cboCiudades" runat="server" Width="50%" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
   </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">

    $(document).ready(function () {
        inicializar();
    });

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_pageLoaded(inicializar);

    function inicializar()
    {
        $("#txtNroDocClientetxt").focusout(function () {
            if ($("#txtNroDocClientetxt").val() != "") {
                $('#btnBuscarCliente').trigger("click");
            }
        });
    }

    function soloNumeros(e) {
        var key = window.Event ? e.which : e.keyCode
        return (key >= 48 && key <= 57)
    }
</script>