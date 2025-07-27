using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using PetroleraManager.Entities;

namespace PetroleraManager.DataAccess
{

    public class LotesDataAccess : EntityManagerDataAccess<LOTES, LotesExtendedView, LotesParameters, DataModelContext>
    {
        public override List<LotesExtendedView> ReadExtendedView(LotesParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                if (paramentersEntity.NroObleaDesde != 0)
                {

                    var query = from t in context.CreateQuery<LOTES>(this.EntityName)
                                 .Where(x => (x.NroObleaDesde <= paramentersEntity.NroObleaDesde))

                                select new LotesExtendedView
                                {
                                    ID = t.ID,
                                    NroObleaDesde = t.NroObleaDesde,
                                    NroObleaHasta = t.NroObleaHasta,
                                    LoteActivo = t.LoteActivo
                                };

                    return query.OrderBy(x => x.NroObleaDesde).ToList();
                }
                else
                {
                    var query = from t in context.CreateQuery<LOTES>(this.EntityName)
                                select new LotesExtendedView
                                {
                                    ID = t.ID,
                                    NroObleaDesde = t.NroObleaDesde,
                                    NroObleaHasta = t.NroObleaHasta,
                                    LoteActivo = t.LoteActivo
                                };

                    return query.OrderBy(x => x.NroObleaDesde).ToList();
                }
            }
        }

        public void CambiaEstado(List<LOTES> lotes, Guid idLote) 
        {
            //var lotes = ReadAll();
            for(int i=0; i<lotes.Count(); i++)
            {
                lotes[i].LoteActivo = (idLote == lotes[i].ID);
                Update(lotes[i]);
            }
        }

        public LOTES ReadLoteActivo()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<LOTES>(this.EntityName)
                             .Where(x => x.LoteActivo == true)

                            select t;

                return query.FirstOrDefault();
            }
        }

        public LOTES ValidarNroObleaLote(Decimal nroOblea)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<LOTES>(this.EntityName)
                             .Where(x => x.NroObleaDesde <= nroOblea && x.NroObleaHasta >= nroOblea)

                            select t;

                return query.FirstOrDefault();
            }
        }

        public bool LoteValido(Decimal nroObleaDesde, Decimal nroObleaHasta)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<LOTES>(this.EntityName)
                             .Where(x => (x.NroObleaDesde <= nroObleaDesde && x.NroObleaHasta >= nroObleaHasta)
                                      || (x.NroObleaDesde >= nroObleaDesde && x.NroObleaHasta <= nroObleaHasta))

                            select t;

                return (!(query.Count()==0));
            }
        }
    }
}