using CrossCutting.DatosDiscretos;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;


namespace PetroleraManagerIntranet.Web.Despacho
{
    public partial class Despacho : PageBase
    {
        #region Properties
        private DespachoLogic despachoLogic;

        private DespachoLogic DespachoLogic
        {
            get
            {
                if (despachoLogic == null) despachoLogic = new DespachoLogic();
                return despachoLogic;
            }
        }

        private DespachoDetalleLogic despachoDetalleLogic;
        private DespachoDetalleLogic DespachoDetalleLogic
        {
            get
            {
                if (despachoDetalleLogic == null) despachoDetalleLogic = new DespachoDetalleLogic();
                return despachoDetalleLogic;
            }
        }

        private ObleasLogic obleasLogic;
        private ObleasLogic ObleasLogic
        {
            get
            {
                if (obleasLogic == null) obleasLogic = new ObleasLogic();
                return obleasLogic;
            }
        }

        private PHCilindrosLogic phCilindrosLogic;
        private PHCilindrosLogic PHCilindrosLogic
        {
            get
            {
                if (phCilindrosLogic == null) phCilindrosLogic = new PHCilindrosLogic();
                return phCilindrosLogic;
            }
        }
        public List<DespachoExtendedView> Tramites
        {
            get
            {
                return ViewState["TRAMITES"] as List<DespachoExtendedView>;
            }
            set
            {
                ViewState["TRAMITES"] = value;
            }
        }
        public List<ZonasTallerView> ZonasTallerViewState
        {
            get
            {
                if (ViewState["ZONASTALLERVIEWSTATE"] == null)
                {
                    List<ZonasTallerView> listaVacia = new List<ZonasTallerView>();
                    ViewState["ZONASTALLERVIEWSTATE"] = listaVacia;
                    return ViewState["ZONASTALLERVIEWSTATE"] as List<ZonasTallerView>;
                }
                else
                {
                    return ViewState["ZONASTALLERVIEWSTATE"] as List<ZonasTallerView>;
                }
            }
            set
            {
                ViewState["ZONASTALLERVIEWSTATE"] = value;
            }
        }
        public List<DetalleDespachoView> CarritoTramites
        {
            get
            {
                if (ViewState["CARRITOTRAMITES"] == null)
                {
                    List<DetalleDespachoView> listaVacia = new List<DetalleDespachoView>();
                    ViewState["CARRITOTRAMITES"] = listaVacia;
                    return ViewState["CARRITOTRAMITES"] as List<DetalleDespachoView>;
                }
                else
                {
                    return ViewState["CARRITOTRAMITES"] as List<DetalleDespachoView>;
                }
            }
            set
            {
                ViewState["CARRITOTRAMITES"] = value;
            }
        }
        public DESPACHO DespachoAModificar
        {
            get
            {
                if (ViewState["DespachoAModificar"] == null)
                    return null;

                return ViewState["DespachoAModificar"] as DESPACHO;
            }
            set
            {
                ViewState["DespachoAModificar"] = value;
            }
        }



        private readonly Color evenColor = Color.White;
        private readonly Color oddColor = Color.LightGray;

        private String ZonaSeleccionada
        {
            get { return ViewState["ZONASELECCIONADA"].ToString(); }
            set { ViewState["ZONASELECCIONADA"] = value; }
        }

        private Guid? DespachoAModificarID
        {
            get
            {
                if (ViewState["DespachoAModificarID"] == null)
                    return default(Guid?);

                return Guid.Parse(ViewState["DespachoAModificarID"].ToString());
            }
            set
            {
                ViewState["DespachoAModificarID"] = value;
            }
        }

        #endregion



        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ActualizarNumeroZona();

