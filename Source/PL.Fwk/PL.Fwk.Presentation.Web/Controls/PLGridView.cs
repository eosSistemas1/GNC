using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace PL.Fwk.Presentation.Web.Controls
{
    public class PLGridView:GridView
    {
        #region Properties

        #endregion
        #region Constructor
        public PLGridView()
        {

        }        
        #endregion

        #region Methods

        public void AddBindField(String columnNanme, String headerText)
        {
            //this.AutoGenerateColumns = false;


            //BoundColumn column = new BoundColumn();
            //column.HeaderText = headerText;
            //column.DataField = columnNanme;

            //this.Columns.Add(column);
        }

        #endregion


    }
}
