using CrossCutting.DatosDiscretos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;

namespace PetroleraManager.Web.Tramites
{
    public partial class ObleasCorreccionErrores : PageBase
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
                this.CargarObleas();
            }
        }

        private void CargarObleas()
        {
            List<ObleasExtendedView> obleas = this.Logic.ReadObleasPorEstado(ESTADOSFICHAS.AsignadaConError, GetDinamyc.MinDatetime, GetDinamyc.MaxDatetime);

            grd.DataSource = obleas;
            grd.DataBind();
        }

        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
        {            
            GridView grd = (GridView)sender;
            Guid obleaID = new Guid(grd.DataKeys[int.Parse(e.CommandArgument.ToString())].Values["ID"].ToString());

            GridViewRow fila = grd.Rows[int.Parse(e.CommandArgument.ToString())];

            if (e.CommandName == "seleccionar")
            {
                hdnObleaSeleccionadaID.Value = obleaID.ToString();
                lblTituloMsj.Text = String.Format("Corrección:  Oblea:{0} - Operación:{1}", grd.Rows[int.Parse(e.CommandArgument.ToString())].Cells[0].Text, grd.Rows[int.Parse(e.CommandArgument.ToString())].Cells[4].Text);
                modalExt.Show();
            }
                      
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}