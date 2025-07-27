<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistroPesos.ascx.cs" Inherits="PetroleraManagerIntranet.Web.UserControls.ProcesosPHPasos.RegistroPesos" %>

<div class="row forms col-sm-12">
    <div class="col-sm-6">
        <strong>Peso vacío:&nbsp;</strong>
    </div>
    <div class="col-sm-6">
        <asp:TextBox ID="txtPesoVacio" runat="server" CssClass="form-control mg" />
    </div>

    <div class="col-sm-6">
        <strong>Peso marcado cilindro:&nbsp;</strong>
    </div>
    <div class="col-sm-6">
        <asp:TextBox ID="txtPesoMarcadoCilindro" runat="server" class="form-control mg" />
    </div>
</div>
