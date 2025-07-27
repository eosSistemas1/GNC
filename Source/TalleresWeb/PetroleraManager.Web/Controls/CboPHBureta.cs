using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Logic;
using TalleresWeb.Entities;
using PL.Fwk.Entities;
using CrossCutting.DatosDiscretos;

namespace PetroleraManager.Web.Controls
{
    public class CboPHBureta : PLComboBox
    {

        public override void LoadData()
        {

            List<PL.Fwk.Entities.ViewEntity> dt = new List<PL.Fwk.Entities.ViewEntity>();
            BuretaExtendedView dr = new BuretaExtendedView();
            String valor = (2170 - 8).ToString(GetDinamyc.FormatoNumerico2d);
            dr.ID = valor;
            dr.Descripcion = "1300 cc";
            dt.Add(dr);


            valor = (2170 * new Decimal(1.00785) - new Decimal(2.4119)).ToString(GetDinamyc.FormatoNumerico2d);
            dr = new BuretaExtendedView();
            dr.ID = valor;
            dr.Descripcion = "2000 cc";
            dt.Add(dr);

            this.DataSource = dt;
        }
    }

    public class BuretaExtendedView : ViewEntity
    {
        public String ID { get; set; }
        public String Descripcion { get; set; }
    }
}