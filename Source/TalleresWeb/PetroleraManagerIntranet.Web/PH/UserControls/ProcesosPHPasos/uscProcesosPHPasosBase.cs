using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using TalleresWeb.Entities;
using TalleresWeb.Logic;

namespace PetroleraManagerIntranet.Web.UserControls.ProcesosPHPasos
{
    public abstract class uscProcesosPHPasosBase : System.Web.UI.UserControl
    {
        #region Properties

        public Tuple<bool, string> ValidarOK => new Tuple<bool, string>(true, string.Empty);
        public Tuple<bool, string> ValidarOKConMensaje(string mensaje) => new Tuple<bool, string>(true, mensaje);
        public Tuple<bool, string> ValidarERROR(string mensaje) => new Tuple<bool, string>(false, mensaje);

        private PHCilindrosLogic phCilindrosLogic;
        public PHCilindrosLogic PHCilindrosLogic
        {
            get
            {
                if (phCilindrosLogic == null) phCilindrosLogic = new PHCilindrosLogic();
                return phCilindrosLogic;
            }
        }
        #endregion

        #region Methods
        public PHCilindros PHCilindros
        {
            get
            {
                if (ViewState["PHCILINDRO"] == null) return null;

                return (PHCilindros)ViewState["PHCILINDRO"];
            }
            set 
            {                
                ViewState["PHCILINDRO"] = value; 
            }
        }

        public Tuple<bool, string> Validar()
        {
            try
            {
                DeshabilitarControles();

                return ValidarAction();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public void Guardar(Guid usuarioID, bool saltearValidacion, bool finalizarProceso)
        {
            try
            {         
                GuardarAction(usuarioID, saltearValidacion);

                if(finalizarProceso) 
                    this.PHCilindrosLogic.FinalizarProcesoPH(PHCilindros, usuarioID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public abstract void GuardarAction(Guid usuarioID, bool saltearValidacion = false);

        /// <summary>
        /// Valida el paso correspondiente
        /// </summary>
        /// <returns>devuelve mensaje de error y si puede continuar</returns>
        public abstract Tuple<bool, string> ValidarAction();

        public abstract void DeshabilitarControles();        
        #endregion
    }
}