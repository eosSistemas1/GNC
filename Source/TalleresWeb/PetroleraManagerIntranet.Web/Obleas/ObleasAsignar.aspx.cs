using AjaxControlToolkit;
using CrossCutting.DatosDiscretos;
using PetroleraManagerIntranet.Web.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Obleas
{
    public partial class ObleasAsignar : PageBase
    {
        #region Members

        private ObleasLogic _logic;

        #endregion

        #region Properties

        public ObleasLogic Logic
        {
            get
            {
                if (this._logic == null) this._logic = new ObleasLogic();
                return this._logic;
            }
            set { _logic = value; }
        }

        #endregion

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.cboEstadoFicha.SelectedIndex = 0;
                this.CargarGrilla();

                chkSelected.Value = "";
                updFiltros.Update();



            }
        }

        private void CargarGrilla()
        {
            Guid idEstadoFicha = !String.IsNullOrWhiteSpace(cboEstadoFicha.SelectedValue) ? new Guid(cboEstadoFicha.SelectedValue) : Guid.Empty;

            //TODO: Mejorar!!
            List<ObleasExtendedView> obleas = this.Logic.ReadObleasPorEstado(idEstadoFicha, default(DateTime?), default(DateTime?));

            if (idEstadoFicha == Guid.Empty)
            {
                obleas = obleas.Where(o => o.IdEstadoFicha == ESTADOSFICHAS.Asignada ||
                                           o.IdEstadoFicha == ESTADOSFICHAS.AsignadaConError ||
                                           o.IdEstadoFicha == ESTADOSFICHAS.RechazadaPorEnte).ToList();
            }

            int contadorParcial = 0;
            int contador = 0;

            if (obleas.Any())
            {
                String zonaAnterior = null;
                Guid idTallerAnterior = Guid.Empty;
                Table tablaFichas = new Table();
                tablaFichas.Width = Unit.Percentage(100);

                AccordionPane pane = new AccordionPane();
                pane.ClientIDMode = ClientIDMode.Static;
                pane.ID = "panel";


                foreach (ObleasExtendedView o in obleas)
                {
                    System.Drawing.Color color = System.Drawing.Color.LightGreen;

                    if (o.IdEstadoFicha == ESTADOSFICHAS.AsignadaConError) color = System.Drawing.Color.LightYellow;
                    if (o.IdEstadoFicha == ESTADOSFICHAS.RechazadaPorEnte) color = System.Drawing.Color.Coral;

                    if (zonaAnterior != o.Zona)
                    {
                        pane = new AccordionPane();
                        LinkButton lblTituloZona = new LinkButton();
                        lblTituloZona.PostBackUrl = "#";
                        lblTituloZona.CssClass = "panel-title";
                        lblTituloZona.Text = o.Zona;
                        pane.ID += "z" + contador;
                        pane.HeaderCssClass = "panel-heading";
                        pane.HeaderContainer.Controls.Add(lblTituloZona);
                        Accordion1.Panes.Add(pane);
                        tablaFichas = new Table();
                        tablaFichas.Width = Unit.Percentage(100);
                    }

                    if (idTallerAnterior != o.IdTaller)
                    {
                        tablaFichas = new Table();
                        tablaFichas.CssClass = "table table-bordered table-hover";
                        tablaFichas.Width = Unit.Percentage(100);
                        TableRow tr = new TableRow();
                        TableCell tc = new TableCell();
                        Label lblTituloTaller = new Label();
                        lblTituloTaller.Text = o.Taller;
                        tc.Controls.Add(lblTituloTaller);
                        tc.ColumnSpan = 10;
                        tr.Cells.Add(tc);
                        tablaFichas.Rows.Add(tr);

                        #region agrego encabezado de tabla

                        TableRow trhead = new TableRow();
                        trhead.CssClass = "GridHeader";
                        TableCell titFecha = new TableCell();
                        TableCell titObleaAnt = new TableCell();
                        TableCell titObleaNueva = new TableCell();
                        TableCell titDominio = new TableCell();
                        TableCell titCliente = new TableCell();
                        TableCell titOperacion = new TableCell();
                        TableCell titBtnObs = new TableCell();
                        TableCell titFechaAlta = new TableCell();
                        TableCell titBtnProcesar = new TableCell();
                        TableCell titBtnReimprimir = new TableCell();
                        titFecha.Text = "FECHA";
                        titObleaAnt.Text = "OBLEA ANTERIOR";
                        titObleaNueva.Text = "OBLEA NUEVA";
                        titDominio.Text = "DOMINIO";
                        titCliente.Text = "CLIENTE";
                        titOperacion.Text = "OPERACION";
                        titBtnObs.Text = "OBS.";
                        titFechaAlta.Text = "FECHA ALTA";
                        titBtnProcesar.Text = "CONSULTAR";
                        titBtnReimprimir.Text = "IMPRIMIR OBLEA";
                        trhead.Cells.Add(titFecha);
                        trhead.Cells.Add(titObleaAnt);
                        trhead.Cells.Add(titObleaNueva);
                        trhead.Cells.Add(titDominio);
                        trhead.Cells.Add(titCliente);
                        trhead.Cells.Add(titOperacion);
                        trhead.Cells.Add(titBtnObs);
                        trhead.Cells.Add(titFechaAlta);
                        trhead.Cells.Add(titBtnProcesar);
                        trhead.Cells.Add(titBtnReimprimir);
                        tablaFichas.Rows.Add(trhead);

                        #endregion

                        pane.ContentContainer.Controls.Add(tablaFichas);
                        contadorParcial = 0;
                    }

                    TableRow trD = new TableRow();
                    trD.CssClass = o.IdEstadoFicha == ESTADOSFICHAS.AprobadaConError ? "GridRowRed" : "GridRow";
                    TableCell tdFecha = new TableCell();
                    TableCell tdObleaAnt = new TableCell();
                    TableCell tdObleaNueva = new TableCell();
                    TableCell tdDominio = new TableCell();
                    TableCell tdCliente = new TableCell();
                    TableCell tdOperacion = new TableCell();
                    TableCell tdBtnObs = new TableCell();
                    TableCell tdFechaAlta = new TableCell();
                    TableCell tdBtnProcesar = new TableCell();
                    TableCell tdBtnImprimirOblea = new TableCell();

                    #region agrego detalle

                    Label lblFecha = new Label();
                    lblFecha.Text = o.FechaHabilitacion.ToShortDateString();
                    tdFecha.HorizontalAlign = HorizontalAlign.Center;
                    tdFecha.Controls.Add(lblFecha);

                    Label lblObleaAnt = new Label();
                    lblObleaAnt.Text = o.NroObleaAnterior;
                    tdObleaAnt.HorizontalAlign = HorizontalAlign.Center;
                    tdObleaAnt.Controls.Add(lblObleaAnt);

                    Label lblObleaNueva = new Label();
                    lblObleaNueva.Text = o.NroObleaNueva;
                    tdObleaNueva.HorizontalAlign = HorizontalAlign.Center;
                    tdObleaNueva.Controls.Add(lblObleaNueva);

                    Label lblDominio = new Label();
                    lblDominio.Text = o.Dominio.ToUpper();
                    tdDominio.HorizontalAlign = HorizontalAlign.Center;
                    tdDominio.Controls.Add(lblDominio);

                    Label lblCliente = new Label();
                    lblCliente.Text = o.NombreyApellido.ToUpper();
                    tdCliente.HorizontalAlign = HorizontalAlign.Left;
                    tdCliente.Controls.Add(lblCliente);

                    Label lblOperacion = new Label();
                    lblOperacion.Text = o.Operacion;
                    tdOperacion.HorizontalAlign = HorizontalAlign.Center;
                    tdOperacion.Controls.Add(lblOperacion);

                    #endregion

                    #region botones detalle

                    if (!String.IsNullOrEmpty(o.Observacion))
                    {
                        String titulo = String.Format("Dominio: {0}", o.Dominio.ToUpper());
                        String texto = String.Format("Taller:{0}  Observación:{1}", o.Taller, o.Observacion);
                        ImageButton btnObservacion = new ImageButton();
                        btnObservacion.ImageUrl = SiteMaster.UrlBase + @"img/Iconos/observaciones.png";
                        btnObservacion.OnClientClick = "alert('" + titulo + "', '" + texto + "'); return false;";
                        tdBtnObs.HorizontalAlign = HorizontalAlign.Center;
                        tdBtnObs.Controls.Add(btnObservacion);
                    }

                    Label lblFechaAlta = new Label();
                    lblFechaAlta.Text = o.FechaAlta.ToString("dd/MM/yyyy hh:mm");
                    tdFechaAlta.HorizontalAlign = HorizontalAlign.Center;
                    tdFechaAlta.Controls.Add(lblFechaAlta);

                    #region boton procesamiento

                    ImageButton btnProcesamiento = new ImageButton();
                    btnProcesamiento.ImageUrl = SiteMaster.UrlBase + @"img/Iconos/modificar.png";
                    String urlProcesamiento = SiteMaster.UrlBase + @"Obleas/ObleasIngresar.aspx?id=" + o.ID + "&e=" + o.IdEstadoFicha;
                    btnProcesamiento.PostBackUrl = urlProcesamiento;
                    tdBtnProcesar.HorizontalAlign = HorizontalAlign.Center;
                    tdBtnProcesar.Controls.Add(btnProcesamiento);

                    #endregion

                    #region check seleccionado                    
                    CheckBox chkImprimir = new CheckBox();
                    chkImprimir.ClientIDMode = ClientIDMode.Static;
                    chkImprimir.ID = String.Format("chkSelected{0}", o.ID.ToString());
                    chkImprimir.Visible = o.IdEstadoFicha != ESTADOSFICHAS.RechazadaPorEnte;
                    tdBtnImprimirOblea.HorizontalAlign = HorizontalAlign.Center;
                    tdBtnImprimirOblea.Controls.Add(chkImprimir);
                    #endregion

                    #endregion

                    trD.Cells.Add(tdFecha);
                    trD.Cells.Add(tdObleaAnt);
                    trD.Cells.Add(tdObleaNueva);
                    trD.Cells.Add(tdDominio);
                    trD.Cells.Add(tdCliente);
                    trD.Cells.Add(tdOperacion);
                    trD.Cells.Add(tdBtnObs);
                    trD.Cells.Add(tdFechaAlta);
                    trD.Cells.Add(tdBtnProcesar);
                    trD.Cells.Add(tdBtnImprimirOblea);

                    trD.BackColor = color;
                    tablaFichas.Rows.Add(trD);

                    contadorParcial++;
                    contador++;
                    zonaAnterior = o.Zona;
                    idTallerAnterior = o.IdTaller;
                }

                lblMensaje.Visible = false;
            }
            else
            {
                lblMensaje.Visible = true;
            }

            lblTituloPagina.Text = String.Format("RESUMEN DE IMPORTACIÓN -  FICHAS TÉCNICAS: ({0})", contador);

            this.updFiltros.Update();
        }

        #endregion

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {
            chkSelected.Value = String.Empty;

            this.CargarGrilla();
        }

        protected void cboEstadoFichaAsignar_OnSelectedIndexChange(object sender, EventArgs e)
        {
            this.CambiaEstado();
        }

        private void CambiaEstado()
        {
            lblTituloPagina.Text = String.Format("RESUMEN DE IMPORTACIÓN -  FICHAS TÉCNICAS: ({0})", 0);

            this.CargarGrilla();
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            List<Guid> seleccionadas = new List<Guid>();

            var chkSeleccionados = chkSelected.Value.Split('|');

            if (!String.IsNullOrWhiteSpace(chkSelected.Value))
            {
                foreach (String item in chkSeleccionados)
                {
                    if (String.IsNullOrWhiteSpace(item)) continue;

                    Guid id = Guid.Parse(item.Replace("chkSelected", String.Empty));
                    seleccionadas.Add(id);

                    var oblea = this.Logic.Read(id);

                    if (oblea.IdEstadoFicha == ESTADOSFICHAS.Asignada)
                    {
                        this.Logic.CambiarEstado(id, ESTADOSFICHAS.Finalizada, String.Format("Finalizada: {0} - Usuario: {1}", DateTime.Now, this.UsuarioID), this.UsuarioID);
                    }
                    else if (oblea.IdEstadoFicha == ESTADOSFICHAS.AsignadaConError)
                    {
                        this.Logic.CambiarEstado(id, ESTADOSFICHAS.FinalizadaConError, String.Format("Finalizada con error: {0} - Usuario: {1}", DateTime.Now, this.UsuarioID), this.UsuarioID);
                    }
                }

                Session["OBLEASSELECCIONADASIMPRIMIR"] = seleccionadas;
                chkSelected.Value = "";

                PrintBoxCtrl.PrintBox("Imprimir", "../Obleas/ObleasImprimirTarjetaVerde.aspx", "", "../Obleas/ObleasAsignar.aspx");


            }
            else
            {
                MessageBoxCtrl1.MessageBox("Imprmir", "Debe seleccionar al menos una ficha para imprimir", MessageBoxCtrl.TipoWarning.Warning);
            }
        }
    }
}