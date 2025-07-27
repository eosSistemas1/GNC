using System;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    public class PHCilindrosPendientesView
    {
        public Guid IDCilindroUnidad { get; set; }
        public String Taller { get; set; }
        public String CodigoHomologacionCilindro { get; set; }
        public String NumeroSerieCilindro { get; set; }
        public String CodigoHomologacionValvula { get; set; }
        public String NumeroSerieValvula { get; set; }
        public Guid ID { get; set; }
        public int? NroOperacionCRPC { get; set; }
        public string Dominio { get; set; }
        public Guid IDEstadoPH { get; set; }
        public string Estado { get; set; }        
        public Guid IDPH { get; set; }
        public DateTime FechaOperacion { get; set; }
    }
}
