using System;

namespace PetroleraManagerIntranet.Web.UserControls.ProcesosPHPasos
{
    public partial class RegistroPesos : uscProcesosPHPasosBase
    {
        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(this.PHCilindros != null)
                {
                    txtPesoVacio.Text = this.PHCilindros.PesoVacioCilindro.HasValue ? this.PHCilindros.PesoVacioCilindro.Value.ToString() : String.Empty;                    
                    txtPesoMarcadoCilindro.Text = this.PHCilindros.PesoMarcadoCilindro.HasValue ? this.PHCilindros.PesoMarcadoCilindro.Value.ToString() : String.Empty;
                }
            }
        }
       
        public override void GuardarAction(Guid usuarioID, bool saltearValidacion)
        {
            try
            {
                this.PHCilindros.PesoVacioCilindro = !String.IsNullOrWhiteSpace(txtPesoVacio.Text) ? double.Parse(txtPesoVacio.Text) : default(double?);
                this.PHCilindros.PesoMarcadoCilindro = !String.IsNullOrWhiteSpace(txtPesoMarcadoCilindro.Text) ? double.Parse(txtPesoMarcadoCilindro.Text) : default(double?);

                this.PHCilindrosLogic.Update(this.PHCilindros);                
            }
            catch (Exception ex)
            {
                throw ex;              
            }                      
        }
        
        public override void DeshabilitarControles()
        {
            txtPesoVacio.Enabled = false;
            txtPesoMarcadoCilindro.Enabled = false;
        }
       
        public override Tuple<bool, string> ValidarAction()
        {
            var pesoVacioCilindro = !String.IsNullOrWhiteSpace(txtPesoVacio.Text) ? double.Parse(txtPesoVacio.Text) : default(double?);
            var pesoMarcadoCilindro = !String.IsNullOrWhiteSpace(txtPesoMarcadoCilindro.Text) ? double.Parse(txtPesoMarcadoCilindro.Text) : default(double?);

            if (pesoVacioCilindro.HasValue && pesoMarcadoCilindro.HasValue)
            {
                double diferencia = Math.Abs(pesoMarcadoCilindro.Value - pesoVacioCilindro.Value);
                var valorDiferencia = 2;
                if (diferencia >= valorDiferencia)
                {
                    return ValidarERROR($"La diferencia es mayor a {valorDiferencia} kg . Debe observar cilindro");
                }
            }

            return ValidarOK;
        }
        #endregion
    }
}