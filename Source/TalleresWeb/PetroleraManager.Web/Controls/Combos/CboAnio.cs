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
    public class CboAnio : PLComboBox
    {

        public override void LoadData()
        {

            List<PL.Fwk.Entities.ViewEntity> dt = new List<PL.Fwk.Entities.ViewEntity>();
            AnioExtendedView dr = new AnioExtendedView();
            dr.ID = "-1";
            dr.Descripcion = "AÑO";
            dt.Add(dr);
            
            for (int i = DateTime.Now.Year; i >= 1980; i--)
            {
                AnioExtendedView dr2 = new AnioExtendedView();
                System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString());
                dr2.ID = i.ToString();
                dr2.Descripcion = i.ToString();
                dt.Add(dr2);
            }

            this.DataSource = dt;
        }
    }

    public class AnioExtendedView : ViewEntity
    {
        public String ID { get; set; }
        public String Descripcion { get; set; }
    }
}