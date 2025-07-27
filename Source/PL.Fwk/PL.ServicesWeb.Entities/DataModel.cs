using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace PL.ServicesWeb.Entities
{
    public partial class T_MENU : IIdentifiable
    {
        public T_MENU()
        {
            _ID = Guid.NewGuid();
        }
    }


    public partial class T_SERVICIOS : IIdentifiable
    {
        public T_SERVICIOS()
        {
            _ID = Guid.NewGuid();
        }

    }


    public partial class ROLES : IIdentifiable
    {
        public ROLES()
        {
            _ID = Guid.NewGuid();
        }

    }


    public partial class USUARIOS : IIdentifiable
    {
        public USUARIOS()
        {
            _ID = Guid.NewGuid();
        }
    }

}
