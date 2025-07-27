using CrossCutting.DatosDiscretos;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;

namespace PetroleraManagerIntranet.Web.Controls
{
    public class CboZona : DropDownList
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
            var zonas = new List<ZonasView>()
            {
                new ZonasView(){ ID=ZONAS.Centro, Descripcion=ZONAS.Centro },
                new ZonasView(){ ID=ZONAS.Comisionista, Descripcion=ZONAS.Comisionista },
                new ZonasView(){ ID=ZONAS.Este, Descripcion=ZONAS.Este },
                new ZonasView(){ ID=ZONAS.Norte, Descripcion=ZONAS.Norte },
                new ZonasView(){ ID=ZONAS.Oeste, Descripcion=ZONAS.Oeste },
                new ZonasView(){ ID=ZONAS.Sur, Descripcion=ZONAS.Sur }
            };
            this.DataTextField = "Descripcion";
            this.DataValueField = "ID";

            this.DataSource = zonas;
            this.DataBind();
        }
    }
}