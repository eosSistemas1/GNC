using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;
using System.Drawing;
using TalleresWeb.Entities;

namespace TalleresWeb.Web.TalleresWeb.Consultas
{
    public partial class ConsultaEnBase : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.Page.Title = "Sección Talleristas - Consulta en Base";
            }
        }

        protected void lnkOblea_Click(object sender, EventArgs e)
        {
            MuestroTabla(tdOblea);
            txtIdOblea.Focus();
        }
        protected void lnkCliente_Click(object sender, EventArgs e)
        {
            MuestroTabla(tdCliente);
            txtNroDoc.Focus();
        }
        protected void lnkDominio_Click(object sender, EventArgs e)
        {
            MuestroTabla(tdDominio);
            txtDomino.Focus();
        }
        protected void lnkRegulador_Click(object sender, EventArgs e)
        {
            MuestroTabla(tdRegulador);
            txtSerieReg.Focus();
        }
        protected void lnkCilindro_Click(object sender, EventArgs e)
        {
            MuestroTabla(tdCilindro);
            txtSerieCil.Focus();
        }
        protected void lnkValvula_Click(object sender, EventArgs e)
        {
            MuestroTabla(tdValvula);
            txtSerieVal.Focus();
        }

        private void MuestroTabla(System.Web.UI.HtmlControls.HtmlTableRow tr)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            HabilitarControles(true);

            tdOblea.Visible = false;
            tdCliente.Visible = false;
            tdDominio.Visible = false;
            tdRegulador.Visible = false;
            tdCilindro.Visible = false;
            tdValvula.Visible = false;

            tr.Visible = true;
            tr.Focus();

            Session["MUESTRACLIENTE"] = tdCliente.Visible;

            btnNuevaBusq.Visible = false;
            btnAceptar.Visible = true;
            trMensaje.Visible = false;
        }

        protected void txt_TextChanged(object sender, EventArgs e)
        {
            String msjValidar = String.Empty;
            Page.Validate();
            if (Page.IsValid)
            {
                btnAceptar_Click(this, new EventArgs());
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            String msjValidar = String.Empty;
            Page.Validate();
            if (Page.IsValid)
            {
                Aceptar();
                HabilitarControles(false);
                btnNuevaBusq.Visible = true;
                btnAceptar.Visible = false;
            }
        }


        protected void Aceptar()
        {

            #region Validaciones
            //if (tdOblea.Visible && txtIdOblea.Text == String.Empty) { msjValidar = "Ingrese Oblea."; txtIdOblea.Focus(); }
            //if (tdCliente.Visible && txtNroDoc.Text == String.Empty) { msjValidar = "Ingrese Nro Documento."; txtNroDoc.Focus(); }
            //if (tdDominio.Visible && (validarDominio(txtDomino.Text))) 
            //{ 
            //    msjValidar = "El formato del dominio ingresado no es correcto."; txtDomino.Focus(); 
            //}

            //if ((tdRegulador.Visible) && (txtSerieReg.Text == String.Empty)) { msjValidar = "Ingrese nro de serie a buscar."; txtSerieReg.Focus(); }
            //if ((tdCilindro.Visible) && (txtSerieCil.Text == String.Empty)) { msjValidar = "Ingrese nro de serie a buscar."; txtSerieReg.Focus(); }
            //if ((tdValvula.Visible) && (txtSerieVal.Text == String.Empty)) { msjValidar = "Ingrese nro de serie a buscar."; txtSerieReg.Focus(); }
            #endregion

            Guid idTaller = MasterTalleres.IdTaller;
            ObleasLogic logic = new ObleasLogic();

            ObleasParameters param = new ObleasParameters();
            List<ObleasExtendedView> objConsulta = new List<ObleasExtendedView>();

            if (tdOblea.Visible)
            {
                param.NroObleaNueva = txtIdOblea.Text;
                objConsulta = logic.ReadAllConsultaEnBase(param);
            }
            if (tdCliente.Visible) {
                param.TipoDocClienteID = new Guid(cboTipoDoc.SelectedValue.ToString());
                param.NroDocCliente = txtNroDoc.Text;
                objConsulta = logic.ReadAllConsultaEnBase(param);
            }

            if (tdRegulador.Visible)
            {
                param.MarcaRegID = cboMarcaReg.SelectedValue;
                param.SerieReg =  txtSerieReg.Text;
                ObleasReguladoresLogic regLogic = new ObleasReguladoresLogic(); 
                objConsulta = regLogic.ReadAllConsultaEnBase(param);
            }

            if (tdCilindro.Visible)
            {
                param.MarcaCilID = cboMarcaCil.SelectedValue;
                param.SerieCil = txtSerieCil.Text;
                ObleasCilindrosLogic regLogic = new ObleasCilindrosLogic();
                objConsulta = regLogic.ReadAllConsultaEnBase(param);
            }

            if (tdValvula.Visible)
            {
                param.MarcaValID = cboMarcaVal.SelectedValue;
                param.SerieVal = txtSerieVal.Text;
                ObleasValvulasLogic regLogic = new ObleasValvulasLogic();
                objConsulta = regLogic.ReadAllConsultaEnBase(param);
            }

            if (tdDominio.Visible)
            {
                param.Dominio = txtDomino.Text;
                objConsulta = logic.ReadAllConsultaEnBase(param);
            }

            if (objConsulta.Count > 0)
            {
                GridView1.DataSource = objConsulta;
                GridView1.DataBind();
                trMensaje.Visible = false;
                tdResultados.Visible = true;
            }
            else
            {
                lblMsjSinResultados.ForeColor = Color.Red;
                lblMsjSinResultados.Text = "No se encontraron resultados.";
                trMensaje.Visible = true;
                tdResultados.Visible = false;
            }
        }

        protected void btnNuevaBusq_Click(object sender, EventArgs e)
        {
            trMensaje.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
            btnAceptar.Visible = true;
            btnNuevaBusq.Visible = false;
            HabilitarControles(true);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer))
            {
                e.Row.Cells[0].Text = DateTime.Parse(e.Row.Cells[0].Text).ToShortDateString();

                String idoblea = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
                ImageButton btn = (ImageButton)e.Row.Cells[5].FindControl("btnDetalle");
                if (idoblea != String.Empty)
                {
                    btn.OnClientClick = "javascript:popup('DetalleConsulta.aspx?idoblea=" + idoblea + "');";
                }
                else
                {
                    btn.Enabled = false;
                }
            }
        }

        protected void HabilitarControles(Boolean habilitar)
        {
            txtIdOblea.Enabled = habilitar;

            cboTipoDoc.Enabled = habilitar;
            txtNroDoc.Enabled = habilitar;

            txtDomino.Enabled = habilitar;

            cboMarcaReg.Enabled = habilitar;
            txtSerieReg.Enabled = habilitar;
            cboMarcaCil.Enabled = habilitar;
            txtSerieCil.Enabled = habilitar;
            cboMarcaVal.Enabled = habilitar;
            txtSerieVal.Enabled = habilitar;

            if (habilitar)
            {
                txtIdOblea.Text = String.Empty;
                txtNroDoc.Text = String.Empty;
                txtDomino.Text = String.Empty;
                txtSerieReg.Text = String.Empty;
                txtSerieCil.Text = String.Empty;
                txtSerieVal.Text = String.Empty;
            }

        }
    }
}