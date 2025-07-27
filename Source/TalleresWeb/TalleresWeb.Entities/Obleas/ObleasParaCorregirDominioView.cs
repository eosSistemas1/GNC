using PL.Fwk.Entities;
using System;

namespace TalleresWeb.Entities
{
    [Serializable]
    public class ObleasParaCorregirDominioView : ViewEntity
    {        
        public DateTime FechaHabilitacion { get; set; }        
        public String NumeroOblea { get; set; }
        public String DominioConError { get; set; }
        public String DominioOK { get; set; }        
        public String Taller { get; set; }
        public Guid IdObleaHistoricoEstado { get; set; }
        public Guid VehiculoID { get; set; }
    }
}
