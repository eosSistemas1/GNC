using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.PH.ControlCodigos
{
    public partial class Index : PageBase
    {
        private PHCilindrosLogic phCilindrosLogic;
        public PHCilindrosLogic PHCilindrosLogic
        {
            get
            {
                if (this.phCilindrosLogic == null) this.phCilindrosLogic = new PHCilindrosLogic();
                return this.phCilindrosLogic;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [System.Web.Services.WebMethod]
        public static string CalcularCantidades()
        {
            PHCilindrosLogic phCilindrosLogic = new PHCilindrosLogic();
            String cantidaddes = phCilindrosLogic.LeerPHActualmenteEnProceso();

            var cilindrosParaEvaluar = phCilindrosLogic.ReadCilindrosPHParaEvaluarValvulas().Count.ToString();
            var cilindrosParaVerificarCodigos = phCilindrosLogic.ReadCilindrosPHParaVerificarCodigos().Count.ToString();

            cantidaddes = $"{cantidaddes}|{cilindrosParaEvaluar}|{cilindrosParaVerificarCodigos}";

            return cantidaddes;
        }

    }
}