            if (!IsPostBack)
            {

                if (Request.QueryString["id"] != null)
                {
                    this.DespachoAModificarID = new Guid(Request.QueryString["id"].ToString());
                    this.DespachoAModificar = this.DespachoLogic.Read(this.DespachoAModificarID.Value);
                    lblTitulo.Text = $"Modificar despacho de trámites y mercadería Nro: {this.DespachoAModificar.Numero}";

                    var despacho = this.DespachoAModificar;

                    foreach (var t in despacho.DESPACHODETALLE)
                    {
                        DetalleDespachoView itemDetalleDespacho = new DetalleDespachoView();
                        if (t.IdOblea != null)
                            itemDetalleDespacho.IDTramite = (Guid)t.IdOblea;
                        if (t.IdPHCilindro != null)
                            itemDetalleDespacho.IDTramite = (Guid)t.IdPHCilindro;
                        if (t.IdPedido != null)
                            itemDetalleDespacho.IDTramite = (Guid)t.IdPedido;

                        itemDetalleDespacho.IdTaller = (Guid)t.IdTaller;

                        if (t.IdOblea != null)
                            itemDetalleDespacho.TipoTramite = "OBLEA";
                        if (t.IdPHCilindro != null)
                            itemDetalleDespacho.TipoTramite = "PH";
                        if (t.IdPedido != null)
                            itemDetalleDespacho.TipoTramite = "PEDIDO";

                        itemDetalleDespacho.Taller = t.Talleres.RazonSocialTaller;
                        if (t.IdOblea != null)
                            itemDetalleDespacho.Dominio = t.Obleas.Vehiculos.Descripcion;
                        if (t.IdPHCilindro != null)
                            itemDetalleDespacho.Dominio = t.PHCilindros.PH.Vehiculos.Descripcion;

                        /*  if (t.IdPedido != null)
                            itemDetalleDespacho.Dominio = t.Pedidos.Vehiculos.Descripcion;
                        */
                        itemDetalleDespacho.ZonaTaller = t.Talleres.Zona;
                        itemDetalleDespacho.Estado = "existente";
                        this.ObtenerZonas(t.Talleres.Zona, "agregar");
                        this.CarritoTramites.Add(itemDetalleDespacho);
                    }

                    repeaterZonas.DataSource = this.ZonasTallerViewState;
                    repeaterZonas.DataBind();

                }
                else
                {
                    this.DespachoAModificarID = null;
                    lblTitulo.Text = "Despacho de trámites y mercadería";
                }

                NoTramites.Visible = !this.CarritoTramites.Any();

            }
        }
        protected void btnAgregarTramite_Carrito(object sender, EventArgs e)
        {
            this.ObtenerDetalleADespachar();
            var zonaVaciaDeTramites = this.CarritoTramites.FirstOrDefault(o => o.ZonaTaller == ZonaSeleccionada);

            if (zonaVaciaDeTramites != null)
            {
                this.ObtenerZonas(ZonaSeleccionada, "agregar");

                if (!this.CarritoTramites.Any()) //la pregunta tiene que ser si selecciono un tramite en la zonaseleccionada tmb
                {
                    this.MessageBoxCtrl.MessageBox(null, "Debe seleccionar al menos un trámite para agregar al carrito", UserControls.MessageBoxCtrl.TipoWarning.Warning);
                }
                else
                {
                    repeaterZonas.DataSource = this.ZonasTallerViewState;
                    repeaterZonas.DataBind();
                }
                NoTramites.Visible = !this.CarritoTramites.Any();
            }
            else
            {
                this.MessageBoxCtrl.MessageBox(null, "Debe seleccionar al menos un trámite para agregar al carrito", UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        private void VaciarTablaDespuesDeDespachar()
        {
            NoTramites.Visible = this.CarritoTramites.Any();
            ViewState["CARRITOTRAMITES"] = null;
            ViewState["ZONASTALLERVIEWSTATE"] = null;
            repeaterZonas.DataSource = this.ZonasTallerViewState;
            repeaterZonas.DataBind();
        }
        private void ActualizarNumeroZona()
        {
            lblNorte.Text = $"{this.DespachoLogic.ReadTramitesPendientesByZona("NORTE").Count}";
            lblSur.Text = $"{this.DespachoLogic.ReadTramitesPendientesByZona("SUR").Count}";
            lblEste.Text = $"{this.DespachoLogic.ReadTramitesPendientesByZona("ESTE").Count}";
            lblOeste.Text = $"{this.DespachoLogic.ReadTramitesPendientesByZona("OESTE").Count}";
            lblComisionista.Text = $"{this.DespachoLogic.ReadTramitesPendientesByZona("COMISIONISTA").Count}";
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

            List<String> mensajes = this.VerificarDetalleADespachar();

            if (!mensajes.Any())
            {
                this.CargarZona(zona.ToUpper());
            }
            else
            {
                MessageBoxCtrl.MessageBox("", mensajes, UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
        }
        private void CargarZona(string zona)
        {
            this.ZonaSeleccionada = zona;
            this.Tramites = this.DespachoLogic.ReadTramitesPendientesByZona(zona);

            List<ViewEntity> talleres = this.ObtenerTalleres();

            repeaterDespacho.DataSource = talleres;
            repeaterDespacho.DataBind();
        }
        private List<ZonasTallerView> ObtenerZonas(string ZonaSeleccionada, string accion)
        {
            ZonasTallerView ZonaNorteTaller = new ZonasTallerView();
            ZonasTallerView ZonaSurTaller = new ZonasTallerView();
            ZonasTallerView ZonaEsteTaller = new ZonasTallerView();
            ZonasTallerView ZonaOesteTaller = new ZonasTallerView();
            ZonasTallerView ZonaComisionistaTaller = new ZonasTallerView();

            ZonaNorteTaller.ZonaTaller = "NORTE";
            ZonaSurTaller.ZonaTaller = "SUR";
            ZonaEsteTaller.ZonaTaller = "ESTE";
            ZonaOesteTaller.ZonaTaller = "OESTE";
            ZonaComisionistaTaller.ZonaTaller = "COMISIONISTA";

            var existeZonaEnViewState = this.ZonasTallerViewState.FirstOrDefault(o => o.ZonaTaller == ZonaSeleccionada);

            if (existeZonaEnViewState == null && accion == "agregar")
            {

                if (ZonaSeleccionada == "NORTE")
                    this.ZonasTallerViewState.Add(ZonaNorteTaller);
                if (ZonaSeleccionada == "SUR")
                    this.ZonasTallerViewState.Add(ZonaSurTaller);
                if (ZonaSeleccionada == "ESTE")
                    this.ZonasTallerViewState.Add(ZonaEsteTaller);
                if (ZonaSeleccionada == "OESTE")
                    this.ZonasTallerViewState.Add(ZonaOesteTaller);
                if (ZonaSeleccionada == "COMISIONISTA")
                    this.ZonasTallerViewState.Add(ZonaComisionistaTaller);

            }

            if (existeZonaEnViewState == null && accion == "borrar")
            {
                if (ZonaSeleccionada == "NORTE")
                    this.ZonasTallerViewState.Remove(ZonaNorteTaller);
                if (ZonaSeleccionada == "SUR")
                    this.ZonasTallerViewState.Remove(ZonaSurTaller);
                if (ZonaSeleccionada == "ESTE")
                    this.ZonasTallerViewState.Remove(ZonaEsteTaller);
                if (ZonaSeleccionada == "OESTE")
                    this.ZonasTallerViewState.Remove(ZonaOesteTaller);
                if (ZonaSeleccionada == "COMISIONISTA")
                    this.ZonasTallerViewState.Remove(ZonaComisionistaTaller);
            }
            return this.ZonasTallerViewState;
        }
        protected void repeaterZonas_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnZonaTaller = e.Item.FindControl("hdnZonaTaller") as HiddenField;
                List<DetalleDespachoView> dt = ObtenerTramitesADespacharPorZona(hdnZonaTaller.Value);
                Repeater tablaDatos = e.Item.FindControl("tablaDatos") as Repeater;
                tablaDatos.DataSource = dt;
                tablaDatos.DataBind();

            }

        }
        private List<DetalleDespachoView> ObtenerTramitesADespacharPorZona(string ZonaTaller)
        {
            List<DetalleDespachoView> tramitesPorZona = new List<DetalleDespachoView>();

            int cantTramitesCarritoTramites = this.CarritoTramites.Count();
            int x;

            for (x = 0; x < cantTramitesCarritoTramites; x++)
            {
                if (this.CarritoTramites[x].ZonaTaller == ZonaTaller)
                    tramitesPorZona.Add(this.CarritoTramites[x]);

            }
            return tramitesPorZona;
        }

        private List<ViewEntity> ObtenerTalleres()
        {
            List<ViewEntity> talleres = new List<ViewEntity>();
            foreach (var tramite in this.Tramites.GroupBy(t => t.IdTaller))
            {
                talleres.Add(new ViewEntity(tramite.First().IdTaller, tramite.First().RazonSocialTaller));
            }
            return talleres;
        }

        private List<TramiteDespachoView> ObtenerTramitesPorTaller(Guid tallerID)
        {
            List<DespachoExtendedView> tramitesPorTaller = this.Tramites.Where(t => t.IdTaller == tallerID).ToList();
            List<TramiteDespachoView> tramites = new List<TramiteDespachoView>();

            foreach (var t in tramitesPorTaller)
            {
                TramiteDespachoView tramite = new TramiteDespachoView();
                tramite = t.MapToTramiteDespachoView();
                tramites.Add(tramite);
            }
            return tramites;
        }

        protected void repeaterDespacho_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnTallerID = e.Item.FindControl("hdnTallerID") as HiddenField;
                List<TramiteDespachoView> dt = ObtenerTramitesPorTaller(new Guid(hdnTallerID.Value));
                GridView grdTramites = e.Item.FindControl("grdTramites") as GridView;

                grdTramites.DataSource = dt.OrderBy(t => t.Dominio).ThenBy(t => t.Fecha);
                grdTramites.DataBind();
            }
        }
        protected void BorrarSeleccionadasCarrito(object sender, EventArgs e)
        {

            foreach (RepeaterItem zonas in repeaterZonas.Items)
            {
                Repeater tablaDatos = (Repeater)zonas.FindControl("tablaDatos");
                foreach (RepeaterItem tramiteSeleccionado in tablaDatos.Items)
                {
                    Guid idTramite = new Guid(((HiddenField)tramiteSeleccionado.FindControl("hdnTramiteID")).Value);
                    Boolean borrarTramiteCarrito = ((CheckBox)tramiteSeleccionado.FindControl("tramiteSel")).Checked;
                    String hdnZonaTaller2 = ((HiddenField)tramiteSeleccionado.FindControl("hdnZonaTaller2")).Value;

                    string nombreTaller;
                    if (!borrarTramiteCarrito)
                    {
                        continue;
                    }
                    else
                    {

                        if (this.CarritoTramites.Any())
                        {
                            var oblea = this.CarritoTramites.FirstOrDefault(o => o.IDTramite == idTramite);

                            if (oblea != null)
                            {

                                if (this.DespachoAModificar != null)
                                {
                                    if (this.CarritoTramites.Count() == 1)
                                    {
                                        this.MessageBoxCtrl.MessageBox(null, "El despacho debe tener al menos un trámite", UserControls.MessageBoxCtrl.TipoWarning.Warning);
                                    }
                                    else
                                    {
                                        this.CarritoTramites.Remove(oblea);
                                        this.DespachoAModificar = this.DespachoLogic.Read(this.DespachoAModificarID.Value);
                                        var despachoDetalleABorrar = this.DespachoAModificar;

                                        foreach (var t in despachoDetalleABorrar.DESPACHODETALLE)
                                        {
                                            if (t.IdOblea == idTramite)
                                            {
                                                this.ObleasLogic.CambiarEstado(idTramite, ESTADOSFICHAS.Finalizada, $"Finalizada: {DateTime.Now.ToString()}", this.UsuarioID);
                                                this.DespachoDetalleLogic.Delete(t.ID);

                                                this.CargarZona(ZonaSeleccionada);
                                                this.ActualizarNumeroZona();

                                            }
                                            if (t.IdPHCilindro == idTramite)
                                            {
                                                this.PHCilindrosLogic.CambiarEstado(idTramite, EstadosPH.Finalizada, $"Finalizada: {DateTime.Now.ToString()}", this.UsuarioID);
                                                this.DespachoDetalleLogic.Delete(t.ID);

                                                this.CargarZona(ZonaSeleccionada);
                                                this.ActualizarNumeroZona();

                                            }
                                            /* 
                                             if (t.IdPedido == idTramite)
                                             {
                                                 this.PedidosLogic.CambiarEstado(idTramite, ESTADOSFICHAS.Finalizada, $"Finalizada: {DateTime.Now.ToString()}", this.UsuarioID);
                                                 this.DespachoDetalleLogic.Delete(t.ID);

                                                 this.CargarZona(ZonaSeleccionada);
                                                 this.ActualizarNumeroZona();

                                             }
                                             */
                                        }
                                    }
                                }
                                else
                                {
                                    this.CarritoTramites.Remove(oblea);
                                }
                            }

                            var zonaVaciaTramites = this.ZonasTallerViewState.FirstOrDefault(o => o.ZonaTaller == hdnZonaTaller2);
                            if (zonaVaciaTramites != null)
                            {
                                var existeTramitesZona = this.CarritoTramites.FirstOrDefault(o => o.ZonaTaller == hdnZonaTaller2);
                                if (existeTramitesZona == null)
                                {
                                    this.ZonasTallerViewState.Remove(zonaVaciaTramites);
                                    this.ObtenerZonas(hdnZonaTaller2, "borrar");
                                }

                            }
                        }

                        foreach (RepeaterItem taller in repeaterDespacho.Items)
                        {
                            nombreTaller = ((HiddenField)taller.FindControl("nombreTaller")).Value;
                            GridView grdTramites = (GridView)taller.FindControl("grdTramites");
                            foreach (GridViewRow row in grdTramites.Rows)
                            {
                                DetalleDespachoView itemDetalleDespacho = new DetalleDespachoView();
                                itemDetalleDespacho.IDTramite = (Guid)grdTramites.DataKeys[row.RowIndex].Values["ID"];
                                itemDetalleDespacho.TipoTramite = row.Cells[1].Text;
                                if (idTramite == itemDetalleDespacho.IDTramite)
                                {
                                    if (((CheckBox)row.FindControl("chkDespachar")).Visible)
                                    {
                                        ((CheckBox)row.FindControl("chkDespachar")).Checked = false;
                                        ((CheckBox)row.FindControl("chkDespachar")).Enabled = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            repeaterZonas.DataSource = this.ZonasTallerViewState;
            repeaterZonas.DataBind();
            NoTramites.Visible = !this.CarritoTramites.Any();
        }

        protected void grdTramites_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView grilla = sender as GridView;
                Guid estadoID = (Guid)grilla.DataKeys[e.Row.RowIndex].Values["IdEstado"];

                ObtenerColorEstado(e, estadoID);

                SetearColorFilaGrilla(e, grilla);

                for (int x = 0; x < this.CarritoTramites.Count(); x++)
                {
                    if (this.CarritoTramites[x].IDTramite == (Guid)grilla.DataKeys[e.Row.RowIndex].Values["ID"])
                    {
                        if (((CheckBox)e.Row.FindControl("chkDespachar")).Visible)
                        {
                            ((CheckBox)e.Row.FindControl("chkDespachar")).Checked = true;
                            ((CheckBox)e.Row.FindControl("chkDespachar")).Enabled = false;
                        }
                    }
                }
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
            if (estadoID == ESTADOSFICHAS.Bloqueada || estadoID == EstadosPH.Bloqueada)
            {
                e.Row.Cells[6].CssClass = "estado-rojo";
            }
            else if (estadoID == ESTADOSFICHAS.Finalizada
                      || estadoID == ESTADOSFICHAS.FinalizadaConError
                      || estadoID == EstadosPH.Finalizada
                      //|| estadoID == ESTADOSPEDIDOS.Finalizado
                      )
            {
                e.Row.Cells[6].CssClass = "estado-verde";
                ((CheckBox)e.Row.FindControl("chkDespachar")).Visible = true;

            }
            else
            {
                e.Row.Cells[6].CssClass = "estado-amarillo";
            }
        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            List<String> mensajes = this.VerificarDetalleADespachar();
            if (!mensajes.Any())
            {
                List<DetalleDespachoView> detalleDespacho = this.CarritoTramites;

                if (detalleDespacho.Any())
                {
                    if (this.DespachoAModificar != null)
                    {
                        if (this.DespachoAModificar.IdFlete == CrossCutting.DatosDiscretos.FLETE.RetiroOficina)
                        {
                            radRetiraEnOficina.Checked = true;
                            radFletePropio_CheckedChanged(radRetiraEnOficina, new EventArgs());
                        }
                        else if (this.DespachoAModificar.FLETE.EsFletePropio)
                        {
                            radFletePropio.Checked = true;
                            radFletePropio_CheckedChanged(radFletePropio, new EventArgs());
                        }
                        else
                        {
                            radFleteExterno.Checked = true;
                            radFletePropio_CheckedChanged(radFleteExterno, new EventArgs());
                        }

                        cboFlete.SelectedValue = this.DespachoAModificar.IdFlete.ToString();
                        cboConductor.SelectedValue = this.DespachoAModificar.IdChofer.ToString();
                    }

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupAceptar();", true);


                    this.lblMessage.Text = String.Format("Se despachará(n) {0} trámite(s)", detalleDespacho.Count());

                }
                else
                {
                    this.MessageBoxCtrl.MessageBox(null, "Debe seleccionar al menos un trámite para despachar", UserControls.MessageBoxCtrl.TipoWarning.Warning);
                }
            }
            else
            {
                MessageBoxCtrl.MessageBox(null, mensajes, UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        private List<String> VerificarDetalleADespachar()
        {
            List<String> mensajes = new List<String>();
            foreach (RepeaterItem taller in repeaterDespacho.Items)
            {

                GridView grdTramites = (GridView)taller.FindControl("grdTramites");
                foreach (GridViewRow row in grdTramites.Rows)
                {
                    Boolean despacha = ((CheckBox)row.FindControl("chkDespachar")).Checked;

                    if (!despacha)
                    {
                        continue;
                    }
                    else
                    {
                        DetalleDespachoView itemDetalleDespacho = new DetalleDespachoView();
                        itemDetalleDespacho.IDTramite = (Guid)grdTramites.DataKeys[row.RowIndex].Values["ID"];

                        if (this.CarritoTramites.Any())
                        {
                            bool exsteOblea = this.CarritoTramites.Any(o => o.IDTramite == itemDetalleDespacho.IDTramite);

                            if (!exsteOblea)
                            {
                                mensajes.Add("Hay trámites seleccionados de esta zona que no fueron agregados al carrito");
                            }
                        }
                        else
                        {
                            mensajes.Add("Hay trámites seleccionados de esta zona que no fueron agregados al carrito");
                        }
                    }

                }

            }
            return mensajes;
        }

        private List<DetalleDespachoView> ObtenerDetalleADespachar()
        {
            bool tramiteSeleccionado = false;
            foreach (RepeaterItem taller in repeaterDespacho.Items)
            {
                Guid idTaller = new Guid(((HiddenField)taller.FindControl("hdnTallerID")).Value);
                string nombreTaller = ((HiddenField)taller.FindControl("nombreTaller")).Value;

                GridView grdTramites = (GridView)taller.FindControl("grdTramites");
                foreach (GridViewRow row in grdTramites.Rows)
                {
                    Boolean despacha = ((CheckBox)row.FindControl("chkDespachar")).Checked;

                    if (!despacha) continue;

                    DetalleDespachoView itemDetalleDespacho = new DetalleDespachoView();
                    itemDetalleDespacho.IDTramite = (Guid)grdTramites.DataKeys[row.RowIndex].Values["ID"];
                    itemDetalleDespacho.IdTaller = idTaller;
                    itemDetalleDespacho.TipoTramite = row.Cells[1].Text;
                    itemDetalleDespacho.Taller = nombreTaller;
                    itemDetalleDespacho.Dominio = row.Cells[3].Text;
                    itemDetalleDespacho.ZonaTaller = ZonaSeleccionada;
                    if (this.DespachoAModificarID != null)
                        itemDetalleDespacho.Estado = "agregado";
                    else
                        itemDetalleDespacho.Estado = "existente";


                    if (this.CarritoTramites.Any())
                    {
                        bool exsteOblea = this.CarritoTramites.Any(o => o.IDTramite == itemDetalleDespacho.IDTramite);

                        if (!exsteOblea)
                        {
                            this.CarritoTramites.Add(itemDetalleDespacho);
                            ((CheckBox)row.FindControl("chkDespachar")).Enabled = false;
                            tramiteSeleccionado = true;
                        }

                    }
                    else
                    {
                        this.CarritoTramites.Add(itemDetalleDespacho);
                        ((CheckBox)row.FindControl("chkDespachar")).Enabled = false;
                        tramiteSeleccionado = true;
                    }
                }
            }
            if (tramiteSeleccionado == false && this.CarritoTramites.Any() == false)
            {
                this.MessageBoxCtrl.MessageBox(null, "Debe seleccionar al menos un trámite para agregar al carrito", UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
            return this.CarritoTramites;
        }

        protected void btnGuardar_ServerClick(object sender, EventArgs e)
        {
            bool esModificacion = this.DespachoAModificarID.HasValue;

            var despacho = new DESPACHO();

            if (esModificacion)
                despacho = this.DespachoLogic.Read(this.DespachoAModificarID.Value);

            despacho.ID = esModificacion ? despacho.ID : Guid.NewGuid();
            despacho.Fecha = esModificacion ? despacho.Fecha : DateTime.Now;
            despacho.IdFlete = new Guid(cboFlete.SelectedValue);
            despacho.IdChofer = new Guid(cboConductor.SelectedValue);
            despacho.IdUsuario = this.UsuarioID;

            if (radFletePropio.Checked == true ||
                radFleteExterno.Checked == true ||
                radRetiraEnOficina.Checked == true)
            {
                var detalleTramitesADespachar = ObtenerDetalleADespachar();

                if (!esModificacion)
                {
                    this.DespachoLogic.Add(despacho);

                    foreach (var item in detalleTramitesADespachar)
                    {
                        AgregarTramiteNuevoADespachoDetalle(despacho, item);
                    }

                    this.CargarZona(ZonaSeleccionada);
                    this.VaciarTablaDespuesDeDespachar();
                }
                else
                {

                    if (detalleTramitesADespachar.Any())
                    {
                        this.DespachoLogic.Update(despacho);

                        foreach (var item in detalleTramitesADespachar)
                        {
                            var tramiteExistente = despacho.DESPACHODETALLE.Any(x => x.IdOblea == item.IDTramite || x.IdPHCilindro == item.IDTramite || x.IdPedido == item.IDTramite);
                            if (tramiteExistente) continue;

                            AgregarTramiteNuevoADespachoDetalle(despacho, item);
                        }
                    }
                    else
                    {
                        this.MessageBoxCtrl.MessageBox(null, "Debe seleccionar al menos un trámite para despachar", UserControls.MessageBoxCtrl.TipoWarning.Warning);
                    }
                }

                this.ActualizarNumeroZona();
                this.ImprimirDespacho(despacho.ID);
            }
            else
            {
                this.MessageBoxCtrl.MessageBox(null, "Debe seleccionar tipo de flete", UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }

        }

        private void AgregarTramiteNuevoADespachoDetalle(DESPACHO despacho, DetalleDespachoView item)
        {
            DESPACHODETALLE despachoDetalle = new DESPACHODETALLE();
            despachoDetalle.ID = Guid.NewGuid();
            despachoDetalle.IdDespacho = despacho.ID;
            despachoDetalle.IdTaller = item.IdTaller;

            if (item.TipoTramite.ToUpper() == "OBLEA") despachoDetalle.IdOblea = item.IDTramite;
            if (item.TipoTramite.ToUpper() == "PH") despachoDetalle.IdPHCilindro = item.IDTramite;
            if (item.TipoTramite.ToUpper() == "PEDIDO") despachoDetalle.IdPedido = item.IDTramite;

            this.DespachoDetalleLogic.Add(despachoDetalle);
            if (despachoDetalle.IdOblea.HasValue) this.ObleasLogic.CambiarEstado(despachoDetalle.IdOblea.Value, ESTADOSFICHAS.Despachada, $"Despachada: {DateTime.Now.ToString()}", this.UsuarioID);
            if (despachoDetalle.IdPHCilindro.HasValue) this.PHCilindrosLogic.CambiarEstado(despachoDetalle.IdPHCilindro.Value, EstadosPH.Despachada, $"Despachada: {DateTime.Now.ToString()}", this.UsuarioID);
            //if (despachoDetalle.IdPedido.HasValue) this.PedidosLogic.CambiarEstado(despachoDetalle.IdPedido.Value, ESTADOSPEDIDOS.Despachado, null , this.UsuarioID); 
        }

        private void ImprimirDespacho(Guid despachoID)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupImprimir('" + despachoID + "');", true);
        }

        protected void radFletePropio_CheckedChanged(object sender, EventArgs e)
        {
            var cbo = (RadioButton)sender;

            cboFlete.Visible = true;
            cboConductor.Visible = false;

            if (cbo.ID.ToUpper() != "RADRETIRAENOFICINA")
            {
                cboFlete.Enabled = true;
                cboFlete.LoadData(radFletePropio.Checked);
                cboConductor.Visible = radFletePropio.Checked;
            }
            else
            {
                cboFlete.LoadData();
                List<ViewEntity> newSource = new List<ViewEntity>();
                foreach (ListItem item in cboFlete.Items)
                {
                    if (item.Value == CrossCutting.DatosDiscretos.FLETE.RetiroOficina.ToString())
                        newSource.Add(new ViewEntity(new Guid(item.Value), item.Text));
                }
                cboFlete.DataSource = newSource;
                cboFlete.DataBind();
            }
        }
        #endregion     
    }
}


