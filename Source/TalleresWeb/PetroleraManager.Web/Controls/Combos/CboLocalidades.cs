using PL.Fwk.Entities;
using PL.Fwk.Presentation.Web.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using TalleresWeb.Entities;
using TalleresWeb.Logic;

namespace PetroleraManager.Web.Controls
{
    public class CboLocalidades : PLComboBox
    {
        #region Methods

        public override void LoadData()
        {
            LocalidadesLogic logic = new LocalidadesLogic();
            List<ViewEntity> dt = new List<ViewEntity>();

            var localidades = logic.ReadAll().OrderBy(x => x.Descripcion);

            foreach (Localidades loc in localidades)
            {
                String descripcion = loc.Descripcion + ", " + loc.Provincias.Descripcion;
                LocalidadesExtendedView dr = new LocalidadesExtendedView();
                dr.ID = loc.ID;
                dr.Descripcion = descripcion;
                dt.Add(dr);
            }

            this.DataSource = dt;
        }

        #endregion
    }
}