using System;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    [Serializable]
    public class DetalleDespachoView : ViewEntity
    {
        public string TipoTramite { get; set; }
        public string Dominio { get; set; }
        public string Taller { get; set; }
        public string ZonaTaller { get; set; }
        public Guid IDTramite { get; set; }
        public Guid IdTaller { get; set; }
        public string Estado { get; set; }
    }
}
