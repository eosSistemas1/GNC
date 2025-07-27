using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    [Serializable]
    public class PHCilindrosInformarView
    {
        public Guid ID { get; set; }
        public string Taller { get; set; }
        public DateTime FechaHabilitacion { get; set; }
        public string ObleaAnterior { get; set; }
        public string Dominio { get; set; }
        public string Cliente { get; set; }
        public string Telefono { get; set; }
        public string NumeroSerie { get; set; }
        public string CodigoHomologacion { get; set; }
    }
}
