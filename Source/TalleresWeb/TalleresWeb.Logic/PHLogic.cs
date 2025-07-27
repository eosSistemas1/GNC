using CrossCutting.DatosDiscretos;
using PL.Fwk.BusinessLogic;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;

namespace TalleresWeb.Logic
{
    public class PHLogic : EntityManagerLogic<PH, PHExtendedView, PHParameters, PHDataAccess>
    {
        #region Properties
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

        private CilindrosUnidadLogic cilindrosUnidadLogic;
        private CilindrosUnidadLogic CilindrosUnidadLogic
        {
            get
            {
                if (cilindrosUnidadLogic == null) cilindrosUnidadLogic = new CilindrosUnidadLogic();
                return cilindrosUnidadLogic;
            }
        }

        private ValvulaUnidadLogic valvulaUnidadLogic;
        private ValvulaUnidadLogic ValvulaUnidadLogic
        {
            get
            {
                if (valvulaUnidadLogic == null) valvulaUnidadLogic = new ValvulaUnidadLogic();
                return valvulaUnidadLogic;
            }
        }
        #endregion        

        public PHPrintViewWebApi ReadForPrint(Guid idPH)
        {
            var ph = this.ReadDetalladoByID(idPH);

            PHPrintViewWebApi result = PHPrintViewWebApi.GetViewFromEntity(ph);

            return result;
        }

        public PH ReadDetalladoByID(Guid idPH)
        {
            return EntityDataAccess.ReadDetalladoByID(idPH);
        }

        public PH ReadDetalladoByphCilindroID(Guid phcilindroID)
        {
            return EntityDataAccess.ReadDetalladoByphCilindroID(phcilindroID);
        }

        public PH ReadDetalladoByIDParaConsulta(Guid idPH)
        {
            return EntityDataAccess.ReadDetalladoByIDParaConsulta(idPH);
        }

        /// <summary>
        /// Graba ph si se cargó por una oblea
        /// </summary>
        /// <param name="idOblea"></param>
        /// <param name="idPH"></param>
        /// <param name="idObleaCilindro"></param>
        public void GuardarPHExtranet(Guid idOblea, Guid idPH, List<Guid> idObleaCilindros)
        {
            using (TransactionScope ss = new TransactionScope())
            {
                try
                {
                    var oblea = this.ObleasLogic.ReadDetalladoByID(idOblea);
                    var ph = this.Add(new PH()
                    {
                        ID = idPH,
                        IdTaller = oblea.IdTaller,
                        //IdNroCartaTaller,
                        IDVehiculo = oblea.IdVehiculo,
                        IDCliente = oblea.IdCliente,
                        IdPEC = CrossCutting.DatosDiscretos.PEC.PEAR,
                        NroObleaHabilitante = oblea.Descripcion,
                        FechaOperacion = DateTime.Now,
                        IdCRPC = CrossCutting.DatosDiscretos.CRPC.PEAR,
                    });

                    foreach (var idObleaCilindro in idObleaCilindros)
                    {
                        ObleasCilindros cilindro = oblea.ObleasCilindros.Where(c => c.ID == idObleaCilindro).FirstOrDefault();

                        this.PHCilindrosLogic.Add(
                            new PHCilindros()
                            {
                                ID = Guid.NewGuid(),
                                IdPH = idPH,
                                IdCilindroUnidad = cilindro.IdCilindroUnidad,
                                IdValvulaUnidad = cilindro.ObleasValvulas.Any() ? cilindro.ObleasValvulas.FirstOrDefault().IdValvulaUnidad : default(Guid?),
                                IdEstadoPH = CrossCutting.DatosDiscretos.EstadosPH.EnEsperaCilindros
                            });
                    }
                    ss.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ss.Dispose();
                }
            }
        }

        /// <summary>
        /// Graba ph si se cargó por la pantalla de ph
        /// </summary>
        /// <param name="ph"></param>
        /// <returns></returns>
        public ViewEntity SaveFromExtranet(PHViewWebApi ph)
        {
            Guid usuarioID = USUARIOS.Admin;
            return SaveFromExtranet(ph, usuarioID);
        }

        public ViewEntity SaveFromExtranet(PHViewWebApi ph, Guid usuarioID)
        {
            return SaveFromExtranet(ph, usuarioID, CrossCutting.DatosDiscretos.PEC.PEAR);
        }

