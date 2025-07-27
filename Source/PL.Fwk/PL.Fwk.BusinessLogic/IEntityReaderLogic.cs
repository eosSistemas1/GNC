using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;
using System.Data.Objects.DataClasses;

namespace PL.Fwk.BusinessLogic
{
    public interface IEntityReaderLogic<E>
        where E: EntityObject
    {
        E Read(Guid id);
        ViewEntity ReadView(Guid id);
        List<E> ReadAll();
        List<ViewEntity> ReadListView();
    }
}
