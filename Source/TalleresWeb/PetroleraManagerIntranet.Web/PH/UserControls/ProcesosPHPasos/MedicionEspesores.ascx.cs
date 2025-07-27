using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PetroleraManagerIntranet.Web.UserControls.ProcesosPHPasos
{
    public partial class MedicionEspesores : uscProcesosPHPasosBase
    {
        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.PHCilindros != null)
                {
                    txtLecturaParedA.Text = this.PHCilindros.LecturaAParedCiindrol.HasValue ? this.PHCilindros.LecturaAParedCiindrol.Value.ToString() : String.Empty;
                    txtLecturaParedB.Text = this.PHCilindros.LecturaBParedCilindro.HasValue ? this.PHCilindros.LecturaBParedCilindro.Value.ToString() : String.Empty;

                    txtLecturaFondoA.Text = this.PHCilindros.LecturaAFondoCilindro.HasValue ? this.PHCilindros.LecturaAFondoCilindro.Value.ToString() : String.Empty;
                    if(this.PHCilindros.TipoFondoCilindro != null)
                        txtTipoFondo.SelectedValue = this.PHCilindros.TipoFondoCilindro.Trim();
                }
            }
        }
        public override void GuardarAction(Guid usuarioID, bool saltearValidacion)
        {
            try
            {
                this.PHCilindros.LecturaAParedCiindrol = !String.IsNullOrWhiteSpace(txtLecturaParedA.Text) ? double.Parse(txtLecturaParedA.Text) : default(double?);
                this.PHCilindros.LecturaBParedCilindro = !String.IsNullOrWhiteSpace(txtLecturaParedB.Text) ? double.Parse(txtLecturaParedB.Text) : default(double?);

                this.PHCilindros.LecturaAFondoCilindro = !String.IsNullOrWhiteSpace(txtLecturaFondoA.Text) ? double.Parse(txtLecturaFondoA.Text) : default(double?);
                this.PHCilindros.TipoFondoCilindro = txtTipoFondo.SelectedValue.Trim();                

                this.PHCilindrosLogic.Update(this.PHCilindros);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public override void DeshabilitarControles()
        {
            txtLecturaFondoA.Enabled = false;
            txtLecturaParedA.Enabled = false;
            txtLecturaParedB.Enabled = false;
            txtTipoFondo.Enabled = false;
        }
        public override Tuple<bool, string> ValidarAction()
        {
            var espesorAdmisibleCilindro = 0d;
            var espesorMinimoMedidoPared = this.PHCilindros.LecturaBParedCilindro;
            var espesorMedidoEnFondo = this.PHCilindros.LecturaAFondoCilindro;
            var coeficienteFondo = this.PHCilindros.TipoFondoCilindro ==
                                            CrossCutting.DatosDiscretos.TIPOSFONDOS.CONCAVO ? 2 : 1.5;


            if (espesorMinimoMedidoPared < espesorAdmisibleCilindro)
                return ValidarERROR("El espesor medido es menor al admisible");

            return ValidarOK;
        }
        #endregion
    }
}