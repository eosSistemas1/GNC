using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    [Serializable]
    public class PHCilindrosConsultaView : PHCilindrosView
    {
        public Guid? IDEstadoPH { get; set; }
        public bool ModificaEstado { get; set; }
    }
}
