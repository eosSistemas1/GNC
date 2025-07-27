<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EliminarOblea.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Obleas.EliminarOblea" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagName="MessageBoxCtrl" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="col-sm-12">
        <h4>Eliminar Oblea</h4>
    </div>

    <div class="col-sm-3"><strong>Nro. Oblea Anterior:</strong></div>
    <div class="col-sm-9">
        <asp:Label ID="lblObleaAnterior" Text="" runat="server" />
    </div>

    <div class="col-sm-3"><strong>Titular:</strong></div>
    <div class="col-sm-9">
        <asp:Label ID="lblTitular" Text="" runat="server" />
    </div>

    <div class="col-sm-3"><strong>Vehículo:</strong></div>
    <div class="col-sm-9">
        <asp:Label ID="lblVehiculo" Text="" runat="server" />
    </div>

    <div class="col-sm-3"><strong>Taller:</strong></div>
    <div class="col-sm-9">
        <asp:Label ID="lblTaller" Text="" runat="server" />
    </div>

    <div class="col-sm-3"><strong>Observaciones:</strong></div>
    <div class="col-sm-9">
        <asp:Label ID="lblObservaciones" Text="" runat="server" />
    </div>

    <hr />

    <div class="col-sm-12">
        <asp:TextBox id="txtObservaciones" Width="100%" runat="server" height="50px" Rows="20" Columns="50" TextMode="MultiLine"></asp:TextBox>
    </div>

    <hr />
    <div class="col-sm-12">
        <div class="col-sm-8 no-padding"></div>
        <div class="col-sm-2 no-padding">
            <button type="button" class="btn btn-primary btn-block nn" aria-label="" name="" id="btnAceptar" runat="server" onserverclick="btnAceptar_ServerClick" title="Guardar" alt="Guardar/Imprimir"><i class="fa fa-check" aria-hidden="true"></i>&nbsp; Eliminar</button>
        </div>
        <div class="col-sm-2 no-padding">
            <a href="ConsultarFichasTecnicas.aspx" class="btn btn-danger btn-block nn" aria-label="" name="" id="" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp; Cancelar</a>
        </div>
    </div>

    <uc1:MessageBoxCtrl ID="MessageBoxCtrl1" runat="server" />
</asp:Content>
