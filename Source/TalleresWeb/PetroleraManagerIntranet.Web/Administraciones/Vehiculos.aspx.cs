using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Script.Services;
using System.ComponentModel;
using System.IO;
using System.Reflection;
namespace PetroleraManagerIntranet.Web.Administraciones
{
    public partial class Vehiculos : PageBase
    {
        #region Properties
        private int Busqueda = 1;
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

        public class Vehiculo
        {
            public Guid ID { get; set; }
            public string Descripcion { get; set; }
        }
        public class DataTables
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<Vehiculo> data { get; set; }
        }

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Accion();
                if (Busqueda == 1)
                    this.Buscar();
            }
        }

        public static List<Vehiculo> SortByColumnWithOrder(string order, string orderDir, List<Vehiculo> data)
        {
            // Initialization.    
            List<Vehiculo> lst = new List<Vehiculo>();
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



        public static List<Vehiculo> LoadData()
        {
            // Initialization.    
            List<Vehiculo> lst = new List<Vehiculo>();
            VehiculosParameters vehiculosParameter = new VehiculosParameters();
            var vehiculos = new VehiculosLogic().ReadListView();

            vehiculos.ForEach(dto => {
                var r = new Vehiculo();
                r.ID = dto.ID;
                r.Descripcion = dto.Descripcion;
                lst.Add(r);
            });

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

                List<Vehiculo> data = Vehiculos.LoadData();
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
                data = Vehiculos.SortByColumnWithOrder(order, orderDir, data);
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
                this.NuevoVehiculo.Visible = false;
            }
            if (Request.QueryString["a"] == "C")
            {
                this.AccionConsulta(false);
                this.AccionUsuario.InnerText = "CONSULTAR";
                this.NuevoVehiculo.Visible = false;

                this.txtDominio.Enabled = false;
                this.txtMarca.Enabled = false;
                this.txtModelo.Enabled = false;
                this.txtAnio.Enabled = false;
                this.txtNumeroRA.Enabled = false;

                this.chkEsRA.Enabled = false;
                this.chkEsInyeccion.Enabled = false;

            }
            if (Request.QueryString["a"] == "B") this.Eliminar();

            this.divDatos.Visible = !this.divBuscar.Visible;
        }

        private void Eliminar()
        {
            try
            {
                Guid id = new Guid(Request.QueryString["id"].ToString());
                VehiculosLogic.Delete(id);
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
            txtDominio.Focus();
            Busqueda = 2;
            this.NuevoVehiculo.Visible = false;
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
            Busqueda = 2;
        }

        private void LimpiarCampos()
        {
            txtDominio.Text = String.Empty;
            txtMarca.Text = String.Empty;
            txtModelo.Text = String.Empty;
            txtAnio.Text = String.Empty;
            chkEsRA.Checked = false;
            txtNumeroRA.Text = String.Empty;
            chkEsInyeccion.Checked = false;
        }

        private void CargarDatosModificacion(Guid id)
        {
            TalleresWeb.Entities.Vehiculos entity = VehiculosLogic.Read(id);

            if (entity != null)
            {
                hddID.Value = entity.ID.ToString();
                txtDominio.Text = entity.Descripcion;
                txtMarca.Text = entity.MarcaVehiculo;
                txtModelo.Text = entity.ModeloVehiculo;
                txtAnio.Text = entity.AnioVehiculo.HasValue ? entity.AnioVehiculo.Value.ToString() : String.Empty;
                chkEsRA.Checked = entity.RA.HasValue ? entity.RA.Value : false;
                txtNumeroRA.Text = entity.NumeroRA;
                chkEsInyeccion.Checked = entity.EsInyeccionVehiculo.HasValue ? entity.EsInyeccionVehiculo.Value : false;
            }
        }


        protected void Buscar_ServerClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.txtFiltro.Value))
            {
                this.Buscar();
            }
            else
            {
                this.MessageBoxCtrl.MessageBox(null, "Debe ingresar filtro.", UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        private void Buscar()
        {
            this.divBuscar.Visible = true;
            this.divDatos.Visible = false;

            VehiculosParameters vehiculosParameter = new VehiculosParameters();
            vehiculosParameter.Descripcion = txtFiltro.Value;
            this.NuevoVehiculo.Visible = true;
        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            TalleresWeb.Entities.Vehiculos vehiculo = new TalleresWeb.Entities.Vehiculos();
            Guid id = !String.IsNullOrWhiteSpace(hddID.Value) ? new Guid(hddID.Value) : Guid.NewGuid();
            vehiculo.ID = id;
            vehiculo.Descripcion = txtDominio.Text.Trim().ToUpper();
            vehiculo.MarcaVehiculo = txtMarca.Text.Trim().ToUpper(); 
            vehiculo.ModeloVehiculo = txtModelo.Text.Trim().ToUpper();
            int value;
            if (int.TryParse(txtAnio.Text, out value))          
            vehiculo.AnioVehiculo = value;
            vehiculo.RA = chkEsRA.Checked;
            vehiculo.NumeroRA = txtNumeroRA.Text;
            vehiculo.EsInyeccionVehiculo = chkEsInyeccion.Checked;

            List<String> mensajes = this.Validar(vehiculo);

            if (!mensajes.Any())
            {
                VehiculosLogic.AddVehiculo(vehiculo);

                LimpiarCampos();
                divDatos.Visible = false;
                divBuscar.Visible = true;
            }
            else
            {
                MessageBoxCtrl.MessageBox("", mensajes, UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
            if (Busqueda == 1)
                Buscar();
        }

        private List<String> Validar(TalleresWeb.Entities.Vehiculos entity)
        {
            List<String> mensajes = new List<String>();

            if (String.IsNullOrWhiteSpace(entity.Descripcion))
            {
                Busqueda = 2;
                mensajes.Add("Ingrese dominio");
            }
            else
            {
                Busqueda = 1;
            }

            if (String.IsNullOrWhiteSpace(entity.MarcaVehiculo))
            {
                Busqueda = 2;
                mensajes.Add("Ingrese marca");
            }
            else
            {
                Busqueda = 1;
            }

            if (String.IsNullOrWhiteSpace(entity.ModeloVehiculo))
            {
                Busqueda = 2;
                mensajes.Add("Ingrese modelo");
            }
            else
            {
                Busqueda = 1;
            }

            if (!entity.AnioVehiculo.HasValue)
            {
                Busqueda = 2;
                mensajes.Add("Ingrese año");
            }
            else
            {
                Busqueda = 1;
            }

            if (entity.RA.Value && String.IsNullOrWhiteSpace(entity.NumeroRA))
            {
                Busqueda = 2;
                mensajes.Add("Si el vehículo es RA el número RA es obligatorio");
            }
            else
            {
                Busqueda = 1;
            }

            Boolean existeOtroVehiculoConMismoDominio = this.VehiculosLogic.ExisteOtroVehiculoConMismoDominio(entity.ID, entity.Descripcion);

            if (!string.IsNullOrWhiteSpace(txtDominio.Text) && existeOtroVehiculoConMismoDominio)
            {
                Busqueda = 2;
                mensajes.Add("Existe otro vehículo con el mismo dominio");
            }
            else
            {
                Busqueda = 1;
            }

            return mensajes;
        }
        #endregion
    }
}