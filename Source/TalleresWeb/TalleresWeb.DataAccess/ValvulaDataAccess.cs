using PL.Fwk.DataAccess;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class ValvulaDataAccess : EntityManagerDataAccess<Valvula, ValvulaExtendedView, ValvulaParameters, TalleresWebEntities>
    {
        #region Methods
        public override List<ViewEntity> ReadListView()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Valvula>(this.EntityName)

                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };

                return query.OrderBy(t => t.Descripcion).ToList();
            }
        }

        public override List<ValvulaExtendedView> ReadExtendedView(ValvulaParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Valvula>(this.EntityName)
                            .Where(x => x.Descripcion.Contains(paramentersEntity.Descripcion))
                            .OrderBy(o => o.Descripcion)
                            select new ValvulaExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion,
                            };

                return query.ToList();
            }
        }

        /*  public List<Valvula> ReadByCodigoHomologacion(String codHomologacion)
          {
              using (var context = this.GetEntityContext())
              {
                  var query = from t in context.CreateQuery<Valvula>(this.EntityName)
                      .Where(x => x.Descripcion.Equals(codHomologacion))
                              select t;

                  return query.ToList();
              }
          }*/
        public List<Valvula> ReadDetalladoByID(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Valvula>(this.EntityName)
                            .Where(x => x.ID.Equals(id))
                            select t;

                return query.ToList();
            }
        }
        public List<Valvula> ReadByCodigoHomologacion(String codHomologacion)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Valvula>(this.EntityName)
                .Include(nameof(MarcasValvulas))
                    .Where(x => x.Descripcion.Contains(codHomologacion))
                            select t;

                return query.ToList();
            }
        }

        public List<String> ReadListCodigosHomologacion(String codigoHomologacion)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Valvula>(this.EntityName)
                            .Where(c => c.Descripcion.Contains(codigoHomologacion))
                            select t;

                return query.Select(x => x.Descripcion).ToList();
            }
        }

      
        public List<string> ReadValvulaYMarca(string codigoHomologacion)
        {
            using (var context = this.GetEntityContext())
            {
                string ch = codigoHomologacion.ToUpper();
                var query = context.Valvula
                            .Include("MarcasValvulas")
                            .Where(r => r.Descripcion == ch);

                List<string> valor = new List<string>();
                foreach (var item in query)
                {
                    String marca = item.MarcasValvulas != null && !String.IsNullOrWhiteSpace(item.MarcasValvulas.Descripcion) ? item.MarcasValvulas.Descripcion : "Sin Marca";
                    valor.Add($"{marca}");
                }

                return valor;
            }
        }


        #endregion
    }
}