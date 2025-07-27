using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;
using TalleresWeb.Web.Logic;

namespace TalleresWeb.Web.UI.Tramites.PH
{
    public partial class Default : PageBase
    {
        #region Properties
        private PHLogic phLogic;
        public PHLogic PHLogic
        {
            get
            {
                if (phLogic == null) phLogic = new PHLogic();
                return phLogic;
            }
        }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cboPEC.LoadData();
                cboPEC.SelectedValue = CrossCutting.DatosDiscretos.PEC.PEAR;
                lblTallerDescripcion.Text = SiteMaster.Taller.Descripcion;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                List<String> mensajes = this.Validar();

                if (!mensajes.Any())
                {
                    PHViewWebApi cartaCompromiso = ObtenerDatosAGuardar();

                    ViewEntity o = this.PHLogic.SaveFromExtranet(cartaCompromiso);                    
                    this.ImprimirPH(o.ID);
                }
                else
                {
                    this.MessageBox.MessageBox(null, mensajes, UserControls.MessageBoxCtrl.TipoWarning.Warning);
                }
            }
            catch (Exception ex)
            {
                this.MessageBox.MessageBox(null, ex.Message, UserControls.MessageBoxCtrl.TipoWarning.Error);
            }
        }

        private PHViewWebApi ObtenerDatosAGuardar()
        {
            List<PHCilindrosViewWebApi> cilindros = new List<PHCilindrosViewWebApi>();
            foreach (var item in this.CilindrosPH.CilindrosPHCargados)
            {
                PHCilindrosViewWebApi cilindro = new PHCilindrosViewWebApi();
                cilindro.Capacidad = item.CapacidadCil;
                cilindro.MarcaCilindro = item.MarcaCil;
                if (!string.IsNullOrWhiteSpace(item.FechaFabricacionCil))
                {
                    cilindro.MesFabCilindro = DateTime.Parse(item.FechaFabricacionCil).Month.ToString("00");
                    cilindro.AnioFabCilindro = DateTime.Parse(item.FechaFabricacionCil).Year.ToString().Substring(2, 2);
                }
                cilindro.NumeroSerieCilindro = item.SerieCil.ToUpper();
                cilindro.NumeroSerieValvula = item.SerieVal.ToUpper();
                cilindro.CodigoHomologacionCilindro = item.CodigoCil.ToUpper();
                cilindro.CodigoHomologacionValvula = item.CodigoVal.ToUpper();
                cilindro.Observacion = item.Observaciones;
                cilindros.Add(cilindro);
            }

            PHViewWebApi cartaCompromiso = new PHViewWebApi()
            {
                ID = Guid.NewGuid(),
                VehiculoDominio = txtDominio.Value.Trim().ToUpper(),
                VehiculoMarca = txtMarca.Value.Trim().ToUpper(),
                VehiculoModelo = txtModelo.Value.Trim().ToUpper(),
                ClienteTipoDocumento = cboTipoDocumento.SelectedValueString,
                ClientesNumeroDocumento = txtNumeroDocumento.Value.Trim(),
                ClienteRazonSocial = txtNombre.Value.Trim().ToUpper(),
                ClienteDomicilio = txtDomicilio.Value.Trim().ToUpper(),
                ClienteLocalidad = cboLocalidad.SelectedValueString,
                ClienteCodigoPostal = txtCodigoPostal.Value.Trim().ToUpper(),
                ClienteTelefono = txtTelefono.Value.Trim().ToUpper(),
                NroObleaHabilitante = txtNroOblea.Value.Trim(),
                Cilindros = cilindros,

                TallerID = SiteMaster.Taller.ID,
                UsuarioID = SiteMaster.Usuario.ID,
                PECID = cboPEC.SelectedValue
            };
            return cartaCompromiso;
        }

        private List<String> Validar()
        {
            List<String> mensajes = new List<String>();
            if (String.IsNullOrWhiteSpace(txtDominio.Value)) mensajes.Add("- Debe ingresar un vehículo");
            if (String.IsNullOrWhiteSpace(txtNumeroDocumento.Value)
                   || cboTipoDocumento.SelectedIndex == -1) mensajes.Add("- Debe ingresar un cliente");
            if (String.IsNullOrWhiteSpace(txtNroOblea.Value)) mensajes.Add("- Debe ingresar número oblea");
            if (!CilindrosPH.CilindrosPHCargados.Any())
            { mensajes.Add("- Debe ingresar al menos un cilindro"); }
            else
            {
                ValidarCilindros(CilindrosPH.CilindrosPHCargados, mensajes);
            }

            return mensajes;
        }

        private void ValidarCilindros(List<PHCilindrosView> cilindrosPHCargados, List<string> mensajes)
        {
            //foreach (var item in cilindrosPHCargados)
            //{
            //    var hayPHEnCurso = PHCilindrosLogic.HayPHEnCurso(item.CodigoCil, item.SerieCil);

            //    if (hayPHEnCurso)
            //        mensajes.Add($"El cilindro {item.SerieCil} tiene una PH en curso.");
            //}
        }

        private void ImprimirPH(Guid phID)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupImprimir('" + phID + "');", true);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void Cancelar()
        {
            if (Request.QueryString.Count > 0)
            {
                Response.Redirect("ConsultarCC.aspx");
            }
            else
            {
                Response.Redirect(Request.RawUrl);
            }
        }
        #endregion

    }
}