using System;
using System.Collections.Generic;
using System.Web.UI;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.PH
{
    public partial class AsignarPH : PageBase
    {
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
            grdPH.DataSource = null;
            grdPH.DataBind();

            List<PHCilindrosInformarView> ph = logic.ReadPHParaAsignar();

            if (ph.Count > 0)
            {
                grdPH.DataSource = ph;
                grdPH.DataBind();
                lblMensaje.Visible = false;                
            }
            else
            {                
                lblMensaje.Visible = true;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}