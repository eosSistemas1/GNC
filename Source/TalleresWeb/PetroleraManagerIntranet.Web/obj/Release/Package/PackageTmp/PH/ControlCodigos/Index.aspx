<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Mobile.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PetroleraManagerIntranet.Web.PH.ControlCodigos.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../css/messageButton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="destacados" class="row">

        <div class="col-sm-4 text-center">
            <div class="notifications">
                <div class="new-message">
                    <span id="lblVerificarCodigos">Actualizando</span>
                </div>
                <div class="messages">
                    <a href="VerificarCodigos.aspx" class="btn btn-block btn-lg">
                        <i class="fa fa-barcode fa-5x"></i>
                        <br>
                        <h3>Verificar Códigos</h3>
                    </a>
                </div>
            </div>
        </div>
        <div class="col-sm-4 text-center">
            <div class="notifications">
                <div class="new-message">
                    <span id="lblEvaluarValvulas">Actualizando</span>                    
                </div>
                <div class="messages">
                    <a href="EvaluarValvulas.aspx" class="btn btn-block btn-lg">
                        <i class="fa fa-circle-o fa-5x"></i>
                        <br>
                        <h3>Evaluar Válvulas</h3>
                    </a>
                </div>
            </div>
        </div>
        <div class="col-sm-4 text-center">
            <a href="ReimprimirHojaRuta.aspx" class="btn btn-block btn-lg">
                <i class="fa fa-print fa-5x"></i>
                <br>
                <h3>Reimprimir Hoja de Ruta</h3>
            </a>
        </div>
    </div>
    <div class="col-sm-2 text-center"></div>
    <div class="col-sm-4 text-center">
        <span id="liveclock" style="position: absolute; left: 0; top: 0;"><font size="15" face="Arial"><b><font size="3">Hora:</font><br>10:13:14 PM</b></font></span>
    </div>

    <div class="col-sm-6 text-center">
        <div class="col-sm-12 text-center">
            <h4><strong>CILINDROS</strong></h4>
        </div>
        <div class="col-sm-4 text-center">
            <h3>En Proceso</h3>
            <br/>
            <h5><span id="txtEnProceso" /></h5>
        </div>
        <div class="col-sm-4 text-center">
            <h3>Para Procesar</h3>
            <br/>
            <h5><span id="txtIngresadas" /></h5>
        </div>
        <div class="col-sm-4 text-center">
            <h3>Finalizadas</h3>
            <br/>
            <h5><span id="txtFinalizadas" /></h5>
        </div>
    </div>

    <script language="JavaScript" type="text/javascript">
        function showClock() {
            if (!document.layers && !document.all && !document.getElementById)
                return

            var Digital = new Date()
            var hours = Digital.getHours()
            var minutes = Digital.getMinutes()
            var seconds = Digital.getSeconds()

            var dn = "PM"
            if (hours < 12)
                dn = "AM"
            if (hours > 12)
                hours = hours - 12
            if (hours == 0)
                hours = 12

            if (minutes <= 9)
                minutes = "0" + minutes
            if (seconds <= 9)
                seconds = "0" + seconds

            showPH();

            //change font size here to your desire
            myclock = "<font size='15' face='Arial' ><b><font size='3'>Hora:</font></br>" + hours + ":" + minutes + ":"
                + seconds + " " + dn + "</b></font>"
            if (document.layers) {
                document.layers.liveclock.document.write(myclock)
                document.layers.liveclock.document.close()
            }
            else if (document.all)
                liveclock.innerHTML = myclock
            else if (document.getElementById)
                document.getElementById("liveclock").innerHTML = myclock
            setTimeout("showClock()", 10000)
        }

        function showPH() {
            $.ajax({
                type: "POST",
                url: "Index.aspx/CalcularCantidades",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var cantidades = response.d.split("|");
                    $("#txtIngresadas").text(cantidades[0]);
                    $("#txtEnProceso").text(cantidades[1]);                    
                    $("#txtFinalizadas").text(cantidades[2]);   
                    $("#lblEvaluarValvulas").text(cantidades[3]);
                    $("#lblVerificarCodigos").text(cantidades[4]);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });

            setTimeout("showPH()", 300000)
        }

        function show() {
            this.showClock();

            this.showPH();
        }
        window.onload = show;
         //-->
    </script>
</asp:Content>
