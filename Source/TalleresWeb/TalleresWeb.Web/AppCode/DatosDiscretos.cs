using System;

namespace CrossCutting.DatosDiscretos
{
    public static class CRPCs
    {
        #region Members

        public static Guid PEAR = new Guid("359B1E2A-541C-48CE-B768-3BC00C86DC4D");

        #endregion
    }

    public static class ESTADOSFICHAS
    {
        #region Members

        public static Guid Bloqueada = new Guid("0a8a032d-9263-4ac9-ab0e-77a1eef74997");
        public static Guid Eliminada = new Guid("cc40d9f2-e2d8-4e0f-aa02-b6a201dfa388");
        public static Guid EnEntrega = new Guid("01878FD7-9F0C-4D8B-B67A-463483A715DA");
        public static Guid EnEsperaDeOriginales = new Guid("CADC2079-9052-4CBD-804B-7271E200D998");
        public static Guid Informada = new Guid("8EAC4809-E766-4011-8806-4621AFBA6384");
        public static Guid PendienteRevision = new Guid("15c1ebba-8b6b-4fbb-a692-f815a8b72734");

        #endregion
    }

    public static class ESTADOSPH
    {
        #region Members

        public static Guid Bloqueada = new Guid("da927ace-be38-4b57-951e-017ba63b5796");
        public static Guid EnEsperaCilindros = new Guid("4fe9f2a3-2cc7-4957-8837-9ca19b659982");
        public static Guid EnProceso = new Guid("27114a6b-b593-493a-9f12-453b885094a7");
        public static Guid Finalizada = new Guid("20b48012-bbcc-4b1c-aed2-5dfd69c18bab");
        public static Guid Informada = new Guid("02612375-6a19-44ca-9fa1-3d171951437a");
        public static Guid Ingresada = new Guid("3139753b-9cd7-4eb4-91f6-e15f1037c37a");

        #endregion
    }

    public static class GetDinamyc
    {
        #region Members

        public static int CantCopiasCartaPH = int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASPH"].ToString());
        public static int CantCopiasFacturaVenta = 0;
        public static int CantCopiasFichaTecnica = int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASFT"].ToString());
        public static int CantCopiasMovCaja = int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASPAGO"].ToString());

        //int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASFACTURAVENTA"].ToString());
        public static int CantCopiasRemitoVenta = 0;

        public static int CantDiasVencimientoOT = int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["DIASVENCOT"].ToString());
        public static String CuitEmpresa = System.Web.Configuration.WebConfigurationManager.AppSettings["CUITEMPRESA"].ToString();
        public static String FormatoNumerico2d = System.Web.Configuration.WebConfigurationManager.AppSettings["FORMATONUMERICO2D"].ToString();
        public static String FormatoNumerico4d = System.Web.Configuration.WebConfigurationManager.AppSettings["FORMATONUMERICO4D"].ToString();
        public static String LogoEmpresa = System.Web.Configuration.WebConfigurationManager.AppSettings["LOGOEMPRESA"].ToString();
        public static DateTime MaxDatetime = new DateTime(2200, 01, 01);

        //int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASREMITOVENTA"].ToString());
        public static DateTime MinDatetime = new DateTime(1800, 01, 01);

        public static String RazonSocialEmpresa = System.Web.Configuration.WebConfigurationManager.AppSettings["RAZONSOCIALEMPRESA"].ToString();

