using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    public class UsuarioBasicView
    {
        public Guid ID { get; set; }
        public String Descripcion { get; set; }
        public Guid? IDTaller { get; set; }
        public String RazonSocialTaller { get; set; }
        public String MatriculaTaller { get; set; }
    }
}
