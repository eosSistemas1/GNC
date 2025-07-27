<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscInspecciones.ascx.cs" Inherits="PetroleraManagerIntranet.Web.UserControls.ProcesosPHPasos.uscInspecciones" %>

<div class="col-sm-12" style="height:200px; width:100%; overflow:auto;">
    <asp:GridView ID="grdInspecciones" runat="server" class="table table-bordered table-hover" BorderWidth="0"
        AutoGenerateColumns="false" DataKeyNames="ID" Width="100%" ShowHeader="false" ShowHeaderWhenEmpty="false"
        EmptyDataText="<center>No hay inspecciones configuradas para este estado.</center>">
        <Columns>
            <asp:BoundField DataField="Descripcion" ShowHeader="false" ItemStyle-BorderStyle="None" />
            <asp:TemplateField ItemStyle-CssClass="text-center" ShowHeader="false" ItemStyle-BorderStyle="None">
                <ItemTemplate>
                    <asp:CheckBox ID="chkInspeccion" runat="server" Checked='<%# Eval("ValorInspeccion") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-CssClass="text-center" ShowHeader="false" ItemStyle-BorderStyle="None">
                <ItemTemplate>
                    <asp:TextBox ID="txtObservacion" runat="server" Text='<%# Eval("Observacion") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>