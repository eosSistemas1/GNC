using TalleresWeb.Web.Logic;
using TalleresWeb.Web.UI.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;

namespace TalleresWeb.Web.UI.Account
{
    public partial class ChangePassword : PageBase
    {
        #region Members

        private UsuarioLogic _usuarioLogic;

        #endregion

        #region Methods

        protected void Page_Load(Object sender, EventArgs e)
        {
            this._usuarioLogic = new UsuarioLogic();            
        }

        protected void btnAceptar_Click(Object sender, EventArgs e)
        {
            String msjValidar = String.Empty;
            if (this.NewPassword.Text.Length < 5) 
                msjValidar += "- La contraseña nueva debe tener al menos 5 caracteres.";           
            if (msjValidar == String.Empty)
            {
                Usuario userData = this._usuarioLogic.ReadByUserName(Context.User.Identity.Name);

                if (userData.Contrasenia != CurrentPassword.Text.Trim())
                {
                    MessageBoxCtrl1.MessageBox(null, "- La contraseña actual no coincide con la ingresada", MessageBoxCtrl.TipoWarning.Warning);
                    return;
                }

                userData.Contrasenia = this.NewPassword.Text.Trim();
                //userData.EsPrimerIngreso = false;
                if(!this._usuarioLogic.Update(userData))
                {
                    MessageBoxCtrl1.MessageBox(null, "- Se ha producido un error al cambiar la contraseña. Vuelva a intentarlo.", MessageBoxCtrl.TipoWarning.Warning);
                    return;
                }

                if (userData != null)
                {
                    String IDusrIDrol = userData.ID.ToString() + "|" + userData.IdRol;

                    FormsAuthenticationTicket tkt;
                    String cookiestr;
                    HttpCookie ck;
                    tkt = new FormsAuthenticationTicket(1,                              //Version
                                                        userData.Descripcion.Trim(),    // user name
                                                        DateTime.Now,                   // creation
                                                        DateTime.Now.AddMinutes(180),   // Expiration
                                                        false,                          // Persistent
                                                        userData.ID.ToString());        // User data
                    cookiestr = FormsAuthentication.Encrypt(tkt);
                    ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                    ck.Path = FormsAuthentication.FormsCookiePath;
                    Response.Cookies.Add(ck);

                    Session["USERID"] = userData.ID;
                }

                String strRedirect = "~/Default.aspx";
                Response.Redirect(strRedirect, false);
            }
            else
            {
                MessageBoxCtrl1.MessageBox(null, msjValidar, MessageBoxCtrl.TipoWarning.Warning);
            }
        }

        protected void CancelPushButton_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        #endregion

       

    }
}