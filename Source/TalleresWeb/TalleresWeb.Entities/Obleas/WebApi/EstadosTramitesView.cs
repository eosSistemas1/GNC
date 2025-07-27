using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TalleresWeb.Entities
{
    public class EstadosTramitesView
    {
        public Guid ID { get; set; }
        public Guid IdTaller { get; set; }
        public String NombreyApellido { get; set; }
        public DateTime FechaTramite { get; set; }
        public String TipoTramite { get; set; }
        public Guid? IDOperacion { get; set; }
        public String Operacion { get; set; }
        public Guid IdVehiculo { get; set; }
        public String Dominio { get; set; }
        public Guid IdEstado { get; set; }
        public String Estado { get; set; }
        public String ObleaAnterior { get; set; }
        public String ObleaAsignada { get; set; }
        public String Pedido { get; set; }
        public String Observacion { get; set; }
    }
}