        /// <summary>
        /// Graba ph si se cargó por la pantalla de ph
        /// </summary>
        /// <param name="ph"></param>
        /// <returns></returns>
        public ViewEntity SaveFromExtranet(PHViewWebApi ph, Guid usuarioID, Guid idPec)
        {
            Guid idPH = Guid.NewGuid();

            using (TransactionScope ss = new TransactionScope())
            {
                try
                {
                    var oblea = this.ObleasLogic.ReadAllByNroObleaNueva(ph.NroObleaHabilitante);

                    var vehiculo = this.GuardarVehiculo(ph);

                    var cliente = this.GuardarCliente(ph);

                    var phNueva = this.Add(new PH()
                    {
                        ID = idPH,
                        IdTaller = ph.TallerID,
                        //IdNroCartaTaller,
                        IDVehiculo = vehiculo.ID,
                        IDCliente = cliente.ID,
                        IdPEC = idPec,
                        NroObleaHabilitante = oblea != null ? oblea.Descripcion : ph.NroObleaHabilitante,
                        FechaOperacion = DateTime.Now,
                        IdCRPC = CrossCutting.DatosDiscretos.CRPC.PEAR
                    });

                    List<PHCilindrosViewWebApi> cilindros = ph.Cilindros;

                    foreach (var item in cilindros)
                    {
                        decimal? capacidad = String.IsNullOrEmpty(item.Capacidad) ? default(decimal?) : decimal.Parse(item.Capacidad);

                        var idPHCilindro = Guid.NewGuid();

                        this.PHCilindrosLogic.Add(
                            new PHCilindros()
                            {
                                ID = idPHCilindro,
                                IdPH = idPH,
                                IdCilindroUnidad = CilindrosUnidadLogic.LeerCrearCilindroUnidad(item.CodigoHomologacionCilindro, item.NumeroSerieCilindro, item.MesFabCilindro, item.AnioFabCilindro, item.MarcaCilindro, capacidad),
                                IdValvulaUnidad = ValvulaUnidadLogic.LeerCrearValvulaUnidad(item.CodigoHomologacionValvula, item.NumeroSerieValvula),
                                IdEstadoPH = EstadosPH.Ingresada,
                                ObservacionPH = item.Observacion
                            });

                        this.PHCilindrosLogic.CambiarEstado(idPHCilindro, EstadosPH.Ingresada, item.Observacion, usuarioID);
                    }

                    ss.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ss.Dispose();
                }

                return new ViewEntity(idPH);
            }
        }

        /// <summary>
        /// Actualiza la carta compromiso y su estado
        /// </summary>
        /// <param name="cartaCompromiso"></param>
        public void UpdateCartaCompromiso(Guid id, PHViewWebApi cartaCompromiso, Guid usuarioID, bool actualizaEstado)
        {
            using (TransactionScope ss = new TransactionScope())
            {
                try
                {
                    var ccActualizar = this.ReadDetalladoByID(id);

                    var oblea = this.ObleasLogic.ReadAllByNroObleaNueva(cartaCompromiso.NroObleaHabilitante);

                    var vehiculo = this.GuardarVehiculo(cartaCompromiso);

                    var cliente = this.GuardarCliente(cartaCompromiso);

                    ccActualizar.IdTaller = cartaCompromiso.TallerID;
                    ccActualizar.IdNroCartaTaller = ccActualizar.IdNroCartaTaller;
                    ccActualizar.IDVehiculo = vehiculo.ID;
                    ccActualizar.IDCliente = cliente.ID;
                    ccActualizar.IdPEC = cartaCompromiso.PECID.Value;
                    ccActualizar.NroObleaHabilitante = oblea != null ? oblea.Descripcion : ccActualizar.NroObleaHabilitante;
                    ccActualizar.FechaOperacion = ccActualizar.FechaOperacion;
                    ccActualizar.IdCRPC = CrossCutting.DatosDiscretos.CRPC.PEAR;


                    foreach (var item in cartaCompromiso.Cilindros)
                    {
                        bool esPhCilindroNuevo = item.ID == Guid.Empty;
                        var idPHCilindro = esPhCilindroNuevo ? Guid.NewGuid() : item.ID;
                        var pHCilindroNuevo = esPhCilindroNuevo ? new PHCilindros() : this.PHCilindrosLogic.Read(idPHCilindro);

                        pHCilindroNuevo.ID = idPHCilindro;
                        pHCilindroNuevo.IdPH = ccActualizar.ID;
                        decimal? capacidad = !String.IsNullOrWhiteSpace(item.Capacidad) ? decimal.Parse(item.Capacidad) : default(decimal?);
                        pHCilindroNuevo.IdCilindroUnidad = CilindrosUnidadLogic.LeerCrearCilindroUnidad(item.CodigoHomologacionCilindro, item.NumeroSerieCilindro, item.MesFabCilindro, item.AnioFabCilindro, item.MarcaCilindro, capacidad);
                        pHCilindroNuevo.IdValvulaUnidad = ValvulaUnidadLogic.LeerCrearValvulaUnidad(item.CodigoHomologacionValvula, item.NumeroSerieValvula);
                        if (item.IdEstadoPH.HasValue) pHCilindroNuevo.IdEstadoPH = item.IdEstadoPH.Value;
                        pHCilindroNuevo.ObservacionPH = item.Observacion;


                        if (esPhCilindroNuevo)
                        {
                            pHCilindroNuevo.IdEstadoPH = CrossCutting.DatosDiscretos.EstadosPH.EnEsperaCilindros;
                            this.PHCilindrosLogic.Add(pHCilindroNuevo);
                            this.PHCilindrosLogic.CambiarEstado(pHCilindroNuevo.ID, pHCilindroNuevo.IdEstadoPH, $"Consultar CC: nuevo cilindro estado: {pHCilindroNuevo.IdEstadoPH}", usuarioID);
                        }
                        else
                        {
                            this.PHCilindrosLogic.Update(pHCilindroNuevo);
                        }

                        if (actualizaEstado && item.IdEstadoPH.HasValue)
                            this.PHCilindrosLogic.CambiarEstado(idPHCilindro, item.IdEstadoPH.Value, $"Consultar CC: nuevo estado: {item.IdEstadoPH.Value}", usuarioID);
                    }

                    this.Update(ccActualizar);

                    ss.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ss.Dispose();
                }
            }
        }        

