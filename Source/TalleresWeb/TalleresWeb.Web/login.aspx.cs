using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using TalleresWeb.Logic;

namespace TalleresWeb.Web
{
    public partial class login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    Response.Redirect("TalleresWeb/default.aspx");
                }
                else
                {
                    //Utility.AgregaEnter(ref txtPass, ref btnIngresar);
                }
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            String userData = ValidarUsuario(cboTipoDoc.SelectedValue.ToString(),
                                             txtNroDocumento.Text,
                                             txtUsuario.Text,
                                             txtPass.Text);

            if (userData != String.Empty)
            {
                FormsAuthenticationTicket tkt;
                string cookiestr;
                HttpCookie ck;
                tkt = new FormsAuthenticationTicket(1,                              //Version
                                                    txtUsuario.Text.Trim(),         // user name
                                                    DateTime.Now,                   // creation
                                                    DateTime.Now.AddMinutes(300),    // Expiration
                                                    chkPersistCookie.Checked,       // Persistent
                                                    userData);                      // User data
                cookiestr = FormsAuthentication.Encrypt(tkt);
                ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                if (chkPersistCookie.Checked)
                    ck.Expires = tkt.Expiration;
                ck.Path = FormsAuthentication.FormsCookiePath;
                Response.Cookies.Add(ck);


                string strRedirect;
                strRedirect = Request["ReturnUrl"];
                if (strRedirect == null)
                    strRedirect = "TalleresWeb/default.aspx";
                Response.Redirect(strRedirect, false);

            }
            else
            {
                Response.Redirect("~/Account/login.aspx", true);
            }
        }

        private String ValidarUsuario(String idTipoDoc, String nroDoc, String usuario, String pass)
        {
            String valor = String.Empty;

            String encryptedPass = Genericos.EncriptaContrasenia(pass.Trim());
            UsuariosLogic logic = new UsuariosLogic();
            var usr = logic.LoadAllLogin(usuario, encryptedPass, new Guid(idTipoDoc), nroDoc);

            if (usr != null)
            {
                //if (ValidarHora(usr.IdRol))
               // {
                    valor = usr.IdTaller.ToString();
                //}
                //else
                //{
                //    lblError.Text = "El usuario ingresado esta fuera del horario permitido.";
                //}
            }

            return valor;
        }

        private bool ValidarHora(int idRol)
        {
            Boolean valor = false;
            String xHora = DateTime.Now.Hour.ToString();
            String xMinuto = DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();

            int hora = int.Parse(xHora + xMinuto);

            S_RolesLogic roles = new S_RolesLogic();

            var rol = roles.ReadByIdRol(idRol);

            if (rol != null)
            {
                if (rol.ID != CrossCutting.DatosDiscretos.ROL.Administrador)
                {
                    DayOfWeek dia = DateTime.Now.DayOfWeek;
                    switch (dia)
                    {
                        case DayOfWeek.Saturday:
                            // si es sabado ver horarios de sabado
                            if (hora > rol.HoraInicioSabado && hora < rol.HoraFinSabado)
                            {
                                valor = true;
                            }
                            break;
                        case DayOfWeek.Sunday:
                            // si es domingo devuelvo false
                            break;
                        default:
                            //si es cualquier dia busco el horario 
                            if ((hora > rol.HoraInicioSemanaM && hora < rol.HoraFinSemanaM) ||
                                (hora > rol.HoraInicioSemanaT && hora < rol.HoraFinSemanaT))
                            {
                                valor = true;
                            }
                            break;
                    }
                }
                else
                {
                    //si es administrador puede entrar cualquier dia, en cualquier hora, incluso Domingos.
                    valor = true;
                }

            }

            return valor;
        }
    }
}