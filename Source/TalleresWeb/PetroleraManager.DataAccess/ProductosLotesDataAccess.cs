using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using PetroleraManager.Entities;

namespace PetroleraManager.DataAccess
{

    public class ProductoLoteDataAccess : EntityManagerDataAccess<PRODUCTOLOTE, ProductoLoteExtendedView, ProductoLoteParameters, DataModelContext>
    {
        public List<ProductoLoteExtendedView> ReadProductoLoteByCodigo(String param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PRODUCTOLOTE>(this.EntityName)
                              .Include("PRODUCTOS")
                              .Where(x => x.PRODUCTOS.Codigo.Equals(param)
                                                        && x.PRODUCTOS.Activo == true
                                                        && x.Activo == true
                                                        && x.PRODUCTOS.TipoProductoID != CrossCutting.DatosDiscretos.TIPOPRODUCTO.Conceptos
                                                        && x.CantidadExistente > 0)
                              .OrderByDescending(x => x.PRODUCTOS.FechaAlta)

                            select new ProductoLoteExtendedView
                            {
                                ID = t.PRODUCTOS.ID,
                                CodigoProducto = t.PRODUCTOS.Codigo,
                                ProductoLoteID = t.ID,
                                Descripcion = t.PRODUCTOS.Descripcion,
                                Lote = t.Descripcion,
                                //FechaVencimiento = t.FechaVencimiento.GetValueOrDefault(DateTime.MinValue.Date),
                                CantidadExistente = t.CantidadExistente.Value
                            };

                return query.ToList();
            }
        }
        public List<ProductoLoteExtendedView> ReadProductoLoteByDescripcion(String param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PRODUCTOLOTE>(this.EntityName)
                              .Include("PRODUCTOS")
                              .Where(x => x.PRODUCTOS.Descripcion.Contains(param)
                                                        && x.PRODUCTOS.Activo == true
                                                        && x.Activo == true
                                                        && x.PRODUCTOS.TipoProductoID != CrossCutting.DatosDiscretos.TIPOPRODUCTO.Conceptos
                                                        //&& x.CantidadExistente > 0
                                                        )
                              .OrderByDescending(x => x.PRODUCTOS.FechaAlta)

                            select new ProductoLoteExtendedView
                            {
                                ID = t.PRODUCTOS.ID,
                                CodigoProducto = t.PRODUCTOS.Codigo,
                                ProductoLoteID = t.ID,
                                Descripcion = t.PRODUCTOS.Descripcion,
                                Lote = t.Descripcion,
                                //FechaVencimiento = t.FechaVencimiento.GetValueOrDefault(DateTime.MinValue.Date),
                                CantidadExistente = t.CantidadExistente.Value
                            };

                return query.ToList();
            }
        }
        
        public PRODUCTOLOTE ReadByID(Guid ProductoID)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PRODUCTOLOTE>(this.EntityName)
                            .Include("PRODUCTOS")
                            .Include("PRODUCTOS.BASEIMPONIBLECOMPRAS")
                            .Include("PRODUCTOS.BASEIMPONIBLEVENTAS")
                            .Include("PRODUCTOS.IMPUESTOINTERNOCOMPRAS")
                            .Include("PRODUCTOS.IMPUESTOINTERNOVENTAS")
                            .Where(x => x.ID.Equals(ProductoID) && x.Activo == true)
                            select t;

                return query.ToList().FirstOrDefault();
            }
        }
        public PRODUCTOLOTE ReadByProductoID(Guid ProductoID)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PRODUCTOLOTE>(this.EntityName)
                            .Include("PRODUCTOS")
                            .Include("PRODUCTOS.BASEIMPONIBLECOMPRAS")
                            .Include("PRODUCTOS.BASEIMPONIBLEVENTAS")
                            .Include("PRODUCTOS.IMPUESTOINTERNOCOMPRAS")
                            .Include("PRODUCTOS.IMPUESTOINTERNOVENTAS")
                            .Where(x => x.ProductoID.Equals(ProductoID) && x.Activo == true)
                            select t;

                return query.ToList().FirstOrDefault();
            }
        }

        public void AddProductoLoteUsaLote(PRODUCTOLOTE entity)
        { 
            /* si el producto usa lote 
             *          crear un registro con el nuevo lote y vencimiento
             * si el producto no usa lote, busca registro generico y le suma lo comprado
             */ 

            ProductosDataAccess producto = new ProductosDataAccess();
            if (producto.Read(entity.ProductoID).UsaLote)
            {
                Add(entity);
            }
            else
            {
                try
                {
                    var registro = ReadByProductoID(entity.ProductoID);
                    registro.Cantidad += entity.Cantidad;
                    registro.CantidadExistente += entity.CantidadExistente;
                    registro.FechaCompra = entity.FechaCompra;
                    registro.PrecioCompra = entity.PrecioCompra;
                    Update(registro);
                }
                catch
                {
                    throw new Exception("El producto ingresado no existe o esta inactivo.");
                }
            }
        }

        public List<ProductosExtendedView> ReadInformeValorizacionExistencias(ProductosParameters param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PRODUCTOLOTE>(this.EntityName)
                             .Include("PRODUCTOS")
                             .Include("TIPOPRODUCTO")
                             .Where(x => //x.Descripcion.Contains(param.Descripcion) 
                                            x.Activo == true
                                            && x.PRODUCTOS.Activo == true
                                            && x.PRODUCTOS.TipoProductoID != CrossCutting.DatosDiscretos.TIPOPRODUCTO.Conceptos
                                            && x.PRODUCTOS.TipoProductoID != CrossCutting.DatosDiscretos.TIPOPRODUCTO.Compuestos
                                            && x.PRODUCTOS.TipoProductoID != Guid.Empty)
                            //.OrderBy(x => x.PRODUCTOS.FABRICANTES.Descripcion)
                            //.OrderBy(x => x.PRODUCTOS.RUBROS.Descripcion)
                            .OrderBy(x => x.PRODUCTOS.Descripcion)
                            select new ProductosExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.PRODUCTOS.Descripcion,
                                CodigoProducto = t.PRODUCTOS.Codigo,
                                TipoProductoID = t.PRODUCTOS.TipoProductoID,
                                TipoProductoDescripcion = t.PRODUCTOS.TIPOPRODUCTO.Descripcion,
                                CantidadMinima = t.PRODUCTOS.StockMin.Value,
                                CantidadAComprar = t.PRODUCTOS.StockMax.Value,
                                CantidadExistente = t.CantidadExistente.Value,
                                ImporteCompra = t.PrecioCompra.Value,
                                ImporteTotalLote = t.CantidadExistente.Value * t.PrecioCompra.Value
                            };

                return query.ToList();
            }
        }
    }
}