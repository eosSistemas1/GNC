using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace PetroleraManager.Entities
{
    [Serializable]
    public class InventarioDetalleExtendedView : ViewEntity
    {
        public Guid ProveedorID { get; set; }
        public String ProveedorRazonSocial{ get; set; }
        public Guid ClienteID { get; set; }
        public String ClienteRazonSocial { get; set; }
        public DateTime FechaComprobante{ get; set; }
        public String TipoComprobanteDescripcion{ get; set; }
        public String NroComprobante{ get; set; }

        public Guid ProductoID { get; set; }
        public Guid ProductoLoteID { get; set; } 
        public String CodigoProducto { get; set; }
        public Decimal Cantidad { get; set; }
        public Decimal Bonificacion { get; set; }
        public Decimal PrecioUnitario { get; set; }
        public Decimal PrecioTotal { get; set; }
        public String Lote { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public String Observacion { get; set; }
        public String TipoMovimiento { get; set; }
        public Boolean UsaLote { get; set; }
    }
}
