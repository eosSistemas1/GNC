<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Mobile.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PetroleraManagerIntranet.Web.PH.Proceso.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="wrapper-nm">
        <main id="central-nm" role="main">

            <div id="destacados" class="row">

                <div class="col-sm-6 text-center">
                    <a href="SeleccionarCilindro.aspx?estacion=1" class="btn btn-block btn-lg">
                        <i class="fa fa-code fa-5x"></i>
                        <br />
                        <h3><%=CrossCutting.DatosDiscretos.ESTACIONES.ESTACION1 %></h3>
                    </a>
                </div>
                <div class="col-sm-6 text-center">
                    <a href="SeleccionarCilindro.aspx?estacion=2" class="btn btn-block btn-lg">
                        <i class="fa fa-balance-scale fa-5x"></i>
                        <br />
                        <h3><%=CrossCutting.DatosDiscretos.ESTACIONES.ESTACION2 %></h3>
                    </a>
                </div>
                <div class="col-sm-6 text-center">
                    <a href="SeleccionarCilindro.aspx?estacion=3" class="btn btn-block btn-lg">
                        <i class="fa fa-circle-o fa-5x"></i>
                        <br />
                        <h3><%=CrossCutting.DatosDiscretos.ESTACIONES.ESTACION3 %></h3>
                    </a>
                </div>
                <div class="col-sm-6 text-center">
                    <a href="SeleccionarCilindro.aspx?estacion=4" class="btn btn-block btn-lg">
                        <i class="fa fa-binoculars fa-5x"></i>
                        <br />
                        <h3><%=CrossCutting.DatosDiscretos.ESTACIONES.ESTACION4 %></h3>
                    </a>
                </div>
            </div>
        </main>
    </div>
</asp:Content>
