using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetroleraManager.DataAccess;
using PL.Fwk.BusinessLogic;
using PetroleraManager.Entities;
using System.Transactions;

namespace PetroleraManager.Logic
{
    public class LotesLogic : EntityManagerLogic<LOTES,LotesExtendedView,LotesParameters,LotesDataAccess>
    {
      
        public LOTES ReadLoteActivo()
        {
            LotesDataAccess oa = new LotesDataAccess();
            return oa.ReadLoteActivo();
        }

        public LOTES ValidarNroObleaLote(Decimal nroOblea)
        {
            LotesDataAccess oa = new LotesDataAccess();
            return oa.ValidarNroObleaLote(nroOblea);
        }

        public bool LoteValido(Decimal nroObleaDesde, Decimal nroObleaHasta)
        {
            int valido = 0;
            LotesDataAccess oa = new LotesDataAccess();
            
            var lote = oa.LoteValido(nroObleaDesde, nroObleaHasta);
            if(lote) valido++;

            var minOblea = ValidarNroObleaLote(nroObleaDesde);
            if (minOblea != null) valido++;

            var maxOblea = ValidarNroObleaLote(nroObleaHasta);
            if (maxOblea != null) valido++;

            Boolean valor = valido > 0 ? false : true;
            return valor;
        }
    }
}
