using PL.Fwk.BusinessLogic;
using System.Collections.Generic;
using TalleresWeb.Entities;
using PL.Fwk.Entities;
using CrossCutting.DatosDiscretos;
using System;
using System.Transactions;
using TalleresWeb.DataAccess;
using System.Linq;

namespace TalleresWeb.Logic
{
    public class DespachoLogic : EntityManagerLogic<DESPACHO, DespachoExtendedView, DespachoParameters, DespachoDataAccess>
    {
        #region Properties
        private DespachoDetalleLogic despachoDetalleLogic;
        private DespachoDetalleLogic DespachoDetalleLogic
        {
            get
            {
                if (despachoDetalleLogic == null) despachoDetalleLogic = new DespachoDetalleLogic();
                return despachoDetalleLogic;
            }
        }

        private ObleasLogic obleasLogic;
        private ObleasLogic ObleasLogic
        {
            get
            {
                if (obleasLogic == null) obleasLogic = new ObleasLogic();
                return obleasLogic;
            }
        }

        private PHCilindrosLogic phCilindrosLogic;
        private PHCilindrosLogic PHCilindrosLogic
        {
            get
            {
                if (phCilindrosLogic == null) phCilindrosLogic = new PHCilindrosLogic();
                return phCilindrosLogic;
            }
        }

        //private PedidosLogic pedidosLogic;
        //private PedidosLogic PedidosLogic
        //{
        //    get
        //    {
        //        if (pedidosLogic == null) pedidosLogic = new ObleasLogic();
        //        return pedidosLogic;
        //    }
        //}
        #endregion

        #region Methods
        public override ViewEntity Add(DESPACHO entity)
        {
            using (TransactionScope ss = new TransactionScope())
            {
                try
                {
                    entity.Activo = true;
                    var detalle = entity.DESPACHODETALLE;

                    foreach (var item in detalle)
                    {
                        if (item.IdOblea.HasValue) this.ObleasLogic.CambiarEstado(item.IdOblea.Value, ESTADOSFICHAS.Despachada, $"Despachada: {DateTime.Now.ToString()}", entity.IdUsuario);
                        if (item.IdPHCilindro.HasValue) this.PHCilindrosLogic.CambiarEstado(item.IdPHCilindro.Value, EstadosPH.Despachada, $"Despachada: {DateTime.Now.ToString()}" , entity.IdUsuario);
                        //if (item.IdPedido.HasValue) this.PedidosLogic.CambiarEstado(item.IdPedido.Value, ESTADOSPEDIDOS.Despachado, $"Despachada: {DateTime.Now.ToString()}" , entity.IdUsuario); 
                    }

                    ViewEntity despacho = base.Add(entity);

                    ss.Complete();

                    return despacho;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    ss.Dispose();
                }
            }
        }

        public List<DespachoExtendedView> ReadTramitesPendientesByZona(string zona)
        {
            List<DespachoExtendedView> despachoEV = this.EntityDataAccess.ReadTramitesPendientesByZona(zona);

            return despachoEV;
        }

        public void InicioFinDespacho(String numeroDespacho, Guid idUsuario)
        {
            using (TransactionScope ss = new TransactionScope())
            {
                try
                {
                    DESPACHO despacho = this.EntityDataAccess.ReadDespachoByNumero(numeroDespacho);

                    if (despacho == null)
                        throw new Exception("No existe el despacho ingresado");

                    if (!despacho.FechaHoraSalida.HasValue && !despacho.FechaHoraLlegada.HasValue)
                    {
                        despacho.FechaHoraSalida = DateTime.Now;
                    }
                    else if (despacho.FechaHoraSalida.HasValue && !despacho.FechaHoraLlegada.HasValue)
                    {
                        var detalle = despacho.DESPACHODETALLE;

                        foreach (var item in detalle)
                        {
                            if (item.IdOblea.HasValue) this.ObleasLogic.CambiarEstado(item.IdOblea.Value, ESTADOSFICHAS.Entregada, $"Entregada despacho nro {despacho.Numero}", idUsuario);
                            //if (item.IdPH.HasValue) this.PHLogic.CambiarEstado(item.IdPH.Value, ESTADOSPH.Despachada, null , entity.IdUsuario); 
                            //if (item.IdPedido.HasValue) this.PedidosLogic.CambiarEstado(item.IdPedido.Value, ESTADOSPEDIDOS.Despachado, null , entity.IdUsuario); 
                        }

                        despacho.FechaHoraLlegada = DateTime.Now;
                    }

                    this.Update(despacho);

                    ss.Complete();
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    ss.Dispose();
                }
            }
        }

        public List<DespachoEnCursoView> ReadDespachosEnCurso()
        {
            return EntityDataAccess.ReadDespachosEnCurso();
        }

        public List<DespachoEnCursoView> ReadDespachosEnCursoEntreFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            return EntityDataAccess.ReadDespachosEnCursoEntreFechas(fechaDesde, fechaHasta);
        }
        
        public void EliminarDespacho(String nroDespacho, Guid idUsuario)
        {
            try
            {
                var despacho = this.EntityDataAccess.ReadDespachoByNumero(nroDespacho);

                foreach (var item in despacho.DESPACHODETALLE)
                {
                    this.RechazarItemDespacho(idUsuario, despacho, item);
                }

                despacho.Activo = false;
                this.Update(despacho);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public void RechazarDespachoTaller(int numeroDespacho, Guid idTaller, Guid idUsuario)
        {
            using (TransactionScope ss = new TransactionScope())
            {
                try
                {
                    DESPACHO despacho = this.EntityDataAccess.ReadDespachoByNumero(numeroDespacho.ToString());

                    var despachoDetalle = despacho.DESPACHODETALLE.Where(d => d.IdTaller == idTaller).ToList();

                    foreach (var item in despachoDetalle)
                    {
                        this.RechazarItemDespacho(idUsuario, despacho, item);
                    }

                    ss.Complete();
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    ss.Dispose();
                }
            }
        }

        private void RechazarItemDespacho(Guid idUsuario, DESPACHO despacho, DESPACHODETALLE despachoDetalle)
        {
            if (despachoDetalle.IdOblea.HasValue) this.ObleasLogic.CambiarEstado(despachoDetalle.IdOblea.Value, ESTADOSFICHAS.Finalizada, $"Rechazo despacho nro {despacho.Numero}", idUsuario);
            if (despachoDetalle.IdPHCilindro.HasValue) this.PHCilindrosLogic.CambiarEstado(despachoDetalle.IdPHCilindro.Value, EstadosPH.Finalizada, $"Rechazo despacho nro {despacho.Numero}", idUsuario); 
            //if (item.IdPedido.HasValue) this.PedidosLogic.CambiarEstado(item.IdPedido.Value, ESTADOSPEDIDOS.Finalizada, null , entity.IdUsuario); 

            this.DespachoDetalleLogic.Delete(despachoDetalle.ID);
        }
        #endregion
    }
}
