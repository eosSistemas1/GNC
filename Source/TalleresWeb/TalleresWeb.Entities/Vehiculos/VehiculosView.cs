using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    public class VehiculosView
    {
        public String VehiculoDominio { get; set; }
        public String VehiculoMarca { get; set; }
        public String VehiculoModelo { get; set; }
        public int? VehiculoAnio { get; set; }
        public String VehiculoNumeroRA { get; set; }
        public Boolean? VehiculoEsRA { get; set; }
        public Boolean? VehiculoEsInyeccion { get; set; }

        public static VehiculosView VehiculosToVehiculosView(Vehiculos vehiculo)
        {
            if (vehiculo != null)
            {
                VehiculosView vehiculosView = new VehiculosView();
                vehiculosView.VehiculoDominio = vehiculo.Descripcion;
                vehiculosView.VehiculoMarca = vehiculo.MarcaVehiculo;
                vehiculosView.VehiculoModelo = vehiculo.ModeloVehiculo;
                vehiculosView.VehiculoAnio = vehiculo.AnioVehiculo;
                vehiculosView.VehiculoNumeroRA = vehiculo.NumeroRA;
                vehiculosView.VehiculoEsRA = vehiculo.RA;
                vehiculosView.VehiculoEsInyeccion = vehiculo.EsInyeccionVehiculo;
                return vehiculosView;
            }
            return default(VehiculosView);
        }
    }
}
