using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Entities;

namespace TalleresWeb.Entities
{
    public class PHCilindrosHojaRutaView
    {
        public DateTime FechaOperacion { get; set; }
        public string NumeroSerie { get; set; }
        public string CodigoHomologacion { get; set; }       
        public double ParedMinimo { get; set; }
        public double Diámetro { get; set; }
        public string NormaFabricacion { get; set; }
        public double FondoMinimo { get; set; }
        public string Marca { get; set; }
        public string MarcaValvula { get; set; }
        public int MesFabricacion { get; set; }
        public int AnioFabricacion { get; set; }
        public DateTime FechaUltimaRevision { get; set; }
        public string RazonSocialTaller { get; set; }
        public int? NroOperacionCRPC { get; set; }
        public string Cliente { get; set; }
        public string NumeroSerieValvula { get; set; }
        public string CodigoHomologacionValvula { get; set; }
        public string MatriculaTaller { get; set; }
        public decimal Capacidad { get; set; }
    }
}
