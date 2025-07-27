using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PL.Fwk.Presentation.Web.Controls;
using TalleresWeb.Logic;
using TalleresWeb.Entities;
using CrossCutting.DatosDiscretos;
using PL.Fwk.Entities;

namespace PetroleraManager.Web.Controls
{
    public class CboMSDB : PLComboBox
    {        
        public override void LoadData()
        {
            this.DataSource = Valores();
        }

        private List<ViewEntity> Valores()
        {
            List<ViewEntity> dt = new List<ViewEntity>();

            EstadosFichasExtendedView dr = new EstadosFichasExtendedView();
            dr.ID = MSDB.Montaje;
            dr.Descripcion = "MONTAJE";
            dt.Add(dr);

            EstadosFichasExtendedView dr1 = new EstadosFichasExtendedView();
            dr1.ID = MSDB.Sigue;
            dr1.Descripcion = "SIGUE";
            dt.Add(dr1);

            EstadosFichasExtendedView dr2 = new EstadosFichasExtendedView();
            dr2.ID = MSDB.Desmontaje;
            dr2.Descripcion = "DESMONTAJE";
            dt.Add(dr2);

            EstadosFichasExtendedView dr3 = new EstadosFichasExtendedView();
            dr3.ID = MSDB.Baja;
            dr3.Descripcion = "BAJA";
            dt.Add(dr3);

            return dt;
        }
    }
}