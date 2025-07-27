using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PL.Fwk.Presentation.Web.Controls;
using System.Web.UI.WebControls;

namespace PetroleraManager.Web.Controls
{ 
    public class BtnAceptar : PLButton
    {     
        protected override void OnInit(EventArgs e)
        {            
            this.UseSubmitBehavior = false;
            this.Text = !String.IsNullOrEmpty(this.Text) ? "       " + this.Text : "       Aceptar";
            this.Height = Unit.Pixel(35);
            this.Style.Add("background", "transparent url(/Imagenes/Iconos/correcta.png) center left no-repeat;");
        }
    }
}