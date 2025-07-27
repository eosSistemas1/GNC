using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace PetroleraManager.Entities
{
    public class UsuariosParameters : ParametersEntity
    {
        public String UserName { get; set; }
        public String Password { get; set; }
    }
}
