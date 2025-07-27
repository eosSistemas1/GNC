using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;
using System.Data.Objects.DataClasses;

namespace PL.Fwk.DataAccess
{
    public interface IEntityManagerDataAccess<E,EV,P> : IDisposable, IEntityReaderDataAccess<E>
        where E : EntityObject, IIdentifiable
        where EV : ViewEntity
        where P : ParametersEntity
    {
        ViewEntity Add(E entity);
        void Update(E entity);
        void Delete(Guid id);

        List<EV> ReadExtendedView(P parametersEntity);
        List<ViewEntity> ReadListView(P parametersEntity);
    }
}
