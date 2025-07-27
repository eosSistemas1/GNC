using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TalleresWeb.Web.UI.Controls
{
    public class CboCantidadAVisualizar : DropDownList
    {
        public CboCantidadAVisualizar()
        {
            List<String> valores = new List<String>();
            valores.Add("5");
            valores.Add("10");
            valores.Add("25");
            valores.Add("50");
            valores.Add("100");
            this.DataSource = valores;
            this.DataBind();
        }
    }

}