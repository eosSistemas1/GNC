using System;
using System.ComponentModel;

namespace CrossCutting.DatosDiscretos
{

    public static class GetDinamyc
    {
        public static String LogoEmpresa = System.Web.Configuration.WebConfigurationManager.AppSettings["LOGOEMPRESA"].ToString();
        public static String RazonSocialEmpresa = System.Web.Configuration.WebConfigurationManager.AppSettings["RAZONSOCIALEMPRESA"].ToString();
        public static String CuitEmpresa = System.Web.Configuration.WebConfigurationManager.AppSettings["CUITEMPRESA"].ToString();
        public static String FormatoNumerico2d = "0.00";
        public static String FormatoNumerico4d = "0.0000";
        //public static int CantCopiasCartaPH = int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASPH"].ToString());
        public static int CantCopiasFacturaVenta = 0;//int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASFACTURAVENTA"].ToString());
        public static int CantCopiasRemitoVenta = 0; //int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASREMITOVENTA"].ToString());
        public static DateTime MinDatetime = new DateTime(1800, 01, 01);
        public static DateTime MaxDatetime = new DateTime(2200, 01, 01);
        //public static int CantDiasVencimientoOT = int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["DIASVENCOT"].ToString());
        //public static int CantCopiasFichaTecnica = int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASFT"].ToString());
        //public static int CantCopiasMovCaja = int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["COPIASPAGO"].ToString());
        public static String UrlArchivosEnte = System.Web.Configuration.WebConfigurationManager.AppSettings["URLARCHIVOSENTE"]?.ToString();
        public static String PasswordEliminacion = System.Web.Configuration.WebConfigurationManager.AppSettings["PASSWORDELIMINACION"]?.ToString();
        public static String UrlImagenesObleas = System.Web.Configuration.WebConfigurationManager.AppSettings["URLIMAGENESOBLEAS"]?.ToString();
        public static String SelloIngeniero1 = "selloMazza.png";
    }

    public static class LOCALIDADES
    {
        public static Guid Rosario = new Guid("B895D4AA-0024-4011-AB0E-AACC37F15B51");
    }

    public static class USUARIOS
    {
        public static Guid Admin = new Guid("9E1051FA-B7BC-4AB5-AEE4-2E3E311B3706");
    }

    public static class TIPOVEHICULO
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

    public static class ESTADOSFICHAS
    {
        public static Guid PendienteRevision = new Guid("15C1EBBA-8B6B-4FBB-A692-F815A8B72734");

        public static Guid Aprobada = new Guid("EEFA8BE5-19F0-4998-9F06-167A6D689F0E");
        public static Guid AprobadaConError = new Guid("58D0335C-7448-4B27-9B1B-A72D9A3676AD");

        public static Guid Informada = new Guid("0FE45975-09B8-474A-B652-37F6F23E585B");
        public static Guid InformadaConError = new Guid("5034C935-9784-4311-908B-8253345B3D6E");

        public static Guid Finalizada = new Guid("16D0D4DE-E936-43A4-AF18-016B6D2B85D4");
        public static Guid FinalizadaConError = new Guid("D649DE98-9D18-4E02-8502-217236160E0D");

        public static Guid Eliminada = new Guid("BAC37237-63F0-4C69-B34A-49C281E005BB");
        public static Guid Bloqueada = new Guid("BA3525F2-66FC-4315-AD75-340FD34AE343");

        public static Guid Asignada = new Guid("38BDE77A-B4B2-40BA-8415-281332415066");
        public static Guid AsignadaConError = new Guid("F33F2CE7-66CA-4A18-AF95-2909AC7320DE");
        public static Guid RechazadaPorEnte = new Guid("AA0E0BC0-A12F-423D-A05F-BDF85F3FF780");

        public static Guid Despachada = new Guid("30E8374E-C2DD-4EE2-BE6D-08EE9D6EB68E");
        public static Guid Entregada = new Guid("1d009290-1e88-471f-99d3-a4e85b5ef9d9");
    }

    public static class EstadosPH
    {
        public static Guid Ingresada = new Guid("3139753b-9cd7-4eb4-91f6-e15f1037c37a");
        public static Guid EnEsperaCilindros = new Guid("4fe9f2a3-2cc7-4957-8837-9ca19b659982");
        public static Guid EnProceso = new Guid("27114a6b-b593-493a-9f12-453b885094a7");
        public static Guid Finalizada = new Guid("20b48012-bbcc-4b1c-aed2-5dfd69c18bab");
        public static Guid Bloqueada = new Guid("da927ace-be38-4b57-951e-017ba63b5796");
        public static Guid Despachada = new Guid("80d09ec0-e983-48bf-950b-c71021942773");
        public static Guid Entregada = new Guid("b02e804c-9dc5-496d-8a53-6082e15acf90");
        public static Guid VerificarCodigos = new Guid("DF9894E1-AC41-45FD-B58B-7A5CDD9670A3");
        public static Guid IngresarEnLinea = new Guid("3EB2D29C-14AF-42E0-BBBB-594E39DEA18B");
        public static Guid SolicitaVerificacion = new Guid("1785046E-6335-43EA-84F2-3A2445093DD3");
        public static Guid ExcelGenerado = new Guid("beccd0c2-df61-4706-b150-ac223b90265f");
        public static Guid Observar = new Guid("420cfbbe-cda3-4d3f-8cf8-1d22bf6f1189");
        public static Guid Informada = new Guid("02612375-6A19-44CA-9FA1-3D171951437A");
    }

