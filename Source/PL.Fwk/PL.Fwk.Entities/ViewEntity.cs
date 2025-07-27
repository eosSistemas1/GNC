using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PL.Fwk.Entities
{
    [Serializable]
    public class ViewEntity : IIdentifiable
    {
        public ViewEntity()
        {

        }
        public ViewEntity(Guid id)
        {
            this.ID = id;
        }
        public ViewEntity(Guid id, String descripcion)
        {
            this.ID = id;
            this.Descripcion = descripcion;
        }
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
