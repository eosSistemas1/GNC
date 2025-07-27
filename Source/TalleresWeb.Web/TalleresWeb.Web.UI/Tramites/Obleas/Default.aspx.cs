using CrossCutting.DatosDiscretos;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Web.Logic;

namespace TalleresWeb.Web.UI.Tramites.Obleas
{
    public partial class Default : PageBase
    {
        #region Properties
        private TipoDocumentoLogic tipoDocumentoLogic;
        public TipoDocumentoLogic TipoDocumentoLogic
        {
            get
            {
                if (tipoDocumentoLogic == null) tipoDocumentoLogic = new TipoDocumentoLogic();
                return tipoDocumentoLogic;
            }
        }

        private TipoOperacionLogic tipoOperacionLogic;
        public TipoOperacionLogic TipoOperacionLogic
        {
            get
            {
                if (tipoOperacionLogic == null) tipoOperacionLogic = new TipoOperacionLogic();
                return tipoOperacionLogic;
            }
        }

        private ObleasLogic obleasLogic;
        public ObleasLogic ObleasLogic
        {
            get
            {
                if (obleasLogic == null) obleasLogic = new ObleasLogic();
                return obleasLogic;
            }
        }

        private ClientesLogic clientesLogic;
        public ClientesLogic ClientesLogic
        {
            get
            {
                if (clientesLogic == null) clientesLogic = new ClientesLogic();
                return clientesLogic;
            }
        }

        private VehiculosLogic vehiculosLogic;
        public VehiculosLogic VehiculosLogic
        {
            get
            {
                if (vehiculosLogic == null) vehiculosLogic = new VehiculosLogic();
                return vehiculosLogic;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cboLocalidad.SelectedValue = LOCALIDADES.Rosario;
                IDTipoOperacionConversion.Value = TIPOOPERACION.Conversion.ToString();

                this.CargarDatos();

                this.DeshabilitarControles();
            }
        }

        private void DeshabilitarControles()
        {
            //txtNombreApellido.ReadOnly = true;
            //txtDomicilio.ReadOnly = true;
            //cboLocalidad.Enabled = false;
            //txtTelefono.ReadOnly = true;

            //txtMarca.ReadOnly = true;
            //txtModelo.ReadOnly = true;
            //txtAnio.ReadOnly = true;            
            //chkEsInyeccion.Enabled = true;
        }

        private void CargarDatos()
        {
            this.CargarCboTipoOperacion();
        }

