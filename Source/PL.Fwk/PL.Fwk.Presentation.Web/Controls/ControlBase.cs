using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace PL.Fwk.Presentation.Web.Controls
{
    public class ControlBase:WebControl
    {
        public ControlBase()
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            
            base.OnLoad(e);
            InitializeControl();
        }

        public virtual void InitializeControl()
        {
 
        }
    }
}
