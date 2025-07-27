using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Logic;

namespace PetroleraManager.Web.Controls
{
    public class CboRT : PLComboBox
    {
        public override void LoadData()
        {
            RTLogic logic = new RTLogic();
            var rt = logic.ReadListView().OrderBy(x => x.Descripcion).ToList();
            //rt.Insert(0, new PL.Fwk.Entities.ViewEntity() { ID = Guid.Empty, Descripcion = "--Seleccione--"});
            this.DataSource = rt;
        }

    }
}