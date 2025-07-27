using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;
using System.Data.Objects.DataClasses;
using System.Data.Objects;

namespace PL.Fwk.DataAccess
{
    public class EntityManagerDataAccess<E, EV, P, OC> : EntityReaderDataAccess<E, OC>, IEntityManagerDataAccess<E, EV, P>
        where E : EntityObject, IIdentifiable
        where EV : ViewEntity
        where P : ParametersEntity
        where OC : ObjectContext, new()
    {

        public Func<E, ViewEntity> CreateViewEntity;

        #region Contructor
        public EntityManagerDataAccess()
        {
            this.CreateViewEntity = e => new ViewEntity() { ID = e.ID, Descripcion = e.Descripcion };

        }
        #endregion

        #region Methods
        public virtual ViewEntity Add(E entity)
        {
            using (OC context = this.GetEntityContext())
            {
                context.AddObject(this.EntityName, entity);
                context.SaveChanges();
            }

            return new ViewEntity();
        }
        public virtual void Update(E entity)
        {
            using (OC context = this.GetEntityContext())
            {
                if (entity.EntityKey == null)
                {
                    entity.EntityKey = context.CreateEntityKey(this.EntityName, entity);
                }

                context.Attach(entity);
                context.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
                context.SaveChanges();
                context.AcceptAllChanges();
            }
        }
        public virtual void Delete(Guid id)
        {
            using (OC context = this.GetEntityContext())
            {
                E entity = this.Read(context, id);
                context.DeleteObject(entity);
                context.SaveChanges();
            }
        }



        public virtual List<EV> ReadExtendedView(P paramentersEntity)
        {
            throw new NotImplementedException();
        }


        public virtual List<ViewEntity> ReadListView(P parametersEntity)
        {
            using (OC context = this.GetEntityContext())
            {
                var filters = this.CreateFilters(parametersEntity);
                var lista = context.CreateQuery<E>(this.EntityName).Where(filters);


                return lista.Select<E, ViewEntity>(this.CreateViewEntity)
                    .OrderBy(e => e.Descripcion)
                    .ToList();
            }
        }

        public virtual Func<E, bool> CreateFilters(P parametersEntity)
        {
            Func<E, bool> filters = f =>
                (parametersEntity.ID == Guid.Empty || f.ID == parametersEntity.ID)
                &&
                (String.IsNullOrEmpty(parametersEntity.Descripcion) || f.Descripcion.ToLower().Contains(parametersEntity.Descripcion.ToLower()));

            return filters;

        }
        #endregion

    }
}
