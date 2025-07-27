<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TalleresWeb.Web.UI._Default" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="destacados" class="row">
        <div class="col-sm-4 text-center">
            <a href="/Tramites/Obleas/Default.aspx" class="btn btn-block btn-lg">
                <i class="fa fa-list-alt fa-5x"></i>
                <br>
                <h3>Nueva Oblea</h3>
            </a>
        </div>
        <div class="col-sm-4 text-center">
            <a href="/Tramites/Obleas/FotosCapturar.aspx" class="btn btn-block btn-lg">
                <i class="fa fa-camera-retro fa-5x"></i>
                <br>
                <h3>Ingresar Documentacion</h3>
            </a>
        </div>
        <div class="col-sm-4 text-center">
            <a href="/Tramites/PH/Default.aspx" class="btn btn-block btn-lg">
                <i class="fa fa-check fa-5x"></i>
                <br>
                <h3>Prueba Hidráulica</h3>
            </a>
        </div>
    </div>

    <div id="contenido">
        <div class="row">
            <%--<div class="col-sm-12">
			            <h3>Novedades</h3>
                    </div>
                	<div class="col-md-4">
			            <h4>Talleristas</h4>
                        <ul>
                        	<li>Phasellus mollis eget arcu sed mollis. Maecenas vitae facilisis purus.</li>
                        	<li>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur suscipit erat risus, a faucibus urna dignissim sit amet. Praesent nec venenatis libero.</li>
                        	<li>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur suscipit erat risus, a faucibus urna dignissim sit amet. Praesent nec venenatis libero.</li>
                        </ul>
                    </div>
                	<div class="col-md-4">
			            <h4>GNC</h4>
                        <ul>
                        	<li>Phasellus mollis eget arcu sed mollis. Maecenas vitae facilisis purus. Curabitur suscipit erat risus, a faucibus urna dignissim sit amet.</li>
                        	<li>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur suscipit erat risus, a faucibus urna dignissim sit amet. Praesent nec venenatis libero.</li>
                        </ul>
                    </div>
                	<div class="col-md-4">
			            <h4>Legales</h4>
                        <ul>
                        	<li>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur suscipit erat risus, a faucibus urna dignissim sit amet. Praesent nec venenatis libero.</li>
                        	<li>Phasellus mollis eget arcu sed mollis. Maecenas vitae facilisis purus.</li>
                        	<li>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur suscipit erat risus, a faucibus urna dignissim sit amet. Praesent nec venenatis libero.</li>
                        </ul>
                    </div>--%>
        </div>
    </div>
</asp:Content>
