using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Logic;

namespace TalleresWeb.Controls
{
    public class CboTiposOperaciones : PLComboBox
    {
        public override void LoadData()
        {
            TiposOperacionesLogic logic = new TiposOperacionesLogic();
            this.DataSource = logic.ReadEVOperaciones();
        }
    }
}