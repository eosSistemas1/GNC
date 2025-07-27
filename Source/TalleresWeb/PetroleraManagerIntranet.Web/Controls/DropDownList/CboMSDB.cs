using CrossCutting.DatosDiscretos;
using PL.Fwk.Entities;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;

namespace PetroleraManagerIntranet.Web.Controls
{
    public class CboMSDB : DropDownList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (string.IsNullOrWhiteSpace(CssClass))
                this.Attributes.Add("class", "form-control nn");

            LoadData();
        }

        public void LoadData()
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

            this.DataTextField = "Descripcion";
            this.DataValueField = "ID";
            this.DataSource = dt;
            this.DataBind();
        }
    }
}