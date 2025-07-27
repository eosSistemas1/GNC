using TalleresWeb.Web.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using PL.Fwk.Entities;

namespace TalleresWeb.Web.UI.Account
{
    public partial class Login : System.Web.UI.Page
    {
        #region Members

        private UsuarioLogic _usuarioLogic;

        #endregion

        #region Methods

        protected void Page_Load(Object sender, EventArgs e)
        {
            this._usuarioLogic = new UsuarioLogic();
        }

        protected void btnLogin_Click(Object sender, EventArgs e)
        {
            try
            {
                UsuariosParameters up = new UsuariosParameters();
                up.UserName = UserName.Text;
                up.Password = Password.Text;

                var userData = this._usuarioLogic.Login(up);

                if (userData == null)
                {
                    this.MessageBox(null, "Inicio de Sesión incorrecto, vuelva a intentarlo.", TalleresWeb.Web.UI.UserControls.MessageBoxCtrl.TipoWarning.Error);
                    return;
                }

                if (userData.IDTaller == null || userData.IDTaller == Guid.Empty)
                {
                    this.MessageBox(null, "El usuario ingresado no pertenece a un taller de nuestra red.", TalleresWeb.Web.UI.UserControls.MessageBoxCtrl.TipoWarning.Error);
                    return;
                }

                try
                {
                    String usuario = String.Format("{0}|{1}|{2}|{3}|{4}|{5}", this.UserName.Text,
                                                                          userData.ID,
                                                                          userData.Descripcion,
                                                                          userData.IDTaller,
                                                                          userData.RazonSocialTaller,
                                                                          userData.MatriculaTaller);
                    FormsAuthentication.SetAuthCookie(usuario, RememberMe.Checked);

                    FormsAuthenticationTicket ticket1 =
                       new FormsAuthenticationTicket(
                            1,                                   // version
                            this.UserName.Text.Trim(),   // get username  from the form
                            DateTime.Now,                        // issue time is now
                            DateTime.Now.AddMinutes(300),         // expires in 10 minutes
                            RememberMe.Checked,                                              // cookie is not persistent
                            usuario                             // role assignment is stored
                                                                // in userData
                            );

                    HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket1));

                    Response.Cookies.Add(cookie1);

                    // 4. Do the redirect. 
                    String returnUrl1;
                    
                    // the login is successful
                    if (Request.QueryString["ReturnUrl"] == null)
                    {
                        returnUrl1 = "~/Default.aspx";
                    }
                    //login not unsuccessful 
                    else
                    {
                        returnUrl1 = Request.QueryString["ReturnUrl"];
                    }

                    Response.Redirect(returnUrl1, false);
                }

                catch (Exception ex)
                {
                    this.MessageBox(null, ex.Message, UserControls.MessageBoxCtrl.TipoWarning.Error);
                }
            }
            catch (Exception ex)
            {
                this.MessageBox(null, ex.InnerException.Message, TalleresWeb.Web.UI.UserControls.MessageBoxCtrl.TipoWarning.Error);
            }
        }

        /// <summary>
        /// muestra un msj box
        /// </summary>
        /// <param name="Titulo">Titulo del mensaje (String.Empty no muestra titulo)</param>
        /// <param name="Mensaje">Mensaje (String.Empty no muestra mensaje)</param>
        /// <param name="TipoMensaje">Info, Warning, Error, Success</param>
        public Boolean MessageBox(String Titulo, String Mensaje, TalleresWeb.Web.UI.UserControls.MessageBoxCtrl.TipoWarning TipoMensaje)
        {
            this.MessageBoxCtrl.MessageBox(Titulo, Mensaje, TipoMensaje);
            return TipoMensaje == TalleresWeb.Web.UI.UserControls.MessageBoxCtrl.TipoWarning.Success;
        }

        #endregion
    }
}