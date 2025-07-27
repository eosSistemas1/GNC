using PL.Fwk.DataAccess;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class ReguladoresDataAccess : EntityManagerDataAccess<Reguladores, ReguladoresExtendedView, ReguladoresParameters, TalleresWebEntities>
    {
        #region Methods        
        public override List<ViewEntity> ReadListView()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Reguladores>(this.EntityName)

                            select new ViewEntity
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };

                return query.OrderBy(t => t.Descripcion).ToList();
            }
        }

        public override List<ReguladoresExtendedView> ReadExtendedView(ReguladoresParameters param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Reguladores>(this.EntityName)
                            .Where(x => x.Descripcion.Contains(param.Descripcion))

                            select new ReguladoresExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };


                return query.ToList();
            }
        }

        public List<String> ReadListCodigosHomologacion(String codigoHomologacion)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Reguladores>(this.EntityName)
                            .Where(c => c.Descripcion.Contains(codigoHomologacion))
                            select t;

                return query.Select(x => x.Descripcion).ToList();
            }
        }

        public List<Reguladores> ReadDetalladoByID(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Reguladores>(this.EntityName)
                            .Where(x => x.ID.Equals(id))
                            select t;

                return query.ToList();
            }
        }


        public List<Reguladores> ReadByCodigoHomologacion(String codHomologacion)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Reguladores>(this.EntityName)
                .Include(nameof(MarcasRegulador))
                    .Where(x => x.Descripcion.Contains(codHomologacion))
                            select t;

                return query.ToList();
            }
        }



        public List<String> ReadListReguladores(String codigoHomologacion)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Reguladores>(this.EntityName)
                            .Where(c => c.Descripcion.Contains(codigoHomologacion))
                            select t;

                return query.Select(x => x.Descripcion).ToList();
            }
        }

        public List<string> ReadReguladorYMarca(string codigoHomologacion)
        {
            using (var context = this.GetEntityContext())
            {
                string ch = codigoHomologacion.ToUpper();
                var query = context.Reguladores
                            .Include("MarcasReguladores")
                            .Where(r => r.Descripcion == ch);

                List<string> valor = new List<string>();
                foreach (var item in query)
                {
                    String marca = item.MarcasRegulador != null && !String.IsNullOrWhiteSpace(item.MarcasRegulador.Descripcion) ? item.MarcasRegulador.Descripcion : "Sin Marca";
                    valor.Add($"{marca}");
                }

                return valor;
            }
        }


        #endregion
    }
}