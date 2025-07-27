using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using PetroleraManager.Entities;

namespace PetroleraManager.DataAccess
{

    public class ComprobantesComprasDataAccess : EntityManagerDataAccess<COMPROBANTESCOMPRAS, ComprobantesComprasExtendedView, ComprobantesComprasParameters, DataModelContext>
    {
        public List<ComprobantesComprasExtendedView> ReadCompProveedorByNro(String nroComprobante, 
                                                                            Guid idCliente,
                                                                            Guid idTipoComprobante)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<COMPROBANTESCOMPRAS>(this.EntityName)
                             .Include("PRODUCTOS")
                             .Where(x => x.NroComprobante.Equals(nroComprobante) 
                                    && x.ProveedoresID.Equals(idCliente)
                                    && x.TiposComprobantesID.Equals(idTipoComprobante)
                                    && x.Activo.Equals(true))

                            select new ComprobantesComprasExtendedView
                            {
                                ID = t.ID,
                                //ProductoID = t.ProductoID.Value,
                                ////ProductoLoteID = t.ProductoLoteID,
                                //Cantidad = t.Cantidad,
                                //Lote = String.Empty,
                                ////FechaVencimiento 
                                //Observacion = t.Observacion,
                                ////TipoMovimiento 
                                ////Iva 
                                ////MontoImpuestoInternoCompra 
                                ////ImpuestoInternoCompra 
                                ////Exento
                                //UsaLote = t.PRODUCTOS.UsaLote
                            };

                return query.ToList();
            }
        }

        public List<ComprobantesComprasExtendedView> ReadComprasPorProveedor(ComprobantesComprasParameters param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<COMPROBANTESCOMPRAS>(this.EntityName)
                                .Include("TIPOSCOMPROBANTES")
                                .Include("PROVEEDORES")
                                .Where(x => (x.ProveedoresID.Value.Equals(param.ProveedoresID) || param.ProveedoresID.Equals(Guid.Empty))
                                    //         && x.ProveedoresID.Equals(idProveedor)
                                    //         && x.TiposComprobantesID.Equals(idTipoComprobante)
                                        && x.Activo.Equals(true))
                             .OrderBy(x => x.ProveedoresID)

                            select new ComprobantesComprasExtendedView
                            {
                                //ID = t.ID,
                                ProveedorID = t.ProveedoresID.Value,
                                ProveedorRazonSocial = t.PROVEEDORES.Descripcion,
                                FechaComprobante = t.FechaComprobante,
                                TipoComprobanteDescripcion = t.TIPOSCOMPROBANTES.Descripcion,
                                NroComprobante = t.LetraComprobante + t.NroComprobante,
                                Percepcion1 = t.PercepcionesMonto1.Value,
                                Percepcion2 = t.PercepcionesMonto2.Value,
                                Percepcion3 = t.PercepcionesMonto3.Value,
                                Percepcion4 = t.PercepcionesMonto4.Value,
                                Percepcion5 = t.PercepcionesMonto5.Value,
                                MontoComprobante = t.MontoComprobante
                            };

                return query.ToList();
            }
        }
    }
}