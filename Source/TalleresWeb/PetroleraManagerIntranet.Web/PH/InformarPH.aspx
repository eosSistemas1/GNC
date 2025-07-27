<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InformarPH.aspx.cs" Inherits="PetroleraManagerIntranet.Web.PH.InformarPH" %>

<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function Check(parentChk) {
            var elements = document.getElementsByTagName("input");
            for (i = 0; i < elements.length; i++) {
                if (parentChk.checked == true) {
                    if (IsCheckBox(elements[i])) {
                        elements[i].checked = true;
                    }
                }
                else {
                    elements[i].checked = false;
                }
            }

        }

        function IsCheckBox(chk) {
            if (chk.type == 'checkbox') return true;
            else return false;
        }
    </script>

    <div id="contenido">
        <div class="row">            
            <div class="col-sm-12">
                <h4>INFORMAR PRUEBAS HIDRÁULICAS<asp:Label ID="lblTituloPagina" runat="server" Text=""></asp:Label></h4>                
            </div>
            <hr />
        </div>        

        <div class="col-sm-12" style="overflow: auto; height: 380px; width: 100%; text-align: center;">
            <asp:GridView ID="grdPH" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="ID"
                class="table table-bordered table-hover">
                <Columns>
                    <asp:BoundField HeaderText="Taller" DataField="Taller" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Fecha" DataField="FechaHabilitacion" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField HeaderText="Nro. oblea" DataField="ObleaAnterior" />
                    <asp:BoundField HeaderText="Dominio" DataField="Dominio" />
                    <asp:BoundField HeaderText="Cod. Homologación" DataField="CodigoHomologacion" />
                    <asp:BoundField HeaderText="Serie Cilindro" DataField="NumeroSerie" />
                    <asp:BoundField HeaderText="Cliente" DataField="Cliente" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkTodos" runat="server" onclick="Check(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle CssClass="GridRow"></EditRowStyle>
                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                <RowStyle CssClass="GridRow"></RowStyle>
            </asp:GridView>
            <br />
            <asp:Label ID="lblMensaje" runat="server" Visible="false" Text="No hay ph disponibles para ser informadas." CssClass="failureNotification"></asp:Label>
        </div>

        <b>Ultima Actualización:</b> <asp:Label ID="lblUltimaActualización" runat="server" Text=""></asp:Label>

        <div class="col-sm-12 pull-right">
            <div class="col-sm-6 no-padding"></div>
            <div class="col-sm-2 no-padding">
                <button type="button" class="btn btn-primary btn-block nn" id="lnkVerArchivos" onclick="window.open('VerArchivos.aspx','VerArchivosEnte');" title="Ver Archivos" alt="Ver Archivos"><i class="fa fa-check" aria-hidden="true"></i>&nbsp Ver Archivos</button>
            </div>
            <div class="col-sm-2 no-padding">
                <button type="button" class="btn btn-primary btn-block nn" id="btnAceptar" runat="server" onserverclick="btnAceptar_Click" title="Aceptar" alt="Aceptar"><i class="fa fa-check" aria-hidden="true"></i>&nbsp Aceptar</button>
            </div>
            <div class="col-sm-2 no-padding">
                <button type="button" class="btn btn-danger btn-block nn" id="btnCancelar" runat="server" onserverclick="btnCancelar_Click" title="Cancelar" alt="Cancelar"><i class="fa fa-close" aria-hidden="true"></i>&nbsp Cancelar</button>
            </div>
        </div>
    </div>

    <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl1" />
</asp:Content>
