using PL.Fwk.Entities;
using System;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using TalleresWeb.Entities;
using TalleresWeb.Web.Logic;

namespace TalleresWeb.Web.UI
{
    public partial class Foto : PageBase
    {
        #region Propiedades
        private ObleasLogic obleasLogic;
        public ObleasLogic ObleasLogic
        {
            get
            {
                if (this.obleasLogic == null) this.obleasLogic = new ObleasLogic();
                return this.obleasLogic;
            }            
        }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["idTipo"] != null) this.TipoFoto = Request.QueryString["idTipo"].ToString();

            if (!this.IsPostBack)
            {
                if (Request.InputStream.Length > 0)
                {                    
                    using (StreamReader reader = new StreamReader(Request.InputStream))
                    {
                        String hexString = Server.UrlEncode(reader.ReadToEnd());
                        String imageName = this.GetImageName();
                        String imagePath = string.Format("~/Captures/{0}.png", imageName);

                        var newFullPath = Server.MapPath(imagePath);                                              

                        this.ObleasLogic.GuardarImagen(new ImagenModel()
                        {
                            ImageContent = ConvertHexToBytes(hexString),
                            ImageName = imageName
                        });

                        //if (File.Exists(newFullPath)) File.Delete(newFullPath);

                        File.WriteAllBytes(newFullPath, ConvertHexToBytes(hexString));
                        Session["CapturedImage"] = ResolveUrl(imagePath);
                    }
                }
            }
        }

        private String GetImageName()
        {
            String imageName = String.Empty;
            
            //String fecha = DateTime.Now.ToString("dd-MM-yyyy");
            String tallerID = ((ViewEntity)HttpContext.Current.Session["TALLERID"]).ID.ToString();
            String dominio = Session["DOMINIOFOTO"].ToString().ToUpper();           

            return String.Format("{0}_{1}_{2}", tallerID, dominio, this.TipoFoto);
        }

        private static byte[] ConvertHexToBytes(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        [WebMethod(EnableSession = true)]
        public static string GetCapturedImage()
        {
            try
            {
                string url = HttpContext.Current.Session["CapturedImage"].ToString();
                HttpContext.Current.Session["CapturedImage"] = null;
                return url;
            }
            catch (Exception e)
            {
                return e.Message;
            }
            
        }
        #endregion
    }
}