    public static class FLETE
    {
        public static Guid RetiroOficina = new Guid("9384D047-0A1D-49F9-BAFE-BDAB633856CA");
        public static Guid FletePropio = new Guid("D732E1F6-01A0-4C3C-8CB4-5B6B16913976");
        public static Guid FleteNoPropio = new Guid("F2E8998E-C2C3-4D7A-8762-D55AC5F00238");
    }

    public static class PEC
    {
        public static Guid PEAR = new Guid("86897FAE-E3A5-4CDA-BC28-0BFCE4E9AAFB");
    }

    public static class PEC_RT
    {
        public static Guid PEC_RT_Principal = new Guid("F73EA3BD-F574-475E-9FA7-0C7F5B890966");
    }

    public static class CRPC
    {
        public static String CodigoCRPC = System.Web.Configuration.WebConfigurationManager.AppSettings["CODIGOCRPC"];
        public static Guid PEAR = new Guid("D53A8119-0B84-43E5-B92B-8C7390CD7CE4");
        public static Guid FAB = new Guid("816FA65A-BAA3-47AB-A5F3-A3D6B729A95F");
    }

    public static class MSDB
    {
        public static Guid Montaje = new Guid("99a81ad0-aa6b-417f-a805-0673b3fbffe6");
        public static Guid Sigue = new Guid("62c39fab-fcfa-4dd7-9ac1-9353d1aeaeb1");
        public static Guid Desmontaje = new Guid("4fa0916b-58fe-47fb-bbc3-716e4ccaea6c");
        public static Guid Baja = new Guid("a6eec6ee-080f-4b5d-b7bf-58316186375b");
    }

    public static class TIPOOPERACION
    {
        public static Guid Conversion = new Guid("7feb144d-c501-4112-ab69-af22ac9eebc0");
        public static Guid Modificacion = new Guid("88c34f11-c9d9-4531-af3d-255559b8202d");
        public static Guid RevisionAnual = new Guid("de953ebc-a81a-4c78-a1e6-dc6425747c73");
        public static Guid RevisionCRPC = new Guid("5fc69fec-dd4b-4ca6-aec6-d6ace4bd64a5");
        public static Guid Baja = new Guid("a6eec6ee-080f-4b5d-b7bf-58316186375b");
        public static Guid Reemplazo = new Guid("866baae7-ae27-47bb-aa5e-5bc32f5aa97a");
    }

    public static class Imagenes
    {
        public static String Accept = "/Images/accept.png";
        public static String Exclamation = "/Images/exclamation.png";
    }

    public static class ZONAS
    {
        public static String Norte = "NORTE";
        public static String Sur = "SUR";
        public static String Este = "ESTE";
        public static String Oeste = "OESTE";
        public static String Centro = "CENTRO";
        public static String Comisionista = "COMISIONISTA";
    }

    public static class CodigoRol
    {
        public static String Administrador = "admin";
    }

    public static class ROL
    {
        #region Members
        public static Guid Administrador = new Guid("cc58abf6-fc0b-486d-bc4b-05c8857b7f36");
        public static Guid Taller = new Guid("494956b3-5e21-4ab3-a706-fd5bf5243e0b");
        public static Guid Ingeniero = new Guid("9e79124a-c717-469e-8724-ba84585dcbde");
        #endregion
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

    public static class MARCASINEXISTENTES
    {
        public static Guid Reguladores = new Guid("71DDE79E-9C80-47E7-B040-5ED9E159BF70");
        public static Guid Cilindros = new Guid("01387DC6-E27E-4D9A-9299-C581A68931B1");
        public static Guid Valvulas = new Guid("760224CE-5904-4A3A-B130-7CF1F2AF89E2");
    }

    public static class TIPOSFONDOS
    {
        public static string CONVEXO = "CONVEXO";
        public static string CONCAVO = "CONCAVO";
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

    public static class ERRORTIPO
    {
        public static Guid DOMINIO = new Guid("c65dbe5e-12ab-48f9-b295-f80e8acf405d");
        public static Guid REGULADORSerie = new Guid("d6008e25-6c4e-45dd-b504-05b88f9874db");
        public static Guid REGULADORHomologacion = new Guid("00398f56-fad6-4831-9235-decc67592e09");
        public static Guid CILINDROSerie = new Guid("52b4616b-80a0-4b47-9cc5-6acdea2a32da");
        public static Guid CILINDROHomologacion = new Guid("451baea7-8303-4162-86bb-7392eff8ce5a");
        public static Guid VALVULASerie = new Guid("604055ed-9b2d-4b76-a647-7cd16890d82c");
        public static Guid VALVULAHomologacion = new Guid("a0512ff8-eb7d-408a-8be5-573409f8d856");
    }

