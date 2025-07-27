using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Logic;
using CrossCutting.DatosDiscretos;

namespace PetroleraManager.Web.Controls
{
    public class CboEstadosFichasAsignar : PLComboBox
    {

        public bool IsComboFilter { get; set; }

        public override void LoadData()
        {
            EstadosFichasLogic logic = new EstadosFichasLogic();
            var lVe = logic.ReadListView()
                           .Where (f => f.ID == ESTADOSFICHAS.Asignada || f.ID == ESTADOSFICHAS.AsignadaConError || f.ID == ESTADOSFICHAS.RechazadaPorEnte)
                           .OrderBy (x => x.Descripcion).ToList();

            if (IsComboFilter)
            {
                lVe.Insert(0, new PL.Fwk.Entities.ViewEntity { ID = Guid.Empty, Descripcion = "-- TODOS --" });
                this.DataSource = lVe;
            }
            else
            {
                this.DataSource = lVe;
            }
        }

    }
}