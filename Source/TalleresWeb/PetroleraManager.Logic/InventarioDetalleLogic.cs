using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetroleraManager.DataAccess;
using PL.Fwk.BusinessLogic;
using PetroleraManager.Entities;

namespace PetroleraManager.Logic
{
    public class InventarioDetalleLogic : EntityManagerLogic<INVENTARIODETALLE, InventarioDetalleExtendedView, InventarioDetalleParameters, InventarioDetalleDataAccess>
    {
        public List<InventarioDetalleExtendedView> ReadComprasPorArticulo(InventarioDetalleParameters param)
        {
            InventarioDetalleDataAccess oa = new InventarioDetalleDataAccess();
            return oa.ReadComprasPorArticulo(param);
        }

        public List<InventarioDetalleExtendedView> ReadVentasPorArticulo(InventarioDetalleParameters param)
        {
            InventarioDetalleDataAccess oa = new InventarioDetalleDataAccess();
            return oa.ReadVentasPorArticulo(param);
        }
    }
}
