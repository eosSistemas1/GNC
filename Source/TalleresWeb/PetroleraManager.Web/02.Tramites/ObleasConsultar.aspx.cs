using AjaxControlToolkit;
using CrossCutting.DatosDiscretos;
using PL.Fwk.Presentation.Web.Controls;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;

namespace PetroleraManager.Web.Tramites
{
    public partial class ObleasConsultar : PageBase
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
            if (!Page.IsPostBack)
            {
                this.cboEstadoFicha.IsComboFilter = true;
                this.cboEstadoFicha.LoadData();
                this.cboEstadoFicha.AutomaticLoad = true;

                this.cboEstadoFicha.SelectedValue = ESTADOSFICHAS.PendienteRevision;

                if (Request.QueryString["e"] != null)
                {
                    try
                    {
                        Guid idEstado = Guid.Parse(Request.QueryString["e"].ToString());
                        this.cboEstadoFicha.SelectedValue = idEstado;
                    }
                    catch
                    { }
                }

                updFiltros.Update();

                this.CambiaEstado();
            }
        }

        private void CargarGrilla()
        {
            Guid idEstadoFicha = cboEstadoFicha.SelectedValue;

            DateTime? fechaDesde = String.IsNullOrEmpty(calFechaD.Text) ? default(DateTime?) : DateTime.Parse(calFechaD.Text);
            DateTime? fechaHasta = String.IsNullOrEmpty(calFechaH.Text) ? default(DateTime?) : DateTime.Parse(calFechaH.Text);

            List<ObleasExtendedView> obleas = this.Logic.ReadObleasPorEstado(idEstadoFicha, fechaDesde, fechaHasta);

            int contadorParcial = 0;
            int contador = 0;

            if (obleas.Count > 0)
            {
                String zonaAnterior = null;
                Guid idTallerAnterior = Guid.Empty;
                Table tablaFichas = new Table();
                tablaFichas.Width = Unit.Percentage(100);

                AccordionPane pane = new AccordionPane();

                foreach (ObleasExtendedView o in obleas)
                {
                    if (zonaAnterior != o.Zona)
                    {
                        pane = new AccordionPane();
                        Label lblTituloZona = new Label();
                        lblTituloZona.Text = o.Zona;
                        pane.ID += "z" + contador;
                        pane.HeaderContainer.Controls.Add(lblTituloZona);
                        Accordion1.Panes.Add(pane);
                        tablaFichas = new Table();
                        tablaFichas.Width = Unit.Percentage(100);
                    }

                    if (idTallerAnterior != o.IdTaller)
                    {
                        tablaFichas = new Table();
                        tablaFichas.Width = Unit.Percentage(100);
                        TableRow tr = new TableRow();
                        TableCell tc = new TableCell();
                        Label lblTituloTaller = new Label();
                        lblTituloTaller.Text = o.Taller;
                        tc.Controls.Add(lblTituloTaller);
                        tc.ColumnSpan = 11;                        
                        tr.Cells.Add(tc);
                        tablaFichas.Rows.Add(tr);

                        #region agrego encabezado de tabla

                        TableRow trhead = new TableRow();
                        trhead.CssClass = "GridHeader";
                        TableCell titFecha = new TableCell();
                        TableCell titObleaAnt = new TableCell();
                        TableCell titDominio = new TableCell();
                        TableCell titCliente = new TableCell();
                        TableCell titOperacion = new TableCell();
                        TableCell titBtnObs = new TableCell();
                        TableCell titFechaAlta = new TableCell();
                        TableCell titObleaAsignada = new TableCell();
                        TableCell titFechaVencimiento = new TableCell();
                        TableCell titBtnProcesar = new TableCell();
                        TableCell titBtnEliminar = new TableCell();
                        TableCell titBtnReimprimir = new TableCell();
                        titFecha.Text = "FECHA";
                        titObleaAnt.Text = "OBLEA ANTERIOR";
                        titDominio.Text = "DOMINIO";
                        titCliente.Text = "CLIENTE";
                        titOperacion.Text = "OPERACION";
                        titBtnObs.Text = "OBS.";
                        titFechaAlta.Text = "FECHA ALTA";
                        titObleaAsignada.Text = "OBLEA ASIGNADA";
                        titFechaVencimiento.Text = "VENCIMIENTO";
                        titBtnProcesar.Text = "CONSULTAR";
                        titBtnEliminar.Text = "ELIMINAR";
                        titBtnReimprimir.Text = "REIMPRIMIR";
                        trhead.Cells.Add(titFecha);
                        trhead.Cells.Add(titObleaAnt);
                        trhead.Cells.Add(titDominio);
                        trhead.Cells.Add(titCliente);
                        trhead.Cells.Add(titOperacion);
                        trhead.Cells.Add(titBtnObs);
                        trhead.Cells.Add(titFechaAlta);
                        trhead.Cells.Add(titObleaAsignada);
                        trhead.Cells.Add(titFechaVencimiento);
                        trhead.Cells.Add(titBtnProcesar);
                        trhead.Cells.Add(titBtnEliminar);
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
                    TableCell tdDominio = new TableCell();
                    TableCell tdCliente = new TableCell();
                    TableCell tdOperacion = new TableCell();
                    TableCell tdBtnObs = new TableCell();
                    TableCell tdFechaAlta = new TableCell();
                    TableCell tdObleaAsignada = new TableCell();
                    TableCell tdFechaVencimiento = new TableCell();
                    TableCell tdBtnProcesar = new TableCell();
                    TableCell tdBtnEliminar = new TableCell();
                    TableCell tdBtnReimprimir = new TableCell();

                    #region agrego detalle

                    PLLabel lblFecha = new PLLabel();
                    lblFecha.Text = o.FechaHabilitacion.ToShortDateString();
                    tdFecha.HorizontalAlign = HorizontalAlign.Center;
                    tdFecha.Controls.Add(lblFecha);

                    PLLabel lblObleaAnt = new PLLabel();
                    lblObleaAnt.Text = o.Descripcion;
                    tdObleaAnt.HorizontalAlign = HorizontalAlign.Center;
                    tdObleaAnt.Controls.Add(lblObleaAnt);

                    PLLabel lblDominio = new PLLabel();
                    lblDominio.Text = o.Dominio.ToUpper();
                    tdDominio.HorizontalAlign = HorizontalAlign.Center;
                    tdDominio.Controls.Add(lblDominio);

                    PLLabel lblCliente = new PLLabel();
                    lblCliente.Text = o.NombreyApellido.ToUpper();
                    tdCliente.HorizontalAlign = HorizontalAlign.Left;
                    tdCliente.Controls.Add(lblCliente);

                    PLLabel lblOperacion = new PLLabel();
                    lblOperacion.Text = o.Operacion;
                    tdOperacion.HorizontalAlign = HorizontalAlign.Center;
                    tdOperacion.Controls.Add(lblOperacion);

                    #endregion

                    #region botones detalle

                    if (!String.IsNullOrEmpty(o.Observacion))
                    {
                        String titulo = String.Format("Dominio: {0}", o.Dominio.ToUpper());
                        String texto = String.Format("Taller:{0}  Observación:{1}", o.Taller, o.Observacion);
                        PLImageButton btnObservacion = new PLImageButton();
                        btnObservacion.ImageUrl = SiteMaster.UrlBase + @"Imagenes/Iconos/observaciones.png";
                        btnObservacion.OnClientClick = "alert('" + titulo + "', '" + texto + "'); return false;";
                        tdBtnObs.HorizontalAlign = HorizontalAlign.Center;
                        tdBtnObs.Controls.Add(btnObservacion);
                    }

                    PLLabel lblFechaAlta = new PLLabel();
                    lblFechaAlta.Text = o.FechaAlta.ToString("dd/MM/yyyy hh:mm");
                    tdFechaAlta.HorizontalAlign = HorizontalAlign.Center;
                    tdFechaAlta.Controls.Add(lblFechaAlta);

                    PLLabel lblObleaAsignada = new PLLabel();
                    lblObleaAsignada.Text = o.NroObleaNueva;
                    tdObleaAsignada.HorizontalAlign = HorizontalAlign.Center;
                    tdObleaAsignada.Controls.Add(lblObleaAsignada);

                    PLLabel lblFechaVencimiento = new PLLabel();
                    lblFechaVencimiento.Text = o.FechaVencimiento.HasValue? o.FechaVencimiento.Value.ToString("MM/yyyy") : String.Empty;
                    tdFechaVencimiento.HorizontalAlign = HorizontalAlign.Center;
                    tdFechaVencimiento.Controls.Add(lblFechaVencimiento);

                    #region boton procesamiento

                    PLImageButton btnProcesamiento = new PLImageButton();
                    btnProcesamiento.ImageUrl = SiteMaster.UrlBase + @"Imagenes/Iconos/modificar.png";
                    //String urlProcesamiento = SiteMaster.UrlBase + @"02.Tramites/ProcesarOblea.aspx?id=" + o.ID;
                    String urlProcesamiento = SiteMaster.UrlBase + @"02.Tramites/ObleasIngresar.aspx?id=" + o.ID + "&e=" + o.IdEstadoFicha;
                    btnProcesamiento.PostBackUrl = urlProcesamiento;
                    tdBtnProcesar.HorizontalAlign = HorizontalAlign.Center;
                    tdBtnProcesar.Controls.Add(btnProcesamiento);

                    #endregion

                    #region boton eliminar

                    PLImageButton btnEliminar = new PLImageButton();
                    btnEliminar.ImageUrl = SiteMaster.UrlBase + @"Imagenes/Iconos/eliminar.png";
                    String urlEliminar = SiteMaster.UrlBase + @"02.Tramites/EliminarOblea.aspx?id=" + o.ID;
                    btnEliminar.PostBackUrl = urlEliminar;

                    btnEliminar.Visible = o.IdEstadoFicha != ESTADOSFICHAS.Eliminada
                                          && o.IdEstadoFicha != ESTADOSFICHAS.Asignada
                                          && o.IdEstadoFicha != ESTADOSFICHAS.AsignadaConError
                                          && o.IdEstadoFicha != ESTADOSFICHAS.Finalizada
                                          && o.IdEstadoFicha != ESTADOSFICHAS.Entregada
                                          && o.IdEstadoFicha != ESTADOSFICHAS.Despachada
                                          && o.IdEstadoFicha != ESTADOSFICHAS.Informada
                                          && o.IdEstadoFicha != ESTADOSFICHAS.InformadaConError;

                    tdBtnEliminar.HorizontalAlign = HorizontalAlign.Center;
                    tdBtnEliminar.Controls.Add(btnEliminar);

                    #endregion

                    #region boton reimprimir

                    PLImageButton btnReimprimir = new PLImageButton();
                    btnReimprimir.ImageUrl = SiteMaster.UrlBase + @"Imagenes/Iconos/imprimir.png";
                    String urlReimprimir = SiteMaster.UrlBase + @"02.Tramites/ObleasImprimir.aspx?id=" + o.ID;
                    btnReimprimir.OnClientClick = "window.open('" + urlReimprimir + "', '_blank', 'height=800,width=600,status=no'); return false;";
                    tdBtnReimprimir.HorizontalAlign = HorizontalAlign.Center;
                    tdBtnReimprimir.Controls.Add(btnReimprimir);

                    #endregion

                    #endregion

                    trD.Cells.Add(tdFecha);
                    trD.Cells.Add(tdObleaAnt);
                    trD.Cells.Add(tdDominio);
                    trD.Cells.Add(tdCliente);
                    trD.Cells.Add(tdOperacion);
                    trD.Cells.Add(tdBtnObs);
                    trD.Cells.Add(tdFechaAlta);
                    trD.Cells.Add(tdObleaAsignada);
                    trD.Cells.Add(tdFechaVencimiento);
                    trD.Cells.Add(tdBtnProcesar);
                    trD.Cells.Add(tdBtnEliminar);
                    trD.Cells.Add(tdBtnReimprimir);
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

            lblTituloPagina.Text = String.Format("Consultar Fichas Técnicas: ({0})", contador);

            this.updFiltros.Update();
        }

        #endregion

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {
            if (this.cboEstadoFicha.SelectedValue == ESTADOSFICHAS.Finalizada
                || this.cboEstadoFicha.SelectedValue == ESTADOSFICHAS.Entregada
                || this.cboEstadoFicha.SelectedValue == ESTADOSFICHAS.Asignada
                || this.cboEstadoFicha.SelectedValue == ESTADOSFICHAS.AsignadaConError
                || this.cboEstadoFicha.SelectedValue == Guid.Empty)
            {
                if (String.IsNullOrEmpty(calFechaD.Text) ||
                    String.IsNullOrEmpty(calFechaH.Text))
                {
                    MessageBoxCtrl1.MessageBox(null, "Debe ingresar las fechas <br>", UserControls.MessageBoxCtrl.TipoWarning.Warning);
                    updFiltros.Update();
                    return;
                }
                else
                {
                    DateTime desde = DateTime.Parse(calFechaD.Text);
                    DateTime hasta = DateTime.Parse(calFechaH.Text);

                    if ((hasta - desde).TotalDays > 31)
                    {
                        MessageBoxCtrl1.MessageBox(null, "Las fechas ingresadas no pueden superar el mes<br>", UserControls.MessageBoxCtrl.TipoWarning.Warning);
                        updFiltros.Update();
                        return;
                    }
                }
            }

            this.CargarGrilla();
        }

        protected void cboEstadoFicha_OnSelectedIndexChange(object sender, EventArgs e)
        {
            this.CambiaEstado();
        }

        private void CambiaEstado()
        {
            lblTituloPagina.Text = String.Format("Consultar Fichas Técnicas: ({0})", 0);

            if (this.cboEstadoFicha.SelectedValue == ESTADOSFICHAS.Finalizada
                || this.cboEstadoFicha.SelectedValue == ESTADOSFICHAS.Entregada
                || this.cboEstadoFicha.SelectedValue == ESTADOSFICHAS.Asignada
                || this.cboEstadoFicha.SelectedValue == ESTADOSFICHAS.AsignadaConError
                || this.cboEstadoFicha.SelectedValue == Guid.Empty)
            {
                calFechaD.Visible = true;
                calFechaH.Visible = true;
                lnkBuscar.Visible = true;
            }
            else
            {
                calFechaD.Visible = false;
                calFechaH.Visible = false;
                lnkBuscar.Visible = false;

                this.CargarGrilla();
            }
        }
    }
}