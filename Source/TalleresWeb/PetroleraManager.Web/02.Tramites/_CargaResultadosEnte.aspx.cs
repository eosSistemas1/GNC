using System;
using System.IO;
using TalleresWeb.Entities;
using TalleresWeb.Logic;

namespace PetroleraManager.Web.Tramites
{
    public partial class CargaResultadosEnte : PageBase
    {
        #region Members

        private ObleasLogic obleasLogic;

        #endregion

        #region Properties

        public ObleasLogic ObleasLogic
        {
            get
            {
                if (this.obleasLogic == null)
                    this.obleasLogic = new ObleasLogic();

                return this.obleasLogic;
            }
        }

        private Int32 TotalFichasProcesadas
        {
            get
            {
                if (ViewState["TOTALFICHASPROCESADAS"] == null
                     || String.IsNullOrEmpty(ViewState["TOTALFICHASPROCESADAS"].ToString())) ViewState["TOTALFICHASPROCESADAS"] = 0;

                return Int32.Parse(ViewState["TOTALFICHASPROCESADAS"].ToString());
            }
            set
            {
                if (ViewState["TOTALFICHASPROCESADAS"] == null
                     || String.IsNullOrEmpty(ViewState["TOTALFICHASPROCESADAS"].ToString())) ViewState["TOTALFICHASPROCESADAS"] = 0;

                ViewState["TOTALFICHASPROCESADAS"] = Int32.Parse(ViewState["TOTALFICHASPROCESADAS"].ToString()) + value;
            }
        }

        #endregion

        #region Methods

        protected void lnkAceptar_Click(object sender, EventArgs e)
        {
            if (this.fuArchivoOK.HasFile || this.fuArchivoErrores.HasFile)
            {
                //genero el informe
                Guid informeID = ObleasLogic.CrearInforme();

                //Proceso Archivo OK
                Int32 fichasOK = this.ProcesarArchivoOK(informeID);

                //Proceso Archivo Errores
                Int32 fichasError = this.ProcesarArchivoErrores(informeID);

                ObleasLogic.ActualizarInforme(informeID, this.TotalFichasProcesadas, fichasOK, fichasError);

                String mensaje = String.Format("Se actualizaron {0} obleas OK. <br>  Se actualizaron {1} obleas Con Error.", fichasOK, fichasError);
                this.MessageBoxCtrl.MessageBox(null, mensaje, UserControls.MessageBoxCtrl.TipoWarning.Info);
            }
            else
            {
                this.MessageBoxCtrl.MessageBox(null, "Debe ingresar al menos un archivo", PetroleraManager.Web.UserControls.MessageBoxCtrl.TipoWarning.Success);
            }
        }

        protected void lnkCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private Int32 ParsearYActualizarObleaErronea(String textLine, Guid informeID)
        {
            String[] fila = textLine.Split(';');

            ObleaCargaResultadosView oblea = new ObleaCargaResultadosView();
            oblea.PEC = fila[0].ToString();
            oblea.CUIT = fila[1].ToString();
            oblea.NroInternoObleaTaller = fila[2].ToString();
            oblea.CodigoTaller = fila[3].Split(' ')[0].ToString();

            oblea.DescripcionError = fila[3].Remove(0, 8).ToString();

            return this.ObleasLogic.ActualizarObleaErrorAsignada(oblea, informeID);
        }

        private Int32 ParsearYActualizarObleaOK(String textLine, Guid informeID)
        {
            String[] fila = textLine.Split(';');

            ObleaCargaResultadosView oblea = new ObleaCargaResultadosView();
            oblea.PEC = fila[0].ToString();
            oblea.CUIT = fila[1].ToString();
            oblea.CodigoTaller = fila[2].ToString();
            oblea.NroInternoObleaTaller = fila[3].ToString();
            oblea.NroObleaAsignada = fila[4].ToString();
            oblea.Dominio = fila[5].ToString();

            return this.ObleasLogic.ActualizarObleaAsignada(oblea, informeID);
        }

        private Int32 ProcesarArchivoErrores(Guid informeID)
        {
            Int32 cantidadFichasActualizadas = 0;

            if (this.fuArchivoErrores.HasFile)
            {
                StreamReader reader = new StreamReader(this.fuArchivoErrores.FileContent);
                do
                {
                    String textLine = reader.ReadLine();

                    Int32 ficha = this.ParsearYActualizarObleaErronea(textLine, informeID);
                    cantidadFichasActualizadas += ficha;

                    TotalFichasProcesadas = TotalFichasProcesadas + 1;

                } while (reader.Peek() != -1);
                reader.Close();
            }

            return cantidadFichasActualizadas;
        }

        private Int32 ProcesarArchivoOK(Guid informeID)
        {
            Int32 cantidadFichasActualizadas = 0;

            if (this.fuArchivoOK.HasFile)
            {
                StreamReader reader = new StreamReader(this.fuArchivoOK.FileContent);
                Boolean primeraLinea = true;
                do
                {
                    if (primeraLinea)
                    {
                        reader.ReadLine();
                        primeraLinea = false;
                        continue;
                    }

                    String textLine = reader.ReadLine();

                    Int32 ficha = this.ParsearYActualizarObleaOK(textLine, informeID);
                    cantidadFichasActualizadas += ficha;

                    TotalFichasProcesadas = TotalFichasProcesadas + 1;

                } while (reader.Peek() != -1);
                reader.Close();
            }

            return cantidadFichasActualizadas;
        }

        #endregion
    }
}