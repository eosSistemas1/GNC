using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace PL.Fwk.Presentation.Web.Controls
{
    public class PLButton : Button
    {
        #region Properties

        public String _ImageUrl;
        public String ImageUrl
        {
            get { return this._ImageUrl; }
            set { this._ImageUrl = value.ToString(); }
        }
        #endregion


        protected override void OnInit(EventArgs e)
        {
            if (_ImageUrl != null)
            {
                    this.Text = "       " + this.Text;
                    this.Height = Unit.Pixel(35);              
                    this.Style.Add("background", "transparent url(" + ImageUrl + ") center left no-repeat;");
            }

        }
    }
}
