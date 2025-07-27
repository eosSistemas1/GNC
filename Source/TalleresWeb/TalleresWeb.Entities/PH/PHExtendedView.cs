using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    public class PHExtendedView : ViewEntity
    {
        public Guid IDTaller { get; set; }
        public Guid IDNroCartaTaller { get; set; }
        public Guid IDVehiculo { get; set; }
        public Guid IDCliente { get; set; }
        public Guid IDPEC { get; set; }
        public String NroObleaHabilitante { get; set; }
        public Guid IDCRPC { get; set; }
    }
}
