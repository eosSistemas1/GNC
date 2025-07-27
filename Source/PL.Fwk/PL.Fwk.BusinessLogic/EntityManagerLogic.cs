using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using PL.Fwk.Entities;
using PL.Fwk.DataAccess;

namespace PL.Fwk.BusinessLogic
{
    public class EntityManagerLogic<E,EV, P, DA>:EntityReaderLogic<E,DA>, IEntityManagerLogic<E,EV,P>
        where E : EntityObject, IIdentifiable
        where EV: ViewEntity 
        where P:ParametersEntity
        where DA : IEntityManagerDataAccess<E,EV,P>, new()
    {
        public virtual ViewEntity Add(E entity)
        {
          return  this.EntityDataAccess.Add(entity);
        }
        public virtual void Update(E entity)
        {
            this.EntityDataAccess.Update(entity);
        }
        public virtual void Delete(Guid id)
        {
            this.EntityDataAccess.Delete(id);
        }


        public List<EV> ReadExtendedView(P parametersEntity)
        {
            return this.EntityDataAccess.ReadExtendedView(parametersEntity);
        }

        public List<ViewEntity> ReadListView(P parametersEntity)
        {
            return this.EntityDataAccess.ReadListView(parametersEntity);
        }

    }
}
