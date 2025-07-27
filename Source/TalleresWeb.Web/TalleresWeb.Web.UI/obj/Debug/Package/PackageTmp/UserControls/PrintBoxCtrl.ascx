<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrintBoxCtrl.ascx.cs" Inherits="TalleresWeb.Web.UI.UserControls.PrintBoxCtrl" %>


<div style="position: fixed; left: 0; top: 0;">
    
     <ajaxtoolkit:ModalPopupExtender ID="MPE" runat="server" TargetControlID="btnTarget" PopupControlID="modal"
        DropShadow="false" CancelControlID="btnCancel" OnCancelScript="return false;"
        CacheDynamicResults="false">
    </ajaxtoolkit:ModalPopupExtender>

    <div style="display: none;">
        <asp:Button ID="btnTarget" runat="server" Text="Cancelar" />
    </div>

    <asp:Panel ID="modal" runat="server" CssClass="modal-dialog" Style="display: none; z-index:9999; width: 800px; height: 400px; margin-top:-50px !important;">    
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">
                    <asp:Label ID="lblTituloMsj" Text="Visualizar / Imprimir" runat="server" />
                </h4>
            </div>        
            <div class="panel-body center">
                <iframe id="frmReporte" runat="server" style="width: 100%;" height="300"></iframe>
                <asp:Image ID="imgAccesoDenegado" runat="server" ImageUrl="~/Imagenes/Iconos/permisoDenegado.gif" Visible="false" />
            </div>
            <div class="panel-footer center-block text-center">
                <div class="row center-block text-center">        
                    <asp:Button ID="btnCancel" runat="server" CssClass="close" Text="Cerrar"/>
                </div>
            </div>
        </div>
    </asp:Panel>
</div>