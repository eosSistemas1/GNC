using CrossCutting.DatosDiscretos;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Obleas
{
    public partial class ConsultarFichasTecnicas : PageBase
    {
        #region Properties
        public List<ObleasExtendedView> obleas
        {
            get
            {
                if (ViewState["OBLEAS"] == null) return new List<ObleasExtendedView>();
                return (List<ObleasExtendedView>)ViewState["OBLEAS"];
            }
            set
            {
                ViewState["OBLEAS"] = value;
            }
        }
        private ObleasLogic logic;
        public ObleasLogic Logic
        {
            get
            {
                if (this.logic == null) this.logic = new ObleasLogic();
                return this.logic;
            }
            set { logic = value; }
        }
        public string ZonaSeleccionada
        {
            get
            {
                if (ViewState["ZonaSeleccionada"] == null) ViewState["ZonaSeleccionada"] = "NORTE";
                return ViewState["ZonaSeleccionada"].ToString();
            }
            set
            {
                ViewState["ZonaSeleccionada"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {                
                this.cboEstadoFicha.SelectedValue = ESTADOSFICHAS.PendienteRevision.ToString();

                if (Request.QueryString["e"] != null)
                {
                    try
                    {
                        Guid idEstado = Guid.Parse(Request.QueryString["e"].ToString());
                        this.cboEstadoFicha.SelectedValue = idEstado.ToString();
                    }
                    catch
                    { }
                }
               
                this.CambiaEstado();
            }
        }

        private void ActualizarNumerosZona()
        {
            lblNorte.Text = $"{obleas.Where(o=> o.Zona == "NORTE").LongCount()}";
            lblSur.Text = $"{obleas.Where(o => o.Zona == "SUR").LongCount()}";
            lblEste.Text = $"{obleas.Where(o => o.Zona == "ESTE").LongCount()}";
            lblOeste.Text = $"{obleas.Where(o => o.Zona == "OESTE").LongCount()}";
            lblComisionista.Text = $"{obleas.Where(o => o.Zona == "COMISIONISTA").LongCount()}";
        }

        protected void btnZona_ServerClick(object sender, EventArgs e)
        {
            if (RequiereFechas())
            {
                if (String.IsNullOrEmpty(calFechaD.Value) ||
                    String.IsNullOrEmpty(calFechaH.Value))
                {
                    MessageBoxCtrl1.MessageBox(null, "Debe ingresar las fechas <br>", UserControls.MessageBoxCtrl.TipoWarning.Warning);                  
                    return;
                }
                else
                {
                    DateTime desde = DateTime.Parse(calFechaD.Value);
                    DateTime hasta = DateTime.Parse(calFechaH.Value);

                    if ((hasta - desde).TotalDays > 31)
                    {
                        MessageBoxCtrl1.MessageBox(null, "Las fechas ingresadas no pueden superar el mes<br>", UserControls.MessageBoxCtrl.TipoWarning.Warning);                        
                        return;
                    }
                }
            }

            HtmlButton button = sender as HtmlButton;
            String zona = String.Empty;

            Norte.Attributes["class"] = "btn btn-primary btn-sm";
            Sur.Attributes["class"] = "btn btn-primary btn-sm";
            Este.Attributes["class"] = "btn btn-primary btn-sm";
            Oeste.Attributes["class"] = "btn btn-primary btn-sm";
            Comisionista.Attributes["class"] = "btn btn-primary btn-sm";

            string claseActiva = "btn btn-primary btn-sm active";

            if (button.ID.ToUpper() == ZONAS.Norte)
            {
                zona = ZONAS.Norte;
                Norte.Attributes["class"] = claseActiva;
            }
            if (button.ID.ToUpper() == ZONAS.Sur)
            {
                zona = ZONAS.Sur;
                Sur.Attributes["class"] = claseActiva;
            }
            if (button.ID.ToUpper() == ZONAS.Este)
            {
                zona = ZONAS.Este;
                Este.Attributes["class"] = claseActiva;
            }
            if (button.ID.ToUpper() == ZONAS.Oeste)
            {
                zona = ZONAS.Oeste;
                Oeste.Attributes["class"] = claseActiva;
            }
            if (button.ID.ToUpper() == ZONAS.Comisionista)
            {
                zona = ZONAS.Comisionista;
                Comisionista.Attributes["class"] = claseActiva;
            }

            if (String.IsNullOrEmpty(zona))
                throw new Exception("Debe seleccionar una zona");

            ZonaSeleccionada = zona;
            this.CargarGrilla(zona);
        }

        private void CargarGrilla(string zona)
        {
            Guid idEstadoFicha = Guid.Parse(cboEstadoFicha.SelectedValue);

            DateTime? fechaDesde = String.IsNullOrEmpty(calFechaD.Value) ? default(DateTime?) : DateTime.Parse(calFechaD.Value);
            DateTime? fechaHasta = String.IsNullOrEmpty(calFechaH.Value) ? default(DateTime?) : DateTime.Parse(calFechaH.Value);
           
            obleas = this.Logic.ReadObleasPorEstado(idEstadoFicha, fechaDesde, fechaHasta);

            ActualizarNumerosZona();

            List<ViewEntity> talleres = this.ObtenerTalleres(obleas, zona);

            repeaterZonas.DataSource = talleres;
            repeaterZonas.DataBind();
        }

        private List<ViewEntity> ObtenerTalleres(List<ObleasExtendedView> obleas, string zona)
        {
            List<ViewEntity> talleres = new List<ViewEntity>();
            var talleresPorZona = obleas.Where(x => x.Zona == zona);
            foreach (var tramite in talleresPorZona.GroupBy(t => t.IdTaller))
            {
                talleres.Add(new ViewEntity(tramite.First().IdTaller, tramite.First().Taller));
            }
            return talleres;
        }

        protected void repeaterZonas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item
                || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnTallerID = e.Item.FindControl("hdnTallerID") as HiddenField;
                Guid idTaller = new Guid(hdnTallerID.Value);
                List<ObleasExtendedView> dt = obleas.Where(t => t.IdTaller == idTaller).ToList();

                Repeater repeaterTaller = e.Item.FindControl("repeaterTaller") as Repeater;
                repeaterTaller.DataSource = dt.OrderBy(t => t.Dominio).ThenBy(t => t.FechaAlta);
                repeaterTaller.DataBind();
            }
        }

        private void CambiaEstado()
        {
            lblTituloPagina.Text = String.Format("CONSULTAR FICHAS TÉCNICAS: ({0})", 0);

            if (RequiereFechas())
            {
                calFechaD.Visible = true;
                calFechaH.Visible = true;
                btnBuscar.Visible = true;

                repeaterZonas.DataSource = null;
                repeaterZonas.DataBind();
            }
            else
            {
                calFechaD.Value = string.Empty;
                calFechaH.Value = string.Empty;
                calFechaD.Visible = false;
                calFechaH.Visible = false;
                btnBuscar.Visible = false;

                this.CargarGrilla(ZonaSeleccionada);
            }            
        }

        private bool RequiereFechas()
        {
            Guid estadoSeleccionado = Guid.Parse(this.cboEstadoFicha.SelectedValue);
            return (estadoSeleccionado == ESTADOSFICHAS.Finalizada
                    || estadoSeleccionado == ESTADOSFICHAS.Entregada
                    || estadoSeleccionado == ESTADOSFICHAS.Asignada
                    || estadoSeleccionado == ESTADOSFICHAS.AsignadaConError
                    || estadoSeleccionado == Guid.Empty);
        }

        protected void cboEstadoFicha_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CambiaEstado();
        }
        
        protected void btnBuscar_ServerClick(object sender, EventArgs e)
        {
            if (RequiereFechas())
            {
                if (String.IsNullOrEmpty(calFechaD.Value) ||
                    String.IsNullOrEmpty(calFechaH.Value))
                {
                    MessageBoxCtrl1.MessageBox(null, "Debe ingresar las fechas <br>", UserControls.MessageBoxCtrl.TipoWarning.Warning);                    
                    return;
                }
                else
                {
                    DateTime desde = DateTime.Parse(calFechaD.Value);
                    DateTime hasta = DateTime.Parse(calFechaH.Value);

                    if ((hasta - desde).TotalDays > 31)
                    {
                        MessageBoxCtrl1.MessageBox(null, "Las fechas ingresadas no pueden superar el mes", UserControls.MessageBoxCtrl.TipoWarning.Warning);                        
                        return;
                    }
                }
            }

            this.CargarGrilla(this.ZonaSeleccionada);
        }
    }
}