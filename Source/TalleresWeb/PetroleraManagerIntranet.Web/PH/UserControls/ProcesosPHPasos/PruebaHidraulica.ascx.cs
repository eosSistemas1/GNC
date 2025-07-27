using PetroleraManagerIntranet.Web.PH.Proceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace PetroleraManagerIntranet.Web.UserControls.ProcesosPHPasos
{
    public partial class PruebaHidraulica : uscProcesosPHPasosBase
    {
        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.PHCilindros != null)
                {
                    txtPesoCilindroLleno.Text = this.PHCilindros.PesoCilinroLleno.HasValue ? this.PHCilindros.PesoCilinroLleno.Value.ToString() : String.Empty; ;
                    txtTemperaturaAmbiente.Text = this.PHCilindros.TemperaturaAmbiente.HasValue ? this.PHCilindros.TemperaturaAmbiente.Value.ToString() : String.Empty; ; ;

                    txtNivelAguaInicial.Text = this.PHCilindros.LecturaMinBureta.HasValue ? this.PHCilindros.LecturaMinBureta.Value.ToString() : String.Empty;
                    txtNivelAguaPesionPrueba.Text = this.PHCilindros.NivelAguAPresionPruebaCilindro.HasValue ? this.PHCilindros.NivelAguAPresionPruebaCilindro.Value.ToString() : String.Empty; ;
                    txtNivelAguaDespresurizado.Text = this.PHCilindros.LecturaMaxBureta.HasValue ? this.PHCilindros.LecturaMaxBureta.Value.ToString() : String.Empty;

                    cboPresionDePrueba.SelectedValue = this.PHCilindros.PresionPruebaCilindro.HasValue ? this.PHCilindros.PresionPruebaCilindro.Value.ToString() : String.Empty;
                    cboBureta.SelectedValue = this.PHCilindros.NroBureta.HasValue ? this.PHCilindros.NroBureta.Value.ToString() : String.Empty;
                }

            }

            HabilitarPanel();
        }

        public override void GuardarAction(Guid usuarioID, bool saltearValidacion)
        {
            if (saltearValidacion) return;

            List<string> mensajes = ValidarCamposIngresados();

            if (!mensajes.Any())
            {
                try
                {
                    this.PHCilindros = PHCilindrosLogic.Read(this.PHCilindros.ID);

                    this.PHCilindros.TemperaturaAmbiente = double.Parse(txtTemperaturaAmbiente.Text);
                    this.PHCilindros.PesoCilinroLleno = double.Parse(txtPesoCilindroLleno.Text);
                    this.PHCilindros.TemperaturaAmbiente = double.Parse(txtTemperaturaAmbiente.Text);
                    this.PHCilindros.LecturaMinBureta = double.Parse(txtNivelAguaInicial.Text);
                    this.PHCilindros.NivelAguAPresionPruebaCilindro = double.Parse(txtNivelAguaPesionPrueba.Text);
                    this.PHCilindros.LecturaMaxBureta = double.Parse(txtNivelAguaDespresurizado.Text);
                    this.PHCilindros.PresionPruebaCilindro = !string.IsNullOrWhiteSpace(cboPresionDePrueba.SelectedItem.Text) ? double.Parse(cboPresionDePrueba.SelectedValue) : default(double?);
                    this.PHCilindros.NroBureta = !string.IsNullOrWhiteSpace(cboBureta.SelectedValue) ? int.Parse(cboBureta.SelectedValue) : default(int?);

                    var idValvula = int.Parse(cboValvulaPrueba.SelectedValue);
                    double pesoValvulaCarga = new ValvulaCarga().GetValvulaCargaByID(idValvula).Peso;
                    this.PHCilindros.PesoAguaCilindro = this.PHCilindros.PesoCilinroLleno.Value - this.PHCilindros.PesoVacioCilindro.Value - pesoValvulaCarga;

                    var aprobado = new ProcesoPHHelper().EvaluacionPH(this.PHCilindros.PesoAguaCilindro.Value,
                                                                      this.PHCilindros.NivelAguAPresionPruebaCilindro.Value,
                                                                      this.PHCilindros.PresionPruebaCilindro.Value,
                                                                      this.PHCilindros.TemperaturaAmbiente.Value,
                                                                      idValvula,
                                                                      this.PHCilindros.PesoCilinroLleno.Value,
                                                                      this.PHCilindros.PesoVacioCilindro.Value);

                    string observaciones = aprobado ? $"APROBADO {DateTime.Now}" : $"RECHAZADO {DateTime.Now}";
                    string numeroCertificado = "";

                    this.PHCilindros.NroCertificadoPH = numeroCertificado;
                    this.PHCilindros.Rechazado = !aprobado;
                    this.PHCilindros.ObservacionPH = observaciones;
                    this.PHCilindrosLogic.Update(this.PHCilindros);
                }
                catch (Exception ex)
                {
                    MessageBoxCtrl.MessageBox(null, ex.Message, MessageBoxCtrl.TipoWarning.Error);
                }
            }
            else
            {
                MessageBoxCtrl.MessageBox(null, mensajes, MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        private List<string> ValidarCamposIngresados()
        {
            List<string> errores = new List<string>();
            if (string.IsNullOrWhiteSpace(txtPesoCilindroLleno.Text)) errores.Add("Debe ingresar peso del cilindro lleno");
            if (string.IsNullOrWhiteSpace(txtTemperaturaAmbiente.Text)) errores.Add("Debe ingresar la temperatura ambiente");


            return errores;
        }

        public override void DeshabilitarControles()
        {
            txtPesoCilindroLleno.Enabled = false;
            txtTemperaturaAmbiente.Enabled = false;

            txtNivelAguaInicial.Enabled = false;
            txtNivelAguaPesionPrueba.Enabled = false;
            txtNivelAguaDespresurizado.Enabled = false;

            cboPresionDePrueba.Enabled = false;
            cboBureta.Enabled = false;
        }
        public override Tuple<bool, string> ValidarAction()
        {           
            return ValidarOK;
        }
        protected void txtPesoCilindroLleno_TextChanged(object sender, EventArgs e)
        {
            List<string> mensaje = new List<string>();

            TextBox txt = (TextBox)sender;

            if (txt.ID == "txtPesoCilindroLleno")
            {
                if(string.IsNullOrWhiteSpace(txt.Text)) mensaje.Add("El peso de cilindro ingresado es invalido");

                try
                {
                    double.Parse(txt.Text);
                }
                catch
                {
                    mensaje.Add("El peso de cilindro ingresado es invalido");
                }
            }

            if (txt.ID == "txtTemperaturaAmbiente")
            {
                if (string.IsNullOrWhiteSpace(txt.Text)) mensaje.Add("La temperatura obligatoria");

                double tmp = 0;
                try
                {
                    tmp = double.Parse(txt.Text);
                }
                catch
                {
                    mensaje.Add("La temperatura ingresada debe ser mayor a 38 y menor a 100");
                }

                if(tmp < 38 || tmp > 100) mensaje.Add("La temperatura debe ser mayor a 38 y menor a 100");
            }

            if (!mensaje.Any())
            {
                HabilitarPanel();
            }
            else
            {
                MessageBoxCtrl.MessageBox(null, mensaje, MessageBoxCtrl.TipoWarning.Warning);
            }

        }
        private void HabilitarPanel()
        {
            panel1.Visible = !string.IsNullOrEmpty(txtPesoCilindroLleno.Text)
                                && !string.IsNullOrEmpty(txtTemperaturaAmbiente.Text);
        }
        #endregion
    }
}