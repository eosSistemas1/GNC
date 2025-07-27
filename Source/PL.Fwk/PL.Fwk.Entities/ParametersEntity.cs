using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PL.Fwk.Entities
{
    [Serializable]
    public class ParametersEntity
    {
        public const String IDPropertieName = "ID";
        private Guid _ID;

        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }


        public const String DescripcionPropertieName = "Descripcion";
        private String _descripcion;

        public String Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
    }
}
