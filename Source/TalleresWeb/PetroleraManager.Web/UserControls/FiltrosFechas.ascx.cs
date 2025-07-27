using CrossCutting.DatosDiscretos;
using System;
using System.Web.UI.WebControls;

namespace PetroleraManager.Web.UserControls
{
    public partial class FiltrosFechas : System.Web.UI.UserControl
    {
        #region Propiedades

        public DateTime FechaDesde { 
            get{
                return GetFechaDesde();
            }
        }
        public DateTime FechaHasta
        {
            get
            {
                return GetFechaHasta();
            }
        }

        private DateTime GetFechaDesde()
        {
            DateTime valor = GetDinamyc.MinDatetime;
            if (chkPorRangoFechas.Checked) valor = calFechaD.Text != String.Empty ? DateTime.Parse(calFechaD.Text) : GetDinamyc.MinDatetime;
            else if (chkPorQuincenas.Checked)
            {
                if ((cboQuincena.SelectedIndex != -1) &&
                    (cboMes.SelectedIndex != -1) &&
                    (cboAnio.SelectedIndex != -1))
                {
                    String dia = cboQuincena.SelectedValueString.ToString() == "2" ? "16" : "01";
                    String mes = cboMes.SelectedValueString.ToString();
                    String anio = cboAnio.SelectedValueString.ToString();

                    if (int.Parse(dia) < 1 || int.Parse(mes) < 1 || int.Parse(anio) < 1)
                        throw new Exception("La fecha ingresada no es válida.");

                    valor = DateTime.Parse(dia + "/" + mes + "/" + anio);

                }
            }
            return valor;
        }
        private DateTime GetFechaHasta()
        {
            DateTime valor = GetDinamyc.MaxDatetime;
            if (chkPorRangoFechas.Checked) valor = calFechaH.Text != String.Empty ? DateTime.Parse(calFechaH.Text) : GetDinamyc.MaxDatetime;
            else if (chkPorQuincenas.Checked)
            {
                if ((cboQuincena.SelectedIndex != -1) &&
                    (cboMes.SelectedIndex != -1) &&
                    (cboAnio.SelectedIndex != -1))
                {
                    String anio = cboAnio.SelectedValueString.ToString();
                    String mes = cboMes.SelectedValueString.ToString();
                    String dia = cboQuincena.SelectedValueString.ToString() == "1" ? "15" : DateTime.DaysInMonth(int.Parse(anio),int.Parse(mes)).ToString("00");
                   
                    valor = DateTime.Parse(dia + "/" + mes + "/" + anio);

                }
            }
            return valor;
        }

        private Boolean _RangoDefault;
        public Boolean RangoDefault {
            set 
            {
                _RangoDefault = value;
            }
        }
        #endregion

        #region Metodos
        protected void CheckedChanged(object sender, EventArgs e)
        {
            var s = (CheckBox)sender;

            if (s.ID == chkPorRangoFechas.ID) chkPorQuincenas.Checked = !chkPorRangoFechas.Checked;
            if (s.ID == chkPorQuincenas.ID) chkPorRangoFechas.Checked = !chkPorQuincenas.Checked;

            calFechaD.Visible = chkPorRangoFechas.Checked;
            calFechaH.Visible = chkPorRangoFechas.Checked;

            cboQuincena.Visible = chkPorQuincenas.Checked;
            cboMes.Visible = chkPorQuincenas.Checked;
            cboAnio.Visible = chkPorQuincenas.Checked;

            if (_RangoDefault) cboMes.SelectedValueString = DateTime.Now.Month.ToString("00");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                chkPorQuincenas.Checked = _RangoDefault;
                chkPorRangoFechas.Checked = !chkPorQuincenas.Checked;

                CheckedChanged(chkPorQuincenas, new EventArgs());
            }
        }
        #endregion
    }
}