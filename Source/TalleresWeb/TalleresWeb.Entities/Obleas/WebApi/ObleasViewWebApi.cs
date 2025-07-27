using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TalleresWeb.Entities
{
    public class ObleasViewWebApi
    {
        public ObleasViewWebApi()
        {
            Reguladores = new List<ObleasReguladoresExtendedView>();
            Cilindros = new List<ObleasCilindrosExtendedView>();
            Valvulas = new List<ObleasValvulasExtendedView>();
        }

        public string ObleaNumeroAnterior { get; set; }

        public int? VehiculoAnio { get; set; }
        public string VehiculoDominio { get; set; }
        public bool? VehiculoEsInyeccion { get; set; }
        public bool? VehiculoEsRA { get; set; }
        public string VehiculoMarca { get; set; }
        public string VehiculoModelo { get; set; }
        public string VehiculoNumeroRA { get; set; }
        public Guid IdUso { get; set; }

        public Guid ClienteTipoDocumentoID { get; set; }
        public string ClienteNombreApellido { get; set; }
        public string ClienteTipoDocumento { get; set; }
        public string ClienteNumeroDocumento { get; set; }        
        public string ClienteDomicilio { get; set; }
        public Guid ClienteLocalidadID { get; set; }
        public string ClienteLocalidad { get; set; }
        public string ClienteTelefono { get; set; }
        public string ClienteCelular { get; set; }
        public string ClienteEmail { get; set; }

        public List<ObleasReguladoresExtendedView> Reguladores { get; set; }
        public List<ObleasCilindrosExtendedView> Cilindros { get; set; }
        public List<ObleasValvulasExtendedView> Valvulas { get; set; }

        public PHExtendedView PH { get; set; }
        public PHCilindrosExtendedView PHCilindros { get; set; }


        public Guid OperacionID { get; set; }
        public Guid UsuarioID { get; set; }

        public string Observacion { get; set; }
        public DateTime FechaHabilitacion { get; set; }

        public Guid TallerID { get; set; }
        public string TallerRazonSocial { get; set; }
        public string TalleresDomicilio { get; set; }
        public string TallerCuit { get; set; }
        public string TallerMatricula { get; set; }
        public bool ExisteObleaPendiente { get; set; }
        public bool ExisteTramitePendienteParaElDominio { get; set; }
        
    }
}
