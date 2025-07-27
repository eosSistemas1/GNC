using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Logic;
using PL.Fwk.Entities;

namespace PetroleraManager.Web.Controls
{
    public class CboMarcasCilindros : PLComboBox
    {
        public bool IsComboFilter { get; set; }

        public override void LoadData()
        {
            MarcasCilindrosLogic logic = new MarcasCilindrosLogic();
            var items = logic.ReadListView().OrderBy(x => x.Descripcion).ToList();

            if (IsComboFilter)
            { 
                ViewEntity item = new ViewEntity();
                item.ID = Guid.Empty;
                item.Descripcion = "Seleccione";
                items.Insert(0,item);
            }
            this.DataSource = items;
        }

    }
}