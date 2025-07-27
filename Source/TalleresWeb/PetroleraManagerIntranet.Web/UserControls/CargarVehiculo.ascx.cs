using System;
using System.Linq;
using TalleresWeb.Entities;
using TalleresWeb.Logic;

namespace PetroleraManagerIntranet.Web.UserControls
{
    public delegate void VehiculoChangedEventHandler();

    public partial class CargarVehiculo : System.Web.UI.UserControl
    {
        #region Members

        private VehiculosLogic logic = new VehiculosLogic();
        public event VehiculoChangedEventHandler VehiculoChanged;

        #endregion

        #region Properties

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

        public VehiculosExtendedView VehiculoCargado
        {
            get
            {
                if (String.IsNullOrEmpty(hddID.Value)) return default(VehiculosExtendedView);

                VehiculosExtendedView vehiculo = new VehiculosExtendedView();
                vehiculo.ID = new Guid(hddID.Value);
                vehiculo.Descripcion = txtDominioVehiculo.Text.ToUpper().Trim();
                vehiculo.MarcaVehiculo = txtMarcaAuto.Text.ToUpper().Trim();
                vehiculo.ModeloVehiculo = txtModeloAuto.Text.ToUpper().Trim();                
                vehiculo.EsInyeccionVehiculo = chkBoxEsInyeccion.Checked;
                vehiculo.AnioVehiculo = !String.IsNullOrWhiteSpace(txtAnioAuto.Text) ? int.Parse(txtAnioAuto.Text) : 0;
                

                Guid idUso = CrossCutting.DatosDiscretos.TIPOVEHICULO.Particular;
                if (chkTipoVAutoelevadores.Checked) idUso = CrossCutting.DatosDiscretos.TIPOVEHICULO.Autoelevadores;
                if (chkTipoVPickUp.Checked) idUso = CrossCutting.DatosDiscretos.TIPOVEHICULO.PickUp;
                if (chkTipoVTaxi.Checked) idUso = CrossCutting.DatosDiscretos.TIPOVEHICULO.Taxi;
                if (chkTipoVOtros.Checked) idUso = CrossCutting.DatosDiscretos.TIPOVEHICULO.Otros;
                if (chkTipoVMoto.Checked) idUso = CrossCutting.DatosDiscretos.TIPOVEHICULO.Moto;
                if (chkTipoVOficial.Checked) idUso = CrossCutting.DatosDiscretos.TIPOVEHICULO.Oficial;
                vehiculo.IdUso = idUso;

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

                Guid idUso = vehiculo.IdUso;
                chkTipoVPart.Checked = (idUso == CrossCutting.DatosDiscretos.TIPOVEHICULO.Particular);
                chkTipoVAutoelevadores.Checked = (idUso == CrossCutting.DatosDiscretos.TIPOVEHICULO.Autoelevadores);
                chkTipoVPickUp.Checked = (idUso == CrossCutting.DatosDiscretos.TIPOVEHICULO.PickUp);
                chkTipoVTaxi.Checked = (idUso == CrossCutting.DatosDiscretos.TIPOVEHICULO.Taxi);
                chkTipoVOtros.Checked = (idUso == CrossCutting.DatosDiscretos.TIPOVEHICULO.Otros);
                chkTipoVMoto.Checked = (idUso == CrossCutting.DatosDiscretos.TIPOVEHICULO.Moto);
                chkTipoVOficial.Checked = (idUso == CrossCutting.DatosDiscretos.TIPOVEHICULO.Oficial);

                btnBuscarVehiculo.Visible = false;
                btnBuscarOtroAuto.Visible = true;
                HabilitarVehiculo(true);
            }
        }

        public Boolean EsVehiculoValido
        {
            get
            {
                return
                        this.VehiculoCargado != null &&
                        !String.IsNullOrWhiteSpace(txtDominioVehiculo.Text) &&
                        !String.IsNullOrWhiteSpace(txtMarcaAuto.Text) &&
                        !String.IsNullOrWhiteSpace(txtModeloAuto.Text) &&
                        !String.IsNullOrWhiteSpace(txtAnioAuto.Text);
            }
        }

