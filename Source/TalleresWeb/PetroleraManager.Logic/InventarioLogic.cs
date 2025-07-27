using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetroleraManager.DataAccess;
using PL.Fwk.BusinessLogic;
using PetroleraManager.Entities;

namespace PetroleraManager.Logic
{
    public class InventarioLogic : EntityManagerLogic<INVENTARIO, InventarioExtendedView, InventarioParameters, InventarioDataAccess>
    {
        public INVENTARIO ReadRemitoDetallado(Guid id)
        {
            InventarioDataAccess oa = new InventarioDataAccess();
            return oa.ReadRemitoDetallado(id);
        }

        public List<InventarioExtendedView> ReadComprasPorProveedor(InventarioParameters param)
        {
            InventarioDataAccess oa = new InventarioDataAccess();
            return oa.ReadComprasPorProveedor(param);
        }

        public List<InventarioExtendedView> ReadVentasPorCliente(InventarioParameters param)
        {
            InventarioDataAccess oa = new InventarioDataAccess();
            return oa.ReadVentasPorCliente(param);
        }
    }
}
