using CrossCutting.DatosDiscretos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;

namespace PetroleraManager.Web.Tramites
{
    public partial class VerArchivos : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                calFecha.Value = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        protected void grdArchivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                String filename = e.CommandArgument.ToString();

                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "application/octet-stream";
                response.AddHeader("Content-Disposition", "attachment; filename=" + filename + ";");
                response.TransmitFile(filename);
                response.Flush();
                response.End();


            }
            catch (Exception ex)
            {
                MessageBoxCtrl.MessageBox("", ex.Message, UserControls.MessageBoxCtrl.TipoWarning.Error);
            }
        }

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(calFecha.Value))
            {
                List<FilesExtendedView> files = new List<FilesExtendedView>();

                try
                {
                   
                    String url = this.GenerarPathArchivos(DateTime.Parse(calFecha.Value));

                    var strFiles = System.IO.Directory.GetDirectories(url);
                    Array.Sort(strFiles);
                    for (int i = 0; i < strFiles.Length; i++)
                    {
                        var parse = strFiles[i].Replace(@"\", @"*").Split('*');
                        FilesExtendedView fila = new FilesExtendedView();
                        fila.NumeroInforme = long.Parse(parse[5]);
                        fila.FechaHoraInforme = new DateTime(int.Parse(parse[2]), int.Parse(parse[3]), int.Parse(parse[4]));

                        String descripcionUSR = System.IO.Directory.GetFiles(strFiles[i] + @"\emitidos\").Where(x => x.Contains(@"\USR") && x.Contains(".txt")).Single();
                        String descripcionREG = System.IO.Directory.GetFiles(strFiles[i] + @"\emitidos\").Where(x => x.Contains(@"\REG") && x.Contains(".txt")).Single();
                        String descripcionCIL = System.IO.Directory.GetFiles(strFiles[i] + @"\emitidos\").Where(x => x.Contains(@"\CIL") && x.Contains(".txt")).Single();
                        String descripcionVAL = System.IO.Directory.GetFiles(strFiles[i] + @"\emitidos\").Where(x => x.Contains(@"\VAL") && x.Contains(".txt")).Single();


                        fila.descripcionUSR = descripcionUSR.Replace(@"\", @"*").Split('*')[7];
                        fila.descripcionREG = descripcionREG.Replace(@"\", @"*").Split('*')[7];
                        fila.descripcionCIL = descripcionCIL.Replace(@"\", @"*").Split('*')[7];
                        fila.descripcionVAL = descripcionVAL.Replace(@"\", @"*").Split('*')[7];

                        fila.urlUSR = descripcionUSR;
                        fila.urlREG = descripcionREG;
                        fila.urlCIL = descripcionCIL;
                        fila.urlVAL = descripcionVAL;

                        files.Add(fila);
                    }
                }
                catch
                {                  
                }

                grdArchivos.DataSource = files;
                grdArchivos.DataBind();
            }
            else
            {
                MessageBoxCtrl.MessageBox("", "Debe ingresar una fecha", UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        private string GenerarPathArchivos(DateTime fecha)
        {
            String dia = fecha.Date.Day.ToString("00");
            String mes = fecha.Month.ToString("00");
            String anio = fecha.Year.ToString("0000");

            String path = String.Format("{0}{1}\\{2}\\{3}", GetDinamyc.UrlArchivosEnte
                                                                         , anio
                                                                         , mes
                                                                         , dia);

            return path;
        }
    }
}