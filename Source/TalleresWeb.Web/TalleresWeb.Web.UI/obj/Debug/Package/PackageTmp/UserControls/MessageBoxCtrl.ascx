<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageBoxCtrl.ascx.cs" Inherits="TalleresWeb.Web.UI.UserControls.MessageBoxCtrl" %>
<div style="position: fixed; left: 0; top: 0; z-index: 100000">
    <ajaxtoolkit:ModalPopupExtender ID="MPE" runat="server" TargetControlID="btnCancel" PopupControlID="modal"
        DropShadow="false" CancelControlID="btnCancel" OnCancelScript="return false;"
        CacheDynamicResults="false">
    </ajaxtoolkit:ModalPopupExtender>
    <div style="display: none;">
        <asp:Button ID="btnTarget" runat="server" Text="Cancelar" />
    </div>
    <asp:Panel ID="modal" runat="server" CssClass="modal-dialog" Style="display: none; z-index:9999">
            <div class="modal-content">
                <div class="modal-header">                               
                    <h4 class="modal-title">
                        <asp:Image ID="imgMsg" runat="server" />
                        <asp:Label ID="lblTituloMsj" runat="server" Text="Mensaje" />
                    </h4>
                </div>
                <div class="panel-body center">
                    <asp:Label ID="lblMsj" runat="server" />
                </div>
                 <div class="panel-footer center-block text-center">
                    <div class="row center-block text-center">
                        <asp:Button ID="btnOk" runat="server" CssClass="btn btn-primary" Text="Imprimir" Visible="false" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-error" Text="Cerrar"/>
                    </div>
                </div>
            </div>
    </asp:Panel>
</div>
