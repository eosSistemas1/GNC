using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    public class S_AccesosCUParameters : ParametersEntity
    {
        public String UsuarioDsc { get; set; }
        public String Descripcion { get; set; }
        public int IdRol { get; set; }
        public int IdPadre { get; set; }

    }
}
