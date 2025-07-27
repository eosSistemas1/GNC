using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Logic;

namespace TalleresWeb.Controls
{
    public class CboCRPC : PLComboBox
    {

        public override void LoadData()
        {
            CRPCLogic logic = new CRPCLogic();
            this.DataSource = logic.ReadListView();
        }

    }
}