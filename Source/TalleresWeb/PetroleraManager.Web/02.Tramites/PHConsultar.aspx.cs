using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using System.Data;
using AjaxControlToolkit;
using DatosDiscretos;
using System.Drawing;

namespace PetroleraManager.Web.Tramites
{
    public partial class PHConsultar : PageBase
    {
        //ESTADOSFICHAS estado = new ESTADOSFICHAS();
        PHCilindrosLogic logic = new PHCilindrosLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            PHCILINDROSParameters param = new PHCILINDROSParameters();
            param.EstadoFichaPendiente = DatosDiscretos.ESTADOSPH.Ingresada;
            param.EstadoFichaBloqueada = DatosDiscretos.ESTADOSPH.Bloqueada;
            var obleas = logic.ReadPendientes(param);

            int contadorParcial = 0;
            int contador = 0;

            if (obleas.Count > 0)
            {
                String zonaAnterior = String.Empty;
                Guid idTallerAnterior = Guid.Empty;
                String DominioAnterior = String.Empty;
                Color ColorDomimioRow = ColorTranslator.FromHtml("#E9E9E9");

                Table tablaFichas = new Table();
                tablaFichas.Width = Unit.Percentage(100);

                //TableRow trEspacio = new TableRow();
                //TableCell tcEspacio = new TableCell();
                //tcEspacio.ColumnSpan = 9;
                //tcEspacio.Height = Unit.Pixel(10);

                AccordionPane pane = new AccordionPane();

                foreach (PHCILINDROSExtendedView o in obleas)
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
                        tc.ColumnSpan = 9;
                        //tr.Cells.Add(tcEspacio);
                        tr.Cells.Add(tc);
                        tablaFichas.Rows.Add(tr);

                        #region agrego encabezado de tabla

                        TableRow trhead = new TableRow();
                        trhead.CssClass = "GridHeader";
                        TableCell titFecha = new TableCell();
                        TableCell titObleaAnt = new TableCell();
                        TableCell titDominio = new TableCell();
                        TableCell titCliente = new TableCell();
                        TableCell titTelefono = new TableCell();
                        TableCell titSerieCil = new TableCell();
                        TableCell titFechaAlta = new TableCell();
                        TableCell titBtnProcesar = new TableCell();
                        TableCell titBtnEliminar = new TableCell();
                        TableCell titBtnReimprimir = new TableCell();
                        titFecha.Text = "FECHA PH";
                        titObleaAnt.Text = "OBLEA";
                        titDominio.Text = "DOMINIO";
                        titCliente.Text = "CLIENTE";
                        titTelefono.Text = "TELEFONO";
                        titSerieCil.Text = "SERIE CIL";
                        //titFechaAlta.Text = "FECHA ALTA";
                        titBtnProcesar.Text = "CONSULTAR";
                        titBtnEliminar.Text = "ELIMINAR";
                        titBtnReimprimir.Text = "REIMPRIMIR";
                        trhead.Cells.Add(titFecha);
                        trhead.Cells.Add(titObleaAnt);
                        trhead.Cells.Add(titDominio);
                        trhead.Cells.Add(titCliente);
                        trhead.Cells.Add(titTelefono);
                        trhead.Cells.Add(titSerieCil);
                        //trhead.Cells.Add(titFechaAlta);
                        trhead.Cells.Add(titBtnProcesar);
                        trhead.Cells.Add(titBtnEliminar);
                        trhead.Cells.Add(titBtnReimprimir);
                        tablaFichas.Rows.Add(trhead);
                        #endregion

                        pane.ContentContainer.Controls.Add(tablaFichas);
                        contadorParcial = 0;
                    }

                    if (o.Dominio.ToUpper().Trim() != DominioAnterior.ToUpper().Trim())
                    {
                        ColorDomimioRow = ColorDomimioRow == ColorTranslator.FromHtml("#FFFFFF") ?
                            ColorTranslator.FromHtml("#E9E9E9") : ColorTranslator.FromHtml("#FFFFFF");
                    }

