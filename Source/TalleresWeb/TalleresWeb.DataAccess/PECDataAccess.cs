using PL.Fwk.DataAccess;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class PECDataAccess : EntityManagerDataAccess<PEC, PECExtendedView, PECParameters, TalleresWebEntities>
    {
        #region Methods
        public override List<ViewEntity> ReadListView()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PEC>(this.EntityName)

                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = "(" + t.Descripcion + ") " + t.RazonSocialPEC
                            };
                return query.ToList();
            }
        }
        public List<PEC> ReadDetalladoByID(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PEC>(this.EntityName)
                            .Include("Localidades")
                            .Include("Localidades.Provincias")
                            .Where(x => x.ID.Equals(id))
                            select t;

                return query.ToList();
            }
        }

        #endregion
    }
}