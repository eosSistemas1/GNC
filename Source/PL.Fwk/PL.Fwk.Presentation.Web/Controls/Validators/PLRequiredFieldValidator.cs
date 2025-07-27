using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace PL.Fwk.Presentation.Web.Controls.Validators
{
    public class PLRequiredFieldValidator:RequiredFieldValidator
    {
        public PLRequiredFieldValidator():base()
        {
            if (String.IsNullOrEmpty(ErrorMessage))
            {
                ErrorMessage = "&nbsp; &nbsp; * Campo Requerido";
                this.Visible = true;
                this.Display = ValidatorDisplay.Dynamic;
                this.Enabled = true;
            }
        }
        
        
    }
}
