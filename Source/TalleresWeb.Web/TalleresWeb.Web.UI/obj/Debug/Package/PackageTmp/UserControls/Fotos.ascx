<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Fotos.ascx.cs" Inherits="TalleresWeb.Web.UI.UserControls.Fotos" %>

<asp:HiddenField ID="hdnTallerID" ClientIDMode="Static" runat="server" />

<div class="col-sm-12 no-padding">
    <div class="col-sm-3 no-padding">
        <a href="#tab1" data-toggle="tab" onclick="showTab(1);">
            <span class="step">
                <img id="imgDniFrenteC" src="/img/camara/dni-frente-foto-no.gif" alt="Dni Frente" width="100" />
            </span>
            <span class="title">Dni Frente</span>
        </a>
    </div>
    <div class="col-sm-3 no-padding">
        <a href="#tab2" data-toggle="tab" onclick="showTab(2);">
            <span class="step">
                <img id="imgDniDorsoC" src="/img/camara/dni-dorso-foto-no.gif" alt="Dni Dorso" width="100" />
            </span>
            <span class="title">Dni Dorso</span>
        </a>
    </div>
    <div class="col-sm-3 no-padding">
        <a href="#tab3" data-toggle="tab" onclick="showTab(3);">
            <span class="step">
                <img id="imgCedulaFrenteC" src="/img/camara/cedula-frente-foto-no.gif" alt="Dni Frente" width="100" />
            </span>
            <span class="title">Cédula Frente</span>
        </a>
    </div>
    <div class="col-sm-3 no-padding">
        <a href="#tab4" data-toggle="tab" onclick="showTab(4);">
            <span class="step">
                <img id="imgCedulaDorsoC" src="/img/camara/cedula-dorso-foto-no.gif" alt="Dni Dorso" width="100" />
            </span>
            <span class="title">Cédula Dorso</span>
        </a>
    </div>
</div>


<div class="tab-pane" id="tab1">
    <br>
    <input type="hidden" id="hdnTipoImagen" />
    <h3><strong>
        <label id="lblPaso"></label>
    </strong>
        <label id="lblDescPaso"></label>
    </h3>

    <div class="col-sm-12 no-padding">
        <select id="videoSelect" onchange="start();"></select>
        <%--<button id="startCameraButton" >Iniciar Cámara</button>--%>
        <br />

        <div class="col-sm-6 no-padding">
            Cámara<br />
            <div class="camera">
                <video id="video">Cámara no dispobible.</video>
                <br />
                <button id="takePictureButton">Tomar Foto</button>
            </div>
        </div>
        <div class="col-sm-6 no-padding"></div>
        Imágen<br />
        <canvas id="canvas"></canvas>
        <br />
    </div>
</div>


<script>
    var currentIndex = 0;
    function showTab(index) {

        $("#lblPaso").text("Paso " + index);

        if (index == 1) {
            $("#hdnTipoImagen").val("DNIFRENTE");
            $("#lblDescPaso").text("- Dni Frente");
        }
        if (index == 2) {
            $("#hdnTipoImagen").val("DNIDORSO");
            $("#lblDescPaso").text("- Dni Dorso");
        }
        if (index == 3) {
            $("#hdnTipoImagen").val("TJFRENTE");
            $("#lblDescPaso").text("- Cédula Frente");
        }
        if (index == 4) {
            $("#hdnTipoImagen").val("TJDORSO");
            $("#lblDescPaso").text("- Cédula Dorso");
        }

        currentIndex = index;

        return false;
    }
</script>

