using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace PetroleraManager.Entities
{
    public class ProveedoresParameters:ParametersEntity
    {
        public int Codigo { get; set; }
        public String Descripcion { get; set; }
    }
}
