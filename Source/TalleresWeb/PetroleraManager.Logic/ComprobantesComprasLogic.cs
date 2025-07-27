using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetroleraManager.DataAccess;
using PL.Fwk.BusinessLogic;
using PetroleraManager.Entities;

namespace PetroleraManager.Logic
{
    public class ComprobantesComprasLogic : EntityManagerLogic<COMPROBANTESCOMPRAS, ComprobantesComprasExtendedView, ComprobantesComprasParameters, ComprobantesComprasDataAccess>
    {
        public List<ComprobantesComprasExtendedView> ReadCompProveedorByNro(String nroComprobante, 
                                                                       Guid idProveedor,
                                                                       Guid idTipoComprobante)
        {
            ComprobantesComprasDataAccess oa = new ComprobantesComprasDataAccess();
            return oa.ReadCompProveedorByNro(nroComprobante, 
                                             idProveedor,
                                             idTipoComprobante);
        }

        public List<ComprobantesComprasExtendedView> ReadComprasPorProveedor(ComprobantesComprasParameters param)
        {
            ComprobantesComprasDataAccess oa = new ComprobantesComprasDataAccess();
            return oa.ReadComprasPorProveedor(param);
        }
    }
}