        #endregion

        #region Methods

        public void SetFocus()
        {
            this.txtDominioVehiculo.Focus();
        }

        protected void btnBuscarOtroAuto_Click(object sender, EventArgs e)
        {
            hddID.Value = String.Empty;
            LimpiarVehiculo(true);
            HabilitarVehiculo(true);            
            txtDominioVehiculo.Focus();
            btnBuscarOtroAuto.Visible = false;
            btnBuscarVehiculo.Visible = true;            
            txtDominioVehiculo.ReadOnly = false;
        }

        protected void btnBuscarVehiculo_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtDominioVehiculo.Text))
            {
                txtDominioVehiculo.Text = txtDominioVehiculo.Text.ToUpper();

                var vehiculo = logic.ReadByDominio(txtDominioVehiculo.Text);

                if (vehiculo.Any())
                {
                    if (this.logic.TieneTramitesPendientes(txtDominioVehiculo.Text))
                        MessageBoxCtrl1.MessageBox(null, "- El vehículo ingresado posee trámites pendientes", MessageBoxCtrl.TipoWarning.Warning);

                    //si encuentro el auto muestro los datos y el boton siguiente para permitir modificar
                    hddID.Value = vehiculo.FirstOrDefault().ID.ToString();
                    txtMarcaAuto.Text = vehiculo.FirstOrDefault().MarcaVehiculo.ToUpper();
                    txtModeloAuto.Text = vehiculo.FirstOrDefault().ModeloVehiculo.ToUpper();
                    txtAnioAuto.Text = vehiculo.FirstOrDefault().AnioVehiculo.ToString();
                    chkBoxEsInyeccion.Checked = vehiculo.FirstOrDefault().EsInyeccionVehiculo;
                    txtAnioAuto.Text = vehiculo.FirstOrDefault().AnioVehiculo.ToString();
                    chkBoxEsInyeccion.Checked = vehiculo.FirstOrDefault().EsInyeccionVehiculo;

                    btnBuscarOtroAuto.Visible = true;                    

                    HabilitarVehiculo(true);

                    this.VehiculoChanged();
                }
                else
                {
                    //si no encuentro el auto limpio los campos y permito ingresar uno.. muestro btn siguiente para que acepte
                    hddID.Value = Guid.NewGuid().ToString();
                    //LimpiarVehiculo(false);
                    HabilitarVehiculo(true);
                    txtDominioVehiculo.ReadOnly = false;

                    btnBuscarOtroAuto.Visible = false;                    

                    txtMarcaAuto.Focus();                    
                }

                UpdatePanel1.Update();
            }       
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
        }

        private void HabilitarVehiculo(Boolean valor)
        {
            txtDominioVehiculo.ReadOnly = valor;
            txtMarcaAuto.ReadOnly = !valor;
            txtModeloAuto.ReadOnly = !valor;
            txtAnioAuto.ReadOnly = !valor;
            chkBoxEsInyeccion.Enabled = valor;
            chkTipoVTaxi.Enabled = valor;
            chkTipoVPickUp.Enabled = valor;
            chkTipoVPart.Enabled = valor;
            chkTipoVOficial.Enabled = valor;
            chkTipoVOtros.Enabled = valor;
            chkTipoVAutoelevadores.Enabled = valor;
            chkTipoVMoto.Enabled = valor;
        }

        private void LimpiarVehiculo(Boolean limpiaDominio)
        {
            //lblTituloVehiculo.Text = String.Empty;

            if (limpiaDominio) txtDominioVehiculo.Text = String.Empty;
            txtMarcaAuto.Text = String.Empty;
            txtModeloAuto.Text = String.Empty;
            txtAnioAuto.Text = String.Empty;
            chkBoxEsInyeccion.Checked = false;
            chkTipoVTaxi.Checked = false;
            chkTipoVPickUp.Checked = false;
            chkTipoVPart.Checked = true;
            chkTipoVOficial.Checked = false;
            chkTipoVOtros.Checked = false;
            chkTipoVMoto.Checked = false;
            chkTipoVAutoelevadores.Checked = false;
        }

        #endregion
    }
}