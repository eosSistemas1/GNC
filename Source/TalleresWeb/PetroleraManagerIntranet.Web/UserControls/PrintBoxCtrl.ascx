<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrintBoxCtrl.ascx.cs" Inherits="PetroleraManagerIntranet.Web.UserControls.PrintBoxCtrl" %>

<div style="position: fixed; left: 0; top: 0;">
    <ajaxToolkit:ModalPopupExtender ID="MPE" runat="server" TargetControlID="btnTarget" PopupControlID="modal"
        BackgroundCssClass="modalBackground" DropShadow="true" CancelControlID="btnCancel"
        CacheDynamicResults="false">
    </ajaxToolkit:ModalPopupExtender>

    <div style="display: none;">
        <asp:Button ID="btnTarget" runat="server" Text="Cancelar" />
    </div>

    <asp:Panel ID="modal" runat="server" Style="display: none; width: 800px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">
                    <asp:Label ID="lblTituloMsj" runat="server" Text="Visualizar / Imprimir" />
                </h4>
            </div>

            <div class="modal-body">
                <iframe id="frmReporte" runat="server" style="width: 100%; height: 300px"></iframe>
                <asp:Image ID="imgAccesoDenegado" runat="server" ImageUrl="~/img/Iconos/permisoDenegado.gif" Visible="false" />
            </div>

            <div class="modal-footer">
                <div class="row center-block text-center" style="width: 100%; text-align: center">
                    <asp:Button ID="btnCancel" runat="server" Text="Cerrar" />
                </div>
            </div>
        </div>
    </asp:Panel>
</div>
