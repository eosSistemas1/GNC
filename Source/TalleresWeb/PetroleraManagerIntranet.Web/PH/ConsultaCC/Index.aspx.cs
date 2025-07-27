using CrossCutting.DatosDiscretos;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.PH
{
    public partial class Index : PageBase
    {
        #region Properties
        private PHCilindrosLogic phCilindrosLogic;
        private PHCilindrosLogic PHCilindrosLogic
        {
            get
            {
                if (phCilindrosLogic == null) phCilindrosLogic = new PHCilindrosLogic();
                return phCilindrosLogic;
            }
        }

        private readonly Color evenColor = Color.White;
        private readonly Color oddColor = Color.LightGray;

        private String ZonaSeleccionada
        {
            get { return ViewState["ZONASELECCIONADA"].ToString(); }
            set { ViewState["ZONASELECCIONADA"] = value; }
        }


        public List<PHConsultaView> Tramites
        {
            get
            {
                return ViewState["TRAMITES"] as List<PHConsultaView>;
            }
            set
            {
                ViewState["TRAMITES"] = value;
            }
        }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ActualizarNumeroZona();

            if (!IsPostBack)
            {               
                lblTitulo.Text = "Consulta de PH";
            }
        }

        private void ActualizarNumeroZona()
        {
            lblNorte.Text = $"{this.PHCilindrosLogic.ReadPHPendientesByZona("NORTE").Count}";
            lblSur.Text = $"{this.PHCilindrosLogic.ReadPHPendientesByZona("SUR").Count}";
            lblEste.Text = $"{this.PHCilindrosLogic.ReadPHPendientesByZona("ESTE").Count}";
            lblOeste.Text = $"{this.PHCilindrosLogic.ReadPHPendientesByZona("OESTE").Count}";
            lblComisionista.Text = $"{this.PHCilindrosLogic.ReadPHPendientesByZona("COMISIONISTA").Count}";

            lblVerificarCodigos.Text = $"{this.PHCilindrosLogic.ReadCilindrosPHPorEstado(EstadosPH.SolicitaVerificacion).Count}";
        }

        protected void btnZona_ServerClick(object sender, EventArgs e)
        {
            HtmlButton button = sender as HtmlButton;
            String zona = String.Empty;

            Norte.Attributes["class"] = "btn btn-primary btn-sm";
            Sur.Attributes["class"] = "btn btn-primary btn-sm";
            Este.Attributes["class"] = "btn btn-primary btn-sm";
            Oeste.Attributes["class"] = "btn btn-primary btn-sm";
            Comisionista.Attributes["class"] = "btn btn-primary btn-sm";

            if (button.ID.ToUpper() == ZONAS.Norte)
            {
                zona = ZONAS.Norte;
                Norte.Attributes["class"] = "btn btn-primary btn-sm active";
            }
            if (button.ID.ToUpper() == ZONAS.Sur)
            {
                zona = ZONAS.Sur;
                Sur.Attributes["class"] = "btn btn-primary btn-sm active";
            }
            if (button.ID.ToUpper() == ZONAS.Este)
            {
                zona = ZONAS.Este;
                Este.Attributes["class"] = "btn btn-primary btn-sm active";
            }
            if (button.ID.ToUpper() == ZONAS.Oeste)
            {
                zona = ZONAS.Oeste;
                Oeste.Attributes["class"] = "btn btn-primary btn-sm active";
            }
            if (button.ID.ToUpper() == ZONAS.Comisionista)
            {
                zona = ZONAS.Comisionista;
                Comisionista.Attributes["class"] = "btn btn-primary btn-sm active";
            }


            if (String.IsNullOrEmpty(zona))
                throw new Exception("Debe seleccionar una zona");

            this.CargarZona(zona.ToUpper());
        }

        private void CargarZona(string zona)
        {
            this.ZonaSeleccionada = zona;

            this.Tramites = this.PHCilindrosLogic.ReadPHPendientesByZona(zona);

            List<ViewEntity> talleres = this.ObtenerTalleres();

            repeaterDespacho.DataSource = talleres;
            repeaterDespacho.DataBind();

        }

        private List<ViewEntity> ObtenerTalleres()
        {
            List<ViewEntity> talleres = new List<ViewEntity>();
            foreach (var tramite in this.Tramites.GroupBy(t => t.TallerID))
            {
                talleres.Add(new ViewEntity(tramite.First().TallerID, tramite.First().TallerRazonSocial));
            }
            return talleres;
        }

        private List<PHConsultaView> ObtenerTramitesPorTaller(Guid tallerID)
        {
            List<PHConsultaView> tramitesPorTaller = this.Tramites.Where(t => t.TallerID == tallerID).ToList();                    
            return tramitesPorTaller;
        }

        protected void repeaterDespacho_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item
                || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnTallerID = e.Item.FindControl("hdnTallerID") as HiddenField;

                List<PHConsultaView> dt = ObtenerTramitesPorTaller(new Guid(hdnTallerID.Value));

                GridView grdTramites = e.Item.FindControl("grdTramites") as GridView;
                grdTramites.DataSource = dt.OrderBy(t => t.Dominio).ThenBy(t => t.Fecha);
                grdTramites.DataBind();
            }
        }

        protected void grdTramites_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView grilla = sender as GridView;
                Guid estadoID = (Guid)grilla.DataKeys[e.Row.RowIndex].Values["EstadoPHID"];

                ObtenerColorEstado(e, estadoID);

                SetearColorFilaGrilla(e, grilla);
            }
        }

        private void SetearColorFilaGrilla(GridViewRowEventArgs e, GridView grilla)
        {
            var filaActual = e.Row;

            if (filaActual.RowIndex == 0) e.Row.BackColor = evenColor;

            if (filaActual.RowIndex > 0)
            {
                var filaAnterior = grilla.Rows[e.Row.RowIndex - 1];

                if (filaActual.Cells[3].Text != filaAnterior.Cells[3].Text)
                {
                    filaActual.BackColor = oddColor;
                    if (filaAnterior.BackColor == oddColor)
                        e.Row.BackColor = evenColor;
                }
                else
                {
                    filaActual.BackColor = filaAnterior.BackColor;
                }
            }
        }

        private static void ObtenerColorEstado(GridViewRowEventArgs e, Guid estadoID)
        {
            if (estadoID == EstadosPH.Bloqueada) //estadoID == EstadosPH.Rechazada 
            {
                e.Row.Cells[5].CssClass = "estado-rojo";
            }
            else if (estadoID == EstadosPH.Ingresada)
            {
                e.Row.Cells[5].CssClass = "estado-verde";
            }
        }        
        #endregion
    }
}