        private void CargarCboTipoOperacion()
        {
            cboTipoOperacion.DataValueField = "ID";
            cboTipoOperacion.DataTextField = "Descripcion";
            cboTipoOperacion.DataSource = this.TipoOperacionLogic.ReadListTiposOperaciones();
            cboTipoOperacion.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            List<String> mensaje = this.ValidarBuscar();
            if (!mensaje.Any())
            {

                ObleasViewWebApi oblea = this.CargarOblea();
                if (oblea != null)
                {
                    if (!oblea.ExisteObleaPendiente && !oblea.ExisteTramitePendienteParaElDominio)
                    {
                        this.BindearOblea(oblea);
                    }
                    else
                    {
                        String msjObleaExistente = $"La Oblea Solicitante Nro. {txtNroObleaAnterior.Text};  ya tiene un tramite en curso para el Dominio {oblea.VehiculoDominio} ;por cualquier inconveniente comuníquese con Petrolera Italo Argentina srl.-";
                        Master.MessageBox(null, msjObleaExistente, UserControls.MessageBoxCtrl.TipoWarning.Warning);
                        return;
                    }
                }


                if (Request.Browser.Browser == "Chrome")
                {
                    txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    txtFecha.Text = DateTime.Now.ToString("MM/dd/yyyy");
                }

                lblOperacion.Text = cboTipoOperacion.SelectedItem.Text;
                String nroObleaAnterior = cboTipoOperacion.SelectedValue != TIPOOPERACION.Conversion.ToString() ? txtNroObleaAnterior.Text : "0";
                lblObleaAnterior.Text = nroObleaAnterior;

                pnlBuscar.Visible = false;
                pnlIngresar.Visible = true;
            }
            else
            {
                Master.MessageBox(null, mensaje, UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        private List<String> ValidarBuscar()
        {
            List<String> mensajes = new List<String>();
            if (cboTipoOperacion.SelectedIndex == -1 ||
                cboTipoOperacion.SelectedValue == Guid.Empty.ToString())
            {
                mensajes.Add("Debe ingresar el tipo de operación");
            }
            else
            {
                Guid selectedValue = new Guid(cboTipoOperacion.SelectedValue);

                if (selectedValue != TIPOOPERACION.Conversion
                    && String.IsNullOrEmpty(txtNroObleaAnterior.Text))
                {
                    mensajes.Add("Debe ingresar el número de oble anterior");
                }
            }

            return mensajes;
        }

        private void BindearOblea(ObleasViewWebApi oblea)
        {
            if (oblea != null)
            {
                cboTipoDocumento.SelectedValue = oblea.ClienteTipoDocumentoID;
                txtNumeroDocumento.Text = oblea.ClienteNumeroDocumento;
                txtNombreApellido.Text = oblea.ClienteNombreApellido;
                txtDomicilio.Text = oblea.ClienteDomicilio;
                cboLocalidad.SelectedValue = oblea.ClienteLocalidadID;
                txtTelefono.Text = oblea.ClienteTelefono;
                txtCelular.Text = oblea.ClienteCelular;
                txtEmail.Text = oblea.ClienteEmail;

                txtDominio.Text = oblea.VehiculoDominio;
                txtMarca.Text = oblea.VehiculoMarca;
                txtModelo.Text = oblea.VehiculoModelo;
                txtAnio.Text = oblea.VehiculoAnio.HasValue ? oblea.VehiculoAnio.Value.ToString() : String.Empty;
                chkEsInyeccion.Checked = oblea.VehiculoEsInyeccion.HasValue ? oblea.VehiculoEsInyeccion.Value : false;

                this.Reguladores.TipoperacionID = new Guid(cboTipoOperacion.SelectedValue);
                this.Reguladores.ReguladoresCargados = oblea.Reguladores;
                this.Reguladores.TipoperacionID = new Guid(cboTipoOperacion.SelectedValue);

                this.CilindrosValvulas.TipoperacionID = new Guid(cboTipoOperacion.SelectedValue);
                this.CilindrosValvulas.CargarDetalle(oblea.Cilindros, oblea.Valvulas);

                txtFecha.Focus();
            }
        }

        private ObleasViewWebApi CargarOblea()
        {
            ObleasParametersWebApi criteria = this.GenerarCriteria();

            var oblea = this.ObleasLogic.ReadOblea(criteria);

            return oblea;
        }

        private ObleasParametersWebApi GenerarCriteria()
        {
            return new ObleasParametersWebApi()
            {
                TipoOperacionID = new Guid(cboTipoOperacion.SelectedValue),
                NumeroOblea = txtNroObleaAnterior.Text
            };
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            ObleasViewWebApi oblea = this.ObtenerDatosOblea();

            List<String> mensajes = this.ValidarOblea(oblea);

            if (!mensajes.Any())
            {
                oblea.ObleaNumeroAnterior = oblea.ObleaNumeroAnterior == "0" ? String.Empty : oblea.ObleaNumeroAnterior;
                ViewEntity o = this.ObleasLogic.Guardar(oblea);

                if (o.ID != Guid.Empty)
                {
                    String urlRetorno = "Default.aspx";
                    Guid? idPH = oblea.PH != null ? oblea.PH.ID : default(Guid?);
                    this.ImprimirOblea(o.ID, idPH, urlRetorno);

                }
                else
                {
                    Master.MessageBox(null, o.Descripcion, UserControls.MessageBoxCtrl.TipoWarning.Error);
                }
            }
            else
            {
                Master.MessageBox(null, mensajes, UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        /// <summary>
        /// Imprimo la oblea creada
        /// </summary>        
        private void ImprimirOblea(Guid idOblea, Guid? idPH, String urlRetorno)
        {
            String url = SiteMaster.UrlBase + $@"Tramites/Obleas/ObleasImprimir.aspx?id={idOblea}&fotos=false";
            if (idPH.HasValue && idPH.Value != Guid.Empty)
                url += $"&idPH={idPH.Value}";
            PrintBoxCtrl1.PrintBox("Imprimir", url, String.Empty, urlRetorno);
        }

        private List<String> ValidarOblea(ObleasViewWebApi oblea)
        {
            List<String> msjValida = new List<String>();

            if (oblea.OperacionID == Guid.Empty) msjValida.Add("- Seleccione tipo de operación");
            if (oblea.FechaHabilitacion == DateTime.MinValue) msjValida.Add("- Ingrese fecha de habilitación");
            if (!this.EsClienteValido(oblea)) msjValida.Add("- Ingrese todos los datos del cliente");

            if (!this.EsVehiculoValido(oblea))
            {
                msjValida.Add("- Ingrese todos los datos del vehículo");
            }
            else
            {
                if (String.IsNullOrEmpty(oblea.VehiculoDominio) || !ObleasLogic.ValidarDominio(oblea.VehiculoDominio,
                                                                                               oblea.VehiculoAnio.Value,
                                                                                               oblea.IdUso))
                {
                    msjValida.Add("El dominio no tiene el formato correcto o no fue ingresado");
                }

                if (this.ObleasLogic.ExisteTramitePendienteParaElDominio(txtDominio.Text))
                {
                    msjValida.Add("- El vehículo ingresado posee trámites pendientes <br>");
                }
            }

            if (!oblea.Reguladores.Any())
            {
                msjValida.Add("- Debe cargar un regulador");
            }

            if (oblea.Reguladores.Where(t => t.MSDBRegID == MSDB.Sigue).Count() > 1)
            {
                msjValida.Add("- No puede ingresar mas de un regulador en SIGUE");
            }

            if (!oblea.Cilindros.Any())
            {
                msjValida.Add("- Debe cargar un cilindro");
            }

            //if (!oblea.Valvulas.Any())
            //{
            //    msjValida.Add("- Debe cargar una válvula");
            //}

            if (oblea.OperacionID != null
                && oblea.OperacionID != Guid.Empty
                && oblea.OperacionID != TIPOOPERACION.Conversion
                && String.IsNullOrWhiteSpace(oblea.ObleaNumeroAnterior))
            {
                msjValida.Add("- Debe ingresar numero de oblea anterior");
            }

            return msjValida;
        }

        /// <summary>
        /// Valida que el cliente ingresado sea valido
        /// </summary>        
        private Boolean EsClienteValido(ObleasViewWebApi oblea)
        {
            return !String.IsNullOrWhiteSpace(oblea.ClienteNombreApellido) &&
                   !String.IsNullOrWhiteSpace(oblea.ClienteDomicilio) &&
                   !String.IsNullOrWhiteSpace(oblea.ClienteLocalidad) &&
                   !String.IsNullOrWhiteSpace(oblea.ClienteNumeroDocumento) &&
                   !String.IsNullOrWhiteSpace(oblea.ClienteTelefono) &&
                   oblea.ClienteLocalidadID != Guid.Empty &&
                   oblea.ClienteTipoDocumentoID != Guid.Empty;
        }

        /// <summary>
        /// Valida que el cliente ingresado sea valido
        /// </summary>        
        private Boolean EsVehiculoValido(ObleasViewWebApi oblea)
        {
            if (!oblea.VehiculoAnio.HasValue) return false;

            return (oblea.VehiculoAnio.HasValue && oblea.VehiculoAnio.Value > 1950) &&
                   !String.IsNullOrWhiteSpace(oblea.VehiculoDominio) &&
                   !String.IsNullOrWhiteSpace(oblea.VehiculoMarca) &&
                   !String.IsNullOrWhiteSpace(oblea.VehiculoModelo);
        }

        private ObleasViewWebApi ObtenerDatosOblea()
        {
            ObleasViewWebApi oblea = new ObleasViewWebApi();

            oblea.FechaHabilitacion = DateTime.Now;

            oblea.TallerID = SiteMaster.Taller.ID;
            oblea.OperacionID = new Guid(cboTipoOperacion.SelectedValue);
            oblea.UsuarioID = SiteMaster.Usuario.ID;
            oblea.ObleaNumeroAnterior = txtNroObleaAnterior.Text;

            oblea.ClienteTipoDocumentoID = cboTipoDocumento.SelectedValue;
            oblea.ClienteNombreApellido = txtNombreApellido.Text;
            oblea.ClienteTipoDocumento = cboTipoDocumento.SelectedText;
            oblea.ClienteNumeroDocumento = txtNumeroDocumento.Text;
            oblea.ClienteDomicilio = txtDomicilio.Text;
            oblea.ClienteLocalidadID = cboLocalidad.SelectedValue;
            oblea.ClienteLocalidad = cboLocalidad.SelectedText.Trim();
            oblea.ClienteTelefono = txtTelefono.Text;
            oblea.ClienteCelular = txtCelular.Text;
            oblea.ClienteEmail = txtEmail.Text;

            oblea.VehiculoDominio = txtDominio.Text.ToUpper().Trim();
            oblea.VehiculoMarca = txtMarca.Text.ToUpper().Trim();
            oblea.VehiculoModelo = txtModelo.Text.ToUpper().Trim();
            oblea.VehiculoAnio = !String.IsNullOrWhiteSpace(txtAnio.Text) ? int.Parse(txtAnio.Text) : default(int?);
            oblea.VehiculoEsInyeccion = chkEsInyeccion.Checked;

            oblea.Reguladores = this.Reguladores.ReguladoresCargados;
            oblea.Cilindros = this.CilindrosValvulas.CilindrosCargados;
            oblea.Valvulas = this.CilindrosValvulas.ValvulasCargadas;

            foreach (var item in oblea.Cilindros)
            {
                if (!oblea.Valvulas.Any()) continue;

                var valUni = oblea.Valvulas.FirstOrDefault(v => v.IdObleaCil == item.ID);
                if (valUni != null) item.IDValUni = valUni.IDValUni;
            }

            oblea.Observacion = txtObservaciones.Text;

            if (oblea.Cilindros.Any(oc => oc.RealizaPH))
            {
                oblea.PH = new PHExtendedView();
                oblea.PH.ID = Guid.NewGuid();
            }

            /// TODO: USO Particular poner el que va
            oblea.IdUso = new Guid("7CD632C5-73F1-4EE6-9273-C2806E24563D");
            return oblea;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../default.aspx");
        }

        protected void txtModalNumeroDocumento_TextChanged(object sender, EventArgs e)
        {
            var cliente = this.ClientesLogic.ReadByDocumento(cboTipoDocumento.SelectedValue, txtNumeroDocumento.Text);
            if (cliente != null)
            {
                txtNombreApellido.Text = cliente.ClienteNombreApellido;
                txtDomicilio.Text = cliente.ClienteDomicilio;
                cboLocalidad.SelectedValue = cliente.ClienteLocalidadID;
                txtTelefono.Text = cliente.ClienteTelefono;
                txtCelular.Text = cliente.ClienteCelular;
                txtEmail.Text = cliente.ClienteEmail;
                txtDominio.Focus();
            }
            else
            {
                txtNombreApellido.Text = String.Empty;
                txtDomicilio.Text = String.Empty;
                cboLocalidad.SelectedIndex = -1;
                txtTelefono.Text = String.Empty;
                txtCelular.Text = String.Empty;
                txtEmail.Text = String.Empty;
                txtNombreApellido.Focus();
            }

            txtNombreApellido.ReadOnly = false;
            txtDomicilio.ReadOnly = false;
            cboLocalidad.Enabled = true;
            txtTelefono.ReadOnly = false;
            txtCelular.ReadOnly = false;
            txtEmail.ReadOnly = false;

            if (txtNumeroDocumento.Text != String.Empty)
                Session["NRODOCUMENTOFOTO"] = txtNumeroDocumento.Text;
        }

        protected void txtModalDominio_TextChanged(object sender, EventArgs e)
        {
            if (!this.ObleasLogic.ExisteTramitePendienteParaElDominio(txtDominio.Text))
            {
                VehiculosView vehiculo = this.VehiculosLogic.ReadByDominio(txtDominio.Text);
                if (vehiculo != null)
                {
                    txtDominio.Text = vehiculo.VehiculoDominio;
                    txtMarca.Text = vehiculo.VehiculoMarca;
                    txtModelo.Text = vehiculo.VehiculoModelo;
                    txtAnio.Text = vehiculo.VehiculoAnio.HasValue ? vehiculo.VehiculoAnio.Value.ToString() : String.Empty;
                    chkEsInyeccion.Checked = vehiculo.VehiculoEsInyeccion.HasValue ? vehiculo.VehiculoEsInyeccion.Value : false;
                    Reguladores.SetFocus();
                }
                else
                {
                    txtMarca.Text = String.Empty;
                    txtModelo.Text = String.Empty;
                    txtAnio.Text = String.Empty;
                    chkEsInyeccion.Checked = false;

                    txtMarca.Focus();
                }

                txtMarca.ReadOnly = false;
                txtModelo.ReadOnly = false;
                txtAnio.ReadOnly = false;
                chkEsInyeccion.Enabled = false;

                if (txtDominio.Text != String.Empty)
                {
                    Session["DOMINIOFOTO"] = txtDominio.Text;

                    String nombreArchivoDniFrente = String.Format("{0}_{1}_{2}", SiteMaster.Taller.ID, txtDominio.Text, "DNIFRENTE");
                    String urlArchivoDniFrente = $"/Captures/{nombreArchivoDniFrente}.png";

                    String nombreArchivoDniDorso = String.Format("{0}_{1}_{2}", SiteMaster.Taller.ID, txtDominio.Text, "DNIDORSO");
                    String urlArchivoDniDorso = $"/Captures/{nombreArchivoDniDorso}.png";

                    String nombreArchivoTarjetaFrente = String.Format("{0}_{1}_{2}", SiteMaster.Taller.ID, txtDominio.Text, "TJFRENTE");
                    String urlArchivoTarjetaFrente = $"/Captures/{nombreArchivoTarjetaFrente}.png";

                    String nombreArchivoTarjetaDorso = String.Format("{0}_{1}_{2}", SiteMaster.Taller.ID, txtDominio.Text, "TJDORSO");
                    String urlArchivoTarjetaDorso = $"/Captures/{nombreArchivoTarjetaDorso}.png";

                    imgDniFrente.ImageUrl = File.Exists(Server.MapPath($"~{urlArchivoDniFrente}")) ? urlArchivoDniFrente : "~/img/dni-frente.gif";
                    imgDniDorso.ImageUrl = File.Exists(Server.MapPath($"~{urlArchivoDniDorso}")) ? urlArchivoDniDorso : "~/img/dni-dorso.gif";
                    imgTarjetaDorso.ImageUrl = File.Exists(Server.MapPath($"~{urlArchivoTarjetaDorso}")) ? urlArchivoTarjetaDorso : "~/img/cedula-dorso.gif";
                    imgTarjetaFrente.ImageUrl = File.Exists(Server.MapPath($"~{urlArchivoTarjetaFrente}")) ? urlArchivoTarjetaFrente: "~/img/cedula-frente.gif";
                }
            }
            else
            {
                String mensaje = $"El dominio {txtDominio.Text} ya tiene un tramite en curso; por cualquier inconveniente comuníquese con Petrolera Italo Argentina srl.-";
                Master.MessageBox(null, mensaje, UserControls.MessageBoxCtrl.TipoWarning.Warning);

                txtMarca.Text = String.Empty;
                txtModelo.Text = String.Empty;
                txtAnio.Text = String.Empty;
                chkEsInyeccion.Checked = false;

                txtMarca.Focus();
            }


        }

        protected void imgEliminar_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton img = (ImageButton)sender;

            //falta eliminar archivo
            String path = Server.MapPath($"~{hdnImagen.Value.Split('?')[0]}");
            if (File.Exists(path)) File.Delete(path);

            if (path.ToUpper().Contains("DNIFRENTE")) imgDniFrente.ImageUrl = "~/img/dni-frente.gif";
            if (path.ToUpper().Contains("DNIDORSO")) imgDniDorso.ImageUrl = "~/img/dni-dorso.gif";
            if (path.ToUpper().Contains("TJFRENTE")) imgTarjetaFrente.ImageUrl = "~/img/cedula-frente.gif";
            if (path.ToUpper().Contains("TJDORSO")) imgTarjetaDorso.ImageUrl = "~/img/cedula-dorso.gif";
        }
    }
}
