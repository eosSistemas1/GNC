using TalleresWeb.Logic;
using PL.Fwk.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using TalleresWeb.Entities;


namespace PetroleraManager.WebApi.Controllers
{
    //[Authorize]
    public class LocalidadesController : ApiController
    {

        #region Members


        private LocalidadesLogic localidadesLogic;

        public LocalidadesLogic LocalidadesLogic
        {
            get
            {
                if (this.localidadesLogic == null) this.localidadesLogic = new LocalidadesLogic();
                return localidadesLogic;
            }
        }

        #endregion

        #region Methods

        [ActionName("ReadListLocalidades")]
        [HttpGet]
        public List<ViewEntity> ReadListLocalidades()
        {
            var lista = this.LocalidadesLogic.ReadListView().OrderBy(l => l.Descripcion);
            return lista.ToList();
        }

        [ActionName("ReadLocalidadByID")]
        [HttpGet]
        public LocalidadesExtendedView ReadLocalidadByID(Guid localidadID)
        {
            var localidad = this.LocalidadesLogic.Read(localidadID);
            return new LocalidadesExtendedView()
            {
                ID = localidad.ID,
                Descripcion = localidad.Descripcion,
                CP = localidad.CodigoPostal,
                Provincia = localidad.Provincias.Descripcion
            };
        }
        
        #endregion

    }
}