<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscCargarVehiculo.ascx.cs" Inherits="TalleresWeb.Web.UserControls.uscCargarVehiculo" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <fieldset class="aField">
            <legend>VEHICULO</legend>
            <input id="hddID" type="hidden" runat="server" />
            <PLs:PLPanel ID="panel" runat="server" DefaultButton="btnBuscarVehiculo">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td width="50%">
                        <PLs:PLTextBoxMasked ID="txtDominioVehiculo" runat="server" MaskType="None" Mask="LLL999" LabelText="Dominio:" Required="true" WidthTxt="90px"/>
                    </td>
                    <td width="50%">
                        <asp:Button ID="btnBuscarVehiculo" runat="server" Text="Buscar" OnClick="btnBuscarVehiculo_Click" CausesValidation="false"  />
                        <asp:LinkButton ID="btnBuscarOtroAuto" runat="server" OnClick="btnBuscarOtroAuto_Click"
                            CausesValidation="false" Visible="false">Otro</asp:LinkButton>
                    </td>
                </tr>
            </table>
            </PLs:PLPanel>
            <hr />
            <asp:Panel ID="pnlVehiculo" runat="server" Visible="true" Enabled="false" Height="110px">
                <table border="0" cellpadding="0" cellspacing="1" width="100%">
                    <tr>
                        <td width="50%">
                            <PLs:PLTextBox ID="txtMarcaAuto" runat="server" MaxLength="50" LabelText="MARCA:"></PLs:PLTextBox>
                        </td>
                        <td width="50%">
                            <PLs:PLTextBox ID="txtModeloAuto" runat="server" MaxLength="50" LabelText="MODELO:"></PLs:PLTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <PLs:PLTextBoxMasked ID="txtAnioAuto" runat="server" MaskType="Number" Mask="9999" LabelText="AÑO:" />
                        </td>
                        <td>
                            <PEARGNC:CboUso ID="cboUso" runat="server" LabelText="Uso:" AutomaticLoad="true" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:CheckBox ID="chkBoxEsInyeccion" runat="server" Text=" ES INYECCION?" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
    </ContentTemplate>
</asp:UpdatePanel>
