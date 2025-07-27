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
    public class CboPHTipoFondo : PLComboBox
    {

        public override void LoadData()
        {

            List<PL.Fwk.Entities.ViewEntity> dt = new List<PL.Fwk.Entities.ViewEntity>();
            TipoFondoExtendedView dr = new TipoFondoExtendedView();
            dr.ID = "CONCAVO";
            dr.Descripcion = "CONCAVO";
            dt.Add(dr);

            dr = new TipoFondoExtendedView();
            dr.ID = "CONVEXO";
            dr.Descripcion = "CONVEXO";
            dt.Add(dr);

            this.DataSource = dt;
        }
    }

    public class TipoFondoExtendedView : ViewEntity
    {
        public String ID { get; set; }
        public String Descripcion { get; set; }
    }
}