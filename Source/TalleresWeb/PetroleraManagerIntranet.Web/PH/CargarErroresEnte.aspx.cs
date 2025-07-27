using CrossCutting.DatosDiscretos;
using System;
using System.IO;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.PH
{
    public partial class CargarErroresEnte : PageBase
    {
        #region Properties
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
        protected void Page_Load(object sender, EventArgs e)
        {          
        }

        protected void btnAceptar_ServerClick(object sender, EventArgs e)
        {
            if (this.fuArchivoErrores.HasFile)
            {
                //Proceso Archivo Errores
                Int32 fichasError = this.ProcesarArchivoErrores();

                String mensaje = String.Format("Se actualizaron {1} obleas Con Error.", fichasError, fichasError);
                this.MessageBoxCtrl1.MessageBox(null, mensaje, Web.UserControls.MessageBoxCtrl.TipoWarning.Success);                    
            }
            else
            {
                this.MessageBoxCtrl1.MessageBox(null, "Debe ingresar al menos un archivo", Web.UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }

        }

        protected void btnCancelar_ServerClick(object sender, EventArgs e)
        {
        }

        private Int32 ProcesarArchivoErrores()
        {
            Int32 cantidadFichasActualizadas = 0;

            if (this.fuArchivoErrores.HasFile)
            {
                StreamReader reader = new StreamReader(this.fuArchivoErrores.FileContent);
                do
                {
                    String textLine = reader.ReadLine();

                    if (textLine != null)
                    {
                        Int32 ficha = this.ParsearYActualizarObleaErronea(textLine);
                        cantidadFichasActualizadas += ficha;
                    }

                } while (reader.Peek() != -1);
                reader.Close();
            }

            return cantidadFichasActualizadas;
        }

        private Int32 ParsearYActualizarObleaErronea(String textLine)
        {
            String[] fila = textLine.Split(':');

            PHErroresEnteView ph = new PHErroresEnteView();
            ph.MatriculaCRPC = fila[1].Split(' ')[0].ToString();
            ph.NroRevision = fila[2].Trim().Split(' ')[0].ToString();
            ph.DescripcionError = fila[2].Trim().ToString();

            var pHCilindros = PHCilindrosLogic.ReadByNroOperacionCRPC(int.Parse(ph.NroRevision), EstadosPH.Informada);

            if (pHCilindros != null)
            {
                PHCilindrosLogic.CambiarEstado(pHCilindros.ID, EstadosPH.Informada, ph.DescripcionError, this.UsuarioID);
                return 1;
            }

            return 0;
        }

        #endregion      
    }
}