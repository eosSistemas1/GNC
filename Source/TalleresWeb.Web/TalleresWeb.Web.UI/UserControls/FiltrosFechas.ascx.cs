using CrossCutting.DatosDiscretos;
using System;
using System.Web.UI.WebControls;

namespace TalleresWeb.Web.UI.UserControls
{
    public partial class FiltrosFechas : System.Web.UI.UserControl
    {
        #region Members

        private Boolean _RangoDefault;

        #endregion

        #region Propiedades

        public DateTime FechaDesde
        {
            get
            {
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

                        
        public Boolean RangoDefault
        {
            get
            {
                return this._RangoDefault;
            }
            set
            {
                this._RangoDefault = value;
            }
        }

        #endregion

        #region Metodos

        protected void CheckedChanged(Object sender, EventArgs e)
        {
            var s = (CheckBox)sender;

            if (s.ID == chkPorRangoFechas.ID) chkPorQuincenas.Checked = !chkPorRangoFechas.Checked;
            if (s.ID == chkPorQuincenas.ID) chkPorRangoFechas.Checked = !chkPorQuincenas.Checked;

            calFechaD.Visible = chkPorRangoFechas.Checked;
            calFechaH.Visible = chkPorRangoFechas.Checked;

            cboQuincena.Visible = chkPorQuincenas.Checked;
            cboMes.Visible = chkPorQuincenas.Checked;
            cboAnio.Visible = chkPorQuincenas.Checked;
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.chkPorQuincenas.Checked = this.RangoDefault;
                this.chkPorRangoFechas.Checked = !chkPorQuincenas.Checked;

                CheckedChanged(chkPorQuincenas, new EventArgs());
            }
        }

        private DateTime GetFechaDesde()
        {
            DateTime valor = Configuracion.MinDatetime;
            if (chkPorRangoFechas.Checked) valor = this.calFechaD.Text != String.Empty ? DateTime.Parse(this.calFechaD.Text) : Configuracion.MinDatetime;
            else if (chkPorQuincenas.Checked)
            {
                if ((cboQuincena.SelectedIndex != -1) &&
                    (cboMes.SelectedIndex != -1) &&
                    (cboAnio.SelectedIndex != -1))
                {
                    String dia = cboQuincena.SelectedValueString.ToString() == "2" ? "16" : "01";
                    String mes = cboMes.SelectedValueString.ToString();
                    String anio = cboAnio.SelectedValueString.ToString();

                    valor = DateTime.Parse(dia + "/" + mes + "/" + anio);

                }
            }
            return valor;
        }

        private DateTime GetFechaHasta()
        {
            DateTime valor = Configuracion.MaxDatetime;
            if (chkPorRangoFechas.Checked) valor = this.calFechaH.Text != String.Empty ? DateTime.Parse(this.calFechaH.Text) : Configuracion.MaxDatetime;
            else if (chkPorQuincenas.Checked)
            {
                if ((cboQuincena.SelectedIndex != -1) &&
                    (cboMes.SelectedIndex != -1) &&
                    (cboAnio.SelectedIndex != -1))
                {
                    String anio = cboAnio.SelectedValueString.ToString();
                    String mes = cboMes.SelectedValueString.ToString();
                    String dia = cboQuincena.SelectedValueString.ToString() == "1" ? "15" : DateTime.DaysInMonth(Int32.Parse(anio), Int32.Parse(mes)).ToString("00");

                    valor = DateTime.Parse(dia + "/" + mes + "/" + anio);

                }
            }
            return valor;
        }

        #endregion
    }
}