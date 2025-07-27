using System;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    public class DespachoEnCursoView : ViewEntity
    {
        public String Descripcion
        {
            get { return this.Numero.ToString(); }
        }
        public DateTime Fecha { get; set; }
        public String Flete { get; set; }
        public DateTime? FechaHoraSalida { get; set; }
        public String Zona { get; set; }
        public int Numero { get; set; }
    }
}
