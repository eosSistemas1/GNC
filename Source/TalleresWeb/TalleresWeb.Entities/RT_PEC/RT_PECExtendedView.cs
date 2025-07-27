using PL.Fwk.Entities;
using System;

namespace TalleresWeb.Entities
{
    [Serializable]
    public class RT_PECExtendedView : ViewEntity
    {
        public Guid RTID { get; set; }
        public Guid PECID { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
    }
}
