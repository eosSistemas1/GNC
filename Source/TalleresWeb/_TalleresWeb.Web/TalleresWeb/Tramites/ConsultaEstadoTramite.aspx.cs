using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;
using TalleresWeb.Entities;
using CrossCutting.DatosDiscretos;
using Common.Web.UserControls;

namespace TalleresWeb.Web
{
    public partial class ConsultaEstadoTramite : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!User.Identity.IsAuthenticated) Response.Redirect("~/login.aspx");
            if (!Page.IsPostBack)
            {
                grdAnteriores.HeaderStyle.CssClass = "GridHeader";
                grdAnteriores.RowStyle.CssClass = "GridRow";
                grdAnteriores.AlternatingRowStyle.CssClass = "GridAlternateRow";
                grdAyer.HeaderStyle.CssClass = "GridHeader";
                grdAyer.RowStyle.CssClass = "GridRow";
                grdAyer.AlternatingRowStyle.CssClass = "GridAlternateRow";
                grdHoy.HeaderStyle.CssClass = "GridHeader";
                grdHoy.RowStyle.CssClass = "GridRow";
                grdHoy.AlternatingRowStyle.CssClass = "GridAlternateRow";

                this.Page.Title = "Sección Talleristas - Consulta Estado Trámites";

                CargarGrilla(grdAnteriores, DateTime.Parse("01/01/1980"), DateTime.Now.AddDays(-2));
                CargarGrilla(grdAyer, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
                CargarGrilla(grdHoy, DateTime.Now.Date, DateTime.Now.Date);

                if (grdAnteriores.Rows.Count < 0) paneAnteriores.Enabled = false;
                if (grdAyer.Rows.Count < 0) paneAyer.Enabled = false;
                if (grdHoy.Rows.Count < 0) paneAnteriores.Enabled = false;

                MyAccordion.SelectedIndex = MyAccordion.Panes.IndexOf(paneHoy);
            }
        }

        private void CargarGrilla(GridView grd, DateTime fechaDesde, DateTime fechaHasta)
        {
            Guid idTaller = MasterTalleres.IdTaller;

            if (idTaller != null)
            {
                ObleasParameters param = new ObleasParameters();
                param.FechaDesde = fechaDesde;
                param.FechaHasta = fechaHasta;
                param.TallerID = idTaller;

                ObleasLogic oLogic = new ObleasLogic();
                var obleas = oLogic.ReadTramitesSinFinalizar(param); 

                grd.DataSource = obleas;
                grd.DataBind();

                CargarTitulos();
            }
            updatePanel.Update();
        }

        private void CargarTitulos()
        {
            lblTituloAnteriores.Text = "TRÁMITES ANTERIORES (" + grdAnteriores.Rows.Count + ")";
            lblTituloAyer.Text = "TRÁMITES AYER (" + grdAyer.Rows.Count + ")";
            lblTituloHoy.Text = "TRÁMITES HOY (" + grdHoy.Rows.Count + ")";

            lblTituloAnteriores.Attributes.Add("onmouseover", "this.style.cursor='hand';");
            lblTituloAyer.Attributes.Add("onmouseover", "this.style.cursor='hand';");
            lblTituloHoy.Attributes.Add("onmouseover", "this.style.cursor='hand';");

        }

        /*uso los siguientes eventos para las 3 grillas */
        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer))
            {
                //cargar los datos q tiene q ver.
                try
                {
                    e.Row.Cells[0].Text = DateTime.Parse(e.Row.Cells[0].Text).ToString("dd/MM/yyyy");
                }
                catch
                {
                    e.Row.Cells[0].Text = String.Empty;
                }

                GridView grd = (GridView)sender;
                ImageButton btnObs = (ImageButton)e.Row.Cells[5].Controls[1];
                String obs = grd.DataKeys[e.Row.RowIndex].Values["Observacion"] != null ? grd.DataKeys[e.Row.RowIndex].Values["Observacion"].ToString() : String.Empty;
                if (obs == String.Empty)
                {
                    btnObs.ToolTip = "Observaciones";
                    btnObs.Visible = false;
                }
                Image img = new Image();
                img.ImageAlign = ImageAlign.AbsMiddle;
                img.Height = Unit.Pixel(20);
                img.Width = Unit.Pixel(20);
                String urlbase = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + '/';

                Guid estado = new Guid(grd.DataKeys[e.Row.RowIndex].Values["IdEstadoFicha"].ToString());
                if (estado == EstadosFicha.Bloqueado)
                {
                    img.ImageUrl = urlbase + "Images/Iconos/Bloqueada.png";
                    img.AlternateText = "Bloqueada";
                    img.ToolTip = "Bloqueada";
                }
                else if (estado == EstadosFicha.PendienteRevision)
                {
                    img.ImageUrl = urlbase + "Images/Iconos/PendienteRevision.png";
                    img.AlternateText = "Pendiente de Revisión";
                    img.ToolTip = "Pendiente de Revisión";
                }
                else
                {
                    img.ImageUrl = urlbase + "Images/Iconos/Correcta.png";
                    img.AlternateText = "Correcto / En entrega";
                    img.ToolTip = "Correcto / En entrega";
                }


                e.Row.Cells[9].Controls.Add(img);

                String idoblea = grd.DataKeys[e.Row.RowIndex].Values["ID"].ToString();
                ImageButton btn = (ImageButton)e.Row.Cells[5].FindControl("btnModifica");
                btn.Visible = false;

                #region modificar
                if (idoblea != String.Empty)
                {
                    if ((estado == EstadosFicha.PendienteRevision) || (estado == EstadosFicha.Bloqueado))
                    {
                        btn.ToolTip = "Modificar";
                        btn.OnClientClick = "window.location = 'ModificaFicha.aspx?idoblea=" + idoblea + "';";
                        btn.Visible = true;
                    }
                }
                #endregion
     
                #region Reimprimir
                ImageButton btnReimprimir = (ImageButton)e.Row.Cells[6].FindControl("btnReimprimir");
                if (idoblea != String.Empty)
                {
                    btnReimprimir.ToolTip = "Reimprimir";
                    String script = "window.open('ImprimirOblea.aspx?id =" + idoblea + "','Oblea');";
                    btnReimprimir.OnClientClick = script;
                }
                else
                {
                    btnReimprimir.Enabled = false;
                }
                #endregion

            }
        }
        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView grd = (GridView)sender;
            if (e.CommandName == "eliminar")
            {
                Guid idOblea = new Guid(grd.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());
                ObleasLogic objObleas = new ObleasLogic();
                var oblea = objObleas.Read(idOblea);
                oblea.IdEstadoFicha = EstadosFicha.Eliminada;
                oblea.MotivoBajaFicha = "Baja:" + DateTime.Now + " Usuario:" + Context.User.Identity.Name.ToString();
                objObleas.Update(oblea);

                String mensaje = "La oblea fue eliminada.";
                MessageBoxCtrl1.MessageBox(null, mensaje, "ConsultaEstadoTramite.aspx", MessageBoxCtrl.TipoWarning.Warning);

            }

            if (e.CommandName == "observaciones")
            {
                lblObs.Text = e.CommandArgument.ToString();
                ModalPopupExtender1.Show();
            }

            updatePanel.Update();
        }
    }
}