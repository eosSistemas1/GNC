using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using PetroleraManager.Entities;
using PL.Fwk.Entities;

namespace PetroleraManager.DataAccess
{

    public class TransportesDataAccess : EntityManagerDataAccess<TRANSPORTES, TransportesExtendedView, TransportesParameters, DataModelContext>
    {
        public override List<TransportesExtendedView> ReadExtendedView(TransportesParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<TRANSPORTES>(this.EntityName)
                            .Where(x => x.Descripcion.Contains(paramentersEntity.Descripcion) && x.Activo == true)
                            .OrderBy(o=> o.Descripcion)
                            select new TransportesExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion,
                                Domicilio = t.Domicilio,
                                Telefono = t.Telefono,
                                Activo = t.Activo
                            };

                return query.ToList();
            }
        }

        public override List<ViewEntity> ReadListView()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<TRANSPORTES>(this.EntityName)
                             .Where(x => x.Activo == true)
                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };
                return query.ToList();
            }
        }

        public override List<TRANSPORTES> ReadAll()
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<TRANSPORTES>(this.EntityName)
                             .Where(x => x.Activo == true)
                             select t;

                return entity.ToList();
            }
        }

        public override TRANSPORTES Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<TRANSPORTES>(this.EntityName)
                             .Where(x => x.ID == id && x.Activo == true)
                             select t;

                return entity.FirstOrDefault();
            }
        }
    }
}