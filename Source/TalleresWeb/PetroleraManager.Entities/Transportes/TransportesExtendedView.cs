using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace PetroleraManager.Entities
{
    public class TransportesExtendedView:ViewEntity
    {
        public String Domicilio { get; set; }
        public String Telefono { get; set; }
        public Boolean Activo { get; set; }
    }
}
