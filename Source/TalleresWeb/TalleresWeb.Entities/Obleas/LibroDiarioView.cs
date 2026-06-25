using PL.Fwk.Entities;
using System;

namespace TalleresWeb.Entities
{
    [Serializable]
    public class LibroDiarioView : ViewEntity
    {
        public DateTime Fecha { get; set; }
        public string Operacion { get; set; }
        public string NombreCliente { get; set; }
        public string Vehiculo { get; set; }
        public string Dominio { get; set; }
        public string CodigoRegulador { get; set; }
        public string NroSerieRegulador { get; set; }
        public string CodigosCilindros { get; set; }
        public string NrosSeriesCilindros { get; set; }
        public string CodigosValvulas { get; set; }
        public string NrosSeriasValvulas { get; set; }
        public string NroObleaAnterior { get; set; }
        public string NroObleaNueva { get; set; }
    }
}
