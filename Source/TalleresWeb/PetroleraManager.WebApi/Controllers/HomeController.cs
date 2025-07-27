using System;
using System.Linq;
using System.Web.Http;
using TalleresWeb.Logic;

namespace PetroleraManager.WebApi.Controllers
{
    public class HomeController : ApiController
    {
        #region Members


        private ProvinciasLogic provinciasLogic;

        public ProvinciasLogic ProvinciasLogic
        {
            get
            {
                if (provinciasLogic == null) provinciasLogic = new ProvinciasLogic();
                return provinciasLogic;
            }
        }

        #endregion

        #region Methods

        // GET api/home
        public String Get()
        {
            try
            {
                var a = ProvinciasLogic.ReadListView();                
                return "Serice: OK - SQL : OK" ;
            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion
    }
}
