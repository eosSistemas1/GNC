using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace PetroleraManager.Entities
{
    public class ProveedoresExtendedView : ViewEntity
    {
        public int Codigo { get; set; }
        public String RazonSocial { get; set; }
        public String Domicilio { get; set; }
        public String Telefono { get; set; }
        public String Localidad { get; set; }
        public String Provincia { get; set; }
        public String CUIT { get; set; }
    }
}
