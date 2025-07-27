using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace PL.Fwk.DataAccess
{
    public class EntityFrameworkDataAccess<OC>
        where OC : ObjectContext, new()
    {
      
        protected OC GetEntityContext()
        {
            OC context = new OC();
            return context;
        }
    }
}
