using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TalleresWeb.Web.UI.Controls
{
    public class BtnSearch : ImageButton
    {
        public BtnSearch()
        {           
            this.ImageUrl = "~/Images/Iconos/buscar.png";
            this.CausesValidation = false;
        }
    }

}