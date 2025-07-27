using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TalleresWeb.Web.UI.UserControls
{
    public partial class Fotos : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hdnTallerID.Value = ((ViewEntity)HttpContext.Current.Session["TALLERID"]).ID.ToString();
        }
    }
}