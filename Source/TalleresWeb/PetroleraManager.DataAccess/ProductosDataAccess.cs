using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using PetroleraManager.Entities;
using PL.Fwk.Entities;

namespace PetroleraManager.DataAccess
{

    public class ProductosDataAccess : EntityManagerDataAccess<PRODUCTOS, ProductosExtendedView, ProductosParameters, DataModelContext>
    {
        public List<ProductosExtendedView> ReadConceptoByCodigo(String param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PRODUCTOS>(this.EntityName)
                              .Include("PRODUCTOLOTE")
                              .Where(x => x.Codigo == param && x.Activo == true && x.TipoProductoID == CrossCutting.DatosDiscretos.TIPOPRODUCTO.Conceptos)
                              .OrderByDescending(x => x.FechaAlta)

                            select new ProductosExtendedView
                            {
                                ID = t.ID,
                                CodigoProducto = t.Codigo,
                                ProductoLoteID = t.PRODUCTOLOTE.FirstOrDefault().ID,
                                Descripcion = t.Descripcion,
                            };

                return query.ToList();
            }
        }
        public List<ProductosExtendedView> ReadProductoByCodigo(String param)
        {
            using (var context = this.GetEntityContext())
            {
               var query = from t in context.CreateQuery<PRODUCTOS>(this.EntityName)
                              .Include("PRODUCTOLOTE")
                               .Where(x => x.Codigo.Equals(param) && x.Activo == true
                                    && x.TipoProductoID != CrossCutting.DatosDiscretos.TIPOPRODUCTO.Conceptos)
                              .OrderByDescending(x => x.FechaAlta)
                
                            select new ProductosExtendedView
                            {
                                ID = t.ID,
                                CodigoProducto = t.Codigo,
                                ProductoLoteID = t.PRODUCTOLOTE.FirstOrDefault().ID,
                                Descripcion = t.Descripcion,
                                UsaLote = t.UsaLote
                            };

                return query.ToList();
            }
        }
        public List<ProductosExtendedView> ReadProductoSimpleByCodigo(String param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PRODUCTOS>(this.EntityName)
                               .Include("PRODUCTOLOTE")
                                .Where(x => x.Codigo.Equals(param) && x.Activo == true
                                     && x.TipoProductoID == CrossCutting.DatosDiscretos.TIPOPRODUCTO.Simples)
                               .OrderByDescending(x => x.FechaAlta)

                            select new ProductosExtendedView
                            {
                                ID = t.ID,
                                CodigoProducto = t.Codigo,
                                ProductoLoteID = t.PRODUCTOLOTE.FirstOrDefault().ID,
                                Descripcion = t.Descripcion,
                                UsaLote = t.UsaLote
                            };

                return query.ToList();
            }
        }

        public List<ProductosExtendedView> ReadConceptosByDescripcion(String param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PRODUCTOS>(this.EntityName)
                              .Include("PRODUCTOLOTE")
                              .Where(x => x.Descripcion.Contains(param) && x.Activo == true && x.TipoProductoID == CrossCutting.DatosDiscretos.TIPOPRODUCTO.Conceptos)
                              .OrderByDescending(x => x.FechaAlta)
                            
                              select new ProductosExtendedView
                              {
                                  ID = t.ID,
                                  CodigoProducto = t.Codigo,
                                  ProductoLoteID = t.PRODUCTOLOTE.FirstOrDefault().ID,
                                  Descripcion = t.Descripcion,

                              };

                return query.ToList();
            }
        }
        public List<ProductosExtendedView> ReadProductoByDescripcion(String param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PRODUCTOS>(this.EntityName)
                              .Where(x => x.Descripcion.Contains(param) && x.Activo == true && x.TipoProductoID != CrossCutting.DatosDiscretos.TIPOPRODUCTO.Conceptos)
                              .OrderByDescending(x => x.FechaAlta)

                            select new ProductosExtendedView
                            {
                                ID = t.ID,
                                CodigoProducto = t.Codigo,
                                Descripcion = t.Descripcion
                            };

                return query.ToList();
            }
        }
        public List<ProductosExtendedView> ReadProductoSimpleByDescripcion(String param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PRODUCTOS>(this.EntityName)
                              .Where(x => x.Descripcion.Contains(param) && x.Activo == true && x.TipoProductoID == CrossCutting.DatosDiscretos.TIPOPRODUCTO.Simples)
                              .OrderByDescending(x => x.FechaAlta)

                            select new ProductosExtendedView
                            {
                                ID = t.ID,
                                CodigoProducto = t.Codigo,
                                Descripcion = t.Descripcion
                            };

                return query.ToList();
            }
        }

        public override PRODUCTOS Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PRODUCTOS>(this.EntityName)
                            .Include("BASEIMPONIBLECOMPRAS")
                            .Include("IMPUESTOINTERNOCOMPRAS")
                            .Include("BASEIMPONIBLEVENTAS")
                            .Include("IMPUESTOINTERNOVENTAS")
                            .Include("PRODUCTOLOTE")
                            .Where(x => x.ID.Equals(id) && x.Activo == true)
                            select t;

                return query.ToList().FirstOrDefault();
            }
        }


        public PRODUCTOS ReadByProductoID(Guid ID)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PRODUCTOS>(this.EntityName)
                            .Include("BASEIMPONIBLECOMPRAS")
                            .Include("IMPUESTOINTERNOCOMPRAS")
                            .Include("BASEIMPONIBLEVENTAS")
                            .Include("IMPUESTOINTERNOVENTAS")
                            .Include("PRODUCTOSCOMPONENTES")
                            .Where(x => x.ID.Equals(ID) && x.Activo == true)
                            select t;

                return query.ToList().FirstOrDefault();
            }
        }

        public override ViewEntity Add(PRODUCTOS entity)
        {
            var item = Read(entity.ID);
           // var items = ReadProductosByCodigo(entity.Codigo);

            if (item == null)
            {
               // if (items.Count == 0)
               // {
                    //si es nuevo lo agrego
                    return base.Add(entity);
                //}
                //else
                //{
                //    throw new Exception("Ya existe un producto con ese código.");
                //}
            }
            else
            {
                //Boolean modificar = true;

                //foreach (PRODUCTOS p in items)
                //{
                //    if (p.ID != entity.ID) modificar = false;
                //}

                //if (modificar)
                //{
                    Update(entity);
                    return null;
                //}
                //else
                //{
                //    throw new Exception("Ya existe un producto con ese código.");
                //}
            }
        }
        private List<PRODUCTOS> ReadProductosByCodigo(String codigo)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PRODUCTOS>(this.EntityName)
                             .Where(x => x.Codigo == codigo)

                            select t;
                return query.ToList();
            }
        }

        public override List<ProductosExtendedView> ReadExtendedView(ProductosParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PRODUCTOS>(this.EntityName)
                             .Include("PRODUCTOLOTE")
                             .Where(x => x.Descripcion.Contains(paramentersEntity.Descripcion) && x.Activo == true && x.TipoProductoID != CrossCutting.DatosDiscretos.TIPOPRODUCTO.Conceptos)

                            select new ProductosExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };

                return query.ToList();
            }
        }

        public List<ProductosExtendedView> ReadProductosSimples(ProductosParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PRODUCTOS>(this.EntityName)
                             .Include("PRODUCTOLOTE")
                             .Where(x => x.Descripcion.Contains(paramentersEntity.Descripcion) && x.Activo == true && x.TipoProductoID == CrossCutting.DatosDiscretos.TIPOPRODUCTO.Simples)

                            select new ProductosExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };

                return query.ToList();
            }
        }

        public List<ProductosExtendedView> ReadProductosCompuestos(ProductosParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PRODUCTOS>(this.EntityName)
                             .Include("PRODUCTOLOTE")
                             .Where(x => x.Descripcion.Contains(paramentersEntity.Descripcion) && x.Activo == true && x.TipoProductoID == CrossCutting.DatosDiscretos.TIPOPRODUCTO.Compuestos)

                            select new ProductosExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion
                            };

                return query.ToList();
            }
        }
    }
}