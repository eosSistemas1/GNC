using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PetroleraManager.Web.UserControls
{
    public partial class PrintBoxCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PrintBox(String Titulo, String reportURL)
        {
            this.PrintBox(Titulo, reportURL, String.Empty, null);
        }

        public void PrintBox(String Titulo, String reportURL, String reportContent)
        {
            this.PrintBox(Titulo, reportURL, reportContent, null);
        }

        public void PrintBox(String Titulo, String reportURL, String reportContent, String ResponseUrl)
        {
            if (!String.IsNullOrEmpty(Titulo)) lblTituloMsj.Text = Titulo;

            if (!String.IsNullOrEmpty(reportURL))
            {
                frmReporte.Attributes.Add("src", reportURL);
            }
            else if (!String.IsNullOrEmpty(reportContent))
            {
                frmReporte.InnerHtml = reportContent;
            }
            else
            {
                frmReporte.Visible = false;
                imgAccesoDenegado.Visible = true;
            }


            if (String.IsNullOrEmpty(ResponseUrl))
            {
                MPE.OnCancelScript = "return false;";
            }
            else
            {
                MPE.OnCancelScript = "window.location.href='" + ResponseUrl + "';";
            }


            MPE.Show();
        }

       
    }
}