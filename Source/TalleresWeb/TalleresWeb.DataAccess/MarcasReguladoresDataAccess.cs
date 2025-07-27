using System;
using System.Linq;
using PL.Fwk.DataAccess;
using PL.Fwk.Entities;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class MarcasReguladoresDataAccess : EntityManagerDataAccess<MarcasRegulador, MarcasReguladoresExtendedView, MarcasReguladoresParameters, TalleresWebEntities>
    {
        public ViewEntity ReadByDescripcion(String marcaReguladores)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<MarcasRegulador>(this.EntityName)
                            .Where(x => x.Descripcion == marcaReguladores)
                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };
                return query.FirstOrDefault();
            }
        }

    }
}