using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using PetroleraManager.Entities;

namespace PetroleraManager.DataAccess
{

    public class ObleasLibresDataAccess : EntityManagerDataAccess<OBLEASLIBRES, ObleasLibresExtendedView, ObleasLibresParameters, DataModelContext>
    {
        public OBLEASLIBRES ReadNumeroLibre()
        {
            using (var context = this.GetEntityContext())
            {
                var query = (from t in context.CreateQuery<OBLEASLIBRES>(this.EntityName)
                            orderby t.Descripcion
                             select t).Take(1);

                return query.FirstOrDefault();
            }
        }
    }
}