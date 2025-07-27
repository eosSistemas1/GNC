using PL.Fwk.Entities;
using System;
using System.Collections.Generic;

namespace TalleresWeb.Entities
{
    public class PHPrintViewWebApi : ViewEntity
    {
        public PHPrintViewWebApi()
        {
            this.Cilindros = new List<PHCilindrosPrintView>();
        }


        public DateTime FechaOperacion { get; set; }
        public string NroObleaHabilitante { get; set; }

        public string ClienteRazonSocial { get; set; }
        public string ClienteTipoDocumento { get; set; }
        public string ClientesNumeroDocumento { get; set; }
        public string ClienteDomicilio { get; set; }
        public string ClienteTelefono { get; set; }
        public string ClienteLocalidad { get; set; }
        public string ClienteCodigoPostal { get; set; }
        public string ClienteProvincia { get; set; }

        public string VehiculoMarca { get; set; }
        public string VehiculoModelo { get; set; }
        public string VehiculoDominio { get; set; }

        public string TallerRazonSocial { get; set; }
        public string PECRazonSocial { get; set; }

        public List<PHCilindrosPrintView> Cilindros { get; set; }

        public static PHPrintViewWebApi GetViewFromEntity(PH ph)
        {
            PHPrintViewWebApi valor = new PHPrintViewWebApi()
            {
                ID = ph.ID,
                NroObleaHabilitante = ph.NroObleaHabilitante,
                TallerRazonSocial = ph.Talleres.Descripcion,
                ClienteRazonSocial = ph.Clientes.Descripcion,
                ClienteTipoDocumento = ph.Clientes.DocumentosClientes.Descripcion,
                ClientesNumeroDocumento = ph.Clientes.NroDniCliente,
                ClienteDomicilio = ph.Clientes.CalleCliente,
                ClienteTelefono = ph.Clientes.TelefonoCliente,
                ClienteLocalidad = ph.Clientes.Localidades.Descripcion,
                ClienteCodigoPostal = ph.Clientes.Localidades.CodigoPostal,
                ClienteProvincia = ph.Clientes.Localidades.Provincias.Descripcion,
                VehiculoDominio = ph.Vehiculos.Descripcion,
                VehiculoMarca = ph.Vehiculos.MarcaVehiculo,
                VehiculoModelo = ph.Vehiculos.ModeloVehiculo,
                PECRazonSocial = ph.PEC.Descripcion,
                FechaOperacion = ph.FechaOperacion
            };

            foreach (var cil in ph.PHCilindros)
            {
                string marcaCilindro = "Sin Marca";
                string marcaValvula = "Sin Marca";
                try
                {
                    if (cil.CilindrosUnidad.Cilindros != null && cil.CilindrosUnidad.Cilindros.MarcasCilindros != null)
                        marcaCilindro = cil.CilindrosUnidad.Cilindros.MarcasCilindros.Descripcion;
                }
                catch { }

                try
                {
                    if (cil.Valvula_Unidad != null && cil.Valvula_Unidad.Valvula != null && cil.Valvula_Unidad.Valvula.MarcasValvulas != null)
                        marcaValvula = cil.Valvula_Unidad.Valvula.MarcasValvulas.Descripcion;
                }
                catch { }

                PHCilindrosPrintView cilindroPH = new PHCilindrosPrintView()
                {
                    CodigoHomologacionCilindro = cil.CilindrosUnidad.Cilindros.Descripcion.ToUpper().Trim(),
                    NumeroSerieCilindro = cil.CilindrosUnidad.Descripcion.Trim(),
                    AnioFabCilindro = cil.CilindrosUnidad.AnioFabCilindro.HasValue ? cil.CilindrosUnidad.AnioFabCilindro.Value.ToString("00") : String.Empty,
                    MesFabCilindro = cil.CilindrosUnidad.MesFabCilindro.HasValue ? cil.CilindrosUnidad.MesFabCilindro.Value.ToString("00") : String.Empty,
                    Capacidad = cil.CilindrosUnidad.Cilindros.CapacidadCil.HasValue ? cil.CilindrosUnidad.Cilindros.CapacidadCil.Value.ToString() : String.Empty,
                    MarcaCilindro = marcaCilindro,
                    CodigoHomologacionValvula = cil.Valvula_Unidad.Valvula.Descripcion.ToUpper().Trim(),
                    NumeroSerieValvula = cil.Valvula_Unidad != null ? cil.Valvula_Unidad.Descripcion : string.Empty,
                    MarcaValvula = marcaValvula
                };

                valor.Cilindros.Add(cilindroPH);
            }

            return valor;
        }
    }

    public class PHCilindrosPrintView
    {
        public string CodigoHomologacionCilindro { get; set; }
        public string NumeroSerieCilindro { get; set; }
        public string MarcaCilindro { get; set; }
        public string Capacidad { get; set; }
        public string MesFabCilindro { get; set; }
        public string AnioFabCilindro { get; set; }
        public string MarcaValvula { get; set; }
        public string CodigoHomologacionValvula { get; set; }
        public string NumeroSerieValvula { get; set; }
    }
}
