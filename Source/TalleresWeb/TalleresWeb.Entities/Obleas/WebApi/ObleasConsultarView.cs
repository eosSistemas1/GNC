using System;

namespace TalleresWeb.Entities
{    
    public class ObleasConsultarView
    {
        public Guid ID { get; set; }    
        public DateTime FechaHabilitacion { get; set; }
        public string NroObleaAnterior { get; set; }
        public string Dominio { get; set; }
        public string NombreyApellido { get; set; }
        public string Operacion { get; set; }
        public DateTime FechaAlta { get; set; }
        public string NroObleaNueva { get; set; }       
    }
}
