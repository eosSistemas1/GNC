using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using TalleresWeb.Entities;
using PL.Fwk.Entities;

namespace TalleresWeb.DataAccess
{

    public class TiposOperacionesDataAccess : EntityManagerDataAccess<Operaciones, TiposOperacionesExtendedView, TiposOperacionesParameters, TalleresWebEntities>
    {

        public List<ViewEntity> ReadEVOperaciones()
        {
            using (var context = this.GetEntityContext())
            {
                String codigos = ".CMRXB";

                var query = from t in context.CreateQuery<Operaciones>(this.EntityName)
                    .Where(x => x.CodigoGestionEnte.Equals(".")
                                ||x.CodigoGestionEnte.Equals("C")
                                || x.CodigoGestionEnte.Equals("M")
                                || x.CodigoGestionEnte.Equals("R")
                                || x.CodigoGestionEnte.Equals("X")
                                || x.CodigoGestionEnte.Equals("B")
                                )

                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };

                return query.ToList();
            }
        }

        public List<ViewEntity> ReadEVMSDB()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Operaciones>(this.EntityName)
                    .Where(x => x.CodigoGestionEnte.Equals("M")
                             || x.CodigoGestionEnte.Equals("S")
                             || x.CodigoGestionEnte.Equals("D")
                             || x.CodigoGestionEnte.Equals("B")                    
                            )

                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };

                return query.ToList();
            }
        }
    }
}