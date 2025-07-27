using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace PetroleraManager.Entities
{
    public class LocalidadesExtendedView:ViewEntity
    {
        public String CP { get; set; }
        public Guid ProvinciasID { get; set; }
        public Boolean Activo { get; set; }
    }
}
