using System;

namespace TalleresWeb.Entities
{
    public class InformeDetalleBasicView
    {
        public Guid ID { get; set; }

        public String Taller { get; set; }

        public String Dominio { get; set; }

        public String NumeroObleaAnterior { get; set; }

        public String Operacion { get; set; }

        public String Fecha { get; set; }
    }
}
