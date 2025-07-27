using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TalleresWeb.Entities
{
    [Serializable]
    public class DetalleCilindrosValvulasView : ViewEntity
    {
        public DetalleCilindrosValvulasView()
        {
            this.ID = Guid.NewGuid();
        }

        public Guid IdObleaCilindro { get; set; }
        public Guid IDCilindro { get; set; }
        public Guid IDCilindroUnidad { get; set; }       
        public String CodigoCilindro { get; set; }
        public String NroSerieCilindro { get; set; }
        public String CilindroFabMes { get; set; }
        public String CilindroFabAnio { get; set; }
        public String CilindroRevMes { get; set; }
        public String CilindroRevAnio { get; set; }
        public Guid CRPCCilindroID { get; set; }
        public String CRPCCilindro { get; set; }
        public Guid MSDBCilindroID { get; set; }
        public String MSDBCilindro { get; set; }
        public String NroCertificadoPH { get; set; }

        public Guid IdObleaValvula1 { get; set; }
        public Guid IDValvula1 { get; set; }
        public Guid IDValvula1Unidad { get; set; }        
        public String CodigoValvula1 { get; set; }
        public String NroSerieValvula1 { get; set; }
        public Guid MSDBValvula1ID { get; set; }
        public String MSDBValvula1 { get; set; }

        public Guid IdObleaValvula2 { get; set; }
        public Guid IDValvula2 { get; set; }
        public Guid IDValvula2Unidad { get; set; }
        public String CodigoValvula2 { get; set; }
        public String NroSerieValvula2 { get; set; }
        public Guid MSDBValvula2ID { get; set; }
        public String MSDBValvula2 { get; set; }

        public Boolean RealizaPH { get; set; }

        /// <summary>
        /// Hace las validaciones correspondientes a Cilindro y Válvula al dar de alta
        /// </summary>
        /// <returns></returns>
        public List<String> RegistroValido()
        {
            List<String> mensajes = new List<String>();

            mensajes.AddRange(this.ValidacionesGeneralesCilindro());

            this.ValidarNroCertificadoPH(mensajes);

            if(!mensajes.Any()) this.ValidarCRPCCilindro(mensajes);

            mensajes.AddRange(this.ValidacionesGeneralesValvula1());

            mensajes.AddRange(this.ValidacionesGeneralesValvula2());

            return mensajes;
        }

        private List<String> ValidacionesGeneralesCilindro()
        {
            List<String> mensaje = new List<String>();

            ValidarCodigoHomologacionCilindro(mensaje);

            ValidarNumeroSerieCilindro(mensaje);
                       
            return mensaje;
        }

        private void ValidarNumeroSerieCilindro(List<string> mensaje)
        {
            if (String.IsNullOrEmpty(this.NroSerieCilindro))
            {
                mensaje.Add("- Debe ingresar nro. de serie de cilindro.");
            }
        }

        private void ValidarCodigoHomologacionCilindro(List<string> mensaje)
        {
            if (String.IsNullOrEmpty(this.CodigoCilindro))
            {
                mensaje.Add("- Debe ingresar un código de homologación del cilindro.");
            }
            else
            {
                if (this.CodigoCilindro.Length != 4)
                {
                    mensaje.Add("- El código de homologación del cilindro es incorrecto.");
                }
            }
        }

        // validaciones para PH 
        private void ValidarNroCertificadoPH(List<String> mensajes)
        {
            if (!String.IsNullOrEmpty(this.NroCertificadoPH) && !String.IsNullOrEmpty(this.CRPCCilindro))
            {
                if (this.NroCertificadoPH.Length < 10 ||
                        this.NroCertificadoPH.ToUpper().Substring(0, 4) != this.CRPCCilindro.ToUpper().Trim())

                    mensajes.Add("- El nro. de certificado PH no es correcto.");
            }
        }

        private void ValidarCRPCCilindro(List<String> mensajes)
        {
            if (this.CilindroFabMes == this.CilindroRevMes && this.CilindroFabAnio == this.CilindroRevAnio)
            {
                this.CRPCCilindro = "FAB";
                this.CRPCCilindroID = CrossCutting.DatosDiscretos.CRPC.FAB;
                this.NroCertificadoPH = String.Empty;
            }
            else
            {
                int anio = int.Parse(this.CilindroRevAnio) < 60 ? int.Parse(20 + this.CilindroRevAnio) : int.Parse(19 + this.CilindroRevAnio);
                DateTime fechaRev = new DateTime(anio, int.Parse(this.CilindroRevMes), 1);
                DateTime fechaTope = new DateTime(2009, 10, 1);
            }
        }

        private List<String> ValidacionesGeneralesValvula1()
        {
            List<String> mensaje = new List<String>();

            //ValidarCodigoHomologacionValvula(mensaje);

            //ValidarNumeroSerieValvula(mensaje);

            return mensaje;
        }

        private void ValidarNumeroSerieValvula(List<string> mensaje)
        {
            if (String.IsNullOrEmpty(this.NroSerieValvula1)) mensaje.Add("- Debe ingresar número de serie de la válvula.");
        }

        private void ValidarCodigoHomologacionValvula(List<string> mensaje)
        {
            if (String.IsNullOrEmpty(this.CodigoValvula1))
            {
               mensaje.Add("- Debe ingresar código de homologación de la válvula.");
            }
            else
            {
                if (this.CodigoValvula1.Length != 4) mensaje.Add("- El código de homologación de la válvula es incorrecto.");
            }
        }

        private List<String> ValidacionesGeneralesValvula2()
        {
            if (String.IsNullOrEmpty(this.CodigoValvula2) && String.IsNullOrEmpty(this.NroSerieValvula2)) return new List<String>();

            List<String> mensaje = new List<String>();

            if (String.IsNullOrEmpty(this.CodigoValvula2))
            {
                mensaje.Add("- Debe ingresar código de homologación de la válvula.");
            }
            else
            {
                if (this.CodigoValvula2.Length != 4)
                {
                    mensaje.Add("- El código de homologación de la válvula es incorrecto.");
                }
            }

            if (String.IsNullOrEmpty(this.NroSerieValvula2))
            {
                mensaje.Add("- Debe ingresar número de serie de la válvula.");
            }

            return mensaje;
        }
    }
}
