using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using System.Drawing;

namespace TalleresWeb.Web.UserControls
{
    public partial class uscCargarVehiculo : System.Web.UI.UserControl
    {
        //Vehiculos vehiculos = new Vehiculos();
        VehiculosLogic logic = new VehiculosLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            txtDominioVehiculo.CssClass = "input";
        }

        protected void btnBuscarVehiculo_Click(object sender, EventArgs e)
        {
            if (txtDominioVehiculo.Text != String.Empty)
            {
                var obj = logic.ReadByDominio(txtDominioVehiculo.Text);

                if (obj.Count > 0)
                {
                    //si encuentro el auto muestro los datos y el boton siguiente para permitir modificar
                    hddID.Value = obj.FirstOrDefault().ID.ToString();
                    txtMarcaAuto.Text = obj.FirstOrDefault().MarcaVehiculo.ToUpper();
                    txtModeloAuto.Text = obj.FirstOrDefault().ModeloVehiculo.ToUpper();
                    txtAnioAuto.Text = obj.FirstOrDefault().AnioVehiculo.ToString();
                    chkBoxEsInyeccion.Checked = obj.FirstOrDefault().EsInyeccionVehiculo;
                    cboUso.SelectedValue = obj.FirstOrDefault().IdUso;
                    btnBuscarOtroAuto.Visible = true;

                    HabilitarAuto(true);
                    UpdatePanel1.Update();
                }
                else
                {
                    //si no encuentro el auto limpio los campos y permito ingresar uno.. muestro btn siguiente para que acepte
                    hddID.Value = Guid.NewGuid().ToString();
                    LimpiarAuto(false);
                    HabilitarAuto(true);

                    btnBuscarOtroAuto.Visible = false;
                    cboUso.SelectedValue = CrossCutting.DatosDiscretos.TipoVehiculo.Particular;
                    txtMarcaAuto.Focus();
                }

                pnlVehiculo.Enabled = true;
            }
            else
            {
                txtDominioVehiculo.Focus();
            }
        }
        private void LimpiarAuto(Boolean limpiaDominio)
        {
            //lblTituloVehiculo.Text = String.Empty;

            if (limpiaDominio) txtDominioVehiculo.Text = String.Empty;
            txtMarcaAuto.Text = String.Empty;
            txtModeloAuto.Text = String.Empty;
            txtAnioAuto.Text = String.Empty;
            chkBoxEsInyeccion.Checked = false;
            //chkTipoVTaxi.Checked = false;
            //chkTipoVPickUp.Checked = false;
            //chkTipoVPart.Checked = true;
            //chkTipoVBus.Checked = false;
            //chkTipoVOficial.Checked = false;
            //chkTipoVOtros.Checked = false;
            //chkTipoVMoto.Checked = false;
            //chkTipoVAutoelevadores.Checked = false;
        }
        private void HabilitarAuto(Boolean valor)
        {
            txtDominioVehiculo.ReadOnlyTxt = !valor;
            txtMarcaAuto.ReadOnlyTxt = !valor;
            txtModeloAuto.ReadOnlyTxt = !valor;
            txtAnioAuto.ReadOnlyTxt = !valor;
            chkBoxEsInyeccion.Enabled = valor;
            //chkTipoVTaxi.Enabled = valor;
            //chkTipoVPickUp.Enabled = valor;
            //chkTipoVPart.Enabled = valor;
            //chkTipoVBus.Enabled = valor;
            //chkTipoVOficial.Enabled = valor;
            //chkTipoVOtros.Enabled = valor;
            //chkTipoVAutoelevadores.Enabled = valor;
            //chkTipoVMoto.Enabled = valor;
        }
        protected void btnBuscarOtroAuto_Click(object sender, EventArgs e)
        {
            hddID.Value = String.Empty;
            LimpiarAuto(true);
            HabilitarAuto(true);
            //Genericos.Utility.SetFocus(txtDominioVehiculo);
            txtDominioVehiculo.Focus();
            btnBuscarOtroAuto.Visible = false;
            btnBuscarVehiculo.Visible = true;
            pnlVehiculo.Enabled = false;
            txtDominioVehiculo.ReadOnlyTxt = false;
        }


