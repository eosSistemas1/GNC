using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetroleraManager.DataAccess;
using PL.Fwk.BusinessLogic;
using PetroleraManager.Entities;

namespace PetroleraManager.Logic
{
    public class ProductosLogic : EntityManagerLogic<PRODUCTOS,ProductosExtendedView,ProductosParameters,ProductosDataAccess>
    {

        public List<ProductosExtendedView> ReadConceptosByDescripcion(String param)
        {
            ProductosDataAccess oa = new ProductosDataAccess();
            return oa.ReadConceptosByDescripcion(param);
        }
        public List<ProductosExtendedView> ReadProductoByDescripcion(String param)
        {
            ProductosDataAccess oa = new ProductosDataAccess();
            return oa.ReadProductoByDescripcion(param);
        }
        public List<ProductosExtendedView> ReadProductoSimpleByDescripcion(String param)
        {
            ProductosDataAccess oa = new ProductosDataAccess();
            return oa.ReadProductoSimpleByDescripcion(param);
        }

        public List<ProductosExtendedView> ReadConceptoByCodigo(String param)
        {
            ProductosDataAccess oa = new ProductosDataAccess();
            return oa.ReadConceptoByCodigo(param);
        }
        public List<ProductosExtendedView> ReadProductoByCodigo(String param)
        {
            ProductosDataAccess oa = new ProductosDataAccess();
            return oa.ReadProductoByCodigo(param);
        }
        public List<ProductosExtendedView> ReadProductoSimpleByCodigo(String param)
        {
            ProductosDataAccess oa = new ProductosDataAccess();
            return oa.ReadProductoSimpleByCodigo(param);
        }


        public List<ProductoLoteExtendedView> ReadProductoLoteByCodigo(String param)
        {
            ProductoLoteDataAccess oa = new ProductoLoteDataAccess();
            return oa.ReadProductoLoteByCodigo(param);
        }
        public List<ProductoLoteExtendedView> ReadProductoLoteByDescripcion(String param)
        {
            ProductoLoteDataAccess oa = new ProductoLoteDataAccess();
            return oa.ReadProductoLoteByDescripcion(param);
        }

        public PRODUCTOS ReadByProductoID(Guid ID)
        {
            ProductosDataAccess oa = new ProductosDataAccess();
            return oa.ReadByProductoID(ID);
        }

        public List<ProductosExtendedView> ReadInformeValorizacionExistencias(ProductosParameters param)
        {
            ProductoLoteDataAccess oa = new ProductoLoteDataAccess();
            return oa.ReadInformeValorizacionExistencias(param);
        }

        public List<ProductosExtendedView> ReadProductosSimples(ProductosParameters param)
        {
            ProductosDataAccess oa = new ProductosDataAccess();
            return oa.ReadProductosSimples(param);
        }
        public List<ProductosExtendedView> ReadProductosCompuestos(ProductosParameters param)
        {
            ProductosDataAccess oa = new ProductosDataAccess();
            return oa.ReadProductosCompuestos(param);
        }
    }
}
