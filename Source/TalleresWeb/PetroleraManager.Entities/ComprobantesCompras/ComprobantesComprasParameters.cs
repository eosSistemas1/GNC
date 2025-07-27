using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace PetroleraManager.Entities
{
    public class ComprobantesComprasParameters : ParametersEntity
    {
        public DateTime FechaComprobante { get; set; }
        public decimal MontoComprobante { get; set; }
        public string NroComprobante { get; set; }
        public Guid ProductoID { get; set; }
        public Guid ProveedoresID { get; set; }
        public Guid TiposComprobantesID { get; set; }
    }
}
