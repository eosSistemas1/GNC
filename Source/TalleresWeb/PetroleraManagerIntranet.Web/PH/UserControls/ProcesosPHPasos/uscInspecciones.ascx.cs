using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;

namespace PetroleraManagerIntranet.Web.UserControls.ProcesosPHPasos
{
    public partial class uscInspecciones : System.Web.UI.UserControl
    {
        #region Properties
        private InspeccionesLogic inspeccionesLogic;
        private InspeccionesLogic InspeccionesLogic
        {
            get
            {
                if (this.inspeccionesLogic == null) this.inspeccionesLogic = new InspeccionesLogic();
                return this.inspeccionesLogic;
            }
        }

        public void SetearInspeccionesSeleccionadas(EntityCollection<InspeccionesPH> inspeccionesPH)
        {
            if (!inspeccionesPH.Any()) return;

            foreach (GridViewRow row in grdInspecciones.Rows)
            {
                Guid ID = new Guid(grdInspecciones.DataKeys[row.RowIndex].Values["ID"].ToString());

                var inspeccion = inspeccionesPH.FirstOrDefault(i => i.IdInspeccion == ID);

                if (inspeccion != null)
                {
                    ((CheckBox)row.FindControl("chkInspeccion")).Checked = inspeccion.ValorInspeccion.Value;
                    ((TextBox)row.FindControl("txtObservacion")).Text = inspeccion.ObservacionesInspeccion;
                }
            }
        }

        public List<InspeccionesView> Inspecciones
        {
            get
            {
                List<InspeccionesView> valor = new List<InspeccionesView>();
                foreach (GridViewRow row in grdInspecciones.Rows)
                {
                    valor.Add(new InspeccionesView()
                    {
                        ID = new Guid(grdInspecciones.DataKeys[row.RowIndex].Values["ID"].ToString()),
                        Descripcion = row.Cells[0].Text,
                        ValorInspeccion = ((CheckBox)row.FindControl("chkInspeccion")).Checked,
                        Observacion = ((TextBox)row.FindControl("txtObservacion")).Text
                    });
                }
                return valor;
            }
            set
            {
                grdInspecciones.DataSource = value;
                grdInspecciones.DataBind();
            }
        }

        public List<InspeccionesView> InspeccionesSeleccionadas
        {
            get
            {                
                return this.Inspecciones;
            }
        }

        public bool Enabled 
        {
            get { return grdInspecciones.Enabled; }
            set { grdInspecciones.Enabled = value; } 
        }

        #endregion

        #region Methods
        public void CargarInspecciones(Guid tipoInspeccionID)
        {
            this.Inspecciones = InspeccionesView.GetFromViewEntity(InspeccionesLogic.ReadInspeccionesPorTipo(tipoInspeccionID));
        }
        #endregion
    }
}