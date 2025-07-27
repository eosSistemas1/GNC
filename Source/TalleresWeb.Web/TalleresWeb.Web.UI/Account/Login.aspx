<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TalleresWeb.Web.UI.Account.Login" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>
<!DOCTYPE html>


<!DOCTYPE html>
<html lang="es">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="Empresa rosarina dedicada a la venta, comercialización y renovación de todos los elementos del sistema GNC automotor.">
    <meta name="keywords" content="gnc, gas, oblea, renovación, renovar, auto, taller, talleres, rosario, santa fe">
    <meta name="author" content="Hedra - Diseño y comunicación">

    <title>Petrolera ItaloArgentina</title>

    <!-- Bootstrap Core CSS -->
    <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="../vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Raleway:300,400,600,700" rel="stylesheet">
    
    <link rel="shortcut icon" href="img/favicon.ico">
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="img/favicon144.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="img/favicon114.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="img/favicon72.png">
    <link rel="apple-touch-icon-precomposed" href="img/favicon57.png">

    <!-- Plugin CSS -->
    <link href="../vendor/magnific-popup/magnific-popup.css" rel="stylesheet">

    <!-- Theme CSS -->
    <link href="../css/creative.css" rel="stylesheet">

</head>

<body id="login">

    <nav id="mainNav" class="navbar navbar-default-login navbar-fixed-top">
        <div class="container-fluid">
            <div class="navbar-header">
                <a class="navbar-brand page-scroll"><img src="../img/logo.png" alt="Petrolera ItaloArgentina" width="209" height="42"></a>
            </div>
        </div>
    </nav>

    <section class="login">
        <div class="datos text-center">
            <h3>Iniciar sesión</h3>
            <form id="form1" runat="server">
                <asp:ScriptManager runat="server"></asp:ScriptManager>
                <div class="input-group">
                    <span class="input-group-addon"><i class="fa fa-user"></i></span>
                    <asp:TextBox ID="UserName" runat="server" class="form-control" placeholder="Usuario" autofocus />                    
                </div>
                <div class="input-group">
                    <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                    <asp:TextBox ID="Password" runat="server" class="form-control" TextMode="Password" placeholder="Contraseña"></asp:TextBox>                    
                </div>
                <asp:Button ID="btnLogin" runat="server" Text="Ingresar" ValidationGroup="LoginUserValidationGroup" class="btn btn-primary btn-block" OnClick="btnLogin_Click" />                
                <div class="col-xs-6 pull-left text-left"><a href="#">Olvidé mi Contraseña</a></div>
                <div class="col-xs-6 pull-right text-right">
                    <asp:CheckBox id="RememberMe" type="checkbox" name="RememberMe" runat="server" />
                    <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Recordarme</asp:Label>                    
                </div>

                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                    CssClass="failureNotification" ErrorMessage="Ingrese Usuario." ToolTip="Ingrese Usuario."
                    ValidationGroup="LoginUserValidationGroup" Display="Dynamic">* Ingrese Usuario</asp:RequiredFieldValidator>

                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                    CssClass="failureNotification" ErrorMessage="Ingrese contraseña." ToolTip="Ingrese contraseña."
                    ValidationGroup="LoginUserValidationGroup" Display="Dynamic">* Ingrese contraseña.</asp:RequiredFieldValidator>

                <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />
            </form>
        </div>
	</section>

    <!-- jQuery -->
    <script src="../vendor/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="../vendor/bootstrap/js/bootstrap.min.js"></script>

    <!-- Plugin JavaScript -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-easing/1.3/jquery.easing.min.js"></script>
    <script src="../vendor/scrollreveal/scrollreveal.min.js"></script>
    <script src="../vendor/magnific-popup/jquery.magnific-popup.min.js"></script>

    <!-- Theme JavaScript -->
    <script src="../js/creative.min.js"></script>
    
    
</body>

</html>

