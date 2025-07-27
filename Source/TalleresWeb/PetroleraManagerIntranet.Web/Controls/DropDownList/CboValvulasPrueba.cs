using PetroleraManagerIntranet.Web.PH.Proceso;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace PetroleraManagerIntranet.Web.Controls
{
    public class CboValvulasPrueba : DropDownList
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
            List<ValvulaCarga> dt = new List<ValvulaCarga>();
            
            var valvulas = Tablas.ValvulaCarga();

            foreach (var valvula in valvulas)
            {
                ValvulaCarga val = new ValvulaCarga();
                val.ID = valvula.ID;
                val.Descripcion = valvula.Descripcion;
                dt.Add(val);
            }

            this.DataTextField = "Descripcion";
            this.DataValueField = "ID";
            this.DataSource = dt;
            this.DataBind();
        }
    }
}