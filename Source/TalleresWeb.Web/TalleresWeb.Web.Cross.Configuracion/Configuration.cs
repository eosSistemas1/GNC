using System;

namespace CrossCutting.DatosDiscretos
{
    public static class Configuracion
    {
        public static String LogoEmpresa = System.Web.Configuration.WebConfigurationManager.AppSettings["LOGOEMPRESA"].ToString();
        public static String RazonSocialEmpresa = System.Web.Configuration.WebConfigurationManager.AppSettings["RAZONSOCIALEMPRESA"].ToString();        
        public static String DomicilioEmpresa = System.Web.Configuration.WebConfigurationManager.AppSettings["DOMICILIOEMPRESA"].ToString();
        public static String LocalidadEmpresa = System.Web.Configuration.WebConfigurationManager.AppSettings["LOCALIDADEMPRESA"].ToString();
        public static String CUITEmpresa = System.Web.Configuration.WebConfigurationManager.AppSettings["CUITEMPRESA"].ToString();
        public static String IIBBEmpresa = System.Web.Configuration.WebConfigurationManager.AppSettings["IIBBEMPRESA"].ToString();
        public static String IVAEmpresa = System.Web.Configuration.WebConfigurationManager.AppSettings["IVAEMPRESA"].ToString();
        public static String NroAgRetencionIIBB = System.Web.Configuration.WebConfigurationManager.AppSettings["NROAGRETENCIONIIBB"].ToString();

        public static String FormatoNumerico2d = System.Web.Configuration.WebConfigurationManager.AppSettings["FORMATONUMERICO2D"].ToString();
        public static String FormatoNumerico4d = System.Web.Configuration.WebConfigurationManager.AppSettings["FORMATONUMERICO"].ToString();
        public static int CantCopiasFacturaVenta = System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASFACTURAVENTA"] != null ? int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASFACTURAVENTA"].ToString()) : 1;
        public static int CantCopiasRemitoVenta = System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASREMITOVENTA"] != null ? int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASREMITOVENTA"].ToString()) : 1;
        public static int CantCopiasPago = System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASPAGO"] != null ? int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASPAGO"].ToString()) : 1;
        public static int CantCopiasCertifRetIIBB = System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASCERTIFIIBB"] != null ? int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASCERTIFIIBB"].ToString()) : 1;
        public static int CantCopiasCobranza = System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASCOBRANZA"] != null ? int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASCOBRANZA"].ToString()) : 1;
        public static int CantCopiasPedidoVenta = System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASPEDIDOS"] != null ? int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASPEDIDOS"].ToString()) : 1;
        public static int CantRenglonesFactura = int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["RENGLONESFACTURA"].ToString());
        public static DateTime MinDatetime = new DateTime(1800, 01, 01);
        public static DateTime MaxDatetime = new DateTime(2200, 01, 01);
        public static Boolean CalculaRetencionesIIBB = System.Web.Configuration.WebConfigurationManager.AppSettings["CALCULARRETENCIONESIIBB"] != null ? Boolean.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["CALCULARRETENCIONESIIBB"].ToString()) : false;

        public static Guid ProductoRedondeo = new Guid("BF55320E-3A71-4079-86F3-2666C5BD80B6");

        public static Guid Pedido = new Guid("85189831-A176-4EC9-AACB-1C30C1812AF1");

        public static Guid Sucursal = new Guid("73B724AF-F39D-4989-968B-068660143DAA");
    }
}
