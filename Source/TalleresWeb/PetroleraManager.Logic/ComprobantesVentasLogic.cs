using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetroleraManager.DataAccess;
using PL.Fwk.BusinessLogic;
using PetroleraManager.Entities;

namespace PetroleraManager.Logic
{
    public class ComprobantesVentasLogic : EntityManagerLogic<COMPROBANTESVENTAS, ComprobantesVentasExtendedView, ComprobantesVentasParameters, ComprobantesVentasDataAccess>
    {
        public List<ComprobantesVentasExtendedView> ReadCompClienteByNro(String nroComprobante,
                                                                       Guid idCliente,
                                                                       Guid idTipoComprobante)
        {
            ComprobantesVentasDataAccess oa = new ComprobantesVentasDataAccess();
            return oa.ReadCompClienteByNro(nroComprobante,
                                             idCliente,
                                             idTipoComprobante);
        }

        public COMPROBANTESVENTAS ReadComprobanteDetallado(Guid id)
        {
            ComprobantesVentasDataAccess oa = new ComprobantesVentasDataAccess();
            return oa.ReadComprobanteDetallado(id);
        }
    }
}
