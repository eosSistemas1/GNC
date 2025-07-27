using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace PetroleraManager.Entities
{
    [Serializable]
    public class ComprobantesVentasDetalleExtendedView : ViewEntity
    {
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

        public Decimal Iva { get; set; }
        public Decimal IvaAlicuota { get; set; }
        public Decimal MontoImpuestoInternoCompra { get; set; }
        public Decimal ImpuestoInternoCompra { get; set; }
        public Decimal Exento { get; set; }

        public Boolean UsaLote { get; set; }
    }
}
