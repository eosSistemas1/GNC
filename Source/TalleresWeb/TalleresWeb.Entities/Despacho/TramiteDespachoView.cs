using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TalleresWeb.Entities
{
    public class TramiteDespachoView : ViewEntity
    {
        public DateTime Fecha { get; set; }
        public String Tramite { get; set; }
        public String Operacion { get; set; }
        public String Dominio { get; set; }
        public String ObleaAnterior { get; set; }
        public String ObleaAsignada { get; set; }
        public String TipoTramite { get; set; }
        public Guid? IDOperacion { get; internal set; }
        public Guid? IdVehiculo { get; internal set; }
        public Guid IdEstado { get; internal set; }
        public string Estado { get; internal set; }
    }
}
