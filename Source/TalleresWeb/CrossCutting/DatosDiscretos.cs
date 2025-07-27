using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrossCutting.DatosDiscretos
{

    public static class GetDinamyc
    {
        public static String LogoEmpresa = System.Web.Configuration.WebConfigurationManager.AppSettings["LOGOEMPRESA"].ToString();
        public static String RazonSocialEmpresa = System.Web.Configuration.WebConfigurationManager.AppSettings["RAZONSOCIALEMPRESA"].ToString();
        public static String CuitEmpresa = System.Web.Configuration.WebConfigurationManager.AppSettings["CUITEMPRESA"].ToString();
        public static String FormatoNumerico2d = System.Web.Configuration.WebConfigurationManager.AppSettings["FORMATONUMERICO2D"].ToString();
        public static String FormatoNumerico4d = System.Web.Configuration.WebConfigurationManager.AppSettings["FORMATONUMERICO4D"].ToString();
        public static int CantCopiasCartaPH = int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASPH"].ToString());
        public static int CantCopiasFacturaVenta = 0;//int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASFACTURAVENTA"].ToString());
        public static int CantCopiasRemitoVenta = 0; //int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASREMITOVENTA"].ToString());
        public static DateTime MinDatetime = new DateTime(1800, 01, 01);
        public static DateTime MaxDatetime = new DateTime(2200, 01, 01);        
        public static int CantDiasVencimientoOT = int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["DIASVENCOT"].ToString());
        public static int CantCopiasFichaTecnica = int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASFT"].ToString());        
        public static int CantCopiasMovCaja = int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASPAGO"].ToString());
    }

    public static class TipoVehiculo
    {
        public static Guid Oficial = new Guid("30b7e7ed-b638-44d8-b983-e294fadb36b6");
        public static Guid PickUp = new Guid("dc058aa9-bc45-4acc-b96d-30d2a81f3b73");
        public static Guid Otros = new Guid("a4d07036-c045-48d9-8b9e-8b5955fae5bd");
        public static Guid Particular = new Guid("7cd632c5-73f1-4ee6-9273-c2806e24563d");
        public static Guid Taxi = new Guid("57715a4f-c095-47e0-86ea-7a102de140f2");
        public static Guid Bus = new Guid("e854d6ec-08fc-4964-bfa4-e08b170d27f2");
        public static Guid Moto = new Guid("8616c426-ee18-4e65-bdb9-ae698462f2c6");
        public static Guid Autoelevadores = new Guid("9a76004b-79a3-40f0-83d6-1800f5e6770d");
    }

    public static class EstadosFicha
    {        
        public static Guid Aprobada = new Guid("eefa8be5-19f0-4998-9f06-167a6d689f0e");
        public static Guid AprobadaConError = new Guid("58d0335c-7448-4b27-9b1b-a72d9a3676ad");
        public static Guid Bloqueada = new Guid("ba3525f2-66fc-4315-ad75-340fd34ae343");
        public static Guid ErrorInformada = new Guid("5034c935-9784-4311-908b-8253345b3d6e");
        public static Guid Finalizada = new Guid("16d0d4de-e936-43a4-af18-016b6d2b85d4");
        public static Guid Informada = new Guid("0fe45975-09b8-474a-b652-37f6f23e585b");       
        public static Guid PendienteRevision = new Guid("15c1ebba-8b6b-4fbb-a692-f815a8b72734");
    }

    public static class EstadosPH
    {
        public static Guid Ingresada = new Guid("3139753b-9cd7-4eb4-91f6-e15f1037c37a");
        public static Guid EnEsperaCilindros = new Guid("4fe9f2a3-2cc7-4957-8837-9ca19b659982");
        public static Guid EnProceso = new Guid("27114a6b-b593-493a-9f12-453b885094a7");
        public static Guid Finalizada = new Guid("20b48012-bbcc-4b1c-aed2-5dfd69c18bab");
        public static Guid Bloqueada = new Guid("da927ace-be38-4b57-951e-017ba63b5796");
    }

    public static class IdPec
    {
        public static Guid idPEAR = new Guid("86897FAE-E3A5-4CDA-BC28-0BFCE4E9AAFB");
    }

    public static class IdCRPC
    {
        public static Guid idPEAR = new Guid("359B1E2A-541C-48CE-B768-3BC00C86DC4D");
    }

    public static class MSDB
    {
        public static Guid Montaje = new Guid("99a81ad0-aa6b-417f-a805-0673b3fbffe6");
        public static Guid Sigue = new Guid("62c39fab-fcfa-4dd7-9ac1-9353d1aeaeb1");
        public static Guid Desmontaje = new Guid("4fa0916b-58fe-47fb-bbc3-716e4ccaea6c");
        public static Guid Baja = new Guid("a6eec6ee-080f-4b5d-b7bf-58316186375b");
    }

    public static class TipoOperacion
    {
        public static Guid Conversion = new Guid("7feb144d-c501-4112-ab69-af22ac9eebc0");
        public static Guid Modificacion = new Guid("88c34f11-c9d9-4531-af3d-255559b8202d");
        public static Guid RevisionAnual = new Guid("de953ebc-a81a-4c78-a1e6-dc6425747c73");
        public static Guid RevisionCRPC = new Guid("5fc69fec-dd4b-4ca6-aec6-d6ace4bd64a5");
        public static Guid Baja = new Guid("a6eec6ee-080f-4b5d-b7bf-58316186375b");
    }

    public static class Imagenes
    {
        public static String Accept = "/Images/accept.png";
        public static String Exclamation = "/Images/exclamation.png";
    }

    public static class Zonas
    {
        public static String Norte = "Norte";
        public static String Sur = "Sur";
        public static String Este = "Este";
        public static String Oeste = "Oeste";
        public static String Centro = "Centro";
    }

    public static class CodigoRol
    {
        public static String Administrador = "admin";
    }


    public static class TiposDocumentos
    {
        public static Guid DNI = new Guid("16834059-E4D3-418D-9ECF-14FBC740193F");
        public static Guid LE = new Guid("93249994-187A-47E4-A0CC-5D7F8E086283");
        public static Guid PASAPORTE = new Guid("4B19F502-EA31-43FC-9E1D-886A862685D6");
        public static Guid CI = new Guid("70331DFC-352A-4123-B83E-A1D7C7297E10");
        public static Guid LC = new Guid("1BA143B2-06BB-4857-B705-D792FD2B2D64");
        public static Guid CUIT = new Guid("FD0675D6-1FB3-4B24-AB8E-D94F41BF71D7");
    }

    public static class MarcasInexistentes
    {
        public static Guid Reguladores = new Guid("71DDE79E-9C80-47E7-B040-5ED9E159BF70");
        public static Guid Cilindros = new Guid("01387DC6-E27E-4D9A-9299-C581A68931B1");
        public static Guid Valvulas = new Guid("760224CE-5904-4A3A-B130-7CF1F2AF89E2");
    }




    /* -- Talleres GNC -- */
    public static class ESTADOSERVICIO
    {
        public static Guid PRESUPUESTO = new Guid("A0E88C8E-52F2-4AF0-8881-E48CCA5E0317");
        public static Guid INGRESADO = new Guid("97310107-23A8-4C87-8811-44B87539B658");
        public static Guid REVISADO = new Guid("F1D8B9D6-57B8-4CEF-A065-71F0454C061A");
        public static Guid ENTREGADO = new Guid("3FDC8487-BBA4-4ED8-B108-98D20E179DF7");
        public static Guid RECHAZADO = new Guid("8546e8b7-fa30-4047-b125-2eb4992044b7");
        public static Guid ACEPTADO = new Guid("3569D89B-5731-442D-80E9-CC892798C901");
    }

    public static class TIPOTRABAJO
    {
        public static Guid MECANICA = new Guid(System.Web.Configuration.WebConfigurationManager.AppSettings["MECANICA"].ToString());
        public static Guid GNC = new Guid(System.Web.Configuration.WebConfigurationManager.AppSettings["GNC"].ToString());
        public static Guid LUBRICENTRO = new Guid(System.Web.Configuration.WebConfigurationManager.AppSettings["LUBRICENTRO"].ToString());
    }

    public static class VALORES
    {
        public static Guid EFECTIVO = new Guid(System.Web.Configuration.WebConfigurationManager.AppSettings["EFE"].ToString());
        public static Guid CHEQUETERCERO = new Guid(System.Web.Configuration.WebConfigurationManager.AppSettings["CHT"].ToString());
        public static Guid CHEQUEPROPIO = new Guid(System.Web.Configuration.WebConfigurationManager.AppSettings["CHP"].ToString());
        public static Guid TARJETA = new Guid(System.Web.Configuration.WebConfigurationManager.AppSettings["TAR"].ToString());
    }

    public static class TIPOMOVCAJA
    { 
        public static Guid PAGOS = new Guid("6849750B-E96E-48BA-8E56-0F835CE35120");
        public static Guid EGRESOSVARIOS = new Guid("AFFCB9DB-5AA8-482E-BB8E-2FCA0E66E735");
        public static Guid COBRANZA = new Guid("98DBB9A6-F872-433C-98D1-338E9FE90CAA");
        public static Guid INGRESOSVARIOS = new Guid("34E7FF24-A071-4731-B902-ED32767E3BC9");
        public static Guid APERTURA = new Guid("c3fadfe5-0558-49c1-b054-fbe2d42ac93f");
        public static Guid CIERRE = new Guid("5cb5db3d-18f9-4b1f-9eb6-2cb7ada798b0");
    }

    public static class ESTADOSCHEQUES
    {
        public static Guid CARTERA = new Guid("41D5817E-3F50-498C-9BFC-E318444F130B");
        public static Guid ENTREGADO = new Guid("DBEF9313-2BC2-48AA-BBA6-05BB2A04C970");
        public static Guid RECHAZADO = new Guid("322CBEC9-7087-4908-9DBE-6F418E79FD7C");
    }

}