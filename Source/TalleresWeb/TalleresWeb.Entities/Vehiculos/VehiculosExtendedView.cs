using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    public class VehiculosExtendedView : ViewEntity
    {
        public String MarcaVehiculo { get; set; }
        public String ModeloVehiculo { get; set; }
        public int AnioVehiculo { get; set; }
        public Boolean EsInyeccionVehiculo { get; set; }
        public Guid IdDuenioVehiculo { get; set; }
        public Guid IdUso { get; set; }
    }
}
