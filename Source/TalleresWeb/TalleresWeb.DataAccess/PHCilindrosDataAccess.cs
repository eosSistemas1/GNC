using CrossCutting.DatosDiscretos;
using PL.Fwk.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{

    public class PHCilindrosDataAccess : EntityManagerDataAccess<PHCilindros, PHCilindrosExtendedView, PHCilindrosParameters, TalleresWebEntities>
    {
        public override PHCilindros Read(Guid id)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PHCilindros>(this.EntityName)
                                             .Include("PH")
                                             .Include("InspeccionesPH")
                                             .Where(x => x.ID == id)
                            select t;

                return query.FirstOrDefault();
            }
        }

        public PHCilindros ReadPhCilindroDetallado(Guid iDPhCilindros)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PHCilindros>(this.EntityName)
                                             .Include("PH")
                                             .Include("PH.CRPC")
                                             .Include("PH.PEC")
                                             .Include("PH.Talleres")
                                             .Include("PH.Clientes")
                                             .Include("PH.Clientes.Localidades")
                                             .Include("PH.Clientes.Localidades.Provincias")
                                             .Include("PH.Clientes.DocumentosClientes")
                                             .Include("PH.Vehiculos")
                                             .Include("CilindrosUnidad")
                                             .Include("CilindrosUnidad.Cilindros")
                                             .Include("CilindrosUnidad.Cilindros.MarcasCilindros")
                                             .Include("Valvula_Unidad")
                                             .Include("Valvula_Unidad.Valvula")
                                             .Include("Valvula_Unidad.Valvula.MarcasValvulas")
                                             .Include("InspeccionesPH")
                                             .Where(x => x.ID == iDPhCilindros)
                            select t;

                return query.FirstOrDefault();
            }
        }

        /// <summary>
        /// Devuelve Cilindros pendientes del paso Registro Peso
        /// </summary>
        /// <returns></returns>
        public List<PHCilindrosPendientesView> ReadCilindrosPendientesRegistroPeso()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PHCilindros>(this.EntityName)
                                            .Where(x => (!x.PesoMarcadoCilindro.HasValue
                                                        || !x.PesoVacioCilindro.HasValue)
                                                        && (x.IdEstadoPH == EstadosPH.EnProceso ||
                                                            x.IdEstadoPH == EstadosPH.ExcelGenerado))
                            select new PHCilindrosPendientesView()
                            {
                                ID = t.ID,
                                IDCilindroUnidad = t.IdCilindroUnidad,
                                NroOperacionCRPC = t.NroOperacionCRPC,
                                CodigoHomologacionCilindro = t.CilindrosUnidad.Cilindros.Descripcion,
                                NumeroSerieCilindro = t.CilindrosUnidad.Descripcion,
                                Taller = t.PH.Talleres.RazonSocialTaller
                            };

                return query.ToList();
            }
        }

        public PHCilindros ReadByNroOperacionCRPC(int nroRevision, Guid? idEstadoPH)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PHCilindros>(this.EntityName)
                             .Where(x => x.NroOperacionCRPC == nroRevision 
                                         &&
                                         (idEstadoPH == null || x.IdEstadoPH == idEstadoPH))
                            select t;

                return query.FirstOrDefault();
            }
        }

        public List<PHCilindrosPendientesView> ReadCilindrosPHPorEstado(Guid estadoPHID)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PHCilindros>(this.EntityName)
                                            .Where(x => x.IdEstadoPH == estadoPHID)
                            select new PHCilindrosPendientesView
                            {
                                IDPH = t.PH.ID,
                                ID = t.ID,
                                IDCilindroUnidad = t.IdCilindroUnidad,
                                IDEstadoPH = t.IdEstadoPH,
                                FechaOperacion = t.PH.FechaOperacion,
                                NroOperacionCRPC = t.NroOperacionCRPC,
                                CodigoHomologacionCilindro = t.CilindrosUnidad.Cilindros.Descripcion,
                                NumeroSerieCilindro = t.CilindrosUnidad.Descripcion,
                                CodigoHomologacionValvula = t.Valvula_Unidad.Valvula.Descripcion,
                                NumeroSerieValvula = t.Valvula_Unidad.Descripcion,
                                Estado = t.ESTADOSPH.Descripcion,
                                Dominio = t.PH.Vehiculos.Descripcion.ToUpper(),
                                Taller = t.PH.Talleres.Descripcion + "-" + t.PH.Talleres.RazonSocialTaller
                            };

                return query.ToList();
            }
        }

        /// <summary>
        /// valida si hay una ph finalizada , entregada o despachada para ese cilindro.
        /// </summary>
        /// <param name="idPHCilindro"></param>
        /// <returns></returns>
        public bool HayPHEnCurso(string codHomoCilindro, string serieCilindro)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PHCilindros>(this.EntityName)
                                            .Where(x => x.CilindrosUnidad.Descripcion == serieCilindro &&
                                                        x.CilindrosUnidad.Cilindros.Descripcion == codHomoCilindro &&
                                                        x.IdEstadoPH != EstadosPH.Entregada &&
                                                        x.IdEstadoPH != EstadosPH.Despachada &&
                                                        x.IdEstadoPH != EstadosPH.Finalizada)
                            select t;

                return query.Any();
            }
        }

        public List<PHCilindrosPendientesView> ReadCilindrosPHParaReimprimirHojaDeRuta()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PHCilindros>(this.EntityName)
                                            .Where(x => x.IdEstadoPH == EstadosPH.EnProceso ||
                                                        x.IdEstadoPH == EstadosPH.ExcelGenerado)
                            select new PHCilindrosPendientesView
                            {
                                ID = t.ID,
                                IDCilindroUnidad = t.IdCilindroUnidad,
                                IDEstadoPH = t.IdEstadoPH,
                                NroOperacionCRPC = t.NroOperacionCRPC,
                                CodigoHomologacionCilindro = t.CilindrosUnidad.Cilindros.Descripcion,
                                NumeroSerieCilindro = t.CilindrosUnidad.Descripcion,
                                CodigoHomologacionValvula = t.Valvula_Unidad.Valvula.Descripcion,
                                NumeroSerieValvula = t.Valvula_Unidad.Descripcion,
                                Estado = t.ESTADOSPH.Descripcion,
                                Dominio = t.PH.Vehiculos.Descripcion.ToUpper(),
                                Taller = t.PH.Talleres.Descripcion + "-" + t.PH.Talleres.RazonSocialTaller
                            };

                return query.OrderBy(x => x.NroOperacionCRPC).ToList();
            }
        }

        public List<PHCilindrosPendientesView> ReadCilindrosPHParaVerificarCodigos()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PHCilindros>(this.EntityName)
                                            .Where(x => x.IdEstadoPH == EstadosPH.VerificarCodigos
                                                      || x.IdEstadoPH == EstadosPH.IngresarEnLinea)
                            select new PHCilindrosPendientesView
                            {
                                ID = t.ID,
                                IDCilindroUnidad = t.IdCilindroUnidad,
                                IDEstadoPH = t.IdEstadoPH,
                                NroOperacionCRPC = t.NroOperacionCRPC,
                                CodigoHomologacionCilindro = t.CilindrosUnidad.Cilindros.Descripcion,
                                NumeroSerieCilindro = t.CilindrosUnidad.Descripcion,
                                CodigoHomologacionValvula = t.Valvula_Unidad.Valvula.Descripcion,
                                NumeroSerieValvula = t.Valvula_Unidad.Descripcion,
                                Estado = t.ESTADOSPH.Descripcion,
                                Dominio = t.PH.Vehiculos.Descripcion.ToUpper(),
                                Taller = t.PH.Talleres.Descripcion + "-" + t.PH.Talleres.RazonSocialTaller
                            };

                return query.ToList();
            }
        }

        public List<PHCilindrosPendientesView> ReadCilindrosPHParaEvaluarValvulas()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PHCilindros>(this.EntityName)
                                            .Where(x => x.IdValvulaUnidad.HasValue
                                                        && (x.RoscaValvula == null || x.RoscaValvula == "")
                                                        && (x.FuncValvula == null || x.FuncValvula == ""))
                            select new PHCilindrosPendientesView
                            {
                                ID = t.ID,
                                IDCilindroUnidad = t.IdCilindroUnidad,
                                IDEstadoPH = t.IdEstadoPH,
                                NroOperacionCRPC = t.NroOperacionCRPC,
                                CodigoHomologacionCilindro = t.CilindrosUnidad.Cilindros.Descripcion,
                                NumeroSerieCilindro = t.CilindrosUnidad.Descripcion,
                                CodigoHomologacionValvula = t.Valvula_Unidad.Valvula.Descripcion,
                                NumeroSerieValvula = t.Valvula_Unidad.Descripcion,
                                Estado = t.ESTADOSPH.Descripcion,
                                Dominio = t.PH.Vehiculos.Descripcion.ToUpper(),
                                Taller = t.PH.Talleres.Descripcion + "-" + t.PH.Talleres.RazonSocialTaller
                            };

                return query.ToList();
            }
        }

        /// <summary>
        /// Devuelve cilindros pendientes del paso Medición Espesores
        /// </summary>
        /// <returns></returns>
        public List<PHCilindrosPendientesView> ReadCilindrosPendientesMedicionEspesores()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PHCilindros>(this.EntityName)
                                            .Where(x => (!x.LecturaAParedCiindrol.HasValue
                                                       || !x.LecturaBParedCilindro.HasValue                                                       
                                                       || !x.LecturaAFondoCilindro.HasValue
                                                       || x.TipoFondoCilindro == null
                                                       || x.TipoFondoCilindro == string.Empty)
                                                       && (x.IdEstadoPH == EstadosPH.EnProceso ||
                                                            x.IdEstadoPH == EstadosPH.ExcelGenerado))
                            select new PHCilindrosPendientesView()
                            {
                                ID = t.ID,
                                IDCilindroUnidad = t.IdCilindroUnidad,
                                NroOperacionCRPC = t.NroOperacionCRPC,
                                CodigoHomologacionCilindro = t.CilindrosUnidad.Cilindros.Descripcion,
                                NumeroSerieCilindro = t.CilindrosUnidad.Descripcion,
                                Taller = t.PH.Talleres.RazonSocialTaller
                            };

                return query.ToList();
            }
        }

        /// <summary>
        /// devuelve la cantidad de ph en proceso , ingresadas y finalizadas 
        /// </summary>
        /// <returns>{en proceso}|{ingresadas}|{finalizadas}</returns>
        public string LeerPHActualmenteEnProceso()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PHCilindros>(this.EntityName)
                                             .Where(x => (x.IdEstadoPH == EstadosPH.EnEsperaCilindros
                                                       || x.IdEstadoPH == EstadosPH.IngresarEnLinea
                                                       || x.IdEstadoPH == EstadosPH.VerificarCodigos
                                                       || x.IdEstadoPH == EstadosPH.SolicitaVerificacion
                                                       || x.IdEstadoPH == EstadosPH.Ingresada
                                                       || x.IdEstadoPH == EstadosPH.EnProceso
                                                       || x.IdEstadoPH == EstadosPH.Finalizada))
                            select t;

                int cantidadParaProcesar = query.Count(x => x.IdEstadoPH == EstadosPH.EnEsperaCilindros
                                                         || x.IdEstadoPH == EstadosPH.IngresarEnLinea
                                                         || x.IdEstadoPH == EstadosPH.VerificarCodigos
                                                         || x.IdEstadoPH == EstadosPH.SolicitaVerificacion
                                                         || x.IdEstadoPH == EstadosPH.Ingresada);
                int cantidadEnProceso = query.Count(x => x.IdEstadoPH == EstadosPH.EnProceso);
                int cantidadFinalizada = query.Count(x => x.IdEstadoPH == EstadosPH.Finalizada);

                return $"{cantidadParaProcesar}|{cantidadEnProceso}|{cantidadFinalizada}";
            }
        }

        /// <summary>
        /// Devuelve cilindros pendientes del paso Inspección Exterior
        /// </summary>
        /// <returns></returns>
        public List<PHCilindrosPendientesView> ReadCilindrosPendientesInspeccionExterior()
        {
            return this.ReadInspeccionesPorTipo(INSPECCIONTIPO.EXTERIOR);
        }

        public int LeerUltimoNumeroOperacion()
        {
            using (var context = this.GetEntityContext())
            {
                var maxNroOperacionCRPC = (from c in context.CreateQuery<PHCilindros>(this.EntityName)
                                           where c.NroOperacionCRPC.HasValue
                                           select c)
                             .Max(c => c.NroOperacionCRPC);
                return maxNroOperacionCRPC.HasValue ? maxNroOperacionCRPC.Value : 0;
            }
        }

        /// <summary>
        /// Devuelve cilindros pendientes del paso Prueba Hidráulica
        /// </summary>
        /// <returns></returns>
        public List<PHCilindrosPendientesView> ReadCilindrosPendientesPruebaHidraulica()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PHCilindros>(this.EntityName)
                                            .Where(x => !x.Rechazado.HasValue
                                                     && (x.IdEstadoPH == EstadosPH.EnProceso ||
                                                            x.IdEstadoPH == EstadosPH.ExcelGenerado))
                            select new PHCilindrosPendientesView()
                            {
                                ID = t.ID,
                                IDCilindroUnidad = t.IdCilindroUnidad,
                                NroOperacionCRPC = t.NroOperacionCRPC,
                                CodigoHomologacionCilindro = t.CilindrosUnidad.Cilindros.Descripcion,
                                NumeroSerieCilindro = t.CilindrosUnidad.Descripcion,
                                Taller = t.PH.Talleres.RazonSocialTaller
                            };

                return query.ToList();
            }
        }

        /// <summary>
        /// Devuelve cilindros pendientes del paso Inspeccion Roscas
        /// </summary>
        /// <returns></returns>
        public List<PHCilindrosPendientesView> ReadCilindrosPendientesInspeccionRoscas()
        {
            return this.ReadInspeccionesPorTipo(INSPECCIONTIPO.ROSCA);
        }

        /// <summary>
        /// Devuelve cilindros pendientes del paso Inspeccion Interior
        /// </summary>
        /// <returns></returns>
        public List<PHCilindrosPendientesView> ReadCilindrosPendientesInspeccionInterior()
        {
            return this.ReadInspeccionesPorTipo(INSPECCIONTIPO.INTERIOR);
        }

        private List<PHCilindrosPendientesView> ReadInspeccionesPorTipo(Guid inspeccionTipo)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PHCilindros>(this.EntityName)
                                            .Where(x => ((x.IdEstadoPH == EstadosPH.EnProceso ||
                                                            x.IdEstadoPH == EstadosPH.ExcelGenerado)
                                                          &&
                                                          (!x.InspeccionesPH.Any(i => i.IdPHCilndro == x.ID && i.Inspecciones.IdInspeccionTipo == inspeccionTipo)))
                                                    )
                            select new PHCilindrosPendientesView()
                            {
                                ID = t.ID,
                                IDCilindroUnidad = t.IdCilindroUnidad,
                                CodigoHomologacionCilindro = t.CilindrosUnidad.Cilindros.Descripcion,
                                NumeroSerieCilindro = t.CilindrosUnidad.Descripcion,
                                Taller = t.PH.Talleres.RazonSocialTaller,
                                NroOperacionCRPC = t.NroOperacionCRPC
                            };

                return query.ToList();
            }
        }

        public PHCilindros ReadEnProcesoByCilindroUnidadID(Guid? cilindroUnidadID)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PHCilindros>(this.EntityName)
                                            .Where(x => x.IdCilindroUnidad == cilindroUnidadID.Value
                                                     && (x.IdEstadoPH == EstadosPH.EnProceso
                                                         || x.IdEstadoPH == EstadosPH.Ingresada))
                            select t;

                return query.Any() ? query.First() : default(PHCilindros);
            }
        }

        /// <summary>
        /// Devuelve las ph pendientes por zona, si zona es "" devuelve todas las zonas
        /// </summary>
        /// <param name="zona"></param>
        /// <returns></returns>
        public List<PHConsultaView> ReadPHPendientesByZona(string zona)
        {
            using (var context = this.GetEntityContext())
            {
                List<Guid> estadosPosibles = new List<Guid>();
                estadosPosibles.Add(EstadosPH.Ingresada);
                estadosPosibles.Add(EstadosPH.EnEsperaCilindros);
                estadosPosibles.Add(EstadosPH.Bloqueada);

                var query = from t in context.CreateQuery<PHCilindros>(this.EntityName)
                                             .Where(x => (estadosPosibles.Contains(x.IdEstadoPH)) &&
                                                         (zona.Trim() == String.Empty
                                                             || x.PH.Talleres.Zona.ToUpper().Trim() == zona.ToUpper().Trim()))
                            select new PHConsultaView
                            {
                                ID = t.ID,
                                PHID = t.IdPH,
                                TallerID = t.PH.IdTaller,
                                TallerRazonSocial = t.PH.Talleres.RazonSocialTaller,
                                Cliente = t.PH.Clientes.Descripcion,
                                Dominio = t.PH.Vehiculos.Descripcion,
                                EstadoPHID = t.IdEstadoPH,
                                EstadoPH = t.ESTADOSPH.Descripcion,
                                Fecha = t.PH.FechaOperacion,
                                NroObleaHabilitante = t.PH.NroObleaHabilitante,
                                NroSerieCilindro = t.CilindrosUnidad.Descripcion
                            };

                return query.ToList();
            }
        }

        /// <summary>
        /// Devuelve los datos para imprimir la hoja de ruta del proceso de PH
        /// </summary>
        /// <param name="idPhCilindro"></param>
        /// <returns></returns>
        public PHCilindrosHojaRutaView ReadParaImprimirHojaRuta(Guid idPhCilindro)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PHCilindros>(this.EntityName)
                                             .Where(x => x.ID == idPhCilindro)
                            select new PHCilindrosHojaRutaView()
                            {
                                FechaOperacion = t.PH.FechaOperacion,
                                AnioFabricacion = t.CilindrosUnidad.AnioFabCilindro ?? 0,
                                MesFabricacion = t.CilindrosUnidad.MesFabCilindro ?? 0,
                                CodigoHomologacion = t.CilindrosUnidad.Cilindros.Descripcion,
                                Capacidad = t.CilindrosUnidad.Cilindros.CapacidadCil ?? 0,
                                Diámetro = t.CilindrosUnidad.Cilindros.DiametroCilindro ?? 0,
                                NumeroSerie = t.CilindrosUnidad.Descripcion,
                                MatriculaTaller = t.PH.Talleres.Descripcion,
                                RazonSocialTaller = t.PH.Talleres.RazonSocialTaller,
                                ParedMinimo = t.CilindrosUnidad.Cilindros.EspesorAdmisibleCil ?? 0,
                                FondoMinimo = t.CilindrosUnidad.Cilindros.EspesorAdmisibleCil ?? 0,
                                Marca = t.CilindrosUnidad.Cilindros.MarcasCilindros.Descripcion,
                                MarcaValvula = t.Valvula_Unidad.Valvula.MarcasValvulas.Descripcion,
                                NormaFabricacion = t.CilindrosUnidad.Cilindros.NormaFabCilindro,
                                FechaUltimaRevision = DateTime.Now,
                                NroOperacionCRPC = t.NroOperacionCRPC,
                                Cliente = t.PH.Clientes.Descripcion,
                                NumeroSerieValvula = t.Valvula_Unidad.Descripcion,
                                CodigoHomologacionValvula = t.Valvula_Unidad.Valvula.Descripcion
                            };

                return query.FirstOrDefault();
            }
        }

        /// <summary>
        /// Devuelve las phCilindros que estan para informar
        /// </summary>
        /// <returns></returns>
        public List<PHCilindrosInformarView> ReadPHParaInformar()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PHCilindros>(this.EntityName)
                                             .Where(x => x.IdEstadoPH == EstadosPH.EnProceso && x.Rechazado.HasValue)
                            select new PHCilindrosInformarView
                            {
                                ID = t.ID,
                                Taller = t.PH.Talleres.Descripcion,
                                FechaHabilitacion = t.PH.FechaOperacion,
                                ObleaAnterior = t.PH.NroObleaHabilitante,
                                Dominio = t.PH.Vehiculos.Descripcion,
                                NumeroSerie = t.CilindrosUnidad.Descripcion,
                                CodigoHomologacion = t.CilindrosUnidad.Cilindros.Descripcion,
                                Cliente = t.PH.Clientes.Descripcion,
                                Telefono = t.PH.Clientes.TelefonoCliente
                            };

                return query.ToList();
            }
        }

        
        /// <summary>
        /// Devuelve las phCilindros que estan para asignar e imprimir
        /// </summary>
        /// <returns></returns>
        public List<PHCilindrosInformarView> ReadPHParaAsignar()
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<PHCilindros>(this.EntityName)
                                             .Where(x => x.IdEstadoPH == EstadosPH.Informada)
                            select new PHCilindrosInformarView
                            {
                                ID = t.ID,
                                Taller = t.PH.Talleres.Descripcion,
                                FechaHabilitacion = t.PH.FechaOperacion,
                                ObleaAnterior = t.PH.NroObleaHabilitante,
                                Dominio = t.PH.Vehiculos.Descripcion,
                                NumeroSerie = t.CilindrosUnidad.Descripcion,
                                CodigoHomologacion = t.CilindrosUnidad.Cilindros.Descripcion,
                                Cliente = t.PH.Clientes.Descripcion,
                                Telefono = t.PH.Clientes.TelefonoCliente
                            };

                return query.ToList();
            }
        }
    }
}