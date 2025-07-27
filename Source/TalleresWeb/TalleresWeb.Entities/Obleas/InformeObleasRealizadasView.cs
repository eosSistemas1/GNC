using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TalleresWeb.Entities
{
    public class InformeObleasRealizadasView
    {
        public int CantidadBaja { get; set; }
        public int CantidadConversion { get; set; }
        public int CantidadModificacion { get; set; }
        public int CantidadReemplazo { get; set; }
        public int CantidadRevision { get; set; }
        public int CantidadRevisionCRPC { get; set; }
        public int CantidadTotalEntregadas { get; set; }
        public int CantidadTotalRealizadas { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public string Taller { get; set; }
    }
}
