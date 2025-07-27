using PL.Fwk.DataAccess;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class CilindrosDataAccess : EntityManagerDataAccess<Cilindros, CilindrosExtendedView, CilindrosParameters, TalleresWebEntities>
    {
        #region Methods

       /* public override List<CilindrosExtendedView> ReadExtendedView(CilindrosParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Cilindros>(this.EntityName)
                            .Where(x => x.Descripcion.Contains(paramentersEntity.Descripcion))
                            .OrderBy(o => o.Descripcion)
                            select new CilindrosExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion,
                            };

                return query.ToList();
            }
        }   */     

        public List<Cilindros> ReadByCodigoHomologacion(String codHomologacion)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Cilindros>(this.EntityName)
                            .Include(nameof(MarcasCilindros))
                    .Where(x => x.Descripcion.Equals(codHomologacion))
                            select t;

                return query.ToList();
            }
        }

        public List<string> ReadCilindroMarcaYCapacidad(string codigoHomologacion)
        {
            using (var context = this.GetEntityContext())
            {
                string ch = codigoHomologacion.ToUpper();
                var query = context.Cilindros
                            .Include("MarcasCilindros")
                            .Where(r => r.Descripcion == ch);

                List<string> valor = new List<string>();
                foreach (var item in query)
                {
                    String marca = item.MarcasCilindros != null && !String.IsNullOrWhiteSpace(item.MarcasCilindros.Descripcion) ? item.MarcasCilindros.Descripcion : "Sin Marca";
                    String capacidad = item.CapacidadCil.HasValue ? item.CapacidadCil.Value.ToString() : "0";
                    valor.Add($"{marca}|{capacidad}");
                }

                return valor;
            }
        }

        public List<String> ReadListCodigosHomologacion(String codigoHomologacion)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Cilindros>(this.EntityName)
                            .Where (c => c.Descripcion.Contains(codigoHomologacion))
                            select t;

                return query.Select(x => x.Descripcion).ToList();
            }
        }
        #endregion
    }
}