    public static class INSPECCIONTIPO
    {
        public static Guid ROSCA = new Guid("09957fca-efe9-431b-a891-3a85d450beab");
        public static Guid INTERIOR = new Guid("02a676aa-9688-4423-9065-4b1e497cdff0");
        public static Guid EXTERIOR = new Guid("74ae7f9d-851c-45f9-b9f1-d995ed895dc9");
    }

    public static class INSPECCIONES
    {
        public static Guid ABOLLADURAS = new Guid("7ECCD71F-25FE-498D-AC04-D5F74C6D7222");
        public static Guid ABOLLADURAS_CON_ESTRIAS = new Guid("A00AA492-9240-4AD1-BE1A-049F7AE5DCAA");

        public static Guid CORROSION_GENERALIZADA_EXTERIOR = new Guid("A435B66B-7B45-4F1F-A954-5982C9F92625");
        public static Guid CORROSION_GENERALIZADA_INTERIOR = new Guid("F2E273F1-D930-46C6-92BF-7BF4A6817C9C");
        public static Guid CORROSION_LOCALIZADA_EXTERIOR = new Guid("7E4BA54F-189F-40D8-9AEA-B8A29C4F7324");
        public static Guid CORROSION_LOCALIZADA_INTERIOR = new Guid("73DCF7C3-CCE0-48B3-95E8-7ECA8576BF0B");

        public static Guid DAÑO_POR_FUEGO = new Guid("EA2F7CD0-4122-4ED9-9EC8-D4460E606621");

        public static Guid DEFECTOS_DEL_CUELLO = new Guid("2E01FD76-DAAE-4F7D-BB65-4C0F770F16F2");

        public static Guid DEFORMACION_ROSCA = new Guid("66F28A83-DECB-4D46-A9D5-CDCCF49E6AD0");

        public static Guid DESGASTE_LOCAL = new Guid("CC69A3E9-B79A-41D0-B6E6-400A745938E5");

        public static Guid ESPESOR_INSUFICIENTE = new Guid("87E71CE7-E363-4466-8D22-9849C901D8CC");

        public static Guid FALTA_MATERIAL = new Guid("D7786F43-FF30-4E69-86F3-083FD90C60CC");

        public static Guid FISURA_EXTERIOR = new Guid("E90951FA-0C5D-4DB7-8428-C14334E2815F");
        public static Guid FISURA_INTERIOR = new Guid("8B9A6BBB-E5DD-496C-B838-7B75DC926442");
        public static Guid FISURA_ROSCA = new Guid("7FEB248B-6752-4162-9A8F-CB53E753639F");

        public static Guid GLOBOS = new Guid("5713BE27-C809-4937-8819-DB8FD405BE41");

        public static Guid LAMINADO_EXTERIOR = new Guid("1154624A-9543-4B64-BDCF-2DBDD8826B0C");
        public static Guid LAMINADO_INTERIOR = new Guid("016A79BF-ED84-48D6-82CE-288B52F3C6B7");

        public static Guid OVALADO = new Guid("036FE475-1DD9-4A89-8229-5E9DF00DC81B");

        public static Guid PERFIL_INCOMPLETO = new Guid("A9043789-1515-454A-8A14-B7ABACD7A129");

        public static Guid PICADURAS_AISLADAS_EXTERIOR = new Guid("824FA1BD-B321-4475-A107-8CDD93950E32");
        public static Guid PICADURAS_AISLADAS_INTERIOR = new Guid("95EE351F-3CCE-45CA-8820-F482AFEE9DE7");

        public static Guid PINCHADURAS = new Guid("6A17CEEA-518B-48E3-8D8A-648DA64B3156");
        
        public static Guid PINTURA_INTERIOR_DEFECTUOSA = new Guid("3EF2F874-232F-442A-951C-62BA34679381");
    }

    public static class ESTACIONES
    {
        public static string ESTACION1 = "Estación 1.- Verificar Marcado del cilindro";
        public static string ESTACION2 = "Estación 2.- Registro peso C/agua.";
        public static string ESTACION3 = "Estación 3.- Inspección Interior.";
        public static string ESTACION4 = "Estación 4.- Cilindros Observados.";
    }

    public enum PasosProcesoPH
    {
        //ESTACION 1
        [Description("Inspección Visual")]
        InspeccionVisual,
        [Description("Inspección Roscas")]
        InspeccionRoscas,
        [Description("Inspección Exterior")]
        InspeccionExterior,
        [Description("Medición Espesores")]
        MedicionEspesores,
        [Description("Registro Peso")]
        RegistroPeso,

        //ESTACION 2
        [Description("Prueba Hidráulica")]
        PruebaHidraulica,

        //ESTACION 3
        [Description("Inspección Interior")]
        InspeccionInterior,

        //ESTACION Observados
        [Description("Cilindros Observados")]
        CilindrosObservados,

    }




}