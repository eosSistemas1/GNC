using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TalleresWeb.Web.Logic;

namespace TalleresWeb.Web.UI.Controls
{
    public class CboPEC : ComboBase
    {
        public override void LoadData()
        {
            PECLogic logic = new PECLogic();
            this.DataSource = logic.ReadListView().OrderBy(x => x.Descripcion).ToList();
        }
    }
}