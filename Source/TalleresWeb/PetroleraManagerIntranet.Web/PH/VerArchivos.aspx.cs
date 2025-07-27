using CrossCutting.DatosDiscretos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Web.Cross;
using TalleresWeb.Web.Cross.Configuracion;

namespace PetroleraManagerIntranet.Web.PH
{
    public partial class VerArchivos : PageBase
    {
        public string Url
        {
            get
            {
                return MapPath("~/Archivos/PH/").ToString();
            }
        }
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
                response.AddHeader("Content-Disposition", "attachment; filename=" + filename.Replace(Url, string.Empty) + ";");
                response.TransmitFile(filename);
                response.Flush();
                response.End();


            }
            catch (Exception ex)
            {
                MessageBoxCtrl.MessageBox("", ex.Message, Web.UserControls.MessageBoxCtrl.TipoWarning.Error);
            }
        }

        protected void btnBuscar_ServerClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(calFecha.Value))
            {
                List<FilesInformePH> files = new List<FilesInformePH>();

                try
                {

                    var fecha = DateTime.Parse(calFecha.Value).ToString("dd_MM_yyyy");

                    var strFiles = System.IO.Directory.GetFiles(Url).ToList().Where(x => x.Contains(fecha) && x.EndsWith(".txt"));

                    foreach (var file in strFiles)
                    {
                        FilesInformePH filesInformePH = new FilesInformePH()
                        {
                            FileName = file.Replace(Url, string.Empty),
                            FilePath = file
                        };

                        files.Add(filesInformePH);
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
                MessageBoxCtrl.MessageBox("", "Debe ingresar una fecha", Web.UserControls.MessageBoxCtrl.TipoWarning.Warning);
            }
        }
    }
}