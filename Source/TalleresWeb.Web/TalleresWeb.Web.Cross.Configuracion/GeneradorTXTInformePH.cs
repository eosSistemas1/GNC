using Ionic.Zip;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace TalleresWeb.Web.Cross.Configuracion
{
    public class GeneradorTXTInformePH
    {
        private const String extension = ".txt";
        public static String Separador = ";";
        public static String LineaNueva = "\n";

        public static void GenerarREVTxt(List<REVTxtExtendedView> datos, String dirPath)
        {
            //directorio
            DirectoryInfo DIR = new DirectoryInfo(dirPath);

            if (!DIR.Exists)
            {
                DIR.Create();
            }

            //nombre del archivo
            String fic = @dirPath + "\\REV_" + DateTime.Now.ToString("dd_MM_yyyy_hhmm") + extension;

            FileStream stream = File.Open(fic, FileMode.Create);
            StreamWriter filaSB = new StreamWriter(stream);

            foreach (REVTxtExtendedView item in datos)
            {
                filaSB.WriteLine(item.RCRPCOD + Separador +
                                item.RCRPCRT + Separador +
                                item.RPECCOD + Separador +
                                item.RTALCOD + Separador +
                                item.RTALCUIT + Separador +
                                item.RPROAPYN + Separador +
                                item.RPROTDOC + Separador +
                                item.RPRONDOC + Separador +
                                item.RPRODMCL + Separador +
                                item.RPROLCLD + Separador +
                                item.RPROPRVN + Separador +
                                item.RPROCDPT + Separador +
                                item.RPROTLFN + Separador +
                                item.RPRODMNO + Separador +
                                item.RCILCODH + Separador +
                                item.RCILNSER + Separador +
                                item.RCILMESF + Separador +
                                item.RCILANOF + Separador +
                                item.RCILMTRL + Separador +
                                item.RCILCPCD + Separador +
                                item.RCILREV + Separador +
                                item.RREVRSLT + Separador +
                                item.RREVGLOB + Separador +
                                item.RREVABO1 + Separador +
                                item.RREVABOE + Separador +
                                item.RREVFISU + Separador +
                                item.RREVLAMI + Separador +
                                item.RREVPINC + Separador +
                                item.RREVDEFR + Separador +
                                item.RREVDESL + Separador +
                                item.RREVCOR + Separador +
                                item.RREVOVAL + Separador +
                                item.RREVFDME + Separador +
                                item.RREVEVSA + Separador +
                                item.RREVPERM + Separador +
                                item.RREVDPFC + Separador +
                                item.RREVOTR1 + Separador +
                                item.RREVOTR2 + Separador +
                                item.RREVFREV + Separador +
                                item.RREVFVEN + Separador +
                                item.NROCERTIFICADO + Separador +
                                item.XRECTIPOPR + Separador +
                                item.XRECFECMODE + Separador +
                                item.XRECFECTRF);
            }

            filaSB.Close();

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(fic, "");
                zip.Save(fic.Replace(extension, ".zip"));
            }
        }
    }

    public class REVTxtExtendedView : ViewEntity
    {
        public String RCRPCOD { get; set; }
        public String RCRPCRT { get; set; }
        public String RPECCOD { get; set; }
        public String RTALCOD { get; set; }
        public String RTALCUIT { get; set; }
        public String RPROAPYN { get; set; }
        public String RPROTDOC { get; set; }
        public String RPRONDOC { get; set; }
        public String RPRODMCL { get; set; }
        public String RPROLCLD { get; set; }
        public String RPROPRVN { get; set; }
        public String RPROCDPT { get; set; }
        public String RPROTLFN { get; set; }
        public String RPRODMNO { get; set; }
        public String RCILCODH { get; set; }
        public String RCILNSER { get; set; }
        public String RCILMESF { get; set; }
        public String RCILANOF { get; set; }
        public String RCILMTRL { get; set; }
        public String RCILCPCD { get; set; }
        public String RCILREV { get; set; }
        public String RREVRSLT { get; set; }
        public String RREVGLOB { get; set; }
        public String RREVABO1 { get; set; }
        public String RREVABOE { get; set; }
        public String RREVFISU { get; set; }
        public String RREVLAMI { get; set; }
        public String RREVPINC { get; set; }
        public String RREVDEFR { get; set; }
        public String RREVDESL { get; set; }
        public String RREVCOR { get; set; }
        public String RREVOVAL { get; set; }
        public String RREVFDME { get; set; }
        public String RREVEVSA { get; set; }
        public String RREVPERM { get; set; }
        public String RREVDPFC { get; set; }
        public String RREVOTR1 { get; set; }
        public String RREVOTR2 { get; set; }
        public String RREVFREV { get; set; }
        public String RREVFVEN { get; set; }
        public String NROCERTIFICADO { get; set; }
        public String XRECTIPOPR { get; set; }
        public String XRECFECMODE { get; set; }
        public String XRECFECTRF { get; set; }
    }
}
