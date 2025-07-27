<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Despacho.aspx.cs" Inherits="PetroleraManagerIntranet.Web.Despacho.Despacho" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../css/messageButton.css" rel="stylesheet" />
    <link href="../css/despacho.css" rel="stylesheet" />
    
    <div id="contenido">
        <!-- Row de arriba -->
        <div class="row">
            <br />
            <div class="col-sm-10">
                <h4>
                    <asp:Label ID="lblTitulo" runat="server" Text="Despacho de trámites y mercadería"></asp:Label></h4>
            </div>
            <div class="col-sm-2">
            </div>
        </div>
        <!-- Fin de Row de arriba -->
        <!-- Row de contenido ("Row maestra") -->
        <div class="row">
            <div class="col-sm-9">
                <!-- Col del lado izquierdo sm-9 -->
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnBorrarElementoCarrito" />
                    </Triggers>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Este" />
                    </Triggers>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Sur" />
                    </Triggers>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Norte" />
                    </Triggers>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Comisionista" />
                    </Triggers>
                     <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Oeste" />
                    </Triggers>              
                        
                    <ContentTemplate>
                <div class="row">
                    <!-- Row 12 de zonas nomas -->
                    <div class="col-sm-2 mr-2">
                        <div class="notifications">
                            <div class="new-message">
                                <asp:Label ID="lblNorte" Text="" runat="server" />
                            </div>
                            <div class="messages">
                                <button id="Norte" runat="server" onserverclick="btnZona_ServerClick" type="button" class="btn btn-primary btn-sm"><i class="fa fa-map-marker" aria-hidden="true"></i>Norte</button>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="notifications">
                            <div class="new-message">
                                <asp:Label ID="lblSur" Text="" runat="server" />
                            </div>
                            <div class="messages">
                                <button id="Sur" runat="server" onserverclick="btnZona_ServerClick" type="button" class="btn btn-primary btn-sm"><i class="fa fa-map-marker" aria-hidden="true"></i>Sur</button>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="notifications">
                            <div class="new-message">
                                <asp:Label ID="lblEste" Text="" runat="server" />
                            </div>
                            <div class="messages">
                                <button id="Este" runat="server" onserverclick="btnZona_ServerClick" type="button" class="btn btn-primary btn-sm"><i class="fa fa-map-marker" aria-hidden="true"></i>Este</button>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="notifications">
                            <div class="new-message">
                                <asp:Label ID="lblOeste" Text=" Oeste" runat="server" />
                            </div>
                            <div class="messages">
                                <button id="Oeste" runat="server" onserverclick="btnZona_ServerClick" type="button" class="btn btn-primary btn-sm"><i class="fa fa-map-marker" aria-hidden="true"></i>Oeste</button>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="notifications">
                            <div class="new-message">
                                <asp:Label ID="lblComisionista" Text=" Comisionista" runat="server" />
                            </div>
                            <div class="messages">
                                <button id="Comisionista" runat="server" onserverclick="btnZona_ServerClick" type="button" class="btn btn-primary btn-sm"><i class="fa fa-map-marker" aria-hidden="true"></i>Comisionista</button>
                            </div>
                        </div>
                    </div>

                </div>

                <!-- Termina row de zonas-->

                <!-- Abre row de Talleres-->
 
                <div class="row">
           
                   
                    <div class="col-sm-11">
                        <br />
                        <asp:Repeater ID="repeaterDespacho" runat="server" OnItemDataBound="repeaterDespacho_ItemDataBound">
                            <ItemTemplate>
                                <div class="panel panel-default taller">
                                    <div class="panel-heading" role="tab" id="headingOne" >
                                        <asp:HiddenField ID="hdnTallerID" runat="server" Value='<%# Eval("ID")%>' />
                                        <asp:HiddenField ID="nombreTaller" runat="server" Value='<%# Eval("Descripcion")%>' />
                                        <h4 class="panel-title" data-toggle="collapse" data-target='<%# "#"+ Eval("ID") %>'>
                                            <a href="#"><strong><%# Eval("Descripcion")%></strong>
                                                <i class="more-less glyphicon glyphicon-plus"></i>
                                            </a>
                                        </h4>
                                    </div>
                                    
                                       <div id='<%# Eval("ID")%>' class="collapse">
                                        <asp:GridView ID="grdTramites" runat="server" class="table table-bordered table-hover"
                                            AutoGenerateColumns="false" DataKeyNames="ID, IdEstado"
                                            OnRowDataBound="grdTramites_RowDataBound">
                                            <Columns>
                                                <asp:BoundField HeaderText="FECHA"  DataField="Fecha" DataFormatString="{0:dd/MM/yyyy hh:mm}" />
                                                <asp:BoundField HeaderText="TRÁMITE" DataField="TipoTramite" />
                                                <asp:BoundField HeaderText="OPERACIÓN" DataField="Operacion" />
                                                <asp:BoundField HeaderText="DOMINIO" DataField="Dominio" />
                                                <asp:BoundField HeaderText="OBLEA ANTERIOR" DataField="ObleaAnterior" />
                                                <asp:BoundField HeaderText="OBLEA ASIGNADA" DataField="ObleaAsignada" />
                                                <asp:TemplateField ItemStyle-CssClass="estado-amarillo" HeaderText="ESTADO">
                                                    <ItemTemplate>
                                                        <span class="fa fa-circle"></span><%# Eval("Estado")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DESPACHAR" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>                                           
                                                        <asp:CheckBox ID="chkDespachar" runat="server" class="checkbox finalizados" Visible="false" ></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <br>
                    </div>
                    <div class="col-sm-1"></div>
                </div>
                        <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />

                     </ContentTemplate>
                   </asp:UpdatePanel>       

				<div class="row" style="width:97%">
                      <div class="col-sm-8">
                       </div>                     

                    <div class="col-sm-4">
                        <button id="btnAgregarAlCarrito" runat="server" onserverclick="btnAgregarTramite_Carrito" type="button" class="btn btn-primary btn-sm"><i class="fa fa-truck" aria-hidden="true"></i>Agregar al carrito</button>
                    </div>
                </div>
                
            </div>

            <div class="col-sm-3">
                <h1 class="btn btn-primary btn-sm tramiteSeleccionado" style="width: 100%; margin: 0px; margin-bottom: 23px;">Tr&aacute;mites seleccionados</h1>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">                    
                    
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAgregarAlCarrito" />

                    </Triggers>
                    
					 <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnBorrarElementoCarrito" />

                    </Triggers>
                    
                    <ContentTemplate>
                       
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>Taller
                   <br />
                                Tr&aacute;mite</th>
                            <th colspan="2" style="border-right: hidden;">Dominio</th>
                        </tr>
                    </thead>
                    <tbody>
					<asp:Repeater ID="repeaterZonas" runat="server" OnItemDataBound="repeaterZonas_ItemDataBound">
                            <ItemTemplate>
							<asp:HiddenField ID="hdnZonaTaller" runat="server" Value='<%# Eval("ZonaTaller")%>' />
							  <tr>
					            <th style="font-weight:bold; border-right: hidden;" colspan=4><%#Eval("ZonaTaller")%></th>
					            </tr>
							
                        <asp:Repeater ID="tablaDatos" runat="server">
                            <ItemTemplate>
                                
                                <tr>
                                    <td style="display:none;">
                                        <asp:HiddenField ID="hdnTramiteID" runat="server" Value='<%#Eval("IDTramite")%>' />
                                        <asp:HiddenField ID="hdnZonaTaller2" runat="server" Value='<%#Eval("ZonaTaller")%>' />
                                    </td>
                                    <td colspan="2" style="border-bottom: hidden;"><%#Eval("Taller")%></td>
                                    <td rowspan="2" style="border-left: hidden;">                                  
                                                        <asp:CheckBox ID="tramiteSel" runat="server"></asp:CheckBox>
                                                                 </td>
                                </tr>
                                <tr>
                                    <td style="border-right: hidden;"><%#Eval("TipoTramite")%></td>
                                    <td style="border-right: hidden;"><%#Eval("Dominio")%></td>
                                </tr>

                            </ItemTemplate>
                        </asp:Repeater>
						
				        </ItemTemplate>
                  </asp:Repeater>
                        <tr>
                            <td colspan="3">
                                <Label ID="NoTramites" visible="true" runat="server">No hay tramites seleccionados</Label>
                            </td>
                        </tr>
                    </tbody>

                </table>
                       
                    <div class="row">
                     <div class="col-sm-3"></div>
                     <div class="col-sm-5" style="padding-left:2px;">
                        <button id="btnBorrarElementoCarrito" runat="server" onserverclick="BorrarSeleccionadasCarrito"  type="button" class="btn btn-primary btn-sm text-center">
                            <i class="fa fa-trash" aria-hidden="true"></i>&nbsp Eliminar
                        </button>    
                      </div>  
                      <div class="col-sm-4"></div>   
                    </div>


                 </ContentTemplate>
                   </asp:UpdatePanel>

                <br />
                 <div class="row">
                     <div class="col-sm-2"></div>
                     <div class="col-sm-6">                                        
                                    <button id="btnAceptar" runat="server" onserverclick="btnAceptar_ServerClick" type="button" class="btn btn-primary btn-sm text-center"><i class="fa fa-truck" aria-hidden="true"></i>&nbsp Despachar</button>                 
                       </div>  
                      <div class="col-sm-4"></div>   
                    </div>
   <!-- Modal Aceptar Despacho-->
    <div class="modal fade" id="modalAceptar">
        <div class="modal-dialog" style="left: 0px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Confirmar despacho</h4>
                </div>
                <div class="modal-body" style="min-height: 70px;">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                             
                            <asp:Label ID="lblMessage" runat="server" />
                            <div class="col-sm-12 text-center">
                                Flete:<br />
                                <div class="col-sm-4 text-center">
                                    <asp:RadioButton ID="radFletePropio" runat="server" class="checkbox" Text="Propio" GroupName="flete" OnCheckedChanged="radFletePropio_CheckedChanged" AutoPostBack="true" />
                                </div>
                                <div class="col-sm-4 text-center">
                                    <asp:RadioButton ID="radFleteExterno" runat="server" class="checkbox" Text="Externo" GroupName="flete" OnCheckedChanged="radFletePropio_CheckedChanged" AutoPostBack="true" />
                                </div>
                                <div class="col-sm-4 text-center">
                                    <asp:RadioButton ID="radRetiraEnOficina" runat="server" class="checkbox" Text="Sucursal" GroupName="flete" OnCheckedChanged="radFletePropio_CheckedChanged" AutoPostBack="true" />
                                </div>
                            </div>
                            <div class="col-sm-12 text-center">
                                <Controls:CboFletes ID="cboFlete" runat="server" Visible="false" CssClass="form-control"></Controls:CboFletes>
                                <Controls:CboConductores ID="cboConductor" runat="server" Visible="false" CssClass="form-control"></Controls:CboConductores>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <hr />
                    <div class="col-sm-12 text-right">
                        <button id="btnGuardar" runat="server" onserverclick="btnGuardar_ServerClick" type="button" class="btn btn-primary">Guardar</button>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal Imprimir -->
    <div class="modal fade" id="modalImprimir">
        <div class="modal-dialog" style="left: 0px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Imprimir despacho</h4>
                </div>
                <div class="modal-body">
                    <iframe id="frameImprimir" style="width: 100%; height: 400px;"></iframe>
                </div>
                <div class="modal-footer">
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <button type="button" style="display: none;" id="btnShowPopupAceptar" class="btn btn-primary btn-lg"
        data-toggle="modal" data-target="#modalAceptar">
        AceptarDespacho
    </button>

    <button type="button" style="display: none;" id="btnShowPopupImprimir" class="btn btn-primary btn-lg"
        data-toggle="modal" data-target="#modalImprimir">
        ImprimirDespacho
    </button>

  <script type="text/javascript"> 

      function ShowPopupAceptar() {
          $("#btnShowPopupAceptar").click();
      }

      function ShowPopupImprimir(despachoID) {

          $("#btnShowPopupImprimir").click();

          var url = "ImprimirDespacho.aspx?id=" + despachoID;

          var $iframe = $("#frameImprimir");
          if ($iframe.length) {
              $iframe.attr('src', url);
              return false;
          }
      }

    </script>

            </div>
            <!-- Fin de la Row maestra -->
        </div>
    </div>
  </div>
   
</asp:Content>
