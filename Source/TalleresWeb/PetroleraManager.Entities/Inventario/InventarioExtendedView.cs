using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace PetroleraManager.Entities
{
    public class InventarioExtendedView : ViewEntity
    {
        public Boolean Activo { get; set; }
        public DateTime FechaComprobante { get; set; }
        public Decimal MontoComprobante { get; set; }
        public String NroComprobante { get; set; }
        public Guid ClientesID { get; set; }
        public String ClientesRazonSocial { get; set; }
        public Guid ProveedorID { get; set; }
        public String ProveedorRazonSocial { get; set; }
        public String TipoComprobanteDescripcion { get; set; }
    }
}
