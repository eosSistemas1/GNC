using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetroleraManager.DataAccess;
using PL.Fwk.BusinessLogic;
using PetroleraManager.Entities;

namespace PetroleraManager.Logic
{
    public class ProductosComponentesLogic : EntityManagerLogic<PRODUCTOSCOMPONENTES, ProductosCompuestosExtendedView, ProductosCompuestosParameters, ProductosComponentesDataAccess>
    {

        public List<ProductosCompuestosExtendedView> ReadAllByProductoCompuestoID(Guid IDProductoCompuesto)
        {
            ProductosComponentesDataAccess oa = new ProductosComponentesDataAccess();
            return oa.ReadAllByProductoCompuestoID(IDProductoCompuesto);
        }
    }
}
