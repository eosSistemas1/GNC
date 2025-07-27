using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TalleresWeb.Entities  
{
    public class PHCilindrosVerificarCodigosParameter
    {
        public Guid ID { get; set; }
        public bool SolicitarRevision { get; set; }
        public string NumeroSerieCilLeido { get; set; }
        public string CodigoHomologacionCilLeido { get; set; }
        public string NumeroSerieValLeido { get; set; }
        public string CodigoHomologacionValLeido { get; set; }
        public Guid UsuarioID { get; set; }
    }
}
