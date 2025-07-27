using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using PetroleraManager.Entities;

namespace PetroleraManager.DataAccess
{

    public class ComprobantesComprasDetalleDataAccess : EntityManagerDataAccess<COMPROBANTESCOMPRASDETALLE, ComprobantesComprasDetalleExtendedView, ComprobantesComprasDetalleParameters, DataModelContext>
    {
        public List<InventarioDetalleExtendedView> ReadDetalleByIdComprobante(Guid idComprobante)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<COMPROBANTESCOMPRASDETALLE>(this.EntityName)
                             .Include("PRODUCTOS")
                             .Where(x => x.ComprobantesID.Equals(idComprobante))

                            select new InventarioDetalleExtendedView
                            {
                                ID = t.ID,
                                ProductoID  = t.ProductoID.Value,
                                Descripcion = t.PRODUCTOS.Descripcion,
                                //ProductoLoteID = t.ProductoLoteID,
                                Cantidad = t.Cantidad,
                                Lote = String.Empty,
                                //FechaVencimiento 
                                Observacion = t.Observacion,
                                //TipoMovimiento 
                                //Iva 
                                //MontoImpuestoInternoCompra 
                                //ImpuestoInternoCompra 
                                //Exento
                                UsaLote = t.PRODUCTOS.UsaLote
                            };

                return query.ToList();
            }
        }

        public List<ComprobantesComprasDetalleExtendedView> ReadComprasPorArticulo(ComprobantesComprasParameters param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<COMPROBANTESCOMPRASDETALLE>(this.EntityName)
                                .Include("COMPROBANTESCOMPRAS")
                                .Include("COMPROBANTESCOMPRAS.TIPOSCOMPROBANTES")
                                .Include("COMPROBANTESCOMPRAS.PROVEEDORES")
                                .Where(x => (x.COMPROBANTESCOMPRAS.ProveedoresID.Value.Equals(param.ProveedoresID) || param.ProveedoresID.Equals(Guid.Empty))
                                        && (x.ProductoID.Value.Equals(param.ProductoID) || param.ProductoID.Equals(Guid.Empty))
                                    //&& x.COMPROBANTESCOMPRAS.FechaComprobante
                                        && x.PRODUCTOS.TipoProductoID != CrossCutting.DatosDiscretos.TIPOPRODUCTO.Conceptos
                                        && x.COMPROBANTESCOMPRAS.Activo.Equals(true))
                             .OrderBy(x => x.COMPROBANTESCOMPRAS.ProveedoresID)

                            select new ComprobantesComprasDetalleExtendedView
                            {
                                ProveedorID = t.COMPROBANTESCOMPRAS.ProveedoresID.Value,
                                ProveedorRazonSocial = t.COMPROBANTESCOMPRAS.PROVEEDORES.Descripcion,
                                FechaComprobante = t.COMPROBANTESCOMPRAS.FechaComprobante,
                                TipoComprobanteDescripcion = t.COMPROBANTESCOMPRAS.TIPOSCOMPROBANTES.Descripcion,
                                NroComprobante = t.COMPROBANTESCOMPRAS.LetraComprobante + t.COMPROBANTESCOMPRAS.NroComprobante,
                                Descripcion = t.PRODUCTOS.Descripcion,
                                Cantidad = t.Cantidad,
                                PrecioUnitario = t.PrecioUnitario,
                            };

                return query.ToList();
            }
        }
    }
}