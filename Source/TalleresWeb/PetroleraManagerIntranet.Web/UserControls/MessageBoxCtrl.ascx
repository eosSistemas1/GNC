<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageBoxCtrl.ascx.cs" Inherits="PetroleraManagerIntranet.Web.UserControls.MessageBoxCtrl" %>

<script type="text/javascript">    
    function openModal(mensaje, imagen, returnURL) {
        if (returnURL && returnURL != "") {
            $('#modalMensaje').on('hidden.bs.modal', function () {
                window.location.replace(returnURL);
            });
        }

        $('#lblMsj').html(mensaje);
        $('#imgMsg').attr('src', imagen);
        $('#modalMensaje').modal('show');
    }
</script>

<div class="clearfix"></div>

<div class="modal" id="modalMensaje" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-content">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <h4 class="modal-title" id="myModalLabel">  
                <img ID="imgMsg" src="#" style="width:25px"/>
                <b><asp:Label ID="lblTituloMsj" runat="server" Text="" /></b>
            </h4>
        </div>
        <div class="modal-body">
            <table style="width: 50%">
                <tr>
                    <td style="width: 10%; align-content: center;">                        
                        
                    </td>
                    <td style="width: 90%">
                        <div style="max-height: 200px; overflow: auto;">
                            <label ID="lblMsj" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnOk" class="btn btn-default" runat="server" Text="Imprimir" Visible="false" />
            <asp:Button ID="btnCancel" class="btn btn-success" runat="server" Text="Aceptar" data-dismiss="modal" />            
        </div>
    </div>
</div>


