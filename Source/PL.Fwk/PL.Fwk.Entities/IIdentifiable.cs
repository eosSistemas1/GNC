using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PL.Fwk.Entities
{
    public interface IIdentifiable
    {
        Guid ID { get; set; }
        String Descripcion { get; }

    }
}
