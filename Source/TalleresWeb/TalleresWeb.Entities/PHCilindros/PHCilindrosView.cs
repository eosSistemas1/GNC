using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    [Serializable]
    public class PHCilindrosView : ViewEntity
    {
        public string CapacidadCil { get; set; }
        public string CodigoCil { get; set; }
        public string CodigoVal { get; set; }        
        public string MarcaCil { get; set; }
        public string Observaciones { get; set; }
        public string SerieCil { get; set; }
        public string SerieVal { get; set; }
        public string MesFabricacionCil { get; set; }
        public string AnioFabricacionCil { get; set; }

        public string FechaFabricacionCil
        {
            get
            {
                string mes = this.MesFabricacionCil;
                string anio = this.AnioFabricacionCil;

                if (String.IsNullOrWhiteSpace(mes) || String.IsNullOrWhiteSpace(anio)) return string.Empty;

                int a = int.Parse(anio);
                a = a < 60 ? a + 2000 : a + 1900;
                int m = int.Parse(mes);
                string fecha = $"01/{m.ToString("00")}/{a.ToString()}";
                return DateTime.TryParse(fecha, out DateTime valor) ? valor.ToString() : String.Empty;
            }

        }
    }
}
