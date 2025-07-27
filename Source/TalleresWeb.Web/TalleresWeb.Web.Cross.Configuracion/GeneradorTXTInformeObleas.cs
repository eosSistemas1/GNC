using Ionic.Zip;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace TalleresWeb.Web.Cross.Configuracion
{
    public class GeneradorTXTInformeObleas
    {
        private const String extension = ".txt";
        public static String Separador = ";";
        public static String LineaNueva = "\n";

        public static void GenerarUSRTxt(List<USRTxtExtendedView> datos, String dirPath)
        {
            //directorio
            DirectoryInfo DIR = new DirectoryInfo(dirPath);

            if (!DIR.Exists)
            {
                DIR.Create();
            }

            //nombre del archivo
            String fic = dirPath + "\\USR_" + DateTime.Now.ToString("dd_MM_yyyy_hhmm") + extension;
            FileStream stream = File.Open(fic, FileMode.Create);
            StreamWriter filaSB = new StreamWriter(stream);

            foreach (USRTxtExtendedView item in datos)
            {
                filaSB.WriteLine(item.PECCOD + Separador +
                                item.TALCUIT + Separador +
                                item.TNROINTOPR + Separador +
                                item.CODIGOTALLER + Separador +
                                item.TIPODOCRT_PEC + Separador +
                                item.NRODOCRT_PEC + Separador +
                                item.TIPODOCRT_TALLER + Separador +
                                item.NRODOCRT_TALLER + Separador +
                                item.UCODGEST + Separador +
                                item.UDESCGEST + Separador +
                                item.UOBLEAANT + Separador +
                                item.UOBLEANEW + Separador +
                                item.UDOMINIO + Separador +
                                item.UMARCA + Separador +
                                item.UMODELO + Separador +
                                item.UANO + Separador +
                                item.UTIPUSO + Separador +
                                item.UAPEYNOM + Separador +
                                item.UCALLEYNRO + Separador +
                                item.ULOCALIDAD + Separador +
                                item.UPROVINCIA + Separador +
                                item.UCODPOSTAL + Separador +
                                item.UTELEFONO + Separador +
                                item.UTIPODOC + Separador +
                                item.UNRODOC + Separador +
                                item.UFECREV + Separador +
                                item.UFECMONT + Separador +
                                item.UFECHAB + Separador +
                                item.UFECVENHAB + Separador +
                                item.XFECMODREC + Separador +
                                item.XTIPOPRREC);

            }

            filaSB.Close();

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(fic, "");
                zip.Save(fic.Replace(extension, ".zip"));
            }

        }

        public static void GenerarCILTxt(List<CILTxtExtendedView> datos, String dirPath)
        {
            //directorio
            DirectoryInfo DIR = new DirectoryInfo(dirPath);

            if (!DIR.Exists)
            {
                DIR.Create();
            }

            //nombre del archivo
            String fic = @dirPath + "\\CIL_" + DateTime.Now.ToString("dd_MM_yyyy_hhmm") + extension;

            FileStream stream = File.Open(fic, FileMode.Create);
            StreamWriter filaSB = new StreamWriter(stream);

            foreach (CILTxtExtendedView item in datos)
            {
                filaSB.WriteLine(item.PECCOD + Separador +
                item.TALCUIT + Separador +
                item.TNROINTOPR + Separador +
                item.CODIGOTALLER + Separador +
                item.UDOMINIO + Separador +
                item.UTIPUSO + Separador +
                item.CILCODH + Separador +
                item.CNROSERIE + Separador +
                item.CCODGEST + Separador +
                item.CFECGEST + Separador +
                item.CCAPACIDAD + Separador +
                item.CMESFABR + Separador +
                item.CANOFABR + Separador +
                item.CUPHCRPC + Separador +
                item.CUPHMES + Separador +
                item.CUPHANO + Separador +
                item.FECVENCCRPC + Separador +
                item.CUPHRESULT + Separador +
                item.NROCERTIFICADO + Separador +
                item.XFECMODREC + Separador +
                item.XTIPOPRREC);
            }

            filaSB.Close();

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(fic, "");
                zip.Save(fic.Replace(extension, ".zip"));
            }
        }

        public static void GenerarVALCILTxt(List<VALCILTxtExtendedView> datos, String dirPath)
        {
            //directorio
            DirectoryInfo DIR = new DirectoryInfo(dirPath);

            if (!DIR.Exists)
            {
                DIR.Create();
            }

            //nombre del archivo
            String fic = @dirPath + "\\VALCIL_" + DateTime.Now.ToString("dd_MM_yyyy_hhmm") + extension;

            FileStream stream = File.Open(fic, FileMode.Create);
            StreamWriter filaSB = new StreamWriter(stream);

            foreach (VALCILTxtExtendedView item in datos)
            {
                filaSB.WriteLine(item.PECCOD + Separador +
                                item.TALCUIT + Separador +
                                item.TNROINTOPR + Separador +
                                item.CODIGOTALLER + Separador +
                                item.VCDOMINIO + Separador +
                                item.TIPUSO + Separador +
                                item.VCCODVALVULA + Separador +
                                item.VCNROSERIE + Separador +
                                item.VCCODGEST + Separador +
                                item.VCFECGEST + Separador +
                                item.CILCODH + Separador +
                                item.CNROSERIE + Separador +
                                item.XTIPOPRREC);
            }

            filaSB.Close();

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(fic, "");
                zip.Save(fic.Replace(extension, ".zip"));
            }
        }

        public static void GenerarREGTxt(List<REGTxtExtendedView> datos, String dirPath)
        {
            //directorio
            DirectoryInfo DIR = new DirectoryInfo(dirPath);

            if (!DIR.Exists)
            {
                DIR.Create();
            }

            //nombre del archivo
            String fic = @dirPath + "\\REG_" + DateTime.Now.ToString("dd_MM_yyyy_hhmm") + extension;

            FileStream stream = File.Open(fic, FileMode.Create);
            StreamWriter filaSB = new StreamWriter(stream);

            foreach (REGTxtExtendedView item in datos)
            {
                filaSB.WriteLine(item.PECCOD + Separador +
                                    item.TALCUIT + Separador +
                                    item.TNROINTOPR + Separador +
                                    item.CODIGOTALLER + Separador +
                                    item.UDOMINIO + Separador +
                                    item.TIPUSO + Separador +
                                    item.RCODREG + Separador +
                                    item.RNROSERIE + Separador +
                                    item.RCODGEST + Separador +
                                    item.RFECGEST + Separador +
                                    item.XFECMODREC + Separador +
                                    item.XTIPOPRREC);
            }

            filaSB.Close();

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(fic, "");
                zip.Save(fic.Replace(extension, ".zip"));
            }
        }

        public class USRTxtExtendedView : ViewEntity
        {
            public String PECCOD { get; set; }
            public String TALCUIT { get; set; }
            public String TNROINTOPR { get; set; }
            public String CODIGOTALLER { get; set; }
            public String TIPODOCRT_PEC { get; set; }

            public String NRODOCRT_PEC { get; set; }
            public String TIPODOCRT_TALLER { get; set; }
            public String NRODOCRT_TALLER { get; set; }
            public String UCODGEST { get; set; }
            public String UDESCGEST { get; set; }
            public String UOBLEAANT { get; set; }
            public String UOBLEANEW { get; set; }
            public String UDOMINIO { get; set; }
            public String UMARCA { get; set; }
            public String UMODELO { get; set; }
            public String UANO { get; set; }
            public String UTIPUSO { get; set; }
            public String UAPEYNOM { get; set; }
            public String UCALLEYNRO { get; set; }
            public String ULOCALIDAD { get; set; }
            public String UPROVINCIA { get; set; }
            public String UCODPOSTAL { get; set; }
            public String UTELEFONO { get; set; }
            public String UTIPODOC { get; set; }
            public String UNRODOC { get; set; }
            public String UFECREV { get; set; }
            public String UFECMONT { get; set; }
            public String UFECHAB { get; set; }
            public String UFECVENHAB { get; set; }
            public String XFECMODREC { get; set; }
            public String XTIPOPRREC { get; set; }
        }

        public class CILTxtExtendedView : ViewEntity
        {
            public String PECCOD { get; set; }
            public String TALCUIT { get; set; }
            public String TNROINTOPR { get; set; }
            public String CODIGOTALLER { get; set; }
            public String UDOMINIO { get; set; }
            public String UTIPUSO { get; set; }
            public String CILCODH { get; set; }
            public String CNROSERIE { get; set; }
            public String CCODGEST { get; set; }
            public String CFECGEST { get; set; }
            public String CCAPACIDAD { get; set; }
            public String CMESFABR { get; set; }
            public String CANOFABR { get; set; }
            public String CUPHCRPC { get; set; }
            public String CUPHMES { get; set; }
            public String CUPHANO { get; set; }
            public String FECVENCCRPC { get; set; }
            public String CUPHRESULT { get; set; }
            public String NROCERTIFICADO { get; set; }
            public String XFECMODREC { get; set; }
            public String XTIPOPRREC { get; set; }
        }

        public class VALCILTxtExtendedView : ViewEntity
        {
            public String PECCOD { get; set; }
            public String TALCUIT { get; set; }
            public String TNROINTOPR { get; set; }
            public String CODIGOTALLER { get; set; }
            public String VCDOMINIO { get; set; }
            public String TIPUSO { get; set; }
            public String VCCODVALVULA { get; set; }
            public String VCNROSERIE { get; set; }
            public String VCCODGEST { get; set; }
            public String VCFECGEST { get; set; }
            public String CILCODH { get; set; }
            public String CNROSERIE { get; set; }
            public String XTIPOPRREC { get; set; }
        }

        public class REGTxtExtendedView : ViewEntity
        {
            public String PECCOD { get; set; }
            public String TALCUIT { get; set; }
            public String TNROINTOPR { get; set; }
            public String CODIGOTALLER { get; set; }
            public String UDOMINIO { get; set; }
            public String TIPUSO { get; set; }
            public String RCODREG { get; set; }
            public String RNROSERIE { get; set; }
            public String RCODGEST { get; set; }
            public String RFECGEST { get; set; }
            public String XFECMODREC { get; set; }
            public String XTIPOPRREC { get; set; }
        }
    }
}