using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using TalleresWeb.Web;
using TalleresWeb.Logic;
using TalleresWeb.Entities;

namespace TalleresWeb.Web
{
    public partial class MasterTalleres : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                MostrarUsuario(Context.User.Identity.Name);
                AmarMenuPrincipal(Context.User.Identity.Name);
                pMicuenta.Visible = true;
            }
            else
            { pMicuenta.Visible = false; }
        }

        public void MostrarUsuario(String usuario)
        {
            if (usuario != String.Empty)
            {
                NombreUsuario.Text = usuario.ToLower();
                pMicuenta.Visible = true;
            }
            else
            {
                pMicuenta.Visible = false;

            }
        }

        public void AmarMenuPrincipal(String usuario)
        {
            //si esta logueado
            if (Request.IsAuthenticated)
            {
                //armo el menu y lo muestro
                Accordion1.Visible = true;
                #region Armar Menu con acordion
                if (usuario.Trim() != String.Empty)
                {
                    UsuariosLogic usuarioLogic = new UsuariosLogic();
                    var objUsuario = usuarioLogic.ReadByNombre(Context.User.Identity.Name);

                    int id = 0;

                    S_AccesosCUParameters paramPadres = new S_AccesosCUParameters();
                    paramPadres.UsuarioDsc = objUsuario.Descripcion;
                    paramPadres.IdRol = objUsuario.IdRol;
                    paramPadres.IdPadre = 0;

                    S_AccesosCULogic cuLogic = new S_AccesosCULogic();
                    var itemsPadres = cuLogic.ReadPadresByRol(paramPadres);

                    foreach (S_AccesosCUExtendedView padre in itemsPadres)
                    {
                        AjaxControlToolkit.AccordionPane pane = new AjaxControlToolkit.AccordionPane();
                        pane.ID = "pane_" + id;

                        Image i = new Image();
                        i.ImageUrl = URLBase + "Images/" + padre.UrlImagen;
                        pane.Controls[0].Controls.Add(i);

                        Label h = new Label();
                        h.Attributes.Add("onmouseover", "this.style.cursor='hand';");
                        h.Text = "  " + padre.Descripcion.ToUpper();
                        pane.Controls[0].Controls.Add(h);

                        #region Hijos
                        S_AccesosCUParameters paramHijos = new S_AccesosCUParameters();
                        paramHijos.UsuarioDsc = objUsuario.Descripcion;
                        paramHijos.IdRol = objUsuario.IdRol;
                        paramHijos.IdPadre = padre.IdCU;

                        var itemsHijos = cuLogic.ReadPadresByRol(paramHijos);

                        Table tb = new Table();
                        int id0 = 0;

                        foreach (S_AccesosCUExtendedView hijo in itemsHijos) 
                        {
                            TableRow fila = new TableRow();
                            TableCell cell;
                            TableCell cellespacio;

                            HyperLink lnk = new HyperLink();
                            lnk.Text = "\t" + hijo.Descripcion;

                            String url = URLBase + hijo.Url;

                            if (hijo.AbrirEnVentanaNueva)
                            {
                                if (hijo.Url.IndexOf("http://") > -1) url = hijo.Url;
                                lnk.Target = "_blank";
                            }

                            lnk.NavigateUrl = url;
                            lnk.ID = "lnk" + id + "_" + id0;

                            cellespacio = new TableCell();
                            cellespacio.Width = Unit.Pixel(20);

                            cell = new TableCell();
                            cell.VerticalAlign = VerticalAlign.Middle;
                            cell.HorizontalAlign = HorizontalAlign.Left;
                            cell.Controls.Add(lnk);

                            fila.Cells.Add(cellespacio);
                            fila.Cells.Add(cell);
                            tb.Controls.Add(fila);

                            id0++;
                            //TreeNode child = new TreeNode();
                            //child.Text = "\t" + objHijos.s_DescCU;
                            //child.ImageUrl = URLBase + "Images/" + objHijos.s_UrlImagen;
                            //child.NavigateUrl = URLBase + objHijos.s_Url;
                            //child.Target = "_self";
                            //if (objHijos.s_Url.IndexOf("http://") > -1) child.Target = "_blank";
                            //node.ChildNodes.Add(child);
                            //child.CollapseAll();
                        }

                        if (itemsHijos.Count > 0) pane.Controls[1].Controls.Add(tb);

                        #endregion

                        Accordion1.Panes.Add(pane);
                        id++;
                    }
                }
                #endregion
            }
            else
            {
                //oculto el menu
                Accordion1.Visible = false;
            }

        }

        public static string URLBase
        {
            get
            {
                HttpContext context = HttpContext.Current;
                string baseUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd('/') + '/';
                return baseUrl;
            }
        }

        public static Guid IdTaller
        {
            get
            {
                HttpContext context = HttpContext.Current;
                FormsIdentity id =
                            (FormsIdentity)context.User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;

                Guid userData = new Guid(ticket.UserData);
                return userData;
            }
        }

        public static String Usuario
        {
            get
            {
                HttpContext context = HttpContext.Current;
                return context.User.Identity.Name;
            }
        }

        public static Guid IdUsuarioLogueado
        {
            get
            {
                String nombre = Usuario;

                UsuariosLogic logic = new UsuariosLogic();
                return logic.ReadByNombre(nombre).ID;
            }
        }
    }
}