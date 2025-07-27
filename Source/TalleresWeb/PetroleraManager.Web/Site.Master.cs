using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PL.Fwk.Presentation.Web.Controls;
using System.Web.UI.HtmlControls;
using TalleresWeb.Logic;

namespace PetroleraManager.Web
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                AddEventHandlers();
                // AddPageContent();
                imgHeader.ImageUrl = SiteMaster.UrlBase + CrossCutting.DatosDiscretos.GetDinamyc.LogoEmpresa;
            }
        }

        // menu

        private void AddPageContent()
        {
            string pageName = HttpContext.Current.Request.Url.AbsolutePath.Substring(
                         HttpContext.Current.Request.Url.AbsolutePath.LastIndexOf("/") + 1);

            ContentPlaceHolder contentPlaceHolder = (ContentPlaceHolder)this.Page.Master.FindControl("MainContent");
            Label label = new Label();
            label.Text = " <br /> Content for page: " + pageName;
            contentPlaceHolder.Controls.Add(label);
        }

        private void AddEventHandlers()
        {
            NavigationMenu.MenuItemDataBound += new MenuEventHandler(NavigationMenu_MenuItemDataBound);
            SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);
            SiteMapPath1.ItemCreated += new SiteMapNodeItemEventHandler(SiteMapPath1_ItemCreated);
        }

        void NavigationMenu_MenuItemDataBound(object sender, MenuEventArgs e)
        {
            SiteMapNode node = (SiteMapNode)e.Item.DataItem;

            //set the target of the navigation menu item (blank, self, etc...)
            if (node["target"] != null)
            {
                e.Item.Target = node["target"];
            }
            //create access key button
            if (node["accesskey"] != null)
            {
                CreateAccessKeyButton(node["accesskey"] as string, node.Url);
            }
        }

        SiteMapNode SiteMap_SiteMapResolve(object sender, SiteMapResolveEventArgs e)
        {
            if (SiteMap.CurrentNode != null)
            {
                SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
                SiteMapNode tempNode = currentNode;
                tempNode = ReplaceNodeText(tempNode);

                return currentNode;
            }

            return null;
        }

        protected void SiteMapPath1_ItemCreated(object sender, SiteMapNodeItemEventArgs e)
        {
            if (e.Item.ItemType == SiteMapNodeItemType.Root)
            {
                SiteMapPath1.PathSeparator = " ";
            }
            else
            {
                SiteMapPath1.PathSeparator = " >> ";
            }
        }

        void CreateAccessKeyButton(string ak, string url)
        {
            HtmlButton inputBtn = new HtmlButton();
            inputBtn.Style.Add("width", "1px");
            inputBtn.Style.Add("height", "1px");
            inputBtn.Style.Add("position", "absolute");
            inputBtn.Style.Add("left", "-2555px");
            inputBtn.Style.Add("z-index", "-1");
            inputBtn.Attributes.Add("type", "button");
            inputBtn.Attributes.Add("value", "");
            inputBtn.Attributes.Add("accesskey", ak);
            inputBtn.Attributes.Add("onclick", "navigateTo('" + url + "');");

            AccessKeyPanel.Controls.Add(inputBtn);
        }

        internal SiteMapNode ReplaceNodeText(SiteMapNode smn)
        {
            //current node
            if (smn != null && smn.Title.Contains("<u>"))
            {
                smn.Title = smn.Title.Replace("<u>", "").Replace("</u>", "");
            }

            //parent nd
            if (smn.ParentNode != null)
            {
                if (smn.ParentNode.Title.Contains("<u>"))
                {
                    SiteMapNode gpn = smn.ParentNode;
                    smn.ParentNode.Title = smn.ParentNode.Title.Replace("<u>", "").Replace("</u>", "");
                    smn = ReplaceNodeText(gpn);
                }
            }
            return smn;
        }
        // fin prueba menu

        public static string UrlBase
        {
            get
            {
                HttpContext context = HttpContext.Current;
                string baseUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd('/') + '/';
                return baseUrl;
            }
        }

        public static Guid Pec
        {
            get
            {
                Guid valor = new Guid("86897FAE-E3A5-4CDA-BC28-0BFCE4E9AAFB");
                return valor;
            }
        }

        //public static String Usuario
        //{
        //    get
        //    {
        //        HttpContext context = HttpContext.Current;
        //        return context.User.Identity.Name;
        //    }
        //}

        public static Guid IdUsuarioLogueado
        {
            get
            {
                HttpContext context = HttpContext.Current;
                String nombre = context.User.Identity.Name;

                UsuariosLogic logic = new UsuariosLogic();
                return logic.ReadByNombre(nombre).ID;
            }
        }

        public static Guid Sucursal
        {
            get
            {
                Guid valor = Guid.Parse("73B724AF-F39D-4989-968B-068660143DAA");
                return valor;
            }
        }

        public static Guid RTPEC
        {
            get
            {
                Guid valor = Guid.Parse("F73EA3BD-F574-475E-9FA7-0C7F5B890966");
                return valor;
            }
        }

        //public static Guid Rol
        //{
        //    get
        //    {
        //        HttpContext context = HttpContext.Current;
        //        Guid valor = new Guid(context.User.Identity.Name.Split('|')[1].ToString());
        //        return valor;
        //    }
        //}

    }
}
