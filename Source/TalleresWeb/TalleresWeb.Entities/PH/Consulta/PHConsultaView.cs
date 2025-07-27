using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TalleresWeb.Entities
{
    [Serializable]
    public class PHConsultaView
    {
        public string Cliente { get; set; }
        public string Dominio { get; set; }
        public string EstadoPH { get; set; }
        public Guid EstadoPHID { get; set; }
        public DateTime Fecha { get; set; }
        public Guid ID { get; set; }
        public string NroObleaHabilitante { get; set; }
        public string NroSerieCilindro { get; set; }
        public Guid PHID { get; set; }
        public Guid TallerID { get; set; }
        public string TallerRazonSocial { get; set; }
    }
}
