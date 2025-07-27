using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using PetroleraManager.Entities;

namespace PetroleraManager.DataAccess
{

    public class InventarioDetalleDataAccess : EntityManagerDataAccess<INVENTARIODETALLE, InventarioDetalleExtendedView, InventarioDetalleParameters, DataModelContext>
    {
        public List<InventarioDetalleExtendedView> ReadComprasPorArticulo(InventarioDetalleParameters param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<INVENTARIODETALLE>(this.EntityName)
                                .Include("INVENTARIO")
                                .Include("INVENTARIO.TIPOSCOMPROBANTES")
                                .Include("INVENTARIO.PROVEEDORES")
                                .Where(x => (x.INVENTARIO.ProveedoresID.Value.Equals(param.ProveedoresID) || param.ProveedoresID.Equals(Guid.Empty))
                                        && (x.ProductoID.Value.Equals(param.ProductoID) || param.ProductoID.Equals(Guid.Empty))
                                        && x.INVENTARIO.TiposComprobantesID == CrossCutting.DatosDiscretos.TIPOCOMPROBANTE.RemitoCompra
                                        && x.PRODUCTOS.TipoProductoID != CrossCutting.DatosDiscretos.TIPOPRODUCTO.Conceptos
                                        && (x.PRODUCTOS.RubroID.Value.Equals(param.RubroID) || param.RubroID.Equals(Guid.Empty))
                                        && (x.INVENTARIO.Fecha >= param.FechaD || param.FechaD == CrossCutting.DatosDiscretos.GetDinamyc.MinDatetime)
                                        && (x.INVENTARIO.Fecha <= param.FechaH || param.FechaH == CrossCutting.DatosDiscretos.GetDinamyc.MaxDatetime)
                                        && x.INVENTARIO.Activo.Equals(true))
                             .OrderBy(x => x.INVENTARIO.ProveedoresID)

                            select new InventarioDetalleExtendedView
                            {
                                ProveedorID = t.INVENTARIO.ProveedoresID.Value,
                                ProveedorRazonSocial = t.INVENTARIO.PROVEEDORES.Descripcion,
                                FechaComprobante = t.INVENTARIO.Fecha,
                                TipoComprobanteDescripcion = t.INVENTARIO.TIPOSCOMPROBANTES.Descripcion,
                                NroComprobante = "X" + t.INVENTARIO.NroComprobante,
                                Descripcion = t.PRODUCTOS.Descripcion,
                                Cantidad = t.Cantidad,
                                PrecioUnitario = t.PrecioUnitario.Value,
                            };

                return query.ToList();
            }
        }

        public List<InventarioDetalleExtendedView> ReadVentasPorArticulo(InventarioDetalleParameters param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<INVENTARIODETALLE>(this.EntityName)
                                .Include("INVENTARIO")
                                .Include("INVENTARIO.TIPOSCOMPROBANTES")
                                .Include("INVENTARIO.CLIENTES")
                                .Where(x => (x.INVENTARIO.ClientesID.Value.Equals(param.ClientesID) || param.ClientesID.Equals(Guid.Empty))
                                        && (x.ProductoID.Value.Equals(param.ProductoID) || param.ProductoID.Equals(Guid.Empty))
                                        && x.INVENTARIO.TiposComprobantesID == CrossCutting.DatosDiscretos.TIPOCOMPROBANTE.RemitoVenta
                                        && x.PRODUCTOS.TipoProductoID != CrossCutting.DatosDiscretos.TIPOPRODUCTO.Conceptos
                                        && (x.PRODUCTOS.RubroID.Value.Equals(param.RubroID) || param.RubroID.Equals(Guid.Empty))
                                        && (x.INVENTARIO.Fecha >= param.FechaD || param.FechaD == CrossCutting.DatosDiscretos.GetDinamyc.MinDatetime)
                                        && (x.INVENTARIO.Fecha <= param.FechaH || param.FechaH == CrossCutting.DatosDiscretos.GetDinamyc.MaxDatetime)
                                        && x.INVENTARIO.Activo.Equals(true))
                             .OrderBy(x => x.INVENTARIO.ProveedoresID)

                            select new InventarioDetalleExtendedView
                            {
                                ClienteID = t.INVENTARIO.ClientesID.Value,
                                ClienteRazonSocial = t.INVENTARIO.CLIENTES.Descripcion,
                                FechaComprobante = t.INVENTARIO.Fecha,
                                TipoComprobanteDescripcion = t.INVENTARIO.TIPOSCOMPROBANTES.Descripcion,
                                NroComprobante = "X" + t.INVENTARIO.NroComprobante,
                                Descripcion = t.PRODUCTOS.Descripcion,
                                Cantidad = t.Cantidad,
                                PrecioUnitario = t.PrecioUnitario.Value,
                            };

                return query.ToList();
            }
        }
    }
}