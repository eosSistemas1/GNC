using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Logic;

namespace PetroleraManager.Web.Controls
{
    public class CboTalleres : PLComboBox
    {

        public override void LoadData()
        {
            TalleresLogic logic = new TalleresLogic();
            this.DataSource = logic.ReadListView().OrderBy(x => x.Descripcion).ToList();
        }
    }
}