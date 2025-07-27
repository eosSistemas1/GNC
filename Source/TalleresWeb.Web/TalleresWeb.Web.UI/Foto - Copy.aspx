<%@ Page Title="Fotos" Language="C#" AutoEventWireup="true" CodeBehind="Foto.aspx.cs" Inherits="TalleresWeb.Web.UI.Foto" %>

<html>
<body>
    <form runat="server" style="overflow: hidden;">
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script src='<%=ResolveUrl("~/Webcam_Plugin/jquery.webcam.js") %>' type="text/javascript"></script>
        <script type="text/javascript">
            var pageUrl = '<%=ResolveUrl("~/Foto.aspx") %>';
            $(function () {
                jQuery("#webcam").webcam({
                    width: 320,
                    height: 240,
                    mode: "save",
                    swffile: '<%=ResolveUrl("~/Webcam_Plugin/jscam.swf") %>',
                    //debug: function (type, status) {
                    //    $('#camStatus').append(type + ": " + status + '<br /><br />');
                    //},
                    onSave: function (data) {
                        $.ajax({
                            type: "POST",
                            url: pageUrl + "/GetCapturedImage",
                            data: '',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (r) {
                                var aleatorio = Math.ceil(Math.random() * 100);
                                var imgSrc = r.d + '?' + aleatorio;
                                
                                $("[id*=imgCapture]").css("visibility", "visible");
                                $("[id*=imgCapture]").attr("src", imgSrc);
                                //$("[id*=imgCapture]").hide(0).attr('src', imgSrc).show(0);
                            },
                            failure: function (response) {
                                alert(response.d);
                            }
                        });
                    },
                    onCapture: function () {
                        webcam.save(pageUrl);
                    }
                });
            });
            function Capture() {
                webcam.capture();
                return false;
            }
        </script>

        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center">
                    <u>Cámara</u>
                </td>
                <td></td>
                <td align="center">
                    <u>Imágen</u>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="webcam">
                    </div>
                </td>
                <td>&nbsp;
                </td>
                <td>
                    <asp:Image ID="imgCapture" runat="server" Style="visibility: hidden; width: 320px; height: 240px" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btnCapture" Text="Capture" runat="server" OnClientClick="return Capture();" />        
        <br />
        <%--<span id="camStatus" style="visibility: collapse;"></span>--%>
    </form>
</body>
</html>
