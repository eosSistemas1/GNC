using CrossCutting.DatosDiscretos;
using System;
using System.Linq;
using TalleresWeb.Logic;

namespace PetroleraManagerIntranet.Web.UserControls.ProcesosPHPasos
{
    public partial class InspeccionRoscas : uscProcesosPHPasosBase
    {
        #region Properties
        private InspeccionesPHLogic inspeccionesPHLogic;
        private InspeccionesPHLogic InspeccionesPHLogic
        {
            get
            {
                if (this.inspeccionesPHLogic == null) this.inspeccionesPHLogic = new InspeccionesPHLogic();
                return this.inspeccionesPHLogic;
            }
        }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.uscInspecciones.CargarInspecciones(INSPECCIONTIPO.ROSCA);

                if (this.PHCilindros != null && this.PHCilindros.InspeccionesPH != null)
                {
                    this.uscInspecciones.SetearInspeccionesSeleccionadas(this.PHCilindros.InspeccionesPH);
                }
            }
        }

        public override void GuardarAction(Guid usuarioID, bool saltearValidacion)
        {
            try
            {
                this.InspeccionesPHLogic.SaveInspecciones(this.uscInspecciones.InspeccionesSeleccionadas, this.PHCilindros.ID);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void DeshabilitarControles()
        {
            uscInspecciones.Enabled = false;
        }

        public override Tuple<bool, string> ValidarAction()
        {           
            if (this.uscInspecciones.InspeccionesSeleccionadas.Any(i => i.ValorInspeccion))
                return ValidarERROR("Observar cilindro");

            return ValidarOK;
        }
        #endregion
    }
}