using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Logic;
using TalleresWeb.Entities;
using PL.Fwk.Entities;

namespace PetroleraManager.Web.Controls
{
    public class CboPHPresiondePrueba : PLComboBox
    {

        public override void LoadData()
        {

            List<PL.Fwk.Entities.ViewEntity> dt = new List<PL.Fwk.Entities.ViewEntity>();
            PresiondePruebaExtendedView dr = new PresiondePruebaExtendedView();
            dr.ID = "300";
            dr.Descripcion = "300";
            dt.Add(dr);

            dr = new PresiondePruebaExtendedView();
            dr.ID = "375";
            dr.Descripcion = "375";
            dt.Add(dr);

            this.DataSource = dt;
        }
    }

    public class PresiondePruebaExtendedView : ViewEntity
    {
        public String ID { get; set; }
        public String Descripcion { get; set; }
    }
}