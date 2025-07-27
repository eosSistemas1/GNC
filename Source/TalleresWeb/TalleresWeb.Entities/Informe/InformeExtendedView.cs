using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    public class InformeExtendedView : ViewEntity
    {   
        public Guid IdInforme { get; set; }

        public Int64 NumeroInforme { get; set; }

        public DateTime FechaHoraInforme { get; set; }

        public Boolean EstadoInforme { get; set; }

        public Int32 CantidadObleasEnviadas { get; set; }

        public Int32 CantidadObleasAsignadas { get; set; }

        public Int32 CantidadObleasRechazadas { get; set; }
    }
}
