using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    [Serializable]
    public class ObleasReguladoresExtendedView : ViewEntity
    {
        public Guid IDReg { get; set; }
        public Guid IDRegUni { get; set; }
        public String CodigoReg { get; set; }
        public String NroSerieReg { get; set; }
        public Guid MSDBRegID { get; set; }
        public String MSDBReg { get; set; }
    }
}
