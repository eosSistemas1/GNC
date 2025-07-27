using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Data.Objects;
using PL.Fwk.Entities;

namespace PL.Fwk.DataAccess
{
    public interface IEntityReaderDataAccess<E> :IDisposable
        where E : EntityObject, IIdentifiable

    {
        E Read(Guid id);        
        ViewEntity ReadView(Guid id);
        List<E> ReadAll();
        List<ViewEntity> ReadListView();
    }


}
