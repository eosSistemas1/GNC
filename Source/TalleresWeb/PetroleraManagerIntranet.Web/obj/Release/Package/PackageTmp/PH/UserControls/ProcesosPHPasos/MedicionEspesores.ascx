<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MedicionEspesores.ascx.cs" Inherits="PetroleraManagerIntranet.Web.UserControls.ProcesosPHPasos.MedicionEspesores" %>

<div class="row forms col-sm-12">

    <div class="col-sm-4">
        <p><strong>SOBRE FONDO</strong></p>
    </div>
    <div class="col-sm-8"></div>
    <div class="col-sm-12">
        <div class="col-sm-6"><strong>Tipo Fondo:&nbsp;</strong></div>
        <div class="col-sm-6">
            <Controls:CboTipoFondo ID="txtTipoFondo" runat="server"></Controls:CboTipoFondo>
        </div>
    </div>
    <div class="col-sm-12">
        <div class="col-sm-6"><strong>Espesor Fondo:&nbsp;</strong></div>
        <div class="col-sm-6">
            <asp:TextBox ID="txtLecturaFondoA" runat="server" CssClass="form-control mg" />
        </div>
    </div>

    <hr />

    <div class="col-sm-4">
        <p><strong>SOBRE PARED</strong></p>
    </div>
    <div class="col-sm-8"></div>
    <div class="col-sm-12">
        <div class="col-sm-6"><strong>Espesor MAX:&nbsp;</strong></div>
        <div class="col-sm-6">
            <asp:TextBox ID="txtLecturaParedA" runat="server" CssClass="form-control mg" />
        </div>
    </div>
    <div class="col-sm-12">
        <div class="col-sm-6"><strong>Espesor Medido en Pared (B):&nbsp;</strong></div>
        <div class="col-sm-6">
            <asp:TextBox ID="txtLecturaParedB" runat="server" CssClass="form-control mg" />
        </div>
    </div>

</div>
