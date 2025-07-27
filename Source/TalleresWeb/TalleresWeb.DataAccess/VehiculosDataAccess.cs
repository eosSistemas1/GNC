using CrossCutting.DatosDiscretos;
using PL.Fwk.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;

namespace TalleresWeb.DataAccess
{
    public class VehiculosDataAccess : EntityManagerDataAccess<Vehiculos, VehiculosExtendedView, VehiculosParameters, TalleresWebEntities>
    {
        #region Methods

        public void Add(VehiculosExtendedView entity)
        {
            var vehiculo = this.Read(entity.ID);

            Vehiculos vehiculoNuevo = new Vehiculos();
            vehiculoNuevo.ID = entity.ID;
            vehiculoNuevo.Descripcion = entity.Descripcion;
            vehiculoNuevo.MarcaVehiculo = entity.MarcaVehiculo;
            vehiculoNuevo.ModeloVehiculo = entity.ModeloVehiculo;
            vehiculoNuevo.AnioVehiculo = entity.AnioVehiculo;
            vehiculoNuevo.IdDuenioVehiculo = entity.IdDuenioVehiculo;
            vehiculoNuevo.EsInyeccionVehiculo = entity.EsInyeccionVehiculo;

            if (vehiculo == null)
            {
                //si no existe el vehiculo lo agrego
                this.Add(vehiculoNuevo);
            }
            else
            {
                //si existe lo actualizo
                this.Update(vehiculoNuevo);
            }
        }        

        public Boolean TieneTramitesPendientes(String dominio)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Vehiculos>(this.EntityName)
                    .Where(x => x.Descripcion == dominio
                             && x.Obleas.Any(o => o.IdEstadoFicha == CrossCutting.DatosDiscretos.ESTADOSFICHAS.PendienteRevision
                                               || o.IdEstadoFicha == CrossCutting.DatosDiscretos.ESTADOSFICHAS.Aprobada
                                               || o.IdEstadoFicha == CrossCutting.DatosDiscretos.ESTADOSFICHAS.AprobadaConError)
                            )
                            select t;

                return query.Any();
            }
        }

        public List<VehiculosExtendedView> ReadByDominio(String pDominio)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Vehiculos>(this.EntityName)
                    .Where(x => x.Descripcion.Equals(pDominio))
                            select t;

                var vehiculos = query.ToList();

                var response = new List<VehiculosExtendedView>();

                foreach (var vehiculo in vehiculos)
                {
                    var v = new VehiculosExtendedView()
                    {
                        ID = vehiculo.ID,
                        MarcaVehiculo = vehiculo.MarcaVehiculo,
                        ModeloVehiculo = vehiculo.ModeloVehiculo,
                        AnioVehiculo = vehiculo.AnioVehiculo.HasValue ? vehiculo.AnioVehiculo.Value : 0,
                        EsInyeccionVehiculo = vehiculo.EsInyeccionVehiculo.HasValue ? vehiculo.EsInyeccionVehiculo.Value : false,
                        IdDuenioVehiculo = vehiculo.IdDuenioVehiculo.HasValue ? vehiculo.IdDuenioVehiculo.Value : Guid.Empty                        
                    };

                    v.IdUso = vehiculo.Obleas != null && vehiculo.Obleas.Any() ? vehiculo.Obleas.FirstOrDefault().IdUso : TIPOVEHICULO.Particular;

                    response.Add(v);
                }

                return response;
            }
        }

        public Vehiculos ReadVehiculoByDominio(String pDominio)
        {
            using (var context = this.GetEntityContext())
            {
                var query = from t in context.CreateQuery<Vehiculos>(this.EntityName)
                    .Where(x => x.Descripcion.Equals(pDominio))
                            select t;

                return query.FirstOrDefault();
            }
        }
        #endregion
    }
}