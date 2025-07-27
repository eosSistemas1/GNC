using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    public class UsuariosParameters : ParametersEntity
    {
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
