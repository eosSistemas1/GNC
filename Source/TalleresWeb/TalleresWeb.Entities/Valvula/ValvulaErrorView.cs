using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    public class ValvulaErrorView
    {
        public Guid IDValvulaOblea { get; set; }

        public String CodHomologacionActual { get; set; }
        public String CodHomologacionNuevo { get; set; }

        public String NroSerieActual { get; set; }
        public String NroSerieNuevo { get; set; }
    }
}
