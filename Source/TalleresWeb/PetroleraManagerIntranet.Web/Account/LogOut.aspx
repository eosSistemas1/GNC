<%@ Page Title="Cerrar Sesión" Language="C#"  AutoEventWireup="true" CodeBehind="LogOut.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Account.LogOut" %>


    <div id="consulta" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Cerrar Sesión</h4>
                </div>
                <div class="modal-body">
                    <p>Saliendo del sistema...</p>
                </div>

                <div class="modal-footer">
                    <input type="submit" id="aceptarButton" class="btn btn-success" data-dismiss="modal" value="Aceptar" />
                </div>
            </div>

        </div>
    </div>

    <!-- jQuery -->
    <script src="../vendor/jquery/jquery.min.js"></script>

    <script>
        

        function redirect() {            
            $(location).attr('href', '/');
        }

        function focusButton() {
            $('#aceptarButton').focus();
        }

        $(document).ready(function () {
            $('#consulta').bind('hidden.bs.modal', redirect);
            $('#consulta').bind('shown.bs.modal', focusButton);
            $('#consulta').modal();
        });
        
    </script>
