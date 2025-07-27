using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using PL.Fwk.Entities;

namespace PL.Fwk.BusinessLogic
{
    public interface IEntityManagerLogic<E,EV,P>:IEntityReaderLogic<E>
        where E: EntityObject
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
