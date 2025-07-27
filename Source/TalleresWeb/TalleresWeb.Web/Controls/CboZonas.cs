using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Logic;
using TalleresWeb.Entities;
using PL.Fwk.Entities;

namespace TalleresWeb.Controls
{
    public class CboZonas : PLComboBox
    {

        public override void LoadData()
        {

            List<PL.Fwk.Entities.ViewEntity> dt = new List<PL.Fwk.Entities.ViewEntity>();
            ZonasExtendedView dr = new ZonasExtendedView();
            dr.ID = "";
            dr.Descripcion = "-- SELECCIONE --";
            dt.Add(dr);

            ZonasExtendedView drN = new ZonasExtendedView();
            drN.ID = "NORTE";
            drN.Descripcion = "NORTE";
            dt.Add(drN);

            ZonasExtendedView drS = new ZonasExtendedView();
            drS.ID = "SUR";
            drS.Descripcion = "SUR";
            dt.Add(drS);

            ZonasExtendedView drE = new ZonasExtendedView();
            drE.ID = "ESTE";
            drE.Descripcion = "ESTE";
            dt.Add(drE);

            ZonasExtendedView drO = new ZonasExtendedView();
            drO.ID = "OESTE";
            drO.Descripcion = "OESTE";
            dt.Add(drO);

            this.DataSource = dt;
        }
    }

    public class ZonasExtendedView : ViewEntity
    {
        public String ID { get; set; }
        public String Descripcion { get; set; }
    }
}