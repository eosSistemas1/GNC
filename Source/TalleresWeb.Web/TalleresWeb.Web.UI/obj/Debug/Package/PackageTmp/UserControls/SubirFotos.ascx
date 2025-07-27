<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubirFotos.ascx.cs" Inherits="TalleresWeb.Web.UI.UserControls.SubirFotos" %>

<asp:HiddenField ID="hdnTallerID" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="hdnDominio" ClientIDMode="Static" runat="server" />


<div class="col-sm-12 no-padding">  
    <div class="col-sm-3 no-padding">
        <h1>Dni - Frente</h1>
        <img id="imgDniFrente1" src="/img/scanner/dni-frente-foto-no.gif" alt="Dni Frente" width="100" />
        <asp:FileUpload ID="fileDniFrente" runat="server" ToolTip="DNI frente" ClientIDMode="Static" onchange="changeImageScanner('1');"/>
        <asp:Label ID="labelDniFrente" Text="" runat="server" />
    </div>
    <div class="col-sm-3 no-padding">
        <h1>Dni - Dorso</h1>
        <img id="imgDniDorso1" src="/img/scanner/dni-dorso-foto-no.gif" alt="Dni Dorso" width="100" />
        <asp:FileUpload ID="fileDniDorso" runat="server" ToolTip="DNI dorso" ClientIDMode="Static" onchange="changeImageScanner('2');"/>
        <asp:Label ID="labelDniDorso" Text="" runat="server" />
    </div>
    <div class="col-sm-3 no-padding">       
        <h1>Cédula - Frente</h1>
        <img id="imgCedulaFrente1" src="/img/scanner/cedula-frente-foto-no.gif" alt="Dni Frente" width="100" />
        <asp:FileUpload ID="fileTjFrente" runat="server" ToolTip="Tarjeta frente" ClientIDMode="Static" onchange="changeImageScanner('3');"/>
        <asp:Label ID="labelTjFrente" Text="" runat="server" />
    </div>
    <div class="col-sm-3 no-padding">
        <h1>Cédula - Dorso</h1>
        <img id="imgCedulaDorso1" src="/img/scanner/cedula-dorso-foto-no.gif" alt="Dni Dorso" width="100" />
        <asp:FileUpload ID="fileTjDorso" runat="server" ToolTip="Tarjeta dorso" ClientIDMode="Static" onchange="changeImageScanner('4');"/>
        <asp:Label ID="labelTjDorso" Text="" runat="server" />
    </div>
    <hr />
    <div class="col-sm-12 no-padding">        
        <asp:Button CssClass="btn btn-primary" ID="btnGuardar" Text="Guardar" runat="server" OnClientClick="Guardar();" OnClick="btnGuardar_Click" />
    </div>

    <script type="text/javascript">

        function Guardar() {
            if (validar()) {
                var dominio = $("#txtDominio").val();

                $("#hdnDominio").val(dominio);
            }
            else {
                alert('Ingrese alguna imagen');
                return false;
            }
        }

        function changeImageScanner(index) {            
            if (index == 1) {
                $("#imgDniFrente1").attr("src", "/img/scanner/dni-frente-foto-si.gif?timestamp=" + new Date().getTime());
            }
            if (index == 2) {
                $('#imgDniDorso1').attr("src", "/img/scanner/dni-dorso-foto-si.gif?timestamp=" + new Date().getTime());
            }
            if (index == 3) {
                $('#imgCedulaFrente1').attr("src", "/img/scanner/cedula-frente-foto-si.gif?timestamp=" + new Date().getTime());
            }
            if (index == 4) {
                $('#imgCedulaDorso1').attr("src", "/img/scanner/cedula-dorso-foto-si.gif?timestamp=" + new Date().getTime());
            }
        }

        function validar() {
            var fotoDNI1 = document.getElementById('<%=fileDniFrente.ClientID%>').files.length > 0;
            var fotoDNI2 = document.getElementById('<%=fileDniDorso.ClientID%>').files.length > 0;
            var fotoTJ1 = document.getElementById('<%=fileTjFrente.ClientID%>').files.length > 0;
            var fotoTJ2 = document.getElementById('<%=fileTjDorso.ClientID%>').files.length > 0;
            return (fotoDNI1 || fotoDNI2 || fotoTJ1 || fotoTJ2);
        }

       

       

        
    </script>    
    <style>
        .aa{
            background-color:aqua;
        }
    </style>
</div>



