using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    [Serializable]
    public class ObleasValvulasExtendedView : ViewEntity
    {
        public Guid IDVal { get; set; }
        public Guid IDValUni { get; set; }
        public Guid IdObleaCil { get; set; }
        public String CodigoVal { get; set; }
        public String NroSerieVal { get; set; }
        public Guid MSDBValID { get; set; }
        public String MSDBVal { get; set; }
        public String OrdenCil { get; set; }
    }
}
