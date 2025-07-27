using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TalleresWeb.Web.Logic;

namespace TalleresWeb.Web.UI.Controls
{
    public class CboCRPC : ComboBase
    {
        public override void LoadData()
        {
            CRPCLogic logic = new CRPCLogic();
            this.DataSource = logic.ReadListView().OrderBy(x => x.Descripcion).ToList();
        }
    }
}