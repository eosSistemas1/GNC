using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    [Serializable]
    public class ObleasExtendedView : ViewEntity
    {        
        public DateTime FechaHabilitacion { get; set; }
        public DateTime FechaAlta { get; set; }
        public String NroObleaAnterior { get; set; }
        public String Dominio { get; set; }
        public String NombreyApellido { get; set; }
        public String Telefono { get; set; }
        public String Observacion { get; set; }
        public Guid IdTaller { get; set; }
        public String Taller { get; set; }
        public Guid IdEstadoFicha { get; set; }
        public String Zona { get; set; }
        public String NroObleaNueva { get; set; }
        public String EstadoFicha { get; set; }
        public String Operacion { get; set; }
        public int NroInternoOpercion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public Guid IdOpreracion { get; set; }
        public String TallerEmail { get; set; }
    }
}
