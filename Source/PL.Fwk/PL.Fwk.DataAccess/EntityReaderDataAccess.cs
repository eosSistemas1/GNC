using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Data.Objects;
using PL.Fwk.Entities;

namespace PL.Fwk.DataAccess
{
    public class EntityReaderDataAccess<E, OC>:EntityFrameworkDataAccess<OC>, IEntityReaderDataAccess<E>
        where E: EntityObject, IIdentifiable
        where OC:ObjectContext, new()
    {

        protected String EntityName
        {
            get { return String.Format("{0}", typeof(E).Name); }
        }

        #region Methods 
            
            public virtual E Read(Guid id)
            {
                using (OC context = this.GetEntityContext() )
                {
                  return  this.Read(context, id);
                }
            }

            /// <summary>
            /// Retorna la entidad
            /// </summary>
            /// <param name="context"></param>
            /// <param name="id"></param>
            /// <returns></returns>
            public virtual E Read(OC context, Guid id)
            {
                E entity = context.CreateQuery<E>(this.EntityName).Where(e => e.ID == id).FirstOrDefault<E>();
                return entity;
            }
            /// <summary>
            /// Retorna todas las Entidades
            /// </summary>
            /// <returns></returns>
            public virtual List<E> ReadAll()
            {
                using (OC context = this.GetEntityContext() )
                {
                    List<E> entities = context.CreateQuery<E>(this.EntityName).ToList();
                    return entities;
                }
               
            }
            
            /// <summary>
            /// Retorna una sola instancia de la entidad solicitada en una vista
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public virtual ViewEntity ReadView(Guid id)
        {
            using (OC context = this.GetEntityContext())
            {
                var query = from e in context.CreateQuery<E>(this.EntityName)
                                .Where(x => x.ID == id)

                            select new ViewEntity
                            {
                                ID = e.ID,
                                Descripcion = e.Descripcion,

                            };

                return query.FirstOrDefault() ;
            }
        }


            /// <summary>
            /// Retorna una sola instancia de la entidad solicitada en una vista
            /// </summary>
            /// <param name="id"></param>
            /// <returns>Lista de ViewEntities</returns>
            public virtual List<ViewEntity> ReadListView()
        {
            using (OC context = this.GetEntityContext())
            {
                var query = from e in context.CreateQuery<E>(this.EntityName)
                                
                            select new ViewEntity
                            {
                                ID = e.ID,
                                Descripcion = e.Descripcion,

                            };

                return query.ToList();
            }
        }


        #endregion

        #region IDisposable Members

        public void Dispose()
        {
           
        }

        #endregion
    }
}
