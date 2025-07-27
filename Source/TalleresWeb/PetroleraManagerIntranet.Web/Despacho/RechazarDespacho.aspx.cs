using CrossCutting.DatosDiscretos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;


namespace PetroleraManagerIntranet.Web.Despacho
{
    public partial class RechazarDespacho : PageBase
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

        private DESPACHO DespachoAModificar
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

        private DespachoDetalleLogic despachoDetalleLogic;
        private DespachoDetalleLogic DespachoDetalleLogic
        {
            get
            {
                if (despachoDetalleLogic == null) despachoDetalleLogic = new DespachoDetalleLogic();
                return despachoDetalleLogic;
            }
        }
        #endregion


        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    var idDespacho = new Guid(Request.QueryString["id"].ToString());
                    this.DespachoAModificar = this.DespachoLogic.Read(idDespacho);
                    lblTitulo.Text = $"Rechazar despacho Nro: {DespachoAModificar.Numero}";

                    List<ZonasTallerView> zonas = this.ObtenerZonas();
                    repeaterZonas.DataSource = zonas;
                    repeaterZonas.DataBind();
                }
            }
        }

        private List<ZonasTallerView> ObtenerZonas()
        {
            List<ZonasTallerView> zonasTalleres = new List<ZonasTallerView>();
            var zonas = DespachoAModificar.DESPACHODETALLE.GroupBy(x => x.Talleres.Zona).ToList();
            foreach (var item in zonas)
            {
                zonasTalleres.Add(new ZonasTallerView() { ZonaTaller = item.Key });
            }
            return zonasTalleres;
        }

        protected void btnRechazarTramiteCarrito_ServerClick(object sender, EventArgs e)
        {
            var tramitesARechazar = ObtenerIDTramitesARechazar();

            foreach (var despachoDetalleID in tramitesARechazar)
            {
                RechazarItemDespacho(this.UsuarioID, despachoDetalleID);
            }

            MessageBoxCtrl.MessageBox(null, "Se rechazaron los tramites correctamente.", "InicioFinDespacho.aspx", UserControls.MessageBoxCtrl.TipoWarning.Success);
        }

        private List<Guid> ObtenerIDTramitesARechazar()
        {
            List<Guid> idTramitesARechazar = new List<Guid>();

            foreach (RepeaterItem zona in repeaterZonas.Items)
            {
                var grdZona = ((GridView)zona.FindControl("grdZona"));

                foreach (GridViewRow tramite in grdZona.Rows)
                {
                    CheckBox chkSeleccionado = ((CheckBox)tramite.FindControl("chkSeleccionado"));
                    if (chkSeleccionado.Checked)
                    {
                        Guid id = new Guid(grdZona.DataKeys[tramite.RowIndex].Values["ID"].ToString());
                        idTramitesARechazar.Add(id);
                    }
                }
            }

            return idTramitesARechazar;
        }

        private void RechazarItemDespacho(Guid idUsuario, Guid idDetalle)
        {
            var numero = this.DespachoAModificar.Numero;
            var despachoDetalle = this.DespachoAModificar.DESPACHODETALLE.Single(d => d.ID == idDetalle);

            if (despachoDetalle.IdOblea.HasValue) this.ObleasLogic.CambiarEstado(despachoDetalle.IdOblea.Value, ESTADOSFICHAS.Finalizada, $"Rechazo despacho nro {numero}", idUsuario);
            if (despachoDetalle.IdPHCilindro.HasValue) this.PHCilindrosLogic.CambiarEstado(despachoDetalle.IdPHCilindro.Value, EstadosPH.Finalizada, $"Rechazo despacho nro {numero}", idUsuario);
            //TODO: habilitar para despachos
            //if (item.IdPedido.HasValue) this.PedidosLogic.CambiarEstado(item.IdPedido.Value, ESTADOSPEDIDOS.Finalizada, null , entity.IdUsuario); 

            this.DespachoDetalleLogic.Delete(despachoDetalle.ID);
        }

        protected void repeaterZonas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var zona = ((HiddenField)e.Item.FindControl("hdnZonaTaller")).Value;

            var tramitesZona = this.DespachoAModificar.DESPACHODETALLE.Where(d => d.Talleres.Zona == zona).OrderBy(a => a.Talleres.Descripcion);

            List<DetalleDespachoView> detalleDespachoView = new List<DetalleDespachoView>();
            foreach (var tramite in tramitesZona)
            {
                var tipoTramite = tramite.IdOblea.HasValue ? "OBLEA" :
                                    tramite.IdPHCilindro.HasValue ? "PH" :
                                    tramite.IdPedido.HasValue ? "PEDIDO" : string.Empty;
                var dominio = tramite.IdOblea.HasValue ? tramite.Obleas.Vehiculos.Descripcion :
                                    tramite.IdPHCilindro.HasValue ? tramite.PHCilindros.PH.Vehiculos.Descripcion : string.Empty;

                detalleDespachoView.Add(new DetalleDespachoView()
                {
                    ID = tramite.ID,
                    TipoTramite = tipoTramite,
                    Dominio = dominio,
                    Taller = tramite.Talleres.RazonSocialTaller
                });
            }


            var grdZona = ((GridView)e.Item.FindControl("grdZona"));
            grdZona.DataSource = detalleDespachoView;
            grdZona.DataBind();
        }

        protected void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            var chkTodos = (CheckBox)sender;
            var grid = (GridView)chkTodos.Parent.Parent.Parent.Parent;
            foreach (GridViewRow row in grid.Rows)
            {
                var chkSeleccionado = (CheckBox)row.FindControl("chkSeleccionado");
                if (chkSeleccionado != null)
                    chkSeleccionado.Checked = chkTodos.Checked;
            }
        }
        #endregion        
    }
}