        #endregion
    }

    public static class IMAGENES
    {
        //public const String Observaciones = SiteMaster.UrlBase + @"Imagenes/Iconos/observaciones.png";
        //public const String Modificar = SiteMaster.UrlBase + @"Imagenes/Iconos/modificar.png";
        //public const String Procesar = SiteMaster.UrlBase + @"Imagenes/Iconos/modificar.png";
        //public const String Eliminar = SiteMaster.UrlBase + @"Imagenes/Iconos/eliminar.png";
        //public const String Imprimir = SiteMaster.UrlBase + @"Imagenes/Iconos/imprimir.png";
    }

    public static class INSPECCIONES
    {
        #region Members

        public static Guid ABOLLADURAS = new Guid("b62017bd-dc9a-493e-8911-ee236dd86eaf");
        public static Guid CORROSION = new Guid("ff9dac5b-7f06-4301-b9d9-65943e71e04f");
        public static Guid DEFORMACIONMARCADO = new Guid("f4039d69-2b04-44a4-82b1-23184bd969b1");
        public static Guid DESGASTELOCALIZADO = new Guid("c2c963c1-9a4c-4f47-9ac0-76162f16b902");
        public static Guid ESTRIAS = new Guid("849cf03f-4b07-45bb-9d42-a5112c57161d");
        public static Guid EXPANSIONEXCESIVA = new Guid("ad34ff52-328b-4c2d-a722-331abee3faef");
        public static Guid FISURAS = new Guid("6ec5fc9f-aa24-4e23-bde8-491e22e54afe");
        public static Guid FUEGO = new Guid("310bf2ff-c985-4e48-a92c-5e5881061c4a");
        public static Guid GLOBOS = new Guid("dd160828-c73d-46c5-9816-530dbee79843");
        public static Guid LAMINADO = new Guid("e6b46bb9-a1bd-462d-a012-ad25d780b651");
        public static Guid OTROS = new Guid("1d34362e-d18d-4dce-bb6a-822d84176ff2");
        public static Guid OVALADO = new Guid("ba083a39-8b2d-47e6-b548-6448fc9580b8");
        public static Guid PERDIDAMASA = new Guid("f01acee2-02a8-4d8e-a5b9-11a9d02340b1");
        public static Guid PINCHADURAS = new Guid("9602fb45-51ea-4013-9a85-2c4b36669420");
        public static Guid ROSCADEFECTUOSA = new Guid("1df9c6ad-adb3-462c-b280-e72e314d568d");

        #endregion
    }

    public static class MARCASINEXISTENTES
    {
        #region Members

        public static Guid Cilindros = new Guid("01387DC6-E27E-4D9A-9299-C581A68931B1");
        public static Guid Reguladores = new Guid("71DDE79E-9C80-47E7-B040-5ED9E159BF70");
        public static Guid Valvulas = new Guid("760224CE-5904-4A3A-B130-7CF1F2AF89E2");

        #endregion
    }

    public static class MSDB
    {
        #region Members

        public static Guid Baja = new Guid("a6eec6ee-080f-4b5d-b7bf-58316186375b");
        public static Guid Desmontaje = new Guid("4fa0916b-58fe-47fb-bbc3-716e4ccaea6c");
        public static Guid Montaje = new Guid("99a81ad0-aa6b-417f-a805-0673b3fbffe6");
        public static Guid Sigue = new Guid("62c39fab-fcfa-4dd7-9ac1-9353d1aeaeb1");

        #endregion
    }

    public static class PEC
    {
        #region Members

        public static Guid PEAR = new Guid("86897FAE-E3A5-4CDA-BC28-0BFCE4E9AAFB");

        #endregion
    }

    public static class TIPOOPERACION
    {
        #region Members

        public static Guid Baja = new Guid("a6eec6ee-080f-4b5d-b7bf-58316186375b");
        public static Guid Conversion = new Guid("7feb144d-c501-4112-ab69-af22ac9eebc0");
        public static Guid Modificacion = new Guid("88c34f11-c9d9-4531-af3d-255559b8202d");
        public static Guid RevisionAnual = new Guid("de953ebc-a81a-4c78-a1e6-dc6425747c73");
        public static Guid RevisionCRPC = new Guid("5fc69fec-dd4b-4ca6-aec6-d6ace4bd64a5");

        #endregion
    }

    public static class TIPOVEHICULO
    {
        #region Members

        public static Guid Autoelevadores = new Guid("9a76004b-79a3-40f0-83d6-1800f5e6770d");
        public static Guid Bus = new Guid("e854d6ec-08fc-4964-bfa4-e08b170d27f2");
        public static Guid Moto = new Guid("8616c426-ee18-4e65-bdb9-ae698462f2c6");
        public static Guid Oficial = new Guid("30b7e7ed-b638-44d8-b983-e294fadb36b6");
        public static Guid Otros = new Guid("a4d07036-c045-48d9-8b9e-8b5955fae5bd");
        public static Guid Particular = new Guid("7cd632c5-73f1-4ee6-9273-c2806e24563d");
        public static Guid PickUp = new Guid("dc058aa9-bc45-4acc-b96d-30d2a81f3b73");
        public static Guid Taxi = new Guid("57715a4f-c095-47e0-86ea-7a102de140f2");

        #endregion
    }

    public static class USO
    {
        #region Members

        public static Guid Autoelevadores = new Guid("9A76004B-79A3-40F0-83D6-1800F5E6770D");
        public static Guid Bus = new Guid("E854D6EC-08FC-4964-BFA4-E08B170D27F2");
        public static Guid Moto = new Guid("8616C426-EE18-4E65-BDB9-AE698462F2C6");
        public static Guid Oficial = new Guid("30B7E7ED-B638-44D8-B983-E294FADB36B6");
        public static Guid Otros = new Guid("A4D07036-C045-48D9-8B9E-8B5955FAE5BD");
        public static Guid Particular = new Guid("7CD632C5-73F1-4EE6-9273-C2806E24563D");
        public static Guid PickUp = new Guid("DC058AA9-BC45-4ACC-B96D-30D2A81F3B73");
        public static Guid Taxi = new Guid("57715A4F-C095-47E0-86EA-7A102DE140F2");

        #endregion
    }

    public static class ROL
    {
        #region Members
        public static Guid Administrador = new Guid("cc58abf6-fc0b-486d-bc4b-05c8857b7f36");
        public static Guid Taller = new Guid("494956b3-5e21-4ab3-a706-fd5bf5243e0b");
        public static Guid Ingeniero = new Guid("9e79124a-c717-469e-8724-ba84585dcbde");
        #endregion
    }

}