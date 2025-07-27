using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    public class TalleresRTExtendedView : ViewEntity
    {
        public Boolean EsRTPrincipal { get; set; }
        public DateTime FechaDesdeRTT { get; set; }
        public DateTime? FechaHastaRTT { get; set; }
        public Guid TalleresID { get; set; }
    }
}
