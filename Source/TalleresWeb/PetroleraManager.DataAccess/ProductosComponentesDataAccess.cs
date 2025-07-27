using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using PetroleraManager.Entities;
using PL.Fwk.Entities;

namespace PetroleraManager.DataAccess
{

    public class ProductosComponentesDataAccess : EntityManagerDataAccess<PRODUCTOSCOMPONENTES, ProductosCompuestosExtendedView, ProductosCompuestosParameters, DataModelContext>
    {
        public List<ProductosCompuestosExtendedView> ReadAllByProductoCompuestoID(Guid IDProductoCompuesto)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<PRODUCTOSCOMPONENTES>(this.EntityName)
                             .Include("PRODUCTOS1")
                             .Where(x => x.IDProductoCompuesto.Equals(IDProductoCompuesto))
                             select new ProductosCompuestosExtendedView
                             {
                                 ID = t.ID,
                                 IDProductoCompuesto = t.IDProductoCompuesto,
                                 IDProductoComponente = t.IDProductoCompone,
                                 DescProductoComponente = t.PRODUCTOS1.Descripcion,
                                 Cantidad = t.Cantidad,
                                 PrecioUComponente = t.PRODUCTOS1.PrecioCompra.Value,
                                 PrecioTComponente = t.PRODUCTOS1.PrecioCompra.Value * t.Cantidad
                             };


                return entity.ToList();
            }
        }
    }
}