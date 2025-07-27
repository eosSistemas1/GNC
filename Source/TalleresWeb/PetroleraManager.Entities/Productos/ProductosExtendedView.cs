using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace PetroleraManager.Entities
{
    public class ProductosExtendedView:ViewEntity
    {
        public String CodigoProducto { get; set; }
        public Guid TipoProductoID { get; set; }
        public String TipoProductoDescripcion { get; set; }
        public Decimal CantidadExistente { get; set; }
        public Decimal CantidadMinima { get; set; }
        public Decimal CantidadAComprar { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public Decimal ImporteCompra { get; set; }
        public Decimal ImporteTotalLote { get; set; }
        public Guid ProductoLoteID { get; set; }
        public String ProductoLoteDescripcion { get; set; }
        public Guid UnidadIDCompra { get; set; }
        public Boolean UsaLote { get; set; }
    }
}
