using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    [Serializable]
    public class ObleasCilindrosExtendedView : ViewEntity
    {
        public String OrdenCil { get; set; }
        public Guid IDCil { get; set; }
        public Guid IDCilUni { get; set; }
        public Guid IDValUni { get; set; }
        public String CodigoCil { get; set; }
        public String NroSerieCil { get; set; }
        public String CilFabMes { get; set; }
        public String CilFabAnio { get; set; }
        public String CilRevMes { get; set; }
        public String CilRevAnio { get; set; }
        public Guid CRPCCilID { get; set; }
        public String CRPCCil { get; set; }
        public Guid MSDBCilID { get; set; }
        public String MSDBCil { get; set; }
        public String NroCertificadoPH { get; set; }
        public Guid IdPec { get; set; }
        public Boolean Rechazado { get; set; }
        public Boolean RealizaPH { get; set; }
    }
}
