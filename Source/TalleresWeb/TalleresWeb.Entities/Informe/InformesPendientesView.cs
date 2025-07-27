using System;

namespace TalleresWeb.Entities
{
    public class InformesPendientesView
    {
        public Guid ID { get; set; }
        public long Numero { get; set; }
        public DateTime FechaHora { get; set; }

        public int CantidadObleasEnviadas { get; set; }
        public int CantidadObleasAsignadas { get; set; }
        public int CantidadObleasRechazadas { get; set; }
        public int CantidadObleasBaja { get; set; }
    }
}
