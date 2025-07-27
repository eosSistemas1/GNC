using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    public class PHViewWebApi : ViewEntity
    {
        public PHViewWebApi()
        {
            this.Cilindros = new List<PHCilindrosViewWebApi>();
        }


        public string NroObleaHabilitante { get; set; }

        public string ClienteRazonSocial { get; set; }
        public string ClienteTipoDocumento { get; set; }
        public string ClientesNumeroDocumento { get; set; }
        public string ClienteDomicilio { get; set; }
        public string ClienteTelefono { get; set; }
        public string ClienteLocalidad { get; set; }
        public string ClienteCodigoPostal { get; set; }
        public string ClienteProvincia { get; set; }
        public string ClienteCelular { get; set; }
        public string ClienteLocalidadID { get; set; }
        public string ClienteEmail { get; set; }

        public string VehiculoMarca { get; set; }
        public string VehiculoModelo { get; set; }
        public string VehiculoDominio { get; set; }
        public int? VehiculoAnio { get; set; }

        public Guid TallerID { get; set; }
        public string TallerRazonSocial { get; set; }
        public string PECRazonSocial { get; set; }

        public List<PHCilindrosViewWebApi> Cilindros { get; set; }
        public Guid? UsuarioID { get; set; }
        public Guid? PECID { get; set; }

        public static PHViewWebApi GetViewFromEntity(PH ph)
        {
            PHViewWebApi valor = new PHViewWebApi()
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
                ClienteCelular = ph.Clientes.CelularCliente,
                ClienteLocalidadID = ph.Clientes.IdLocalidad.ToString(),
                ClienteEmail = ph.Clientes.MailCliente
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
                    if (cil.Valvula_Unidad.Valvula != null && cil.Valvula_Unidad.Valvula.MarcasValvulas != null)
                        marcaValvula = cil.Valvula_Unidad.Valvula.MarcasValvulas.Descripcion;
                }
                catch { }

                PHCilindrosViewWebApi cilindroPH = new PHCilindrosViewWebApi()
                {
                    NumeroSerieCilindro = cil.CilindrosUnidad.Descripcion.Trim(),
                    AnioFabCilindro = cil.CilindrosUnidad.AnioFabCilindro.HasValue ? cil.CilindrosUnidad.AnioFabCilindro.Value.ToString("00") : String.Empty,
                    MesFabCilindro = cil.CilindrosUnidad.MesFabCilindro.HasValue ? cil.CilindrosUnidad.MesFabCilindro.Value.ToString("00") : String.Empty,
                    Capacidad = cil.CilindrosUnidad.Cilindros.CapacidadCil.HasValue ? cil.CilindrosUnidad.Cilindros.CapacidadCil.Value.ToString() : String.Empty,
                    MarcaCilindro = marcaCilindro,
                    NumeroSerieValvula = cil.Valvula_Unidad.Descripcion,
                    MarcaValvula = marcaValvula
                };

                valor.Cilindros.Add(cilindroPH);
            }

            return valor;
        }
    }

    public class PHCilindrosViewWebApi
    {
        public string NumeroSerieCilindro { get; set; }
        public string MarcaCilindro { get; set; }
        public string Capacidad { get; set; }
        public string MesFabCilindro { get; set; }
        public string AnioFabCilindro { get; set; }
        public string MarcaValvula { get; set; }
        public string NumeroSerieValvula { get; set; }
        public string CodigoHomologacionCilindro { get; set; }
        public string CodigoHomologacionValvula { get; set; }
        public Guid? IdEstadoPH { get; set; }
        public string Observacion { get; set; }
        public Guid ID { get; set; }
    }
}
