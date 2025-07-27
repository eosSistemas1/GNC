using System;
using System.Linq;
using PL.Fwk.DataAccess;
using PL.Fwk.Entities;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class MarcasCilindrosDataAccess : EntityManagerDataAccess<MarcasCilindros, MarcasCilindrosExtendedView, MarcasCilindrosParameters, TalleresWebEntities>
    {
        public ViewEntity ReadByDescripcion(String marcaCilindro)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<MarcasCilindros>(this.EntityName)
                            .Where(x => x.Descripcion == marcaCilindro)
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