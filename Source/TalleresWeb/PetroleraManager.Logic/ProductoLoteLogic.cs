using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetroleraManager.DataAccess;
using PL.Fwk.BusinessLogic;
using PetroleraManager.Entities;

namespace PetroleraManager.Logic
{
    public class ProductoLoteLogic : EntityManagerLogic<PRODUCTOLOTE, ProductoLoteExtendedView, ProductoLoteParameters, ProductoLoteDataAccess>
    {
      
        public void AddProductoLoteUsaLote(PRODUCTOLOTE param)
        {
            ProductoLoteDataAccess oa = new ProductoLoteDataAccess();
            oa.AddProductoLoteUsaLote(param);
        }

        public PRODUCTOLOTE ReadByID(Guid ID)
        {
            ProductoLoteDataAccess oa = new ProductoLoteDataAccess();
            return oa.ReadByID(ID);
        }

        public PRODUCTOLOTE ReadByProductoID(Guid ProductoID)
        {
            ProductoLoteDataAccess oa = new ProductoLoteDataAccess();
            return oa.ReadByProductoID(ProductoID);
        }
    }
}
