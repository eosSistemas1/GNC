using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    public class S_AccesosCUExtendedView : ViewEntity
    {
        public int IdCU { get; set; }
        public String Descripcion { get; set; }
        public String CodigoCU { get; set; }
        public int IdPadre { get; set; }
        public String Url { get; set; }
        public String UrlImagen { get; set; }
        public int IdRol { get; set; }
        public Boolean Permitido { get; set; }
        public Boolean Activo { get; set; }
        public String Usuario { get; set; }
        public Boolean AbrirEnVentanaNueva { get; set; }
    }
}
