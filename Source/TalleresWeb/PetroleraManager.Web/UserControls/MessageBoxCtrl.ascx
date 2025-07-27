<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageBoxCtrl.ascx.cs" Inherits="PetroleraManager.Web.UserControls.MessageBoxCtrl" %>

<div style="position: fixed; left: 0; top: 0;">
    <PLs:PLModalPopupExtender ID="MPE" runat="server" TargetControlID="btnTarget" PopupControlID="Panel1"
        BackgroundCssClass="modalBackground" DropShadow="true" CancelControlID="btnCancel"
        CacheDynamicResults="false">
    </PLs:PLModalPopupExtender>

    <div style="display: none;">
        <PLs:PLButton ID="btnTarget" runat="server" Text="Cancelar" />
    </div>

    <PLs:PLPanel ID="Panel1" runat="server" CssClass="CajaDialogo" Width="100%">
        <fieldset>
            <legend><span class="LabelLegend">
                <PLs:PLLabel ID="lblTituloMsj" runat="server" Text="Aviso" /></span></legend>
            <table width="100%">
                <tr>
                    <td width="15%">
                        <PLs:PLImage ID="imgMsg" runat="server" />
                    </td>
                    <td width="85%">
                        <div style="max-height: 200px; overflow:auto;">
                            <PLs:PLLabel ID="lblMsj" runat="server" />
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <center>
            <PLs:PLButton ID="btnOk" runat="server" Text="Imprimir" Visible="false" />
            <PLs:PLButton ID="btnCancel" runat="server" Text="Aceptar" />
        </center>
        <br />
    </PLs:PLPanel>
</div>
