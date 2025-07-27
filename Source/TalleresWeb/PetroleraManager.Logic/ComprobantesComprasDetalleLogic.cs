using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetroleraManager.DataAccess;
using PL.Fwk.BusinessLogic;
using PetroleraManager.Entities;

namespace PetroleraManager.Logic
{
    public class ComprobantesComprasDetalleLogic : EntityManagerLogic<COMPROBANTESCOMPRASDETALLE, ComprobantesComprasDetalleExtendedView, ComprobantesComprasDetalleParameters, ComprobantesComprasDetalleDataAccess>
    {
        public List<InventarioDetalleExtendedView> ReadDetalleByIdComprobante(Guid idComprobante)
        {
            ComprobantesComprasDetalleDataAccess oa = new ComprobantesComprasDetalleDataAccess();
            return oa.ReadDetalleByIdComprobante(idComprobante);
        }
    }
}
