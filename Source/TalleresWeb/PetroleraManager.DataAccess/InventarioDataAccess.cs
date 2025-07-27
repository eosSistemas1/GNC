using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using PetroleraManager.Entities;

namespace PetroleraManager.DataAccess
{

    public class InventarioDataAccess : EntityManagerDataAccess<INVENTARIO, InventarioExtendedView, InventarioParameters, DataModelContext>
    {
        public INVENTARIO ReadRemitoDetallado(Guid ID)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<INVENTARIO>(this.EntityName)
                              .Include("INVENTARIODETALLE")
                              .Include("CLIENTES")
                              .Include("CLIENTES.CONDICIONES")
                              .Include("CLIENTES.LOCALIDADES")
                              .Include("CLIENTES.REGIVA")
                              .Include("INVENTARIODETALLE.PRODUCTOS")
                              .Where(x => x.ID.Equals(ID)
                                          && x.Activo.Equals(true))
                             select t;

                return entity.FirstOrDefault();
            }

        }

        public List<InventarioExtendedView> ReadComprasPorProveedor(InventarioParameters param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<INVENTARIO>(this.EntityName)
                                .Include("TIPOSCOMPROBANTES")
                                .Include("PROVEEDORES")
                                .Where(x => (x.ProveedoresID.Value.Equals(param.ProveedoresID) || param.ProveedoresID.Equals(Guid.Empty))
                                        && (x.Fecha >= param.FechaD || param.FechaD == CrossCutting.DatosDiscretos.GetDinamyc.MinDatetime)
                                        && (x.Fecha <= param.FechaH || param.FechaH == CrossCutting.DatosDiscretos.GetDinamyc.MaxDatetime)
                                        && x.TiposComprobantesID.Value.Equals(DatosDiscretos.TIPOCOMPROBANTE.RemitoCompra)
                                        && x.Activo.Equals(true))
                                .OrderBy(x => x.ProveedoresID).ThenByDescending(x => x.Fecha)
                                

                            select new InventarioExtendedView
                            {
                                ProveedorID = t.ProveedoresID.Value,
                                ProveedorRazonSocial = t.PROVEEDORES.Descripcion,
                                FechaComprobante = t.Fecha,
                                TipoComprobanteDescripcion = t.TIPOSCOMPROBANTES.Descripcion,
                                NroComprobante = "X" + t.NroComprobante,
                                MontoComprobante = t.MontoComprobante.Value
                            };

                return query.ToList();
            }
        }

        public List<InventarioExtendedView> ReadVentasPorCliente(InventarioParameters param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<INVENTARIO>(this.EntityName)
                                .Include("TIPOSCOMPROBANTES")
                                .Include("CLIENTES")
                                .Where(x => (x.ClientesID.Value.Equals(param.ClientesID) || param.ClientesID.Equals(Guid.Empty))
                                        && (x.Fecha >= param.FechaD || param.FechaD == CrossCutting.DatosDiscretos.GetDinamyc.MinDatetime)
                                        && (x.Fecha <= param.FechaH || param.FechaH == CrossCutting.DatosDiscretos.GetDinamyc.MaxDatetime)
                                        && x.TiposComprobantesID.Value.Equals(DatosDiscretos.TIPOCOMPROBANTE.RemitoVenta)
                                        && x.Activo.Equals(true))
                             .OrderBy(x => x.ClientesID).ThenByDescending(x => x.Fecha)

                            select new InventarioExtendedView
                            {
                                ClientesID = t.ClientesID.Value,
                                ClientesRazonSocial = t.CLIENTES.Descripcion,
                                FechaComprobante = t.Fecha,
                                TipoComprobanteDescripcion = t.TIPOSCOMPROBANTES.Descripcion,
                                NroComprobante = "X" + t.NroComprobante,
                                MontoComprobante = t.MontoComprobante.Value
                            };

                return query.ToList();
            }
        }
    }
}