        public VehiculosExtendedView VehiculoCargado
        {
            get
            {
                VehiculosExtendedView vehiculo = new VehiculosExtendedView();
                if (hddID.Value != "")
                {
                    vehiculo.ID = new Guid(hddID.Value);
                    vehiculo.Descripcion = txtDominioVehiculo.Text.ToUpper().Trim();
                    vehiculo.MarcaVehiculo = txtMarcaAuto.Text.ToUpper().Trim();
                    vehiculo.ModeloVehiculo = txtModeloAuto.Text.ToUpper().Trim();
                    vehiculo.AnioVehiculo = txtAnioAuto.Text != String.Empty ? int.Parse(txtAnioAuto.Text) : 0;
                    vehiculo.EsInyeccionVehiculo = chkBoxEsInyeccion.Checked;

                    Guid idUso = cboUso.SelectedValue;
                    //if (chkTipoVAutoelevadores.Checked) idUso = CrossCutting.DatosDiscretos.TipoVehiculo.Autoelevadores;
                    //if (chkTipoVPickUp.Checked) idUso = CrossCutting.DatosDiscretos.TipoVehiculo.PickUp;
                    //if (chkTipoVTaxi.Checked) idUso = CrossCutting.DatosDiscretos.TipoVehiculo.Taxi;
                    //if (chkTipoVOtros.Checked) idUso = CrossCutting.DatosDiscretos.TipoVehiculo.Otros;
                    //if (chkTipoVMoto.Checked) idUso = CrossCutting.DatosDiscretos.TipoVehiculo.Moto;
                    //if (chkTipoVBus.Checked) idUso = CrossCutting.DatosDiscretos.TipoVehiculo.Bus;
                    //if (chkTipoVOficial.Checked) idUso = CrossCutting.DatosDiscretos.TipoVehiculo.Oficial;
                    vehiculo.IdUso = idUso;
                }
                else
                {
                    vehiculo.ID = Guid.Empty;
                }

                return vehiculo;
            }
            set
            {
                var vehiculo = (VehiculosExtendedView)value;
                hddID.Value = vehiculo.ID.ToString();
                txtDominioVehiculo.Text = vehiculo.Descripcion;
                txtMarcaAuto.Text = vehiculo.MarcaVehiculo;
                txtModeloAuto.Text = vehiculo.ModeloVehiculo;
                txtAnioAuto.Text = vehiculo.AnioVehiculo.ToString();
                chkBoxEsInyeccion.Checked = vehiculo.EsInyeccionVehiculo;
                cboUso.SelectedValue = vehiculo.IdUso;
                //chkTipoVPart.Checked = (idUso == CrossCutting.DatosDiscretos.TipoVehiculo.Particular);
                //chkTipoVAutoelevadores.Checked = (idUso == CrossCutting.DatosDiscretos.TipoVehiculo.Autoelevadores);
                //chkTipoVPickUp.Checked = (idUso == CrossCutting.DatosDiscretos.TipoVehiculo.PickUp);
                //chkTipoVTaxi.Checked = (idUso == CrossCutting.DatosDiscretos.TipoVehiculo.Taxi);
                //chkTipoVOtros.Checked = (idUso == CrossCutting.DatosDiscretos.TipoVehiculo.Otros);
                //chkTipoVMoto.Checked = (idUso == CrossCutting.DatosDiscretos.TipoVehiculo.Moto);
                //chkTipoVBus.Checked = (idUso == CrossCutting.DatosDiscretos.TipoVehiculo.Bus);
                //chkTipoVOficial.Checked = (idUso == CrossCutting.DatosDiscretos.TipoVehiculo.Oficial);


                btnBuscarVehiculo.Visible = false;
                btnBuscarOtroAuto.Visible = true;
            }
        }

        public String validar 
        {
            get 
            {
                String valor = String.Empty;

                if (txtDominioVehiculo.Text == String.Empty) valor += "- Vehículo: debe ingresar dominio </br>";
                if (txtMarcaAuto.Text == String.Empty) valor += "- Vehículo: debe ingresar marca </br>";
                if (txtModeloAuto.Text == String.Empty) valor += "- Vehículo: debe ingresar modelo </br>";
                if (txtAnioAuto.Text == String.Empty) valor += "- Vehículo: debe ingresar año </br>";
                return valor;
            }
        }
    }
}