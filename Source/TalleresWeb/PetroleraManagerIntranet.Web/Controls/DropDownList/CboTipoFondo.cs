using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using TalleresWeb.Entities;
using TalleresWeb.Logic;
using CrossCutting.DatosDiscretos;

namespace PetroleraManagerIntranet.Web.Controls
{
    public class CboTipoFondo : DropDownList
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
            var tiposFondos = new List<TipoFondoView>()
            {
                new TipoFondoView(){ ID=TIPOSFONDOS.CONCAVO, Descripcion=TIPOSFONDOS.CONCAVO },
                new TipoFondoView(){ ID=TIPOSFONDOS.CONVEXO, Descripcion=TIPOSFONDOS.CONVEXO }
            };
            this.DataTextField = "Descripcion";
            this.DataValueField = "ID";

            this.DataSource = tiposFondos;
            this.DataBind();
        }
    }
}