<script>
    var width = 320;
    var height = 0;
    var streaming = false;
    var localstream = null;

    //startCameraButton.onclick = start;
    takePictureButton.onclick = takepicture;

    navigator.mediaDevices.enumerateDevices()
        .then(gotDevices)
        .catch(function (err) {
            console.log("An error occured while getting device list! " + err);
        });

    function gotDevices(deviceInfos) {
        while (videoSelect.firstChild) {
            videoSelect.removeChild(videoSelect.firstChild);
        }

        for (var i = 0; i !== deviceInfos.length; ++i) {
            var deviceInfo = deviceInfos[i];
            var option = document.createElement('option');
            option.value = deviceInfo.deviceId;
            if (deviceInfo.kind === 'videoinput') {
                option.text = deviceInfo.label || 'Camera ' + (videoSelect.length + 1);
                videoSelect.appendChild(option);
            }
        }

        var ck = getCookie('camara');
    }

    function getCookie(name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');

        for (var i = 0; i < ca.length; i++) {

            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0) {
                return decodeURIComponent(c.substring(nameEQ.length, c.length));
            }

        }

        return null;
    }

    function start() {
        stopVideo();
        clearphoto();
        var videoSource = videoSelect.value;
        var constraints = {
            audio: false,
            video: { deviceId: videoSource ? { exact: videoSource } : undefined }
        };
        navigator.mediaDevices.getUserMedia(constraints).
            then(gotStream).then(gotDevices).catch(handleError);

        return false;
    }



    function gotStream(stream) {
        localstream = stream;
        video.srcObject = stream;
        video.play();
        // Refresh button list in case labels have become available
        return navigator.mediaDevices.enumerateDevices();
    }

    function handleError(error) {
        console.log('navigator.getUserMedia error: ', error);
    }

    video.addEventListener('canplay', function (ev) {
        if (!streaming) {
            height = video.videoHeight / (video.videoWidth / width);
            video.setAttribute('width', width);
            video.setAttribute('height', height);
            canvas.setAttribute('width', width);
            canvas.setAttribute('height', height);

            streaming = true;
        }
    }, false);

    clearphoto();

    function clearphoto() {
        var context = canvas.getContext('2d');
        context.fillStyle = "#AAA";
        context.fillRect(0, 0, canvas.width, canvas.height);
    }

    function showPicture() {

        //var canvas = document.getElementById('viewport'),
        var context = canvas.getContext('2d');

        base_image = new Image();
        base_image.src = '/Captures/1c39e85c-691f-49e8-a6e0-351dbd52d675_enx888_DNIDORSO.png';
        context.drawImage(base_image, 100, 100);
    }

    function takepicture() {
        var context = canvas.getContext('2d');
        if (width && height) {
            canvas.width = width;
            canvas.height = height;
            context.drawImage(video, 0, 0, width, height);

            var dataURL = canvas.toDataURL("image/jpeg", 0.95);
            if (dataURL && dataURL != "data:,") {
                uploadimage(dataURL);
                changeImageCamara(currentIndex);
                return false;
            }
            else {
                console.log("Image not available");
            }
        }
        else {
            clearphoto();
        }
    }

    function changeImageCamara(index) {
        if (index == 1) {
            $('#imgDniFrenteC').attr('src', '/img/camara/dni-frente-foto-si.gif');
            showTab(2);
        }
        if (index == 2) {
            $('#imgDniDorsoC').attr('src', '/img/camara/dni-dorso-foto-si.gif');
            showTab(3);
        }
        if (index == 3) {
            $('#imgCedulaFrenteC').attr('src', '/img/camara/cedula-frente-foto-si.gif');
            showTab(4);
        }
        if (index == 4) {
            $('#imgCedulaDorsoC').attr('src', '/img/camara/cedula-dorso-foto-si.gif');
        }
    }

    function uploadimage(dataurl) {

        var dominio = $("#txtDominio").val();
        var tipoFoto = $("#hdnTipoImagen").val();
        var tallerID = $("#hdnTallerID").val();

        $.ajax({
            url: '../../WebService1.asmx/UploadFile',
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: "{ 'imagen' : '" + JSON.stringify(dataurl) + "', 'tipoFoto' : '" + tipoFoto + "', 'dominio' : '" + dominio + "', 'tallerID' : '" + tallerID + "'}",
            success: function (response) {
                alert('La imagen se capturó correctamente');
            },
            error: function (jqXHR, exception) {
                var msg = '';
                if (jqXHR.status === 0) {
                    msg = 'Not connect.\n Verify Network.';
                } else if (jqXHR.status == 404) {
                    msg = 'Requested page not found. [404]';
                } else if (jqXHR.status == 500) {
                    msg = 'Internal Server Error [500].';
                } else if (exception === 'parsererror') {
                    msg = 'Requested JSON parse failed.';
                } else if (exception === 'timeout') {
                    msg = 'Time out error.';
                } else if (exception === 'abort') {
                    msg = 'Ajax request aborted.';
                } else {
                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                }
                alert(msg);
            }
        });

    }

    function stopVideo() {
        if (localstream) {
            localstream.getTracks().forEach(function (track) {
                track.stop();
                localstream = null;
            });
        }
    }

    $("#videoSelect")
        .change(function () {
            var str = "";
            $("#videoSelect option:selected").each(function () {
                var ck = "camara=" + $(this).text();
                document.cookie = ck;
            });

        })

    $(document).ready(function () {

        showTab(1);

        $("#videoSelect option").each(function () {
            if ($(this).text() == getCookie('camara')) {
                $(this).attr('selected', 'selected');
            }
        });
    });
</script>

