using TalleresWeb.Entities;
using TalleresWeb.Logic;
using System.Web.Http;
using System;

namespace PetroleraManager.WebApi.Controllers
{
    public class UsuarioController : ApiController
    {
        #region Members

        private UsuariosLogic _usuarioLogic = new UsuariosLogic();

        #endregion

        #region Methods

        [ActionName("Login")]
        [HttpPost]
        public UsuarioBasicView Login(UsuariosParameters up)
        {
            UsuarioBasicView u = this._usuarioLogic.LoginExterno(up);
            return u;
        }

        [ActionName("ReadByUserName")]
        [HttpGet]
        public Usuario ReadByUserName(String userName)
        {
            throw new NotImplementedException();
            //return this._usuarioLogic.ReadByUserName(userName);
        }

        [ActionName("Update")]
        [HttpPost]
        public void Update(Usuario user)
        {
            this._usuarioLogic.Update(user);
        }

        #endregion

    }
}
