using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TalleresWeb.Web.UI.Account
{
    public partial class LogOut : PageBase
    {
        #region Methods

        protected void Page_Load(Object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("../index.aspx");        
        }

        #endregion
    }
}