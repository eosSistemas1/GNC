using System;
using System.Linq;
using PL.Fwk.DataAccess;
using PL.Fwk.Entities;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class MarcasValvulasDataAccess : EntityManagerDataAccess<MarcasValvulas, MarcasValvulasExtendedView, MarcasValvulasParameters, TalleresWebEntities>
    {
        public ViewEntity ReadByDescripcion(String marcaValvulas)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<MarcasValvulas>(this.EntityName)
                            .Where(x => x.Descripcion == marcaValvulas)
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