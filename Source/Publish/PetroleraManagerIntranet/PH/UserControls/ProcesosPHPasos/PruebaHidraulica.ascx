<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PruebaHidraulica.ascx.cs" Inherits="PetroleraManagerIntranet.Web.UserControls.ProcesosPHPasos.PruebaHidraulica" %>
<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>


<div class="row forms">

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-sm-6">
                <p><strong>Peso Cilindro lleno:&nbsp;</strong></p>
                <p>
                    <asp:TextBox ID="txtPesoCilindroLleno" runat="server" CssClass="form-control mg" OnTextChanged="txtPesoCilindroLleno_TextChanged" AutoPostBack="true" />
                </p>
            </div>

            <div class="col-sm-6">
                <p><strong>Temperatura ambiente:&nbsp;</strong></p>
                <p>
                    <asp:TextBox ID="txtTemperaturaAmbiente" runat="server" CssClass="form-control mg" OnTextChanged="txtPesoCilindroLleno_TextChanged" AutoPostBack="true" />
                </p>
            </div>

            <asp:Panel ID="panel1" runat="server" Visible="false">
                <div class="col-sm-6">
                    <p><strong>Lectura nivel agua inicial:&nbsp;</strong></p>
                    <p>
                        <asp:TextBox ID="txtNivelAguaInicial" runat="server" CssClass="form-control mg" />
                    </p>
                </div>
                <div class="col-sm-6">
                    <p><strong>Presión de prueba:&nbsp;</strong></p>
                    <p>                        
                        <Controls:CboPresionDePrueba ID="cboPresionDePrueba" runat="server"></Controls:CboPresionDePrueba>                        
                    </p>
                </div>
                <div class="clearfix"></div>
                <div class="col-sm-6">
                    <p><strong>Lectura nivel agua presión prueba:&nbsp;</strong></p>
                    <p>
                        <asp:TextBox ID="txtNivelAguaPesionPrueba" runat="server" CssClass="form-control mg" />
                    </p>
                </div>
                <div class="col-sm-6">
                    <p><strong>Bureta:&nbsp;</strong></p>
                    <p>
                        <Controls:CboBureta ID="cboBureta" runat="server"></Controls:CboBureta>
                    </p>
                </div>
                <div class="clearfix"></div>
                <div class="col-sm-6">
                    <p><strong>Lectura nivel agua despresurizado cilindro:&nbsp;</strong></p>
                    <p>
                        <asp:TextBox ID="txtNivelAguaDespresurizado" runat="server" CssClass="form-control mg" />
                    </p>
                </div>
                <div class="col-sm-6">
                    <p><strong>Valvula Prueba:&nbsp;</strong></p>
                    <p>
                        <Controls:CboValvulasPrueba ID="cboValvulaPrueba" runat="server"></Controls:CboValvulasPrueba>
                    </p>
                </div>
            </asp:Panel>

            <uc1:messageboxctrl runat="server" id="MessageBoxCtrl" />

        </ContentTemplate>
    </asp:UpdatePanel>
</div>
