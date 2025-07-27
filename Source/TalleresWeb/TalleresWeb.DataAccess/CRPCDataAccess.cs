using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{

    public class CRPCDataAccess : EntityManagerDataAccess<CRPC, CRPCExtendedView, CRPCParameters, TalleresWebEntities>
    {
        public override CRPC Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<CRPC>(this.EntityName)
                             .Where(x => x.ID == id)
                             select t;

                return entity.FirstOrDefault();
            }
        }
    }
}