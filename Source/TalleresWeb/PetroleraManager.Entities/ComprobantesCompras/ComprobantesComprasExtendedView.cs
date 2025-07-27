using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace PetroleraManager.Entities
{
    public class ComprobantesComprasExtendedView : ViewEntity
    {
        public bool Activo { get; set; }
        public DateTime FechaComprobante { get; set; }
        public decimal MontoComprobante { get; set; }
        public string NroComprobante { get; set; }
        public decimal Percepcion1 { get; set; }
        public decimal Percepcion2 { get; set; }
        public decimal Percepcion3 { get; set; }
        public decimal Percepcion4 { get; set; }
        public decimal Percepcion5 { get; set; }
        public Guid ProveedorID { get; set; }
        public string ProveedorRazonSocial { get; set; }
        public string TipoComprobanteDescripcion { get; set; }
    }
}