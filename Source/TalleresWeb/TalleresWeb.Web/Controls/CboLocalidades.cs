using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Logic;
using TalleresWeb.Entities;
using PL.Fwk.Entities;

namespace TalleresWeb.Controls
{
    public class CboLocalidades: PLComboBox
    {

        public override void LoadData()
        {
            LocalidadesLogic logic = new LocalidadesLogic();
            List<ViewEntity> dt = new List<ViewEntity>();

            var localidades = logic.ReadAll().OrderBy(x => x.Provincias.Descripcion).ThenBy(x => x.Descripcion);

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
    }
}