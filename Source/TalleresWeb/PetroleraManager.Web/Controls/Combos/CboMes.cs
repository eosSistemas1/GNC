using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Logic;
using TalleresWeb.Entities;
using PL.Fwk.Entities;
using System.Globalization;

namespace PetroleraManager.Web.Controls
{
    public class CboMes : PLComboBox
    {

        public override void LoadData()
        {

            List<PL.Fwk.Entities.ViewEntity> dt = new List<PL.Fwk.Entities.ViewEntity>();
            MesExtendedView dr = new MesExtendedView();
            dr.ID = "-1";
            dr.Descripcion = "MES";
            dt.Add(dr);

            int anioHasta = DateTime.Now.Year;
            for (int i = 1; i < 13; i++)
            {
                MesExtendedView dr2 = new MesExtendedView();
                System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem(ObtenerNombreMesNumero(i), i.ToString());
                dr2.ID = i.ToString();
                dr2.Descripcion = (ObtenerNombreMesNumero(i));
                dt.Add(dr2);
            }

            this.DataSource = dt;
        }

        private string ObtenerNombreMesNumero(int numeroMes)
        {
            try
            {
                DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
                string nombreMes = formatoFecha.GetMonthName(numeroMes);
                return nombreMes.ToUpper();
            }
            catch
            {
                return "Desconocido";
            }
        }
    }

    public class MesExtendedView : ViewEntity
    {
        public String ID { get; set; }
        public String Descripcion { get; set; }
    }    
}






       