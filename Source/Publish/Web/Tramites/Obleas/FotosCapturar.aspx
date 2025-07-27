<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FotosCapturar.aspx.cs" Inherits="TalleresWeb.Web.UI.Tramites.Obleas.FotosCapturar" %>

<%@ Register Src="~/UserControls/Fotos.ascx" TagPrefix="uc1" TagName="Fotos" %>
<%@ Register Src="~/UserControls/SubirFotos.ascx" TagPrefix="uc1" TagName="SubirFotos" %>


<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <link href="/css/wizard.css" rel="stylesheet" />    

    <div class="forms">

        <div class="col-sm-12">
            <h3>Ingresar Documentación</h3>
        </div>
        <hr>

        <div class="col-sm-12">
            <h4>Trámite</h4>
        </div>

        <%--<asp:Panel ID="pnlBuscar" runat="server" DefaultButton="btnBuscar">--%>

<%--            <div class="col-sm-3 form-group">
                <label for="cboTipoOperacion">Operación:</label>
                <asp:DropDownList ID="cboTipoOperacion" runat="server" onchange="cboTipoOperacionChange(this)" ClientIDMode="Static" class="form-control pull-right" />
            </div>

            <div id="divObleaAnterior" class="col-sm-3 form-group">
                <label for="txtNroObleaAnterior">Oblea anterior:</label>
                <asp:TextBox ID="txtNroObleaAnterior" Text="" runat="server" MaxLength="11" class="form-control numerico pull-right" placeholder="oblea anterior" ClientIDMode="Static" />
            </div>

            <div class="col-sm-2 form-group">
                <label for="MainContent_txtNroDocumento">&nbsp;</label>
                <asp:Button ID="btnBuscar" Text="Aceptar" runat="server" OnClick="btnBuscar_Click" class="btn btn-primary btn-block" />
            </div>
            <div class="col-sm-4 form-group"></div>--%>
        <%--</asp:Panel>--%>

        <div class="col-sm-12">&nbsp;</div>

        <asp:Panel ID="pnlIngresar" runat="server" Visible="false">
            <div class="col-sm-12">
                <div class="col-sm-4 no-padding">
                    <p>Fecha:&nbsp;</p>
                    <p>
                        <asp:TextBox ID="txtFecha" runat="server" class="form-control" type="Date" MaxLength="10" />
                    </p>
                </div>
