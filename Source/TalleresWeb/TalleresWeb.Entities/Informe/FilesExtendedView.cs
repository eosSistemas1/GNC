using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    public class FilesExtendedView : ViewEntity
    {        

        public Int64 NumeroInforme { get; set; }

        public DateTime FechaHoraInforme { get; set; }

        public String descripcionUSR { get; set; }
        public String descripcionREG { get; set; }
        public String descripcionCIL { get; set; }
        public String descripcionVAL { get; set; }

        public String urlUSR { get; set; }
        public String urlREG { get; set; }
        public String urlCIL { get; set; }
        public String urlVAL { get; set; }
    }
}
