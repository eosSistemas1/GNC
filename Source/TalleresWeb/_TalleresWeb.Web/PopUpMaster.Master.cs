using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace TalleresWeb.Consultas
{
    public partial class PopUpMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
    }
}