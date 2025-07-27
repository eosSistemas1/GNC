using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Logic;

namespace TalleresWeb.Controls
{
    public class CboMarcasVAL : PLComboBox
    {
        public bool IsComboFilter { get; set; }

        public override void LoadData()
        {
            MarcasValvulasLogic logic = new MarcasValvulasLogic();
            var lVe = logic.ReadListView();

            if (IsComboFilter)
            {
                lVe.Add(new PL.Fwk.Entities.ViewEntity { ID = Guid.Empty, Descripcion = "--TODOS--" });
                this.DataSource = lVe;
                this.SelectedValue = Guid.Empty;
            }
            else
            {
                this.DataSource = lVe;
            }
        }
    }
}