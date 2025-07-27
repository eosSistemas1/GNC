using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace PetroleraManager.Entities
{
    [Serializable]
    public class InventarioDetalleParameters : ParametersEntity
    {
        public Guid ProveedoresID { get; set; }
        public Guid ClientesID { get; set; }
        public Guid ProductoID { get; set; }
        public Guid RubroID { get; set; }
        public DateTime FechaD { get; set; }
        public DateTime FechaH { get; set; }
    }
}
