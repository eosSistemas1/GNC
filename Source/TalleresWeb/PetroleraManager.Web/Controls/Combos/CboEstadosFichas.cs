using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Logic;

namespace PetroleraManager.Web.Controls
{
    public class CboEstadosFichas : PLComboBox
    {

        public bool IsComboFilter { get; set; }

        public override void LoadData()
        {
            EstadosFichasLogic logic = new EstadosFichasLogic();
            var lVe = logic.ReadListView().OrderBy(x => x.Descripcion).ToList();

            if (IsComboFilter)
            {
                lVe.Add(new PL.Fwk.Entities.ViewEntity { ID = Guid.Empty, Descripcion = "-- TODOS --" });
                this.DataSource = lVe;
            }
            else
            {
                this.DataSource = lVe;
            }
        }

    }
}