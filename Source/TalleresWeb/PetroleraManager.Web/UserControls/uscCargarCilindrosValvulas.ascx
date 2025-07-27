<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscCargarCilindrosValvulas.ascx.cs" Inherits="PetroleraManager.Web.UserControls.uscCargarCilindrosValvulas" %>
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

        <fieldset>
            <legend>CILINDROS / VÁLVULAS</legend>
                <table style="width: 100%; border: 0; padding: 0;" class="Grid">
                <tr>
                    <th colspan="9" style="text-align: center" class="GridHeader">CILINDRO</th>
                    <th colspan="3" style="text-align: center" class="GridHeader">VALVULA 1</th>
                    <th colspan="3" style="text-align: center" class="GridHeader">VALVULA 2</th>
                    <th class="GridHeader"></th>
                </tr>
                <tr>
                    <th class="GridHeader">Cod. Homo</th>
                    <th class="GridHeader">N° Serie</th>
                    <th class="GridHeader">Fab. Mes</th>
                    <th class="GridHeader">Fab. Año</th>
                    <th class="GridHeader">Rev. Mes</th>
                    <th class="GridHeader">Rev. Año</th>
                    <th class="GridHeader">CRPC</th>
                    <th class="GridHeader">MSDB</th>
                    <th class="GridHeader">Certif. PH</th>

                    <th class="GridHeader">Cod. Homo</th>
                    <th class="GridHeader">N° Serie</th>
                    <th class="GridHeader">MSDB</th>

                    <th class="GridHeader">Cod. Homo</th>
                    <th class="GridHeader">N° Serie</th>
                    <th class="GridHeader">MSDB</th>
                    <th class="GridHeader"></th>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtCodigoCil" runat="server" MaxLength="4" Width="40px" /></td>
                    <td><asp:TextBox ID="txtSerieCil" runat="server" MaxLength="20" Width="120px" /></td>
                    <td><asp:TextBox ID="txtCilFabMes" runat="server" MaxLength="2" Width="30px" onKeyPress="return soloNumeros(event)" /></td>
                    <td><asp:TextBox ID="txtCilFabAnio" runat="server" MaxLength="2" Width="30px" onKeyPress="return soloNumeros(event)" /></td>
                    <td><asp:TextBox ID="txtCilRevMes" runat="server" MaxLength="2" Width="30px" onKeyPress="return soloNumeros(event)" /></td>
                    <td><asp:TextBox ID="txtCilRevAnio" runat="server" MaxLength="2" Width="30px" onKeyPress="return soloNumeros(event)" /></td>
                    <td><Controls:CboCRPC ID="cboCilCRPC" runat="server" AutomaticLoad="true" WidthCbo="60px" /></td>
                    <td><Controls:CboMSDB ID="cboMSDBCil" runat="server" AutomaticLoad="true" Width="90%" WidthCbo="32px" /></td>
                    <td><asp:TextBox ID="txtNroCertifPH" runat="server" MaxLength="10" Width="80px" /></td>

                    <td><asp:TextBox ID="txtCodigoVal1" runat="server" MaxLength="4" Width="40px" /></td>
                    <td><asp:TextBox ID="txtSerieVal1" runat="server" MaxLength="20" Width="120px" /></td>
                    <td><Controls:CboMSDB ID="cboMSDBVal1" runat="server" AutomaticLoad="true" WidthCbo="32px" /></td>

                    <td><asp:TextBox ID="txtCodigoVal2" runat="server" MaxLength="4" Width="40px" /></td>
                    <td><asp:TextBox ID="txtSerieVal2" runat="server" MaxLength="20" Width="120px" /></td>
                    <td><Controls:CboMSDB ID="cboMSDBVal2" runat="server" AutomaticLoad="true" WidthCbo="32px" /></td>

                    <td style="width:50px;">
                        <Controls:ImgBtnAgregar ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" CausesValidation="false" />
                        <Controls:ImgBtnModificar ID="btnModificar" runat="server" OnClick="btnModificar_Click" CausesValidation="false" Visible="false" />
                        <Controls:ImgBtnCancelar ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" CausesValidation="false" />
                    </td>
                </tr>
            </table>
                <hr />
                <div style="overflow: auto; min-height: 200px; height: 200px">
                <asp:GridView ID="gdvCilindrosValvulas" runat="server" AutoGenerateColumns="False" Width="100%" ShowHeaderWhenEmpty="true" CssClass="Grid"
                    HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow" AlternatingRowStyle-CssClass="GridAlternateRow"
                    DataKeyNames="ID, IdObleaCilindro, IDCilindro, IDCilindroUnidad, CRPCCilindroID, MSDBCilindroID, IdObleaValvula1, IDValvula1, IDValvula1Unidad, MSDBValvula1ID, IdObleaValvula2, IDValvula2, IDValvula2Unidad, MSDBValvula2ID"
                    OnRowCommand="gdvCilindrosValvulas_RowCommand" OnRowDataBound="gdvCilindrosValvulas_RowDataBound">
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

                        <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <Controls:ImgBtnModificar ID="btnModificarFila" runat="server" CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" />
                                <Controls:ImgBtnEliminar ID="btnEliminarFila" runat="server" CausesValidation="false" CommandArgument="<%# Container.DataItemIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>

        <uc1:MessageBoxCtrl runat="server" ID="MessageBoxCtrl" />

        <script type="text/javascript">
             function soloNumeros(e) {
                var key = window.Event ? e.which : e.keyCode
                return (key >= 48 && key <= 57)
             }
        </script>
        </ContentTemplate>
</asp:UpdatePanel>