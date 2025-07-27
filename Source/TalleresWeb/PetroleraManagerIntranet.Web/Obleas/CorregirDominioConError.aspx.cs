using CrossCutting.DatosDiscretos;
using PetroleraManagerIntranet.Web.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.Obleas
{
    public partial class CorregirDominioConError : PageBase
    {
        #region Properties
        private ObleasLogic obleasLogic;        
        private ObleasLogic ObleasLogic
        {
            get
            {
                if (obleasLogic == null)
                    obleasLogic = new ObleasLogic();

                return obleasLogic;
            }
        }

        private VehiculosLogic vehiculosLogic;
        private VehiculosLogic VehiculosLogic
        {
            get
            {
                if (vehiculosLogic == null)
                    vehiculosLogic = new VehiculosLogic();

                return vehiculosLogic;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                calFechaD.Value = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                calFechaH.Value = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            List<string> mensajes = Validar();

            if (!mensajes.Any())
            {
                var fechaDesde = DateTime.Parse(calFechaD.Value);
                var fechaHasta = DateTime.Parse(calFechaH.Value);


                List<ObleasParaCorregirDominioView> obleasACorregir =
                                    ObleasLogic.ReadFichaParaCorregirDominio(fechaDesde, fechaHasta, txtNroOblea.Text, txtDominioError.Text);

                grdObleas.DataSource = obleasACorregir;
            }
            else
            {
                grdObleas.DataSource = null;
                MessageBoxCtrl1.MessageBox(null, mensajes, MessageBoxCtrl.TipoWarning.Warning);
            }

            grdObleas.DataBind();
        }

        private List<string> Validar()
        {
            List<string> mensajes = new List<string>();
            if (!string.IsNullOrWhiteSpace(calFechaD.Value) || !string.IsNullOrWhiteSpace(calFechaH.Value))
            {
                if (string.IsNullOrWhiteSpace(calFechaD.Value)) mensajes.Add("- Debe ingresar fecha desde.");
                if (string.IsNullOrWhiteSpace(calFechaH.Value)) mensajes.Add("- Debe ingresar fecha hasta.");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtNroOblea.Text) && string.IsNullOrWhiteSpace(txtDominioError.Text))
                {
                    mensajes.Add("- Debe ingresar al menos un filtro.");
                }
            }
            return mensajes;
        }

        protected void grdObleas_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = e.Row.Cells[0].Text == GetDinamyc.MinDatetime.ToString("dd/MM/yyyy") ? String.Empty : e.Row.Cells[0].Text;

                var btnModificar = e.Row.Cells[5].FindControl("btnCorregir");
                btnModificar.Visible = !string.IsNullOrWhiteSpace(e.Row.Cells[4].Text);
            }
        }

        protected void grdObleas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                #region Corregir error
                if (e.CommandName == "modificar")
                {
                    Guid vehiculoId = Guid.Parse(e.CommandArgument.ToString());
                    var row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    var dominioOK = row.Cells[4].Text.Trim();

                    try
                    {
                        VehiculosLogic.CorregirDominio(vehiculoId, dominioOK);

                        CargarDatos();

                        MessageBoxCtrl1.MessageBox(null, "El dominio se modificó correctamente.", MessageBoxCtrl.TipoWarning.Success);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxCtrl1.MessageBox(null, ex.Message, MessageBoxCtrl.TipoWarning.Error);
                    }

                    
                }
                #endregion
            }

        }
    }
}