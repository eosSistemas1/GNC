using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PetroleraManagerIntranet.Web.Account
{
    public partial class LogOut : Page
    {
        #region Methods

        protected void Page_Load(Object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
        }

        #endregion
    }
}