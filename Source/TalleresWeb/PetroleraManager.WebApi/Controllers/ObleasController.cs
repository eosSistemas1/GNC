using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TalleresWeb.Entities;
using TalleresWeb.Logic;

namespace PetroleraManager.WebApi.Controllers
{
    [Authorize]
    public class ObleasController : ApiController
    {
        private ObleasLogic obleasLogic;
        public ObleasLogic ObleasLogic
        {
            get
            {
                if (obleasLogic == null) obleasLogic = new ObleasLogic();
                return obleasLogic;
            }
        }


        [ActionName("Read")]
        [HttpPost]
        public ObleasViewWebApi Read(ObleasParametersWebApi criteria)
        {
            try
            {
                return this.ObleasLogic.ReadForIngesoWebApi(criteria);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ActionName("ReadObleasConsulta")]
        [HttpGet]
        public List<ObleasConsultarView> ReadObleasConsulta(string fechaDesde, 
                                                            string fechaHasta, 
                                                            string dominio,
                                                            string numeroOblea, 
                                                            Guid tallerID)
        {
            try
            {
                DateTime fd = DateTime.Parse(fechaDesde);
                DateTime fh = DateTime.Parse(fechaHasta);
                return this.ObleasLogic.ReadObleasConsulta(fd, fh, dominio,numeroOblea,tallerID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ActionName("ReadObleaByID")]
        [HttpGet]
        public ObleasViewWebApi ReadByID(Guid idOblea)
        {
            try
            {
                return this.ObleasLogic.ReadByIDWebApi(idOblea);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ActionName("ReadTramitesByTallerID")]
        [HttpGet]
        public List<EstadosTramitesView> ReadTramitesByTallerID(Guid idTaller)
        {
            try
            {
                return this.ObleasLogic.ReadTramitesByTallerID(idTaller);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ActionName("Guardar")]
        [HttpPost]
        public ViewEntity Guardar(ObleasViewWebApi oblea)
        {
            try
            {
                ViewEntity o = this.ObleasLogic.SaveFromExtranet(oblea);
                return o;
            }
            catch (Exception ex)
            {
                return new ViewEntity(Guid.Empty, ex.InnerException.Message);
            }
        }

        [ActionName("GuardarImagen")]
        [HttpPost]
        public Boolean GuardarImagen(ImagenModel imagen)
        {
            if (imagen.ImageContent.Length > 0)
            {
                if (imagen.ImageContent != null)
                {
                    String name = String.Format("{0}.jpg", imagen.ImageName);

                    

                    var filePath = Path.Combine(HttpContext.Current.Server.MapPath("/UploadedFiles"), name);
                    //TODO: ver si anda : var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, $"UploadedFiles/{name}");
                    using (var imageFile = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.Write(imagen.ImageContent, 0, imagen.ImageContent.Length);
                        imageFile.Flush();
                    }
                }
                return true;
            }

            return false;
        }

        [ActionName("ExisteTramitePendienteParaElDominio")]
        [HttpGet]
        public Boolean ExisteTramitePendienteParaElDominio(String dominio)
        {
            try
            {
                return this.ObleasLogic.ExisteTramitePendienteParaElDominio(dominio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}