                    TableRow trD = new TableRow();
                    trD.CssClass = "GridRow";
                    trD.BackColor = ColorDomimioRow;
                    TableCell tdFecha = new TableCell();
                    TableCell tdObleaAnt = new TableCell();
                    TableCell tdDominio = new TableCell();
                    TableCell tdCliente = new TableCell();
                    TableCell tdTelefono = new TableCell();
                    TableCell tdSerieCil = new TableCell();
                    //TableCell tdFechaAlta = new TableCell();
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

                    PLLabel lblTelefono = new PLLabel();
                    lblTelefono.Text = o.Telefono.Replace(".00", String.Empty);
                    tdTelefono.HorizontalAlign = HorizontalAlign.Center;
                    tdTelefono.Controls.Add(lblTelefono);

                    PLLabel lblSerieCil = new PLLabel();
                    lblSerieCil.Text = o.SerieCil;
                    tdSerieCil.HorizontalAlign = HorizontalAlign.Center;
                    tdSerieCil.Controls.Add(lblSerieCil);
                    #endregion

                    #region botones detalle

                    #region boton procesamiento
                    PLImageButton btnProcesamiento = new PLImageButton();
                    btnProcesamiento.ImageUrl = SiteMaster.UrlBase + @"Imagenes/Iconos/modificar.png";
                    String urlProcesamiento = SiteMaster.UrlBase + @"02.Tramites/PHIngresar.aspx?idPH=" + o.IdPH + "&e=" + o.IdEstadoPH + "&idCP=" + o.ID;
                    btnProcesamiento.PostBackUrl = urlProcesamiento;
                    tdBtnProcesar.HorizontalAlign = HorizontalAlign.Center;
                    tdBtnProcesar.Controls.Add(btnProcesamiento);
                    #endregion

                    #region boton eliminar
                    PLImageButton btnEliminar = new PLImageButton();
                    btnEliminar.ImageUrl = SiteMaster.UrlBase + @"Imagenes/Iconos/eliminar.png";
                    String urlEliminar = SiteMaster.UrlBase + @"02.Tramites/EliminarPH.aspx?id=" + o.ID;
                    btnEliminar.PostBackUrl = urlEliminar;
                    tdBtnEliminar.HorizontalAlign = HorizontalAlign.Center;
                    tdBtnEliminar.Controls.Add(btnEliminar);
                    #endregion

                    #region boton reimprimir
                    PLImageButton btnReimprimir = new PLImageButton();
                    btnReimprimir.ImageUrl = SiteMaster.UrlBase + @"Imagenes/Iconos/imprimir.png";
                    String urlReimprimir = SiteMaster.UrlBase + @"02.Tramites/PHImprimirCarta.aspx?id=" + o.ID;
                    btnReimprimir.OnClientClick = "window.open('" + urlReimprimir + "', '_blank', 'height=800,width=600,status=no'); return false;";
                    tdBtnReimprimir.HorizontalAlign = HorizontalAlign.Center;
                    tdBtnReimprimir.Controls.Add(btnReimprimir);
                    #endregion

                    #endregion

                    trD.Cells.Add(tdFecha);
                    trD.Cells.Add(tdObleaAnt);
                    trD.Cells.Add(tdDominio);
                    trD.Cells.Add(tdCliente);
                    trD.Cells.Add(tdTelefono);
                    trD.Cells.Add(tdSerieCil);
                    //trD.Cells.Add(tdFechaAlta);
                    trD.Cells.Add(tdBtnProcesar);
                    trD.Cells.Add(tdBtnEliminar);
                    trD.Cells.Add(tdBtnReimprimir);
                    tablaFichas.Rows.Add(trD);

                    DominioAnterior = o.Dominio.ToUpper().Trim();

                    contadorParcial++;
                    contador++;
                    zonaAnterior = o.Zona;
                    idTallerAnterior = o.IdTaller;
                }

                lblTituloPagina.Text += "  (" + contador + ")";

                lblMensaje.Visible = false;

            }
            else
            {
                lblMensaje.Visible = true;
            }
        }
    }
}