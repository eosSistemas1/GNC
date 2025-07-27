using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Administraciones
{
    public partial class Clientes : PageBase
    {
        #region Properties
        public static ClientesLogic clientesLogic;
        public ClientesLogic clientesLogicDelete;
        private bool mostrarListado = true;
        public static ClientesLogic ClientesLogic
        {
            get
            {
                if (clientesLogic == null) clientesLogic = new ClientesLogic();
                return clientesLogic;
            }
        }      
        #endregion

        public class DataTables
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<ViewEntity> data { get; set; }
        }

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Accion();
                if (mostrarListado == true)
                    this.Buscar();
            }
        }
        
        public static List<ViewEntity> SortByColumnWithOrder(string order, string orderDir, List<ViewEntity> data)
        {
            // Initialization.    
            List<ViewEntity> lst = new List<ViewEntity>();
            try
            {
                // Sorting    
                switch (order)
                {
                    case "0":
                        // Setting.    
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Descripcion).ToList()
                                                             : data.OrderBy(p => p.Descripcion).ToList();
                        break;
                    case "1":
                        // Setting.    
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderBy(p => p.Descripcion).ToList()
                                                             : data.OrderBy(p => p.Descripcion).ToList();
                        break;
                    default:
                        // Setting.    
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Descripcion).ToList()
                                                             : data.OrderBy(p => p.Descripcion).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                // info.    
                Console.Write(ex);
            }
            // info.    
            return lst;
        }        

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static object GetData()
        {
            // Initialization.    
            DataTables result = new DataTables();
            try
            {
                // Initialization.    
                string search = HttpContext.Current.Request.Params["search[value]"];
                string draw = HttpContext.Current.Request.Params["draw"];
                string order = HttpContext.Current.Request.Params["order[0][column]"];
                string orderDir = HttpContext.Current.Request.Params["order[0][dir]"];
                int startRec = Convert.ToInt32(HttpContext.Current.Request.Params["start"]);
                int pageSize = Convert.ToInt32(HttpContext.Current.Request.Params["length"]);
                // Loading.    

                List<ViewEntity> data = ClientesLogic.ReadListView().OrderBy(c => c.Descripcion).ToList();
                // Total record count.    
                int totalRecords = data.Count;
                // Verification.    
                if (!string.IsNullOrEmpty(search) &&
                  !string.IsNullOrWhiteSpace(search))
                {
                    // Apply search    
                    data = data.Where(p => p.ID.ToString().ToLower().Contains(search.ToLower()) ||
                                p.Descripcion.ToLower().Contains(search.ToLower())).ToList();
                }
                // Sorting.    
                data = Clientes.SortByColumnWithOrder(order, orderDir, data);
                // Filter record count.    
                int recFilter = data.Count;
                // Apply pagination.    
                data = data.Skip(startRec).Take(pageSize).ToList();
                // Loading drop down lists.    
                result.draw = Convert.ToInt32(draw);
                result.recordsTotal = totalRecords;
                result.recordsFiltered = recFilter;
                result.data = data;
            }
            catch (Exception ex)
            {
                // Info    
                Console.Write(ex);
            }
            // Return info.    

            return result;
        }

        /******************************************************************************/

        private void Accion()
        {

            if (Request.QueryString["a"] == null || Request.QueryString["a"] == "B")
                this.divBuscar.Visible = true;
            if (Request.QueryString["a"] == "A")
            {
                this.AccionAlta();
                this.AccionUsuario.InnerText = "NUEVO";
            }
            if (Request.QueryString["a"] == "M")
            {
                this.AccionConsulta(true);
                this.AccionUsuario.InnerText = "MODIFICAR";
                this.NuevoCliente.Visible = false;
            }
            if (Request.QueryString["a"] == "C")
            {
                this.AccionConsulta(false);
                this.AccionUsuario.InnerText = "CONSULTAR";
                this.NuevoCliente.Visible = false;

                this.tipo_dni.Enabled = false;
                this.cboLocalidad.Enabled = false;
                this.txtNroDocumento.Disabled = true;
                this.txtNombreApellido.Disabled = true;
                this.txtPisoDptoCliente.Disabled = true;
                this.txtCelularCliente.Disabled = true;
                this.txtMailCliente.Disabled = true;
                this.txtDomicilio.Disabled = true;
                this.txtNroCalleCliente.Disabled = true;
                this.txtTelefono.Disabled = true;
            }

            if (Request.QueryString["a"] == "B") this.Eliminar();

            this.divDatos.Visible = !this.divBuscar.Visible;
        }

        private void Eliminar()
        {
            try
            {
                Guid id = new Guid(Request.QueryString["id"].ToString());

                ClientesLogic.Delete(id);
            }
            catch (ArgumentNullException)
            {
                MessageBoxCtrl.MessageBox(null, "El item seleccionado no existe.", UserControls.MessageBoxCtrl.TipoWarning.Error);
            }
            catch (UpdateException)
            {
                MessageBoxCtrl.MessageBox(null, "El item seleccionado no se puede eliminar porque tiene trámites relacionados.", UserControls.MessageBoxCtrl.TipoWarning.Error);
            }
            catch (Exception e)
            {
                List<String> mensaje = new List<String>();
                mensaje.Add("No se pudo eliminar el item seleccionado.");
                mensaje.Add(e.InnerException.Message);
                MessageBoxCtrl.MessageBox(null, mensaje, UserControls.MessageBoxCtrl.TipoWarning.Error);
            }

        }

        private void AccionAlta()
        {
            this.divBuscar.Visible = false;
            this.divDatos.Disabled = false;
            this.LimpiarCampos();
            txtNroDocumento.Focus();
            mostrarListado = false;
            this.NuevoCliente.Visible = false;
        }

        private void AccionConsulta(Boolean modifica)
        {
            if (Request.QueryString["id"] != null)
            {
                this.divBuscar.Visible = false;
                this.divDatos.Disabled = !modifica;
                this.btnAceptar.Visible = modifica;
                Guid id = new Guid(Request.QueryString["id"].ToString());
                this.CargarDatosModificacion(id);
            }
            mostrarListado = false;
        }

        private void LimpiarCampos()
        {
            tipo_dni.SelectedIndex = -1;
            txtNroDocumento.Value = String.Empty;
            txtNombreApellido.Value = String.Empty;
            txtDomicilio.Value = String.Empty;
            txtNroCalleCliente.Value = String.Empty;
            cboLocalidad.SelectedIndex = -1;
            txtTelefono.Value = String.Empty;
        }

        private void CargarDatosModificacion(Guid id)
        {
            TalleresWeb.Entities.Clientes entity = ClientesLogic.Read(id);

            if (entity != null)
            {
                hddID.Value = entity.ID.ToString();
                txtNroDocumento.Value = entity.NroDniCliente;
                txtNombreApellido.Value = entity.Descripcion;
                txtDomicilio.Value = entity.CalleCliente;
                txtNroCalleCliente.Value = entity.NroCalleCliente;
                txtTelefono.Value = entity.TelefonoCliente;
                txtPisoDptoCliente.Value = entity.PisoDptoCliente;
                txtMailCliente.Value = entity.MailCliente;
                txtCelularCliente.Value = entity.CelularCliente;
                tipo_dni.SelectedValue = entity.IdTipoDniCliente.ToString();
                cboLocalidad.SelectedValue = entity.IdLocalidad.ToString();

            }
        }
        private void Buscar()
        {
            this.divBuscar.Visible = true;
            this.divDatos.Visible = false;
            this.NuevoCliente.Visible = true;
        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            TalleresWeb.Entities.Clientes cliente = new TalleresWeb.Entities.Clientes();
            Guid id = !String.IsNullOrWhiteSpace(hddID.Value) ? new Guid(hddID.Value) : Guid.NewGuid();
            cliente.ID = id;
            cliente.Descripcion = txtNombreApellido.Value.ToUpper().Trim();
            cliente.CalleCliente = txtDomicilio.Value.ToUpper().Trim();
            cliente.NroCalleCliente = txtNroCalleCliente.Value;
            cliente.IdTipoDniCliente = new Guid(tipo_dni.SelectedValue);
            cliente.NroDniCliente = txtNroDocumento.Value;
            cliente.TelefonoCliente = txtTelefono.Value.ToUpper().Trim();
            cliente.CelularCliente = txtCelularCliente.Value;
            cliente.PisoDptoCliente = txtPisoDptoCliente.Value.ToUpper().Trim();
            cliente.MailCliente = txtMailCliente.Value.ToUpper().Trim();
            cliente.IdLocalidad = new Guid(cboLocalidad.SelectedValue);

            List<String> mensajes = this.Validar(cliente);

            if (!mensajes.Any())
            {
                ClientesLogic.AddCliente(cliente);

                LimpiarCampos();
                divDatos.Visible = false;
                divBuscar.Visible = true;
            }
            else
            {
                MessageBoxCtrl.MessageBox("", mensajes, UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
            if (mostrarListado == true)
                Buscar();
        }

        private List<String> Validar(TalleresWeb.Entities.Clientes entity)
        {
            List<String> mensajes = new List<String>();

            if (String.IsNullOrWhiteSpace(entity.Descripcion))
            {
                mensajes.Add("Ingrese nombre y apellido");
                mostrarListado = false;
            }
            else
            {
                mostrarListado = true;
            }

            if (String.IsNullOrWhiteSpace(entity.NroDniCliente))
            {
                mensajes.Add("Ingrese nùmero de documento");
                mostrarListado = false;
            }
            else
            {
                mostrarListado = true;
            }

            return mensajes;
        }

        #endregion
    }
}
