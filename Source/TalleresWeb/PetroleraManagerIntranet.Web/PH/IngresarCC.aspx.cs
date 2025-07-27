using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.PH
{
    public partial class IngresarCC : PageBase
    {
        #region Properties
        private PHLogic phLogic;
        private PHLogic PHLogic
        {
            get
            {
                if (this.phLogic == null) phLogic = new PHLogic();
                return this.phLogic;
            }
        }

        private PHCilindrosLogic phCilindrosLogic;
        private PHCilindrosLogic PHCilindrosLogic
        {
            get
            {
                if (this.phCilindrosLogic == null) phCilindrosLogic = new PHCilindrosLogic();
                return this.phCilindrosLogic;
            }
        }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                cboPEC.SelectedValue = CrossCutting.DatosDiscretos.PEC.PEAR.ToString();
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

                    Guid pecID = Guid.Parse(cboPEC.SelectedValue);
                    ViewEntity o = this.PHLogic.SaveFromExtranet(cartaCompromiso, this.UsuarioID, pecID);
                    this.ImprimirPH(o.ID);
                }
                else
                {
                    this.MessageBox.MessageBox(null, mensajes, Web.UserControls.MessageBoxCtrl.TipoWarning.Warning);
                }
            }
            catch (Exception ex)
            {
                this.MessageBox.MessageBox(null, ex.Message, Web.UserControls.MessageBoxCtrl.TipoWarning.Error);
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
                ClienteTipoDocumento = cboTipoDocumento.SelectedValue,
                ClientesNumeroDocumento = txtNumeroDocumento.Value.Trim(),
                ClienteRazonSocial = txtNombre.Value.Trim().ToUpper(),
                ClienteDomicilio = txtDomicilio.Value.Trim().ToUpper(),
                ClienteLocalidad = cboLocalidad.SelectedValue,
                ClienteCodigoPostal = txtCodigoPostal.Value.Trim().ToUpper(),
                ClienteTelefono = txtTelefono.Value.Trim().ToUpper(),
                NroObleaHabilitante = txtNroOblea.Value.Trim(),
                Cilindros = cilindros,
                
                TallerID = new Guid(cboTalleres.SelectedValue)
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
            if (cboTalleres.SelectedIndex == -1) mensajes.Add("- Debe seleccionar un taller");
            return mensajes;
        }

        private void ValidarCilindros(List<PHCilindrosView> cilindrosPHCargados, List<string> mensajes)
        {
            foreach (var item in cilindrosPHCargados)
            {
                var hayPHEnCurso = PHCilindrosLogic.HayPHEnCurso(item.CodigoCil, item.SerieCil);

                if (hayPHEnCurso)
                    mensajes.Add($"El cilindro {item.SerieCil} tiene una PH en curso.");
            }
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