using PL.Fwk.Entities;
using System;
using System.Web.Http;
using TalleresWeb.Logic;

namespace PetroleraManager.WebApi.Controllers
{
    [Authorize]
    public class TalleresController : ApiController
    {
        #region Members
        private TalleresLogic talleresLogic;
        public TalleresLogic TalleresLogic
        {
            get
            {
                if (talleresLogic == null) talleresLogic = new TalleresLogic();
                return talleresLogic;
            }
        }
        #endregion

        #region Methods

        [ActionName("ReadTaller")]
        [HttpGet]
        public ViewEntity ReadTaller(Guid idTaller)
        {
            var taller = TalleresLogic.Read(idTaller);

            if (taller != null)
            {
                return new ViewEntity(taller.ID, taller.RazonSocialTaller);
            }

            return default(ViewEntity);
        }

        #endregion
    }
}
