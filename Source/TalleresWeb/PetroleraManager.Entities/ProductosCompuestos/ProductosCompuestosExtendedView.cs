using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace PetroleraManager.Entities
{
    [Serializable]
    public class ProductosCompuestosExtendedView:ViewEntity
    {
        public Guid IDProductoCompuesto { get; set; }
        public Guid IDProductoComponente { get; set; }
        public String DescProductoComponente { get; set; }
        public Decimal Cantidad { get; set; }
        public Decimal PrecioUComponente { get; set; }
        public Decimal PrecioTComponente { get; set; }

    }
}
