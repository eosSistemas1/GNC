using CrossCutting.DatosDiscretos;
using PL.Fwk.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TalleresWeb.DataAccess;
using TalleresWeb.Entities;
using PL.Fwk.Entities;
using System.Linq;

namespace TalleresWeb.Logic
{
    public class VehiculosLogic : EntityManagerLogic<Vehiculos, VehiculosExtendedView, VehiculosParameters, VehiculosDataAccess>
    {
        #region Methods

        public void AddVehiculo(Vehiculos entity)
        {
            var vehiculo = this.Read(entity.ID);

            if (vehiculo == null)
            {
                EntityDataAccess.Add(entity);
            }
            else
            {
                EntityDataAccess.Update(entity);
            }
        }

        public void Add(VehiculosExtendedView entity)
        {            
            EntityDataAccess.Add(entity);
        }

        public List<VehiculosExtendedView> ReadByDominio(String pDominio)
        {
            return EntityDataAccess.ReadByDominio(pDominio);
        }

        public Vehiculos ReadVehiculoByDominio(String pDominio)
        {
            return EntityDataAccess.ReadVehiculoByDominio(pDominio);
        }

        public static Boolean ValidarDominio(VehiculosExtendedView vehiculo)
        {
            String dominio = vehiculo.Descripcion;
            int anio = vehiculo.AnioVehiculo;
            Guid usoID = vehiculo.IdUso;

            if (usoID == TIPOVEHICULO.Particular ||
                usoID == TIPOVEHICULO.Taxi ||
                usoID == TIPOVEHICULO.PickUp ||
                usoID == TIPOVEHICULO.Bus || 
                usoID == TIPOVEHICULO.Oficial || 
                usoID == TIPOVEHICULO.Otros)
            {
                Regex auto = new Regex("^[A-Z][A-Z][A-Z][0-9-][0-9-][0-9-]$");
                Regex autoMercosur = new Regex("^[A-Z][A-Z][0-9-][0-9-][0-9-][A-Z][A-Z]$");

                if (anio == 2016 && (auto.IsMatch(dominio.ToUpper()) || autoMercosur.IsMatch(dominio.ToUpper()))) return true;

                if (anio < 2016 && auto.IsMatch(dominio.ToUpper())) return true;

                if (anio > 2016 && autoMercosur.IsMatch(dominio.ToUpper())) return true;
            }

            if (usoID == TIPOVEHICULO.Moto)
            {
                Regex moto = new Regex("^[0-9-][0-9-][0-9-][A-Z][A-Z][A-Z]$");
                Regex motoMercosur = new Regex("^[A-Z][0-9-][0-9-][0-9-][A-Z][A-Z][A-Z]$");

                if (anio == 2016 && (moto.IsMatch(dominio.ToUpper()) || motoMercosur.IsMatch(dominio.ToUpper()))) return true;

                if (anio < 2016 && moto.IsMatch(dominio.ToUpper())) return true;

                if (anio > 2016 && motoMercosur.IsMatch(dominio.ToUpper())) return true;
            }

            if (usoID == TIPOVEHICULO.Autoelevadores)
            {
                Regex autoelevador = new Regex("^[A-Z][A-Z][0-9-][0-9-][0-9-]$");

                if (autoelevador.IsMatch(dominio.ToUpper())) return true;
            }

            return false;
        }

        public Boolean TieneTramitesPendientes(String dominio)
        {
            return EntityDataAccess.TieneTramitesPendientes(dominio);
        }

        public bool ExisteOtroVehiculoConMismoDominio(Guid idVehiculo, string descripcion)
        {
            VehiculosParameters param = new VehiculosParameters()
            {
                Descripcion = descripcion
            };
            var vehiculos = this.EntityDataAccess.ReadListView(param);

            return vehiculos.Any(v => v.ID != idVehiculo);
        }

        public void CorregirDominio(Guid vehiculoId, string dominioOK)
        {
            var existeOtroVehiculoConElDominio = !this.EntityDataAccess.ReadByDominio(dominioOK).Any();

            if (existeOtroVehiculoConElDominio)
            {
                var vehiculo = this.EntityDataAccess.Read(vehiculoId);
                vehiculo.Descripcion = dominioOK;
                this.Update(vehiculo);
            }
            else
            {
                throw new Exception("Ya existe un vehiculo con el dominio seleccionado");
            }
        }
        #endregion
    }
}