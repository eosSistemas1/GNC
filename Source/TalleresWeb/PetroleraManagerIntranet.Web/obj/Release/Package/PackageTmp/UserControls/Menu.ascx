<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="PetroleraManagerIntranet.Web.UserControls.Menu" %>



<ul id="sidemenu" class="sidebar-nav">    
    <li class="destacado">
        <a href="#top" data-toggle="sidebar-colapse">
            <div class="d-flex w-100 justify-content-start align-items-right">
                <span id="collapse-icon" class="fa fa-2x mr-3"></span>
                <span id="collapse-text" class="menu-collapsed"></span>
            </div>
        </a>
    </li>
    <li>
        <a class="accordion-toggle collapsed toggle-switch" data-toggle="collapse" href="#submenu-2">
            <span class="sidebar-icon"><i class="fa fa-list-alt"></i></span>
            <span class="sidebar-title">Trámites Obleas</span>
            <b class="caret"></b>
        </a>
        <ul id="submenu-2" class="panel-collapse collapse panel-switch" role="menu">
            <li><a href="/Obleas/ObleasIngresar.aspx"><i class="fa fa-plus"></i>Ingresar Fichas Técnicas</a></li>
            <li><a href="/Obleas/ConsultarFichasTecnicas.aspx"><i class="fa fa-file-text"></i>Consultar Fichas Técnicas</a></li>
            <li><a href="/Obleas/ObleasInformar.aspx"><i class="fa fa-sign-in"></i>Informar Fichas Técnicas</a></li>
            <li><a href="/Obleas/CargarResultadosEnte.aspx"><i class="fa fa-building-o"></i>Cargar Resultados ENTE</a></li>
            <li><a href="/Obleas/ObleasAsignar.aspx"><i class="fa fa-check"></i>Asignación de Obleas</a></li>
            <li><a href="/Obleas/ObleasReimpresionTarjetaVerde.aspx"><i class="fa fa-check"></i>Reimprimir tarjeta verde</a></li>
            <li><a href="/Obleas/ReInformarFichaEntregada.aspx"><i class="fa fa-check"></i>Re-Informar ficha entregada</a></li>
            <li><a href="/Obleas/Informes/InformesEstadosObleas.aspx"><i class="fa fa-check"></i>Consultar Obleas</a></li>
            <li><a href="/Obleas/Informes/InformesObleasRealizadas.aspx"><i class="fa fa-check"></i>Obleas Realizadas</a></li>
            <li><a href="/Obleas/CorregirDominioConError.aspx"><i class="fa fa-check"></i>Corregir Dominio Con Error</a></li>
        </ul>
    </li>
    <li>
        <a class="accordion-toggle collapsed toggle-switch" data-toggle="collapse" href="#submenu-5">
            <span class="sidebar-icon"><i class="fa fa-cog"></i></span>
            <span class="sidebar-title">Sistema</span>
            <b class="caret"></b>
        </a>
        <ul id="submenu-5" class="panel-collapse collapse panel-switch" role="menu">
            <li><a href="/Administraciones/Cilindros.aspx"><i class="fa fa-plus"></i>Cilindros</a></li>
            <li><a href="/Administraciones/Clientes.aspx"><i class="fa fa-plus"></i>Clientes</a></li>
            <li><a href="/Administraciones/CRPC.aspx"><i class="fa fa-plus"></i>CRPC</a></li>
            <li><a href="/Administraciones/Localidades.aspx"><i class="fa fa-plus"></i>Localidades</a></li>
            <%--<li><a href="/Administraciones/PEC.aspx"><i class="fa fa-plus"></i>PEC</a></li>--%>
            <li><a href="/Administraciones/Reguladores.aspx"><i class="fa fa-plus"></i>Reguladores</a></li>
            <%--<li><a href="/Administraciones/RT.aspx"><i class="fa fa-plus"></i>Responsable T&eacute;cnico</a></li>--%>
            <%--<li><a href="/Administraciones/S_Roles.aspx"><i class="fa fa-plus"></i>Roles</a></li>--%>
            <%--<li><a href="/Administraciones/Talleres.aspx"><i class="fa fa-plus"></i>Talleres</a></li>--%>
            <li><a href="/Administraciones/Usuarios.aspx"><i class="fa fa-plus"></i>Usuarios</a></li>
            <li><a href="/Administraciones/Valvula.aspx"><i class="fa fa-plus"></i>V&aacute;lvulas</a></li>
            <li><a href="/Administraciones/Vehiculos.aspx"><i class="fa fa-plus"></i>Vehículos</a></li>
        </ul>
    </li>
</ul>
