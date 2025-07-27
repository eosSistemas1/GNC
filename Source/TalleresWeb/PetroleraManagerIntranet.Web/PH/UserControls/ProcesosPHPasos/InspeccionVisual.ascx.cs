using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PetroleraManagerIntranet.Web.UserControls.ProcesosPHPasos
{
    public partial class InspeccionVisual : uscProcesosPHPasosBase
    {
        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }      

        public override void GuardarAction(Guid usuarioID, bool saltearValidacion)
        {
            try
            {
                if (this.PHCilindros != null)
                {
                    this.PHCilindros.InspeccionVisualCorrecta = inspeccionVisualCorrecta.Checked;
                    this.PHCilindrosLogic.Update(this.PHCilindros);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void DeshabilitarControles()
        {
           
        }

        public override Tuple<bool, string> ValidarAction()
        {
            if (!inspeccionVisualCorrecta.Checked) return ValidarERROR("No coinciden los datos. Se debe observar cilindro.");

            return ValidarOK;
        }
        #endregion
    }
}