using System;
using System.Web;
using System.Web.Security;
using TalleresWeb.Entities;
using TalleresWeb.Logic;



namespace PetroleraManager.Web.Account
{
    public partial class Login : System.Web.UI.Page
    {
        #region Members
        private UsuariosLogic usuariosLogic;
        #endregion

        #region Propiedades
        private UsuariosLogic UsuariosLogic
        {
            get
            {
                if (this.usuariosLogic == null) this.usuariosLogic = new UsuariosLogic();

                return this.usuariosLogic;
            }
        }
        #endregion

        #region Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                UsuariosParameters user = new UsuariosParameters();
                user.UserName = LoginUser.UserName;
                user.Password = LoginUser.Password;
                //TODO: up.Password = Seguridad.EncriptaContrasenia(LoginUser.Password);
                
                var userData = this.UsuariosLogic.Login(user);

                if (userData != null)
                {
                    String IDusrIDrol = userData.ID.ToString();// + "|" + userData.RolID;

                    FormsAuthenticationTicket tkt;
                    string cookiestr;
                    HttpCookie ck;
                    tkt = new FormsAuthenticationTicket(1,                                  //Version
                                                        userData.Descripcion.Trim(),        // user name
                                                        DateTime.Now,                       // creation
                                                        DateTime.Now.AddMinutes(500),       // Expiration
                                                        LoginUser.RememberMeSet,            // Persistent
                                                        userData.ID.ToString());            // User data

                    cookiestr = FormsAuthentication.Encrypt(tkt);
                    ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);

                    if (LoginUser.RememberMeSet)
                        ck.Expires = tkt.Expiration.AddYears(1);

                    ck.Path = FormsAuthentication.FormsCookiePath;
                    Response.Cookies.Add(ck);

                    string strRedirect;
                    strRedirect = Request["ReturnUrl"];

                    if (strRedirect == null)
                        strRedirect = "~/Default.aspx";

                    Response.Redirect(strRedirect, false);

                }
                else
                {
                    Response.Redirect("~/Account/login.aspx", false);
                }
            }
            catch (Exception ex)
            {
                msjError.Text = ex.Message;
            }
        }
        #endregion
    }
}
