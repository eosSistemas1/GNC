using System;
using System.Web.UI.WebControls;

namespace PetroleraManager.Web.Controls
{ 
    public class BtnHidden : Button
    {
        protected override void OnInit(EventArgs e)
        {            
            this.Style.Add("display", "none;");
        }
    }
}