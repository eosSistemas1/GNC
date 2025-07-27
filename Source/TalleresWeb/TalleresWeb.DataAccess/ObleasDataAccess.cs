using CrossCutting.DatosDiscretos;
using PL.Fwk.DataAccess;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class ObleasDataAccess : EntityManagerDataAccess<Obleas, ObleasExtendedView, ObleasParameters, TalleresWebEntities>
    {
        #region Methods

        public override Obleas Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Obleas>(this.EntityName)
                             .Include("ObleaHistoricoEstado")
                             .Where(x => x.ID == id)

                            select t;

                return query.FirstOrDefault();
            }
        }

        public override ViewEntity Add(Obleas entity)
        {
            ViewEntity oblea = base.Add(entity);

            ActualizarHistoricoEstadoOblea(entity.ID, entity.IdEstadoFicha, entity.ObservacionesFicha, entity.IdUsuarioAlta.Value);

            return oblea;
        }

        public Obleas ReadAllByNroObleaNueva(String nroObleaNueva)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Obleas>(this.EntityName)
                    .Where(x => x.NroObleaNueva.Equals(nroObleaNueva))
                            select t;

                return query.FirstOrDefault();
            }
        }

        public List<ObleasConsultarView> ReadObleasConsulta(DateTime fechaDesde,
                                                            DateTime fechaHasta,
                                                            string dominio,
                                                            string numeroOblea,
                                                            Guid tallerID)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Obleas>(this.EntityName)
                            .Where(x => (x.NroObleaNueva.Equals(numeroOblea) || String.IsNullOrEmpty(numeroOblea))
                                     && (x.Vehiculos.Descripcion.Equals(dominio) || String.IsNullOrEmpty(dominio))
                                     && (x.FechaHabilitacion >= fechaDesde && x.FechaHabilitacion <= fechaHasta)
                                     && (x.IdTaller == tallerID || tallerID == Guid.Empty))
                            select new ObleasConsultarView
                            {
                                ID = t.ID,
                                FechaHabilitacion = t.FechaHabilitacion.HasValue ? t.FechaHabilitacion.Value : GetDinamyc.MinDatetime,
                                Dominio = t.Vehiculos.Descripcion,
                                NombreyApellido = t.Clientes.Descripcion,
                                NroObleaAnterior = t.Descripcion,
                                NroObleaNueva = t.NroObleaNueva,
                                FechaAlta = t.FechaRealAlta,
                                Operacion = t.Operaciones.Descripcion
                            };

                return query.Take(50).ToList();
            }
        }        

        public List<ObleasParaCorregirDominioView> ReadFichaParaCorregirDominio(DateTime fechaDesde, DateTime fechaHasta, string numeroOblea, string dominioConError)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Obleas>(this.EntityName)
                            .Where(x => (x.FechaHabilitacion >= fechaDesde && x.FechaHabilitacion <= fechaHasta)
                                        && (x.NroObleaNueva.Equals(numeroOblea) || string.IsNullOrEmpty(numeroOblea))
                                        && (x.Vehiculos.Descripcion.Equals(dominioConError) || string.IsNullOrEmpty(dominioConError))                                      
                                        && (x.ObleaHistoricoEstado.Any(e => e.IDEstadoOblea==ESTADOSFICHAS.AprobadaConError))
                                     )
                            select new ObleasParaCorregirDominioView
                            {
                                ID = t.ID,
                                VehiculoID = t.IdVehiculo,
                                FechaHabilitacion = t.FechaHabilitacion.HasValue ? t.FechaHabilitacion.Value : GetDinamyc.MinDatetime,
                                DominioConError = t.Vehiculos.Descripcion,
                                Taller = t.Talleres.Descripcion,                                
                                NumeroOblea = t.NroObleaNueva,
                                IdObleaHistoricoEstado = t.ObleaHistoricoEstado.OrderByDescending(ie => ie.FechaHora)
                                                                                .FirstOrDefault(ie => ie.IDOblea == t.ID && ie.IDEstadoOblea == ESTADOSFICHAS.AprobadaConError).ID
                            };

                return query.ToList();
            }
        }

        public List<ObleasExtendedView> ReadAllConsultaEnBase(ObleasParameters param)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Obleas>(this.EntityName)
                            .Where(x => (x.NroObleaNueva.Equals(param.NroObleaNueva) || String.IsNullOrEmpty(param.NroObleaNueva))
                                     && (x.Vehiculos.Descripcion.Equals(param.Dominio) || String.IsNullOrEmpty(param.Dominio))
                                     && (x.Clientes.IdTipoDniCliente.Equals(param.TipoDocClienteID) || param.TipoDocClienteID.Equals(Guid.Empty))
                                     && (x.Clientes.NroDniCliente.Equals(param.NroDocCliente) || String.IsNullOrEmpty(param.NroDocCliente))
                                     )
                            select new ObleasExtendedView
                            {
                                ID = t.ID,
                                FechaHabilitacion = t.FechaHabilitacion.HasValue ? t.FechaHabilitacion.Value : GetDinamyc.MinDatetime,
                                Dominio = t.Vehiculos.Descripcion,
                                NombreyApellido = t.Clientes.Descripcion,
                                NroObleaAnterior = t.Descripcion,
                                NroObleaNueva = t.NroObleaNueva,
                            };

                return query.ToList();
            }
        }

        public Obleas ReadObleaByObleaCargaResultadosView(ObleaCargaResultadosView oblea)
        {
            using (var context = this.GetEntityContext())
            {
                int nroInternoOperacionTaller = int.Parse(oblea.NroInternoObleaTaller);
                String codigoTaller = oblea.CodigoTaller;

                var query = from t in context.CreateQuery<Obleas>(this.EntityName)
                                            .Include("ObleasCilindros")
                                            .Where(x => x.NroIntOperacTP.Value == nroInternoOperacionTaller
                                                     && x.Talleres.Descripcion == codigoTaller)
                            select t;

                return query.FirstOrDefault();
            }
        }

        public List<Obleas> ReadObleaByNumeroAnterior(String nroObleaAnterior)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Obleas>(this.EntityName)
                                             .Include(nameof(EstadosFichas))
                                             .Where(x => x.Descripcion == nroObleaAnterior)
                            select t;

                return query.ToList();
            }
        }

        /// <summary>
        /// Valida si existe un trámite pendiente para una oblea
        /// </summary>
        /// <param name="vehiculoDominio"></param>
        /// <returns></returns>
        public bool ExisteTramitePendienteParaElNroOblea(String nroObleaAnterior)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Obleas>(this.EntityName)
                                             .Where(x => (x.Descripcion == nroObleaAnterior
                                                            || x.NroObleaNueva == nroObleaAnterior)
                                                        && x.IdEstadoFicha != ESTADOSFICHAS.Entregada
                                                        && x.IdEstadoFicha != ESTADOSFICHAS.Eliminada)
                            select t;

                return query.Any();
            }
        }

        /// <summary>
        /// Valida si existe un trámite pendiente para un dominio
        /// </summary>
        /// <param name="vehiculoDominio"></param>
        /// <returns></returns>
        public bool ExisteTramitePendienteParaElDominio(string dominio)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Obleas>(this.EntityName)
                                             .Where(x => x.Vehiculos.Descripcion == dominio
                                                        && x.IdEstadoFicha != ESTADOSFICHAS.Entregada
                                                        && x.IdEstadoFicha != ESTADOSFICHAS.Eliminada)
                            select t;

                return query.Any();
            }
        }

        public Guid ActualizarHistoricoEstadoOblea(Guid obleaID, Guid idEstadoFicha, String observacion, Guid idUsuario)
        {
            using (var context = this.GetEntityContext())
            {
                Guid id = Guid.NewGuid();

                var query = String.Format("INSERT INTO ObleaHistoricoEstado(ID_ObleaHistoricoEstado, ID_Oblea, ID_EstadoOblea, Descripcion, FechaHoraEstado, ID_Usuario) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", id, obleaID, idEstadoFicha, observacion, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), idUsuario);

                context.ExecuteStoreCommand(query);

                return id;
            }
        }

        public List<ObleasExtendedView> ReadAllInformeEstadoObleas(ObleasParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Obleas>(this.EntityName)
                            .Where(x => (paramentersEntity.TallerID.Equals(Guid.Empty) || x.IdTaller.Equals(paramentersEntity.TallerID))
                                     && (paramentersEntity.EstadoFichaID.Equals(Guid.Empty) || x.IdEstadoFicha.Equals(paramentersEntity.EstadoFichaID))
                                     && (paramentersEntity.Dominio == null || paramentersEntity.Dominio == "" || x.Vehiculos.Descripcion == paramentersEntity.Dominio)
                                     && (paramentersEntity.NroObleaAnterior == null || paramentersEntity.NroObleaAnterior == "" || x.Descripcion == paramentersEntity.NroObleaAnterior)
                                     && (paramentersEntity.NroObleaNueva == null || paramentersEntity.NroObleaNueva == "" || x.NroObleaNueva == paramentersEntity.NroObleaNueva)
                                     && (paramentersEntity.FechaDesde == GetDinamyc.MinDatetime || x.FechaHabilitacion >= paramentersEntity.FechaDesde)
                                     && (paramentersEntity.FechaHasta == GetDinamyc.MaxDatetime || x.FechaHabilitacion <= paramentersEntity.FechaHasta)
                                     && (paramentersEntity.NroDocCliente == null || paramentersEntity.NroDocCliente == "" || x.Clientes.NroDniCliente == paramentersEntity.NroDocCliente)

                                    && (paramentersEntity.CodigoHomologacionRegulador == null || paramentersEntity.CodigoHomologacionRegulador == "" || x.ObleasReguladores.Any(r => r.ReguladoresUnidad.Reguladores.Descripcion == paramentersEntity.CodigoHomologacionRegulador))
                                    && (paramentersEntity.NumeroSerieRegulador == null || paramentersEntity.NumeroSerieRegulador == "" || x.ObleasReguladores.Any(r => r.ReguladoresUnidad.Descripcion == paramentersEntity.NumeroSerieRegulador))

                                    && (paramentersEntity.CodigoHomologacionCilindro == null || paramentersEntity.CodigoHomologacionCilindro == "" || x.ObleasCilindros.Any(r => r.CilindrosUnidad.Cilindros.Descripcion == paramentersEntity.CodigoHomologacionCilindro))
                                    && (paramentersEntity.NumeroSerieCilindro == null || paramentersEntity.NumeroSerieCilindro == "" || x.ObleasCilindros.Any(r => r.CilindrosUnidad.Descripcion == paramentersEntity.NumeroSerieCilindro))

                                    && (paramentersEntity.CodigoHomologacionValvula == null || paramentersEntity.CodigoHomologacionValvula == "" || x.ObleasCilindros.Any(r => r.ObleasValvulas.Any(v => v.Valvula_Unidad.Valvula.Descripcion == paramentersEntity.CodigoHomologacionCilindro)))
                                    && (paramentersEntity.NumeroSerieValvula == null || paramentersEntity.NumeroSerieValvula == "" || x.ObleasCilindros.Any(r => r.ObleasValvulas.Any(v => v.Valvula_Unidad.Descripcion.Contains(paramentersEntity.NumeroSerieValvula))))
                                    )
                            orderby t.Talleres.Zona, t.IdTaller, t.FechaHabilitacion descending
                            select new ObleasExtendedView
                            {
                                ID = t.ID,
                                NroInternoOpercion = t.NroIntOperacTP.Value,
                                Taller = t.Talleres.RazonSocialTaller,
                                FechaHabilitacion = t.FechaHabilitacion.HasValue ? t.FechaHabilitacion.Value : GetDinamyc.MinDatetime,
                                Descripcion = t.Descripcion,
                                Dominio = t.Vehiculos.Descripcion,
                                NombreyApellido = t.Clientes.Descripcion,
                                Telefono = t.Clientes.TelefonoCliente,
                                IdEstadoFicha = t.IdEstadoFicha,
                                EstadoFicha = t.EstadosFichas.Descripcion,
                                FechaVencimiento = t.FechaVencimiento
                            };
                return query.ToList();
            }
        }

        public List<ObleasExtendedView> ReadFichaParaReInformarFichaEntregada(string numeroObleaAsignada, string dominio)

        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Obleas>(this.EntityName)
                            .Where(x => (dominio == null || dominio == "" || x.Vehiculos.Descripcion == dominio)
                                     && (numeroObleaAsignada == null || numeroObleaAsignada == "" || x.NroObleaNueva == numeroObleaAsignada)
                                     && (x.IdEstadoFicha == ESTADOSFICHAS.Entregada))
                            select new ObleasExtendedView
                            {
                                ID = t.ID,
                                NroInternoOpercion = t.NroIntOperacTP.Value,
                                NroObleaNueva = t.NroObleaNueva,
                                Taller = t.Talleres.RazonSocialTaller,
                                FechaHabilitacion = t.FechaHabilitacion.HasValue ? t.FechaHabilitacion.Value : GetDinamyc.MinDatetime,
                                Descripcion = t.Descripcion,
                                Dominio = t.Vehiculos.Descripcion,
                                NombreyApellido = t.Clientes.Descripcion,
                                Telefono = t.Clientes.TelefonoCliente,
                                EstadoFicha = t.EstadosFichas.Descripcion,
                                FechaVencimiento = t.FechaVencimiento
                            };
                return query.ToList();
            }
        }

        public Obleas ReadDetalladoByID(Guid idOblea)
        {

            using (var context = this.GetEntityContext())
            {
                context.ContextOptions.LazyLoadingEnabled = true;

                var query = from t in context.CreateQuery<Obleas>(this.EntityName)
                            .Include("Talleres")
                            .Include("Talleres.TalleresRT")
                            .Include("Talleres.TalleresRT.RT")
                            .Include("Talleres.TalleresRT.RT.DocumentosClientes")
                            .Include("Uso")
                            .Include("PEC")
                            .Include("PEC.RT_PEC")
                            .Include("PEC.RT_PEC.RT")
                            .Include("PEC.RT_PEC.RT.DocumentosClientes")
                            .Include("Operaciones")
                            .Include("Vehiculos")
                            .Include("Clientes")
                            .Include("Clientes.DocumentosClientes")
                            .Include("Clientes.Localidades")
                            .Include("Clientes.Localidades.Provincias")
                            .Include("ObleasReguladores")
                            .Include("ObleasReguladores.Operaciones")
                            .Include("ObleasReguladores.ReguladoresUnidad")
                            .Include("ObleasReguladores.ReguladoresUnidad.Reguladores")
                            .Include("ObleasCilindros")
                            .Include("ObleasCilindros.CRPC")
                            .Include("ObleasCilindros.Operaciones")
                            .Include("ObleasCilindros.CilindrosUnidad")
                            .Include("ObleasCilindros.CilindrosUnidad.PHCilindros")
                            .Include("ObleasCilindros.CilindrosUnidad.Cilindros")
                            .Include("ObleasCilindros.ObleasValvulas.Operaciones")
                            .Include("ObleasCilindros.ObleasValvulas")
                            .Include("ObleasCilindros.ObleasValvulas.Valvula_Unidad")
                            .Include("ObleasCilindros.ObleasValvulas.Valvula_Unidad.Valvula")
                            .Include("ObleaHistoricoEstado")
                            .Include("ObleaHistoricoEstado.ObleaErrorDetalle")
                    .Where(x => x.ID == idOblea)
                            select t;

                return query.FirstOrDefault();
            }

        }

        public Obleas ReadDetalladoByObleaAnterior(String nroObleaAnt)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Obleas>(this.EntityName)
                            .Include("Talleres")
                            .Include("Talleres.TalleresRT")
                            .Include("Talleres.TalleresRT.RT")
                            .Include("Talleres.TalleresRT.RT.DocumentosClientes")
                            .Include("Uso")
                            .Include("PEC")
                            .Include("PEC.RT_PEC")
                            .Include("PEC.RT_PEC.RT")
                            .Include("PEC.RT_PEC.RT.DocumentosClientes")
                            .Include("Operaciones")
                            .Include("Vehiculos")
                            .Include("Clientes")
                            .Include("Clientes.DocumentosClientes")
                            .Include("Clientes.Localidades")
                            .Include("Clientes.Localidades.Provincias")
                            .Include("ObleasReguladores.Operaciones")
                            .Include("ObleasReguladores.ReguladoresUnidad")
                            .Include("ObleasReguladores.ReguladoresUnidad.Reguladores")
                            .Include("ObleasCilindros.CRPC")
                            .Include("ObleasCilindros.Operaciones")
                            .Include("ObleasCilindros.CilindrosUnidad")
                            .Include("ObleasCilindros.CilindrosUnidad.Cilindros")
                            .Include("ObleasCilindros.ObleasValvulas.Operaciones")
                            .Include("ObleasCilindros.ObleasValvulas")
                            .Include("ObleasCilindros.ObleasValvulas.Valvula_Unidad")
                            .Include("ObleasCilindros.ObleasValvulas.Valvula_Unidad.Valvula")
                    .Where(x => x.NroObleaNueva.Equals(nroObleaAnt))
                            select t;

                return query.FirstOrDefault();
            }
        }

        public List<ObleasExtendedView> ReadObleasAInformar(ObleasParameters paramentersEntity)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Obleas>(this.EntityName)
                                      .Where(x => (x.IdEstadoFicha == paramentersEntity.EstadoFichaAprobada)
                                               || (x.IdEstadoFicha == paramentersEntity.EstadoAprobadaConError))
                            orderby t.Talleres.Zona, t.IdTaller, t.FechaHabilitacion descending
                            select new ObleasExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion,
                                FechaHabilitacion = t.FechaHabilitacion.Value,
                                NroObleaAnterior = t.Descripcion,
                                Dominio = t.Vehiculos.Descripcion,
                                NombreyApellido = t.Clientes.Descripcion,
                                Telefono = t.Clientes.TelefonoCliente,
                                Observacion = t.ObservacionesFicha,
                                IdTaller = t.IdTaller,
                                Taller = t.Talleres.Descripcion + " - " + t.Talleres.RazonSocialTaller,
                                IdEstadoFicha = t.IdEstadoFicha,
                                Zona = t.Talleres.Zona,
                                FechaAlta = t.FechaRealAlta,
                                Operacion = t.Operaciones.Descripcion
                            };
                return query.ToList();
            }
        }

        public List<ObleasExtendedView> ReadObleasPorEstado(Guid idEstadoFicha, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Obleas>(this.EntityName)
                            .Where(x => ((x.IdEstadoFicha == idEstadoFicha) || (idEstadoFicha == Guid.Empty))
                                      && (!fechaDesde.HasValue || fechaDesde.Value <= x.FechaHabilitacion)
                                      && (!fechaHasta.HasValue || fechaHasta.Value >= x.FechaHabilitacion))
                            orderby t.Talleres.Zona, t.IdTaller, t.FechaHabilitacion descending
                            select new ObleasExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion,
                                FechaHabilitacion = t.FechaHabilitacion.HasValue ? t.FechaHabilitacion.Value : GetDinamyc.MinDatetime,
                                NroObleaAnterior = t.Descripcion,
                                NroObleaNueva = t.NroObleaNueva,
                                Dominio = t.Vehiculos.Descripcion,
                                NombreyApellido = t.Clientes.Descripcion,
                                Telefono = t.Clientes.TelefonoCliente,
                                Observacion = t.ObservacionesFicha,
                                IdTaller = t.IdTaller,
                                Taller = t.Talleres.Descripcion + " - " + t.Talleres.RazonSocialTaller,
                                IdEstadoFicha = t.IdEstadoFicha,
                                Zona = t.Talleres.Zona,
                                FechaAlta = t.FechaRealAlta,
                                Operacion = t.Operaciones.Descripcion
                            };
                return query.ToList();
            }
        }

        public List<ObleasExtendedView> ReadObleasRealizadas(DateTime fechaDesde, DateTime fechaHasta)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Obleas>(this.EntityName)
                            .Where(x => ((x.IdEstadoFicha == ESTADOSFICHAS.Entregada) ||
                                         (x.IdEstadoFicha == ESTADOSFICHAS.Despachada) ||
                                         (x.IdEstadoFicha == ESTADOSFICHAS.Finalizada) ||
                                         (x.IdEstadoFicha == ESTADOSFICHAS.FinalizadaConError))
                                      && (x.FechaHabilitacion >= fechaDesde) && (x.FechaHabilitacion <= fechaHasta ))
                            orderby t.Talleres.Zona, t.IdTaller, t.FechaHabilitacion descending
                            select new ObleasExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion,
                                FechaHabilitacion = t.FechaHabilitacion.HasValue ? t.FechaHabilitacion.Value : GetDinamyc.MinDatetime,
                                NroObleaAnterior = t.Descripcion,
                                NroObleaNueva = t.NroObleaNueva,
                                Dominio = t.Vehiculos.Descripcion,
                                NombreyApellido = t.Clientes.Descripcion,
                                Telefono = t.Clientes.TelefonoCliente,
                                Observacion = t.ObservacionesFicha,
                                IdTaller = t.IdTaller,
                                Taller = t.Talleres.Descripcion + " - " + t.Talleres.RazonSocialTaller,
                                IdEstadoFicha = t.IdEstadoFicha,
                                IdOpreracion = t.IdOperacion,
                                Zona = t.Talleres.Zona,
                                FechaAlta = t.FechaRealAlta,
                                Operacion = t.Operaciones.Descripcion,
                                TallerEmail = t.Talleres.MailTaller
                            };
                return query.ToList();
            }
        }

        public List<ObleasExtendedView> ReadObleasAVencer(DateTime fechaDesde, DateTime fechaHasta)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Obleas>(this.EntityName)
                            .Where(x =>(x.FechaVencimiento >= fechaDesde) && (x.FechaVencimiento <= fechaHasta))
                            orderby t.Talleres.Zona, t.IdTaller, t.FechaVencimiento descending
                            select new ObleasExtendedView
                            {
                                ID = t.ID,
                                Descripcion = t.Descripcion,                                
                                FechaHabilitacion = t.FechaHabilitacion.HasValue ? t.FechaHabilitacion.Value : GetDinamyc.MinDatetime,
                                FechaVencimiento = t.FechaVencimiento.HasValue ? t.FechaVencimiento.Value : GetDinamyc.MinDatetime,
                                NroObleaAnterior = t.Descripcion,
                                NroObleaNueva = t.NroObleaNueva,
                                Dominio = t.Vehiculos.Descripcion,
                                NombreyApellido = t.Clientes.Descripcion,
                                Telefono = t.Clientes.TelefonoCliente,
                                Observacion = t.ObservacionesFicha,
                                IdTaller = t.IdTaller,
                                Taller = t.Talleres.Descripcion + " - " + t.Talleres.RazonSocialTaller,
                                IdEstadoFicha = t.IdEstadoFicha,
                                IdOpreracion = t.IdOperacion,
                                Zona = t.Talleres.Zona,
                                FechaAlta = t.FechaRealAlta,
                                Operacion = t.Operaciones.Descripcion,
                                TallerEmail = t.Talleres.MailTaller
                            };
                return query.ToList();
            }
        }

        /// <summary>
        /// Devuelve las obleas para despachar
        /// </summary>  
        public List<ObleasDespachoView> ReadAllObleasByZonaIDParaDespacho(String zona)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Obleas>(this.EntityName)
                                      .Where(x => (x.Talleres.Zona.ToUpper() == zona))
                            select new ObleasDespachoView
                            {
                                ID = t.ID,
                                //Descripcion = t.Descripcion,
                                //FechaHabilitacion = t.FechaHabilitacion.Value,
                                //NroObleaAnterior = t.Descripcion,
                                //Dominio = t.Vehiculos.Descripcion,
                                //NombreyApellido = t.Clientes.Descripcion,
                                //Telefono = t.Clientes.TelefonoCliente,
                                //Observacion = t.ObservacionesFicha,
                                //IdTaller = t.IdTaller,
                                //Taller = t.Talleres.Descripcion + " - " + t.Talleres.RazonSocialTaller,
                                //IdEstadoFicha = t.IdEstadoFicha,
                                //Zona = t.Talleres.Zona,
                                //FechaAlta = t.FechaRealAlta,
                                //Operacion = t.Operaciones.Descripcion
                            };
                return query.ToList();
            }
        }

        #endregion

        #region Methods Web Api
        /// <summary>
        /// Devuelve un oblea aplanada en base a filtros
        /// </summary>        
        public ObleasViewWebApi ReadForIngesoWebApi(ObleasParametersWebApi criteria)
        {
            using (var context = this.GetEntityContext())
            {
                Obleas query = null;

                if (criteria.ID != null && criteria.ID != Guid.Empty)
                {
                    query = (from t in context.CreateQuery<Obleas>(this.EntityName)
                                 .Where(x => (x.ID == criteria.ID))
                             select t).FirstOrDefault();
                }
                else if (!String.IsNullOrWhiteSpace(criteria.NumeroOblea))
                {
                    query = (from t in context.CreateQuery<Obleas>(this.EntityName)
                                 .Where(x => x.NroObleaNueva == criteria.NumeroOblea)
                             select t).FirstOrDefault();
                }

                if (query == null) return null;

                Clientes cliente = query.Clientes;
                if (!String.IsNullOrWhiteSpace(criteria.NumeroDocumento)
                    && (cliente.IdTipoDniCliente != criteria.TipoDocumento
                     || cliente.NroDniCliente != criteria.NumeroDocumento))
                {
                    cliente = (new ClientesDataAccess()).ReadClientesViewByTipoyNroDoc(criteria.TipoDocumento,
                                                                           criteria.NumeroDocumento).FirstOrDefault();
                }

                var oblea = new ObleasViewWebApi()
                {
                    FechaHabilitacion = query.FechaHabilitacion.Value,
                    ObleaNumeroAnterior = query.Descripcion,
                    VehiculoDominio = query.Vehiculos.Descripcion,
                    VehiculoMarca = query.Vehiculos.MarcaVehiculo,
                    VehiculoModelo = query.Vehiculos.ModeloVehiculo,
                    VehiculoAnio = query.Vehiculos.AnioVehiculo,
                    VehiculoEsRA = query.Vehiculos.RA,
                    VehiculoNumeroRA = query.Vehiculos.NumeroRA,
                    VehiculoEsInyeccion = query.Vehiculos.EsInyeccionVehiculo,

                    TallerID = query.Talleres.ID,
                    TallerRazonSocial = query.Talleres.RazonSocialTaller,
                    TalleresDomicilio = query.Talleres.DomicilioTaller,
                    TallerCuit = query.Talleres.CuitTaller
                };

                if (cliente != null)
                {
                    oblea.ClienteNombreApellido = cliente.Descripcion;
                    oblea.ClienteDomicilio = cliente.CalleCliente;
                    oblea.ClienteTipoDocumentoID = cliente.DocumentosClientes.ID;
                    oblea.ClienteTipoDocumento = cliente.DocumentosClientes.Descripcion;
                    oblea.ClienteNumeroDocumento = cliente.NroDniCliente;
                    oblea.ClienteTelefono = cliente.TelefonoCliente;
                    oblea.ClienteLocalidadID = cliente.Localidades.ID;
                    oblea.ClienteLocalidad = cliente.Localidades.Descripcion;
                }
                else
                {
                    oblea.ClienteNombreApellido = String.Empty;
                    oblea.ClienteDomicilio = String.Empty;
                    oblea.ClienteTipoDocumentoID = Guid.Empty;
                    oblea.ClienteTipoDocumento = String.Empty;
                    oblea.ClienteNumeroDocumento = String.Empty;
                    oblea.ClienteTelefono = String.Empty;
                    oblea.ClienteLocalidadID = Guid.Empty;
                    oblea.ClienteLocalidad = String.Empty;
                }

                foreach (var obleasRegulador in query.ObleasReguladores.Where(r => r.IdOperacion == MSDB.Sigue || r.IdOperacion == MSDB.Montaje))
                {
                    ObleasReguladoresExtendedView r = new ObleasReguladoresExtendedView();
                    r.ID = obleasRegulador.ID;
                    r.IDReg = obleasRegulador.ReguladoresUnidad.Reguladores.ID;
                    r.IDRegUni = obleasRegulador.IdReguladorUnidad;
                    r.CodigoReg = obleasRegulador.ReguladoresUnidad.Reguladores.Descripcion;
                    r.MSDBRegID = MSDB.Sigue;
                    r.MSDBReg = "Sigue";
                    r.NroSerieReg = obleasRegulador.ReguladoresUnidad.Descripcion;
                    oblea.Reguladores.Add(r);
                }

                foreach (var obleasCilindro in query.ObleasCilindros.Where(r => r.IdOperacion == MSDB.Sigue || r.IdOperacion == MSDB.Montaje))
                {
                    ObleasCilindrosExtendedView c = new ObleasCilindrosExtendedView();
                    c.ID = obleasCilindro.ID;
                    c.IDCil = obleasCilindro.CilindrosUnidad.Cilindros.ID;
                    c.IDCilUni = obleasCilindro.IdCilindroUnidad;
                    c.CodigoCil = obleasCilindro.CilindrosUnidad.Cilindros.Descripcion;
                    c.MSDBCilID = MSDB.Sigue;
                    c.MSDBCil = "Sigue";
                    c.NroSerieCil = obleasCilindro.CilindrosUnidad.Descripcion;

                    c.CilFabMes = obleasCilindro.CilindrosUnidad.MesFabCilindro.HasValue ? obleasCilindro.CilindrosUnidad.MesFabCilindro.Value.ToString("00") : String.Empty;
                    c.CilFabAnio = obleasCilindro.CilindrosUnidad.AnioFabCilindro.HasValue ? obleasCilindro.CilindrosUnidad.AnioFabCilindro.Value.ToString("00") : String.Empty;
                    c.CilRevMes = obleasCilindro.MesUltimaRevisionCil.ToString("00");
                    c.CilRevAnio = obleasCilindro.AnioUltimaRevisionCil.ToString("00");
                    c.CRPCCilID = obleasCilindro.CRPC.ID;
                    c.CRPCCil = obleasCilindro.CRPC.Descripcion;

                    c.NroCertificadoPH = obleasCilindro.NroCertificadoPH;

                    oblea.Cilindros.Add(c);


                    foreach (var obleasValvulas in obleasCilindro.ObleasValvulas.Where(r => r.IdOperacion == MSDB.Sigue || r.IdOperacion == MSDB.Montaje))
                    {
                        ObleasValvulasExtendedView v = new ObleasValvulasExtendedView();
                        v.ID = obleasValvulas.ID;
                        v.IdObleaCil = obleasCilindro.ID;
                        v.IDValUni = obleasValvulas.IdValvulaUnidad;
                        v.CodigoVal = obleasValvulas.Valvula_Unidad.Valvula.Descripcion;
                        v.MSDBValID = MSDB.Sigue;
                        v.MSDBVal = "Sigue";
                        v.NroSerieVal = obleasValvulas.Valvula_Unidad.Descripcion;
                        oblea.Valvulas.Add(v);
                    }
                }

                return oblea;
            }
        }

        /// <summary>
        /// Devuelve un oblea aplanada en base a filtros
        /// </summary>        
        public ObleasViewWebApi ReadByIDWebApi(Guid idOblea)
        {
            using (var context = this.GetEntityContext())
            {
                var query = (from t in context.CreateQuery<Obleas>(this.EntityName)
                             .Where(x => x.ID == idOblea)

                             select t).First();

                var oblea = new ObleasViewWebApi()
                {
                    OperacionID = query.IdOperacion,
                    ObleaNumeroAnterior = query.Descripcion,
                    VehiculoDominio = query.Vehiculos.Descripcion,
                    VehiculoMarca = query.Vehiculos.MarcaVehiculo,
                    VehiculoModelo = query.Vehiculos.ModeloVehiculo,
                    VehiculoAnio = query.Vehiculos.AnioVehiculo,
                    VehiculoEsRA = query.Vehiculos.RA,
                    VehiculoNumeroRA = query.Vehiculos.NumeroRA,
                    VehiculoEsInyeccion = query.Vehiculos.EsInyeccionVehiculo,

                    ClienteNombreApellido = query.Clientes.Descripcion,
                    ClienteDomicilio = query.Clientes.CalleCliente,
                    ClienteTipoDocumentoID = query.Clientes.DocumentosClientes.ID,
                    ClienteTipoDocumento = query.Clientes.DocumentosClientes.Descripcion,
                    ClienteNumeroDocumento = query.Clientes.NroDniCliente,
                    ClienteTelefono = !String.IsNullOrWhiteSpace(query.Clientes.TelefonoCliente.Trim('0')) ? query.Clientes.TelefonoCliente : query.Clientes.CelularCliente,
                    ClienteLocalidadID = query.Clientes.Localidades.ID,
                    ClienteLocalidad = query.Clientes.Localidades.Descripcion,

                    TallerRazonSocial = query.Talleres.RazonSocialTaller,
                    TalleresDomicilio = query.Talleres.DomicilioTaller,
                    TallerCuit = query.Talleres.CuitTaller,
                    TallerID = query.IdTaller,
                    TallerMatricula = query.Talleres.Descripcion
                };

                foreach (var obleasRegulador in query.ObleasReguladores)
                {
                    ObleasReguladoresExtendedView r = new ObleasReguladoresExtendedView();
                    r.ID = obleasRegulador.ID;
                    r.IDReg = obleasRegulador.ReguladoresUnidad.Reguladores.ID;
                    r.IDRegUni = obleasRegulador.IdReguladorUnidad;
                    r.CodigoReg = obleasRegulador.ReguladoresUnidad.Reguladores.Descripcion;
                    r.MSDBRegID = obleasRegulador.IdOperacion;
                    r.MSDBReg = obleasRegulador.Operaciones.Descripcion;
                    r.NroSerieReg = obleasRegulador.ReguladoresUnidad.Descripcion;
                    oblea.Reguladores.Add(r);
                }

                foreach (var obleasCilindro in query.ObleasCilindros)
                {
                    ObleasCilindrosExtendedView c = new ObleasCilindrosExtendedView();
                    c.ID = obleasCilindro.ID;
                    c.IDCil = obleasCilindro.CilindrosUnidad.Cilindros.ID;
                    c.IDCilUni = obleasCilindro.IdCilindroUnidad;
                    c.CodigoCil = obleasCilindro.CilindrosUnidad.Cilindros.Descripcion;
                    c.MSDBCilID = obleasCilindro.IdOperacion;
                    c.MSDBCil = obleasCilindro.Operaciones.Descripcion;
                    c.NroSerieCil = obleasCilindro.CilindrosUnidad.Descripcion;

                    c.CilFabMes = obleasCilindro.CilindrosUnidad.MesFabCilindro.HasValue ? obleasCilindro.CilindrosUnidad.MesFabCilindro.Value.ToString("00") : String.Empty;
                    c.CilFabAnio = obleasCilindro.CilindrosUnidad.AnioFabCilindro.HasValue ? obleasCilindro.CilindrosUnidad.AnioFabCilindro.Value.ToString("00") : String.Empty;
                    c.CilRevMes = obleasCilindro.MesUltimaRevisionCil.ToString("00");
                    c.CilRevAnio = obleasCilindro.AnioUltimaRevisionCil.ToString("00");
                    c.CRPCCilID = obleasCilindro.CRPC.ID;
                    c.CRPCCil = obleasCilindro.CRPC.Descripcion;

                    c.NroCertificadoPH = obleasCilindro.NroCertificadoPH;

                    oblea.Cilindros.Add(c);


                    foreach (var obleasValvulas in obleasCilindro.ObleasValvulas)
                    {
                        ObleasValvulasExtendedView v = new ObleasValvulasExtendedView();
                        v.ID = obleasValvulas.ID;
                        v.IdObleaCil = obleasCilindro.ID;
                        v.IDValUni = obleasValvulas.IdValvulaUnidad;
                        v.CodigoVal = obleasValvulas.Valvula_Unidad.Valvula.Descripcion;
                        v.MSDBValID = obleasValvulas.IdOperacion;
                        v.MSDBVal = obleasValvulas.Operaciones.Descripcion;
                        v.NroSerieVal = obleasValvulas.Valvula_Unidad.Descripcion;
                        oblea.Valvulas.Add(v);
                    }
                }

                return oblea;
            }
        }

        /// <summary>
        /// Devuelve las obleas pendientes para un taller
        /// </summary>
        /// <param name="idEstadoOblea"></param>
        /// <param name="idTaller"></param>
        /// <returns></returns>
        public List<EstadosTramitesView> ReadTramitesByTallerID(Guid idTaller)
        {
            using (var context = this.GetEntityContext())
            {
                SqlParameter pZona = new SqlParameter("@pIdTaller", idTaller);
                var parameters = new object[] { pZona };

                context.CommandTimeout = 200;

                List<EstadosTramitesView> datos = context.ExecuteStoreQuery<EstadosTramitesView>("dbo.spReadTramitesParaDespachoPorTaller @pIdTaller", parameters).ToList();

                List<EstadosTramitesView> tramites = new List<EstadosTramitesView>();
                foreach (var item in datos)
                {
                    if ((item.IdEstado == ESTADOSFICHAS.Bloqueada || item.IdEstado == ESTADOSFICHAS.Eliminada))
                    {
                        int dias = (DateTime.Now - item.FechaTramite).Days;
                        if (dias > 1)
                            continue;
                    }
                    tramites.Add(item);
                }

                return tramites;
            }
        }

        /// <summary>
        /// Valida si existe una oblea con el nro oblea enviado por parámetro
        /// </summary>
        public bool ExisteObleaConNroObleaAnterior(String nroObleaAnterior)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Obleas>(this.EntityName)
                                             .Where(x => x.Descripcion == nroObleaAnterior)
                            select t;

                return query.Any();
            }
        }
        #endregion
    }
}