<%--                <div class="col-sm-4 no-padding">
                    <p>Operación:&nbsp;</p>
                    <p>
                        <asp:Label ID="lblOperacion" runat="server" ClientIDMode="Static"></asp:Label>
                    </p>
                </div>
                <div class="col-sm-4 no-padding">
                    <p>Oblea anterior:&nbsp;</p>
                    <p>
                        <asp:Label ID="lblObleaAnterior" runat="server" ClientIDMode="Static"></asp:Label>
                    </p>
                </div>--%>
            </div>

            <hr>

            <%--<div class="col-sm-6">
                <h4>Cliente</h4>
                <div class="col-sm-3 no-padding">
                    <p>Documento:</p>
                </div>
                <div class="col-sm-4 no-padding">
                    <p>
                        <CONTROLS:CboTipoDocumento ID="cboTipoDocumento" runat="server" AutomaticLoad="true" css="form-control" WidthCbo="100px" />
                    </p>
                </div>
                <div class="col-sm-5 no-padding">
                    <p>
                        <asp:TextBox ID="txtNumeroDocumento" runat="server" class="form-control numerico" AutoPostBack="true" OnTextChanged="txtModalNumeroDocumento_TextChanged" MaxLength="11"></asp:TextBox>
                    </p>
                </div>

                <div class="col-sm-3 no-padding">
                    <p>Cliente:</p>
                </div>
                <div class="col-sm-9 no-padding">
                    <p>
                        <asp:TextBox ID="txtNombreApellido" runat="server" class="form-control"></asp:TextBox>
                    </p>
                </div>

                <div class="col-sm-3 no-padding">
                    <p>Domicilio:</p>
                </div>
                <div class="col-sm-9 no-padding">
                    <p>
                        <asp:TextBox ID="txtDomicilio" runat="server" class="form-control"></asp:TextBox>
                    </p>
                </div>

                <div class="col-sm-3 no-padding">
                    <p>Tel. Fijo:</p>
                </div>
                <div class="col-sm-3 no-padding">
                    <p>
                        <asp:TextBox ID="txtTelefono" runat="server" class="form-control numerico"></asp:TextBox>
                    </p>
                </div>
                <div class="col-sm-3 no-padding">
                    <p>Tel. Celular:</p>
                </div>
                <div class="col-sm-3 no-padding">
                    <p>
                        <asp:TextBox ID="txtCelular" runat="server" class="form-control numerico"></asp:TextBox>
                    </p>
                </div>

                <div class="col-sm-3 no-padding">
                    <p>Email:</p>
                </div>
                <div class="col-sm-9 no-padding">
                    <p>
                        <asp:TextBox ID="txtEmail" runat="server" class="form-control" type="email"></asp:TextBox>
                    </p>
                </div>

                <div class="col-sm-3 no-padding">
                    <p>Localidad:</p>
                </div>
                <div class="col-sm-9 no-padding">
                    <p>
                        <CONTROLS:CboLocalidad ID="cboLocalidad" runat="server" AutomaticLoad="true" css="form-control" />
                    </p>
                </div>

            </div>--%>

            

            <hr>
            <hr>
            <hr>

            <div class="col-sm-3">
                <h4>Observaciones:</h4>
                <div class="col-sm-12 no-padding">
                    <asp:TextBox ID="txtObservaciones" runat="server" Rows="5" Height="100px" TextMode="MultiLine" class="form-control" />
                </div>
            </div>
            
            <div class="col-md-3">
                <asp:Button ID="btnAceptar" runat="server" class="btn btn-primary" OnClick="btnAceptar_Click" Text="Aceptar" />
                <asp:Button ID="btnCancelar" runat="server" class="btn btn-danger" OnClick="btnCancelar_Click" Text="Cancelar" />
            </div>
        </asp:Panel>


        <div class="col-sm-6">
                <h4>Vehículo</h4>
                <div class="col-sm-3 no-padding">Dominio:</div>
                <div class="col-sm-9 no-padding">
                    <asp:TextBox ID="txtDominio" runat="server" class="form-control" MaxLength="7" OnTextChanged="txtModalDominio_TextChanged" AutoPostBack="true" ClientIDMode="Static"></asp:TextBox>
                </div>

                <%--<div class="col-sm-3 no-padding">Marca:</div>
                <div class="col-sm-9 no-padding">
                    <asp:TextBox ID="txtMarca" runat="server" class="form-control"></asp:TextBox>
                </div>

                <div class="col-sm-3 no-padding">Modelo:</div>
                <div class="col-sm-9 no-padding">
                    <asp:TextBox ID="txtModelo" runat="server" class="form-control"></asp:TextBox>
                </div>

                <div class="col-sm-3 no-padding">Año:</div>
                <div class="col-sm-9 no-padding">
                    <asp:TextBox ID="txtAnio" runat="server" class="form-control"></asp:TextBox>
                </div>

                <div class="col-sm-3 no-padding">Es Inyección:</div>
                <div class="col-sm-9 no-padding">
                    <asp:CheckBox ID="chkEsInyeccion" runat="server" class="form-control"></asp:CheckBox>
                </div>--%>
            </div>

        <div class="col-sm-6">
                <h4>Imágenes:</h4>
                <table border="0" style="width: 100%; text-align: center;">
                    <tr>
                        <td></td>
                        <td>
                            <button type="button" class="btn-default" onclick="openModalCapturarFotos();">Capturar</button>
                        </td>
                        <td>                            
                        </td>
                    </tr>                    
                </table>
            </div>

        <!--Modal Fotos-->
        <div id="modalFotos" class="modal forms" role="dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4>Captura de imágenes</h4>
                </div>
                <div class="modal-body no-padding text-center">
                    <uc1:Fotos runat="server" ID="Fotos" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
        
    </div>
     <!--fin Modal fotos-->

    <script type="text/javascript">      
        function cancelar() {
            location.href = '../../Default.aspx';
        }

        function openModalCapturarFotos() {

            var dominio = $('#<%=txtDominio.ClientID %>').val();

            if (dominio && dominio != "" && dominio.length >=6) {
                $('#modalFotos').modal('show');
                start();
            }
            else
            {
                alert('Debe ingresar el dominio.');
                return false;
            }            
        }

        $(document).ready(function () { });

        $("#modalFotos").on("hidden.bs.modal", function () {
            stopVideo();
            clearphoto();
        });
    </script>
</asp:Content>
