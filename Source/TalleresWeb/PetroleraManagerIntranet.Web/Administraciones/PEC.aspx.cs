using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Administraciones
{
    public partial class PEC : PageBase
    {
        #region Properties               
        private bool mostrarListado = true;

        public static PECLogic pecLogic;
        public static PECLogic PECLogic
        {
            get
            {
                if (pecLogic == null) pecLogic = new PECLogic();
                return pecLogic;
            }
        }

        private RT_PECLogic rt_PECLogic;
        public RT_PECLogic RT_PECLogic
        {
            get
            {
                if (rt_PECLogic == null) rt_PECLogic = new RT_PECLogic();
                return rt_PECLogic;
            }
        }

        private RTLogic rtLogic;
        public RTLogic RTLogic
        {
            get
            {
                if (rtLogic == null) rtLogic = new RTLogic();
                return rtLogic;
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

                List<ViewEntity> data = PECLogic.ReadListView().OrderBy(c => c.Descripcion).ToList();
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
                data = SortByColumnWithOrder(order, orderDir, data);
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

                this.cboLocalidad.Enabled = false;
                txtMatricula.Disabled = true;
                txtRazonSocial.Disabled = true;
                txtDomicilio.Disabled = true;
                txtTelefono.Disabled = true;
                txtTelefono2.Disabled = true;
                txtMail.Disabled = true;
                txtMail2.Disabled = true;
            }

            if (Request.QueryString["a"] == "B") this.Eliminar();

            this.divDatos.Visible = !this.divBuscar.Visible;
        }

        private void Eliminar()
        {
            try
            {
                Guid id = new Guid(Request.QueryString["id"].ToString());

                PECLogic.Delete(id);
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
            txtMatricula.Focus();
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
            txtMatricula.Value = String.Empty;
            txtRazonSocial.Value = String.Empty;
            txtDomicilio.Value = String.Empty;
            txtTelefono.Value = String.Empty;
            txtTelefono2.Value = String.Empty;
            txtMail.Value = String.Empty;
            txtMail2.Value = String.Empty;
            cboLocalidad.SelectedIndex = -1;
        }

        private void CargarDatosModificacion(Guid id)
        {
            TalleresWeb.Entities.PEC entity = PECLogic.Read(id);

            if (entity != null)
            {
                hddID.Value = entity.ID.ToString();
                txtMatricula.Value = entity.Descripcion;
                txtRazonSocial.Value = entity.RazonSocialPEC;
                txtDomicilio.Value = entity.DomicilioPEC;
                txtTelefono.Value = entity.TelefonoAPEC;
                txtTelefono2.Value = entity.TelefonoBPEC;
                txtMail.Value = entity.MailAPEC;
                txtMail2.Value = entity.MailBPEC;
                cboLocalidad.SelectedValue = entity.IdLocalidad.ToString();

                CargarPECRT(entity.ID);

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
            TalleresWeb.Entities.PEC pec = new TalleresWeb.Entities.PEC();
            Guid id = !String.IsNullOrWhiteSpace(hddID.Value) ? new Guid(hddID.Value) : Guid.NewGuid();
            pec.ID = id;
            pec.Descripcion = txtMatricula.Value.ToUpper().Trim();
            pec.RazonSocialPEC = txtRazonSocial.Value.ToUpper().Trim();
            pec.DomicilioPEC = txtDomicilio.Value.ToUpper().Trim();
            pec.TelefonoAPEC = txtTelefono.Value.Trim();
            pec.TelefonoBPEC = txtTelefono2.Value.Trim();
            pec.MailAPEC = txtMail.Value.Trim();
            pec.MailBPEC = txtMail2.Value.Trim();
            pec.IdLocalidad = new Guid(cboLocalidad.SelectedValue);

            List<String> mensajes = this.Validar(pec);

            if (!mensajes.Any())
            {

                var esNuevo = PECLogic.Read(pec.ID) == null;

                PECLogic.AddPEC(pec);

                if (esNuevo)
                {
                    foreach (var rT_PEC in this.RT_PECNuevo())
                    {
                        rT_PEC.PECID = pec.ID;                      
                        RT_PECLogic.Add(new RT_PEC() {
                            ID = rT_PEC.ID,
                            PECID = rT_PEC.PECID,
                            RTID = rT_PEC.RTID,
                            FechaDesde = rT_PEC.FechaDesde,
                            FechaHasta = rT_PEC.FechaHasta
                        });
                    }
                }

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

        private List<String> Validar(TalleresWeb.Entities.PEC entity)
        {
            List<String> mensajes = new List<String>();

            if (String.IsNullOrWhiteSpace(entity.Descripcion))
            {
                mensajes.Add("Ingrese matrícula");
                mostrarListado = false;
            }
            else
            {
                mostrarListado = true;
            }

            if (String.IsNullOrWhiteSpace(entity.RazonSocialPEC))
            {
                mensajes.Add("Ingrese razón social");
                mostrarListado = false;
            }
            else
            {
                mostrarListado = true;
            }

            return mensajes;
        }

        #endregion

        #region TalleresRT
        protected void grdRT_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView grd = (GridView)sender;
            Guid idItem = new Guid(grd.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());

            if (e.CommandName == "modificar")
            {
                var rt_pec = RT_PECLogic.Read(idItem);

                txtID.Value = rt_pec.ID.ToString();
                txtIDPEC.Value = rt_pec.PECID.ToString();
                cboRT.SelectedValue = rt_pec.RTID.ToString();
                calFechaDRT.Value = rt_pec.FechaDesde.Value.ToString("dd/MM/yyyy");
                calFechaHRT.Value = rt_pec.FechaHasta.HasValue ? rt_pec.FechaHasta.Value.ToString("dd/MM/yyyy") : String.Empty;

            }
        }

        protected void btnGuardarRT_Click(object sender, EventArgs e)
        {
            List<string> erroresRT = this.ValidarRT();

            if (!erroresRT.Any())
            {
               
                RT_PEC rT_PEC = new RT_PEC
                {
                    PECID = Guid.Empty,
                    ID = !String.IsNullOrWhiteSpace(txtID.Value) ? new Guid(txtID.Value) : Guid.NewGuid(),
                    RTID = new Guid(cboRT.SelectedValue),
                    FechaDesde = DateTime.Parse(calFechaDRT.Value),
                    FechaHasta = !String.IsNullOrWhiteSpace(calFechaHRT.Value) ? DateTime.Parse(calFechaHRT.Value) : default(DateTime?)
                };

                if (Request.QueryString["a"] == "M")
                {
                    Guid idPEC = new Guid(hddID.Value);
                    rT_PEC.PECID = idPEC;

                    this.RT_PECLogic.AddRT(rT_PEC);

                    this.InicializarTallerRT();

                    this.CargarPECRT(rT_PEC.PECID);
                }

                if (Request.QueryString["a"] == "A")
                {
                    RT_PECNuevo(rT_PEC);
                
                    grdRT.DataSource = RT_PECNuevo();
                    grdRT.DataBind();
                }
            }
            else
            {
                MessageBoxCtrl.MessageBox(null, erroresRT, UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        private List<RT_PECExtendedView> RT_PECNuevo()
        {
            return ViewState["RT_PECNUEVO"] != null ? (List<RT_PECExtendedView>)ViewState["RT_PECNUEVO"] : new List<RT_PECExtendedView>();
        }
        private List<RT_PECExtendedView> RT_PECNuevo(RT_PEC rt_pec)
        {
            List<RT_PECExtendedView> lista = ViewState["RT_PECNUEVO"] != null ? (List<RT_PECExtendedView>)ViewState["RT_PECNUEVO"] : new List<RT_PECExtendedView>();

            RT_PECExtendedView rt_pecNuevo = new RT_PECExtendedView()
            {
                ID = rt_pec.ID,
                RTID = rt_pec.RTID,
                PECID = rt_pec.PECID,
                FechaDesde = rt_pec.FechaDesde,
                FechaHasta = rt_pec.FechaHasta
            };
            var rt = RTLogic.Read(rt_pec.RTID);
            rt_pecNuevo.Descripcion = rt != null? rt.MatriculaRT + "-" + rt.NombreApellidoRT : string.Empty;
            lista.Add(rt_pecNuevo);
            ViewState["RT_PECNUEVO"] = lista;

            return lista;
        }

        private List<String> ValidarRT()
        {
            List<String> mensaje = new List<String>();

            if (cboRT.SelectedIndex == -1 || cboRT.SelectedValue == Guid.Empty.ToString()) mensaje.Add("- Seleccione un responsable técnico");
            if (String.IsNullOrWhiteSpace(calFechaDRT.Value)) mensaje.Add("- Seleccione una fecha desde");

            return mensaje;
        }

        private void InicializarTallerRT()
        {
            this.txtID.Value = String.Empty;            
            this.txtIDPEC.Value = String.Empty;
            this.cboRT.SelectedIndex = 0;
            this.calFechaDRT.Value = DateTime.Now.ToString("dd/MM/yyyy");
            this.calFechaHRT.Value = String.Empty;
        }

        private void CargarPECRT(Guid pecID)
        {
            var rt_pec = RT_PECLogic.ReadByPEC(pecID);

            grdRT.DataSource = rt_pec;
            grdRT.DataBind();
        }

        protected void grdRT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Color colorPrincipal = Color.LightGreen;
                Color colorVencido = Color.Coral;

                if (!String.IsNullOrWhiteSpace(e.Row.Cells[1].Text) && e.Row.Cells[1].Text != "&nbsp;")
                {
                    DateTime fechaInicio = DateTime.Parse(e.Row.Cells[1].Text);
                    e.Row.Cells[1].Text = fechaInicio.ToString("dd/MM/yyyy");
                }

                if (!String.IsNullOrWhiteSpace(e.Row.Cells[2].Text) && e.Row.Cells[2].Text != "&nbsp;")
                {
                    DateTime fechaFin = DateTime.Parse(e.Row.Cells[2].Text);

                    if (fechaFin <= DateTime.Now) { e.Row.BackColor = colorVencido; }

                    e.Row.Cells[2].Text = fechaFin.ToString("dd/MM/yyyy");
                }
            }
        }
        #endregion
    }
}
