using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace PL.Fwk.Presentation.Web.Controls
{

    public class PLHidden : HtmlInputHidden
    {
        #region Properties

        public String Text
        {
            get { return this.Value.ToString(); }
            set { this.Value = value.ToString(); }
        }
        #endregion

    }

}
