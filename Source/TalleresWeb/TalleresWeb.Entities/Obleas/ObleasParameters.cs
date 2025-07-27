using PL.Fwk.Entities;
using System;

namespace TalleresWeb.Entities
{
    public class ObleasParameters : ParametersEntity
    {
        #region Properties

        public String Dominio { get; set; }

        public Guid EstadoFichaAprobada { get; set; }

        public Guid EstadoAprobadaConError { get; set; }

        public Guid EstadoFichaBloqueada { get; set; }

        public Guid EstadoFichaPendiente { get; set; }

        public Guid EstadoRechazadaPorEnte { get; set; }

        public DateTime FechaDesde { get; set; }

        public DateTime FechaHasta { get; set; }

        public Guid MarcaCilID { get; set; }

        public Guid MarcaRegID { get; set; }

        public Guid MarcaValID { get; set; }

        public String NroDocCliente { get; set; }

        public String NroObleaNueva { get; set; }

        public String NroObleaAnterior { get; set; }

        public String SerieCil { get; set; }

        public String SerieReg { get; set; }

        public String SerieVal { get; set; }

        public Guid TallerID { get; set; }

        public Guid TipoDocClienteID { get; set; }

        public Guid EstadoFichaID { get; set; }
        public string CodigoHomologacionRegulador { get; set; }
        public string NumeroSerieRegulador { get; set; }
        public string CodigoHomologacionCilindro { get; set; }
        public string NumeroSerieCilindro { get; set; }
        public string CodigoHomologacionValvula { get; set; }
        public string NumeroSerieValvula { get; set; }


        #endregion
    }
}