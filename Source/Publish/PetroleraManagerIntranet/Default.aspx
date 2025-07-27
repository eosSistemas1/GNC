<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div id="destacados" class="row">
        <div class="col-sm-6 text-center">
            <a href="/Obleas/ObleasIngresar.aspx" class="btn btn-block btn-lg">
                <i class="fa fa-file-text-o fa-5x"></i>
                <br>
                <h3>Ingresar Ficha Técnica</h3>
            </a>
        </div>
        <div class="col-sm-6 text-center">
            <a href="/Obleas/ObleasReimpresionTarjetaVerde.aspx" class="btn btn-block btn-lg">
                <i class="fa fa-print fa-5x"></i>
                <br>
                <h3>Reimprimir Tarjeta Verde</h3>
            </a>
        </div>
    </div>

</asp:Content>
