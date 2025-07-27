using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using TalleresWeb.Entities;
using TalleresWeb.Web.Logic;

namespace TalleresWeb.Web.UI
{

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        #region Properties
        private ReguladoresLogic reguladoresLogic;
        private ReguladoresLogic ReguladoresLogic
        {
            get
            {
                if (this.reguladoresLogic == null) this.reguladoresLogic = new ReguladoresLogic();
                return this.reguladoresLogic;
            }
        }

        private CilindrosLogic cilindrosLogic;
        private CilindrosLogic CilindrosLogic
        {
            get
            {
                if (this.cilindrosLogic == null) this.cilindrosLogic = new CilindrosLogic();
                return this.cilindrosLogic;
            }
        }

        private ValvulasLogic valvulasLogic;
        private ValvulasLogic ValvulasLogic
        {
            get
            {
                if (this.valvulasLogic == null) this.valvulasLogic = new ValvulasLogic();
                return this.valvulasLogic;
            }
        }

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

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetReguladoresCodHomAutoCompleteData(string txt)
        {
            List<string> result = this.ReguladoresLogic.ReadListCodigosHomologacion(txt);                        
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetCilindrosCodHomAutoCompleteData(string txt)
        {     
            List<string> result = this.CilindrosLogic.ReadListCodigosHomologacion(txt);
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetValvulasCodHomAutoCompleteData(string txt)
        {
            List<string> result = this.ValvulasLogic.ReadListCodigosHomologacion(txt);
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public VehiculosView GetVehiculoByDominio(string dominio)
        {
            VehiculosView result = new VehiculosLogic().ReadByDominio(dominio);
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ClientesView GetClienteByDocumento(string tipoDocumento, string numeroDocumento)
        {
            ClientesView result = new ClientesLogic().ReadByDocumento(new Guid(tipoDocumento), numeroDocumento);
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public LocalidadesExtendedView GetLocalidadByID(string localidadID)
        {
            LocalidadesExtendedView result = new LocalidadesLogic().ReadLocalidadByID(new Guid(localidadID));
            return result;
        }        

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String GetCilindroByCodigoHomologacion(string txt)

        {
            return this.CilindrosLogic.ReadCilindroByCodigoHomologacion(txt);            
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void UploadFile(string imagen, string tipoFoto, string dominio, string tallerID)
        {
            imagen = imagen.Replace("\"", String.Empty);
            imagen = imagen.Replace("data:image/jpeg;base64,", String.Empty);            
            String nombreImagen = this.GetNombreImagen(dominio, tipoFoto, tallerID);
            
            String imagePath = string.Format("~/Captures/{0}.png", nombreImagen);

            var newFullPath = Server.MapPath(imagePath);
            Byte[] imagenBytes = Convert.FromBase64String(imagen);

            this.ObleasLogic.GuardarImagen(new ImagenModel()
            {
                ImageContent = imagenBytes,
                ImageName = nombreImagen
            });

            //if (File.Exists(newFullPath)) File.Delete(newFullPath);

            using (FileStream fs = new FileStream(newFullPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    //byte[] data = Convert.FromBase64String(imagen);
                    bw.Write(imagenBytes);
                    bw.Close();
                }
            }
            //File.WriteAllText(newFullPath, imagen);           
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetCP(string idLocalidad)
        {
            Guid id = new Guid(idLocalidad);
            var result = new LocalidadesLogic().ReadLocalidadByID(id);
            return result != null ? result.CP.Trim() : String.Empty;
        }

        private String GetNombreImagen(string dominio, string tipoFoto, string tallerID)
        {
            String imageName = String.Empty;
            return String.Format("{0}_{1}_{2}", tallerID, dominio, tipoFoto);
        }     
    }   
}
