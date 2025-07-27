using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    public class TalleresParameters : ParametersEntity
    {
        public String Matricula { get; set; }
        public String Descripcion { get; set; }
    }
}
