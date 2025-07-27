using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Presentation.Web.Pages;
using System.Web;

namespace TalleresWeb.Web
{
    public class PageBase : PLPageBase
    {
        protected override void OnLoad(EventArgs e)
        {

            if (!Context.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/login.aspx");
            }

            base.OnLoad(e);

        }

        public static Guid Usuario
        {
            get
            {
                //Guid valor = Guid.Parse("A934C971-DDEA-4401-B69D-B960615C1BC9");
                HttpContext context = HttpContext.Current;
                Guid valor = new Guid(context.User.Identity.Name.Split('|')[0].ToString());             
                return valor;
            }
        }
    }
}