        /// <summary>
        /// Guardo el vehiculo
        /// </summary>        
        private Vehiculos GuardarVehiculo(PHViewWebApi phEnviada)
        {
            VehiculosLogic vehiculosLogic = new VehiculosLogic();

            Vehiculos vehiculo = vehiculosLogic.ReadVehiculoByDominio(phEnviada.VehiculoDominio);
            if (vehiculo != null)
            {
                vehiculo.MarcaVehiculo = phEnviada.VehiculoMarca;
                vehiculo.ModeloVehiculo = phEnviada.VehiculoModelo;
                vehiculo.AnioVehiculo = phEnviada.VehiculoAnio;
                vehiculosLogic.Update(vehiculo);
            }
            else
            {
                vehiculo = new Vehiculos();
                vehiculo.ID = Guid.NewGuid();
                vehiculo.MarcaVehiculo = phEnviada.VehiculoMarca;
                vehiculo.ModeloVehiculo = phEnviada.VehiculoModelo;
                vehiculo.Descripcion = phEnviada.VehiculoDominio;
                vehiculo.AnioVehiculo = phEnviada.VehiculoAnio;
                vehiculosLogic.Add(vehiculo);
            }
            return vehiculo;
        }

        /// <summary>
        /// Guarda el cliente
        /// </summary>      
        private Clientes GuardarCliente(PHViewWebApi obleaEnviada)
        {
            ClientesLogic clientesLogic = new ClientesLogic();

            Clientes cliente = clientesLogic.ReadClientesViewByTipoyNroDoc(new Guid(obleaEnviada.ClienteTipoDocumento), obleaEnviada.ClientesNumeroDocumento).FirstOrDefault();
            if (cliente != null)
            {
                cliente.Descripcion = obleaEnviada.ClienteRazonSocial;
                cliente.CalleCliente = obleaEnviada.ClienteDomicilio;
                cliente.IdTipoDniCliente = String.IsNullOrWhiteSpace(obleaEnviada.ClienteTipoDocumento) ? cliente.IdTipoDniCliente : Guid.Parse(obleaEnviada.ClienteTipoDocumento);
                cliente.NroDniCliente = obleaEnviada.ClientesNumeroDocumento;
                cliente.TelefonoCliente = obleaEnviada.ClienteTelefono;
                cliente.CelularCliente = obleaEnviada.ClienteCelular;
                cliente.IdLocalidad = String.IsNullOrWhiteSpace(obleaEnviada.ClienteLocalidadID) ? cliente.IdLocalidad : Guid.Parse(obleaEnviada.ClienteLocalidadID);
                cliente.MailCliente = obleaEnviada.ClienteEmail;
                clientesLogic.Update(cliente);
            }
            else
            {
                cliente = new Clientes();
                cliente.ID = Guid.NewGuid();
                cliente.Descripcion = obleaEnviada.ClienteRazonSocial;
                cliente.CalleCliente = obleaEnviada.ClienteDomicilio;
                cliente.IdTipoDniCliente = Guid.Parse(obleaEnviada.ClienteTipoDocumento);
                cliente.NroDniCliente = obleaEnviada.ClientesNumeroDocumento;
                cliente.TelefonoCliente = obleaEnviada.ClienteTelefono;
                cliente.CelularCliente = obleaEnviada.ClienteCelular;
                cliente.IdLocalidad = Guid.Parse(obleaEnviada.ClienteLocalidadID);
                cliente.MailCliente = obleaEnviada.ClienteEmail;
                clientesLogic.AddCliente(cliente);
            }
            return cliente;
        }
    }
}
