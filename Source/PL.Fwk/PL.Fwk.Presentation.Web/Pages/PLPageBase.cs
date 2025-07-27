using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PL.Fwk.Presentation.Web.Pages
{

    public class PLPageBase : System.Web.UI.Page
    {
        public PLPageBase(): base()
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!Context.User.Identity.IsAuthenticated)
            {
                HttpContext context = HttpContext.Current;
                string baseUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd('/') + '/';
                Response.Redirect(baseUrl + "Account/login.aspx");
            }
        }

    }
}
