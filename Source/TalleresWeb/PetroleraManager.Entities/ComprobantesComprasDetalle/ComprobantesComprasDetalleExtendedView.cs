using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace PetroleraManager.Entities
{
    [Serializable]
    public class ComprobantesComprasDetalleExtendedView : ViewEntity
    {
        public Decimal Bonificacion { get; set; }
        public Decimal Cantidad { get; set; }
        public String CodigoProducto { get; set; }
        public Decimal Exento { get; set; }
        public DateTime FechaComprobante { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public Decimal ImpuestoInternoCompra { get; set; }
        public Decimal Iva { get; set; }
        public Decimal IvaAlicuota { get; set; }
        public String Lote { get; set; }
        public Decimal MontoComprobante { get; set; }
        public Decimal MontoImpuestoInternoCompra { get; set; }
        public String NroComprobante { get; set; }
        public String Observacion { get; set; }
        public Decimal PrecioTotal { get; set; }
        public Decimal PrecioUnitario { get; set; }
        public Guid ProductoID { get; set; }
        public Guid ProductoLoteID { get; set; }
        public Guid ProveedorID { get; set; }
        public String ProveedorRazonSocial { get; set; }
        public String TipoComprobanteDescripcion { get; set; }
        public String TipoMovimiento { get; set; }
        public Boolean UsaLote { get; set; }
    }
}
