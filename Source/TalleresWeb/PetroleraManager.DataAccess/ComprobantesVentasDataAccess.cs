using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.DataAccess;
using PetroleraManager.Entities;

namespace PetroleraManager.DataAccess
{

    public class ComprobantesVentasDataAccess : EntityManagerDataAccess<COMPROBANTESVENTAS, ComprobantesVentasExtendedView, ComprobantesVentasParameters, DataModelContext>
    {
        public List<ComprobantesVentasExtendedView> ReadCompClienteByNro(String nroComprobante,
                                                                            Guid idCliente,
                                                                            Guid idTipoComprobante)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<COMPROBANTESVENTAS>(this.EntityName)
                             .Where(x => x.NroComprobante.Equals(nroComprobante)
                                    && x.ClientesID.Equals(idCliente)
                                    && x.TiposComprobantesID.Equals(idTipoComprobante)
                                    && x.Activo.Equals(true))

                            select new ComprobantesVentasExtendedView
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

        public COMPROBANTESVENTAS ReadComprobanteDetallado(Guid ID)
        {
            using (var context = this.GetEntityContext())
            {
                var entity = from t in context.CreateQuery<COMPROBANTESVENTAS>(this.EntityName)
                              .Include("COMPROBANTESVENTASDETALLE")
                              .Include("CLIENTES")
                              .Include("CLIENTES.CONDICIONES")
                              .Include("CLIENTES.LOCALIDADES")
                              .Include("CLIENTES.REGIVA")
                              .Include("COMPROBANTESVENTASDETALLE.PRODUCTOS")
                              .Include("COMPROBANTESVENTASDETALLE.PRODUCTOS.BASEIMPONIBLEVENTAS")
                              .Where(x => x.ID.Equals(ID)
                                          && x.Activo.Equals(true))
                             select t;

                return entity.FirstOrDefault();
            }

        }
    }
}