using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace PetroleraManager.Entities
{
    public class ProductoLoteExtendedView:ViewEntity
    {
        public Guid ProductoLoteID { get; set; }
        public String CodigoProducto { get; set; }
        public String Lote { get; set; }
        public Decimal CantidadExistente { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
