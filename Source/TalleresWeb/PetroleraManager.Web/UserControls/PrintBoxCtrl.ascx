<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrintBoxCtrl.ascx.cs" Inherits="PetroleraManager.Web.UserControls.PrintBoxCtrl" %>

<div style="position: fixed; left: 0; top: 0;">
    <PLs:PLModalPopupExtender ID="MPE" runat="server" TargetControlID="btnTarget" PopupControlID="modal"
        BackgroundCssClass="modalBackground" DropShadow="true" CancelControlID="btnCancel"
        CacheDynamicResults="false">
    </PLs:PLModalPopupExtender>

    <div style="display: none;">
        <asp:Button ID="btnTarget" runat="server" Text="Cancelar" />
    </div>

    <asp:Panel ID="modal" runat="server" CssClass="CajaDialogo" Style="display: none; width: 800px; height: 400px;">
        <div class="row">
            <h4 class="modal-title">
                <asp:Label ID="lblTituloMsj" runat="server" Text="Visualizar / Imprimir" />
            </h4>
        </div>

        <iframe id="frmReporte" runat="server" style="width: 100%; height: 68%"></iframe>
        <asp:Image ID="imgAccesoDenegado" runat="server" ImageUrl="~/Imagenes/Iconos/permisoDenegado.gif" Visible="false" />

        <div class="row center-block text-center">
            <center><asp:Button ID="btnCancel" runat="server" CssClass="close" Text="Cerrar"/></center>
        </div>
    </asp:Panel>
</div>
