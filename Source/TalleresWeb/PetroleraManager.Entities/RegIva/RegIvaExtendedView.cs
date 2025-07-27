using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace PetroleraManager.Entities
{
    public class RegIvaExtendedView : ViewEntity
    {
        public String Descripcion { get; set; }
        public Boolean Activo { get; set; }
    }
}
