<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CargarCilindrosValvulas.ascx.cs" Inherits="PetroleraManagerIntranet.Web.UserControls.CargarCilindrosValvulas" %>
<%@ Register Src="~/UserControls/MessageBoxCtrl.ascx" TagPrefix="uc1" TagName="MessageBoxCtrl" %>

<asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

        <asp:HiddenField ID="hdnID" runat="server" />

        <asp:HiddenField ID="hdnIdObleaCilindro" runat="server" />
        <asp:HiddenField ID="hdnIDCilindro" runat="server" />
        <asp:HiddenField ID="hdnIDCilindroUnidad" runat="server" />

        <asp:HiddenField ID="hdnIdObleaValvula1" runat="server" />
        <asp:HiddenField ID="hdnIDValvula1" runat="server" />
        <asp:HiddenField ID="hdnIDValvula1Unidad" runat="server" />

        <asp:HiddenField ID="hdnIdObleaValvula2" runat="server" />
        <asp:HiddenField ID="hdnIDValvula2" runat="server" />
        <asp:HiddenField ID="hdnIDValvula2Unidad" runat="server" />

        <div class="row">
            <div class="col-sm-12">
                <h3><strong>CILINDROS / VÁLVULAS</strong></h3>
            </div>
            <hr>
        </div>

        <div class="col-sm-12">
            <div class="col-sm-12 no-padding">
                <asp:GridView ID="gdvCilindrosValvulas" runat="server" AutoGenerateColumns="False" Width="100%" ShowHeaderWhenEmpty="true"
                    class="table table-bordered table-hover" OnRowCommand="gdvCilindrosValvulas_RowCommand" OnRowDataBound="gdvCilindrosValvulas_RowDataBound"
                    DataKeyNames="ID, IdObleaCilindro, IDCilindro, IDCilindroUnidad, CRPCCilindroID, MSDBCilindroID, IdObleaValvula1, IDValvula1, IDValvula1Unidad, MSDBValvula1ID, IdObleaValvula2, IDValvula2, IDValvula2Unidad, MSDBValvula2ID">
                    <Columns>
                        <asp:BoundField DataField="CodigoCilindro" HeaderText="Cod. Homo" HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="NroSerieCilindro" HeaderText="N° Serie" HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="CilindroFabMes" HeaderText="Fab. Mes" HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="CilindroFabAnio" HeaderText="Fab. Año" HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="CilindroRevMes" HeaderText="Rev. Mes" HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="CilindroRevAnio" HeaderText="Rev. Año" HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="CRPCCilindro" HeaderText="CRPC" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="MSDBCilindro" HeaderText="MSDB" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="NroCertificadoPH" HeaderText="N° Cert. PH" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />

                        <asp:BoundField DataField="CodigoValvula1" HeaderText="Valvula_1 Cod. Homo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="NroSerieValvula1" HeaderText="Valvula_1  N° Serie" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="MSDBValvula1" HeaderText="Valvula_1 MSDB" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />

                        <asp:BoundField DataField="CodigoValvula2" HeaderText="Valvula_2 Cod. Homo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="NroSerieValvula2" HeaderText="Valvula_2 N° Serie" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="MSDBValvula2" HeaderText="Valvula_2 MSDB" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />

                        <asp:CheckBoxField ItemStyle-CssClass="spanClass" DataField="realizaPH" HeaderText="PH?" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />

                        <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <Controls:BtnModificar ID="btnModificarFila" runat="server" CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" />
                                <Controls:BtnEliminar ID="btnEliminarFila" runat="server" CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                               
                <table class="table table-bordered table-hover table-condensed">
                    <tr>
                        <th colspan="9" style="text-align: center">CILINDRO</th>
                        <th colspan="3" style="text-align: center">VALVULA 1</th>
                        <th colspan="3" style="text-align: center">VALVULA 2</th>
                        <th></th>
                        <th></th>
                    </tr>
                    <tr>
                        <th>Cod. Homo</th>
                        <th>N° Serie</th>
                        <th>Fab. Mes</th>
                        <th>Fab. Año</th>
                        <th>Rev. Mes</th>
                        <th>Rev. Año</th>
                        <th>CRPC</th>
                        <th>MSDB</th>
                        <th>Certif. PH</th>

                        <th>Cod. Homo</th>
                        <th>N° Serie</th>
                        <th>MSDB</th>

                        <th>Cod. Homo</th>
                        <th>N° Serie</th>
                        <th>MSDB</th>

                        <th class="text-center">PH?</th>
                        <th style="min-width: 75px;">&nbsp;</th>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCodigoCil" runat="server" MaxLength="4" style="max-width: 50px;" class="form-control-small nn" /></td>
                        <td>
                            <asp:TextBox ID="txtSerieCil" runat="server" MaxLength="20" style="max-width: 120px;"  class="form-control-small nn" /></td>
                        <td>
                            <asp:TextBox ID="txtCilFabMes" runat="server" MaxLength="2" style="max-width: 30px;" onKeyPress="return soloNumeros(event)" class="form-control-small nn" /></td>
                        <td>
                            <asp:TextBox ID="txtCilFabAnio" runat="server" MaxLength="2" style="max-width: 30px;" onKeyPress="return soloNumeros(event)" class="form-control-small nn" /></td>
                        <td>
                            <asp:TextBox ID="txtCilRevMes" runat="server" MaxLength="2" style="max-width: 30px;" onKeyPress="return soloNumeros(event)" class="form-control-small nn" /></td>
                        <td>
                            <asp:TextBox ID="txtCilRevAnio" runat="server" MaxLength="2" style="max-width: 30px;" onKeyPress="return soloNumeros(event)" class="form-control-small nn" /></td>
                        <td>
                            <Controls:CboCRPC ID="cboCilCRPC" runat="server" style="min-width: 60px;" CssClass="form-control-small nn"/></td>
                        <td>
                            <Controls:CboMSDB ID="cboMSDBCil" runat="server" style="max-width: 45px;" CssClass="form-control-small nn" /></td>
                        <td>
                            <asp:TextBox ID="txtNroCertifPH" runat="server" MaxLength="10" Width="80px" class="form-control-small nn" /></td>

                        <td>
                            <asp:TextBox ID="txtCodigoVal1" runat="server" MaxLength="4" style="max-width: 50px;" class="form-control-small nn" /></td>
                        <td>
                            <asp:TextBox ID="txtSerieVal1" runat="server" MaxLength="20" style="max-width: 120px;" class="form-control-small nn" /></td>
                        <td>
                            <Controls:CboMSDB ID="cboMSDBVal1" runat="server" style="max-width: 45px;" CssClass="form-control-small nn" /></td>

                        <td>
                            <asp:TextBox ID="txtCodigoVal2" runat="server" MaxLength="4" style="max-width: 50px;" class="form-control-small nn" /></td>
                        <td>
                            <asp:TextBox ID="txtSerieVal2" runat="server" MaxLength="20" style="max-width: 120px;" class="form-control-small nn" /></td>
                        <td>
                            <Controls:CboMSDB ID="cboMSDBVal2" runat="server" style="max-width: 45px;" CssClass="form-control-small nn" /></td>

                        <td>
                            <asp:CheckBox ID="chkRealizaPH" runat="server" /></td>

                        <td>
                            <Controls:BtnAgregar ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" CausesValidation="false" />
                            <Controls:BtnModificar ID="btnModificar" runat="server" OnClick="btnModificar_Click" CausesValidation="false" Visible="false" />
                            <Controls:BtnEliminar ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
                                
            </div>
        </div>

        <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />

        <script type="text/javascript">
            function soloNumeros(e) {
                var key = window.Event ? e.which : e.keyCode
                return (key >= 48 && key <= 57)
            }
        </script>
    </ContentTemplate>
</asp:UpdatePanel>
